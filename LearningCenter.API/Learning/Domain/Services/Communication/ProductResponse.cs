using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Shared.Domain.Services.Communication;

namespace LearningCenter.API.Learning.Domain.Services.Communication;

public class ProductResponse : BaseResponse<Product>
{
    public ProductResponse(string message) : base(message)
    {
    }

    public ProductResponse(Product resource) : base(resource)
    {
    }
}