using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebFormsClient
{
    using System.Reflection;
    using System.Web.UI;

    public class WindsorHttpModule : IHttpModule
    {
        private HttpApplication _context;

        public void Init(HttpApplication context)
        {
            _context = context;
            _context.PreRequestHandlerExecute += InjectDependencies;
        }

        private static object GetInstance(Type type)
        {
            return Container.GetContainer().Resolve(type);
        }

        private  void InjectDependencies(object sender, EventArgs e)
        {
            var page = _context.Context.CurrentHandler as Page;
                if (page != null)
                {
                    Type pageType = page.GetType().BaseType;

                    var ctor = GetInjectableCtor(pageType);

                    if (ctor != null)
                    {
                        object[] arguments =
                            (from parameter in ctor.GetParameters() select GetInstance(parameter.ParameterType)).ToArray();

                        ctor.Invoke(page, arguments);
                    }
                }
        }

        private static ConstructorInfo GetInjectableCtor(Type type)
        {
            var overloadedPublicConstructors = (
                from constructor in type.GetConstructors()
                where constructor.GetParameters().Length > 0
                select constructor).ToArray();

            if (overloadedPublicConstructors.Length == 0)
            {
                return null;
            }

            if (overloadedPublicConstructors.Length == 1)
            {
                return overloadedPublicConstructors[0];
            }

            throw new Exception(string.Format(
                "The type {0} has multiple public " +
                "ctors and can't be initialized.", type));
        }
    

        public void Dispose()
        {
            
        }
    }
}