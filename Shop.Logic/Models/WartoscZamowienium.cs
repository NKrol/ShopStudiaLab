using System;
using System.Collections.Generic;

#nullable disable

namespace Shop.Logic.Models
{
    public partial class WartoscZamowienium
    {
        public int NumerZamówienia { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Mail { get; set; }
        public string KosztCałkowityBrutto { get; set; }
        public string KosztCałkowityNetto { get; set; }
        public string KosztCałkowityStawkiVat { get; set; }
    }
}
