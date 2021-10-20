using System;
using System.Collections.Generic;

#nullable disable

namespace Shop.Logic.Models
{
    public partial class KlientKonto : RecordBase
    {
        public string Mail { get; set; }
        public byte[] Haslo { get; set; }
        public Guid Sol { get; set; }
        public int KlientId { get; set; }

        public virtual Klient Klient { get; set; }
    }
}
