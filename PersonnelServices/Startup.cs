using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using PersonnelServices.DAL;
using PersonnelServices.DAL.Interface;
using Swashbuckle.AspNetCore.Swagger;

namespace PersonnelServices
{
    public class Startup
    {
        private readonly string productName = "PersonnelService";


        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder()
                             .SetBasePath(Directory.GetCurrentDirectory())
                             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                             .AddJsonFile($"appsettings.{env?.EnvironmentName}.json", optional: true);

            Configuration = builder.Build();
            Environment = env;
        }

        public IConfiguration Configuration { get; }

        private IHostingEnvironment Environment { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            AddApplicationsService(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = $"Connected " + productName + " API", Version = "v1" });
                c.EnableAnnotations();
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().WithMethods("GET", "PUT", "POST", "OPTIONS", "HEAD"));

            app.UseMvc();
            app.UseSwagger(c => { });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Configuration.GetSection("Urls").GetValue<string>("swagger"), "Connected " + productName + " API V1");
            });
        }

        private void AddApplicationsService(IServiceCollection services)
        {
            // Database mongo client
            var connectionString = Configuration.GetSection("ConnectionStrings").GetValue<string>("MongoDBConnectionString");
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(connectionString));
            settings.MaxConnectionIdleTime = TimeSpan.FromMinutes(1);
            var mongoClient = new MongoClient(settings);
            services.AddSingleton<MongoClient>(mongoClient);

            services.AddSingleton<IRepository, MongoRepository>();
        }
    }
}
