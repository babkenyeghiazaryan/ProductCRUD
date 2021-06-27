using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using InterviewTask.Data;
using InterviewTask.Data.Entities;
using InterviewTask.Data.Paging;
using InterviewTask.Models.Paging;
using InterviewTask.Models.Product;
using InterviewTask.Models.ProductRequest;
using InterviewTask.Models.ProductResponse;
using InterviewTask.Services;
using InterviewTask.WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace InterviewTask.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        private readonly IProductService productsService;
        private readonly IMapper mapper;
        public ProductsController(
            IUnitOfWork unitOfWork,
            IProductService productsService,
            IMapper mapper) : base(unitOfWork)
        {
            this.productsService = productsService;
            this.mapper = mapper;

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductItem>>> GetProducts(ProductRequestModel requestModel)
        {
            var mappedPaging = mapper.Map<PagingModel, Pagination>(requestModel.paging);

            var allProducts = await productsService.GetAllProducts(mappedPaging, requestModel.filter);
            var mappedProducts = mapper.Map<IEnumerable<Product>, IEnumerable<ProductItem>>(allProducts);
            requestModel.paging.Count = mappedProducts.Count();
            var responseModel = new ProductListResponsePaging()
            {
                products = mappedProducts,
                paging = requestModel.paging,
                filtering = requestModel.filter
            };
            return Ok(responseModel);
        }
    }
}