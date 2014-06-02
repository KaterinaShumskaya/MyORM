using System;
using System.Collections.Generic;
using System.Linq;

namespace IOCAbstraction
{
    using System.Reflection;

    /// <summary>
    /// Кастомный IoC.
    /// </summary>
    public class MyIOCContainer : IContainer
    {
        /// <summary>
        /// IoC Container.
        /// </summary>
        private readonly IDictionary<Type, RegisteredObject> _container;
 
        /// <summary>
        /// Инстанцирует экземпляр класса <see cref="MyIOCContainer"/>.
        /// </summary>
        public MyIOCContainer()
        {
            _container = new Dictionary<Type, RegisteredObject>();
        }

        /// <inheritdoc/>
        public void Register<T, T1>() where T : class where T1 : T
        {
            Register(typeof(T), typeof(T1));
        }

        public void Register<T>(T obj) where T : class
        {
            _container.Add(typeof(T), new RegisteredObject(obj.GetType(), obj));
        }

        private void Register(Type type, Type ctype)
        {
            _container.Add(type, new RegisteredObject(ctype)); 
        }

        /// <inheritdoc/>
        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));          
        }

        public object Resolve(Type type)
        {
            if (!_container.ContainsKey(type))
            {
                if (!type.IsInterface && !type.IsAbstract)
                {
                    Register(type, type);
                }
                else
                {
                    throw new ArgumentOutOfRangeException(
                        string.Format("Тип {0} не зарегистрирован в IoC контейнере", type.Name));
                }
            }

            return this.GetInstance(type);
        }

        /// <summary>
        /// Получить экземпляр зарегистрированного объекта.
        /// </summary>
        /// <param name="type">Базовый тип объекта.</param>
        /// <returns>Зарегистрированный в IoC контейнере объект.</returns>
        private object GetInstance(Type type)
        {
            var registeredObject = _container[type];
            if (registeredObject.Instance == null)
            {
                var constructorInfo = registeredObject.ConcreteType.GetConstructors(BindingFlags.Instance|BindingFlags.Public|BindingFlags.NonPublic).First();
                var parameters = constructorInfo.GetParameters().Select(parameter => Resolve(parameter.ParameterType));
                registeredObject.CreateInstance(parameters.ToArray());
            }

            return registeredObject.Instance;
        }

        /// <summary>
        /// Регистрируемый в IoC контейнере объект.
        /// </summary>
        private class RegisteredObject
        {
            public RegisteredObject(Type concreteType, object instance = null)
            {
                ConcreteType = concreteType;
                Instance = instance;
            }

            public Type ConcreteType { get; private set; }

            public object Instance { get; private set; }

            public void CreateInstance(params object[] args)
            {
                if(Instance == null)
                {
                    Instance = Activator.CreateInstance(ConcreteType, args);
                }              
            }
        }
    }
}
