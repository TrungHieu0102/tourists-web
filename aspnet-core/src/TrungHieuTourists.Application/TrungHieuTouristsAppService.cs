using System;
using System.Collections.Generic;
using System.Text;
using TrungHieuTourists.Localization;
using Volo.Abp.Application.Services;

namespace TrungHieuTourists;

/* Inherit your application services from this class.
 */
public abstract class TrungHieuTouristsAppService : ApplicationService
{
    protected TrungHieuTouristsAppService()
    {
        LocalizationResource = typeof(TrungHieuTouristsResource);
    }
}
