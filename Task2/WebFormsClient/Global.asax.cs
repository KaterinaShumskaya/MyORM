using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using WebFormsClient;

namespace WebFormsClient
{
    using Persons.DataAccessors;

    using WebFormsClient.Model;

    public class Global : HttpApplication
    {
        public IDataAccessor<StudentGroup> groupAccessor;

        private IList<StudentGroup> _studentGroups;

        public IDataAccessor<Student> studentAccessor;

        private IList<Student> _students;

        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        public void Load()
        {
            _groupAccessor = new MyORM<StudentGroup>();
            _studentAccessor = new MyORM<Student>();
        }
    }
}
