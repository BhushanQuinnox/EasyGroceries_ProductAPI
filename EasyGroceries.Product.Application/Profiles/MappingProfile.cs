using AutoMapper;
using EasyGroceries.Product.Application.DTOs;
using EasyGroceries.Product.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyGroceries.Product.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductInfo, ProductInfoDto>().ReverseMap();
        }
    }
}
