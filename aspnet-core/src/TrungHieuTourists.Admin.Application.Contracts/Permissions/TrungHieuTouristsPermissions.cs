namespace TrungHieuTourists.Admin.Permissions;

public static class TrungHieuTouristsPermissions
{
    public const string SystemGroupName = "TrungHieuAdminSystem";
    public const string CatalogGroupName = "TrungHieuAdminCatalog";

    //Add your own permission names. Example:
    public static class Role
    {
        public const string Default = SystemGroupName + ".Role";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class User
    {
        public const string Default = SystemGroupName + ".User";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static class Tour
    {
        public const string Default = CatalogGroupName + ".Tour";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
        public const string AttributeManage = Default + ".Attribute";

    }

    public static class Attribute
    {
        public const string Default = CatalogGroupName + ".Attribute";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }
}
