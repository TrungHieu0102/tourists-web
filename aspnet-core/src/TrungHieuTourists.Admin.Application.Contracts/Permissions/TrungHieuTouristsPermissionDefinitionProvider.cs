using TrungHieuTourists.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace TrungHieuTourists.Admin.Permissions;

public class TrungHieuTouristsPermissionsDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        //Catalog
        var catalogGroup = context.AddGroup(TrungHieuTouristsPermissions.CatalogGroupName, L("Permission:Catalog"));

      

        //Add attribute
        var attributePermission = catalogGroup.AddPermission(TrungHieuTouristsPermissions.Attribute.Default, L("Permission:Catalog.Attribute"));
        attributePermission.AddChild(TrungHieuTouristsPermissions.Attribute.Create, L("Permission:Catalog.Attribute.Create"));
        attributePermission.AddChild(TrungHieuTouristsPermissions.Attribute.Update, L("Permission:Catalog.Attribute.Update"));
        attributePermission.AddChild(TrungHieuTouristsPermissions.Attribute.Delete, L("Permission:Catalog.Attribute.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TrungHieuTouristsResource>(name);
    }
}
