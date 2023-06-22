using MASTEK.TEST.DAL;
using static System.Net.Mime.MediaTypeNames;
using AutoMapper;
using MASTEK.TEST.API.MappingProfiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IBeerservice, Beerservice>();
builder.Services.AddScoped<IBreweryService, BreweryService>();
builder.Services.AddScoped<IBarService, BarService>();

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

