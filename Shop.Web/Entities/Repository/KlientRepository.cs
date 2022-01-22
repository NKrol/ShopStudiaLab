using Shop.Web.Entities.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Entities.Repository
{
    public class KlientRepository : ExtensionRepository<Klient>
    {
        public KlientRepository(Xkom_ProjektContext dbContext) : base(dbContext)
        {

        }
    }
}
