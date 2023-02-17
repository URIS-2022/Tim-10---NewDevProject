using PublicBidding;
using PublicBidding.Data;
using PublicBidding.Entities;
using PublicBidding.Helpers;
using PublicBidding.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<ITypeOfPublicBiddingRepository, TypeOfPublicBiddingRepository>();
builder.Services.AddScoped<IStatusOfPublicBiddingRepository, StatusOfPublicBiddingRepository>();
builder.Services.AddScoped<IPublicBiddingRepository, PublicBiddingRepository>();
builder.Services.AddScoped<ILicitationRepository, LicitationRepository>();
builder.Services.AddScoped<IUserRepository, UserMockRepository>();
builder.Services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
builder.Services.AddScoped<ILoggerService, LoggerService>();
builder.Services.AddScoped<IBuyerService, BuyerService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
builder.Services.AddDbContext<PublicBiddingContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
