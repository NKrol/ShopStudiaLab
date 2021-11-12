using System;
using System.Collections.Generic;

#nullable disable

namespace Shop.Web.Entities.Model
{
    public partial class Kategorie : RecordBase
    {
        public Kategorie()
        {
            Podkategories = new HashSet<Podkategorie>();
            Produkts = new HashSet<Produkt>();
        }
        
        public string NazwaKategorii { get; set; }

        public virtual ICollection<Podkategorie> Podkategories { get; set; }
        public virtual ICollection<Produkt> Produkts { get; set; }
    }
}
