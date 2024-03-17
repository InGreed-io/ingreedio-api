using System.Text;
using InGreedIoApi.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using InGreedIoApi.Data;
using InGreedIoApi.Model;
using InGreedIoApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using InGreedIoApi.Data.Configuration;
using InGreedIoApi.POCO;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddSingleton<IEntityTypeConfiguration<ProductPOCO>, ProductConfiguration>();
builder.Services.AddSingleton<IEntityTypeConfiguration<ReviewPOCO>, ReviewConfiguration>();
builder.Services.AddSingleton<IEntityTypeConfiguration<CategoryPOCO>, CategoryConfiguration>();
builder.Services.AddSingleton<IEntityTypeConfiguration<ApiUser>, ApiUserConfiguration>();
builder.Services.AddSingleton<IEntityTypeConfiguration<IngredientPOCO>, IngredientConfiguration>();
builder.Services.AddSingleton<IEntityTypeConfiguration<CompanyInfoPOCO>, CompanyInfoConfiguration>();
builder.Services.AddSingleton<IEntityTypeConfiguration<FeaturingPOCO>, FeaturingConfiguration>();
builder.Services.AddSingleton<IEntityTypeConfiguration<OperationTypePOCO>, OperationTypeConfiguration>();
builder.Services.AddSingleton<IEntityTypeConfiguration<OperationLogPOCO>, OperationLogConfiguration>();
builder.Services.AddSingleton<IEntityTypeConfiguration<AppNotificationPOCO>, AppNotificationConfiguration>();
builder.Services.AddControllers();

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

builder.Services.AddIdentity<ApiUser, IdentityRole>(options =>
    {
    }).AddEntityFrameworkStores<ApiDbContext>()
    .AddDefaultTokenProviders();

/*
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
    */

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Run migrations
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApiDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();