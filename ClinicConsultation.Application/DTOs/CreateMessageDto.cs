using System.ComponentModel.DataAnnotations;

namespace ClinicConsultation.Application.DTOs
{
    public class CreateMessageDto
    {
        [Required(ErrorMessage = "Role is required.")]
        [RegularExpression("^(user|assistant)$", ErrorMessage = "Role must be 'user' or 'assistant'.")]
        public string Role { get; set; } = "user";

        [Required(ErrorMessage = "Content is required.")]
        [MaxLength(4000, ErrorMessage = "Message content cannot exceed 4000 characters.")]
        public string Content { get; set; } = string.Empty;
    }
}
