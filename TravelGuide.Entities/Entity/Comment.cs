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
    [Table("Comments")]
    public class Comment 
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Şehir")]
        public int CityId { get; set; }
        public virtual City City { get; set; }

        [DisplayName("Yorum Metni")]
        [Required(ErrorMessage = "{0} alanı boş geçilemez.")]
        public string CommentText { get; set; }

        [DisplayName("Yorum Tarihi")]
        public DateTime CommentDate { get; set; }

        public virtual Member Owner { get; set; }
    }
}
