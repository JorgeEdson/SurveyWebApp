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
            if (Session["ShowMessageBox"] != null) 
            {
                ShowMessageBox("Thanks for Response.", this.Page, this);
                Session["ShowMessageBox"] = null;
            }
        }

        /// <summary>
        ///  Method to show a javascript alert on the Page
        /// </summary>
        private void ShowMessageBox(String ex, Page pg, Object obj)
        {
            string s = "<SCRIPT language='javascript'>alert('" + ex.Replace("\r\n", "\\n").Replace("'", "") + "'); </SCRIPT>";
            Type cstype = obj.GetType();
            ClientScriptManager cs = pg.ClientScript;
            cs.RegisterClientScriptBlock(cstype, s, s.ToString());
        }

        /// <summary>
        /// Button Survey event that generates a Respondent and redirects to the survey page
        /// </summary>
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

        /// <summary>
        /// Login Button event that redirects to the Login page
        /// </summary>        
        protected void buttonLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Views/Login.aspx");
        }
        
        /// <summary>
        /// Method to retrieve User IP
        /// </summary>        
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