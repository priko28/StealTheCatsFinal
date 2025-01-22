using Microsoft.EntityFrameworkCore;
using StealTheCats.Core.Repositories;
using StealTheCats.Core.Services;
using StealTheCats.Data.Context;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

// Add services to the container.
builder.Services.AddDbContext<CatDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CatDB")));

builder.Services.AddHttpClient().AddScoped<ICatRepository, CatRepository>();
builder.Services.AddHttpClient().AddScoped<ICatService, CatService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
