using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Web.Entities.Model;

namespace Shop.Web.Entities.Repository
{
    public class PhotoRepository : ExtensionRepository<ZdjProduktu>
    {
        public PhotoRepository(Xkom_ProjektContext db) : base(db)
        {
        }
    }
}
