using APITask_Project.Models.Validations;
using System.ComponentModel.DataAnnotations;

namespace APITask_Project.DTO
{
    public class MyTaskDTO
    {
        
            [Required(ErrorMessage = "عنوان نباید خالی باشد.")]
            public string Title { get; set; }

            [Required(ErrorMessage = "توضیحات نباید خالی باشد.")]
            public string Description { get; set; }

            [Required(ErrorMessage = "تاریخ سررسید نباید خالی باشد.")]
            [DateValidation(ErrorMessage = "تاریخ سررسید باید در آینده باشد.")]
            public DateTime? DueDate { get; set; }

            [Required(ErrorMessage = "مشخص کنید که تسک کامل شده یا نه.")]
            public bool? IsCompleted { get; set; }
       

    }
}
