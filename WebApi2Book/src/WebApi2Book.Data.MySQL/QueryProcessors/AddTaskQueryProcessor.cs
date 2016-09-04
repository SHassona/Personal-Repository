using WebApi2Book.Common;
using WebApi2Book.Common.Security;
using WebApi2Book.Data.Entities;
using WebApi2Book.Data.QueryProcessors;

namespace WebApi2Book.Data.MySQL.QueryProcessors
{
    public class AddTaskQueryProcessor:IAddTaskQueryProcessor
    {
        private readonly IDateTime _dateTime;
        private readonly ISession _session;
        private readonly IUserSession _userSession;

        public AddTaskQueryProcessor(ISession session, IUserSession userSession, IDateTime dateTime)
        {
            _session = session;
            _userSession = userSession;
            _dateTime = dateTime;
        }

        public void AddTask(Task task)
        {
            throw new System.NotImplementedException();
        }
    }
}