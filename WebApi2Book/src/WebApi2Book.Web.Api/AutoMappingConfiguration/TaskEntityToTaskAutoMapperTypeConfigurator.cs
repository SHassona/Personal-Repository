using AutoMapper;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.Entities;

namespace WebApi2Book.Web.Api.AutoMappingConfiguration
{
    public class TaskEntityToTaskAutoMapperTypeConfigurator : Profile // IAutoMapperTypeConfigurator
    {
        public TaskEntityToTaskAutoMapperTypeConfigurator()
        {
            CreateMap<Task, Models.Task>()
                .ForMember(opt => opt.Links, x => x.Ignore())
                .ForMember(opt => opt.Assignees, x => x.ResolveUsing<TaskAssigneesResolver>());
        }

        //        public void Configure()
        //        {
        ////            Mapper.CreateMap<Task, Models.Task>()
        ////            .ForMember(opt => opt.Links, x => x.Ignore())
        ////            .ForMember(opt => opt.Assignees, x => x.ResolveUsing<TaskAssigneesResolver>());
        //            Mapper.Initialize(
        //                cfg =>
        //                {
        //                    cfg.CreateMap<Task, Models.Task>()
        //                        .ForMember(opt => opt.Links, x => x.Ignore())
        //                        .ForMember(opt => opt.Assignees, x => x.ResolveUsing<TaskAssigneesResolver>());
        //                });
        //        }
    }
}