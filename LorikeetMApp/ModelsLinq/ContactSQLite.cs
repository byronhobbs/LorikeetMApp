using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LorikeetMApp.ModelsLinq
{
	[Table("Contact")]
    public class ContactSQLite
    {
        [Key]
		public int ContactId { get; set; }
        public int MemberId { get; set; }
        public string ContactType { get; set; }
        public string ContactName { get; set; }
        public string ContactAddress { get; set; }
        public string ContactPhone { get; set; }
    }
}
