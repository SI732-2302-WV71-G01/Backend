using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Persistence.Repositories;
using LearningCenter.API.Learning.Services;
using LearningCenter.API.Security.Authorization.Handlers.Implementations;
using LearningCenter.API.Security.Authorization.Handlers.Interfaces;
using LearningCenter.API.Security.Authorization.Middleware;
using LearningCenter.API.Security.Authorization.Settings;
using LearningCenter.API.Security.Domain.Repositories;
using LearningCenter.API.Security.Domain.Services;
using LearningCenter.API.Security.Persistence.Repositories;
using LearningCenter.API.Security.Services;
using LearningCenter.API.Shared.Persistence.Contexts;
using LearningCenter.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add CORS
builder.Services.AddCors();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Add API Documentation Information
    
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Version = "v1",
        Title = "ACME Learning Center API",
        Description = "ACME Learning Center RESTful API",
        TermsOfService = new Uri("https://acme-learning.com/tos"),
        Contact = new OpenApiContact
        {
            Name = "ACME.studio",
            Url = new Uri("https://acme.studio")
        },
        License = new OpenApiLicense
        {
            Name = "ACME Learning Center Resources License",
            Url = new Uri("https://acme-learning.com/license")
        }
    });
    options.EnableAnnotations();
    options.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
            },
            Array.Empty<string>()
        }
    });
});

// Add Database Connection

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySQL(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());

// Add lowercase routes
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Shared Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// AppSettings Configuration
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Dependency Injection Configuration
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddScoped<IStoreImageRepository, StoreImageRepository>();
builder.Services.AddScoped<IStoreImageService, StoreImageService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
builder.Services.AddScoped<IPurchaseService, PurchaseService>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
builder.Services.AddScoped<IArticleService, ArticleService>();

// Security Injection Configuration
builder.Services.AddScoped<IJwtHandler, JwtHandler>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// AutoMapper Configuration
builder.Services.AddAutoMapper(
    typeof(LearningCenter.API.Learning.Mapping.ModelToResourceProfile),
    typeof(LearningCenter.API.Learning.Mapping.ResourceToModelProfile),
    
    typeof(LearningCenter.API.Security.Mapping.ModelToResourceProfile),
    typeof(LearningCenter.API.Security.Mapping.ResourceToModelProfile));


var app = builder.Build();

// Validation for ensuring Database Objects are created
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("v1/swagger.json", "v1");
        options.RoutePrefix = "swagger";
    });
}
// Configure CORS 
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Configure Error Handler Middleware
app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure JWT Handling
app.UseMiddleware<JwtMiddleware>();

//Default
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
public partial class Program {}