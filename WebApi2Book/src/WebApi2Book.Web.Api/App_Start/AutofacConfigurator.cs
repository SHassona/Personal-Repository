using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using AutoMapper;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using log4net.Config;
using NHibernate;
using NHibernate.Context;
using NHibernate.Util;
using Ninject;
using Ninject.Activation;
using Ninject.Web.Common;
using WebApi2Book.Common;
using WebApi2Book.Common.Logging;
using WebApi2Book.Common.Security;
using WebApi2Book.Data.QueryProcessors;
using WebApi2Book.Data.SqlServer.Mapping;
using WebApi2Book.Data.SqlServer.QueryProcessors;
using WebApi2Book.Web.Api.Controllers.V1;
using WebApi2Book.Web.Api.InquiryProcessing;
using WebApi2Book.Web.Api.LinkServices;
using WebApi2Book.Web.Api.MaintenanceProcessing;
using WebApi2Book.Web.Api.Security;
using WebApi2Book.Web.Common;
using WebApi2Book.Web.Common.Security;

namespace WebApi2Book.Web.Api
{
    internal class AutofacConfigurator
    {
        public AutofacConfigurator()
        {
        }

        public void Configure(ContainerBuilder containerBuilder)
        {
            AddBindings(containerBuilder);
        }

        private void AddBindings(ContainerBuilder containerBuilder)
        {
            ConfigureLog4Net(containerBuilder);
            ConfigureUserSession(containerBuilder);
            ConfigureNHibernate(containerBuilder);
            ConfigureAutoMapper(containerBuilder);

            //            container.Bind<IDateTime>().To<DateTimeAdapter>().InSingletonScope();
            containerBuilder.RegisterType<DateTimeAdapter>()
                .As<IDateTime>()
                .SingleInstance();
            //            container.Bind<IAddTaskQueryProcessor>().To<AddTaskQueryProcessor>().InRequestScope();
            containerBuilder.RegisterType<AddTaskQueryProcessor>()
                .As<IAddTaskQueryProcessor>()
                .InstancePerLifetimeScope();
            //            container.Bind<IAddTaskMaintenanceProcessor>().To<AddTaskMaintenanceProcessor>().InRequestScope();
            containerBuilder.RegisterType<AddTaskMaintenanceProcessor>()
                .As<IAddTaskMaintenanceProcessor>()
                .InstancePerLifetimeScope();
//            container.Bind<IBasicSecurityService>().To<BasicSecurityService>().InSingletonScope();
            containerBuilder.RegisterType<BasicSecurityService>()
                .As<IBasicSecurityService>()
                .SingleInstance();
//            container.Bind<ITaskByIdQueryProcessor>().To<TaskByIdQueryProcessor>().InRequestScope();
            containerBuilder.RegisterType<TaskByIdQueryProcessor>()
                .As<ITaskByIdQueryProcessor>()
                .InstancePerLifetimeScope();
//            container.Bind<IUpdateTaskStatusQueryProcessor>().To<UpdateTaskStatusQueryProcessor>().InRequestScope();
            containerBuilder.RegisterType<UpdateTaskStatusQueryProcessor>()
                .As<IUpdateTaskStatusQueryProcessor>()
                .InstancePerLifetimeScope();
//            container.Bind<IStartTaskWorkflowProcessor>().To<StartTaskWorkflowProcessor>().InRequestScope();
            containerBuilder.RegisterType<StartTaskWorkflowProcessor>()
                .As<IStartTaskWorkflowProcessor>()
                .InstancePerLifetimeScope();
//            container.Bind<ICompleteTaskWorkflowProcessor>().To<CompleteTaskWorkflowProcessor>().InRequestScope();
            containerBuilder.RegisterType<CompleteTaskWorkflowProcessor>()
               .As<ICompleteTaskWorkflowProcessor>()
               .InstancePerLifetimeScope();
//            container.Bind<IReactivateTaskWorkflowProcessor>().To<ReactivateTaskWorkflowProcessor>().InRequestScope();
            containerBuilder.RegisterType<ReactivateTaskWorkflowProcessor>()
               .As<IReactivateTaskWorkflowProcessor>()
               .InstancePerLifetimeScope();
//            container.Bind<ITaskByIdInquiryProcessor>().To<TaskByIdInquiryProcessor>().InRequestScope();
            containerBuilder.RegisterType<TaskByIdInquiryProcessor>()
               .As<ITaskByIdInquiryProcessor>()
               .InstancePerLifetimeScope();
//            container.Bind<IUpdateTaskQueryProcessor>().To<UpdateTaskQueryProcessor>().InRequestScope();
            containerBuilder.RegisterType<UpdateTaskQueryProcessor>()
               .As<IUpdateTaskQueryProcessor>()
               .InstancePerLifetimeScope();
//            container.Bind<ITaskUsersMaintenanceProcessor>().To<TaskUsersMaintenanceProcessor>().InRequestScope();
            containerBuilder.RegisterType<TaskUsersMaintenanceProcessor>()
               .As<ITaskUsersMaintenanceProcessor>()
               .InstancePerLifetimeScope();
//            container.Bind<IUpdateablePropertyDetector>().To<JObjectUpdateablePropertyDetector>().InSingletonScope();
            containerBuilder.RegisterType<JObjectUpdateablePropertyDetector>()
               .As<IUpdateablePropertyDetector>()
               .SingleInstance();
//            container.Bind<IUpdateTaskMaintenanceProcessor>().To<UpdateTaskMaintenanceProcessor>().InRequestScope();
            containerBuilder.RegisterType<UpdateTaskMaintenanceProcessor>()
               .As<IUpdateTaskMaintenanceProcessor>()
               .InstancePerLifetimeScope();
//            container.Bind<IPagedDataRequestFactory>().To<PagedDataRequestFactory>().InSingletonScope();
            containerBuilder.RegisterType<PagedDataRequestFactory>()
               .As<IPagedDataRequestFactory>()
               .SingleInstance();
//            container.Bind<IAllTasksQueryProcessor>().To<AllTasksQueryProcessor>().InRequestScope();
            containerBuilder.RegisterType<UpdateTaskMaintenanceProcessor>()
               .As<IUpdateTaskMaintenanceProcessor>()
               .InstancePerLifetimeScope();
//            container.Bind<IAllTasksInquiryProcessor>().To<AllTasksInquiryProcessor>().InRequestScope();
            containerBuilder.RegisterType<AllTasksInquiryProcessor>()
               .As<IAllTasksInquiryProcessor>()
               .InstancePerLifetimeScope();
//            container.Bind<ICommonLinkService>().To<CommonLinkService>().InRequestScope();
            containerBuilder.RegisterType<CommonLinkService>()
               .As<ICommonLinkService>()
               .InstancePerLifetimeScope();
//            container.Bind<IUserLinkService>().To<UserLinkService>().InRequestScope();
            containerBuilder.RegisterType<UserLinkService>()
               .As<IUserLinkService>()
               .InstancePerLifetimeScope();
//            container.Bind<ITaskLinkService>().To<TaskLinkService>().InRequestScope();
            containerBuilder.RegisterType<TaskLinkService>()
               .As<ITaskLinkService>()
               .InstancePerLifetimeScope();
//            container.Bind<ITasksControllerDependencyBlock>().To<TasksControllerDependencyBlock>().InRequestScope();
            containerBuilder.RegisterType<TasksControllerDependencyBlock>()
               .As<ITasksControllerDependencyBlock>()
               .InstancePerLifetimeScope();
        }

        private static void ConfigureLog4Net(ContainerBuilder containerBuilder)
        {
            XmlConfigurator.Configure();
            var logManager = new LogManagerAdapter();
            containerBuilder.RegisterInstance(logManager).As<ILogManager>();
//            container.Bind<ILogManager>().ToConstant(logManager);
        }

        private static void ConfigureNHibernate(ContainerBuilder containerBuilder)
        {
            var sessionFactory = Fluently.Configure()
                .Database(
                    MsSqlConfiguration.MsSql2012
                        .ConnectionString(c => c.FromConnectionStringWithKey("WebApi2BookDb")))
                .CurrentSessionContext("web")
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TaskMap>())
                .BuildSessionFactory();
            containerBuilder.RegisterInstance(sessionFactory).As<ISessionFactory>();
//            container.Bind<ISessionFactory>().ToConstant(sessionFactory);

            containerBuilder.Register(CreateSession).As<ISession>().InstancePerDependency();
            //            container.Bind<ISession>().ToMethod(CreateSession).InRequestScope();
            containerBuilder.RegisterType<ActionTransactionHelper>().As<IActionTransactionHelper>().InstancePerLifetimeScope();
//            container.Bind<IActionTransactionHelper>().To<ActionTransactionHelper>().InRequestScope();
        }

        private static ISession CreateSession(IComponentContext componentContext)
        {
            var sessionFactory = componentContext.Resolve<ISessionFactory>();
            if (!CurrentSessionContext.HasBind(sessionFactory))
            {
                var session = sessionFactory.OpenSession();
                CurrentSessionContext.Bind(session);
            }
            return sessionFactory.GetCurrentSession();
        }

        private void ConfigureUserSession(ContainerBuilder containerBuilder)
        {
            var userSession = new UserSession();
            containerBuilder.RegisterInstance(userSession).As<IUserSession>().SingleInstance();
//            container.Bind<IUserSession>().ToConstant(userSession).InSingletonScope();
            containerBuilder.RegisterInstance(userSession).As<IWebUserSession>().SingleInstance();
//            container.Bind<IWebUserSession>().ToConstant(userSession).InSingletonScope();
        }

        private void ConfigureAutoMapper(ContainerBuilder containerBuilder)
        {
            var profileType = typeof(Profile);
            // Get an instance of each Profile in the executing assembly.
            var profiles = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => profileType.IsAssignableFrom(t)
                            && t.GetConstructor(Type.EmptyTypes) != null)
                .Select(Activator.CreateInstance)
                .Cast<Profile>();

            // Initialize AutoMapper with each instance of the profiles found.
            var config = new MapperConfiguration(cfg => profiles.ForEach(cfg.AddProfile));
            containerBuilder.RegisterInstance(config).As<MapperConfiguration>().SingleInstance();
//            container.Bind<MapperConfiguration>().ToMethod(x => config).InSingletonScope();
            containerBuilder.RegisterInstance(config.CreateMapper()).As<IMapper>().SingleInstance();
//            container.Bind<IMapper>().ToConstant(config.CreateMapper()).InSingletonScope();
        }
    }
}