namespace LearningCenter.API.Shared.Domain.Services.Communication;

public abstract class BaseResponse<T>
{
    protected BaseResponse(string message)
    {
        Success = false;
        Message = message;
        Model = default;
    }

    protected BaseResponse(T model)
    { 
        Success = true;
        Message = string.Empty; 
        Model = model;
    }

    public bool Success { get; set; }
    public string Message { get; set; }
    public T Model { get; set; }
}
