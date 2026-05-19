using Amazon.API_s.Errors;
using Amazon.API_s.Extensions;
using Amazon.API_s.Helpers;
using Amazon.API_s.Middlewares;
using Amazon.core.Entities.Identity;
using Amazon.core.Repositories;
using Amazon.repository;
using Amazon.repository.Data;
using Amazon.repository.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace Amazon.API_s
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region Configure Services

            // Add services to the container.

            builder.Services.InvokeApplicationServices(); // we got our services in an external class and made it an extension method and called it here in order to be clean

            builder.Services.InvokeIdentityServices(builder.Configuration);

            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(S =>
            {
                var connection = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });
            


            #endregion

            var app = builder.Build();

            


            #region Ask CLR explicitly to create us an object from StoreContext in order to update database automatically
            var scope = app.Services.CreateScope(); // all services that are of scope type

            var services = scope.ServiceProvider; // get me all that kind of services

            var loggerFactory = app.Services.GetRequiredService<ILoggerFactory>();

            

            try
            {
                var dbContext = services.GetRequiredService<StoreContext>();
                await dbContext.Database.MigrateAsync();
                var identityDbContext = services.GetRequiredService<AppIdentityDbContext>();
                

                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                await identityDbContext.Database.MigrateAsync();
                await AppIdentityDbContextSeed.SeedUsersAsync(userManager);

                

                await StoreContextSeed.SeedData(dbContext);
                
            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An error has occurred during applying the migration");
            }
            #endregion

            // Configure the HTTP request pipeline.
            app.UseMiddleware<ExceptionMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                
                //app.UseDeveloperExceptionPage();
                app.InvokeApplicationMiddlewares();
            }
            
            
            app.Run();
        }
    }
}
