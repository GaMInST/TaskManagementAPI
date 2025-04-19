using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskManagementAPI.Data; 
using TaskManagementAPI.Models; 

var builder = WebApplication.CreateBuilder(args);

// Add InMemory database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TaskManagementDB"));

// Add authentication
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = false,
            // Note: In real project, add IssuerSigningKey later
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ------ SEED DATA START ------
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    // Add Users
    if (!dbContext.Users.Any())
    {
        var user1 = new User { Id = 1, Name = "Admin User" };
        var user2 = new User { Id = 2, Name = "Normal User" };

        dbContext.Users.AddRange(user1, user2);

        // Add Tasks
        var task1 = new TaskItem { Id = 1, Title = "First Task", Description = "Description for Task 1", AssignedUserId = user1.Id };
        var task2 = new TaskItem { Id = 2, Title = "Second Task", Description = "Description for Task 2", AssignedUserId = user2.Id };

        dbContext.TaskItems.AddRange(task1, task2);

        // Add Comments
        var comment1 = new TaskComment { Id = 1, Content = "Please update the task.", TaskItemId = task1.Id, UserId = user2.Id };
        var comment2 = new TaskComment { Id = 2, Content = "Good job!", TaskItemId = task2.Id, UserId = user1.Id };

        dbContext.TaskComments.AddRange(comment1, comment2);

        dbContext.SaveChanges();
    }
}
// ------ SEED DATA END ------

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
