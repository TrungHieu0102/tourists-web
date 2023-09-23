using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace TrungHieuTourists.Data;

/* This is used if database provider does't define
 * ITrungHieuTouristsDbSchemaMigrator implementation.
 */
public class NullTrungHieuTouristsDbSchemaMigrator : ITrungHieuTouristsDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
