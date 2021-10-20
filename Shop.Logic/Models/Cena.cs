using System;
using System.Collections.Generic;

#nullable disable

namespace Shop.Logic.Models
{
    public partial class Cena : RecordBase
    {
        public Cena()
        {
            Zamowienia = new HashSet<Zamowienium>();
        }

        public int? ProduktId { get; set; }
        public double CenaBrutto { get; set; }
        public double StawkaVat { get; set; }
        public double CenaNetto { get; set; }
        public double WartoscNetto { get; set; }

        public virtual Produkt Produkt { get; set; }
        public virtual ICollection<Zamowienium> Zamowienia { get; set; }
    }
}
