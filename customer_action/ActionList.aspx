<%@ Page Title="営業報告一覧" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ActionList.aspx.cs" Inherits="customer_action.ActionList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            width: 47px;
        }
        .auto-style3 {
            width: 100px;
        }
        .auto-style4 {
            width: 18px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:customer_actionConnectionString %>" SelectCommand="SELECT tbl_action.action_date, tbl_staff.staff_name, tbl_customer.customer_name, tbl_company.company_name, tbl_action.action_content FROM tbl_action LEFT OUTER JOIN tbl_customer ON tbl_action.customerID = tbl_customer.customerID LEFT OUTER JOIN tbl_staff ON tbl_action.action_staffID = tbl_staff.staffID LEFT OUTER JOIN tbl_company ON tbl_customer.companyID = tbl_company.companyID where action_date between @start_date and @end_date ORDER BY tbl_action.action_date">
        <SelectParameters>
            <asp:ControlParameter ControlID="StartDateTextBox" Name="start_date" PropertyName="Text" />
            <asp:ControlParameter ControlID="EndDateTextBox" Name="end_date" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>
    <table class="auto-style1">
        <tr>
            <td class="auto-style2">期間：</td>
            <td class="auto-style3">
                <asp:TextBox ID="StartDateTextBox" runat="server" CssClass="imeOff" Width="92px"></asp:TextBox>
            </td>
            <td class="auto-style4">～</td>
            <td class="auto-style3">
                <asp:TextBox ID="EndDateTextBox" runat="server" CssClass="imeOff" Width="92px"></asp:TextBox>
            </td>
            <td>
                <asp:Button ID="FilterButton" runat="server" Text="フィルター実行" Width="120px" />
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" DataSourceID="SqlDataSource1" ForeColor="Black" GridLines="None">
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
        <Columns>
            <asp:BoundField DataField="action_date" DataFormatString="{0:yyyy/MM/dd}" HeaderText="日付" SortExpression="action_date">
            <ItemStyle Width="70px" />
            </asp:BoundField>
            <asp:BoundField DataField="staff_name" HeaderText="対応者" SortExpression="staff_name">
            <ItemStyle Width="60px" />
            </asp:BoundField>
            <asp:BoundField DataField="customer_name" HeaderText="顧客名" SortExpression="customer_name">
            <ItemStyle Width="120px" />
            </asp:BoundField>
            <asp:BoundField DataField="company_name" HeaderText="会社名" SortExpression="company_name">
            <ItemStyle Width="160px" />
            </asp:BoundField>
            <asp:BoundField DataField="action_content" HeaderText="対応内容" SortExpression="action_content">
            <ItemStyle Width="300px" />
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
