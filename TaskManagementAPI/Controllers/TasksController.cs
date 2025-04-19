using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Data;
using TaskManagementAPI.Models;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    public TasksController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // POST /api/tasks
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto createTaskDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var taskItem = new TaskItem
        {
            Title = createTaskDto.Title,
            Description = createTaskDto.Description,
            AssignedUserId = createTaskDto.AssignedUserId
        };

        _dbContext.TaskItems.Add(taskItem);
        await _dbContext.SaveChangesAsync();

        return Ok(taskItem);
    }


    // GET /api/tasks/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(int id)
    {
        var taskItem = await _dbContext.TaskItems.FindAsync(id);
        if (taskItem == null)
            return NotFound();

        return Ok(taskItem);
    }

    // GET /api/tasks/user/{userId}
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetTasksByUser(int userId)
    {
        var tasks = await _dbContext.TaskItems
            .Where(t => t.AssignedUserId == userId)
            .ToListAsync();

        return Ok(tasks);
    }
}
