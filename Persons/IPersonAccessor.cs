using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persons
{
    /// <summary>
    /// Интерфейс, предоставляющий методы для работы с хранилищем данных о людях.
    /// </summary>
    public interface IPersonAccessor
    {
        /// <summary>
        /// Получить весь список людей.
        /// </summary>
        /// <returns>Список людей.</returns>
        IList<Person> GetAll();

        /// <summary>
        /// Получить человека по фамилии.
        /// </summary>
        /// <param name="lastName">Фамилия.</param>
        /// <returns>Найденный человек.</returns>
        IList<Person> GetByLastName(string lastName);

        /// <summary>
        /// Удалить человека из списка по фамилии.
        /// </summary>
        /// <param name="lastName">Фамилия удаляемого человека.</param>
        void DeleteByLastName(string lastName);

        /// <summary>
        /// Добавить человека в список.
        /// </summary>
        /// <param name="person">Добавляемый человек.</param>
        void Insert(Person person);

    }
}
