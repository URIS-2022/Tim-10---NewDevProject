using Contract;
using Contract.Data;
using Contract.Entities;
using Contract.Helpers;
using Contract.ServiceCalls;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<IContractRepository, ContractRepository>();
builder.Services.AddTransient<ITypeOfGuaranteeRepository, TypeOfGuaranteeRepository>();
builder.Services.AddSingleton<IUserRepository, UserMock>();
builder.Services.AddScoped<IDocumentService, DocumentService>();
builder.Services.AddScoped<IBuyerService, BuyerService>();
builder.Services.AddScoped<IPublicBiddingService, PublicBiddingService>();
builder.Services.AddScoped<IGateway, Gateway>();
builder.Services.AddScoped<ILoggerService, LoggerService>();
builder.Services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<Context>();
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
