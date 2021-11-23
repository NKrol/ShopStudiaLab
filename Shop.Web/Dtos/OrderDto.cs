using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Web.Dtos
{
    public class OrderDto
    {
        public List<int> ProductsId { get; set; }
        public int ClientId { get; set; }
        public int Quantity { get; set; }
    }
}
