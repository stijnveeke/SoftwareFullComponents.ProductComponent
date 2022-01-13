using AutoMapper;
using DataModels;
using ProductComponent.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ProductComponent.Profiles
{
    public class ProductProfile: Profile
    {
        [ExcludeFromCodeCoverage]
        public ProductProfile()
        {
            CreateMap<Product, ProductRead>();
            CreateMap<ProductCreate, Product>();
            CreateMap<ProductEdit, Product>();
        }
    }
}
