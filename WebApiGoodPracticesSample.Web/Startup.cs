using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using System;
using WebApiGoodPracticesSample.Web.DAL;
using WebApiGoodPracticesSample.Web.Helpers;
using WebApiGoodPracticesSample.Web.Model;
using WebApiGoodPracticesSample.Web.Services;

namespace WebApiGoodPracticesSample.Web
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
            services
                .AddControllers()
                .AddNewtonsoftJson(opts => opts.SerializerSettings.Converters.Add(new StringEnumConverter()));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiGoodPracticesSample.Web", Version = "v1" });

                c.CustomSchemaIds(x => x.FullName);
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSingleton<IDataRepository<CarModel>, DataRepository<CarModel>>();
            services.AddSingleton<IDataRepository<DriverModel>, DataRepository<DriverModel>>();
            services.AddSingleton<IService<CarModel>, Service<CarModel>>();
            services.AddSingleton<IService<DriverModel>, Service<DriverModel>>();
            services.AddSingleton<CarService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            IDataRepository<CarModel> carRepository,
            IDataRepository<DriverModel> driverRepo)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiGoodPracticesSample.Web v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            EntityFillerHelper.FillDataBase(carRepository, driverRepo);
        }
    }
}
