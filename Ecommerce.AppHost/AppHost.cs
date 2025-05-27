var builder = DistributedApplication.CreateBuilder(args);

var productApi 
    = builder.AddProject<Projects.Ecommerce_ProductService_Api>("apiservice-product");
var orderApi 
    = builder.AddProject<Projects.Ecommerce_OrderService_Api>("apiservice-order");

builder.AddProject<Projects.Ecommerce_Blazor_Web>("webfrontend")
    .WithReference(productApi)
    .WithReference(orderApi);

builder.Build().Run();