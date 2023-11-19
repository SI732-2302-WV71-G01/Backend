using AutoMapper;
using LearningCenter.API.Learning.Domain.Models;
using LearningCenter.API.Learning.Resources;

namespace LearningCenter.API.Learning.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Store, StoreResource>();
        CreateMap<StoreImage, StoreImageResource>();
        CreateMap<Product, ProductResource>();
        CreateMap<Purchase, PurchaseResource>();
        CreateMap<Sale, SaleResource>();
        CreateMap<Article, ArticleResource>();
    }
}