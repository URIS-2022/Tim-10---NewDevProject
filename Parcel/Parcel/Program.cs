using Parcel.Entities;
using Parcel.Data;
using Parcel.Helper;
using Parcel.ServiceCalls;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ICadastralMunicipalityRepository, CadastralMunicipalityRepository>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<ICultureRepository, CultureRepository>();
builder.Services.AddScoped<IDrainageRepository, DrainageRepository>();
builder.Services.AddScoped<IFormOfPropertyRepository, FormOfPropertyRepository>();
builder.Services.AddScoped<IParcelRepository, ParcelRepository>();
builder.Services.AddScoped<IProtectedZoneRepository, ProtectedZoneRepository>();
builder.Services.AddScoped<IWorkabilityRepository, WorkabilityRepository>();
builder.Services.AddSingleton<IUserRepository, UserMockRepository>();
builder.Services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
builder.Services.AddScoped<IBuyerService, BuyerService>();
builder.Services.AddScoped<ILoggerService, LoggerService>();
builder.Services.AddScoped<IGateway, Gateway>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ParcelContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

