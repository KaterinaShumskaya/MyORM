using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persons
{
    using System.IO;
    using System.Xml.Serialization;

    public class FileDataAccessor<T> : IDataAccessor<T> where T : EntityBase
    {
        private IList<T> _data;

        private readonly string _fileName;

        public FileDataAccessor(string fileName, IList<T> data)
        {
            _data = data;
            _fileName = fileName;
            Serialize();
        }

        public IList<T> GetAll()
        {
            return Deserialize();
        }

        public T GetById(int id)
        {
            return Deserialize().SingleOrDefault(x => x.Id == id);
        }

        public void DeleteById(int id)
        {
            _data.Remove(GetById(id));  
            Serialize();
        }

        public void Insert(T entity)
        {
            _data.Add(entity);
            Serialize();
        }

        private void Serialize()
        {
            using (var fileStream = new FileStream(_fileName, FileMode.Create))
            {
                var xmlSerializer = new XmlSerializer(typeof(List<T>));
                xmlSerializer.Serialize(fileStream, _data);
            }
        }

        private IList<T> Deserialize()
        {
            using (var fileStream = new FileStream(_fileName, FileMode.Open))
            {
                var xmlSerializer = new XmlSerializer(typeof(List<T>));
                _data = (IList<T>)xmlSerializer.Deserialize(fileStream);
                return _data;
            }
        }
    }
}
