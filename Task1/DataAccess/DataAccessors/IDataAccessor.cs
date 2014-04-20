namespace Persons.DataAccessors
{
    using System.Collections.Generic;

    using Persons.Domain;

    /// <summary>
    /// Интерфейс, предоставляющий методы для работы с хранилищем данных.
    /// </summary>
    public interface IDataAccessor<T> where T : EntityBase
    {
        /// <summary>
        /// Получить информацию обо всех объектах.
        /// </summary>
        /// <returns>Информация обо всех объектах.</returns>
        IList<T> GetAll();

        /// <summary>
        /// Получить информацию об объекте из хранилища данных.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        /// <returns>Найденная запись.</returns>
        T GetById(int id);

        /// <summary>
        /// Удалить информацию об объекте из хранилища данных.
        /// </summary>
        /// <param name="id">Уникальный идентификатор.</param>
        void DeleteById(int id);

        /// <summary>
        /// Добавить информацию об объекте в хранилище данных.
        /// </summary>
        /// <param name="entity">Добавляемый объект.</param>
        void Insert(T entity);

    }
}
