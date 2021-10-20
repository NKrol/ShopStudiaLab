using System;
using System.Collections.Generic;

#nullable disable

namespace Shop.Logic.Models
{
    public partial class Produkt : RecordBase
    {
        public Produkt()
        {
            Cenas = new HashSet<Cena>();
            Iloscs = new HashSet<Ilosc>();
            ProduktOpis = new HashSet<ProduktOpi>();
            Zamowienia = new HashSet<Zamowienium>();
            ZdjProduktus = new HashSet<ZdjProduktu>();
        }
        
        public string NazwaProduktu { get; set; }
        public decimal KodProduktu { get; set; }
        public int KategorieId { get; set; }
        public int PodkategorieId { get; set; }

        public virtual Kategorie Kategorie { get; set; }
        public virtual Podkategorie Podkategorie { get; set; }
        public virtual ICollection<Cena> Cenas { get; set; }
        public virtual ICollection<Ilosc> Iloscs { get; set; }
        public virtual ICollection<ProduktOpi> ProduktOpis { get; set; }
        public virtual ICollection<Zamowienium> Zamowienia { get; set; }
        public virtual ICollection<ZdjProduktu> ZdjProduktus { get; set; }
    }
}
