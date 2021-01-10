<%@ Page Title="営業担当者の置換" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="StaffReplace.aspx.cs" Inherits="customer_action.StaffReplace" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            width: 300px;
        }
        .auto-style3 {
            width: 100px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
        営業担当者の置換</p>
    <table class="auto-style2">
        <tr>
            <td class="auto-style3">変更前の担当者：</td>
            <td>
                <asp:DropDownList ID="BeforeStaffDropDownList" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource1" DataTextField="staff_name" DataValueField="staffID">
                    <asp:ListItem Value="-1">（選択してください）</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style3">変更後の担当者：</td>
            <td>
                <asp:DropDownList ID="AfterStaffDropDownList" runat="server" AppendDataBoundItems="True" DataSourceID="SqlDataSource1" DataTextField="staff_name" DataValueField="staffID">
                    <asp:ListItem Value="-1">（選択してください）</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="auto-style3">&nbsp;</td>
            <td>
                <asp:Button ID="ExecuteButton" runat="server" OnClick="ExecuteButton_Click" Text="置換処理の実行" />
            </td>
        </tr>
    </table>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:customer_actionConnectionString %>" SelectCommand="SELECT * FROM [tbl_staff]"></asp:SqlDataSource>
    <asp:Label ID="MessageLabel" runat="server" EnableViewState="False"></asp:Label>
</asp:Content>
