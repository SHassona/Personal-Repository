﻿using System;
using System.Net.Http;
using System.Web.Http;
using Moq;
using NUnit.Framework;
using WebApi2Book.Data;
using WebApi2Book.Web.Api.Controllers.V1;
using WebApi2Book.Web.Api.InquiryProcessing;
using WebApi2Book.Web.Api.MaintenanceProcessing;
using WebApi2Book.Web.Api.Models;

namespace WebApi2Book.Web.Api.Tests.Controllers.V1
{
    [TestFixture]
    public class TasksControllerTest
    {
        private TasksControllerDependencyBlockMock _mockBlock;
        private TasksController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockBlock = new TasksControllerDependencyBlockMock();
            _controller = new TasksController(_mockBlock.Object);
        }

        [Test]
        public void GetTasks_returns_correct_response()
        {
            var requestMessage = HttpRequestMessageFactory.CreateRequestMessage();
            var request = new PagedDataRequest(1, 25);
            var response = new PagedDataInquiryResponse<Task>();
            _mockBlock.PagedDataRequestFactoryMock
                .Setup(x => x.Create(requestMessage.RequestUri)).Returns(request);
            _mockBlock.AllTasksInquiryProcessorMock
                .Setup(x => x.GetTasks(request)).Returns(response);
            var actualResponse = _controller.GetTasks(requestMessage);
            Assert.AreSame(response, actualResponse);
        }
    }
}