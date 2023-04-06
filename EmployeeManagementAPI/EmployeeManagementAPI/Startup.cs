using AutoMapper;
using EmployeeManagement.Api.Helper;
using EmployeeManagement.Entities.Models.DTOModels;
using EmployeeManagement.Entities.Models.EntityModels;
using EmployeeManagement.Entities.Models.PayloadModel;
using EmployeeManagement.Helper;
using EmployeeManagement.Api.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using EmployeeManagement.Entities.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace EmployeeManagement.Api
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration()
                //.Enrich.WithProcessId()                
                .Enrich.WithCorrelationId()  
                .ReadFrom.Configuration(configuration)                
                .CreateLogger();           
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy("CORS", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddDbContext<EmployeeManagementContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SecureConnection"));
            });

            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(MyMappingHelper<,>).MakeGenericType(typeof(Employeepayload), typeof(EmployeeDTO)));
                cfg.AddProfile(typeof(MyMappingHelper<,>).MakeGenericType(typeof(EmployeeDTO), typeof(Employee)));
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.RegisterServices();

            services.AddControllers().AddNewtonsoftJson();

            services.AddEndpointsApiExplorer();
                     
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });


                       
            services.AddMvc();
          
            services.AddHttpContextAccessor();
          
        } 

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }
           
            app.UseMiddleware<LogHeaderMiddleware>();
           

            app.UseExceptionHandler(
                 options =>
                 {
                     options.Run(
                         async context =>
                         {
                             context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                             context.Response.ContentType = "text/html";
                             var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
                             if (null != exceptionObject)
                             {
                                 var errorMessage = $"<b>Exception Error: {exceptionObject.Error.Message} </b> {exceptionObject.Error.StackTrace}";
                                 await context.Response.WriteAsync(errorMessage).ConfigureAwait(false);
                             }
                         });
                 });

           

            app.UseCors("CORS");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
