﻿using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using System.ComponentModel.DataAnnotations;
using Xunit;

using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using System.Globalization;

namespace P3AddNewFunctionalityDotNetCore.Tests
{   
    /// <summary>
    /// Contains tests for ProductService
    /// </summary>
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

        /// <summary>
        /// Contains unit tests for ProductViewModel validation.
        /// </summary>
        public class ProductViewModelValidationTest
        {
            // UT_TEST001(SMO) : ProductViewModel objet.
            private ProductViewModel product;

            // UT_TEST001(SMO) : ProductViewModel object instantiation.
            public ProductViewModelValidationTest()
            {
                product = new ProductViewModel();
            }

            /// <summary>
            /// Method for trying to validate the model (object) in parameter.
            /// </summary>
            /// <params>
            /// <paramref name="model"> reference to the object to validate. </paramref>
            /// </params>
            /// <returns>Boolean. True if object validation (required and validation attributes)
            /// is successful.</returns>
            /// <remarks>(DataAnnotations) Method used for tests in ProductViewModelValidationTest Class.</remarks>
            /// <remarks>UT_TEST001(SMO)</remarks>
            private bool ValidateModel(object model)
            {
                // DataAnnotations: Instantiation of a container :
                // Contains the results of a validation request.
                // Null(empty) if validation successfull.
                // Otherwise contains validation error messages list.
                // https://learn.microsoft.com/fr-fr/dotnet/api/system.componentmodel.dataannotations.validationresult?view=net-8.0
                var validationResults = new List<ValidationResult>();
                // DataAnnotations: Instantiation of the context
                // of a performed validation check.
                // https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.validationcontext?view=net-8.0
                var ctx = new ValidationContext(model, null, null);
                // DataAnnotations: method which Determines whether the specified object is valid.
                // Evaluates each Validation Attribute and if Required Attributes are provided.
                // Boolean(true) :  specifies whether to validate all properties.
                // Returns true if validation successful.
                // https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.validator.tryvalidateobject?view=net-8.0
                return Validator.TryValidateObject(model, ctx, validationResults, true);
            }

            /// <summary>
            /// Method which returns the first error message of an attempt to validate the model (object) in parameter. 
            /// </summary>
            /// <params>
            /// <paramref name="model"> reference to the object to validate. </paramref>
            /// </params>
            /// <returns>String. First error message.</returns>
            /// <remarks>(DataAnnotations) Method used for tests in ProductViewModelValidationTest Class.</remarks>
            /// <remarks>UT_TEST001(SMO)</remarks>
            private string GetFirstErrorMessage(object model)
            {
                // DataAnnotations: Instantiation of a container :
                // Contains the results of a validation request.
                // Null(empty) if validation successfull.
                // Otherwise contains validation error messages list.
                // https://learn.microsoft.com/fr-fr/dotnet/api/system.componentmodel.dataannotations.validationresult?view=net-8.0
                var validationResults = new List<ValidationResult>();
                // DataAnnotations: Instantiation of the context
                // of a performed validation check.
                // https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.validationcontext?view=net-8.0
                var ctx = new ValidationContext(model, null, null);
                // DataAnnotations: method which Determines whether the specified object is valid.
                // Evaluates each Validation Attribute and if Required Attributes are provided.
                // Boolean(true) :  specifies whether to validate all properties.
                // Returns true if validation successful.
                // https://learn.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.validator.tryvalidateobject?view=net-8.0
                Validator.TryValidateObject(model, ctx, validationResults, true);
                // Returns the first message of the results of a validation request.(Collection, List<T>).
                return validationResults[0].ErrorMessage;
            }

            /// <summary>
            /// Unit test: product Name can't be empty on product (ProductViewModel class) creation.
            /// </summary>
            /// <remarks>Test if Name field is set as a required field (DataAnnotations).</remarks>
            /// <remarks>UT_TEST001(SMO)</remarks>
            [Fact]
            public void TestProductMissingName()
            {
                // Arrange ==> see public ProductViewModelValidationTest()
                // for instantiation of Product (ProductViewModel class)
                product.Price = "1.0"; // Old: product.Price = "10.0";
                product.Stock = "1";

                // Act ==> fill required fields and set Name to null
                product.Name = null;                

                // Assert
                // Product model validation should failed and returns false.
                Assert.False(ValidateModel(product));
                // Checks if error message resource name corresponds to the one definied in [Required] DataAnnotations.
                Assert.Equal("MissingName", GetFirstErrorMessage(product));

                //Act ==> Single space not allowed.
                product.Name = " ";

                // Assert
                Assert.False(ValidateModel(product));
                Assert.Equal("MissingName", GetFirstErrorMessage(product));
            }

            /// <summary>
            /// Unit test: product Price can't be empty on product (ProductViewModel class) creation.
            /// </summary>
            /// <remarks>Test if Price field is set as a required field (DataAnnotations).</remarks>
            /// <remarks>UT_TEST002(SMO)</remarks>
            [Fact]
            public void TestProductMissingPrice()
            {
                // Arrange ==> see public ProductViewModelValidationTest()
                // for instantiation of Product (ProductViewModel class)
                product.Name = "Unit Test Product : MissingPrice.";
                product.Stock = "1";
                // Act ==> fill required fields and set Price to null
                product.Price = null; 
                
                // Assert
                // Product model validation should failed and returns false.
                Assert.False(ValidateModel(product));
                // Checks if error message resource name corresponds to the one definied in [Required] DataAnnotations.
                Assert.Equal("MissingPrice", GetFirstErrorMessage(product));
            }

            /// <summary>
            /// Unit test: product Stock can't be empty on product (ProductViewModel class) creation.
            /// </summary>
            /// <remarks>Test if Stock field is set as a required field (DataAnnotations).</remarks>
            /// <remarks>UT_TEST003(SMO)</remarks>
            [Fact]
            public void TestProductMissingStock()
            {
                // Arrange ==> see public ProductViewModelValidationTest()
                // for instantiation of Product (ProductViewModel class)
                product.Name = "Unit Test Product : MissingStock.";
                product.Price = "1.0";

                // Act ==> fill required fields and set Stock to null                
                product.Stock = null;

                // Assert
                // Product model validation should failed and returns false.
                Assert.False(ValidateModel(product));
                // Checks if error message resource name corresponds to the one definied in [Required] DataAnnotations.
                Assert.Equal("MissingStock", GetFirstErrorMessage(product));
            }

            /// <summary>
            /// Unit test: product Price allow decimal value with dot as decimal separator 
            /// on product (ProductViewModel class) creation.
            /// </summary>
            /// <remarks>Test if Price DataAnnotations are applied.</remarks>
            /// <remarks>UT_TEST004(SMO)</remarks>
            [Fact]
            public void TestProductPriceNotANumber()
            {
                // Arrange ==> see public ProductViewModelValidationTest()
                // for instantiation of Product (ProductViewModel class)
                product.Name = "Unit Test Product : PriceNotANumber.";
                product.Stock = "1";

                // Act ==> test uncorrect price format.
                product.Price = "A";                

                // Assert
                // Product model validation should failed and returns false.
                Assert.False(ValidateModel(product),"'A' value should be forbiden for price.");
                // Checks if error message resource name corresponds to the one definied in [Required] DataAnnotations.
                Assert.Equal("PriceNotANumber", GetFirstErrorMessage(product));

                // Act ==> additionnals tests of uncorrect price format.                
                product.Price = ".1";
                // Assert
                Assert.False(ValidateModel(product), "'.1' value should be forbiden for price.");
                Assert.Equal("PriceNotANumber", GetFirstErrorMessage(product));

                // Act ==> additionnals tests of uncorrect price format.   
                product.Price = "1,1";
                // Assert
                Assert.False(ValidateModel(product), "'1,1' value should be forbiden for price.");
                Assert.Equal("PriceNotANumber", GetFirstErrorMessage(product));                
            }

            /// <summary>
            /// Unit test: product Price doesn't allow 0 value.
            /// on product (ProductViewModel class) creation.
            /// </summary>
            /// <remarks>Test if Price DataAnnotations are applied.</remarks>
            /// <remarks>UT_TEST005(SMO)</remarks>
            [Fact]
            public void TestProductPriceNotGreaterThanZero()
            {
                // Arrange ==> see public ProductViewModelValidationTest()
                // for instantiation of Product (ProductViewModel class)
                product.Name = "Unit Test Product : PriceNotGreaterThanZero.";
                product.Stock = "1";

                // Act
                product.Price = "0.0";

                // Assert
                // Product model validation should failed and returns false.
                Assert.False(ValidateModel(product));
                // Checks if error message resource name corresponds to the one definied in [Required] DataAnnotations.
                Assert.Equal("PriceNotGreaterThanZero", GetFirstErrorMessage(product));                
            }

            /// <summary>
            /// Unit test: product Stock doesn't allow 0 value.
            /// on product (ProductViewModel class) creation.
            /// </summary>
            /// <remarks>Test if Stock DataAnnotations are applied.</remarks>
            /// <remarks>UT_TEST006(SMO)</remarks>
            [Fact]
            public void TestProductStockNotGreaterThanZero()
            {
                // Arrange ==> see public ProductViewModelValidationTest()
                // for instantiation of Product (ProductViewModel class)
                product.Name = "Unit Test Product : StockNotGreaterThanZero.";
                product.Price = "1.0";

                // Act
                product.Stock = "0";

                // Assert
                // Product model validation should failed and returns false.
                Assert.False(ValidateModel(product));
                // Checks if error message resource name corresponds to the one definied in [Required] DataAnnotations.
                Assert.Equal("StockNotGreaterThanZero", GetFirstErrorMessage(product));
            }

            // Ctrl+M+H (ctrl+M+U to remove) : hide the portion of selected code.
            // additional unit tests not requested by Louis specification. For learning purpose, fluent assertions and test code coverage evaluation.

            /// <summary>
            /// Unit test: product Name length can't be more than 100 caracters.
            /// </summary>
            /// <remarks>Test if StringLength (DataAnnotations) is set for Name field.</remarks>
            /// <remarks>Unit test not required by Louis project specifications.</remarks>
            [Fact]
            public void TestProductNameLength()
            {
                // Not requested by Louis specification. For learning purpose, fluent assertions and test code coverage evaluation.
                // Arrange

                // Act


                // Assert
            }

            /// <summary>
            /// Unit test: product Description length can't be more than 200 caracters.
            /// </summary>
            /// <remarks>Test if StringLength (DataAnnotations) is set for Description field.</remarks>
            /// <remarks>Unit test not required by Louis project specifications.</remarks>
            [Fact]
            public void TestProductDescriptionLength()
            {
                // Not requested by Louis specification. For learning purpose, fluent assertions and test code coverage evaluation.
                // Arrange

                // Act


                // Assert
            }

            /// <summary>
            /// Unit test: product Details length can't be more than 100 caracters.
            /// </summary>
            /// <remarks>Test if StringLength (DataAnnotations) is set for Details field.</remarks>
            /// <remarks>Unit test not required by Louis project specifications.</remarks>
            [Fact]
            public void TestProductDetailsLength()
            {
                // Not requested by Louis specification. For learning purpose, fluent assertions and test code coverage evaluation.
                // Arrange

                // Act


                // Assert
            }

        } // end ProductViewModelValidationTest class

        /// <summary>
        /// Contains integration tests for ProductViewModel.
        /// </summary>
        public class IntegrationTests
        {
            /// <summary>
            /// TEST: Save a product with expected fields required 
            /// This test method doesn't test fields data annotation, only product save.
            /// </summary>
            /// <returns></returns>
            /// <remarks>IT_TEST 001(SMO)</remarks>
            [Fact]
            public void SaveNewProduct()
            {
                // Arrange
                // DB Connection
                // Instanciation Product 

                // Act
                // Product save

                // Assert
                // Check product created

                // Clean UP 
                // Delete created product
            }
        } // end IntegrationTests class

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
         *
        */

    }
}