using InterviewTask.Data;
using InterviewTask.Data.Entities;
using InterviewTask.Data.Paging;
using InterviewTask.Models;
using InterviewTask.Models.Filtering;
using InterviewTask.Models.Paging;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InterviewTask.Services
{
    public class ProductService : ServiceBase, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
           
        }

        public async Task<IEnumerable<Product>> GetAllProducts(Pagination pagination, Filter filter)
        {

            var _products = await unitOfWork.ProductRepository.GetAll(pagination, filter).ToListAsync();

            return _products;
        }

        public async Task<Product> GetProductById(long Id)
        {

            var _product = await unitOfWork.ProductRepository.Get(Id);

            return _product;
        }

        public async Task<Product> CreateNewProduct(Product product)
        {
            var _product = await unitOfWork.ProductRepository.Add(product);
            await unitOfWork.SaveAsync();
            return _product;
        }

        public async Task UpdateProduct(Product product, byte[] RowVersion)
        {
            var _product = await GetProductById(product.Id);
            if (_product != null)
            {
                unitOfWork.ProductRepository.Update(product, RowVersion);
                await unitOfWork.SaveAsync();
            }
        }

        public async Task DeleteProduct(long Id, byte[] RowVersion)
        {
            var product = await unitOfWork.ProductRepository.Get(Id);
            if (product != null)
            {
                unitOfWork.ProductRepository.Remove(product, RowVersion);
                await unitOfWork.SaveAsync();
            }
        }

    }
}
