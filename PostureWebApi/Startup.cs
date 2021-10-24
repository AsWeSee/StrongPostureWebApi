using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using PostureWebApi.Controllers;
using PostureWebApi.DBContexts;
using PostureWebApi.Models;

namespace PostureWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //using (var client = new DatabaseContext())
            //{
            //    client.Database.EnsureCreated();
            //}
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PostureWebApi", Version = "v1" });
            });

            //services.AddDbContext<HistoryContext>(options =>
            //    options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<HistoryContext>(options =>
                options
                    .UseNpgsql(getConnString())
                    .UseSnakeCaseNamingConvention());

            services.AddScoped<HistoryController>();

            //services.AddControllers().AddNewtonsoftJson();
            //services.AddEntityFrameworkSqlite().AddDbContext<DatabaseContext>();
        }

        public string getConnString()
        {
            var databaseUrl = @"postgres://neglykwbluotlb:20597e3ef1a3fcfbc64dc3b0de34f636a5f0582b08aba79cfa66ce2e74b9dbdb@ec2-34-242-89-204.eu-west-1.compute.amazonaws.com:5432/d5kv7ae2sfad77";
            //var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
            var databaseUri = new Uri(databaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Require,
                TrustServerCertificate = true
            };

            return builder.ToString();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PostureWebApi v1"));
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
}
