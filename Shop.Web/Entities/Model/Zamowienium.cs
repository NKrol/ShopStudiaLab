using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Shop.Web.Entities.Model
{
    public partial class Zamowienium : RecordBase
    { 
        public int jakiezamow_id { get; set; }
        public int ProduktId { get; set; }
        public int CenaId { get; set; }
        public int Ilosc { get; set; }
        public DateTime? DataZamowienia { get; set; } = DateTime.Now;
        public int IdKlienta { get; set; }
        public int StatusPlatnosciId { get; set; }
        public int StatusZamowieniaId { get; set; }

        public virtual Cena Cena { get; set; }
        public virtual Klient IdKlientaNavigation { get; set; }
        public virtual Produkt Produkt { get; set; }
        public virtual StatusPlatnosci StatusPlatnosci { get; set; }
        public virtual StatusZamowienium StatusZamowienia { get; set; }
    }
}
