using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebFormsClient
{
    using Castle.Windsor;
    using Castle.Windsor.Installer;

    public class Global : HttpApplication
    {
        private static IWindsorContainer _container;

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            _container = new WindsorContainer();
            _container.Install(FromAssembly.This());
        }

        void Application_End(object sender, EventArgs e)
        {
            Container.GetContainer().Dispose();
        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        internal static object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        internal static void Release(object component)
        {
            _container.Release(component);
        }

        public void Load()
        {
            
        }
    }
}
