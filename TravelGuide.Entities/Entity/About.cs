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
    [Table("About")]
    public class About : MyEntityCommonBase
    {
        [DisplayName("Hakkımızda Resmi")]
        public string AboutImage { get; set; }

        [DisplayName("Başlık")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez."),StringLength(150,ErrorMessage ="{0} alanı en fazla {1} karakter olmalıdır.")]
        public string Title { get; set; }

        [DisplayName("Açıklama")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Description { get; set; }
    }
}
