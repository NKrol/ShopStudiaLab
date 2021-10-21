using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Web.Dtos
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public decimal ProductCode { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public double BruttoPrice { get; set; }
        public double NettoPrice { get; set; }
        public int Quantity { get; set; }
        public string ImgPath { get; set; }
    }
}
