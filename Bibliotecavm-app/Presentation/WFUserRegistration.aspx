<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFUserRegistration.aspx.cs" Inherits="Presentation.WFUserRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="form-container">
        <h2>Registro de Usuarios</h2>
        <label for="TBFirstName">Nombre</label>
        <asp:TextBox ID="TBFirstName" runat="server"></asp:TextBox>

        <label for="TBLastName">Apellido</label>
        <asp:TextBox ID="TBLastName" runat="server"></asp:TextBox>

        <label for="TBEmail">Correo Electrónico</label>
        <asp:TextBox ID="TBEmail" runat="server"></asp:TextBox>

        <label for="TBPassword">Contraseña</label>
        <asp:TextBox ID="TBPassword" runat="server" TextMode="Password"></asp:TextBox>

        <label for="TBSalt">Salt</label>
        <asp:TextBox ID="TBSalt" runat="server"></asp:TextBox>

        <label for="DDLRole">Rol</label>
     <asp:DropDownList ID="DDLRole" runat="server">
         <asp:ListItem Text="Seleccione un rol" Value="" Selected="True"></asp:ListItem>
         <asp:ListItem Text="Administrador" Value="Administrador"></asp:ListItem>
         <asp:ListItem Text="Docente" Value="Docente"></asp:ListItem>
         <asp:ListItem Text="Estudiante" Value="Estudiante"></asp:ListItem>
     </asp:DropDownList>

     <asp:DropDownList ID="DDLEducationLevel" runat="server">
         <asp:ListItem Text="Seleccione su nivel educativo" Value="" Selected="True"></asp:ListItem>
         <asp:ListItem Text="Primaria" Value="Primaria"></asp:ListItem>
         <asp:ListItem Text="Secundaria" Value="Secundaria"></asp:ListItem>
         <asp:ListItem Text="Bachillerato" Value="Bachillerato"></asp:ListItem>
         <asp:ListItem Text="Técnico" Value="Técnico"></asp:ListItem>
         <asp:ListItem Text="Tecnólogo" Value="Tecnólogo"></asp:ListItem>
         <asp:ListItem Text="Pregrado" Value="Pregrado"></asp:ListItem>
         <asp:ListItem Text="Especialización" Value="Especialización"></asp:ListItem>
         <asp:ListItem Text="Maestría" Value="Maestría"></asp:ListItem>
         <asp:ListItem Text="Doctorado" Value="Doctorado"></asp:ListItem>
         <asp:ListItem Text="Postdoctorado" Value="Postdoctorado"></asp:ListItem>
     </asp:DropDownList>


     <button runat="server" onserverclick="BtnSave_Click">Guardar</button>

     <asp:Label ID="LblMessage" runat="server" CssClass="message"></asp:Label>
 </div>
</asp:Content>
