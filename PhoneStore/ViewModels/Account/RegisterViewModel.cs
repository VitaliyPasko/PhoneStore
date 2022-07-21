using System.ComponentModel.DataAnnotations;

namespace PhoneStore.ViewModels.Account
{
    public class RegisterViewModel
    {
        //TODO: Добавить ограничение по минимальной и максимальной длине строк и возраста.
        private const string  RequiredErrorMessage = "Поле обязательно для заполнения";
        [Required(ErrorMessage = RequiredErrorMessage)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = RequiredErrorMessage)]
        [Display(Name = "Возраст")]
        public int Age { get; set; }
        
        [Required(ErrorMessage = RequiredErrorMessage)]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
 
        [Required(ErrorMessage = RequiredErrorMessage)]
        [Compare(nameof(Password), ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string PasswordConfirm { get; set; }
    }
}