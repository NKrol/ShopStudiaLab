using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shop.Web.Entities.Model;

namespace Shop.Web.Entities.Repository
{
    public class ExtensionRepository<T> : RepositoryBase<T> where T : RecordBase
    {
        public ExtensionRepository(Xkom_ProjektContext db) : base(db)
        {
        }

        /// <summary>
        /// Dodawanie danych
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual async Task<T> AddAsync(T value)
        {
            var r = await _db.AddAsync<T>(value);
            await SaveChangesAsync();
            return r.Entity;
        }

        /// <summary>
        /// Aktualizacja danych
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual async Task<T> Update(T value)
        {
            _db.Entry(value).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _db.Update<T>(value);
            await SaveChangesAsync();
            return value;
        }



        /// <summary>
        /// Pobieranie wszystkich danych
        /// </summary>
        /// <returns>Lista rekordów</returns>
        protected virtual IEnumerable<T> GetAll()
        {
            return _db.Set<T>();
        }

        /// <summary>
        /// Wyszukiwanie pojedynczego rekordu po Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Pojedynczy rekord</returns>
        public virtual async Task<T> FindAsync(int id)
        {
            return await _db.FindAsync<T>(id);
        }

        /// <summary>
        /// Wyszukiwanie pierwszego rekordu używając różnych kryteriów
        /// </summary>
        /// <param name="expression">Kryteria</param>
        /// <returns>Pojedynczy rekord</returns>
        public virtual T Find(Func<T, bool> expression)
        {
            return Where(expression).FirstOrDefault();
        }

        /// <summary>
        /// Pobieranie listy rekordów używając różnych kryteriów
        /// </summary>
        /// <param name="expression">Kryteria</param>
        /// <returns>Lista rekordów</returns>
        public virtual IEnumerable<T> Where(Func<T, bool> expression)
        {
            return _db.Set<T>().Where(expression);
        }


        public virtual async Task<T> Delete(T value)
        {
            //value.Remove();
            //return await Update(value);
            return await Delete(value);
        }
    }
}
