using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCAbstraction 
{
    /// <summary>
    /// Интерфейс для IoC контейнера.
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Метод регистрации типа в контейнере.
        /// </summary>
        /// <typeparam name="T">Базовый тип.</typeparam>
        /// <typeparam name="T1">Конкретный тип.</typeparam>
        void Register<T, T1>()
            where T : class
            where T1 : T;

        /// <summary>
        /// Метод разрешения зависимостей.
        /// </summary>
        /// <typeparam name="T">Тип, для котрого необходимо разрешить зависимость.</typeparam>
        T Resolve<T>();

        /// <summary>
        /// Метод регистрации типа в контейнере.
        /// </summary>
        /// <typeparam name="T">Базовый тип.</typeparam>
        /// <param name="obj">Конкретный объект.</param>
        void Register<T>(T obj) where T : class;
    }
}
