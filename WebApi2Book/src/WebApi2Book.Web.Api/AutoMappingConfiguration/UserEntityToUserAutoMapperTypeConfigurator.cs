using AutoMapper;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.Entities;

namespace WebApi2Book.Web.Api.AutoMappingConfiguration
{
    public class UserEntityToUserAutoMapperTypeConfigurator : Profile //IAutoMapperTypeConfigurator
    {
        public UserEntityToUserAutoMapperTypeConfigurator()
        {
            CreateMap<User, Models.User>().ForMember(opt => opt.Links, x => x.Ignore());
        }
//        public void Configure()
//        {
////            Mapper.CreateMap<User, Models.User>().ForMember(opt => opt.Links, x => x.Ignore());
//            Mapper.Initialize(cfg =>
//            {
//                cfg.CreateMap<User, Models.User>().ForMember(opt => opt.Links, x => x.Ignore());
//            });
//        }
    }
}