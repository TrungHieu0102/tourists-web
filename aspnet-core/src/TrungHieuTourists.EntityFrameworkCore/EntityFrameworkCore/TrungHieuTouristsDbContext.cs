using Microsoft.EntityFrameworkCore;
using TrungHieuTourists.Configurations.TourCategories;
using TrungHieuTourists.Configurations.Tours;
using TrungHieuTourists.Countries;
using TrungHieuTourists.IdentitySettings;
using TrungHieuTourists.Oders;
using TrungHieuTourists.Orders;
using TrungHieuTourists.Promotions;
using TrungHieuTourists.Slots;
using TrungHieuTourists.SlotTicketItems;
using TrungHieuTourists.SlotTickets;
using TrungHieuTourists.TourAttributes;
using TrungHieuTourists.TourCategoris;
using TrungHieuTourists.Tours;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace TrungHieuTourists.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class TrungHieuTouristsDbContext :
    AbpDbContext<TrungHieuTouristsDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    //Tourist App
    public DbSet<Country> Contries { get; set; }
    public DbSet<Order> Orders { get; set; }    
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<OrderTransaction> OrderTransactions { get; set; }
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<PromotionCategory> PromotionCategories { get; set; }
    public DbSet<PromotionCountry> PromotionCountries { get; set; }
    public DbSet<PromotionTour> PromotionTours { get; set; }
    public DbSet<PromotionUsageHistory> PromotionUsageHistories { get; set; }
    public DbSet<Slot> Slots { get; set; }
    public DbSet<SlotTicket> SlotTickets { get; set; }
    public DbSet<SlotTicketItem> SlotTicketItems { get; set; }
    public DbSet<TourAttribute> TourAttributes { get; set; }
    public DbSet<TourCategory>TourCategoris { get; set; }
    public DbSet<Tour> Tours { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<TourAttributeDateTime> TourAttributeDateTimes { get; set; }
    public DbSet<TourAttributeDecimal> TourAttributeDecimails { get; set; }
    public DbSet<TourAttributeInt> TourAttributeInts { get; set; }
    public DbSet<TourAttributeText> TourAttributeTexts { get; set; }
    public DbSet<TourAttributeVarchar> TourAttributeVarchars { get; set; }
    public DbSet<TourLink> TourLinks { get; set; }  
    public DbSet<TourReview> TourReviews { get; set; }
    public DbSet<TourTag> TourTags { get; set; }
    public DbSet<IdentitySetting> IdentitySettings { get; set; }



    #endregion

    public TrungHieuTouristsDbContext(DbContextOptions<TrungHieuTouristsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */
        builder.ApplyConfiguration(new TourAttributeConfiguration());

        builder.ApplyConfiguration(new SlotConfiguration());

        builder.ApplyConfiguration(new SlotTicketConfiguration());
        builder.ApplyConfiguration(new SlotTicketItemConfiguration());

        builder.ApplyConfiguration(new CountryConfiguration());

        builder.ApplyConfiguration(new OrderConfiguration());
        builder.ApplyConfiguration(new OrderItemConfiguration());
        builder.ApplyConfiguration(new OrderTransactionConfiguration());

        builder.ApplyConfiguration(new TourCategoryConfiguration());

        builder.ApplyConfiguration(new TourConfiguration());
        builder.ApplyConfiguration(new TourLinkConfiguration());
        builder.ApplyConfiguration(new TourReviewConfiguration());
        builder.ApplyConfiguration(new TourTagConfiguration());
        builder.ApplyConfiguration(new TagConfiguration());
        builder.ApplyConfiguration(new TourAttributeDateTimeConfiguration());
        builder.ApplyConfiguration(new TourAttributeDecimalConfiguration());
        builder.ApplyConfiguration(new TourAttributeIntConfiguration());
        builder.ApplyConfiguration(new TourAttributeTextConfiguration());
        builder.ApplyConfiguration(new TourAttributeVarcharConfiguration());

        builder.ApplyConfiguration(new PromotionConfiguration());
        builder.ApplyConfiguration(new PromotionCategoryConfiguration());
        builder.ApplyConfiguration(new PromotionCountryConfiguration());
        builder.ApplyConfiguration(new PromotionTourConfiguration());
        builder.ApplyConfiguration(new PromotionUsageHistoryConfiguration());

        builder.ApplyConfiguration(new IdentitySettingConfiguration());



    }
}
