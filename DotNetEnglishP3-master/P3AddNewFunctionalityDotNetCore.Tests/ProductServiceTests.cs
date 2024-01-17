using System;
using System.Globalization;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using P3AddNewFunctionalityDotNetCore.Controllers;
using P3AddNewFunctionalityDotNetCore.Data;
using Xunit;
// UPD005 : FluentAssertions tests. 
using FluentAssertions;
using Castle.Components.DictionaryAdapter.Xml;
using Microsoft.AspNetCore.Mvc.Filters;

namespace P3AddNewFunctionalityDotNetCore.Tests
{   
    /// <summary>
    /// Contains tests for ProductService
    /// </summary>
    public class ProductServiceTests
    {
        /* 
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
        */

        // TODO write test methods to ensure a correct coverage of all possibilities

        /// <summary>
        /// Contains unit tests for ProductViewModel.
        /// </summary>
        public class UnitTest
        {
            // UT_TEST001(SMO) : ProductViewModel objet.
            private ProductViewModel product;

            // UT_TEST001(SMO) : ProductViewModel object instantiation (builder).
            public UnitTest()
            {
                // Arrange
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
                product.Price = "1.0"; 
                product.Stock = "1";

                // Act ==> fill required fields and set Name to null
                product.Name = null;                

                // Assert
                // Product model validation should failed and returns false.
                Assert.False(ValidateModel(product));
                // Checks if error message resource name corresponds to the one definied in [Required] DataAnnotations.
                Assert.Equal("Veuillez saisir un nom.", GetFirstErrorMessage(product));
                //Assert.Equal("MissingName", GetFirstErrorMessage(product));

                //Act ==> Single space not allowed.
                product.Name = " ";

                // Assert
                Assert.False(ValidateModel(product));
                Assert.Equal("Veuillez saisir un nom.", GetFirstErrorMessage(product));
                //Assert.Equal("MissingName", GetFirstErrorMessage(product));
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
                Assert.Equal("Veuillez saisir un prix.", GetFirstErrorMessage(product));
                //Assert.Equal("MissingPrice", GetFirstErrorMessage(product));
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
                Assert.Equal("Veuillez saisir un stock.", GetFirstErrorMessage(product));
                //Assert.Equal("MissingStock", GetFirstErrorMessage(product));
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
                Assert.False(ValidateModel(product),"'A' value should be forbiden for Price.");
                // Checks if error message resource name corresponds to the one definied in [Required] DataAnnotations.
                Assert.Equal("La valeur saisie pour le prix doit être un nombre.", GetFirstErrorMessage(product));
                //Assert.Equal("PriceNotANumber", GetFirstErrorMessage(product));

                // Act ==> additionnals tests of uncorrect price format.                
                product.Price = ".1";
                // Assert
                Assert.False(ValidateModel(product), "'.1' value should be forbiden for Price.");
                Assert.Equal("La valeur saisie pour le prix doit être un nombre.", GetFirstErrorMessage(product));
                //Assert.Equal("PriceNotANumber", GetFirstErrorMessage(product));

                // Act ==> additionnals tests of uncorrect price format.   
                product.Price = "1,1";
                // Assert
                Assert.False(ValidateModel(product), "'1,1' value should be forbiden for Price.");
                Assert.Equal("La valeur saisie pour le prix doit être un nombre.", GetFirstErrorMessage(product));
                //Assert.Equal("PriceNotANumber", GetFirstErrorMessage(product));                
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
                Assert.Equal("Le prix doit être supérieur à zéro.", GetFirstErrorMessage(product));
                //Assert.Equal("PriceNotGreaterThanZero", GetFirstErrorMessage(product));                
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
                Assert.Equal("Le stock doit être supérieur à zéro.", GetFirstErrorMessage(product));
                //Assert.Equal("StockNotGreaterThanZero", GetFirstErrorMessage(product));
            }

            /// <summary>
            /// Unit test: product Stock allows only integer value.
            /// on product (ProductViewModel class) creation.
            /// </summary>
            /// <remarks>Test if Stock DataAnnotations are applied.</remarks>
            /// <remarks>UT_TEST007(SMO)</remarks>
            [Fact]
            public void TestProductStockNotAnInteger()
            {
                // Arrange ==> see public ProductViewModelValidationTest()
                // for instantiation of Product (ProductViewModel class)
                product.Name = "Unit Test Product : StockNotAnInteger.";
                product.Price = "1.0";

                // Act
                product.Stock = "A";
                // Assert
                // Product model validation should failed and returns false.
                Assert.False(ValidateModel(product), "'A' value should be forbiden for Stock.");
                // Checks if error message resource name corresponds to the one definied in [Required] DataAnnotations.
                //Assert.Equal("StockNotAnInteger", GetFirstErrorMessage(product));
                Assert.Equal("La valeur saisie pour le stock doit être un entier.", GetFirstErrorMessage(product));

                // Act
                product.Stock = "1.1";
                // Assert                
                Assert.False(ValidateModel(product), "'1.1' value should be forbiden for Stock.");
                Assert.Equal("La valeur saisie pour le stock doit être un entier.", GetFirstErrorMessage(product));
                //Assert.Equal("StockNotAnInteger", GetFirstErrorMessage(product));
            }

            // Ctrl+M+H (ctrl+M+U to remove) : hide the portion of selected code.
            // additional unit tests not requested by Louis specification. For learning purpose, fluent assertions and test code coverage evaluation.

            /// <summary>
            /// Unit test: product Name length can't be more than 100 caracters.
            /// </summary>
            /// <remarks>Test if StringLength (DataAnnotations) is set for Name field.</remarks>
            /// <remarks>Unit test not required by Louis project specifications.</remarks>
            /// <remarks>UT_TEST008(SMO)</remarks>            
            /// <remarks>UPD005(SMO) : FluentAssertions test</remarks>
            [Fact]
            public void TestProductMaxNameLength()
            {
                // *** Not requested by Louis specification ***. For learning purpose, fluent assertions and test code coverage evaluation.
                
                // Arrange ==> see public ProductViewModelValidationTest()
                // for instantiation of Product (ProductViewModel class)
                product.Price = "1.0"; 
                product.Stock = "1";

                // Act ==> test 120 caracters.
                product.Name = "123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 ";

                // Assert
                // Product model validation should failed and returns false.
                // UPD005(SMO) : FluentAssertions test
                //Assert.False(ValidateModel(product));
                ValidateModel(product).Should().Be(false);

                // Checks if error message corresponds to the one defined.
                // UPD005(SMO) : FluentAssertions test
                //Assert.Equal("MaxNameLength", GetFirstErrorMessage(product));
                GetFirstErrorMessage(product).Should().Be("MaxNameLength");
            }

            /// <summary>
            /// Unit test: product Description length can't be more than 200 caracters.
            /// </summary>
            /// <remarks>Test if StringLength (DataAnnotations) is set for Description field.</remarks>
            /// <remarks>Unit test not required by Louis project specifications.</remarks>
            /// <remarks>UT_TEST009(SMO)</remarks>
            [Fact]
            public void TestProductMaxDescriptionLength()
            {
                // *** Not requested by Louis specification ***. For learning purpose, fluent assertions and test code coverage evaluation.

                // Arrange ==> see public ProductViewModelValidationTest()
                // for instantiation of Product (ProductViewModel class)
                product.Name = "Unit Test Product : MaxDescriptionLength.";
                product.Price = "1.0";
                product.Stock = "1";

                // Act ==> test 220 caracters.
                product.Description = "123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 ";

                // Assert
                // Product model validation should failed and returns false.
                // UPD005(SMO) : FluentAssertions test
                //Assert.False(ValidateModel(product));
                ValidateModel(product).Should().Be(false);
                // Checks if error message corresponds to the one defined.
                // UPD005(SMO) : FluentAssertions test
                //Assert.Equal("MaxDescriptionLength", GetFirstErrorMessage(product));
                GetFirstErrorMessage(product).Should().Be("MaxDescriptionLength");
            }

            /// <summary>
            /// Unit test: product Details length can't be more than 400 caracters.
            /// </summary>
            /// <remarks>Test if StringLength (DataAnnotations) is set for Details field.</remarks>
            /// <remarks>Unit test not required by Louis project specifications.</remarks>
            /// <remarks>UT_TEST010(SMO)</remarks>
            [Fact]
            public void TestProductMaxDetailsLength()
            {
                // *** Not requested by Louis specification ***. For learning purpose, fluent assertions and test code coverage evaluation.

                // Arrange ==> see public ProductViewModelValidationTest()
                // for instantiation of Product (ProductViewModel class)
                product.Name = "Unit Test Product : MaxDetailsLength.";
                product.Price = "1.0";
                product.Stock = "1";

                // Act ==> test 420 caracters .
                product.Details = "123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 123456789 ";

                // Assert
                // Product model validation should failed and returns false.
                // UPD005(SMO) : FluentAssertions test
                //Assert.False(ValidateModel(product));
                ValidateModel(product).Should().Be(false);
                // Checks if error message corresponds to the one defined.
                // UPD005(SMO) : FluentAssertions test
                // Assert.Equal("MaxDetailsLength", GetFirstErrorMessage(product));
                GetFirstErrorMessage(product).Should().Be("MaxDetailsLength");
            }

        } // end ProductViewModelValidationTest class

        /// <summary>
        /// Contains integration tests for ProductViewModel.
        /// </summary>
        public class IntegrationTests
        {
            // Arrange

            // IConfiguration : représente un ensemble de propriétés de configuration
            // d’application de clé/valeur.            
            // Le type IConfiguration fournit un affichage unifié des données de configuration.
            // Le fournisseur de configuration JSON permet de mapper les fichiers appsettings.json
            // avec des objets .NET et il est utilisé avec l’injection de dépendances ==>
            // fournir une instance valide lors de l'injection.
            // https://learn.microsoft.com/fr-fr/dotnet/core/extensions/configuration
            private readonly IConfiguration _configuration;
            // Provide resource strings for ProductService
            // https://learn.microsoft.com/fr-fr/aspnet/core/fundamentals/localization/make-content-localizable?view=aspnetcore-8.0
            private readonly IStringLocalizer<ProductService> _localizer;

            /// <summary>
            /// (integration). Save a test product and check if created in database, then delete test record.            
            /// </summary>
            /// <returns></returns>
            /// <remarks>INT_TEST 001(SMO)</remarks>
            [Fact]
            public async Task TestSaveNewProduct()
            {
                // Arrange
                // P3Referential Database connection.
                var options = new DbContextOptionsBuilder<P3Referential>()
                    .UseSqlServer("Server=.;Database=P3Referential-2f561d3b-493f-46fd-83c9-6e2643e7bd0a;Trusted_Connection=True;MultipleActiveResultSets=true")
                    .Options;

                using (var ctx = new P3Referential(options, _configuration))
                {
                    // Inject necessaries dependancies.
                    LanguageService languageService = new();
                    Cart cart = new();
                    ProductRepository productRepository = new(ctx);
                    OrderRepository orderRepository = new(ctx);
                    ProductService productService = new(cart, productRepository, orderRepository, _localizer);
                    ProductController productController = new(productService, languageService);

                    // Instanciation and initialization of a new Product. 
                    ProductViewModel productViewModel = new() { Name = "Product from CREATE integration test: SaveNewProduct", Description = "Description", Details = "Detail", Stock = "1", Price = "150" };

                    // Store number of product before new product creation
                    // Await ==> suspend l’évaluation de la méthode async englobante (SaveNewProduct)
                    // jusqu’à ce que l’opération asynchrone représentée par son opérande se termine.
                    // https://learn.microsoft.com/fr-fr/dotnet/csharp/language-reference/operators/await
                    // CountAsync : retourne en asynchrone le nombre d'éléments de Product.
                    // On ne peut avoir plusieurs tâches asyncrhones simultanées, await assure que les
                    // autres tâches asynchrones dans ce contexte (ctx) sont terminées avant de lancer celle ci.                    
                    int count = await ctx.Product.CountAsync();

                    // Act
                    // Product creation test (create method of productViewModel class (which calls SaveProduct)).
                    productController.Create(productViewModel);

                    // Assert
                    // Ok if product number +1 after product creation.
                    Assert.Equal(count + 1, ctx.Product.Count());

                    // Ok if product found. 
                    // LinQ requête ==> https://learn.microsoft.com/fr-fr/dotnet/csharp/linq/get-started/introduction-to-linq-queries
                    var product = await ctx.Product.Where(x => x.Name == "Product from CREATE integration test: SaveNewProduct").FirstOrDefaultAsync();
                    Assert.NotNull(product);

                    // Clean DB.
                    // Comme on utilise Xunit et non MS TEST, on ne peut pas utiliser l'attribut TestCleanUp
                    // pour s'assurer de la suppression des enregistrements de tests).
                    ctx.Product.Remove(product);
                    // Saves changes in DB.
                    // Await ==> suspend l’évaluation de la méthode async englobante
                    // SaveChangesAsync : sauve les changements dans le contexte de la BDD après avoir
                    // checké qu'il n'y a pas d'opérations en cours dans le même contexte. 
                    await ctx.SaveChangesAsync();
                }
            }

            /// <summary>
            /// (integration) Test delete a new created product and don't find it in the DB anymore., then delete test record.           
            /// </summary>
            /// <returns></returns>
            /// <remarks>INT_TEST 002(SMO)</remarks>
            /// Programmation asynchrone (async Task) https://learn.microsoft.com/fr-fr/dotnet/csharp/asynchronous-programming/async-scenarios
            [Fact]
            public async Task TestDeleteProduct()
            {
                // Arrange
                // P3Referential Database connection.
                var options = new DbContextOptionsBuilder<P3Referential>()
                    .UseSqlServer("Server=.;Database=P3Referential-2f561d3b-493f-46fd-83c9-6e2643e7bd0a;Trusted_Connection=True;MultipleActiveResultSets=true")
                    .Options;

                using (var ctx = new P3Referential(options, _configuration))
                {
                    // Inject necessaries dependancies.
                    LanguageService languageService = new();
                    Cart cart = new();
                    ProductRepository productRepository = new(ctx);
                    OrderRepository orderRepository = new(ctx);
                    ProductService productService = new(cart, productRepository, orderRepository, _localizer);
                    ProductController productController = new(productService, languageService);

                    // Instanciation and initialization of a new Product. 
                    ProductViewModel productViewModel = new() { Name = "Product from DELETE integration test: DeleteProduct", Description = "Description", Details = "Detail", Stock = "1", Price = "150" };

                    // Store number of product before new product creation
                    int count = await ctx.Product.CountAsync();

                    // Product creation (create method of productViewModel class (which calls SaveProduct)).
                    productController.Create(productViewModel);

                    // Check if found and store in product var
                    var product = await ctx.Product.Where(x => x.Name == "Product from DELETE integration test: DeleteProduct").FirstOrDefaultAsync();

                    // Act
                    // Product delete test (call DeleteProduct method from productController class)
                    // which remove product from repository and cart
                    productController.DeleteProduct(product.Id);

                    // Assert
                    // Test if number of products is the same before creation and
                    // after delete of created test product.
                    Assert.Equal(count, ctx.Product.Count());
                    // Test if product delete is not found anymore.
                    var searchProductAgain = await ctx.Product.Where(x => x.Name == "Product from DELETE integration test").FirstOrDefaultAsync();
                    Assert.Null(searchProductAgain);
                }
            }

        } // end IntegrationTests class
    }
}