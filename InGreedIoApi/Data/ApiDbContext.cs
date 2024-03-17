using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InGreedIoApi.Model;
using InGreedIoApi.POCO;

namespace InGreedIoApi.Data;

public class ApiDbContext : IdentityDbContext<ApiUser>
{
    private readonly IEntityTypeConfiguration<ProductPOCO> _productConfiguration;
    private readonly IEntityTypeConfiguration<IngredientPOCO> _ingredientConfiguration;
    private readonly IEntityTypeConfiguration<ReviewPOCO> _reviewConfiguration;
    private readonly IEntityTypeConfiguration<FeaturingPOCO> _featuringConfiguration;
    private readonly IEntityTypeConfiguration<CategoryPOCO> _categoryConfiguration;
    private readonly IEntityTypeConfiguration<CompanyInfoPOCO> _companyInfoConfiguration;
    private readonly IEntityTypeConfiguration<ApiUser> _apiUserConfiguration;

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
        IEntityTypeConfiguration<ApiUser> apiUserConfiguration) : base(options)
    {
        _productConfiguration = productConfiguration;
        _ingredientConfiguration = ingredientConfiguration;
        _reviewConfiguration = reviewConfiguration;
        _featuringConfiguration = featuringConfiguration;
        _categoryConfiguration = categoryConfiguration;
        _companyInfoConfiguration = companyInfoConfiguration;
        _apiUserConfiguration = apiUserConfiguration;
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
    }

    public DbSet<ProductPOCO> Products { get; set; }

    public DbSet<CategoryPOCO> Category { get; set; }

    public DbSet<FeaturingPOCO> Features { get; set; }

    public DbSet<IngredientPOCO> Ingredients { get; set; }

    public DbSet<ReviewPOCO> Reviews { get; set; }
    public DbSet<ApiUser> ApiUsers { get; set; }

    public DbSet<CompanyInfoPOCO> CompanyInfo { get; set; }
}