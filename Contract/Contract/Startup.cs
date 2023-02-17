﻿using Contract.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Contract.Helpers;
using Contract.ServiceCalls;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using Contract.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contract
{
    public class Startup
    {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(setup =>
            setup.ReturnHttpNotAcceptable = true)
                .AddXmlDataContractSerializerFormatters()
                .ConfigureApiBehaviorOptions(setupAction =>
                {
                    setupAction.InvalidModelStateResponseFactory = context =>
                    {
                        ProblemDetailsFactory problemDetailsFactory = context.HttpContext.RequestServices.GetService<ProblemDetailsFactory>();
                        ValidationProblemDetails problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                            context.HttpContext,
                            context.ModelState);
                        problemDetails.Detail = "Check the errors field for more information";
                        problemDetails.Instance = context.HttpContext.Request.Path;

                        var actionExecutingContext = context as ActionExecutingContext;

                    if ((context.ModelState.ErrorCount > 0) &&
                        actionExecutingContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count)
                        {
                            problemDetails.Type = "https://google.com";
                            problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                            problemDetails.Title = "An error occurred during validation";

                            return new UnprocessableEntityObjectResult(problemDetails)
                            {
                                ContentTypes = { "application/problem+json" }
                            };
                        }
                        problemDetails.Status = StatusCodes.Status400BadRequest;
                        problemDetails.Title = "An error occurred during parsing the sent content";
                        return new BadRequestObjectResult(problemDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    };
                });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
            services.AddControllers();
            services.AddSingleton<IContractRepository, ContractMock>();
            services.AddSingleton<ITypeOfGuaranteeRepository, TypeOfGuaranteeMock>();
            services.AddSingleton<IUserRepository, UserMock>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IBuyerService, BuyerService>();
            services.AddScoped<IPublicBiddingService, PublicBiddingService>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();

            

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("ContractOpenApiSpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Contract API",
                        Version = "1",

                        Description = "With this API you can add a contract, update it as well as review all contracts",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Jovana Tanackovic",
                            Email = "tanackovicjovana00@gmail.com",
                            Url = new Uri("http://www.ftn.uns.ac.rs/")
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense
                        {
                            Name = "FTN licence",
                            Url = new Uri("http://www.ftn.uns.ac.rs/")
                        },
                        TermsOfService = new Uri("http://www.ftn.uns.ac.rs/examRegistrationTermsOfService")
                    });


                var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";


                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

                setupAction.IncludeXmlComments(xmlCommentsPath);
            });

            services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("Contract")));


        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("An unknown error occurred, please try later.");
                    });
                });
            }
            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/ContractOpenApiSpec/swagger.json", "Contract API");
                setupAction.RoutePrefix = "";
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
