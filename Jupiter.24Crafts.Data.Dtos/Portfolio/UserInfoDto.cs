using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jupiter._24Crafts.Data.Dtos.Portfolio
{
    public class UserInfoDto
    {
        public Nullable<int> Gender { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<int> MaritalStatus { get; set; }
        public string Hobbies { get; set; }
        public string SelfDescription { get; set; }
        public string Qualification { get; set; }
        public Nullable<int> ExpYear { get; set; }
        public Nullable<int> ExpMonth { get; set; }
        public string Remunaration { get; set; }
        public string CurrAssignment { get; set; }
        public string LastAssignment { get; set; }
        public Nullable<System.DateTime> AvailabilityFrom { get; set; }
        public Nullable<System.DateTime> AvailabilityTo { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public Nullable<int> PrimaryProfessionId { get; set; }
        public string GoogleDriveUrl { get; set; }
        public string YputubeChannel { get; set; }
    }
}
