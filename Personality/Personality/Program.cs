using Personality.Data;
using Personality.Entities;
using Personality.Helper;
using Personality.ServiceCalls;

var builder = WebApplication.CreateBuilder(args);

//add services to the container

builder.Services.AddTransient<IPersonalityRepository, PersonalityRepository>();
builder.Services.AddSingleton<IUserRepository, UserMockRepository>();
builder.Services.AddScoped<ILoggerService, LoggerService>();
builder.Services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<PersonalityContext>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();