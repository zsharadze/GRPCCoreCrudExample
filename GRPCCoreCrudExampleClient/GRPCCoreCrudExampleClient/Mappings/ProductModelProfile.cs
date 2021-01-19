using AutoMapper;
using GRPCCoreCrudExampleServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRPCCoreCrudExampleClient.Mappings
{
    public class ProductModelProfile : Profile
    {
        public ProductModelProfile()
        {
            CreateMap<ProductMessage, ProductModel>();
            CreateMap<ProductModel, ProductMessage>();
            CreateMap<List<ProductMessage>, List<ProductModel>>();

            CreateMap<ProductsResponce, List<ProductModel>>().IncludeMembers(s => s, s => s.Items.ToList());
            CreateMap<List<ProductModel>, ProductsResponce>().ForMember(a => a.Items, y => y.MapFrom(e => e));
        }
    }
}
