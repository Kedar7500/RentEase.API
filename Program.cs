using Microsoft.EntityFrameworkCore;
using RentEase.API.Data;
using RentEase.API.Mappings;
using RentEase.API.Repositories.Implementations;
using RentEase.API.Repositories.Interfaces;
using RentEase.API.Services.Implementations;
using RentEase.API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// adding service for dbcontext
builder.Services.AddDbContext<RentEaseDbContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("RentEaseConnectionString"))
);

builder.Services.AddScoped<IPropertyInterface, PropertyRepository>();
builder.Services.AddScoped<IPropertyService, PropertyService>();

builder.Services.AddAutoMapper(typeof (AutoMapperProfile));

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
