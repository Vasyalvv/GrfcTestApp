using GrfcTestApp.Data;
using GrfcTestApp.Data.Entities;
using GrfcTestApp.Data.Entities.Base;
using GrfcTestApp.Data.Entities.Interfaces;
using GrfcTestApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrfcTestApp.Services.Base
{
    public abstract class RepositoryInDb<T> : IRepository<T>, IDisposable
        where T : Entity
    {
        internal readonly AppDBContext _db;
        internal readonly DbSet<T> _dbSet;

        /// <summary>
        /// Данные из связанных таблиц загружены
        /// </summary>
        internal bool requiredDataLoaded=false;

        public RepositoryInDb(AppDBContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }


        public int Add(T item)
        {
            T result = _dbSet.Add(item);

            _db.SaveChanges();
            return result.Id;
        }

        public T Get(int id) => GetAll().FirstOrDefault(i => i.Id == id);

        //Нужно переопределить в реализации, если в запросе учавствуют связанные таблицы
        public virtual IEnumerable<T> GetAll() => _dbSet.ToList();

        /// <summary>
        /// Удаление данных, в перегруженных методах нужно подгрузить связанные таблицы, 
        /// иначе удаление может пройти не каскадно, в зависимости от загруженных данных в контексе
        /// на момент удаления
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual bool Remove(T item)
        {
            try
            {
                _dbSet.Remove(item);
                _db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public T Update(int id, T item)
        {
            if (item is null)
                return null;
            if (_dbSet.Contains(item))
                return item;
            T destinationItem = _dbSet.FirstOrDefault(i => i.Id == id);
            if (destinationItem is null)
                return null;

            return Update(item, destinationItem);
        }

        //Нужно обязательно переопределять в конкретных реализациях
        protected abstract T Update(T source, T destination);
        
        //Нужно обязательно переопределять в конкретных реализациях
        public abstract T FirstOrCreate(T item);

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
        }

        private bool _disposed;
        protected virtual void Dispose(bool Disposing)
        {
            if (!Disposing || _disposed) return;
            _disposed = true;

            _db.Dispose();
        }

        #endregion
    }
}
