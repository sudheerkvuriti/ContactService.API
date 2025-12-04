using AutoMapper;
using ContactService.API.Middlewares;
using ContactService.API.Models;
using ContactServices.API.Data;
using ContactServices.API.Mappings;
using ContactServices.API.Repository.Implementations;
using ContactServices.API.Repository.Interfaces;
using ContactServices.API.Validators;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// DbContext
builder.Services.AddDbContext<ContactDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

// AutoMapper (register profiles later)
builder.Services.AddAutoMapper(typeof(Program));

// Add controllers & swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region JWT auth basic setup - reads Jwt:Key, Issuer, Audience from appsettings or env
// load settings
var jwtSection = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<JwtSettings>(jwtSection);
var jwtSettings = jwtSection.Get<JwtSettings>() ?? throw new Exception("Jwt settings missing");

// register authentication
var keyBytes = Encoding.UTF8.GetBytes(jwtSettings.Key);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // set true in prod
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidateAudience = true,
        ValidAudience = jwtSettings.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromSeconds(30)
    };
});

// authorization policies example (scopes/roles)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanCreateContact", policy =>
        policy.RequireClaim("scope", "contacts.create"));

    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));
});
#endregion

#region Middle ware

#endregion

// MassTransit (RabbitMQ) -- minimal registration
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(config["RabbitMq:Host"], "/", h =>
        {
            h.Username(config["RabbitMq:Username"] ?? "guest");
            h.Password(config["RabbitMq:Password"] ?? "guest");
        });
    });
});
#region Repositories
builder.Services.AddScoped<IContactRepository, ContactRepository>();
//builder.Services.AddScoped<IContactService, ContactServices>();
builder.Services.AddAutoMapper(typeof(ContactMappingProfile));
builder.Services.AddValidatorsFromAssemblyContaining<ContactValidator>();
#endregion

var app = builder.Build();
// Add logging middleware
app.UseMiddleware<RequestLoggingMiddleware>();
//app.UseMiddleware<Middleware.ExceptionMiddleware>();   // we'll add this file next
//app.UseMiddleware<Middleware.RequestLoggingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
