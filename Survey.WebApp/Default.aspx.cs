using System;
using System.Collections.Generic;
using System.Linq;
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
            Response.Redirect("Views/Survey.aspx");
        }

        protected void buttonLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("Views/Login.aspx");
        }
    }
}