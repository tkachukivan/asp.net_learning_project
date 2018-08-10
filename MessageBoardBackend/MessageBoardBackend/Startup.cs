using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MessageBoardBackend
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
            services.AddDbContext<ApiContext>((opt) => opt.UseInMemoryDatabase("root")); 

            services.AddCors(Options => Options.AddPolicy(
                "Cors", 
                BuilderExtensions =>
                {
                    BuilderExtensions.AllowAnyOrigin()
                                     .AllowAnyMethod()
                                     .AllowAnyHeader();
                }));

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is the secter phrase"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(
                (conf) =>
                {
                    conf.RequireHttpsMetadata = false;
                    conf.SaveToken = true;
                    conf.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        IssuerSigningKey = signingKey,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true
                    };
                }
                    
                );
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseCors("Cors");
            app.UseMvc();

            SeedData(serviceProvider.GetService<ApiContext>());
        }

        public void SeedData(ApiContext context)
        {
            context.Messages.Add(
                new Models.Message
                {
                    Owner = "John",
                    Text = "hello"
                }
            );

            context.Messages.Add(
                new Models.Message
                {
                    Owner = "Tim",
                    Text = "hi"
                }
            );

            context.Users.Add(
                new Models.User
                {
                    Email = "a",
                    FirstName = "Tim",
                    LastName = "a",
                    Password = "a",
                    Id = "1"
                }
            );

            context.SaveChanges();
        }
    }
}
