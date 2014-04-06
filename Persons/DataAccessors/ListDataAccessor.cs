using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persons
{
    public class ListDataAccessor<T> : IDataAccessor<T> where T : EntityBase
    {
        private readonly IList<T> _data; 

        public ListDataAccessor(IList<T> data)
        {
            _data = data;
        }

        public IList<T> GetAll()
        {
            return _data;
        }

        public T GetById(int id)
        {
            return _data.SingleOrDefault(x=>x.Id == id);
        }

        public void DeleteById(int id)
        {
            _data.Remove(GetById(id));    
        }

        public void Insert(T entity)
        {
            _data.Add(entity);
        }
    }
}
