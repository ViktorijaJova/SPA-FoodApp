using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SEDC.FoodApp.Services.Helpers;
using SEDC.FoodApp.Services.Services.Classes;
using SEDC.FoodApp.Services.Services.Interfaces;

namespace SEDC.FoodApp.Web
{
    //Microsoft.AspNetCore.Cors
    //Swashbuckle.AspNetCore
    //Microsoft.EntityFrameworkCore.Design
    //Microsoft.IdentityModel.Tokens
    //System.IdentityModel.Tokens.Jwt
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var connectionStrings = Configuration.GetSection("ConnectionStrings");

            //Mongo DB
            var mongoCs = connectionStrings.GetValue<string>("MongoConncetionString");
            var mongoDbName = connectionStrings.GetValue<string>("MongoDatabase");

            //NpgSql DB
            var npgSqlCs = connectionStrings.GetValue<string>("NpgSqlDatabase");

            //register services
            services.AddTransient<IRestaurantService, RestaurantService>();
            services.AddTransient<IOrderService, OrderService>();

            //Dependency Injection Module
            DIRepositoryModule.RegisterRepositories(services, mongoCs, mongoDbName, npgSqlCs);

            //swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My Api", Version = "v1" });
            });

            //JWT Authentication
            var key = Encoding.UTF8.GetBytes(Configuration.GetSection("ApplicationSettings").GetValue<string>("JWT_secret"));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            //cors
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Api v1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //cors
            app.UseCors(builder => 
                builder.WithOrigins("http://localhost:4200", "http://localhost:45551")
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
