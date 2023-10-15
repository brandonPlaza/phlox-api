using PhloxAPI.Data;
using PhloxAPI.Services.AccountsService;
using PhloxAPI.Services.AdministrationService;
using PhloxAPI.Services.ReportService;
using PhloxAPI.Services.RoutingService;
using PhloxAPI.Services.HelpRequestService;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;




var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
    policy =>
    {
        policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
    });
});

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add administration service to the builder so it can be dependency injected
builder.Services.AddScoped<IAdministrationService, AdministrationService>();

// Add Accounts service to builder so it can be dependency injected 
builder.Services.AddScoped<IAccountsService, AccountsService>();

// Add Reports service to builder so it can be dependency injected 
builder.Services.AddScoped<IReportService, ReportService>();

// Add Routing service to builder so it can be dependency injected 
builder.Services.AddScoped<IRoutingService, RoutingService>();

// Add Help Request service to builder so it can be dependency injected 
builder.Services.AddScoped<IHelpRequestService, HelpRequestService>();

var connection = String.Empty;

builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");



builder.Services.AddDbContext<PhloxDbContext>(options =>
    options.UseSqlServer(connection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
