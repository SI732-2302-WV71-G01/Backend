using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Shared.Domain.Services.Communication;

namespace LearningCenter.API.Learning.Domain.Services.Communication;

public class StoreImageResponse : BaseResponse<StoreImage>
{
    public StoreImageResponse(string message) : base(message)
    {
    }

    public StoreImageResponse(StoreImage model) : base(model)
    {
    }
}