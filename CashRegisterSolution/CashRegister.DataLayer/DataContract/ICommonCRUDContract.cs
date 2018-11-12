using System.Collections.Generic;

namespace CashRegister.DataLayer.DataContract
{
    public interface ICommonCRUDContract<T>
    {
        T Get (string uniqueCode);

        List<T> GetAll ();

        void Add (T entity);

        void Update (T entity);

        void Delete (string uniqueCode);
    }
}
