using payment.Data;
using payment.Helpers;
using payment.ServiceCalls;
using AutoMapper;
using payment.Entities;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IExchangeRateRepository, ExchangeRateRepository>();
builder.Services.AddTransient<IPaymentRepository, PaymentRepository>();
builder.Services.AddSingleton<IUserRepository, UserMockRepository>();
builder.Services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
builder.Services.AddScoped<ILoggerService, LoggerService>();
builder.Services.AddScoped<IPublicBiddingService, PublicBiddingService>();
builder.Services.AddScoped<IBuyerService, BuyerService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<PaymentContext>();
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









