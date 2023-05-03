using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Survey.WebApp.Domain
{
    public class Register
    {
        public int Id { get; set; }
        public string GivenName { get; set; }
        public string LastName { get; set; }
        public DateTime DateBirth { get; set; }
        public string MyProperty { get; set; }
        public int RespondentId { get; set; }
    }
}
