using AutoMapper;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.Entities;

namespace WebApi2Book.Web.Api.AutoMappingConfiguration
{
    public class StatusEntityToStatusAutoMapperTypeConfigurator :Profile// IAutoMapperTypeConfigurator
    {
        public StatusEntityToStatusAutoMapperTypeConfigurator()
        {
            CreateMap<Status, Models.Status>();
        }
//        public void Configure()
//        {
////            Mapper.CreateMap<Status, Models.Status>();
//            Mapper.Initialize(cfg =>
//            {
//                cfg.CreateMap<Status, Models.Status>();
//            });
//        }
    }
}