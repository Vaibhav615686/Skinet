using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddScoped<IProductRespository, ProductRepository>();
builder.Services.AddScoped(typeof(IGeneralRepository<>), typeof(GeneralRepository<>));
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
     app.MapOpenApi();

    // Scalar UI (modern Swagger UI replacement)
    app.MapScalarApiReference(options =>
    {
        options.Title = "My API";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
// try
// {
//     using var scope = app.Services.CreateScope();
//     var services = scope.ServiceProvider;
//     var context = services.GetRequiredService<AppDbContext>();
//     await context.Database.MigrateAsync();
//     await ProductContextSeed.SeedAsync(context);
// }
// catch (Exception ex)
// {
//     Console.WriteLine(ex);
//     throw;

// }
app.Run();
