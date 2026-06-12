using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalksAPI.Mapper;
using NZWalksAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NZWalksDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add AutoMapper using extension method
builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<AutoMapperProfiles>();
});

// Add Repositories
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();

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
