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
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Language_User_Tie = new HashSet<Language_User_Tie>();
            this.Messages = new HashSet<Message>();
            this.Messages1 = new HashSet<Message>();
            this.Opportunities = new HashSet<Opportunity>();
            this.OpportunityResponse_User_Tie = new HashSet<OpportunityResponse_User_Tie>();
            this.ProfileViews = new HashSet<ProfileView>();
            this.ProfileViews1 = new HashSet<ProfileView>();
            this.SpamLists = new HashSet<SpamList>();
            this.SpamLists1 = new HashSet<SpamList>();
            this.User_Role_Tie = new HashSet<User_Role_Tie>();
            this.UserInfoes = new HashSet<UserInfo>();
            this.BlockLists = new HashSet<BlockList>();
            this.BlockLists1 = new HashSet<BlockList>();
            this.Notifications = new HashSet<Notification>();
            this.Profession_User_Tie = new HashSet<Profession_User_Tie>();
        }
    
        public long UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string EmailID { get; set; }
        public string PhoneNum { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public Nullable<int> ProfileType { get; set; }
        public Nullable<bool> IsContentApproverApproved { get; set; }
        public Nullable<bool> IsAdminApproved { get; set; }
        public Nullable<bool> IsPaymentUser { get; set; }
        public Nullable<bool> IsProfileCreated { get; set; }
        public Nullable<int> Views { get; set; }
        public Nullable<int> NumOfOpportunitiesCreated { get; set; }
        public Nullable<bool> IsEmailApproved { get; set; }
        public string CustomerId { get; set; }
        public Nullable<bool> IsImageVideoContentModified { get; set; }
        public string ContentApproverComments { get; set; }
        public string AdminComments { get; set; }
        public Nullable<System.DateTime> LastSignIn { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Language_User_Tie> Language_User_Tie { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> Messages { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> Messages1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Opportunity> Opportunities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OpportunityResponse_User_Tie> OpportunityResponse_User_Tie { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProfileView> ProfileViews { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProfileView> ProfileViews1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpamList> SpamLists { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpamList> SpamLists1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User_Role_Tie> User_Role_Tie { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserInfo> UserInfoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BlockList> BlockLists { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BlockList> BlockLists1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Notification> Notifications { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Profession_User_Tie> Profession_User_Tie { get; set; }
    }
}
