using Core.Pipelines.Interfaces;
using Infra.Pipelines;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SamplePipeline.Core.Pipeline.Interfaces;
using SamplePipeline.Core.Services.Interfaces;
using SamplePipeline.Infra.Pipeline;
using SamplePipeline.Infra.Pipeline.Steps;
using SamplePipeline.Infra.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamplePipeline.Api
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
            services.AddControllers();

            services.AddSwaggerGen();

            services.AddMemoryCache();

            services.AddTransient<IPipelineBuilder, PipelineBuilder>();

            services.AddTransient<IWeatherForecastService, WeatherForecastService>();

            services.AddTransient<IWeatherForecastPipeline, WeatherForecastPipeline>();

            services.AddTransient<IObterWheatherPipelineStep, ObterWheatherPipelineStep>();
            services.AddTransient<ICacheObterWheatherPipelineStep, CacheObterWheatherPipelineStep>();
            services.AddTransient<ICacheSalvarWheatherPipelineStep, CacheSalvarWheatherPipelineStep>();
            services.AddTransient<IExcutaOutroPipelineStep, ExcutaOutroPipelineStep>();
            services.AddTransient<IAcaoExcutaOutroPipelineStep, AcaoExcutaOutroPipelineStep>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
