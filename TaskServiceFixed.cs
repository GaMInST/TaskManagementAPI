using Microsoft.EntityFrameworkCore; /
using System.Collections.Generic;    
using System.Threading.Tasks;         
using Task = TaskManagementAPI.Models.TaskItem; 
public class TaskService
{
    private readonly AppDbContext _dbContext; // Injected database context to interact with the database

    // Constructor to initialize the service with the application's DbContext
    public TaskService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Asynchronously gets a single task by its ID
    public async Task<Task> GetTaskAsync(int id)
    {
        // FirstOrDefaultAsync will return the task if found, or null if not
        return await _dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id);
    }

    // Asynchronously gets all tasks from the database
    public async Task<List<Task>> GetAllTasksAsync()
    {
        // ToListAsync fetches all the tasks and returns them as a list
        return await _dbContext.Tasks.ToListAsync();
    }
}
