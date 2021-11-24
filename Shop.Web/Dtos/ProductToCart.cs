using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Web.Dtos
{
    public class ProductToCart
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string BruttoPrice { get; set; }

    }
}
