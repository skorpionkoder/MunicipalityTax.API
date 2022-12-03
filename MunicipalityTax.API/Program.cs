using Microsoft.EntityFrameworkCore;
using MunicipalityTax.API.Data.Contexts;
using MunicipalityTax.API.Data.Interfaces;
using MunicipalityTax.API.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<MunicipalityTaxContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("MunicipalityTax");
    options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<IMunicipalityTaxRepository, MunicipalityTaxRepository>();

builder.Services.AddDbContext<MunicipalityContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("MunicipalityTax");
    options.UseSqlServer(connectionString);
});
builder.Services.AddScoped<IMunicipalityRepository, MunicipalityRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
