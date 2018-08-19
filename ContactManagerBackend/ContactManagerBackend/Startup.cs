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

namespace ContactManagerBackend
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
            Models.User MockUser = new Models.User
            {
                Email = "a",
                FirstName = "Tim",
                LastName = "a",
                Password = "a",
                Id = Guid.NewGuid(),
            };

            context.Users.Add(
                MockUser
            );

            context.Contacts.Add(
                new Models.Contact
                {
                    Email = "a",
                    FirstName = "Tim",
                    MiddleName = "MIDDLE",
                    LastName = "a",
                    OwnerId = MockUser.Id,
                    Id = Guid.NewGuid(),
                    Address = new Models.Address
                    {
                        Country = "Poland",
                        City = "Krakow",
                        Street = "Rynok Glowny",
                        Building = "1",
                        Appartments = "1",
                        ZipCode = "1",
                    },
                    Birthdate = DateTime.Today,
                    Phones = new List<Models.Phone>
                        {
                            new Models.Phone
                            {
                                Type = Enums.PhoneType.Home,
                                Number = "1234323",
                            },
                            new Models.Phone
                            {
                                Type = Enums.PhoneType.Other,
                                Number = "12133455",
                            }
                        }
                }
            );

            context.SaveChanges();
        }
    }
}
