using System;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using JqueryDataTables.ServerSide.AspNetCoreWeb.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProjectTraining.Common;
using ProjectTraining.DataAccess;
using ProjectTraining.Filters;
using ProjectTraining.Middleware;
using ProjectTraining.Repositories;
using ProjectTraining.Services;
using ProjectTraining.Validations;
using ProjectTraining.ViewModels;

namespace ProjectTraining
{
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    
                })
                .AddCookie(options =>
                {
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
                    options.LoginPath = Constants.LoginPath;
                    options.LogoutPath = Constants.LogoutPath;
                    options.ReturnUrlParameter = Constants.ReturnUrlParameter;
                    options.AccessDeniedPath = new PathString(Constants.PathError401);
                });
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true, // có validate Server tạo JWT không ?
                        ValidateAudience = true,
                        ValidateLifetime = true, //có validate expire time hay không ?
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration[Constants.JwtIssuer],
                        ValidAudience = Configuration[Constants.JwtIssuer],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration[Constants.JwtKey]))
                    };
                });
            
            services.AddAutoMapper();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvc(setup => {
            }).AddFluentValidation();
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(Constants.ConnectionString)));
            services.AddJqueryDataTables();
            services.AddSession();
            services.AddScoped<IUnitOfWork, UnitOfwork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ExpireDateUserFilter>();
            services.AddTransient<IValidator<LoginViewModel>, LoginValidate>();
            services.AddAuthorization(options =>
            {
                options.AddPolicy(Constants.Admin, policy => policy.RequireClaim( ClaimTypes.Role ,Constants.RoleAdmin));
                options.AddPolicy(Constants.User, policy => policy.RequireClaim(ClaimTypes.Role,Constants.RoleUser));
                options.AddPolicy(Constants.AllowAll, policy => policy.RequireRole(ClaimTypes.Role,Constants.RoleAdmin,Constants.RoleUser));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(Constants.PathHomeError);
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();
            
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}