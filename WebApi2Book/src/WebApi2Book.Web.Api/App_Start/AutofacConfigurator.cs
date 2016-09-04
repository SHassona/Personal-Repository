using System;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Autofac.Integration.WebApi;
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

            containerBuilder.RegisterType<DateTimeAdapter>().As<IDateTime>().SingleInstance();
            containerBuilder.RegisterType<AddTaskQueryProcessor>().As<IAddTaskQueryProcessor>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<AddTaskMaintenanceProcessor>().As<IAddTaskMaintenanceProcessor>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<BasicSecurityService>().As<IBasicSecurityService>().SingleInstance();
            containerBuilder.RegisterType<TaskByIdQueryProcessor>().As<ITaskByIdQueryProcessor>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<UpdateTaskStatusQueryProcessor>().As<IUpdateTaskStatusQueryProcessor>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<StartTaskWorkflowProcessor>().As<IStartTaskWorkflowProcessor>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<CompleteTaskWorkflowProcessor>().As<ICompleteTaskWorkflowProcessor>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<ReactivateTaskWorkflowProcessor>().As<IReactivateTaskWorkflowProcessor>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<TaskByIdInquiryProcessor>().As<ITaskByIdInquiryProcessor>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<UpdateTaskQueryProcessor>().As<IUpdateTaskQueryProcessor>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<TaskUsersMaintenanceProcessor>().As<ITaskUsersMaintenanceProcessor>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<JObjectUpdateablePropertyDetector>().As<IUpdateablePropertyDetector>().SingleInstance();
            containerBuilder.RegisterType<UpdateTaskMaintenanceProcessor>().As<IUpdateTaskMaintenanceProcessor>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<PagedDataRequestFactory>().As<IPagedDataRequestFactory>().SingleInstance();
            containerBuilder.RegisterType<AllTasksQueryProcessor>().As<IAllTasksQueryProcessor>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<AllTasksInquiryProcessor>().As<IAllTasksInquiryProcessor>().InstancePerRequest();//.InstancePerLifetimeScope();
            containerBuilder.RegisterType<CommonLinkService>().As<ICommonLinkService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<UserLinkService>().As<IUserLinkService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<TaskLinkService>().As<ITaskLinkService>().InstancePerLifetimeScope();
            containerBuilder.RegisterType<TasksControllerDependencyBlock>().As<ITasksControllerDependencyBlock>().InstancePerDependency();
        }

        private static void ConfigureLog4Net(ContainerBuilder containerBuilder)
        {
            XmlConfigurator.Configure();
            var logManager = new LogManagerAdapter();
            containerBuilder.RegisterInstance(logManager).As<ILogManager>();
        }

        private static void ConfigureNHibernate(ContainerBuilder containerBuilder)
        {
            var sessionFactory = Fluently.Configure()
                .Database(
                    MySQLConfiguration.Standard
                        .ConnectionString(c => c.FromConnectionStringWithKey("WebApi2BookDb")))
                .CurrentSessionContext("web")
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TaskMap>())
                .BuildSessionFactory();
            containerBuilder.RegisterInstance(sessionFactory).As<ISessionFactory>();
            containerBuilder.Register(CreateSession).As<ISession>().InstancePerDependency();
            containerBuilder.RegisterType<ActionTransactionHelper>().As<IActionTransactionHelper>().InstancePerLifetimeScope();
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
            containerBuilder.RegisterInstance(userSession).As<IWebUserSession>().SingleInstance();
        }

        private void ConfigureAutoMapper(ContainerBuilder containerBuilder)
        {
            var profileType = typeof(Profile);
            var profiles = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => profileType.IsAssignableFrom(t)
                            && t.GetConstructor(Type.EmptyTypes) != null)
                .Select(Activator.CreateInstance)
                .Cast<Profile>();
            var config = new MapperConfiguration(cfg => profiles.ForEach(cfg.AddProfile));
            containerBuilder.RegisterInstance(config).As<MapperConfiguration>().SingleInstance();
            containerBuilder.RegisterInstance(config.CreateMapper()).As<IMapper>().SingleInstance();
        }
    }
}