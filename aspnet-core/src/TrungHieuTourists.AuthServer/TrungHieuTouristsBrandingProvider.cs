using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace TrungHieuTourists;

[Dependency(ReplaceServices = true)]
public class TrungHieuTouristsBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "TrungHieuTourists";
}
