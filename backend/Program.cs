using BlogApp.Dbs;
using BlogApp.Interfaces;
using BlogApp.Middlewares;
using BlogApp.Repositories;
using BlogApp.Services;
using BlogApp.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecrectKey"])
            ),
            NameClaimType = "Username",
            RoleClaimType = "Role",
            ClockSkew = TimeSpan.FromSeconds(5) // Set clock skew to zero to prevent token expiration issues
        };
    });

//DbContexts
builder.Services.AddDbContext<BlogAppContext>(options =>{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAuthenService, AuthenService>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IBlogValidator, BlogValidator>();
builder.Services.AddScoped<IAuthenValidator, AuthenValidator>();
builder.Services.AddSingleton<BlogApp.Handlers.TokenHandler>();
builder.Services.AddSingleton<BlogApp.Handlers.FileHandler>();
builder.Services.AddSingleton<BlogApp.Handlers.EmailHandler>();

builder.Services.AddCors(option =>
{
    option.AddPolicy(name: "AllowSpecificOrigin", 
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});


builder.Services.AddOpenApiDocument();

Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .CreateLogger();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["RedisCacheSettings:ConnectionString"];
    options.InstanceName = "BlogApp_";
});

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapGet("/TestAuth", () => "Auth Successful!").RequireAuthorization();

    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
}

app.UseCors("AllowSpecificOrigin");

app.UseSerilogRequestLogging();

app.UseMiddleware<ExceptionMiddleware>();

// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
