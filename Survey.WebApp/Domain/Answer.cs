using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey.WebApp.Domain
{
    public class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int QuestionId { get; set; }
        public int RespondentId { get; set; }
    }
}