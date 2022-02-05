using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelGuide.Entities.EntityBase
{
    public abstract class MyEntityCommonBase
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Kayıt Tarihi")]
        [Column(TypeName = "datetime2")]
        public DateTime? CreatedOn { get; set; }

        [DisplayName("Kaydeden")]
        public string CreatedUsername { get; set; }

        [DisplayName("Güncelleme Tarihi")]
        [Column(TypeName = "datetime2")]
        public DateTime? ModifiedOn { get; set; }

        [DisplayName("Güncelleyen")]
        public string ModifiedUsername { get; set; }

        [DisplayName("Silindi Mi?")]
        public bool IsDeleted { get; set; }

        [DisplayName("Silme Tarihi")]
        [Column(TypeName = "datetime2")]
        public DateTime? DeletedOn { get; set; }

        [DisplayName("Silen")]
        public string DeletedUsername { get; set; }
    }
}
