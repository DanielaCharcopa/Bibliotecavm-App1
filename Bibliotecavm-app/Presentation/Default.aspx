<%@ Page Title="Iniciar Sesión" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Presentation.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-container">
        <h2>Iniciar Sesión</h2>
        <asp:Label ID="LblMsg" runat="server" CssClass="error-message" />
        
       <div class="form-group">
    <label for="TBCorreo">Correo:</label>
    <asp:TextBox ID="TBCorreo" runat="server" CssClass="form-control" placeholder="Ingrese su correo" required />
</div>

<div class="form-group">
    <label for="TBContrasena">Contraseña:</label>
    <asp:TextBox ID="TBContrasena" runat="server" CssClass="form-control" TextMode="Password" placeholder="Ingrese su contraseña" required />
</div>
        
        <asp:Button ID="BtGuardar" runat="server" CssClass="btn" Text="Iniciar Sesión" OnClick="BtGuardar_Click" />
        
        <div class="form-links">
            <a href="WFUserRegistration.aspx">Registrate</a>
        </div>
    </div>
</asp:Content>