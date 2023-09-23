using System.Threading.Tasks;

namespace TrungHieuTourists.Data;

public interface ITrungHieuTouristsDbSchemaMigrator
{
    Task MigrateAsync();
}
