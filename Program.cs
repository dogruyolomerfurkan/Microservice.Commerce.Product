using Commerce.Core.MassTransit;
using Commerce.Core.MongoDB;
using Commerce.Product.Service.Entities;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMongoDB()
                .AddMongoDBRepository<Product>("Products")
                .AddMassTransitWithRabbitMq();

builder.Services.AddControllers(options => { options.SuppressAsyncSuffixInActionNames = false; });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();