<%@ Page Title="営業報告登録" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ActionEdit.aspx.cs" Inherits="customer_action.ActionEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:customer_actionConnectionString %>" SelectCommand="SELECT tbl_customer.customer_name, tbl_action.ID, tbl_action.action_date, tbl_action.action_content, tbl_action.action_staffID, tbl_company.company_name, tbl_action.customerID FROM tbl_customer INNER JOIN tbl_action ON tbl_customer.customerID = tbl_action.customerID LEFT OUTER JOIN tbl_company ON tbl_customer.companyID = tbl_company.companyID WHERE id=@id" UpdateCommand="UPDATE [tbl_action] SET [customerID] = @customerID, [action_date] = @action_date, [action_content] = @action_content, [action_staffID] = @action_staffID WHERE [ID] = @ID">
        <SelectParameters>
            <asp:QueryStringParameter Name="id" QueryStringField="ID" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="customerID" />
            <asp:Parameter Name="action_date" />
            <asp:Parameter Name="action_content" />
            <asp:Parameter Name="action_staffID" />
            <asp:Parameter Name="ID" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:customer_actionConnectionString %>" SelectCommand="SELECT * FROM [tbl_staff]"></asp:SqlDataSource>
    <asp:FormView ID="FormView1" runat="server" DataKeyNames="ID" DataSourceID="SqlDataSource1" DefaultMode="Edit" OnItemUpdated="FormView1_ItemUpdated">
        <EditItemTemplate>
            <table border="1" bordercolordark="#000000" bordercolorlight="#000000" 
                style="width: 600px; border-collapse: collapse">
                <tr>
                    <td bgcolor="#deb887" width="90">
                        ID：</td>
                    <td style="width: 100px">
                        <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#deb887" width="90">
                        顧客名：</td>
                    <td style="width: 100px">
                        <asp:Label ID="CustomerNameLabel" runat="server" Text='<%# Eval("customer_name") %>' 
                            Width="368px"></asp:Label>
                        <asp:Label ID="CustomerIDLabel" runat="server" Text='<%# Bind("customerID") %>' 
                            Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#deb887" width="90">
                        会社名：</td>
                    <td style="width: 100px">
                        <asp:Label ID="ComapnyNameLabel" runat="server" Text='<%# Eval("company_name") %>' 
                            Width="368px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#deb887" width="90">
                        日付：</td>
                    <td style="width: 100px">
                        <asp:TextBox ID="Action_DateTextBox" runat="server" CssClass="ImeOff" 
                            Text='<%# Bind("action_date","{0:yyyy/MM/dd}") %>' Width="85px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Action_DateTextBox" Display="Dynamic" ErrorMessage="必須入力です" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="Action_DateTextBox" ErrorMessage="「yyyy/mm/dd」の書式で入力してください" ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate" Display="Dynamic"></asp:CustomValidator>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#deb887" width="90">
                        内容：</td>
                    <td style="width: 100px">
                        <asp:TextBox ID="Action_ContentTextBox" runat="server" CssClass="ImeOn" 
                            Height="80px" Text='<%# Bind("action_content") %>' TextMode="MultiLine" 
                            Width="489px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#deb887" width="90">
                        対応者：</td>
                    <td style="width: 100px">
                        <asp:DropDownList ID="Action_SatddIDDropDownList" runat="server" AppendDataBoundItems="True" 
                            DataMember="DefaultView" DataSourceID="SqlDataSource2" 
                            DataTextField="staff_name" DataValueField="staffID" 
                            SelectedValue='<%# Bind("action_staffID") %>'>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="登録" />
            <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="キャンセル" OnClick="CancelButton_Click" />
            &nbsp;
           </EditItemTemplate>
        <InsertItemTemplate>
            customer_name:
            <asp:TextBox ID="customer_nameTextBox" runat="server" Text='<%# Bind("customer_name") %>' />
            <br />
            ID:
            <asp:TextBox ID="IDTextBox" runat="server" Text='<%# Bind("ID") %>' />
            <br />
            action_date:
            <asp:TextBox ID="action_dateTextBox" runat="server" Text='<%# Bind("action_date") %>' />
            <br />
            action_content:
            <asp:TextBox ID="action_contentTextBox" runat="server" Text='<%# Bind("action_content") %>' />
            <br />
            action_staffID:
            <asp:TextBox ID="action_staffIDTextBox" runat="server" Text='<%# Bind("action_staffID") %>' />
            <br />
            company_name:
            <asp:TextBox ID="company_nameTextBox" runat="server" Text='<%# Bind("company_name") %>' />
            <br />
            customerID:
            <asp:TextBox ID="customerIDTextBox" runat="server" Text='<%# Bind("customerID") %>' />
            <br />
            <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="挿入" />
            &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="キャンセル" />
        </InsertItemTemplate>
        <ItemTemplate>
            customer_name:
            <asp:Label ID="customer_nameLabel" runat="server" Text='<%# Bind("customer_name") %>' />
            <br />
            ID:
            <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
            <br />
            action_date:
            <asp:Label ID="action_dateLabel" runat="server" Text='<%# Bind("action_date") %>' />
            <br />
            action_content:
            <asp:Label ID="action_contentLabel" runat="server" Text='<%# Bind("action_content") %>' />
            <br />
            action_staffID:
            <asp:Label ID="action_staffIDLabel" runat="server" Text='<%# Bind("action_staffID") %>' />
            <br />
            company_name:
            <asp:Label ID="company_nameLabel" runat="server" Text='<%# Bind("company_name") %>' />
            <br />
            customerID:
            <asp:Label ID="customerIDLabel" runat="server" Text='<%# Bind("customerID") %>' />
            <br />
            <asp:LinkButton ID="EditButton" runat="server" CausesValidation="False" CommandName="Edit" Text="編集" />
        </ItemTemplate>
    </asp:FormView>
</asp:Content>
