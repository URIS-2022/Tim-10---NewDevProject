using Buyer;
using Buyer.Data;
using Buyer.Entities;
using Buyer.Helpers;
using Buyer.ServiceCalls;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPriorityRepository, PriorityRepository>();
builder.Services.AddScoped<IContactPersonRepository, ContactPerosnRepository>();
builder.Services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
builder.Services.AddScoped<IUserRepository, UserMockRepository>();
builder.Services.AddScoped<IIndividialRepository, IndividualRepository>();
builder.Services.AddScoped<ILegalEntityRepository, LegalEntityRepository>();
builder.Services.AddScoped<IBuyerRepository, BuyerRepository>();
builder.Services.AddScoped<ILoggerService, LoggerService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IAuthorizedPersonService, AuthorizedPersonService>();
builder.Services.AddScoped<IPublicBiddingService, PublicBiddingService>();
builder.Services.AddScoped<IGateway, GatewayService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers();

builder.Services.AddDbContext<BuyerContext>();
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