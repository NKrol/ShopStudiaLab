using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Web.Entities.Model;
using Shop.Web.Dtos;

namespace Shop.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly Xkom_ProjektContext _dbContext;

        public CategoryController(Xkom_ProjektContext dbContext)
        {
            _dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var category = _dbContext.Kategories.Include(x => x.Podkategories).ToList();

            List<CategoryDto> dtoList = new();

            category.ForEach(x =>
            {

                var dto = new CategoryDto()
                {
                    Category = x.NazwaKategorii,
                    SubCategory = x.Podkategories.Select(c => c.NazwaPodkategorii).ToList()
                };

                dtoList.Add(dto);
            });

            return Ok(dtoList);
        }
    }
}
