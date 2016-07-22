using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Ninject;

namespace WebApi2Book.Web.Common
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        public NinjectDependencyResolver(IKernel container)
        {
            Container = container;
        }
        public IKernel Container { get; }

        public object GetService(Type serviceType)
        {
            return Container.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Container.GetAll(serviceType);
        }
        public IDependencyScope BeginScope()
        {
            return this;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}