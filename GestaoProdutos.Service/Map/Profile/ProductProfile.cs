using AutoMapper;
using GestaoProduto.Dominio;
using GestaoProduto.Service.Model;

namespace GestaoProduto.Service.Map
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductResponse>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<ProductRequest, Product>().IgnoreAllPropertiesWithAnInaccessibleSetter();
        }
    }
}
