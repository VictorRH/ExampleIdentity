
using ExampleIdentity.Aplication;
using ExampleIdentity.Core.Persistence;
using ExampleIdentity.Middleware;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
}).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<NewStudent>());

builder.Services.AddCors(o => o.AddPolicy("corsAPP", builder =>
{
    builder.WithOrigins("*").
            AllowAnyMethod().
            AllowAnyHeader();
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
});
        

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("v1/swagger.json", "Example CRUD Identity");
});
app.UseMiddleware<HandlerErrorMiddleware>();

//app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("corsApp");
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();