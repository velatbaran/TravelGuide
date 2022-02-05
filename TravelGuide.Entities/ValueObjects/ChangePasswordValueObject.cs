using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelGuide.Entities.ValueObjects
{
    public class ChangePasswordValueObject
    {
        [DisplayName("Şifre")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez."), MinLength(4, ErrorMessage = "{0} alanı En az {1} karakter olmalıdır."), MaxLength(100, ErrorMessage = "{0} alanı En fazla {1} karakter olmalıdır.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Şifre Tekrar")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez."), MinLength(4, ErrorMessage = "{0} alanı En az {1} karakter olmalıdır."), MaxLength(100, ErrorMessage = "{0} alanı En fazla {1} karakter olmalıdır.")]
        [DataType(DataType.Password), Compare("Password", ErrorMessage = "{0} alanı ile {1} alanı uyuşmamaktadır.")]
        public string RePassword { get; set; }
    }
}
