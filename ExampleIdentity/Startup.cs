using ExampleIdentity.Äplication;
using ExampleIdentity.Core.Persistence;
using ExampleIdentity.Middleware;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExampleIdentity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options => options.AddPolicy("corsApp", builder =>
            {
                builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
            }));

            services.AddMediatR(typeof(NewStudent.ExecuteNewStudent).Assembly);
            services.AddAutoMapper(typeof(NewStudent.ExecuteNewStudent));
            services.AddDbContext<ExampleEntityContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ConnectionDB"));
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Entity Example CRUD",
                    Version = "v1"
                });
                c.CustomSchemaIds(c => c.FullName);
            });
            services.AddControllers().AddJsonOptions(x => { x.JsonSerializerOptions.IgnoreNullValues = true; }).
                AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<NewStudent>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<HandlerErrorMiddleware>();

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Example CRUD Identity");
            });

        }
    }
}
