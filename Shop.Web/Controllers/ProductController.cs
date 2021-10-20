using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Repository;

namespace Shop.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductRepository _productRepository;

        public ProductController(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] int skip,[FromQuery] int take)
        {
            var products = _productRepository.GetAll();

            products = products.Skip(skip).Take(take);

            return Json(products);
        }
    }
}
