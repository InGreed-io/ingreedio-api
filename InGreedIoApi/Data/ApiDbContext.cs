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
        SeedIngredients(modelBuilder);
        SeedProducts(modelBuilder);
        AssociateProductsAndIngredients(modelBuilder);
    }

    private void AssociateProductsAndIngredients(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductPOCO>()
            .HasMany(p => p.Ingredients)
            .WithMany(i => i.Products)
            .UsingEntity(j => j.HasData(
                 new { ProductsId = 1, IngredientsId = 3 },  // Cow Milk with Cocoa
                    new { ProductsId = 2, IngredientsId = 15 }, // Almond Milk with Almond
                    new { ProductsId = 3, IngredientsId = 14 }, // Oat Milk with Oat and Turmeric
                    new { ProductsId = 3, IngredientsId = 4 },
                    new { ProductsId = 6, IngredientsId = 19 },
                    new { ProductsId = 6, IngredientsId = 6 }, // Soy Milk with Soy and Spirulina
                    new { ProductsId = 5, IngredientsId = 18 }, // Coconut Milk with Coconut
                    new { ProductsId = 7, IngredientsId = 17 }, // Cashew Milk with Cashew
                    new { ProductsId = 8, IngredientsId = 21 }, // Rice Milk with Rice
                    new { ProductsId = 10, IngredientsId = 3 }, // Chocolate Milk with Cocoa
                    new { ProductsId = 11, IngredientsId = 16 },
                    new { ProductsId = 11, IngredientsId = 1 }, // Strawberry Milk with Strawberry and Cinnamon
                    new { ProductsId = 12, IngredientsId = 20 }  // Vanilla Flavored Milk with Vanilla
                ));
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

    private void SeedIngredients(ModelBuilder modelBuilder)
    {
        var ingredients = new IngredientSeeder().Seed;
        modelBuilder.Entity<IngredientPOCO>().HasData(ingredients);
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