using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TrungHieuTourists.Data;
using Volo.Abp.DependencyInjection;

namespace TrungHieuTourists.EntityFrameworkCore;

public class EntityFrameworkCoreTrungHieuTouristsDbSchemaMigrator
    : ITrungHieuTouristsDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreTrungHieuTouristsDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the TrungHieuTouristsDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<TrungHieuTouristsDbContext>()
            .Database
            .MigrateAsync();
    }
}
