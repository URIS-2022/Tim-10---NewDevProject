using Buyer.Data;
using Buyer.Entities;
using Buyer.Helpers;
using Buyer.ServiceCalls;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.IO;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Buyer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(setup => setup.ReturnHttpNotAcceptable = true)
                .AddXmlDataContractSerializerFormatters();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IBuyerRepository, BuyerRepository>();
            services.AddScoped<IPriorityRepository, PriorityMockRepository>();
            services.AddScoped<IContactPersonRepository, ContactPersonMockRepository>();
            services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
            services.AddScoped<IUserRepository, UserMockRepository>();
            services.AddScoped<IIndividialRepository, IndividualMockRepository>();
            services.AddScoped<ILegalEntityRepository, LegalEntityMockRepository>();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IPublicBiddingService, PublicBiddingService>();

            services.AddDbContext<BuyerContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BuyerDB")));

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("DocumentOpenApiSpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Document API",
                        Version = "1",
                        Description = "Using this API you can maipulate buyer data.",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Amila Salihbegovic",
                            Email = "milasalihbegovic2@gmail.com",
                            Url = new Uri("http://www.ftn.uns.ac.rs/")
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense
                        {
                            Name = "FTN licence",
                            Url = new Uri("http://www.ftn.uns.ac.rs/")
                        },
                        TermsOfService = new Uri("http://www.ftn.uns.ac.rs/examRegistrationTermsOfService")
                    });
            });


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

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            } else
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Error has occured. Please try again later.");
                    });
                });
            }
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/DocumentOpenApiSpecification/swagger.json", "Dokument API");
                setupAction.RoutePrefix = "";
            });


            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
