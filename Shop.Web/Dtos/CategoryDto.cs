using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Web.Dtos
{
    public class CategoryDto
    {
        public string Category { get; set; }
        public List<string> SubCategory { get; set; }
    }
}
