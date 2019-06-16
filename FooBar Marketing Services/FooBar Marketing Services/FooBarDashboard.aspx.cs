using System;
using System.Net;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Security;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FooBar_Marketing_Services
{
    public partial class FooBarDashboard : System.Web.UI.Page
    {
        private Label previouslyOrderedLabel;
        private Label isWebCustomerLabel;
        private Label isPalindromeLabel;
        private Label customerIDLabel;
        private Label favouriteColorsLabel;
        private HyperLink editCustomerHyperLink;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void CustomerSearchListView_DataBound(object sender, EventArgs e)
        {
            //Iterate through all rows in the CustomerSearchListView section
            for (int i = 0; i < CustomerSearchListView.Items.Count; i++)
            {
                editCustomerHyperLink = (HyperLink)CustomerSearchListView.Items[i].FindControl("EditCustomerHyperLink");
                customerIDLabel = (Label)CustomerSearchListView.Items[i].FindControl("CustomerIDLabel");
                previouslyOrderedLabel = (Label)CustomerSearchListView.Items[i].FindControl("PreviouslyOrderedLabel");
                isWebCustomerLabel = (Label)CustomerSearchListView.Items[i].FindControl("IsWebCustomerLabel");
                isPalindromeLabel = (Label)CustomerSearchListView.Items[i].FindControl("IsPalindromeLabel");
                favouriteColorsLabel = (Label)CustomerSearchListView.Items[i].FindControl("FavouriteColorsLabel");
                editCustomerHyperLink.NavigateUrl = "~/CustomerForm.aspx?CustomerID=" + customerIDLabel.Text;
                if (previouslyOrderedLabel.Text == "True")
                {
                    previouslyOrderedLabel.Text = "Yes";
                    previouslyOrderedLabel.CssClass = "textGreen";
                }
                else
                {
                    previouslyOrderedLabel.Text = "No";
                    previouslyOrderedLabel.CssClass = "textRed";
                }
                if (isWebCustomerLabel.Text == "True")
                {
                    isWebCustomerLabel.Text = "Yes";
                    isWebCustomerLabel.CssClass = "textGreen";
                }
                else
                {
                    isWebCustomerLabel.Text = "No";
                    isWebCustomerLabel.CssClass = "textRed";
                }
                if (isPalindromeLabel.Text == "True")
                {
                    isPalindromeLabel.Text = "Yes";
                    isPalindromeLabel.CssClass = "textGreen";
                }
                else
                {
                    isPalindromeLabel.Text = "No";
                    isPalindromeLabel.CssClass = "textRed";
                }
                SqlCommand sqlCommand = new SqlCommand();
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FooBarCustomerDatabase"].ConnectionString);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                DataTable favouriteColoursDataTable = new DataTable();
                sqlCommand = new SqlCommand("SelectFavouriteColoursByCustomerID", connection);
                sqlCommand.Parameters.Add(new SqlParameter("@CustomerID", customerIDLabel.Text));
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlDataAdapter.SelectCommand = sqlCommand;
                sqlDataAdapter.Fill(favouriteColoursDataTable);
                string FavouriteColoursString = "";
                for (int j = 0; j < favouriteColoursDataTable.Rows.Count; j++)
                {
                    if (j == favouriteColoursDataTable.Rows.Count - 1)
                    {
                        FavouriteColoursString += favouriteColoursDataTable.Rows[j]["Name"].ToString();
                    }
                    else
                    {
                        FavouriteColoursString += favouriteColoursDataTable.Rows[j]["Name"].ToString() + ",";

                    }
                }
                favouriteColorsLabel.Text = FavouriteColoursString;
            }
        }

        protected void NewCustomerButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/CustomerForm.aspx?action=create");
        }
    }
}