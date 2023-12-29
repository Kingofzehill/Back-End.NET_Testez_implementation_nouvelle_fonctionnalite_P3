using Microsoft.AspNetCore.Mvc.ModelBinding;
// UPD02(SMO) : DataAnnotations support
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace P3AddNewFunctionalityDotNetCore.Models.ViewModels
{
    public class ProductViewModel
    {
        [BindNever]
        public int Id { get; set; }
        // UPD02(SMO) : mandatory field Name.
        [Required(ErrorMessage = "MissingName")]
        // UPD02(SMO) : add a maximum length of 100.
        [StringLength(100, ErrorMessage = "MaxNameLength")]
        public string Name { get; set; }
        // UPD02(SMO) : add a maximum length of 200.
        [StringLength(200, ErrorMessage = "MaxDescriptionLength")]
        public string Description { get; set; }
        // UPD02(SMO) : add a maximum length of 400.
        [StringLength(400, ErrorMessage = "MaxDetailsLength")]
        public string Details { get; set; }
        // UPD02(SMO) : mandatory field Stock.
        [Required(ErrorMessage = "MissingQuantity")]
        // UPD02(SMO) : Only allow whole (integer) numbers up to 10000
        [RegularExpression(@"([0-9]|[1-9][0-9]|[1-9][0-9][0-9]|[1-9][0-9][0-9][0-9]|10000)$", ErrorMessage = "QuantityNotAnInteger")]
        // UPD02(SMO) : 0 value is forbidden, 10000 max value.
        [Range(1, 10000, ErrorMessage = "QuantityNotGreaterThanZero")]
        // UPD02(SMO) : Change Stock datatype from string to integer.
        // int? datatype for allowing null vaFlue.
        public string Stock { get; set; }
        // UPD02(SMO) : mandatory field Price.
        [Required(ErrorMessage = "MissingPrice")]
        // UPD02(SMO) : allow 2 digits after the decimal separator and 5 numbers before
        // Work good but doesn't allow , as decimal separator ==> [RegularExpression(@"^[0-9]{1,5}(\.[0-9]{1,2})?$", ErrorMessage = "PriceNotANumber")]
        [RegularExpression(@"^[0-9]{1,5}((\.|,)[0-9]{1,2})?$", ErrorMessage = "PriceNotANumber")]
        // UPD02(SMO) : 0 value is forbidden, 10000€ max value*
        // ==> extended from 0.1 to 1 minimum value as
        // it avoids to control the kind of decimal separator : ". / ,".
        // ==> Ok but can't use 0.1 as min value
        //        [Range(1, 10000, ErrorMessage = "PriceNotGreaterThanZero")]
        // Exception with typeof(decimal) ==>  because field is a string, randow error if datype field = double
        //        [Range(typeof(decimal), "0.1", "10000", ErrorMessage = "PriceNotGreaterThanZero")]
        [Range(0.1, 10000, ErrorMessage = "PriceNotGreaterThanZero")]
        // UPD02(SMO) : Change Price datatype from string to double.
        // Change string to double datatype for decimal values management in Range control instruction
        public double Price { get; set; }
    }
}
