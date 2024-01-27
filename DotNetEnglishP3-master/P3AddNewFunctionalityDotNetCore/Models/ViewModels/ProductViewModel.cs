using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
/* UPD004(SMO) : bug fix client side error message display.

using System;
using System.Collections.Generic;
// UPD02(SMO) : DataAnnotations support
using System.ComponentModel.DataAnnotations;
using System.Configuration;

using Microsoft.Extensions.Localization;
using P3AddNewFunctionalityDotNetCore.Controllers; */


namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class ProductViewModel
    {
        [BindNever]
        public int Id { get; set; }
        // UPD004(SMO) : bug fix client side error message display.
        // UPD004(SMO) : add empty string not allowed for Name.
        // UPD002(SMO) : server side data annotations
        [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "MissingName", ErrorMessageResourceType = typeof(Resources.ProductService))]                
        // *** For TESTING fluent assertions purposes ***.
        // UPD05(SMO) : add a maximum length of 100.
        [StringLength(100, ErrorMessage = "MaxNameLength")]
        public string Name { get; set; }
        // *** For TESTING fluent assertions purposes ***.
        // UPD005(SMO) : add a maximum length of 200.
        [StringLength(200, ErrorMessage = "MaxDescriptionLength")]
        public string Description { get; set; }
        // *** For TESTING fluent assertions purposes ***.
        // UPD005(SMO) : add a maximum length of 400.
        [StringLength(400, ErrorMessage = "MaxDetailsLength")]
        public string Details { get; set; }
        // UPD004(SMO) : bug fix client side error message display.
        // UPD002(SMO) : server side data annotations
        [Required(ErrorMessageResourceName = "MissingStock", ErrorMessageResourceType = typeof(Resources.ProductService))]
        // UPD002(SMO) : Update regEx (simplified).
        [RegularExpression("([0-9]+)", ErrorMessageResourceName = "StockNotAnInteger", ErrorMessageResourceType = typeof(Resources.ProductService))]
        // UPD002(SMO) : Update range with integer limit.
        [Range(1, int.MaxValue, ErrorMessageResourceName = "StockNotGreaterThanZero", ErrorMessageResourceType = typeof(Resources.ProductService))]                
        public string Stock { get; set; }
        // UPD004(SMO) : bug fix client side error message display.
        // UPD002(SMO) : server side data annotations
        [Required(ErrorMessageResourceName = "MissingPrice", ErrorMessageResourceType = typeof(Resources.ProductService))]
        // UPD002(SMO) : update regEx for matching with allowed decimal value for float data type in BDD.        
        [RegularExpression(@"^[0-9]+((\.)[0-9]+)*$", ErrorMessageResourceName = "PriceNotANumber", ErrorMessageResourceType = typeof(Resources.ProductService))]
        //[RegularExpression(@"^[0-9]{1,5}((\.|,)[0-9]{1,2})*$", ErrorMessageResourceName = "PriceNotANumber", ErrorMessageResourceType = typeof(Resources.ProductService))]
        //[RegularExpression(@"^(?:\d+(?:\.\d+)?|\.\d+)(?:,(?:\d+(?:\.\d+)?|\.\d+))*$", ErrorMessageResourceName = "PriceNotANumber", ErrorMessageResourceType = typeof(Resources.ProductService))]
        // UPD002(SMO) : update range with min and max value for double.
        // double.Epsilon : smallest positive double value that is greater than zero.
        [Range(double.Epsilon, double.MaxValue, ErrorMessageResourceName = "PriceNotGreaterThanZero", ErrorMessageResourceType = typeof(Resources.ProductService))]                
        public string Price { get; set; }
    }
}
