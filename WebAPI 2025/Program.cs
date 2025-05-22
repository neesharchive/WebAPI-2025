using Microsoft.EntityFrameworkCore;
using WebAPI_2025.Data;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using WebAPI_2025.Services;
using WebAPI_2025.Mappers;
using WebAPI_2025.Repositories;
using AutoMapper.Internal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IGuestHouseRepository, GuestHouseRepository>();
builder.Services.AddScoped<IGuestHouseService, GuestHouseService>();
builder.Services.AddAutoMapper(cfg => cfg.Internal().MethodMappingEnabled = false, typeof(AutoMapperProfile).Assembly);

builder.Services.AddDbContext<AppDbContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
