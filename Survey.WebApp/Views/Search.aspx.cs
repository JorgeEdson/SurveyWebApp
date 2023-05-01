using Survey.WebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Survey.WebApp.Views
{
    public partial class Search : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            var gender = Gender.Text == "Select the gender" ? "" : Gender.Text; ;
            var age = AgeRange.Text == "Select the age" ? "" : AgeRange.Text;
            var stateOrTerritory = StateOrTerritory.Text == "State Or Territory" ? "" : StateOrTerritory.Text;
            var homeSuburb = HomeSuburb.Text;
            var postCode = PostCode.Text;
            var email = Email.Text;
            var bankUsed = BankUsed.Text == "Bank Used" ? "" : BankUsed.Text;
            var aditionalService = AditionalServices.Text == "Additional Services" ? "" : AditionalServices.Text;
            var newspaper = Newspaper.Text == "Newspaper" ? "" : Newspaper.Text;
            var sectionRead = SectionRead.Text == "Most Read Section" ? "" : SectionRead.Text;
            var favoriteSports = FavoriteSports.Text == "Favorite Sports" ? "" : FavoriteSports.Text;
            var travelDestination = TravelDestination.Text == "Travel Destination" ? "" : TravelDestination.Text;
            var username = UserName.Text;

            try
            {
                var result = DatabaseOperationsService.SearchFilter(gender, age, stateOrTerritory, homeSuburb, postCode, email, bankUsed, aditionalService, newspaper,
                                                                    sectionRead, favoriteSports, travelDestination, username);

                if (result != null && result.Count > 0)
                {
                    foreach(var item in result)
                    {
                        item.Name = item.Name == null || item.Name == "" ? "Anonymous" : item.Name;
                        item.LastName = item.LastName == null || item.Name == "" ? "Anonymous" : item.LastName;
                    }

                    TableError.Visible = false;
                    Gridview.Visible = true;
                    Gridview.DataSource = result;
                    Gridview.DataBind();
                }
                else
                {
                    Gridview.Visible = false;
                    TableError.Visible = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void ButtonClear_Click(object sender, EventArgs e)
        {
            Gender.ClearSelection();
            AgeRange.ClearSelection();
            StateOrTerritory.ClearSelection();
            HomeSuburb.Text = "";
            PostCode.Text = "";
            Email.Text = "";
            BankUsed.ClearSelection();
            AditionalServices.ClearSelection();
            Newspaper.ClearSelection();
            SectionRead.ClearSelection();
            FavoriteSports.ClearSelection();
            TravelDestination.ClearSelection();
            UserName.Text = "";
        }
    }
}