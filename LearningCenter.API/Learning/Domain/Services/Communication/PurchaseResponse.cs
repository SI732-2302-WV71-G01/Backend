using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Shared.Domain.Services.Communication;

namespace LearningCenter.API.Learning.Domain.Services.Communication;

public class PurchaseResponse : BaseResponse<Purchase>
{
    public PurchaseResponse(string message) : base(message)
    {
    }

    public PurchaseResponse(Purchase model) : base(model)
    {
    }
}