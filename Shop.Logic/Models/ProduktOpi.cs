using System;
using System.Collections.Generic;

#nullable disable

namespace Shop.Logic.Models
{
    public partial class ProduktOpi : RecordBase
    {
        public int ProduktId { get; set; }
        public string Opis { get; set; }

        public virtual Produkt Produkt { get; set; }
    }
}
