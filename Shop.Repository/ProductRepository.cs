using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Logic.Models;

namespace Shop.Repository
{
    public class ProductRepository : ExtensionRepository<Produkt>
    {
        public ProductRepository(Xkom_ProjektContext db) : base(db)
        {
        }


        public virtual IEnumerable<Produkt> GetAll(int skip, int take)
        {
            return _db.Set<Produkt>().Skip(skip).Take(take);
        }

        public virtual IEnumerable<Produkt> GetSkipTake(int skip, int take, string q)
        {
            var cos = base.Where(x => x.NazwaProduktu.Contains(q ?? "")).Skip(skip).Take(take);
            return cos;
        }
    }
}
