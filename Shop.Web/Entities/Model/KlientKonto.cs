using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

#nullable disable

namespace Shop.Web.Entities.Model
{
    public class KlientKonto : RecordBase
    {
        public string Mail { get; set; }
        public byte[] Haslo { get; set; }
        public Guid Sol { get; set; }
        public int KlientId { get; set; }

        public virtual Klient Klient { get; set; }




        public static byte[] Encrypt(string input)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(input);
            SHA512 sha = new SHA512Managed();
            var b = sha.ComputeHash(bytes);
            return b;
        }
    }
}
