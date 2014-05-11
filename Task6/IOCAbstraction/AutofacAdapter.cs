using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOCAbstraction
{
    using Autofac;

    public class AutofacAdapter : IContainer
    {
        private readonly ContainerBuilder _builder;

        private Autofac.IContainer _container;

        public AutofacAdapter()
        {
            _builder = new ContainerBuilder();
        }

        public void Register<T, T1>() where T : class where T1 : T
        {
            _builder.RegisterType<T1>().As<T>();
        }
        
        public T Resolve<T>()
        {
            _container = _builder.Build();
            return (T)_container.Resolve(typeof(T));
        }

        public void Register<T>(T obj) where T : class
        {
            _builder.RegisterInstance(obj);
        }
    }
}
