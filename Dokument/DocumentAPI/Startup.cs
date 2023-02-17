using DocumentAPI.Data;
using DocumentAPI.Entities;
using DocumentAPI.Helpers;
using DocumentAPI.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;



namespace DocumentAPI
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
			services.AddControllers(setup =>
				setup.ReturnHttpNotAcceptable = true
			).AddXmlDataContractSerializerFormatters()
			.ConfigureApiBehaviorOptions(setupAction =>
			{
				setupAction.InvalidModelStateResponseFactory = context =>
				{
					ProblemDetailsFactory problemDetailsFactory = context.HttpContext.RequestServices
						.GetRequiredService<ProblemDetailsFactory>();
					ValidationProblemDetails problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
						context.HttpContext,
						context.ModelState);

					problemDetails.Detail = "Pogledajte polje errors za detalje.";
					problemDetails.Instance = context.HttpContext.Request.Path;

					var actionExecutiongContext = context as ActionExecutingContext;

					if ((context.ModelState.ErrorCount > 0) &&
						(actionExecutiongContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count))
					{
						problemDetails.Type = "https://google.com";
						problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
						problemDetails.Title = "Došlo je do greške prilikom validacije.";


						return new UnprocessableEntityObjectResult(problemDetails)
						{
							ContentTypes = { "application/problem+json" }
						};
					}

					problemDetails.Status = StatusCodes.Status400BadRequest;
					problemDetails.Title = "Došlo je do greške prilikom parsiranja poslatog sadržaja.";
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
			services.AddSingleton<IDocumentRepository, DocumentRepository>();
			services.AddSingleton<IStatusOfDocumentRepository, StatusOfDocumentRepository>();
			services.AddSingleton<ITypeOfDocumentRepository, TypeOfDocumentRepository>();
			services.AddSingleton<IUserRepository, UserMockRepository>();
			services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
			services.AddScoped<ILoggerService, LoggerService>();


			services.AddSwaggerGen(setupAction =>
			{
				setupAction.SwaggerDoc("DocumentOpenApiSpecification",
					new Microsoft.OpenApi.Models.OpenApiInfo()
					{
						Title = "Document API",
						Version = "1",

						Description = "Pomoću ovog API-ja može da se vrši dodavanje dokumenata i izmenje njihovih stanja, kao i njihov pregled",
						Contact = new Microsoft.OpenApi.Models.OpenApiContact
						{
							Name = "Sandra Melovic",
							Email = "sandramelovic00@gmail.com",
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

			services.AddDbContext<DocumentContext>(options => options.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DocumentDB;Integrated Security=True;Connect Timeout=30;"));



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
						await context.Response.WriteAsync("Došlo je do neočekivane greške. Molimo pokušajte kasnije.");
					});
				});
			}
			app.UseSwagger();
			app.UseSwaggerUI(setupAction =>
			{
				setupAction.SwaggerEndpoint("/swagger/DocumentOpenApiSpecification/swagger.json", "Document API");
				//setupAction.RoutePrefix = "";
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
