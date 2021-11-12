using System;
using System.Collections.Generic;

#nullable disable

namespace Shop.Web.Entities.Model
{
    public partial class StatusZamowienium : RecordBase
    {
        public StatusZamowienium()
        {
            Zamowienia = new HashSet<Zamowienium>();
        }
        
        public string NazwaStatusuZamowienia { get; set; }

        public virtual ICollection<Zamowienium> Zamowienia { get; set; }
    }
}
