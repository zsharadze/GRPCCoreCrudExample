using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRPCCoreCrudExampleServer.Mappings
{
    public class ProductModelProfile : Profile
    {
        public ProductModelProfile()
        {
            // Add as many of these lines as you need to map your objects           
            CreateMap<ProductMessage, ProductModel>().ReverseMap();            
            CreateMap<List<ProductModel>, ProductsResponce>().ForMember(a => a.Items, y => y.MapFrom(e => e));
        }
    }
}
