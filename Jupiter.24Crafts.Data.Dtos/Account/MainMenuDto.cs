using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Dtos.Account
{
    public class MainMenuDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string NavigateURL { get; set; }
        public string Script { get; set; }
        public int? SortOrder { get; set; }
        public bool? IsActive { get; set; }
    }
}
