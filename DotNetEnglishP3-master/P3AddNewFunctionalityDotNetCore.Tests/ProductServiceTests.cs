using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using Xunit;


namespace P3AddNewFunctionalityDotNetCore.Tests
{
    // Arrange.
    // private readonly IConfiguration _configuration;
    // private readonly IStringLocalizer<ProductService> _localizer;

    public class ProductServiceTests
    {
        /// <summary>
        /// Take this test method as a template to write your test method.
        /// A test method must check if a definite method does its job:
        /// returns an expected value from a particular set of parameters
        /// </summary>
        [Fact]
        public void ExampleMethod()
        {
            // Arrange

            // Act


            // Assert
            Assert.Equal(1, 1);
        }

        // TODO write test methods to ensure a correct coverage of all possibilities

        /* Code Help for adding test method
         * 
         * DB Connection :
            IConfigurationRoot configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();
                    var builder = new DbContextOptionsBuilder<AppIdentityDbContext>();
                    var connectionString = configuration.GetConnectionString("P3Identity");
                    builder.UseSqlServer(connectionString);
                    return new AppIdentityDbContext(builder.Options, configuration);


            builder.Services.AddDbContext<P3Referential>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("P3Referential")));

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("P3Identity")));

            builder.Services.AddDefaultIdentity<IdentityUser>()
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddDefaultTokenProviders();

                */

        [Fact]
        public void SaveNewProduct()
        {

        }
    }
}