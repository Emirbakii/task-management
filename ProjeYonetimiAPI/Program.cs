using Microsoft.EntityFrameworkCore;
using ProjeYonetimiAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// 1. JWT Ayarlarını appsettings.json'dan okuyoruz
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // React'in çalıştığı adres
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
// Servislerimizi sisteme kaydediyoruz (Dependency Injection)
builder.Services.AddScoped<ProjeYonetimiAPI.Services.IDashboardService, ProjeYonetimiAPI.Services.DashboardService>();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2. Güvenlik Görevlisi: Sisteme Bearer Token (JWT) kullanacağını öğretiyoruz
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!))
    };
});

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}
// Global Hata Yönetimi Ara Katmanı
app.UseMiddleware<ProjeYonetimiAPI.Middlewares.ExceptionMiddleware>();


app.UseHttpsRedirection();
app.UseCors("AllowReactApp");

// 3. EN ÖNEMLİ KISIM: Kimlik kontrolü mekanizmalarını devreye sokuyoruz
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();