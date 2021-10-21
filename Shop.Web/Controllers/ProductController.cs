using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Shop.Repository;
using Shop.Web.Dtos;
using Shop.Web.Services;

namespace Shop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] ProductQuery query)
        {
            var products = _productService.GetProducts(query);

            return Ok(products);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var result = await _productService.GetProduct(id);

            return Ok(result);
        }
    }
}
