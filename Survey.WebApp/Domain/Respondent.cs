using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey.WebApp.Domain
{
    public class Respondent
    {
        public int Id { get; set; }
        public string IpAddres { get; set; }
        public DateTime RespondendAt { get; set; }
    }
}