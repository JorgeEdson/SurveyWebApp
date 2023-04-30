﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Survey.WebApp
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void buttonHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Default.aspx");
        }

        protected void buttonLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Views/Login.aspx");
        }
    }
}