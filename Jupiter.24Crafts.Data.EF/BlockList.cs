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
    
    public partial class BlockList
    {
        public int BlockId { get; set; }
        public long BlockedByUserId { get; set; }
        public long BlockedUserId { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public bool IsApprovedAdmin { get; set; }
    
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
