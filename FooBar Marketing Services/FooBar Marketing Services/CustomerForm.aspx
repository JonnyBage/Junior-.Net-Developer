<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CustomerForm.aspx.cs" Inherits="FooBar_Marketing_Services.CustomerForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FooBar Marketing Customer Form</title>
    <link href="FooBarDashboardStyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function capitalise(string) {
            return string.toUpperCase();
        }
        function capitaliseSurname(string) {
            string = removeSpaces(string);
            return string.charAt(0).toUpperCase() + string.substring(1).toLowerCase();
        }
        function capitaliseFirstName(string) {
            var words = string.split(" ");
            for (var i = 0; i < words.length; i++) {
                var name = words[i];
                words[i] = name.charAt(0).toUpperCase() + name.substring(1).toLowerCase();
            }
            return words.join(" ");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Customer Form</h1>
            <asp:SqlDataSource runat="server" ID="CustomerDataSource" ConnectionString="<%$ ConnectionStrings:FooBarCustomerDatabase %>">
            </asp:SqlDataSource>
            <asp:FormView ID="CustomerFormView" runat="server" DataKeyNames="CustomerId" DataSourceID="CustomerDataSource"
                EnableModelValidation="True">
                <EditItemTemplate>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Please check the following"
                        CssClass="textDivRed" />
                    <table class="customerTable" style="width: 100%">
                        <tr>
                            <td class="customerHeaderRow" style="width: 200px">First Name:
                            </td>
                            <td style="width: 300px">
                                <asp:TextBox ID="FirstNameTextBox" runat="server" Width="190px" CssClass="aspTextBox"
                                    OnBlur="value=capitaliseFirstName(value);" MaxLength="20" Text='<%# Bind("FirstName") %>'  TabIndex="1"/>
                                <asp:RequiredFieldValidator runat="server" ID="FirstNameValidator" ControlToValidate="FirstNameTextBox"
                                    Text="*" ErrorMessage="First Name"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="customerHeaderRow">Last Name:
                            </td>
                            <td>
                                <asp:TextBox ID="LastNameTextBox" runat="server" Text='<%# Bind("LastName") %>'
                                    CssClass="aspTextBox" OnBlur="value=capitaliseSurname(value);" MaxLength="20"
                                    Width="190px" TabIndex="2"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="LastNameTextBoxValidator" ControlToValidate="LastNameTextBox"
                                    Text="*" ErrorMessage="Last Name"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="customerHeaderRow">Previously Ordered:
                            </td>
                            <td>
                                <asp:CheckBox ID="PreviouslyOrderedCheckbox" runat="server"  TabIndex="3" Checked='<%# Bind("PreviouslyOrdered") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td class="customerHeaderRow">Web Customer:
                            </td>
                            <td>
                                <asp:CheckBox ID="WebCustomerCheckBox" runat="server"  TabIndex="4" Checked='<%# Bind("IsWebCustomer") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td>Date Active:
                            </td>
                            <td>
                                <asp:TextBox runat="server" CssClass="aspTextBox" ID="DateActiveDDTextBox" Text="" onKeyUp="javascript:buildDate(this,event)"
                                    MaxLength="2" Width="15px" TabIndex="5"></asp:TextBox>
                                /
                                    <asp:TextBox runat="server" CssClass="aspTextBox" ID="DateActiveMMTextBox" Text="" onKeyUp="javascript:buildDate(this,event)"
                                        MaxLength="2" Width="15px" TabIndex="6"></asp:TextBox>
                                /
                                    <asp:TextBox runat="server" CssClass="aspTextBox" ID="DateActiveYYTextBox" Text="" onKeyUp="javascript:buildDate(this,event)"
                                        MaxLength="4" Width="30px" TabIndex="7"></asp:TextBox>
                                <span class="footnoteText">(dd/mm/yyyy)</span>
                                <asp:CompareValidator ID="DateActiveTextBoxValidator2" runat="server" ControlToValidate="DateActiveTextBox"
                                    ErrorMessage="A valid date for Date Active: dd/mm/yyyy" Text="*" Operator="DataTypeCheck"
                                    Type="Date" />
                                <asp:RequiredFieldValidator ID="DateActiveTextBoxValidator" runat="server" ControlToValidate="DateActiveTextBox"
                                    ErrorMessage="Date Active in format dd/mm/yyyy" Text="*"></asp:RequiredFieldValidator>
                                <div style="visibility: collapse; display: none;">
                                    <asp:TextBox runat="server" ID="DateActiveTextBox" MaxLength="10" Text='<%# Bind("DateActive")%>'></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="customerHeaderRow">Favourite Colour:
                            </td>
                            <td>
                                <asp:CheckBoxList runat="server" ID="FavouriteColoursCheckBoxList" AutoPostBack="false"  TabIndex="8">
                                    <asp:ListItem Text="Red" Value="1">Red</asp:ListItem>
                                    <asp:ListItem Text="Green" Value="2">Green</asp:ListItem>
                                    <asp:ListItem Text="Blue" Value="3">Blue</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2" style="text-align: center">
                                <asp:Button ID="UpdateButton" runat="server" CssClass="standardButton" CausesValidation="True"
                                    Text="Update" OnClick="UpdateButton_Click" />
                                &nbsp;<asp:Button ID="UpdateCancelButton" runat="server" CssClass="standardButton"
                                    CausesValidation="False" CommandName="Cancel" Text="Cancel" OnClick="UpdateCancelButton_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Please check the following"
                        CssClass="textDivRed" />
                    <table class="customerTable" style="width: 100%">
                        <tr>
                            <td class="customerHeaderRow" style="width: 200px">First Name:
                            </td>
                            <td style="width: 300px">
                                <asp:TextBox ID="FirstNameTextBox" runat="server" Width="190px" CssClass="aspTextBox"
                                    OnBlur="value=capitaliseFirstName(value);" MaxLength="20" Text='<%# Bind("FirstName") %>'  TabIndex="1"/>
                                <asp:RequiredFieldValidator runat="server" ID="FirstNameValidator" ControlToValidate="FirstNameTextBox"
                                    Text="*" ErrorMessage="First Name"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="customerHeaderRow">Last Name:
                            </td>
                            <td>
                                <asp:TextBox ID="LastNameTextBox" runat="server" Text='<%# Bind("LastName") %>'
                                    CssClass="aspTextBox" OnBlur="value=capitaliseSurname(value);" MaxLength="20"
                                    Width="190px" TabIndex="2"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ID="LastNameTextBoxValidator" ControlToValidate="LastNameTextBox"
                                    Text="*" ErrorMessage="Last Name"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="customerHeaderRow">Previously Ordered:
                            </td>
                            <td>
                                <asp:CheckBox ID="PreviouslyOrderedCheckbox" runat="server"  TabIndex="3" Checked='<%# Bind("PreviouslyOrdered") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td class="customerHeaderRow">Web Customer:
                            </td>
                            <td>
                                <asp:CheckBox ID="WebCustomerCheckBox" runat="server"  TabIndex="4" Checked='<%# Bind("IsWebCustomer") %>' />
                            </td>
                        </tr>
                        <tr>
                            <td>Date Active:
                            </td>
                            <td>
                                <asp:TextBox runat="server" CssClass="aspTextBox" ID="DateActiveDDTextBox" Text="" onKeyUp="javascript:buildDate(this,event)"
                                    MaxLength="2" Width="15px" TabIndex="5"></asp:TextBox>
                                /
                                    <asp:TextBox runat="server" CssClass="aspTextBox" ID="DateActiveMMTextBox" Text="" onKeyUp="javascript:buildDate(this,event)"
                                        MaxLength="2" Width="15px" TabIndex="6"></asp:TextBox>
                                /
                                    <asp:TextBox runat="server" CssClass="aspTextBox" ID="DateActiveYYTextBox" Text="" onKeyUp="javascript:buildDate(this,event)"
                                        MaxLength="4" Width="30px" TabIndex="7"></asp:TextBox>
                                <span class="footnoteText">(dd/mm/yyyy)</span>
                                <asp:CompareValidator ID="DateActiveTextBoxValidator2" runat="server" ControlToValidate="DateActiveTextBox"
                                    ErrorMessage="A valid date for Date Active: dd/mm/yyyy" Text="*" Operator="DataTypeCheck"
                                    Type="Date" />
                                <asp:RequiredFieldValidator ID="DateActiveTextBoxValidator" runat="server" ControlToValidate="DateActiveTextBox"
                                    ErrorMessage="Date Active in format dd/mm/yyyy" Text="*"></asp:RequiredFieldValidator>
                                <div style="visibility: collapse; display: none;">
                                    <asp:TextBox runat="server" ID="DateActiveTextBox" MaxLength="10" Text='<%# Bind("DateActive")%>'></asp:TextBox>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="customerHeaderRow">Favourite Colour:
                            </td>
                            <td>
                                <asp:CheckBoxList runat="server" ID="FavouriteColoursCheckBoxList" AutoPostBack="false"  TabIndex="8">
                                    <asp:ListItem Text="Red" Value="1">Red</asp:ListItem>
                                    <asp:ListItem Text="Green" Value="2">Green</asp:ListItem>
                                    <asp:ListItem Text="Blue" Value="3">Blue</asp:ListItem>
                                </asp:CheckBoxList>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2" style="text-align: center">
                                <asp:Button ID="InsertButton" runat="server" CssClass="standardButton" CausesValidation="True" Text="Insert" OnClick="InsertButton_Click"/>
                                &nbsp;<asp:Button ID="InsertCancelButton" runat="server" CssClass="standardButton"
                                    CausesValidation="False" CommandName="Cancel" Text="Cancel" OnClick="InsertCancelButton_Click"/>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td></td>
                        </tr>
                    </table>
                </InsertItemTemplate>
            </asp:FormView>
        </div>
    </form>
</body>
</html>
