using InGreedIoApi.Data.Configuration;
using InGreedIoApi.Data.Seed;
using InGreedIoApi.POCO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
        _productConfiguration = new ProductConfiguration();
        _ingredientConfiguration = new IngredientConfiguration();
        _reviewConfiguration = new ReviewConfiguration();
        _featuringConfiguration = new FeaturingConfiguration();
        _categoryConfiguration = new CategoryConfiguration();
        _companyInfoConfiguration = new CompanyInfoConfiguration();
        _apiUserConfiguration = new ApiUserConfiguration();
        _operationLogConfiguration = new OperationLogConfiguration();
        _operationTypeConfiguration = new OperationTypeConfiguration();
        _appNotificationConfiguration = new AppNotificationConfiguration();
        _preferenceConfiguration = new PreferenceConfiguration();
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
        // Seed data
        SeedCategory(modelBuilder);
        SeedProducts(modelBuilder);
    }

    private void SeedCategory(ModelBuilder modelBuilder)
    {
        var products = new CategorySeeder().Seed;
        modelBuilder.Entity<CategoryPOCO>().HasData(products);
    }

    private void SeedProducts(ModelBuilder modelBuilder)
    {
        var products = new ProductSeeder().Seed;
        modelBuilder.Entity<ProductPOCO>().HasData(products);
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