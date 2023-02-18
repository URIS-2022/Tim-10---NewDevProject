using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Personality.Data;
using Personality.Entities;
using System.Text;
using Personality.ServiceCalls;
using Personality.Helper;

namespace Personality
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
            services.AddSingleton<IPersonalityRepository, PersonalityRepository>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();



            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("UserOpenApiSpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Personality API",
                        Version = "1",

                        Description = "With this API you can add a user, update it as well as review all users",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Una Obradovic",
                            Email = "unaobradovic5@gmail.com",
                            Url = new Uri("http://www.ftn.uns.ac.rs/")
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense
                        {
                            Name = "FTN licence",
                            Url = new Uri("http://www.ftn.uns.ac.rs/")
                        },
                    });


                var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";


                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

                setupAction.IncludeXmlComments(xmlCommentsPath);
            });

            services.AddDbContext<PersonalityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Personality")));


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
                setupAction.SwaggerEndpoint("/swagger/PersonalityOpenApiSpec/swagger.json", "Personality API");
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
