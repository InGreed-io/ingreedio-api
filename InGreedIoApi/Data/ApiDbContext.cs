using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InGreedIoApi.Model;
using InGreedIoApi.POCO;

namespace InGreedIoApi.Data;

public class ApiDbContext : IdentityDbContext<ApiUserPOCO>
{
    private readonly IEntityTypeConfiguration<ProductPOCO> _productConfiguration;
    private readonly IEntityTypeConfiguration<IngredientPOCO> _ingredientConfiguration;
    private readonly IEntityTypeConfiguration<ReviewPOCO> _reviewConfiguration;
    private readonly IEntityTypeConfiguration<FeaturingPOCO> _featuringConfiguration;
    private readonly IEntityTypeConfiguration<CategoryPOCO> _categoryConfiguration;
    private readonly IEntityTypeConfiguration<CompanyInfoPOCO> _companyInfoConfiguration;
    private readonly IEntityTypeConfiguration<OperationLogPOCO> _operationLogConfiguration;
    private readonly IEntityTypeConfiguration<OperationTypePOCO> _operationTypeConfiguration;
    private readonly IEntityTypeConfiguration<AppNotificationPOCO> _appNotificationConfiguration;
    private readonly IEntityTypeConfiguration<ApiUserPOCO> _apiUserConfiguration;
    private readonly IEntityTypeConfiguration<PreferencePOCO> _preferenceConfiguration;

    public ApiDbContext()
    {
    }

    public ApiDbContext(DbContextOptions<ApiDbContext> options,
        IEntityTypeConfiguration<ProductPOCO> productConfiguration,
        IEntityTypeConfiguration<IngredientPOCO> ingredientConfiguration,
        IEntityTypeConfiguration<ReviewPOCO> reviewConfiguration,
        IEntityTypeConfiguration<FeaturingPOCO> featuringConfiguration,
        IEntityTypeConfiguration<CategoryPOCO> categoryConfiguration,
        IEntityTypeConfiguration<CompanyInfoPOCO> companyInfoConfiguration,
        IEntityTypeConfiguration<PreferencePOCO> preferenceConfiguration,
        IEntityTypeConfiguration<ApiUserPOCO> apiUserConfiguration,
        IEntityTypeConfiguration<OperationLogPOCO> operationLogConfiguration,
        IEntityTypeConfiguration<OperationTypePOCO> operationTypeConfiguration,
        IEntityTypeConfiguration<AppNotificationPOCO> appNotificationConfiguration) : base(options)
    {
        _productConfiguration = productConfiguration;
        _ingredientConfiguration = ingredientConfiguration;
        _reviewConfiguration = reviewConfiguration;
        _featuringConfiguration = featuringConfiguration;
        _categoryConfiguration = categoryConfiguration;
        _companyInfoConfiguration = companyInfoConfiguration;
        _apiUserConfiguration = apiUserConfiguration;
        _operationLogConfiguration = operationLogConfiguration;
        _operationTypeConfiguration = operationTypeConfiguration;
        _appNotificationConfiguration = appNotificationConfiguration;
        _preferenceConfiguration = preferenceConfiguration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(_productConfiguration);
        modelBuilder.ApplyConfiguration(_reviewConfiguration);
        modelBuilder.ApplyConfiguration(_categoryConfiguration);
        modelBuilder.ApplyConfiguration(_featuringConfiguration);
        modelBuilder.ApplyConfiguration(_ingredientConfiguration);
        modelBuilder.ApplyConfiguration(_companyInfoConfiguration);
        modelBuilder.ApplyConfiguration(_apiUserConfiguration);
        modelBuilder.ApplyConfiguration(_operationLogConfiguration);
        modelBuilder.ApplyConfiguration(_operationTypeConfiguration);
        modelBuilder.ApplyConfiguration(_appNotificationConfiguration);
        modelBuilder.ApplyConfiguration(_preferenceConfiguration);
    }

    public DbSet<ProductPOCO> Products { get; set; }

    public DbSet<CategoryPOCO> Category { get; set; }

    public DbSet<FeaturingPOCO> Features { get; set; }

    public DbSet<IngredientPOCO> Ingredients { get; set; }

    public DbSet<ReviewPOCO> Reviews { get; set; }
    public DbSet<ApiUserPOCO> ApiUsers { get; set; }

    public DbSet<CompanyInfoPOCO> CompanyInfo { get; set; }

    public DbSet<AppNotificationPOCO> AppNotifications { get; set; }
    public DbSet<OperationLogPOCO> OperationLog { get; set; }
    public DbSet<OperationTypePOCO> OperationTypes { get; set; }

    public DbSet<PreferencePOCO> Preferences { get; set; }
}