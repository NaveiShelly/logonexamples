using Microsoft.EntityFrameworkCore;
using TaskManagerApi.Data;
using TaskManagerApi.Services;
using TaskManagerApi.Models;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevServer",
        builder => builder
            .WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("TasksDb"));

builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Use CORS
app.UseCors("AllowAngularDevServer");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(new
        {
            message = "An unexpected error occurred. Please try again later."
        });
    });
});

app.MapGet("/tasks", async (ITaskService service) =>
{
    var tasks = await service.GetAllTasksAsync();
    return Results.Ok(tasks);
});

app.MapGet("/tasks/{id}", async (int id, ITaskService service) =>
{
    var task = await service.GetTaskByIdAsync(id);
    return task is not null
        ? Results.Ok(task)
        : Results.NotFound(new { message = $"Task with id {id} not found" });
});

app.MapPost("/tasks", async (TaskItem task, ITaskService service) =>
{
    var validationResults = new List<ValidationResult>();
    var context = new ValidationContext(task);
    if (!Validator.TryValidateObject(task, context, validationResults, true))
    {
        return Results.BadRequest(validationResults);
    }

    var created = await service.CreateTaskAsync(task);
    return Results.Created($"/tasks/{created.Id}", created);
});

app.MapPut("/tasks/{id}", async (int id, TaskItem task, ITaskService service) =>
{
    var validationResults = new List<ValidationResult>();
    var context = new ValidationContext(task);
    if (!Validator.TryValidateObject(task, context, validationResults, true))
    {
        return Results.BadRequest(validationResults);
    }

    var updated = await service.UpdateTaskAsync(id, task);
    return updated
        ? Results.Ok(new { message = "Task updated successfully" })
        : Results.NotFound(new { message = $"Task with id {id} not found" });
});

app.MapDelete("/tasks/{id}", async (int id, ITaskService service) =>
{
    var deleted = await service.DeleteTaskAsync(id);
    return deleted
        ? Results.Ok(new { message = "Task deleted successfully" })
        : Results.NotFound(new { message = $"Task with id {id} not found" });
});

app.Run();