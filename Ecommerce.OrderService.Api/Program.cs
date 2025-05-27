using Microsoft.EntityFrameworkCore;
using Ecommerce.OrderService.Api.Data;
using Ecommerce.OrderService.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrderDbContext>(opt 
    => opt.UseSqlServer(builder.Configuration.GetConnectionString("OrderDefaultConnection")));

var app = builder.Build();

app.MapGroup("v1/orders").AddMapOrderEndpoints();

app.Run();
