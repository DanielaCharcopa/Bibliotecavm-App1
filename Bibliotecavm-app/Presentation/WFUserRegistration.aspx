<%@ Page Title="Registro de Usuarios" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFUserRegistration.aspx.cs" Inherits="Presentation.WFUserRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function validateForm() {
            // Obtener el valor del correo electrónico
            var email = document.getElementById("<%= TBEmail.ClientID %>").value;

            // Expresión regular para validar correos de Gmail
            var regex = /^[a-zA-Z0-9._%+-]+@gmail\.com$/;

            // Validar el correo
            if (!regex.test(email)) {
                alert("Por favor, ingrese un correo electrónico válido de Gmail (ejemplo@gmail.com).");
                return false; // Evita que el formulario se envíe
            }

            // Si el correo es válido, permitir el envío del formulario
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-container">
        <h2>Registro de Usuarios</h2>
        
        <!-- Mensaje de retroalimentación -->
        <asp:Label ID="LblMessage" runat="server" CssClass="message"></asp:Label>

        <!-- Campos del formulario -->
        <div>
            <label for="TBFirstName">Nombre:</label>
            <asp:TextBox ID="TBFirstName" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:Label ID="LblNombreMessage" runat="server" CssClass="error-message" Visible="false" Text="El campo 'Nombre' es obligatorio."></asp:Label>
        </div>
        <div>
            <label for="TBLastName">Apellido:</label>
            <asp:TextBox ID="TBLastName" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:Label ID="LblApellidoMessage" runat="server" CssClass="error-message" Visible="false" Text="El campo 'Apellido' es obligatorio."></asp:Label>
        </div>
        <div>
            <label for="TBEmail">Correo Electrónico:</label>
            <asp:TextBox ID="TBEmail" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:Label ID="LblCorreoMessage" runat="server" CssClass="error-message" Visible="false" Text="Por favor, ingrese un correo electrónico válido de Gmail (ejemplo@gmail.com)."></asp:Label>
        </div>
        <div>
            <label for="TBPassword">Contraseña:</label>
            <asp:TextBox ID="TBPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
            <asp:Label ID="LblPasswordMessage" runat="server" CssClass="error-message" Visible="false" Text="Por favor ingrese la contraseña del usuario."></asp:Label>
        </div>
              <div>
    <label for="DDLRole">Rol:</label>
    <asp:DropDownList ID="DDLRole" runat="server" CssClass="form-control">
        <asp:ListItem Text="Seleccione un rol" Value="" />   <%-- La opción "Administrador" se agregará dinámicamente en el código C# --%>
        <asp:ListItem Text="Docente" Value="Docente" />
        <asp:ListItem Text="Estudiante" Value="Estudiante" />
    </asp:DropDownList>
</div>
        <div>
            <label for="DDLEducationLevel">Nivel Educativo:</label>
            <asp:DropDownList ID="DDLEducationLevel" runat="server" CssClass="form-control">
                <asp:ListItem Text="Seleccione un nivel" Value="" />
                <asp:ListItem Text="Primaria" Value="Primaria" />
                <asp:ListItem Text="Secundaria" Value="Secundaria" />
                <asp:ListItem Text="Bachillerato" Value="Bachillerato" />
                <asp:ListItem Text="Técnico" Value="Técnico" />
                <asp:ListItem Text="Tecnólogo" Value="Tecnólogo" />
                <asp:ListItem Text="Pregrado" Value="Pregrado" />
                <asp:ListItem Text="Especialización" Value="Especialización" />
                <asp:ListItem Text="Maestría" Value="Maestría" />
                <asp:ListItem Text="Doctorado" Value="Doctorado" />
                <asp:ListItem Text="Postdoctorado" Value="Postdoctorado" />
            </asp:DropDownList>
        </div>

        <!-- Botón de Guardar -->
        <div style="margin-top: 20px;">
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClientClick="return validateForm();" OnClick="BtnSave_Click" CssClass="btn btn-primary" />
        </div>
    </div>
</asp:Content>