//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ticketTracker.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblUser
    {
        public int idsUser { get; set; }
        public string txtUserName { get; set; }
        public string txtPassword { get; set; }
        public string txtSalt { get; set; }
        public System.DateTime dtmCreate { get; set; }
        public bool blnAdmin { get; set; }
    }

}
