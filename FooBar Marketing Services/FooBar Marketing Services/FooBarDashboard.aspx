<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FooBarDashboard.aspx.cs" Inherits="FooBar_Marketing_Services.FooBarDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FooBar Marketing Dashboard</title>
    <link href="FooBarDashboardStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <h1>FooBar Marketing Dashboard</h1>
        <asp:SqlDataSource runat="server" ID="CustomerSearchDataSource" ConnectionString="<%$ ConnectionStrings:FooBarCustomerDatabase %>" SelectCommand="dbo.SelectTop20Customers" >
        </asp:SqlDataSource>
        <div id="CustomerSearchList" runat="server" >
            <asp:ListView ID="CustomerSearchListView" runat="server" GroupItemCount="1" GroupPlaceholderID="IDRowContainer"
                ItemPlaceholderID="IDItemContainer" DataSourceID="CustomerSearchDataSource" DataKeyNames="CustomerId"
                OnDataBound="CustomerSearchListView_DataBound" >
            <LayoutTemplate>
                <table class="gridView" width="auto" border="1" cellspacing="0" cellpadding="3px">
                    <thead>
                        <tr class="tableHeaderRow" style="text-align: center">
                            <td>
                                Name
                            </td>
                            <td>
                                Previously Ordered
                            </td>
                            <td>
                                Web Customer
                            </td>
                            <td>
                                Date Active
                            </td>
                            <td>
                                Is Palindrome
                            </td>
                            <td>
                                Favourite colours
                            </td>
                        </tr>
                    </thead>
                    <tbody>
                        <tr runat="server" id="IDRowContainer" />
                    </tbody>
                </table>
            </LayoutTemplate>
            <GroupTemplate>
                <tr>
                    <td runat="server" id="IDItemContainer" />
                </tr>
            </GroupTemplate>
            <ItemTemplate>
                <tr class="rowStyle">
                    <td>
                        <asp:HyperLink runat="server" ID="EditCustomerHyperLink">
                            <asp:Label ID="CustomerIDLabel" runat="server" Visible="false" Text='<%#Eval("CustomerId")%>'></asp:Label><asp:Label ID="FirstNameLabel" runat="server" Text='<%# Eval("FirstName")%>' />&nbsp;<asp:Label ID="LastNameLabel" runat="server" Text='<%# Eval("LastName")%>' />
                        </asp:HyperLink>
                    </td>
                    <td>
                        <asp:Label ID="PreviouslyOrderedLabel" runat="server" Text='<%#Eval("PreviouslyOrdered")%>' />
                    </td>
                    <td>
                        <asp:Label ID="IsWebCustomerLabel" runat="server" Text='<%#Eval("IsWebCustomer")%>' />
                    </td>
                    <td>
                        <asp:Label ID="DateActiveLabel" runat="server" Text='<%#Eval("DateActive", "{0:dd/MM/yyyy}")%>' />
                    </td>
                    <td>
                        <asp:Label ID="IsPalindromeLabel" runat="server" Text='<%#Eval("IsPalindrome")%>' />
                    </td>
                    <td>
                        <asp:Label ID="FavouriteColorsLabel" runat="server"/>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr class="altRowStyle">
                    <td>
                        <asp:HyperLink runat="server" ID="EditCustomerHyperLink">
                            <asp:Label ID="CustomerIDLabel" runat="server" Visible="false" Text='<%#Eval("CustomerId")%>'></asp:Label><asp:Label ID="FirstNameLabel" runat="server" Text='<%# Eval("FirstName")%>' />&nbsp;<asp:Label ID="LastNameLabel" runat="server" Text='<%# Eval("LastName")%>' />
                        </asp:HyperLink>
                    </td>
                    <td>
                        <asp:Label ID="PreviouslyOrderedLabel" runat="server" Text='<%#Eval("PreviouslyOrdered")%>'/>
                    </td>
                    <td>
                        <asp:Label ID="IsWebCustomerLabel" runat="server" Text='<%#Eval("IsWebCustomer")%>' />
                    </td>
                    <td>
                        <asp:Label ID="DateActiveLabel" runat="server" Text='<%#Eval("DateActive", "{0:dd/MM/yyyy}")%>' />
                    </td>
                    <td>
                        <asp:Label ID="IsPalindromeLabel" runat="server" Text='<%#Eval("IsPalindrome")%>' />
                    </td>
                    <td>
                        <asp:Label ID="FavouriteColorsLabel" runat="server"/>
                    </td>
            </AlternatingItemTemplate>
            <EmptyDataTemplate>
                No companies were found using the search criteria
            </EmptyDataTemplate>
        </asp:ListView>
        </div>
        &nbsp;
        <div>
            <asp:Button runat="server" ID="NewCustomerButton" CssClass="standardButton" Text="New Customer" OnClick="NewCustomerButton_Click" />
        </div>
    </form>
</body>
</html>