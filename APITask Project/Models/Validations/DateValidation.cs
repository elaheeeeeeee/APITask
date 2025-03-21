using System.ComponentModel.DataAnnotations;

namespace APITask_Project.Models.Validations
{
    public class DateValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || !(value is DateTime))
            {
                return new ValidationResult("تاریخ معتبر نیست.");
            }

            DateTime dueDate = (DateTime)value;
            if (dueDate <= DateTime.Now)
            {
                return new ValidationResult(ErrorMessage ?? "تاریخ سررسید باید در آینده باشد.");
            }

            return ValidationResult.Success;
        }
    }
}
