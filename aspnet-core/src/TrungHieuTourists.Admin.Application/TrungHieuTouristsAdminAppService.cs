using System;
using System.Collections.Generic;
using System.Text;
using TrungHieuTourists.Localization;
using Volo.Abp.Application.Services;

namespace TrungHieuTourists.Admin;

/* Inherit your application services from this class.
 */
public abstract class TrungHieuTouristsAdminAppService : ApplicationService
{
    protected TrungHieuTouristsAdminAppService()
    {
        LocalizationResource = typeof(TrungHieuTouristsResource);
    }
}
