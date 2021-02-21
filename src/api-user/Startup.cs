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

namespace api_user
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        Random random = new Random();

        string[] dictionary = { "Donec","accumsan", "enim", "orci", "nec", "luctus", "velit", "faucibus", "Donec","accumsan", "enim", "orci", "nec", "luctus", "velit", "faucibus" };

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.Map("", builder => {
              builder.Run(async context => {

                var userList = Enumerable.Range(1, random.Next(5, 15)).Select(index => new {
                  Id = index,
                  Name = dictionary[index],
                  BirthDate = DateTime.Now.AddDays(index),
                  Salary = ((random.NextDouble() * 100000) + 50000).ToString("0.##"),
                  ProfilePictureUrl = $"https://i.pravatar.cc/50?{random.Next(1, 1000)}"
                });

                await JsonSerializer.SerializeAsync(context.Response.Body, userList);

              });
            });
        }
    }
}
