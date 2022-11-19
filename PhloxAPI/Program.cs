using PhloxAPI.Data;
using PhloxAPI.Services.AccountsService;
using PhloxAPI.Services.AdministrationService;
using PhloxAPI.Services.ReportService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add administration service to the builder so it can be dependency injected
builder.Services.AddScoped<IAdministrationService, AdministrationService>();

// Add Accounts service to builder so it can be dependency injected 
builder.Services.AddScoped<IAccountsService, AccountsService>();

// Add Reports service to builder so it can be dependency injected 
builder.Services.AddScoped<IReportService, ReportService>();

//Register Db Context with the builder
builder.Services.AddDbContext<PhloxDbContext>();

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
