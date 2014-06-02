using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCAbstraction
{
    using Microsoft.Practices.Unity;

    public class UnityAdapter : IContainer
    {
        private readonly UnityContainer _container;

        public UnityAdapter()
        {
            _container = new UnityContainer();
        }

        public void Register<T, T1>() where T : class where T1 : T
        {
            _container.RegisterType<T, T1>();
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public void Register<T>(T obj) where T : class
        {
            _container.RegisterInstance(typeof(T), obj);
        }
    }
}
