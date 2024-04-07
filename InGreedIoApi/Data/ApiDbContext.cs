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

    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
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

    public virtual DbSet<ProductPOCO> Products { get; set; }

    public virtual DbSet<CategoryPOCO> Category { get; set; }

    public virtual DbSet<FeaturingPOCO> Features { get; set; }

    public virtual DbSet<IngredientPOCO> Ingredients { get; set; }

    public virtual DbSet<ReviewPOCO> Reviews { get; set; }
    public virtual DbSet<ApiUserPOCO> ApiUsers { get; set; }

    public virtual DbSet<CompanyInfoPOCO> CompanyInfo { get; set; }

    public virtual DbSet<AppNotificationPOCO> AppNotifications { get; set; }
    public virtual DbSet<OperationLogPOCO> OperationLog { get; set; }
    public virtual DbSet<OperationTypePOCO> OperationTypes { get; set; }

    public virtual DbSet<PreferencePOCO> Preferences { get; set; }
}