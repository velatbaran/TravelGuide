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
    [Table("Cities")]
    public class City : MyEntityCommonBase
    {
        [DisplayName("Ülke")]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

        [DisplayName("Adı")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(30, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        public string Name { get; set; }

        [DisplayName("Resim")]
        public string Image { get; set; }

        [DisplayName("Slogan")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Slogan { get; set; }

        [DisplayName("Tarihi Bilgisi")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string DateInformation { get; set; }

        [DisplayName("Gezilecek Yerler")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string WanderPlaces { get; set; }

        [DisplayName("Yemekler")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Foods { get; set; }

        [DisplayName("Diğer Bilgiler")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string OtherInformations { get; set; }

        public virtual List<Place> Places { get; set; }
        public virtual List<Comment> Comments { get; set; }

        public City()
        {
            Places = new List<Place>();
            Comments = new List<Comment>();
        }
    }
}
