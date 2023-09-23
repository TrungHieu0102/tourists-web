using Volo.Abp.Modularity;

namespace TrungHieuTourists;

[DependsOn(
    typeof(TrungHieuTouristsApplicationModule),
    typeof(TrungHieuTouristsDomainTestModule)
    )]
public class TrungHieuTouristsApplicationTestModule : AbpModule
{

}
