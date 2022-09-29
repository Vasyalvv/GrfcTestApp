using GrfcTestApp.Data.Entities.Base;
using GrfcTestApp.Data.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrfcTestApp.Services.Interfaces
{
    /// <summary>
    /// Интерфейс хранилища
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T:Entity
    {
        int Add(T item);

        IEnumerable<T> GetAll();

        T Get(int id);

        bool Remove(T item);

        T Update(int id, T item);
    }
}
