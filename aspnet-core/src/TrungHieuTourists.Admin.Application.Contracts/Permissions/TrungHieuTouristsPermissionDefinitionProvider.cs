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

        //Add product
        var productPermission = catalogGroup.AddPermission(TrungHieuTouristsPermissions.Tour.Default, L("Permission:Catalog.Tour"));
        productPermission.AddChild(TrungHieuTouristsPermissions.Tour.Create, L("Permission:Catalog.Tour.Create"));
        productPermission.AddChild(TrungHieuTouristsPermissions.Tour.Update, L("Permission:Catalog.Tour.Update"));
        productPermission.AddChild(TrungHieuTouristsPermissions.Tour.Delete, L("Permission:Catalog.Tour.Delete"));
        productPermission.AddChild(TrungHieuTouristsPermissions.Tour.AttributeManage, L("Permission:Catalog.Tour.AttributeManage"));

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
