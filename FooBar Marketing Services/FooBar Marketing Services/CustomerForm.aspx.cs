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
    public partial class CustomerForm : System.Web.UI.Page
    {
        private CheckBoxList favouriteColoursCheckBoxList;
        private TextBox dateActiveDDTextBox;
        private TextBox dateActiveMMTextBox;
        private TextBox dateActiveYYTextBox;
        private TextBox dateActiveTextBox;
        private TextBox firstNameTextBox;
        private TextBox lastNameTextBox;
        private CheckBox previouslyOrderedCheckbox;
        private CheckBox webCustomerCheckBox;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] == "create")
            {
                CustomerFormView.ChangeMode(FormViewMode.Insert);
            }
            else if (Request.QueryString["CustomerID"] != null)
            {
                int CustomerID = Convert.ToInt32(Request.QueryString["CustomerID"].ToString());
                CustomerFormView.ChangeMode(FormViewMode.Edit);
                CustomerDataSource.SelectCommandType = SqlDataSourceCommandType.StoredProcedure;
                CustomerDataSource.SelectCommand = "SelectCustomerByCustomerID";
                CustomerDataSource.SelectParameters.Add("customerID", DbType.Int32, Request.QueryString["CustomerID"].ToString());

                if (!Page.IsPostBack)
                {
                    favouriteColoursCheckBoxList = (CheckBoxList)CustomerFormView.FindControl("FavouriteColoursCheckBoxList");
                    SqlCommand sqlCommand = new SqlCommand();
                    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["FooBarCustomerDatabase"].ConnectionString);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
                    DataTable favouriteColoursDataTable = new DataTable();
                    sqlCommand = new SqlCommand("SelectFavouriteColoursByCustomerID", connection);
                    sqlCommand.Parameters.Add(new SqlParameter("@CustomerID", Request.QueryString["CustomerID"].ToString()));
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlDataAdapter.SelectCommand = sqlCommand;
                    sqlDataAdapter.Fill(favouriteColoursDataTable);
                    for (int i = 0; i < favouriteColoursDataTable.Rows.Count; i++)
                    {
                        favouriteColoursCheckBoxList.Items.FindByValue(favouriteColoursDataTable.Rows[i]["ColourId"].ToString()).Selected = true;
                    }
                }
            }
            else
            {
                Response.Redirect("~/FooBarDashboard.aspx");
            }
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            dateActiveDDTextBox = (TextBox)CustomerFormView.FindControl("DateActiveDDTextBox");
            dateActiveMMTextBox = (TextBox)CustomerFormView.FindControl("DateActiveMMTextBox");
            dateActiveYYTextBox = (TextBox)CustomerFormView.FindControl("DateActiveYYTextBox");
            dateActiveTextBox = (TextBox)CustomerFormView.FindControl("DateActiveTextBox");
            //Add javascript that auto tabs through the date fields
            if (dateActiveDDTextBox != null && dateActiveMMTextBox != null && dateActiveYYTextBox != null && dateActiveTextBox != null)
            {
                string JavaScript = "<script type=\"text/javascript\" language=\"javascript\">var ActiveDateDD = new String(); var ActiveDateMM = new String(); var ActiveDateYYYY = new String(); var ActiveDateClientID = new String(); ActiveDateDD = document.getElementById('" + dateActiveDDTextBox.ClientID + "'); ActiveDateMM = document.getElementById('" + dateActiveMMTextBox.ClientID + "'); ActiveDateYYYY = document.getElementById('" + dateActiveYYTextBox.ClientID + "'); ActiveDateClientID = document.getElementById('" + dateActiveTextBox.ClientID + "'); function buildDate(el, e) { var evtobj = window.event ? event : e; ActiveDateClientID.value = ActiveDateDD.value + '/' + ActiveDateMM.value + '/' + ActiveDateYYYY.value; if (ActiveDateClientID.value == '//') { ActiveDateClientID.value = ''; } switch (evtobj.keyCode) { case 16: break; case 9: break; default: if (el.id.match('" + dateActiveDDTextBox.ClientID + "') != null && el.value.length == 2) { ActiveDateMM.focus(); } if (el.id.match('" + dateActiveMMTextBox.ClientID + "') != null && el.value.length == 2) { ActiveDateYYYY.focus(); } break; } }";
                if (!Page.IsPostBack)
                {
                    JavaScript += "var DOB = ActiveDateClientID.value;if (DOB.length >= 2){ActiveDateDD.value = DOB.substr(0,2);}if (DOB.length >= 5){ActiveDateMM.value = DOB.substr(3,2);}if (DOB.length >= 10){ActiveDateYYYY.value = DOB.substr(6,4);}ActiveDateClientID.value = DOB.substr(0,10)";
                }
                Page.ClientScript.RegisterStartupScript(base.GetType(), "buildDate", JavaScript + "</script>");
            }
        }

        protected void UpdateCancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FooBarDashboard.aspx");
        }

        protected void UpdateButton_Click(object sender, EventArgs e)
        {
            firstNameTextBox = (TextBox)CustomerFormView.FindControl("FirstNameTextBox");
            lastNameTextBox = (TextBox)CustomerFormView.FindControl("LastNameTextBox");
            previouslyOrderedCheckbox = (CheckBox)CustomerFormView.FindControl("PreviouslyOrderedCheckbox");
            webCustomerCheckBox = (CheckBox)CustomerFormView.FindControl("WebCustomerCheckBox");
            dateActiveTextBox = (TextBox)CustomerFormView.FindControl("DateActiveTextBox");
            DateTime dateActive = DateTime.Parse(dateActiveTextBox.Text, System.Globalization.CultureInfo.GetCultureInfo("en-GB"));
            string fullName = firstNameTextBox.Text.ToUpper().Trim() + lastNameTextBox.Text.ToUpper().Trim();
            bool isPalindrome = fullName.SequenceEqual(fullName.Reverse());
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FooBarCustomerDatabase"].ConnectionString))
            {
                using (SqlCommand updateCustomerCommand = new SqlCommand("UpdateCustomerByCustomerId", conn))
                {
                    updateCustomerCommand.CommandType = CommandType.StoredProcedure;
                    updateCustomerCommand.Parameters.AddWithValue("customerID", Request.QueryString["CustomerID"].ToString());
                    updateCustomerCommand.Parameters.AddWithValue("firstName", firstNameTextBox.Text);
                    updateCustomerCommand.Parameters.AddWithValue("lastName", lastNameTextBox.Text);
                    updateCustomerCommand.Parameters.AddWithValue("previouslyOrdered", previouslyOrderedCheckbox.Checked.ToString());
                    updateCustomerCommand.Parameters.AddWithValue("isWebCustomer", webCustomerCheckBox.Checked.ToString());
                    updateCustomerCommand.Parameters.AddWithValue("activeDate", dateActive);
                    updateCustomerCommand.Parameters.AddWithValue("isPalindrome", isPalindrome.ToString());
                    conn.Open();
                    updateCustomerCommand.ExecuteNonQuery();
                    conn.Close();
                }
            }
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FooBarCustomerDatabase"].ConnectionString))
            {
                using (SqlCommand removefavouriteCommand = new SqlCommand("RemoveFavouriteColoursByCustomerID", conn))
                {
                    removefavouriteCommand.CommandType = CommandType.StoredProcedure;
                    removefavouriteCommand.Parameters.AddWithValue("customerID", Request.QueryString["CustomerID"].ToString());
                    conn.Open();
                    removefavouriteCommand.ExecuteNonQuery();
                    conn.Close();
                }
            }

            favouriteColoursCheckBoxList = (CheckBoxList)CustomerFormView.FindControl("FavouriteColoursCheckBoxList");
            foreach(ListItem listItem in favouriteColoursCheckBoxList.Items)
            {
                if(listItem.Selected)
                {
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FooBarCustomerDatabase"].ConnectionString))
                    {
                        using (SqlCommand insertFavouriteCommand = new SqlCommand("InsertFavouriteColour", conn))
                        {
                            insertFavouriteCommand.CommandType = CommandType.StoredProcedure;
                            insertFavouriteCommand.Parameters.AddWithValue("customerID", Request.QueryString["CustomerID"].ToString());
                            insertFavouriteCommand.Parameters.AddWithValue("colourID", listItem.Value);
                            conn.Open();
                            insertFavouriteCommand.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                }
            }
            Response.Redirect("~/FooBarDashboard.aspx");
        }

        protected void InsertCancelButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/FooBarDashboard.aspx");
        }

        protected void InsertButton_Click(object sender, EventArgs e)
        {
            firstNameTextBox = (TextBox)CustomerFormView.FindControl("FirstNameTextBox");
            lastNameTextBox = (TextBox)CustomerFormView.FindControl("LastNameTextBox");
            previouslyOrderedCheckbox = (CheckBox)CustomerFormView.FindControl("PreviouslyOrderedCheckbox");
            webCustomerCheckBox = (CheckBox)CustomerFormView.FindControl("WebCustomerCheckBox");
            dateActiveTextBox = (TextBox)CustomerFormView.FindControl("DateActiveTextBox");
            DateTime dateActive = DateTime.Parse(dateActiveTextBox.Text, System.Globalization.CultureInfo.GetCultureInfo("en-GB"));
            string fullName = firstNameTextBox.Text.ToUpper().Trim() + lastNameTextBox.Text.ToUpper().Trim();
            bool isPalindrome = fullName.SequenceEqual(fullName.Reverse());

            int CustomerID = 0;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FooBarCustomerDatabase"].ConnectionString))
            {
                using (SqlCommand insertCustomer = new SqlCommand("InsertCustomer", conn))
                {
                    insertCustomer.CommandType = CommandType.StoredProcedure;
                    insertCustomer.Parameters.Add("@customerID", SqlDbType.Int).Direction = ParameterDirection.Output;
                    insertCustomer.Parameters.AddWithValue("firstName", firstNameTextBox.Text);
                    insertCustomer.Parameters.AddWithValue("lastName", lastNameTextBox.Text);
                    insertCustomer.Parameters.AddWithValue("previouslyOrdered", previouslyOrderedCheckbox.Checked.ToString());
                    insertCustomer.Parameters.AddWithValue("isWebCustomer", webCustomerCheckBox.Checked.ToString());
                    insertCustomer.Parameters.AddWithValue("dateActive", dateActive);
                    insertCustomer.Parameters.AddWithValue("isPalindrome",isPalindrome.ToString());
                    conn.Open();
                    insertCustomer.ExecuteScalar();
                    CustomerID = Convert.ToInt32(insertCustomer.Parameters["@customerID"].Value);
                    conn.Close();
                }
            }

            favouriteColoursCheckBoxList = (CheckBoxList)CustomerFormView.FindControl("FavouriteColoursCheckBoxList");
            foreach (ListItem listItem in favouriteColoursCheckBoxList.Items)
            {
                if (listItem.Selected)
                {
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["FooBarCustomerDatabase"].ConnectionString))
                    {
                        using (SqlCommand insertFavouriteCommand = new SqlCommand("InsertFavouriteColour", conn))
                        {
                            insertFavouriteCommand.CommandType = CommandType.StoredProcedure;
                            insertFavouriteCommand.Parameters.Add("customerID", SqlDbType.Int).Value = CustomerID;
                            insertFavouriteCommand.Parameters.Add("colourID", SqlDbType.Int).Value = listItem.Value;
                            conn.Open();
                            insertFavouriteCommand.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                }
            }
            Response.Redirect("~/FooBarDashboard.aspx");
        }
    }
}