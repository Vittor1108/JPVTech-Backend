using JpvTech.Application.Injectors;
using JpvTech.Application.Mappers;
using JPVTech.Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//CONECTION STRING

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.ServiceCollectionRepositories();
builder.Services.ServiceCollectionServices();
builder.Services.ServicesCollectionCommons();

builder.Services.AddSingleton(AutoMapperConfig.ConfigureAutoMapper());
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<SqlContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")));

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
{
    builder.AllowAnyHeader()
          .AllowAnyMethod()
          .SetIsOriginAllowed((host) => true);
}));


var app = builder.Build();
app.UseCors("CorsPolicy");

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
