using Microsoft.EntityFrameworkCore;
using Ecommerce.ProductService.Api.Data;
using Ecommerce.ProductService.Api.Endpoints;
using Ecommerce.ProductService.Api.Kafka;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHostedService<Consumer>();
builder.Services.AddDbContext<ProductDbContext>(opt 
    => opt.UseSqlServer(builder.Configuration.GetConnectionString("ProductDefaultConnection")));

var app = builder.Build();

app.MapGroup("v1/products").AddMapProductEndpoints();

app.Run();
