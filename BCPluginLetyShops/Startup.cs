using System;
using BC.DataContext;
using BC.Interfaces;
using BCPlugin.Interfaces.Services;
using BCPlugin.LetyShops.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BCPlugin.LetyShops;

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
        services.AddScoped<ICashbackService, LetyShopsCashbackService>();
        services.AddScoped<IWebDriver, ChromeDriver>();
        services.AddDbContext<IBCContext, BCContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("BCDatabase")));
        services.AddHttpClient("letyshops", client =>
        {
            client.BaseAddress = new Uri("https://letyshops.com/pl");
        });
        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "BCPluginLetyShops", Version = "v1" });
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BCPluginLetyShops v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}