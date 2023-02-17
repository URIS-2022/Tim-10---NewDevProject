using DocumentAPI.Data;
using DocumentAPI.Entities;
using DocumentAPI.Helpers;
using DocumentAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IStatusOfDocumentRepository, StatusOfDocumentRepository>();
builder.Services.AddScoped<ITypeOfDocumentRepository, TypeOfDocumentRepository>();
builder.Services.AddScoped<IUserRepository, UserMockRepository>();
builder.Services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
builder.Services.AddScoped<ILoggerService, LoggerService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
builder.Services.AddDbContext<DocumentContext>();
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
