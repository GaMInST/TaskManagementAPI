using NUnit.Framework;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using TaskManagementAPI.Data;
using TaskManagementAPI.Models;


namespace TaskManagementAPI.Tests.Controllers
{
    public class TasksControllerTests
    {
        private AppDbContext _dbContext;
        private TasksController _controller;

        [SetUp]
        public void Setup()
        {
            
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new AppDbContext(options);
            _controller = new TasksController(_dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            _dbContext.Dispose();
        }

        [Test]
        public async Task CreateTask_ShouldReturnOkAndPersist_WhenValid()
        {
            // Arrange
            var dto = new CreateTaskDto
            {
                Title = "Test Task",
                Description = "Test Description",
                AssignedUserId = 1
            };

            // Act
            var result = await _controller.CreateTask(dto);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var ok = (OkObjectResult)result;
            var created = (TaskItem)ok.Value!;
            Assert.AreEqual(dto.Title, created.Title);
            Assert.AreEqual(dto.AssignedUserId, created.AssignedUserId);

            // Verify it’s in the DB
            var inDb = _dbContext.TaskItems.Find(created.Id);
            Assert.NotNull(inDb);
        }

        [Test]
        public async Task GetTask_ShouldReturnOk_WhenExists()
        {
            // Arrange
            var entity = new TaskItem
            {
                Title = "Exists",
                Description = "Desc",
                AssignedUserId = 1
            };
            _dbContext.TaskItems.Add(entity);
            _dbContext.SaveChanges();

            // Act
            var result = await _controller.GetTask(entity.Id);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var ok = (OkObjectResult)result;
            var fetched = (TaskItem)ok.Value!;
            Assert.AreEqual(entity.Id, fetched.Id);
        }

        [Test]
        public async Task GetTask_ShouldReturnNotFound_WhenMissing()
        {
            // Act
            var result = await _controller.GetTask(999);

            // Assert
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task GetTasksByUser_ShouldReturnOnlyThatUserTasks()
        {
            // Arrange
            _dbContext.TaskItems.AddRange(new[]
            {
                    new TaskItem { Title = "T1", Description = "D1", AssignedUserId = 1 },
                    new TaskItem { Title = "T2", Description = "D2", AssignedUserId = 2 }
                });
            _dbContext.SaveChanges();

            // Act
            var result = await _controller.GetTasksByUser(1);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var ok = (OkObjectResult)result;
            var list = (List<TaskItem>)ok.Value!;
            Assert.AreEqual(1, list.Count);
            Assert.IsTrue(list.All(t => t.AssignedUserId == 1));
        }
    }
}
