using WebApi2Book.Data.Entities;
using WebApi2Book.Data.QueryProcessors;

namespace WebApi2Book.Data.MySQL.QueryProcessors
{
    public class AllTasksQueryProcessor:IAllTasksQueryProcessor
    {
        public QueryResult<Task> GetTasks(PagedDataRequest requestInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}