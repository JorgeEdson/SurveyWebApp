using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey.WebApp.Domain
{
    public class Option
    {
        public string Text { get; set; }
        public int QuestionId { get; set; }
        public override string ToString() => Text;
    }
}