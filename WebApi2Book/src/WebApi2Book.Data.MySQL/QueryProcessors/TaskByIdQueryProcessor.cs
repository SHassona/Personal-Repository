using WebApi2Book.Data.Entities;
using WebApi2Book.Data.QueryProcessors;

namespace WebApi2Book.Data.MySQL.QueryProcessors
{
    public class TaskByIdQueryProcessor:ITaskByIdQueryProcessor
    {
        public Task GetTask(long taskId)
        {
            throw new System.NotImplementedException();
        }
    }
}