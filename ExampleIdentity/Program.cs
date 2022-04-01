using ExampleIdentity.Aplication;
using ExampleIdentity.Core.Persistence;
using ExampleIdentity.Middleware;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options => options.AddPolicy("corsApp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddMediatR(typeof(NewStudent.ExecuteNewStudent).Assembly);
builder.Services.AddAutoMapper(typeof(NewStudent.ExecuteNewStudent));
builder.Services.AddDbContext<ExampleEntityContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDB"));
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Entity Example CRUD",
        Version = "v1"
    });
    c.CustomSchemaIds(c => c.FullName);
});
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
}).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<NewStudent>());
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<HandlerErrorMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("v1/swagger.json", "Example CRUD Identity");
    });
}
app.UseRouting();
app.UseCors("corsApp");
app.UseAuthorization();
app.MapControllers();
app.Run();
