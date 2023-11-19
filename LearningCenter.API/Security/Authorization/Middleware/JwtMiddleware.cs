using LearningCenter.API.Security.Authorization.Handlers.Interfaces;
using LearningCenter.API.Security.Authorization.Settings;
using LearningCenter.API.Security.Domain.Services;
using LearningCenter.API.Security.Exceptions;
using Microsoft.Extensions.Options;

namespace LearningCenter.API.Security.Authorization.Middleware;

public class JwtMiddleware
{   
    private readonly RequestDelegate _next;
    private readonly AppSettings _appSettings;

    public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
    {
        _next = next;
        _appSettings = appSettings.Value;
    }
    
    public async Task Invoke(HttpContext context, IUserService userService, IJwtHandler handler)
    {
        Console.WriteLine("HOLSA");
        context.Items["Info"] = "null";
        
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var userId = handler.ValidateToken(token);
        
        if (userId != null && userId != -1) // el userId es null cuando el token es null , -1 cuando es un token invalido
        {
            var existingUser = await userService.GetByIdAsyncV2(userId.Value);
            if (existingUser == null)
            {
                context.Items["Info"] = "Expired Token";
            }
            else
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = existingUser;
            }
        }
        else if (userId == -1)//Token Invalido
        {
            context.Items["Info"] = "Invalid token";
        }
        else if(userId == null) //Token es null
            context.Items["User"] = null;
        
        await _next(context);
    }
}