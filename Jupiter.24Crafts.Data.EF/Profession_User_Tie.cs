//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Jupiter._24Crafts.Data.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class Profession_User_Tie
    {
        public int ID { get; set; }
        public long UserID { get; set; }
        public int ProfessionID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public long CreatedBy_UserID { get; set; }
        public Nullable<long> ModifiedBy_UserID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual Profession Profession { get; set; }
        public virtual User User { get; set; }
    }
}
