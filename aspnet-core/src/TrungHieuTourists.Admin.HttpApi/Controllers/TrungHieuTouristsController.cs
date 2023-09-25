using TrungHieuTourists.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace TrungHieuTourists.Admin.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class TrungHieuTouristsController : AbpControllerBase
{
    protected TrungHieuTouristsController()
    {
        LocalizationResource = typeof(TrungHieuTouristsResource);
    }
}
