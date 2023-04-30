using Survey.WebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Survey.WebApp.Views
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            var userName = LoginName.Text;
            var password = Password.Text;

            try
            {
                if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
                {
                    var query = DatabaseOperationsService.Login(userName, password);

                    if (query)
                    {
                        Response.Redirect("/Views/Search.aspx");
                    }
                    else
                    {
                        ErrorLabel.Text = "User not found in database.";
                        ErrorLabel.Visible = true;
                    }
                }
                else
                {
                    ErrorLabel.Text = "Username and Password is required.";
                    ErrorLabel.Visible = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}