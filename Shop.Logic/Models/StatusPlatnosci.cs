using System;
using System.Collections.Generic;

#nullable disable

namespace Shop.Logic.Models
{
    public partial class StatusPlatnosci : RecordBase
    {
        public StatusPlatnosci()
        {
            Zamowienia = new HashSet<Zamowienium>();
        }
        
        public string StatusPlatnosci1 { get; set; }

        public virtual ICollection<Zamowienium> Zamowienia { get; set; }
    }
}
