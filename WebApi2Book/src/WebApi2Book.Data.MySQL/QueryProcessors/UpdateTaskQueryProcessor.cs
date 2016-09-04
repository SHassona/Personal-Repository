using System.Collections.Generic;
using WebApi2Book.Data.Entities;
using WebApi2Book.Data.QueryProcessors;

namespace WebApi2Book.Data.MySQL.QueryProcessors
{
    public class UpdateTaskQueryProcessor:IUpdateTaskQueryProcessor
    {
        public Task GetUpdatedTask(long taskId, Dictionary<string, object> updatedPropertyValueMap)
        {
            throw new System.NotImplementedException();
        }

        public Task ReplaceTaskUsers(long taskId, IEnumerable<long> userIds)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteTaskUsers(long taskId)
        {
            throw new System.NotImplementedException();
        }

        public Task AddTaskUser(long taskId, long userId)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteTaskUser(long taskId, long userId)
        {
            throw new System.NotImplementedException();
        }
    }
}