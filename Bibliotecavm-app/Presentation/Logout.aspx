<%@ Page Title="Cerrar Sesión" Language="C#" MasterPageFile="~/MainUsuario.Master" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="Presentation.Logout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="logout-container">
        <h2>Cerrar Sesión</h2>
        <div class="logout-message">
            <p>Cerrando sesión...</p>
            <p>Por favor, espere un momento.</p>
        </div>
    </div>
</asp:Content>