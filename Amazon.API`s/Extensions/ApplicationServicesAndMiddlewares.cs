using Amazon.API_s.Errors;
using Amazon.API_s.Helpers;
using Amazon.core;
using Amazon.core.Entities.Identity;
using Amazon.core.Repositories;
using Amazon.core.Services;
using Amazon.repository;
using Amazon.repository.Data;
using Amazon.repository.Data.Identity;
using Amazon.service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Amazon.API_s.Extensions
{
    public static class ApplicationServicesAndMiddlewares
    {

        public static IServiceCollection InvokeApplicationServices(this IServiceCollection services)
        {
           services.AddControllers();
            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            services.AddScoped<IOrderService, OrderService>();
            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(typeof(MappingProfiles));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) => // InvalidModelStateResponseFactory is the responsible for generating response for validation errors
                {
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0) // gets us key value pair
                                                         .SelectMany(p => p.Value.Errors) // gets us value
                                                         .Select(p => p.ErrorMessage) // gets us message
                                                         .ToArray();
                    var apiResonse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(apiResonse); // the new standard response for validation error
                };

            });

            return services;
        }
        public static IServiceCollection InvokeIdentityServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 15;

            }).AddEntityFrameworkStores<AppIdentityDbContext>();

            services.AddScoped<IPaymentService, PaymentService>();
            services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); // to allow other origins like angular project to access the api project
                                                                                // because browser prevents them by defaults
                });
            });
            services.AddScoped<ITokenService,TokenService>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
            ).
                AddJwtBearer(options =>
                {
                    options.TokenValidationParameters= new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["JWT:ValidAudience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:KEY"])
                        )
                    };
                });
            return services;
        }

        public static WebApplication InvokeApplicationMiddlewares(this WebApplication application)
        {
            application.UseSwagger();
            application.UseSwaggerUI();
            application.UseAuthentication();
            application.UseAuthorization();
            application.UseHttpsRedirection();

            application.UseStatusCodePagesWithReExecute("/error/{0}"); // 0 is a place holder value of 0 will be set by the value binded to int statusCode in the endpoint

            application.UseCors("MyPolicy");
            application.UseStaticFiles();
            application.UseAuthorization();
            application.UseAuthentication();

            application.MapControllers(); // replaces UseRouting "matches request to an endpoint" and UseEndpoints "execute the matched endpoint" pipelines
                                  // relies on attribute routing that is passed to each controller each  
            return application;
        }
    }
}
