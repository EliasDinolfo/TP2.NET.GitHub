<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UI.Web.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <asp:Panel ID="panelBody" runat="server">
        <asp:Label ID="lblBienvenida" Text="Bienvenido " runat="server"></asp:Label>
        <br />
        <asp:Label ID="lblIDPersona" runat="server" Text="ID: "></asp:Label>
    </asp:Panel>
</asp:Content>
