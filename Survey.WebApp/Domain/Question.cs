using Survey.WebApp.Domain.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Survey.WebApp.Domain
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int Required { get; set; }
        public QuestionType Type { get; set; } = QuestionType.Text;
        public List<Option> Options { get; set; } = new List<Option>();
        public int? NextId { get; set; } = null;

        public bool IsRequired
        {
            get { return Required != 0; }
        }

        public bool HasNext
        {
            get { return NextId != null; }
        }
    }
}