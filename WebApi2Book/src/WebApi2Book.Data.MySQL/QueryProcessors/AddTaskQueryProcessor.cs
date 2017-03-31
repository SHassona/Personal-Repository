using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
//using NHibernate;
using WebApi2Book.Common;
using WebApi2Book.Common.Security;
using WebApi2Book.Data.Exceptions;
using WebApi2Book.Data.QueryProcessors;
using WebApi2Book.DatabaseFirst;
using Task = WebApi2Book.Data.Entities.Task;

namespace WebApi2Book.Data.MySQL.QueryProcessors
{
    public class AddTaskQueryProcessor : IAddTaskQueryProcessor
    {
        private readonly IDateTime _dateTime;
        private readonly DbContext _context;
        private readonly IUserSession _userSession;

        public AddTaskQueryProcessor(DbContext context, IUserSession userSession, IDateTime dateTime)
        {
            _context = context;
            _userSession = userSession;
            _dateTime = dateTime;
        }

        public void AddTask(Task task)
        {
            var taskDal = new DatabaseFirst.Task
            {
                TaskId = (int) task.TaskId,
                Subject = task.Subject,
                StartDate = task.StartDate,
                DueDate = task.DueDate,
                CompletedDate = task.CompletedDate,
                CreatedDate = _dateTime.UtcNow,
                Status = _context.Set<Status>().SingleOrDefault(x => x.Name == "Not Started"),
                User = _context.Set<User>().SingleOrDefault(x => x.Username == _userSession.Username)
            };
            taskDal = _context.Set<DatabaseFirst.Task>().Add(taskDal);
            _context.SaveChanges();
            task.TaskId = taskDal.TaskId;
            task.CreatedDate = taskDal.CreatedDate;
            task.Status = new Entities.Status
            {
                Name = taskDal.Status.Name,
                Ordinal = taskDal.Status.Ordinal,
                StatusId = taskDal.Status.StatusId
            };
            task.CreatedBy = new Entities.User
            {
                Firstname = taskDal.User.Firstname,
                Lastname = taskDal.User.Lastname,
                Username = taskDal.User.Username,
                UserId = taskDal.User.UserId
            };
            AddTaskUser(task, taskDal);
        }

        private void AddTaskUser(Task task, DatabaseFirst.Task taskDal)
        {
            if (task.Users != null && task.Users.Any())
            {
                foreach (var user in task.Users)
                {
                    var persistedUser = _context.Set<User>().SingleOrDefault(x => x.UserId == user.UserId);
                    if (persistedUser == null)
                    {
                        throw new ChildObjectNotFoundException("User not found");
                    }
                    _context.Set<TaskUser>().AddOrUpdate(new TaskUser
                    {
                        TaskId = taskDal.TaskId,
                        UserId = (int) user.UserId,
                        ts = DateTime.Now
                    });
                }
            }
        }
    }
}