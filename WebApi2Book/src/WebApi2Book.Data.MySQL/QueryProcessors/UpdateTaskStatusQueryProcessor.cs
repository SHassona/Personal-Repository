using WebApi2Book.Data.Entities;
using WebApi2Book.Data.QueryProcessors;

namespace WebApi2Book.Data.MySQL.QueryProcessors
{
    public class UpdateTaskStatusQueryProcessor:IUpdateTaskStatusQueryProcessor
    {
        public void UpdateTaskStatus(Task taskToUpdate, string statusName)
        {
            throw new System.NotImplementedException();
        }
    }
}