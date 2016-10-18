using System.ComponentModel.DataAnnotations;
using ExampleApp.Infrastructure;
using System.Collections.Generic;

namespace ExampleApp.Models {

    public class Product : IValidatableObject {

        public int ProductID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IncludeInSale { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext
                validationContext) {

            List<ValidationResult> errors = new List<ValidationResult>();

            if (Name == null || Name == string.Empty) {
                errors.Add(new ValidationResult(
                    "A value is required for the Name property"));
            }

            if (Price == 0) {
                errors.Add(new ValidationResult(
                    "A value is required for the Price property"));
            } else if (Price < 1 || Price > 2000) {
                errors.Add(new ValidationResult("The Price value is out of range"));
            }

            if (IncludeInSale) {
                errors.Add(new ValidationResult(
                    "Request cannot contain values for IncludeInSale"));
            }
            return errors;
        }
    }
}
