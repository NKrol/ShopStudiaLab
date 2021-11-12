using System;
using System.Collections.Generic;

#nullable disable

namespace Shop.Web.Entities.Model
{
    public partial class StatusKoszykow
    {
        public string Mail { get; set; }
        public string Telefon { get; set; }
        public string NazwaProduktu { get; set; }
        public double? CenaBrutto { get; set; }
        public int Ilosc { get; set; }
        public double? Wartosc { get; set; }
        public string StatusPlatnosci { get; set; }
        public string NazwaStatusuZamowienia { get; set; }
        public int ZamowienieId { get; set; }
    }
}
