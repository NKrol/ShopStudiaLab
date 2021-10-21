using System;
using System.Collections.Generic;

#nullable disable

namespace Shop.Logic.Models
{
    public partial class Produkt : RecordBase
    {
        public string NazwaProduktu { get; set; }
        public decimal KodProduktu { get; set; }
        public int KategorieId { get; set; }
        public int PodkategorieId { get; set; }
        public virtual Kategorie Kategorie { get; set; }
        public virtual Podkategorie Podkategorie { get; set; }
        public virtual Cena Cena { get; set; }
        public virtual Ilosc Ilosc { get; set; }
        public virtual ProduktOpi ProduktOpi { get; set; }
        public virtual ZdjProduktu ZdjProduktu { get; set; }
    }
}
