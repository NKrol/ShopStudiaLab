using System;
using System.Collections.Generic;

#nullable disable

namespace Shop.Web.Entities.Model
{
    public partial class ProduktAll
    {
        public string NazwaProduktu { get; set; }
        public string OpisProduktu { get; set; }
        public string Kategoria { get; set; }
        public string Podkategoria { get; set; }
        public double? CenaBrutto { get; set; }
        public string CenaNetto { get; set; }
        public int? StawkaVat { get; set; }
    }
}
