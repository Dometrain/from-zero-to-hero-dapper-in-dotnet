using System.Data;
using Microsoft.Data.SqlClient;
using Building_a_CRUD_API.Models;
using Building_a_CRUD_API.Repositories;
using Building_a_CRUD_API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IProductRepository, ProductRespository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();


app.MapGet("/products", async (IProductService productService) =>
{
    try
    {
        var products = await productService.GetAllProductsAsync();
        return Results.Ok(products);
    }
    catch(NullReferenceException)
    {
        return Results.NotFound();
    }

});

app.MapPost("/products", async (Product product, IProductService productService) =>
{
    await productService.AddProductAsync(product);
});

app.MapPut("/products", async (Product product, IProductService productService) =>
{
    await productService.UpdateProductAsync(product);
});


app.Run();

