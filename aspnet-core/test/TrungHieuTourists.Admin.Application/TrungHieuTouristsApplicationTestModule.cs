using Volo.Abp.Modularity;

namespace TrungHieuTourists.Admin;

[DependsOn(
    typeof(TrungHieuTouristsAdminApplicationModule),
    typeof(TrungHieuTouristsDomainTestModule)
    )]
public class TrungHieuTouristsApplicationTestModule : AbpModule
{

}
