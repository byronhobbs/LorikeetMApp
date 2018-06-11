using System;
using System.Collections.Generic;

namespace LorikeetMApp.Models
{
    public class Contact
    {
        public int ContactId { get; set; }
        public int MemberId { get; set; }
        public string ContactType { get; set; }
        public string ContactName { get; set; }
        public string ContactAddress { get; set; }
        public string ContactPhone { get; set; }
    }
}
