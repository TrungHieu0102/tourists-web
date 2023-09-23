using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TrungHieuTourists.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class TrungHieuTouristsDbContextFactory : IDesignTimeDbContextFactory<TrungHieuTouristsDbContext>
{
    public TrungHieuTouristsDbContext CreateDbContext(string[] args)
    {
        TrungHieuTouristsEfCoreEntityExtensionMappings.Configure();

        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<TrungHieuTouristsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));

        return new TrungHieuTouristsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../TrungHieuTourists.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
