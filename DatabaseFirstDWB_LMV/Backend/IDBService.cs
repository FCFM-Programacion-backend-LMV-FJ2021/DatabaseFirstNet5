using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseFirstDWB_LMV.Backend
{
    public interface IDBService<T> where T: class
    {
        public List<T> Get();
        public T Get(object id);
        public void Add(T newElement);
        public void Update(object id, T elementForUpdate);
        public void Delete(object id);
       

    }
}
