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
    
    public partial class Message
    {
        public long MessageId { get; set; }
        public string Message1 { get; set; }
        public long FromUserId { get; set; }
        public long ToUserId { get; set; }
        public Nullable<bool> IsViewed { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
    
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
