using InterviewTask.Data.Entities;
using InterviewTask.Data.Paging;
using InterviewTask.Models;
using InterviewTask.Models.Filtering;
using InterviewTask.Models.Paging;
using InterviewTask.ServiceModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterviewTask.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProducts(Pagination pagination, Filter filter);

        Task<Product> GetProductById(long Id);

        Task<Product> CreateNewProduct(Product product);

        Task UpdateProduct(Product product);
    }
}