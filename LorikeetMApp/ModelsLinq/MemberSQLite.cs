using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace LorikeetMApp.ModelsLinq
{
	[Table("Member")]
	public class MemberSQLite
    {
        [Key]
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
        public string PictureGUID { get; set; }
        [JsonIgnore]
        public string NumberText
		{
			get
			{
				if (MobileNumber != "")
				{
					return "Mobile - " + MobileNumber;
				}
				else if (TelephoneNumber != "")
				{
					return "Telephone - " + TelephoneNumber;
				}
				return "No Phone Number";
			}
		}
		[JsonIgnore]
		public string FullName => FirstName.Trim() + " " + Surname.Trim();
	}
}
