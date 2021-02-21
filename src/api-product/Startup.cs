using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace api_product
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        Random random = new Random();

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.Map("/random", builder => {
              builder.Run(async context => {
                var product = new {
                  Id = random.Next(1, 50),
                  Date = DateTime.Now.AddDays(random.Next(-20, 20)),
                  Price = (random.NextDouble() * 100).ToString("0.##"),
                  StockCount = random.Next(5, 50),
                  PictureUrl = $"https://picsum.photos/50?{random.Next(1, 1000)}"
                };

                await JsonSerializer.SerializeAsync(context.Response.Body, product);
              });
            });

            app.Map("", builder => {
              builder.Run(async context => {
                var productList = Enumerable.Range(1, random.Next(1, 20)).Select(index => new {
                  Id = index,
                  Date = DateTime.Now.AddDays(index),
                  Price = (random.NextDouble() * 100).ToString("0.##"),
                  StockCount = random.Next(5, 50),
                  PictureUrl = $"https://picsum.photos/50?{random.Next(1, 1000)}"
                });

                await JsonSerializer.SerializeAsync(context.Response.Body, productList);
              });
            });
        }
    }
}
