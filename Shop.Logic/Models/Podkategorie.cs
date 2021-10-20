using System;
using System.Collections.Generic;

#nullable disable

namespace Shop.Logic.Models
{
    public partial class Podkategorie : RecordBase
    {
        public Podkategorie()
        {
            Produkts = new HashSet<Produkt>();
        }
        
        public int KategorieId { get; set; }
        public string NazwaPodkategorii { get; set; }

        public virtual Kategorie Kategorie { get; set; }
        public virtual ICollection<Produkt> Produkts { get; set; }
    }
}
