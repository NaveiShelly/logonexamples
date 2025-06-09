using Microsoft.EntityFrameworkCore;
using ProductManagerApi.Data;
using ProductManagerApi.Services;
using ProductManagerApi.Models;
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
    options.UseInMemoryDatabase("ProductsDb"));

builder.Services.AddScoped<IProductService, ProductService>();
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

app.MapGet("/Products", async (IProductService service) =>
{
    var Products = await service.GetAllProductsAsync();
    return Results.Ok(Products);
});

app.MapGet("/Products/{id}", async (int id, IProductService service) =>
{
    var Product = await service.GetProductByIdAsync(id);
    return Product is not null
        ? Results.Ok(Product)
        : Results.NotFound(new { message = $"Product with id {id} not found" });
});

app.MapPost("/Products", async (ProductItem Product, IProductService service) =>
{
    var validationResults = new List<ValidationResult>();
    var context = new ValidationContext(Product);
    if (!Validator.TryValidateObject(Product, context, validationResults, true))
    {
        return Results.BadRequest(validationResults);
    }

    var created = await service.CreateProductAsync(Product);
    return Results.Created($"/Products/{created.Id}", created);
});

app.MapPut("/Products/{id}", async (int id, ProductItem Product, IProductService service) =>
{
    var validationResults = new List<ValidationResult>();
    var context = new ValidationContext(Product);
    if (!Validator.TryValidateObject(Product, context, validationResults, true))
    {
        return Results.BadRequest(validationResults);
    }

    var updated = await service.UpdateProductAsync(id, Product);
    return updated
        ? Results.Ok(new { message = "Product updated successfully" })
        : Results.NotFound(new { message = $"Product with id {id} not found" });
});

app.MapDelete("/Products/{id}", async (int id, IProductService service) =>
{
    var deleted = await service.DeleteProductAsync(id);
    return deleted
        ? Results.Ok(new { message = "Product deleted successfully" })
        : Results.NotFound(new { message = $"Product with id {id} not found" });
});


app.Run();