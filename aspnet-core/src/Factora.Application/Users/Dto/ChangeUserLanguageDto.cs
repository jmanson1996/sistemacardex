using System.ComponentModel.DataAnnotations;

namespace Factora.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}