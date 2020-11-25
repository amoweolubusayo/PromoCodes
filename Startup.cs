using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PromoCodes_main.Infrastructure.Utility.Security;

namespace PromoCodes_main {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddControllers ();
            services.AddAuthentication (options => {
                    options.DefaultAuthenticateScheme = "JwtBearer";
                    options.DefaultChallengeScheme = "JwtBearer";
                })
                .AddJwtBearer ("JwtBearer", jwtBearerOptions => {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuerSigningKey = TokenConstants.ValidateSigningKey,
                    IssuerSigningKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (TokenConstants.SecurityKey)),
                    ValidateIssuer = TokenConstants.ValidateIssuer,
                    ValidIssuer = TokenConstants.Issuer,
                    ValidateAudience = TokenConstants.ValidateAudience,
                    ValidAudience = TokenConstants.Audience,
                    ValidateLifetime = TokenConstants.ValidateLifeTime, //validate the expiration and not before values in the token
                    ClockSkew = TimeSpan.FromMinutes (TokenConstants.DateToleranceMinutes) //5 minute tolerance for the expiration date
                    };
                });
            services.AddAuthorization (config => {
                config.AddPolicy (Policies.Admin, Policies.AdminPolicy ());
                config.AddPolicy (Policies.User, Policies.UserPolicy ());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseHttpsRedirection ();

            app.UseRouting ();
            app.UseAuthentication ();
            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }
    }
}