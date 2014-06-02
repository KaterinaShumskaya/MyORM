using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFormsClient
{
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using Castle.Windsor.Installer;

    using Persons.DataAccessors;

    public class Container
    {
        private static IWindsorContainer container;

        public static IWindsorContainer GetContainer()
        {
            if (container == null)
            {
                container = new WindsorContainer();
                container.Install(FromAssembly.Containing(typeof(IDataAccessor<>)));
                container.Register(Component.For(typeof(IDataAccessor<>)).ImplementedBy(typeof(MyORM<>)));
            }

            return container;
        }
    }
}