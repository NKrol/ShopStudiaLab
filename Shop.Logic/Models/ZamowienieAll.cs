using System;
using System.Collections.Generic;

#nullable disable

namespace Shop.Logic.Models
{
    public partial class ZamowienieAll
    {
        public int NumerZamówienia { get; set; }
        public string NazwaProduktu { get; set; }
        public double? CenaJednostkowaBrutto { get; set; }
        public double? CenaCałkowitaBrutto { get; set; }
        public double? CenaCałkowitaNetto { get; set; }
        public int Ilość { get; set; }
    }
}
