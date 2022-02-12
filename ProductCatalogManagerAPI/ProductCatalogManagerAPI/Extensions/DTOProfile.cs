using AutoMapper;
using ProductCatalogManagerAPI.Models;

namespace ProductCatalogManagerAPI.Extensions
{
    public class DTOProfile : Profile
    {
        public DTOProfile()
        {
            CreateMap<ProductDTO, Product>();
        }
    }
}
