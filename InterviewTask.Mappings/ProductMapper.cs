using AutoMapper;
using InterviewTask.Data.Entities;
using InterviewTask.Models;
using InterviewTask.Models.Product;
using InterviewTask.ServiceModels;
using System;

namespace InterviewTask.Mappings
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Product, ProductDetails>();
            CreateMap<ProductDetails, Product>();

            CreateMap<Product, ProductItem>();
            CreateMap<ProductItem, Product>();

            CreateMap<ProductDetails, ProductServiceItemDetailsModel>();
            CreateMap<ProductServiceItemDetailsModel, ProductDetails>();
        }
    }
}