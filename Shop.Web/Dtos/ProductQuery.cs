using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Web.Dtos
{
    public class ProductQuery
    {
        public string SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public bool SortName { get; set; }
        public bool SortPrice { get; set; }
        public bool Asc { get; set; } = true;
    }
}
