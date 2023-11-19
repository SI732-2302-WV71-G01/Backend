using AutoMapper;
using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Resources;

namespace LearningCenter.API.Learning.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveStoreResource, Store>();
        CreateMap<SaveStoreImageResource, StoreImage>();
        CreateMap<SaveProductResource, Product>();
        CreateMap<SavePurchaseResource, Purchase>();
        CreateMap<SaveSaleResource, Sale>();
        CreateMap<SaveArticleResource, Article>();
    }
}