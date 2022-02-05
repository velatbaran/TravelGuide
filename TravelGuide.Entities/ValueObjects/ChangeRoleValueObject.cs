using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelGuide.Entities.ValueObjects
{
    public class ChangeRoleValueObject
    {
        [DisplayName("Üye ID")]
        public int MemberId { get; set; }

        [DisplayName("Rol")]
        public int RoleId { get; set; }
    }
}
