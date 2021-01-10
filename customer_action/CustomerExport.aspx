<%@ Page Title="顧客データのエクスポート" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CustomerExport.aspx.cs" Inherits="customer_action.CustomerExport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            width: 400px;
        }
        .auto-style3 {
            width: 100px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        顧客データのエクスポート</p>
    <table class="auto-style2">
        <tr>
            <td class="auto-style3">出力対象担当者：</td>
            <td>
                <asp:DropDownList ID="StaffDropDownList" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource1" DataTextField="staff_name" DataValueField="staffID">
                    <asp:ListItem Value="-1">（すべての担当者）</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="ExportButton" runat="server" OnClick="ExportButton_Click" Text="出力実行" />
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:customer_actionConnectionString %>" SelectCommand="SELECT * FROM [tbl_staff]"></asp:SqlDataSource>
</asp:Content>
