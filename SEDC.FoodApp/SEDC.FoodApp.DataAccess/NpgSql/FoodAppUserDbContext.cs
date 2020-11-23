using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SEDC.FoodApp.Auth.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SEDC.FoodApp.DataAccess.NpgSql
{
    //Microsoft.AspNetCore.Identity.EntityFrameworkCore
    //Microsoft.EntityFrameworkCore.Tools
    //Npgsql.EntityFrameworkCore.PostgreSQL
    public class FoodAppUserDbContext : IdentityDbContext
    {
        public FoodAppUserDbContext(DbContextOptions options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
