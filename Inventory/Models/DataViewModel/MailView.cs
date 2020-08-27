using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATP2_Project.Models.DataViewModel
{
    public class MailView
    {
        public string to { get; set; }
        public string from { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
    }
}