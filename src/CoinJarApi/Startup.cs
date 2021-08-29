using CoinJar.Core.CoinJars;
using CoinJar.Core.DataStores;
using CoinJarApi.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using NLog.Web;
using System;
using System.IO;
using System.Reflection;

namespace CoinJarApi
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
      var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

      services.AddControllers();
      services.AddScoped<ICoinJarManager, CoinJarManager>();
      services.AddSingleton<ICoinJar, DefaultCoinJar>();
      services.AddSingleton<IDataStore, FileDataStore>(); // This can be swapped out for the InMemoryDataStore.
      services.AddSingleton<ILogger>(logger);

      services.AddSwaggerGen(options =>
      {
        options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo
        {
          Title = "CoinJar API",
          Version = "v1",
          Description = "Services for managing a coin jar."
        });

        var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
        options.IncludeXmlComments(xmlPath);
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseExceptionHandler("/error");
      app.UseHttpsRedirection();
      app.UseRouting();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      app.UseSwagger();
      app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v2/swagger.json", "CoinJar API"));

    }
  }
}
