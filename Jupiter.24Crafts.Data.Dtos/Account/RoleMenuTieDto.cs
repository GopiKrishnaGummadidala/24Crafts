using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Dtos.Account
{
    public class RoleMenuTieDto
    {
        public long RoleID { get; set; }
        public long MainMenuID { get; set; }

        public string RoleName { get; set; }

        public List<string> MenuName { get; set; }

        public bool? IsActive { get; set; }
        public long CreatedBy_UserID { get; set; }
        public long? ModifiedBy_UserID { get; set; }
    }
}
