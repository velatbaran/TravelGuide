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
    [Table("RoadDescribeUnits")]
    public class RoadDescribeUnit: MyEntityCommonBase
    {
        [DisplayName("Detay")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string Detail { get; set; }

        [DisplayName("Mekan")]
        public int PlaceId { get; set; }
        public virtual Place Place { get; set; }
    }
}
