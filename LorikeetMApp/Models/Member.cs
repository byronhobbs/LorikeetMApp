using System;
using System.Collections.Generic;

namespace LorikeetMApp.Models
{
    public class Member
	{
        public int MemberId { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public sbyte? Sex { get; set; }
        public sbyte? Aboriginal { get; set; }
        public string StreetAddress { get; set; }
        public string PostCode { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string TelephoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAddress { get; set; }
        public DateTime? DateAltered { get; set; }
    }
}
