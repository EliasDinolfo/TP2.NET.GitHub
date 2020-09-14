<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UI.Web.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <table style="width:100%">
        <tr>
            <td ><asp:Label ID="lblUsuario" runat="server" Text="Usuario: "></asp:Label></td>
            <td><asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblClave" runat="server" Text="Clave: "></asp:Label></td>
            <td><asp:TextBox ID="txtClave" runat="server" TextMode="Password"></asp:TextBox></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td><asp:Button ID="btnIngresar" runat="server" Text="Ingresar" OnClick="btnIngresar_Click"></asp:Button></td>
        </tr>
    </table>
    <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
</asp:Content>
