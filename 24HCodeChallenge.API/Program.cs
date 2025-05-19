using _24HCodeChallenge.API.Services;
using _24HCodeChallenge.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using MyProject.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IPersistence<Pizza, string>, Persistence<Pizza, string>>();
builder.Services.AddScoped<IPersistence<PizzaType, string>, Persistence<PizzaType, string>>();
builder.Services.AddScoped<IPersistence<Order, int>, Persistence<Order, int>>();
builder.Services.AddScoped<IPersistence<OrderDetail, int>, Persistence<OrderDetail, int>>();
builder.Services.AddScoped<IPizzaService, PizzaService>();
builder.Services.AddScoped<IPizzaTypeService, PizzaTypeService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderDetailService, OrderDetailService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
//builder.Services.AddAutoMapper(typeof(PizzaProfile));
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();

app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
