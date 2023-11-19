using AutoMapper;
using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Security.Authorization.Handlers.Interfaces;
using LearningCenter.API.Security.Domain.Models;
using LearningCenter.API.Security.Domain.Repositories;
using LearningCenter.API.Security.Domain.Services;
using LearningCenter.API.Security.Domain.Services.Communication;
using LearningCenter.API.Security.Exceptions;
using BCryptNet = BCrypt.Net.BCrypt;

namespace LearningCenter.API.Security.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository; 
    private readonly IRoleRepository _roleRepository; 
    private readonly IUnitOfWork _unitOfWork; 

    private readonly IJwtHandler _jwtHandler; 
    private readonly IMapper _mapper; 

    
    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IJwtHandler jwtHandler, IMapper mapper, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _jwtHandler = jwtHandler;
        _mapper = mapper;
        _roleRepository = roleRepository;
    }
    
    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
    {
        var user = await _userRepository.FindByUsernameAsync(request.Username); 

        
        if (user == null || !BCryptNet.Verify(request.Password, user.PasswordHash))
        {
            throw new AppException("Username or password is incorrect"); 
        }

        
        var response = _mapper.Map<AuthenticateResponse>(user);
        response.Token = _jwtHandler.GenerateToken(user);
        return response;
    }

    
    public async Task<IEnumerable<User>> ListAsync()
    {
        return await _userRepository.ListAsync(); 
    }
    
    public async Task<User> GetByIdAsync(int id)
    {
        var user = await _userRepository.FindByIdAsync(id); 

        if (user == null)
        {
            throw new KeyNotFoundException("User not found"); 
        }

        return user;
    }

    public async Task<User> GetByIdAsyncV2(int id)
    {
        if (!_userRepository.ExistsById(id)) return null;
        
        var user = await _userRepository.FindByIdAsync(id);
        return user;

    }
    
    public async Task RegisterAsync(RegisterRequest request)
    {
        if (_userRepository.ExistsByUsername(request.Username))
        {
            throw new AppException("Username '" + request.Username + "' is already taken"); 
        }
        
        var existingRole = await _roleRepository.FindByIdAsync(request.RoleId);
        if (existingRole == null)
        {
            throw new AppException("Role with id '" + request.RoleId + "' does not exist"); 
        }
        
        var user = _mapper.Map<User>(request);
        
        user.PasswordHash = BCryptNet.HashPassword(request.Password);
        
        try
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while saving the user: {e.Message}"); 
        }
    }
    
    public async Task UpdateAsync(int id, UpdateRequest request)
    {
        var user = GetById(id);
        
        var userWithSameName = await _userRepository.FindByUsernameAsync(request.Username);
        if (_userRepository.ExistsByUsername(request.Username) && id != userWithSameName.Id)
        {
            throw new AppException("Username '" + request.Username + "' is already taken"); 
        }
        
        var existingRole = await _roleRepository.FindByIdAsync(request.RoleId);
        if (existingRole == null)
        {
            throw new AppException("Role with id '" + request.RoleId + "' does not exist"); 
        }
        
        if (!string.IsNullOrEmpty(request.Password))
        {
            user.PasswordHash = BCryptNet.HashPassword(request.Password);
        }
        
        _mapper.Map(request, user);
        try
        {
            _userRepository.Update(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while updating the user: {e.Message}"); 
        }
    }
    
    public async Task DeleteAsync(int id)
    {
        var user = GetById(id);

        try
        {
            _userRepository.Remove(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while deleting the user: {e.Message}"); 
        }
    }
    
    private User GetById(int id)
    {
        var user = _userRepository.FindById(id);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found"); 
        }
        return user;
    }
}