using System.Net.Http;
using AutoMapper;
using WebApi2Book.Common;
using WebApi2Book.Data;
using WebApi2Book.Data.QueryProcessors;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public class AddTaskMaintenanceProcessor : IAddTaskMaintenanceProcessor
    {
//        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IAddTaskQueryProcessor _queryProcessor;
        private readonly IMapper _autoMapper;

        public AddTaskMaintenanceProcessor(IAddTaskQueryProcessor queryProcessor, IMapper autoMapper)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
//            _unitOfWorkFactory = unitOfWorkFactory;
        }
        public Task AddTask(NewTask newTask)
        {
            var taskEntity = _autoMapper.Map<Data.Entities.Task>(newTask);
            _queryProcessor.AddTask(taskEntity);
//            _unitOfWorkFactory.SaveChanges();
            var task = _autoMapper.Map<Task>(taskEntity);

            // TODO: Implement link service
            task.AddLink(new Link
            {
                Method = HttpMethod.Get.Method,
                Href = "http://localhost:35332/api/v1/tasks/" + task.TaskId,
                Rel = Constants.CommonLinkRelValues.Self
            });

            return task;
        }
    }
}