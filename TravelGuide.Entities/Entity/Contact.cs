using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelGuide.Entities.EntityBase;

namespace TravelGuide.Entities.Entity
{
    [Table("Contact")]
    public class Contact : MyEntityCommonBase
    {
        [DisplayName("Adres")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Address { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(50, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Lütfen {0} alanı için geçerli bir e-mail adresi giriniz.")]
        public string Email { get; set; }

        [DisplayName("Telefon")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(20, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Lütfen {0} alanı için geçerli bir telefon numarası giriniz.")]
        public string Phone { get; set; }

        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public string Telegram { get; set; }
        public string Github { get; set; }
    }
}
