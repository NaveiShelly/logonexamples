using Microsoft.EntityFrameworkCore;
using UserManagerApi.Data;
using UserManagerApi.Services;
using UserManagerApi.Models;
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
    options.UseInMemoryDatabase("UsersDb"));

builder.Services.AddScoped<IUserService, UserService>();
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

app.MapGet("/users", async (IUserService service) =>
{
    var users = await service.GetAllUsersAsync();
    return Results.Ok(users);
});

app.MapGet("/users/{id}", async (int id, IUserService service) =>
{
    var user = await service.GetUserByIdAsync(id);
    return user is not null
        ? Results.Ok(user)
        : Results.NotFound(new { message = $"User with id {id} not found" });
});

app.MapPost("/users", async (UserItem user, IUserService service) =>
{
    var validationResults = new List<ValidationResult>();
    var context = new ValidationContext(user);
    if (!Validator.TryValidateObject(user, context, validationResults, true))
    {
        return Results.BadRequest(validationResults);
    }

    var created = await service.CreateUserAsync(user);
    return Results.Created($"/users/{created.Id}", created);
});

app.MapPut("/users/{id}", async (int id, UserItem user, IUserService service) =>
{
    var validationResults = new List<ValidationResult>();
    var context = new ValidationContext(user);
    if (!Validator.TryValidateObject(user, context, validationResults, true))
    {
        return Results.BadRequest(validationResults);
    }

    var updated = await service.UpdateUserAsync(id, user);
    return updated
        ? Results.Ok(new { message = "User updated successfully" })
        : Results.NotFound(new { message = $"User with id {id} not found" });
});

app.MapDelete("/users/{id}", async (int id, IUserService service) =>
{
    var deleted = await service.DeleteUserAsync(id);
    return deleted
        ? Results.Ok(new { message = "User deleted successfully" })
        : Results.NotFound(new { message = $"User with id {id} not found" });
});


app.MapGet("/users/filters", async (
    string? name,
    string? role,
    bool? isActive,
    IUserService service) =>
{
    var users = await service.FilterUsersAsync(name, role, isActive);
    return Results.Ok(users);
});


app.Run();