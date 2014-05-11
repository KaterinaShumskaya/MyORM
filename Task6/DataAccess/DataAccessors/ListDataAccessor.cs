namespace Persons.DataAccessors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Persons.Domain;

    public class ListDataAccessor<T> : IDataAccessor<T> where T : EntityBase
    {
        private readonly IList<T> _data; 

        public ListDataAccessor(IList<T> data)
        {
            this._data = data;
        }

        public IList<T> GetAll()
        {
            return this._data;
        }

        public T GetById(int id)
        {
            return this._data.SingleOrDefault(x=>x.Id == id);
        }

        public void DeleteById(int id)
        {
            var entity = GetById(id);
            this._data.Remove(entity);    
        }

        public int Insert(T entity)
        {
            entity.Id = this._data.Any(x => true) ? this._data.Select(x => x.Id).Max() + 1 : 1;
            this._data.Add(entity);
            return entity.Id;
        }
    }
}
