using System;
using System.Collections.Generic;

#nullable disable

namespace Shop.Logic.Models
{
    public partial class Zamowienium : RecordBase
    {
        public int ProduktId { get; set; }
        public int CenaId { get; set; }
        public int Ilosc { get; set; }
        public DateTime? DataZamowienia { get; set; }
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
