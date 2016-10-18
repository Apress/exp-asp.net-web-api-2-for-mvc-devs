using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using ExampleApp.Models;
using Ninject;
using Ninject.Extensions.ChildKernel;
using Ninject.Web.Common;

namespace ExampleApp.Infrastructure {

    public class NinjectResolver : System.Web.Http.Dependencies.IDependencyResolver,
            System.Web.Mvc.IDependencyResolver {
        private IKernel kernel;

        public NinjectResolver() : this(new StandardKernel()) { }

        public NinjectResolver(IKernel ninjectKernel) {
            kernel = ninjectKernel;
            AddBindings(kernel);
        }

        public IDependencyScope BeginScope() {
            return this;
        }

        public object GetService(Type serviceType) {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return kernel.GetAll(serviceType);
        }

        public void Dispose() {
            // do nothing
        }

        private void AddBindings(IKernel kernel) {
            kernel.Bind<IRepository>().To<Repository>().InRequestScope();
        }
    }
}
