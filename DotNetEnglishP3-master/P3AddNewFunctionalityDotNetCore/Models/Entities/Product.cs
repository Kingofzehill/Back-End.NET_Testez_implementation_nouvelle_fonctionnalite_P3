using System.Collections.Generic;

namespace P3AddNewFunctionalityDotNetCore.Models.Entities
{
    public partial class Product
    {
        public Product()
        {
            OrderLine = new HashSet<OrderLine>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public string Name { get; set; }
        // UPD02(SMO) : Change Price datatype from string to double.
        // Change string to double datatype for decimal values management in Range control instruction
        // UT_TEST004(SMO) : change price datatype from double to string for test purposes
        // public double Price { get; set; }
        public string Price { get; set; }       
        public int Quantity { get; set; }

        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
