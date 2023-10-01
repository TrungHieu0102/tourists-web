using TrungHieuTourists.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace TrungHieuTourists.Admin.Permissions;

public class TrungHieuTouristsPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TrungHieuTouristsPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(TrungHieuTouristsPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TrungHieuTouristsResource>(name);
    }
}
