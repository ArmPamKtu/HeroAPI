using AutoMapper;
using Db.Entities;
using Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeroUnitTests
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Product, ProductDto>().ReverseMap()
                .ForMember(x => x.ProductVersion, y => y.Ignore()).ReverseMap();
            CreateMap<ProductVersion, ProductVersionDto>().ReverseMap();

            CreateMissingTypeMaps = true;
        }
    }
}
