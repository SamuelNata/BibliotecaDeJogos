using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using GameLib.Repository.DbContext;
using GameLib.API.Extensions;
using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using System.Reflection;
using GameLib.Model.Entity;
using AutoMapper;

namespace GameLib.API
{
    public class Startup
    {
        private string Secret;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Secret = Configuration["App:Secret"];

            services.AddCors();
            services.AddControllers();

            var key = Encoding.ASCII.GetBytes(Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddDbContext<GameLibDbContext>(options =>
                options
                    .UseNpgsql(
                        Configuration.GetConnectionString("GameLib"),
                        o => o.MigrationsAssembly("GameLib.API")
                    ).UseSnakeCaseNamingConvention()
            );

            services.AddSwaggerGen(options =>
            {
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "JWT",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Header com token de autorização JWT. Exemplo: ""Authorization: Bearer {token}""",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "GameLib API",
                    Version = "v1",
                    Description = "Se how to use all of our endpoints",
                });
            });

            services.AddServicesAndRepositories();

            services.AddAutoMapper(Assembly.GetAssembly(typeof(User)));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GameLibDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                dbContext.Database.Migrate();
            }

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();  
            app.UseSwaggerUI(options =>options.SwaggerEndpoint("/swagger/v1/swagger.json", "GameLib API"));
        }
    }
}
