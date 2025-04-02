<%@ Page Title="Registro de Usuarios" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFUserRegistration.aspx.cs" Inherits="Presentation.WFUserRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-container">
        <h2>Registro de Usuarios</h2>
        
        <!-- Mensaje de retroalimentación -->
        <asp:Label ID="LblMessage" runat="server" CssClass="message"></asp:Label>

        <!-- Campos del formulario -->
        <div class="form-group">
            <label for="TBFirstName">Nombre:</label>
            <asp:TextBox ID="TBFirstName" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:Label ID="LblNombreMessage" runat="server" CssClass="error-message" Visible="false" Text="El campo 'Nombre' es obligatorio."></asp:Label>
        </div>
        
        <div class="form-group">
            <label for="TBLastName">Apellido:</label>
            <asp:TextBox ID="TBLastName" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:Label ID="LblApellidoMessage" runat="server" CssClass="error-message" Visible="false" Text="El campo 'Apellido' es obligatorio."></asp:Label>
        </div>
        
        <div class="form-group">
            <label for="TBEmail">Correo Electrónico:</label>
            <asp:TextBox ID="TBEmail" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:Label ID="LblCorreoMessage" runat="server" CssClass="error-message" Visible="false" Text="Por favor, ingrese un correo electrónico válido de Gmail (ejemplo@gmail.com)."></asp:Label>
        </div>
        
        <div class="form-group">
            <label for="TBPassword">Contraseña:</label>
            <asp:TextBox ID="TBPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
            <asp:Label ID="LblPasswordMessage" runat="server" CssClass="error-message" Visible="false" Text="Por favor ingrese la contraseña del usuario."></asp:Label>
        </div>
        
        <div class="form-group">
            <label for="DDLRole">Rol:</label>
            <asp:DropDownList ID="DDLRole" runat="server" CssClass="form-control">
                <asp:ListItem Text="Seleccione un rol" Value="" />
                <asp:ListItem Text="Docente" Value="Docente" />
                <asp:ListItem Text="Estudiante" Value="Estudiante" />
            </asp:DropDownList>
        </div>
        
        <div class="form-group">
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
        <div class="form-group" style="margin-top: 30px;">
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClientClick="return validateForm();" OnClick="BtnSave_Click" CssClass="btn-primary" />
        </div>
    </div>
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
        <style type="text/css">
        /* Estilos generales */
        .form-container {
            max-width: 800px;
            margin: 3% auto;
            padding: 3%;
            background: #fff;
            border-radius: 10px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
        }
        
        h2 {
            text-align: center;
            color: #1a237e; /* Azul oscuro */
            margin-bottom: 30px;
        }
        
        /* Campos del formulario */
        .form-group {
            margin-bottom: 20px;
        }
        
        label {
            display: block;
            margin-bottom: 8px;
            color: #1a237e; /* Azul oscuro */
            font-weight: 500;
        }
        
        .form-control {
            width: 100%;
            padding: 12px;
            border: 1px solid #ddd;
            border-radius: 5px;
            font-size: 16px;
            transition: border-color 0.3s;
        }
        
        .form-control:focus {
            border-color: #1a237e; /* Azul oscuro */
            outline: none;
        }
        
        /* Botones */
        .btn-primary {
            background-color: #1a237e; /* Azul oscuro */
            color: white;
            border: none;
            padding: 12px 25px;
            border-radius: 5px;
            font-size: 16px;
            cursor: pointer;
            transition: background-color 0.3s;
            width: 100%;
        }
        
        .btn-primary:hover {
            background-color: #303f9f; /* Azul un poco más claro */
        }
        
        /* Mensajes */
        .message {
            display: block;
            padding: 10px;
            margin-bottom: 20px;
            border-radius: 5px;
            text-align: center;
        }
        
        .error-message {
            color: #e53935;
            font-size: 14px;
            margin-top: 5px;
            display: block;
        }
        
        /* Dropdowns */
        select.form-control {
            height: 45px;
            appearance: none;
            -webkit-appearance: none;
            -moz-appearance: none;
            background-image: url("data:image/svg+xml;charset=UTF-8,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' fill='%231a237e'%3e%3cpath d='M7 10l5 5 5-5z'/%3e%3c/svg%3e");
            background-repeat: no-repeat;
            background-position: right 10px center;
            background-size: 20px;
        }
        
        /* Responsividad */
        @media (max-width: 768px) {
            .form-container {
                margin: 5% auto;
                padding: 20px;
            }
            
            h2 {
                font-size: 24px;
            }
        }
    </style>
    
</asp:Content>