using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using InterviewTask.Data;
using InterviewTask.Data.Entities;
using InterviewTask.Data.Paging;
using InterviewTask.Models;
using InterviewTask.Models.Paging;
using InterviewTask.Models.Product;
using InterviewTask.Models.ProductRequest;
using InterviewTask.Models.ProductResponse;
using InterviewTask.Services;
using InterviewTask.WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;

namespace InterviewTask.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductService productsService;
        private readonly IMapper mapper;
        public ProductController(
            IUnitOfWork unitOfWork,
            IProductService productsService,
            IMapper mapper) : base(unitOfWork)
        {
            this.productsService = productsService;
            this.mapper = mapper;

        }

        [HttpPost]
        [Route("GetProducts")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ProductItem>>> GetProducts(ProductRequestModel requestModel)
        {
            try
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
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet]
        [Route("GetProduct/{productId}")]
        [AllowAnonymous]
        public async Task<ActionResult<ProductDetails>> GetProduct(int productId)
        {
            try
            {
                var product = await productsService.GetProductById(productId);
                if (product == null)
                    return NotFound("The provided product deosn't excist");
                var mappedProduct = mapper.Map<Product, ProductDetails>(product);
                return Ok(mappedProduct);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
           
        }

        [Authorize]
        [HttpPost]
        [Route("NewProduct")]
        public async Task<ActionResult<ProductDetails>> NewProduct(ProductDetails product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var mapped = mapper.Map<ProductDetails, Product>(product);
                    var newProduct = await productsService.CreateNewProduct(mapped);
                    var mappedCreatedProduct = mapper.Map<Product, ProductDetails>(newProduct);
                    return Ok(mappedCreatedProduct);
                }
                else
                {
                    var errors = new List<string>();
                    foreach (var state in ModelState)
                    {
                        foreach (var error in state.Value.Errors)
                        {
                            errors.Add(error.ErrorMessage);
                        }
                    }
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        [Authorize]
        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(ProductDetails product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var mapped = mapper.Map<ProductDetails, Product>(product);
                    var _product = productsService.GetProductById(product.Id);
                    if (_product == null)
                    {
                        return NotFound("Provided product doesn't exist");
                    }
                    await productsService.UpdateProduct(mapped, product.RowVersion);

                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [Authorize]
        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(ProductDetails product)
        {
            try
            {
                var _product = productsService.GetProductById(product.Id);
                if (_product == null)
                {
                    return NotFound("Provided product doesn't exist");
                }
                await productsService.DeleteProduct(product.Id, product.RowVersion);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

    }
}