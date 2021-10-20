using System;
using System.Collections.Generic;

#nullable disable

namespace Shop.Logic.Models
{
    public partial class Ilosc : RecordBase
    {
        public int ProduktId { get; set; }
        public int Ilosc1 { get; set; }

        public virtual Produkt Produkt { get; set; }
    }
}
