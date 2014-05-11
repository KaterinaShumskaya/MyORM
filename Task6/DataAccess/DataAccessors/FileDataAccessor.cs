namespace Persons.DataAccessors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    using System.Xml.Serialization;
    using System.Xml.XPath;

    using Persons.Domain;

    public class FileDataAccessor<T> : IDataAccessor<T> where T : EntityBase
    {
        private IList<T> _data;

        private readonly string _fileName;

        public FileDataAccessor(string fileName)
        {
            this._fileName = fileName;
        }

        public IList<T> GetAll()
        {
            return this.Deserialize();
        }

        public T GetById(int id)
        {
            return this.Deserialize().SingleOrDefault(x => x.Id == id);
        }

        public void DeleteById(int id)
        {
            var entity = GetById(id);
            this._data.Remove(entity);  
            this.Serialize();
        }

        public int Insert(T entity)
        {
            entity.Id = this._data.Any(x => true) ? this._data.Select(x => x.Id).Max() + 1 : 1;
            this._data.Add(entity);
            this.Serialize();
            return entity.Id;
        }

        private void Serialize()
        {
            using (var fileStream = new FileStream(this._fileName, FileMode.Create))
            {
                var xmlSerializer = new XmlSerializer(typeof(List<T>));
                xmlSerializer.Serialize(fileStream, this._data);
            }
        }

        private IList<T> Deserialize()
        {
            using (var fileStream = new FileStream(this._fileName, FileMode.OpenOrCreate))
            {
                var xmlSerializer = new XmlSerializer(typeof(List<T>));
                try
                {
                    this._data = (IList<T>)xmlSerializer.Deserialize(fileStream);
                }
                catch (Exception)
                {
                    _data = new List<T>();
                }
            return this._data;
            }
        }
    }
}
