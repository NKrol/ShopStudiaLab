using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Logic.Models;

namespace Shop.Repository
{
    public class QuantityRepository : ExtensionRepository<Ilosc>
    {
        public QuantityRepository(Xkom_ProjektContext db) : base(db)
        {
        }
    }
}
