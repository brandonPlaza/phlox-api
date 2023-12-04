using PhloxAPI.Data;
using PhloxAPI.Services.AccountsService;
using PhloxAPI.Services.AdministrationService;
using PhloxAPI.Services.ReportService;
using PhloxAPI.Services.RoutingService;
using PhloxAPI.Services.HelpRequestService;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using PhloxAPI.Services.VoiceCommandsService;




var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
    policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed((host) => true);
    });
});

builder.Services.AddSignalR();

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

// Add Voice Commands service to builder so it can be dependency injected 
builder.Services.AddScoped<IVoiceCommandsService, VoiceCommandsService>();

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

var webSocketOptions = new WebSocketOptions
{
    //KeepAliveInterval = TimeSpan.FromMinutes(2) //default
};
/*webSocketOptions.AllowedOrigins.Add("https://client.com");
webSocketOptions.AllowedOrigins.Add("https://www.client.com");*/

app.UseHttpsRedirection();

app.UseWebSockets();

app.UseCors("MyPolicy");

app.MapHub<HelpRequestHub>("/hubs/notifications");

app.Use(async (context, next) =>
{
    var hubContext = context.RequestServices
                            .GetRequiredService<IHubContext<HelpRequestHub>>();
    //...

    if (next != null)
    {
        await next.Invoke();
    }
});

app.UseAuthorization();

app.MapControllers();

app.Run();
