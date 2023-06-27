using Employee_Managment.Controllers;
using Repository;
using Repository.Interface;
using Services.Implementation;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using Unity.AspNet.Mvc;

namespace Employee_Managment
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var Container = new UnityContainer();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Container.RegisterType<IEmployeeServices, EmployeeServices>();
            Container.RegisterType<IEmployeeRepository, EmployeeRepository>();
            Container.RegisterType<EmployeeController>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(Container));

        }
    }
}
