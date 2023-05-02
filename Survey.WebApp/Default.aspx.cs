using Survey.WebApp.Domain;
using Survey.WebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Survey.WebApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void buttonSurvey_Click(object sender, EventArgs e)
        {
            Respondent respondent = new Respondent();
            respondent.IpAddres = GetIPAddress();
            respondent.RespondendAt = DateTime.Now;
            respondent.Id = DatabaseOperationsService.IncrementId("respondents");
            DatabaseOperationsService.AddRespondent(respondent);
            Session["RespondentId"] = respondent.Id;
            Response.Redirect("Views/Survey.aspx");            
        }

        protected void buttonLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Views/Login.aspx");
        }

        public string GetIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}