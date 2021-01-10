<%@ Page Title="会社マスター管理" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CompanyManage.aspx.cs" Inherits="customer_action.CompanyManage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:customer_actionConnectionString %>" DeleteCommand="DELETE FROM [tbl_company] WHERE [companyID] = @companyID" InsertCommand="INSERT INTO [tbl_company] ([companyID], [company_name], [company_kana], [delete_flag]) VALUES (@companyID, @company_name, @company_kana, @delete_flag)" SelectCommand="SELECT * FROM [tbl_company]" UpdateCommand="UPDATE [tbl_company] SET [company_name] = @company_name, [company_kana] = @company_kana, [delete_flag] = @delete_flag WHERE [companyID] = @companyID">
        <DeleteParameters>
            <asp:Parameter Name="companyID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="companyID" Type="Int32" />
            <asp:Parameter Name="company_name" Type="String" />
            <asp:Parameter Name="company_kana" Type="String" />
            <asp:Parameter Name="delete_flag" Type="Boolean" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="company_name" Type="String" />
            <asp:Parameter Name="company_kana" Type="String" />
            <asp:Parameter Name="delete_flag" Type="Boolean" />
            <asp:Parameter Name="companyID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:Button ID="InsertButton" runat="server" OnClick="InsertButton_Click" Text="会社の追加" />
    <asp:Label ID="MessageLabel" runat="server" EnableViewState="False" ForeColor="Red"></asp:Label>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" DataKeyNames="companyID" DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="None" OnRowUpdating="GridView1_RowUpdating">
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
        <Columns>
            <asp:CommandField ButtonType="Button" ShowEditButton="True" />
            <asp:BoundField DataField="companyID" HeaderText="会社ID" ReadOnly="True" SortExpression="companyID">
            <ItemStyle HorizontalAlign="Center" Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="company_name" HeaderText="会社名" SortExpression="company_name">
            <ControlStyle Width="180px" />
            <ItemStyle Width="200px" />
            </asp:BoundField>
            <asp:BoundField DataField="company_kana" HeaderText="会社名カナ" SortExpression="company_kana">
            <ControlStyle Width="180px" />
            <ItemStyle Width="200px" />
            </asp:BoundField>
        </Columns>
        <FooterStyle BackColor="Tan" />
        <HeaderStyle BackColor="Tan" Font-Bold="True" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <SortedAscendingCellStyle BackColor="#FAFAE7" />
        <SortedAscendingHeaderStyle BackColor="#DAC09E" />
        <SortedDescendingCellStyle BackColor="#E1DB9C" />
        <SortedDescendingHeaderStyle BackColor="#C2A47B" />
    </asp:GridView>
</asp:Content>
