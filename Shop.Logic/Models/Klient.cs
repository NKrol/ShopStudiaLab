using System;
using System.Collections.Generic;

#nullable disable

namespace Shop.Logic.Models
{
    public partial class Klient : RecordBase
    {
        public Klient()
        {
            KlientKontos = new HashSet<KlientKonto>();
            Zamowienia = new HashSet<Zamowienium>();
        }
        
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public decimal? Nip { get; set; }
        public string NazwaFirmy { get; set; }
        public string KodPocztowy { get; set; }
        public string Miasto { get; set; }
        public string Mail { get; set; }
        public string Telefon { get; set; }

        public virtual ICollection<KlientKonto> KlientKontos { get; set; }
        public virtual ICollection<Zamowienium> Zamowienia { get; set; }
    }
}
