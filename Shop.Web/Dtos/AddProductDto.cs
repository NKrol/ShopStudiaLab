using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Web.Dtos
{
    public class AddProductDto
    {
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public double ProductPrice { get; set; }
        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
    }
}
