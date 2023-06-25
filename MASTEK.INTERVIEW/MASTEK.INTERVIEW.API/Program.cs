using MASTEK.INTERVIEW.DAL;
using static System.Net.Mime.MediaTypeNames;
using AutoMapper;
using MASTEK.INTERVIEW.API.MappingProfiles;
using MASTEK.INTERVIEW.ENTITY;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<TestMastekDbContext>(
        options
        => options.UseMySQL("server=104.198.178.39;port=3306;user=ef;password=Ef@2023;database=test-mastek-db;"));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IBeerservice<TestMastekDbContext>, Beerservice<TestMastekDbContext>>();
builder.Services.AddScoped<IBreweryService<TestMastekDbContext>, BreweryService<TestMastekDbContext>>();
builder.Services.AddScoped<IBarService<TestMastekDbContext>, BarService<TestMastekDbContext>>();
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

