using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Shared.Domain.Services.Communication;

namespace LearningCenter.API.Learning.Domain.Services.Communication;

public class StoreResponse : BaseResponse<Store>
{
    public StoreResponse(string message) : base(message)
    {
    }

    public StoreResponse(Store model) : base(model)
    {
    }
}