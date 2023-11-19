using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Shared.Domain.Services.Communication;

namespace LearningCenter.API.Learning.Domain.Services.Communication;

public class SaleResponse : BaseResponse<Sale>
{
    public SaleResponse(string message) : base(message)
    {
    }

    public SaleResponse(Sale model) : base(model)
    {
    }
}