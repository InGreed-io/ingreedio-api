using InGreedIoApi.Configurations;
using InGreedIoApi.Data;
using InGreedIoApi.Data.Configuration;
using InGreedIoApi.Data.Repository;
using InGreedIoApi.Data.Repository.Interface;
using InGreedIoApi.Data.Seed;
using InGreedIoApi.POCO;
using InGreedIoApi.Services;
using InGreedIoApi.Utils.Pagination;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conn = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        var frontendAppUrl = builder.Configuration
            .GetValue<string>("FrontendAppUrl");

        if (string.IsNullOrEmpty(frontendAppUrl)) return;

        policyBuilder.WithOrigins(frontendAppUrl)
            .AllowCredentials()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers()
  .AddJsonOptions(opts =>
    {
        var enumConverter = new JsonStringEnumConverter();
        opts.JsonSerializerOptions.Converters.Add(enumConverter);
    });

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddTransient<IIngredientRepository, IngredientRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IReviewRepository, ReviewRepository>();
builder.Services.AddTransient<IPreferenceRepository, PreferenceRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddSingleton<ISeeder<CategoryPOCO>, CategorySeeder>();
builder.Services.AddSingleton<ISeeder<ProductPOCO>, ProductSeeder>();
builder.Services.AddSingleton<ISeeder<IngredientPOCO>, IngredientSeeder>();
builder.Services.AddScoped<IUserSeeder, ApiUserSeeder>();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IEntityTypeConfiguration<ProductPOCO>, ProductConfiguration>();
builder.Services.AddSingleton<IEntityTypeConfiguration<ReviewPOCO>, ReviewConfiguration>();
builder.Services.AddSingleton<IEntityTypeConfiguration<CategoryPOCO>, CategoryConfiguration>();
builder.Services.AddSingleton<IEntityTypeConfiguration<ApiUserPOCO>, ApiUserConfiguration>();
builder.Services.AddSingleton<IEntityTypeConfiguration<IngredientPOCO>, IngredientConfiguration>();
builder.Services.AddSingleton<IEntityTypeConfiguration<CompanyInfoPOCO>, CompanyInfoConfiguration>();
builder.Services.AddSingleton<IEntityTypeConfiguration<FeaturingPOCO>, FeaturingConfiguration>();
builder.Services.AddSingleton<IEntityTypeConfiguration<OperationTypePOCO>, OperationTypeConfiguration>();
builder.Services.AddSingleton<IEntityTypeConfiguration<OperationLogPOCO>, OperationLogConfiguration>();
builder.Services.AddSingleton<IEntityTypeConfiguration<AppNotificationPOCO>, AppNotificationConfiguration>();
builder.Services.AddSingleton<IEntityTypeConfiguration<PreferencePOCO>, PreferenceConfiguration>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseNpgsql(conn));

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(jwt =>
    {
        var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfig:Secret").Value);
        jwt.SaveToken = true;
        jwt.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false, // temporary, TODO: change on first prod deploy
            ValidateAudience = false, // similar ^
            RequireExpirationTime = false, // TODO: potentially can have refresh tokens
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddIdentity<ApiUserPOCO, IdentityRole>(options =>
    {
    }).AddEntityFrameworkStores<ApiDbContext>()
    .AddDefaultTokenProviders();

/*
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
    */

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddPagination(builder.Configuration);

var app = builder.Build();

// Run migrations
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApiDbContext>();
    context.Database.Migrate();

    var seeder = scope.ServiceProvider.GetRequiredService<IUserSeeder>();
    seeder.SeedAsync().Wait();
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();