using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PromoCodes_main.Infrastructure.Persistence;
using PromoCodes_main.Infrastructure.Swagger;
using PromoCodes_main.Infrastructure.Utility.Security;
using PromoCodes_main.Services;
using PromoCodes_main.Application.Entities;



namespace PromoCodes_main {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }
       


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddSwaggerService (Configuration);
          
           services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
            .AddJwtBearer("JwtBearer", jwtBearerOptions =>
         {
             jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuerSigningKey = TokenConstants.ValidateSigningKey,
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenConstants.SecurityKey)),
                 ValidateIssuer = TokenConstants.ValidateIssuer,
                 ValidateAudience = TokenConstants.ValidateAudience,
                 ValidAudience = TokenConstants.Audience,
                 ClockSkew = TimeSpan.Zero 
             };
         });

            // services.AddAuthorization (config => {
            //     config.AddPolicy (Policies.Admin, Policies.AdminPolicy ());
            //     config.AddPolicy (Policies.User, Policies.UserPolicy ());
            // });
            services.Configure<GeneralSettings> (Configuration.GetSection ("GeneralSettings"));

            // configure DI for application services
            services.AddScoped<IUserService, UserService> ();
            //services.AddAuthentication().AddJwtBearer();
            services.AddPersistence (Configuration);
            services.AddMvcCore ()
                .AddApiExplorer ();
            services.AddMediatR (Assembly.GetExecutingAssembly ());
            services.AddCors ();
            //services.AddAuthorization ();
            services.AddControllers();
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }
            app.UseSwaggerService (Configuration);
            //app.UseHttpsRedirection();

            app.UseRouting ();
            app.UseAuthentication ();
            app.UseAuthorization ();
            app.UseCors (x => x
                .AllowAnyOrigin ()
                .AllowAnyMethod ()
                .AllowAnyHeader ());
            app.UseMiddleware<JwtMiddleware> ();
            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });

        }
    }
}