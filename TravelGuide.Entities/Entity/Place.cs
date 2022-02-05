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
    [Table("Places")]
    public class Place : MyEntityCommonBase
    {
        [DisplayName("Adı")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(50, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Name { get; set; }

        [DisplayName("Resim")]
        public string Image { get; set; }

        [DisplayName("Adres")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Address { get; set; }

        [DisplayName("Telefon")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(20, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Lütfen {0} alanı için geçerli bir telefon numarası giriniz.")]
        public string Phone { get; set; }

        [DisplayName("Açıklama")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Description { get; set; }

        [DisplayName("Yol Tarifi")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string RoadDescribe { get; set; }

        [DisplayName("Enlem")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Latitude { get; set; }

        [DisplayName("Boylam")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Longitude { get; set; }

        [DisplayName("Şehir")]
        public int CityId { get; set; }
        public virtual City City { get; set; }

        public virtual List<RoadDescribeUnit> RoadDescribeUnits { get; set; }
        public Place()
        {
            RoadDescribeUnits = new List<RoadDescribeUnit>();
        }
    }
}
