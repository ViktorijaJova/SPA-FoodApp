using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SEDC.FoodApp.Auth.Models;
using SEDC.FoodApp.DataAccess.Mongo.Repositories.Classes;
using SEDC.FoodApp.DataAccess.Mongo.Repositories.Interfaces;
using SEDC.FoodApp.DataAccess.NpgSql;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.FoodApp.Services.Helpers
{
    public static class DIRepositoryModule
    {
        //Microsoft.AspNetCore.Identity.EntityFrameworkCore
        //Microsoft.AspNetCore.Identity.UI
        //Microsoft.Extensions.DependencyInjection
        public static IServiceCollection RegisterRepositories(IServiceCollection services, 
                                                              string mongoConnectionString,
                                                              string mongoDatabase,
                                                              string npgSqlDatabase) 
        {
            //register mongodb repositories
            services.AddScoped<IRestaurantRepository, RestaurantRepository>(provider => 
                new RestaurantRepository(mongoConnectionString, mongoDatabase));

            services.AddScoped<IOrderRepository, OrderRepository>(provider =>
                    new OrderRepository(mongoConnectionString, mongoDatabase));

            //register npog sql db context
            services.AddDbContext<FoodAppUserDbContext>(options =>
            {
                options.UseNpgsql(npgSqlDatabase, options => options.SetPostgresVersion(new Version(9, 5)));
            });

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<FoodAppUserDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredLength = 4;

            });

            return services;
        }
    }
}
