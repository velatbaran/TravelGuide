using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelGuide.Entities.Entity;
using TravelGuide.Entities.EntityBase;

namespace TravelGuide.Entities.Entity
{
    [Table("Members")]
    public class Member : MyEntityCommonBase
    {
        [DisplayName("Adı")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(50, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        public string Name { get; set; }

        [DisplayName("Soyadı")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(50, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        public string Surname { get; set; }

        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(50, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        public string Username { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(50, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        [DataType(DataType.EmailAddress,ErrorMessage = "Lütfen geçerli bir email adresi giriniz.")]
        public string Email { get; set; }

        [DisplayName("Rol")]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        [DisplayName("Kilitli Mi?"),DefaultValue(false)]
        public bool IsLock { get; set; }

        [DisplayName("Profil Resmi")]
        [ScaffoldColumn(false)]
        public string ProfileImage { get; set; }

        [DisplayName("Şifre")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez."),MinLength(4,ErrorMessage ="{0} alanı En az {1} karakter olmalıdır."),MaxLength(100,ErrorMessage = "{0} alanı En fazla {1} karakter olmalıdır.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Şifre Tekrar")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez."), MinLength(4, ErrorMessage = "{0} alanı En az {1} karakter olmalıdır."), MaxLength(100, ErrorMessage = "{0} alanı En fazla {1} karakter olmalıdır.")]
        [DataType(DataType.Password),Compare("Password",ErrorMessage ="{0} alanı ile {1} alanı uyuşmamaktadır.")]
        public string RePassword { get; set; }

        [DisplayName("Şifre Resetleme Tarihi")]
        [ScaffoldColumn(false)]
        [Column(TypeName = "datetime2")]
        public DateTime? PasswordResetDate { get; set; }

        public virtual List<Comment> Comments { get; set; }
        public Member()
        {
            Comments = new List<Comment>();
        }
    }
}
