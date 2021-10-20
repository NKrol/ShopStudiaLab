using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Logic.Models;

namespace Shop.Repository
{
    public abstract class RepositoryBase<T> where T : RecordBase
    {
        protected Xkom_ProjektContext _db;

        /// <summary>
        /// Konstruktor wykorzystywany przez klasy dziedziczące
        /// </summary>
        /// <param name="db"></param>
        protected RepositoryBase(Xkom_ProjektContext db)
        {
            _db = db;
        }
        /// <summary>
        /// Zapisywanie zmian
        /// </summary>
        /// <returns></returns>
        public virtual async Task SaveChangesAsync(bool skipSave = false)
        {
            if (!skipSave)
            {
                await _db.SaveChangesAsync();
            }
        }
    }
}
