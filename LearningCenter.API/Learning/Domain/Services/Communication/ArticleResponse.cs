using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Shared.Domain.Services.Communication;

namespace LearningCenter.API.Learning.Domain.Services.Communication;

public class ArticleResponse: BaseResponse<Article>
{
    public ArticleResponse(string message) : base(message)
    {
    }

    public ArticleResponse(Article model) : base(model)
    {
    }
}