using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Dtos.Account
{
    public class ResponseRoleDto
    {
        public long RoleID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
    }
}
