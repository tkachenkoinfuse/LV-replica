using Hangfire;
using Hangfire.MySql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using Serilog;

using System.IO;
using ServiceWithHangfire.DAL;


namespace ServiceWithHangfire
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        public IConfiguration _config { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            _config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

            services.AddLogging(configure => configure.AddSerilog());
            try
            {
                Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(_config).CreateLogger();

                //add telegram logger
                TelegramLogger tLogger = new TelegramLogger();
                tLogger.registerTelegramLogger();

            }
            catch (Exception e)
            {
                TelegramLogger.TLogger.LogError(e, "Error: In Startup -> AddLogging ");
                Log.CloseAndFlush();
            };

            services.AddHangfire(configuration =>
            {
                configuration.UseStorage(
                    new MySqlStorage(
                        Constant.connectionStringHangfire,
                        new MySqlStorageOptions
                        {
                           TablesPrefix = "Hangfire"
                        }
                    )
                );


                AppSettings _appSettings = _config.GetSection("AppSettings").Get<AppSettings>();

                Perform perform = new();
                perform.StartPerform(Constant.serviceName, _appSettings);
            }
            );


            services.AddHangfireServer(BackgroundJobServerOptions => BackgroundJobServerOptions.WorkerCount = 5);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hangfire service", Version = "v1" });
            });

            var container = new ServiceResolver(services, _config).GetServiceProvider();
            return container;
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hangfire"));
                
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseHangfireDashboard(Constant.hangfireDashboardUrl);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

    }
}
