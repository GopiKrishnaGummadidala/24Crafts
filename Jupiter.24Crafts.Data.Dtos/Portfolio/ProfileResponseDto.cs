using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Dtos.Portfolio
{
    public class ProfileResponseDto
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public List<string> Profession { get; set; }
        public string ImageSrc { get; set; }
    }
}
