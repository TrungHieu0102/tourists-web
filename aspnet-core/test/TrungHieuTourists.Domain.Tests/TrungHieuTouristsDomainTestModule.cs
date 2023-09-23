using TrungHieuTourists.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace TrungHieuTourists;

[DependsOn(
    typeof(TrungHieuTouristsEntityFrameworkCoreTestModule)
    )]
public class TrungHieuTouristsDomainTestModule : AbpModule
{

}
