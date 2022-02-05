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
    [Table("Roles")]
    public class Role : MyEntityCommonBase
    {
        [DisplayName("Rol Adı")]
        [Required(ErrorMessage ="{0} alanı boş geçilemez."),StringLength(20,ErrorMessage ="{0} alanı en fazla {1} karakter olmalıdır.")]
        public string Name { get; set; }

        public virtual List<Member> Members { get; set; }
        public Role()
        {
            Members = new List<Member>();
        }
    }
}
