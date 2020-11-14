using Microsoft.Extensions.DependencyInjection;
using SEDC.FoodApp.DataAccess.Mongo.Repositories.Classes;
using SEDC.FoodApp.DataAccess.Mongo.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.FoodApp.Services.Helpers
{
    public static class DIRepositoryModule
    {

        public static IServiceCollection RegisterRepositories(IServiceCollection services, 
                                                              string mongoConnectionString,
                                                              string mongoDatabase) 
        {
            //register mongodb repositories
            services.AddScoped<IRestaurantRepository, RestaurantRepository>(provider => 
                new RestaurantRepository(mongoConnectionString, mongoDatabase));

            return services;
        }
    }
}
