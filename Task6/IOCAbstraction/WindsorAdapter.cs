using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCAbstraction
{
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;

    public class WindsorAdapter : IContainer
    {
        public WindsorContainer _container;
 
        public WindsorAdapter()
        {
            _container = new WindsorContainer();
        }

        public void Register<T, T1>() where T : class where T1 : T
        {
            _container.Register(Component.For<T>().ImplementedBy<T1>());
        }

        public T Resolve<T>() 
        {
            return (T)_container.Resolve(typeof(T));
        }

        public void Register<T>(T obj) where T : class
        {
            _container.Register(Component.For<T>().Instance(obj));
        }
    }
}
