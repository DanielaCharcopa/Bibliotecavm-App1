<%@ Page Title="Gestion de usuarios" Language="C#" MasterPageFile="~/MainAdmin.Master" AutoEventWireup="true" CodeBehind="WFUserManagement.aspx.cs" Inherits="Presentation.WFUserManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Agrega las dependencias de jQuery--%> 
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>

    <style type="text/css">
        /* Estilo para ocultar la columna ID pero mantener funcionalidad */
        .hidden-id-column {
            display: none !important;
            width: 0 !important;
            height: 0 !important;
            padding: 0 !important;
            margin: 0 !important;
            border: none !important;
        }
        
        /* Estilos generales para la tabla */
        .table {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }
        
        .table th {
            background-color: #f8f9fa;
            padding: 12px;
            text-align: left;
            border-bottom: 2px solid #dee2e6;
        }
        
        .table td {
            padding: 10px;
            border-bottom: 1px solid #dee2e6;
        }
        
        .table tr:hover {
            background-color: #f5f5f5;
        }
        
        .table-bordered {
            border: 1px solid #dee2e6;
        }
        
        /* Estilos para los mensajes */
        .message {
            display: block;
            padding: 10px;
            margin: 10px 0;
            border-radius: 4px;
        }
        
        .error-message {
            color: #dc3545;
            font-size: 0.875em;
        }
        
        /* Estilos para los formularios */
        .form-container {
            padding: 20px;
            background-color: #f9f9f9;
            border-radius: 5px;
            margin-bottom: 20px;
        }
        
        .form-control {
            width: 100%;
            padding: 8px;
            margin-bottom: 10px;
            border: 1px solid #ced4da;
            border-radius: 4px;
        }
        
        /* Estilos para los botones */
        .btn {
            padding: 8px 15px;
            margin-right: 10px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
        }
        
        .btn-primary {
            background-color: #007bff;
            color: white;
        }
        
        .btn-warning {
            background-color: #ffc107;
            color: black;
        }
        
        .btn-danger {
            background-color: #dc3545;
            color: white;
        }
        
        .btn-info {
            background-color: #17a2b8;
            color: white;
        }
        
        .btn-secondary {
            background-color: #6c757d;
            color: white;
        }
        
        .btn-success {
            background-color: #28a745;
            color: white;
        }
        
        /* Estilos para la paginación (estilo idéntico al que te gusta) */
        .pagination a {
            padding: 6px 12px;
            margin: 0 3px;
            border: 1px solid #ddd;
            text-decoration: none;
            color: #007bff;
        }
        
        .pagination span {
            padding: 6px 12px;
            margin: 0 3px;
            border: 1px solid #007bff;
            background-color: #007bff;
            color: white;
        }
        
        /* Estilos para el estado del usuario */
        .status-active {
            color: #28a745;
            font-weight: bold;
        }
        
        .status-inactive {
            color: #dc3545;
            font-weight: bold;
        }
        
        .status-control {
            margin: 15px 0;
            padding: 10px;
            background-color: #f8f9fa;
            border-radius: 5px;
        }
        
        .edit-mode {
            background-color: #fff3cd;
            padding: 10px;
            margin-bottom: 15px;
            border-radius: 4px;
            border-left: 4px solid #ffc107;
        }
    </style>

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
        <h3>Gestión de Usuarios</h3>
        
        <%--Panel de modo edición--%>
        <asp:Panel ID="PanelEditMode" runat="server" CssClass="edit-mode" Visible="false">
            <strong>Modo Edición:</strong> Está editando un usuario existente. 
            Use <strong>Actualizar</strong> para guardar cambios o 
            <asp:LinkButton ID="LBCancelEdit" runat="server" OnClick="LBCancelEdit_Click" CssClass="btn btn-sm btn-secondary">
                Cancelar
            </asp:LinkButton> para volver al modo nuevo usuario.
        </asp:Panel>
        
        <asp:Label ID="LblMessage" runat="server" CssClass="message"></asp:Label>

        <%--Campo oculto para almacenar el ID del usuario seleccionado--%>
        <asp:HiddenField ID="HFUserId" runat="server" />

        <%--Campos del formulario--%>
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
                <asp:ListItem Text="Seleccione un rol" Value="" />
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
        
        <%--Control de estado del usuario--%>
        <div class="status-control">
            <label>Estado del Usuario:</label>
            <asp:DropDownList ID="DDLEstado" runat="server" CssClass="form-control">
                <asp:ListItem Text="Activo" Value="Activo" Selected="True" />
                <asp:ListItem Text="Inactivo" Value="Inactivo" />
            </asp:DropDownList>
        </div>

        <%--Botones de acción--%>
        <div style="margin-top: 20px;">
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClientClick="return validateForm();" OnClick="BtnSave_Click" CssClass="btn btn-primary" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" CssClass="btn btn-warning" />
            <asp:Button ID="BtnNew" runat="server" Text="Nuevo" OnClick="BtnNew_Click" CssClass="btn btn-info" />
           
        </div>
    </div>
    
    <%--Buscar usuarios por correo--%>
    <div class="form-group" style="margin-top: 20px;">
        <label for="TxtBuscarCorreo">Buscar por Correo Electrónico:</label>
        <div class="input-group">
            <asp:TextBox ID="TxtBuscarCorreo" runat="server" CssClass="form-control" 
                placeholder="Escribe el correo del usuario" AutoPostBack="false"></asp:TextBox>
            <div class="input-group-append">
                <asp:Button ID="BtnBuscar" runat="server" Text="Buscar" 
                    OnClick="BtnBuscar_Click" CssClass="btn btn-info" />
                <asp:Button ID="BtnLimpiarBusqueda" runat="server" Text="Limpiar" 
                    OnClick="BtnLimpiarBusqueda_Click" CssClass="btn btn-secondary" />
            </div>
        </div>
    </div>

    <%-- Mensajes --%>
    <div id="loading" style="display:none; margin: 10px 0;">
        <i class="fa fa-spinner fa-spin"></i> Buscando usuarios...
    </div>

    <asp:Label ID="lblmesaje2" runat="server" CssClass="message"></asp:Label>

    <div id="mensajeNoResultados" runat="server" 
         style="display: none; margin: 10px 0; padding: 10px; 
                background-color: #f8f9fa; border-radius: 4px;">
    </div>

    <%--GridView para mostrar los usuarios--%>
    <asp:GridView ID="GVUsers" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False"
        DataKeyNames="usu_id" OnSelectedIndexChanged="GVUsers_SelectedIndexChanged1"
        AllowPaging="true" PageSize="10" OnPageIndexChanging="GVUsers_PageIndexChanging"
        PagerStyle-CssClass="pagination" 
        PagerSettings-Mode="NumericFirstLast"
        PagerSettings-Position="Bottom"
        PagerSettings-PageButtonCount="5">
        <Columns>
            <asp:BoundField DataField="usu_id" HeaderText="ID" 
                ItemStyle-CssClass="hidden-id-column" 
                HeaderStyle-CssClass="hidden-id-column" />
                
            <asp:BoundField DataField="usu_nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="usu_apellido" HeaderText="Apellido" />
            <asp:BoundField DataField="usu_correo" HeaderText="Correo Electrónico" />
            <asp:BoundField DataField="usu_rol" HeaderText="Rol" />
            <asp:BoundField DataField="usu_nivel_estudios" HeaderText="Nivel Educativo" />
            <asp:TemplateField HeaderText="Estado">
                <ItemTemplate>
                    <span class='<%# Eval("usu_estado").ToString() == "Activo" ? "status-active" : "status-inactive" %>'>
                        <%# Eval("usu_estado") %>
                    </span>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowSelectButton="True" HeaderText="Opción" SelectText="Seleccionar" 
                ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-outline-primary" />
        </Columns>
    </asp:GridView>

    <asp:Label ID="lblMesage" runat="server" CssClass="message"></asp:Label>
</asp:Content>