using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.Entities;
using WebApi2Book.Web.Common;
using User = WebApi2Book.Web.Api.Models.User;

namespace WebApi2Book.Web.Api.AutoMappingConfiguration
{
    public class TaskAssigneesResolver : IValueResolver<Task, object, List<User>>
    {
        public IAutoMapper AutoMapper => WebContainerManager.Get<IAutoMapper>();

        protected  List<User> ResolveCore(Task source)
        {
            return source.Users.Select(x => AutoMapper.Map<User>(x)).ToList();
        }
//
//        public List<User> Resolve(Task source, object destination, List<User> destMember, ResolutionContext context)
//        {
//            return source.Users.Select(x => AutoMapper.Map<User>(x)).ToList();
//        }
        public List<User> Resolve(Task source, object destination, List<User> destMember, ResolutionContext context)
        {
            return source.Users.Select(x => AutoMapper.Map<User>(x)).ToList();
        }
    }
}