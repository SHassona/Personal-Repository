using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi2Book.Data.Entities;
using WebApi2Book.Web.Common;
using User = WebApi2Book.Web.Api.Models.User;

namespace WebApi2Book.Web.Api.AutoMappingConfiguration
{
    public class TaskAssigneesResolver : IValueResolver<Task, Models.Task, List<User>>
    {
        public IMapper AutoMapper => WebContainerManager.Get<IMapper>();
//        private readonly IMapper _autoMapper;

//        public TaskAssigneesResolver(IMapper autoMapper)
//        {
//            _autoMapper = autoMapper;
//        }
        protected  List<User> ResolveCore(Task source)
        {
            return source.Users.Select(x => AutoMapper.Map<User>(x)).ToList();
        }
//
//        public List<User> Resolve(Task source, object destination, List<User> destMember, ResolutionContext context)
//        {
//            return source.Users.Select(x => AutoMapper.Map<User>(x)).ToList();
//        }
        public List<User> Resolve(Task source, Models.Task destination, List<User> destMember, ResolutionContext context)
        {
            return source.Users.Select(x => AutoMapper.Map<User>(x)).ToList();
        }
    }
}