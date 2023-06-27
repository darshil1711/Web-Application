using Repository;
using Repository.Interface;
using Services.Implementation;
using Services.Interface;
using System;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Employee_Managment
{
    public static class UnityConfig
    {
        public static IUnityContainer Container { get; set; }
        public static void RegisterTypes(IUnityContainer container)
        {
            Container = new UnityContainer();
            Container.AddExtension(new Diagnostic());
            Container.RegisterType<IEmployeeServices, EmployeeServices>();
            Container.RegisterType<IEmployeeRepository, EmployeeRepository>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(Container));
        }

       
    }
}