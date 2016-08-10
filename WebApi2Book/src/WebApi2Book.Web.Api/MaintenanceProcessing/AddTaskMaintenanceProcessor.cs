﻿using System.Net.Http;
using AutoMapper;
using WebApi2Book.Common;
using WebApi2Book.Common.TypeMapping;
using WebApi2Book.Data.QueryProcessors;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.MaintenanceProcessing
{
    public class AddTaskMaintenanceProcessor : IAddTaskMaintenanceProcessor
    {
        private readonly IMapper _autoMapper;
        private readonly IAddTaskQueryProcessor _queryProcessor;
        public AddTaskMaintenanceProcessor(IAddTaskQueryProcessor queryProcessor, IMapper autoMapper)
        {
            _queryProcessor = queryProcessor;
            _autoMapper = autoMapper;
        }
        public Task AddTask(NewTask newTask)
        {
            var taskEntity = _autoMapper.Map<Data.Entities.Task>(newTask);
            _queryProcessor.AddTask(taskEntity);
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