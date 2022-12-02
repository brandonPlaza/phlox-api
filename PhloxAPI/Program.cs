using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PhloxAPI.Data;
using PhloxAPI.Services.AccountsService;
using PhloxAPI.Services.AccountsService.JwtProvider;
using PhloxAPI.Services.AdministrationService;
using PhloxAPI.Services.ReportService;
using PhloxAPI.Services.RoutingService;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

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

// Add Reports service to builder so it can be dependency injected 
builder.Services.AddScoped<IRoutingService, RoutingService>();

builder.Services.AddScoped<IJwtProvider, JwtProvider>();


// Enable authorization
builder.Services.AddAuthorization();

// Add JwtBearer authentication
builder.Services.AddAuthentication(options => {

    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme= JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(o => {
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

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

app.UseAuthentication();

app.MapControllers();

app.Run();
