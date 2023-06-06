using System.ComponentModel.DataAnnotations;

namespace AspNetCoreApp.DAL.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }     // адрес элекстронной почты

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }   // пароль

        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }   // переменная запоминания

    }
}