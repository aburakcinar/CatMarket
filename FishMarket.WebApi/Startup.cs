using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FishMarket.WebApi.Data.Contexts;
using FishMarket.WebApi.Data.Interfaces;
using FishMarket.WebApi.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;

namespace FishMarket.WebApi
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
            services.AddMvc();

            var sqlConnectionString = Configuration.GetConnectionString("FishMarketContext");

            services.AddDbContext<FishMarketContext>(options =>
                options.UseMySQL(
                    sqlConnectionString
                )
            );

            //services.AddScoped<IFishDefinitionRepository, FishDefinitionRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
            .AddJwtBearer("JwtBearer", jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("FishMarket.Web.qqq123")),

                    ValidateIssuer = true,
                    ValidIssuer = "FishMarket.Web",

                    ValidateAudience = true,
                    ValidAudience = "FishMarket.Web",

                    ValidateLifetime = true, //validate the expiration and not before values in the token

                    ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            //env.ConfigureNLog("nlog.config");
            //app.AddNLogWeb();

            app.UseMvc();

        }
    }
}
