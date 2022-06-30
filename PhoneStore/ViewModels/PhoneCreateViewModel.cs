using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PhoneStore.Models;

namespace PhoneStore.ViewModels
{
    public class PhoneCreateViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Remote("CheckName", "PhoneValidator", ErrorMessage = "Имя занято")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Минимальная длина 3 символа, максимальная - 50")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле обязательно для заполнения")]
        [Range(1000, 50000, ErrorMessage = "Значение не валидно, вводить можно знаение от 1000 до 5000")]
        public decimal? Price { get; set; }
        public int BrandId { get; set; }
        public List<Brand> Brands { get; set; }
    }
}