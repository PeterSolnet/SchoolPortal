//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PortalEngine.DAL.Domain
{
    using System;
    using System.Collections.Generic;
    
    public partial class Student
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string Email { get; set; }
        public string MatricNo { get; set; }
        public string Phone { get; set; }
        public string Religion { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string LocalGovt { get; set; }
        public Nullable<int> StateID { get; set; }
        public Nullable<int> CountryID { get; set; }
        public int SchoolID { get; set; }
        public byte[] Pictures { get; set; }
    }
}
