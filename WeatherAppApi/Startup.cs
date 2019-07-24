using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WeatherAppApi.Models;
using WeatherAppApi.Services;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Cors.CorsAuthorizationFilterFactory;
using Microsoft.AspNetCore.Mvc.Cors.Internal;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace WeatherAppApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            //var builder = new ConfigurationBuilder()
				//.SetBasePath(env.ContentRootPath)
			//	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
				//.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            //     if (env.IsDevelopment())
			// {
			// 	// For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
			// 	builder.AddUserSecrets<Startup>();
			// }
               // builder.AddEnvironmentVariables();
			   // Configuration = builder.Build();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {


        //   services.AddCors();
        //    services.AddCors(options =>  
        //     {  
        //         options.AddPolicy("CorsPolicy",//Allow Cross origin  
        //             builder => builder.AllowAnyOrigin()  
        //             .AllowAnyMethod()  
        //             .AllowAnyHeader()  
        //             .AllowCredentials()); 
        // //     });
            services.AddCors(options =>
                {
                options.AddPolicy("AllowMyOrigin",
                // builder => builder.WithOrigins("http://192.168.99.100:4200",
                //                                 "http://localhost:4200",
                //                                 "http://192.168.99.100:5000",
                //                                 "http://localhost:5000")
                //                                 .AllowAnyHeader()
                //                                 .AllowAnyMethod();
                 builder =>
                            {
                                 builder.AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                            });
                });
            services.Configure<WeatherstoreDatabaseSettings>(
             Configuration.GetSection(nameof(WeatherstoreDatabaseSettings)));

             services.AddSingleton<IWeatherstoreDatabaseSettings>(sp =>
             sp.GetRequiredService<IOptions<WeatherstoreDatabaseSettings>>().Value);
             services.AddSingleton<WeatherService>();

            services.AddMvc()
            .AddJsonOptions(options => options.UseMemberCasing())
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.Configure<MvcOptions>(options =>
            {
            options.Filters.Add(new CorsAuthorizationFilterFactory("AllowMyOrigin"));
            });

            //Redis Implementation

            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = "redis_image:6379,abortConnect=False";
                  option.InstanceName = "master";                  
                  //option.abortConnect=false;
            });
         
            //For Swagger MiddleWare 
            services.AddSwaggerGen(c =>
            {
             c.SwaggerDoc("v1", new OpenApiInfo { 
                 Title = "My WeatherApp API",
                  Version = "v1",
                  Description="A application for comparing weather report from various API",  
                  Contact =new OpenApiContact
                  {
                      Name = "The Weather Man",
                      Email=string.Empty
                  }        
                   });
              //  c.IncludeXmlComments(System.IO.Path.Combine(System.AppContext.BaseDirectory,"Swagger.xml"));
                 //  var xmlFile = System.AppDomain.CurrentDomain.BaseDirectory - @"commentSwagger.xml";
                  // c.IncludeXmlComments(xmlFile);
            });
            //for bearer
            var key = Encoding.UTF8.GetBytes("1234567891234567");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x=> {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            
            // app.UseCors("fiver");  
            app.UseCors("AllowMyOrigin");
            app.UseHttpsRedirection();
            app.UseMvcWithDefaultRoute();  

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

           //For Swaggeer EndPoint
           app.UseSwagger();

           // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
           app.UseSwaggerUI(c =>
            {
          c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Meather Application Api");
            });
           // }
 
           // app.UseHttpsRedirection();        //Commented By Abhishek
          //For JWT Token

        //  app.UseCors(builder =>
         //   builder.WithOrigins(Configuration["ApplicationSettings:Client_URL"].ToString())
            //.AllowAnyHeader()
          //  .AllowAnyMethod()
            
            //);
            app.UseAuthentication();
            app.UseMvc();
           
           
    }
    }
}

