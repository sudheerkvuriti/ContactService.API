using LeadService.Application.Interfaces.Repositories;
using LeadService.Application.Interfaces.Services;
using LeadService.Infrastructure.Persistence;
using LeadService.Infrastructure.Persistence.Repositories;
using LeadService.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// -------------------------
// 🟦 1. Add Database
// -------------------------
builder.Services.AddDbContext<LeadDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// -------------------------
// 🟦 2. Add Repositories & Services
// -------------------------
builder.Services.AddScoped<ILeadRepository, LeadRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<ILeadCommDetailRepository, LeadCommDetailRepository>();
builder.Services.AddScoped<ILeadService, LeadServiceImplementation>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();


// -------------------------
// 🟦 3. Add Token Service BEFORE build
// -------------------------
builder.Services.AddScoped<JwtTokenService>();

// -------------------------
// 🟦 4. Add Controllers / Swagger
// -------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// -------------------------
// 🟦 5. Add Serilog BEFORE build
// -------------------------
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

// -------------------------
// 🟦 6. JWT Authentication
// -------------------------
var jwtConfig = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtConfig["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtConfig["Issuer"],
        ValidAudience = jwtConfig["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

// -------------------------
// 🟦 7. Authorization Policy
// -------------------------
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));
});

// -------------------------
// 🟦 8. CORS
// -------------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularClient",
        policy => policy
            .WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials());
});

// -------------------------
// 🟧 Build App
// -------------------------
var app = builder.Build();

// -------------------------
// 🟦 9. Middlewares
// -------------------------
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularClient");

// Custom Middlewares
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<RequestLoggingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
