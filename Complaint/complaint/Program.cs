using complaint.Data;
using complaint.Helpers;
using complaint.ServiceCalls;
using complaint.Entities;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddScoped<IComplaintTypeRepository, ComplaintTypeRepository>();
builder.Services.AddScoped<IComplaintStatusRepository, ComplaintStatusRepository>();
builder.Services.AddScoped<IActionRepository, ActionRepository>();
builder.Services.AddScoped<IComplaintRepository, ComplaintRepository>();
builder.Services.AddScoped<IAuthenticationHelper, AuthenticationHelper>();
builder.Services.AddScoped<IUserRepository, UserMockRepository>();
builder.Services.AddScoped<IBuyerService, BuyerService>();
builder.Services.AddScoped<ILoggerService, LoggerService>();
builder.Services.AddDbContext<ComplaintContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());





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

