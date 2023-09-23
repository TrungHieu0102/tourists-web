using TrungHieuTourists.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Modularity;

namespace TrungHieuTourists.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(TrungHieuTouristsEntityFrameworkCoreModule),
    typeof(TrungHieuTouristsApplicationContractsModule)
    )]
public class TrungHieuTouristsDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "TrungHieuTourists:"; });
    }
}
