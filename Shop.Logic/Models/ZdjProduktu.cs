using System;
using System.Collections.Generic;

#nullable disable

namespace Shop.Logic.Models
{
    public partial class ZdjProduktu : RecordBase
    {
        public int ProduktId { get; set; }
        public string PathDoZdj { get; set; }

        public virtual Produkt Produkt { get; set; }
    }
}
