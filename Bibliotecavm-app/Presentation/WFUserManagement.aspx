<%@ Page Title="Gestion de usuarios" Language="C#" MasterPageFile="~/MainAdmin.Master" AutoEventWireup="true" CodeBehind="WFUserManagement.aspx.cs" Inherits="Presentation.WFUserManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />

    <style type="text/css">
        /* Sistema de Grid de 12 columnas */
        .container {
            width: 100%;
            padding: 0 15px;
        }
        
        .row {
            display: flex;
            flex-wrap: wrap;
            margin-left: -15px;
            margin-right: -15px;
        }
        
        .col-4 {
            flex: 0 0 33.333333%;
            max-width: 33.333333%;
            padding-left: 15px;
            padding-right: 15px;
            margin-bottom: 15px;
        }
        
        .col-8 {
            flex: 0 0 66.666667%;
            max-width: 66.666667%;
            padding-left: 15px;
            padding-right: 15px;
            margin-bottom: 15px;
        }
        
        /* Responsive para tablets */
        @media (max-width: 768px) {
            .col-4 {
                flex: 0 0 50%;
                max-width: 50%;
            }
            .col-8 {
                flex: 0 0 100%;
                max-width: 100%;
            }
        }
        
        /* Responsive para móviles */
        @media (max-width: 480px) {
            .col-4 {
                flex: 0 0 100%;
                max-width: 100%;
            }
            .col-8 {
                flex: 0 0 100%;
                max-width: 100%;
            }
        }

        /* Estilo para ocultar la columna ID pero mantener funcionalidad */
        .hidden-id-column {
            display: none !important;
            width: 0 !important;
            height: 0 !important;
            padding: 0 !important;
            margin: 0 !important;
            border: none !important;
        }
        
        /* Nuevos estilos para la tabla */
        .table-responsive {
            margin-top: 20px;
            overflow-x: auto;
        }
        
        .table {
            width: 100%;
            margin-bottom: 1rem;
            color: #212529;
            border-collapse: collapse;
        }
        
        .table-bordered {
            border: 1px solid #dee2e6;
        }
        
        .table-bordered th,
        .table-bordered td {
            border: 1px solid #dee2e6;
        }
        
        .table-striped tbody tr:nth-of-type(odd) {
            background-color: rgba(0, 0, 0, 0.02);
        }
        
        .table-hover tbody tr:hover {
            background-color: rgba(78, 115, 223, 0.05);
        }
        
        .table-dark {
            background-color: #343a40;
            color: white;
        }
        
        .table-dark th {
            border-color: #454d55;
        }
        
        .align-middle {
            vertical-align: middle !important;
        }
        
        .text-end {
            text-align: right !important;
        }
        
        .text-center {
            text-align: center !important;
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
            margin-top: 5px;
            display: block;
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
            padding: 10px;
            margin-top: 5px;
            border: 1px solid #ced4da;
            border-radius: 4px;
            font-size: 14px;
            box-sizing: border-box;
        }
        
        .form-group {
            margin-bottom: 0;
        }
        
        .form-group label {
            font-weight: 600;
            margin-bottom: 5px;
            display: block;
            color: #495057;
        }
        
        /* ESTILOS CORREGIDOS PARA LOS BOTONES (VERSIÓN FUNCIONAL) */
        .btn {
            padding: 12px 25px;
            margin: 5px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-weight: 600;
            font-size: 14px;
            transition: all 0.3s ease;
            display: inline-block;
            text-align: center;
            text-decoration: none;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
        }

        /* Botón Guardar (Verde) */
        .btn-save, input[type="submit"].btn-save {
            background-color: #28a745 !important;
            color: white !important;
            border: 2px solid #28a745 !important;
        }
        .btn-save:hover, .btn-save:focus,
        input[type="submit"].btn-save:hover, 
        input[type="submit"].btn-save:focus {
            background-color: #218838 !important;
            border-color: #1e7e34 !important;
            box-shadow: 0 0 0 0.2rem rgba(40,167,69,0.5);
        }

        /* Botón Actualizar (Azul oscuro) */
        .btn-update, input[type="submit"].btn-update {
            background-color: #1a237e !important;
            color: white !important;
            border: 2px solid #1a237e !important;
        }
        .btn-update:hover, .btn-update:focus,
        input[type="submit"].btn-update:hover, 
        input[type="submit"].btn-update:focus {
            background-color: #303f9f !important;
            border-color: #283593 !important;
            box-shadow: 0 0 0 0.2rem rgba(26,35,126,0.5);
        }

        /* Botón Nuevo (Amarillo) */
        .btn-new, input[type="submit"].btn-new {
            background-color: #ffc107 !important;
            color: #212529 !important;
            border: 2px solid #ffc107 !important;
        }
        .btn-new:hover, .btn-new:focus,
        input[type="submit"].btn-new:hover, 
        input[type="submit"].btn-new:focus {
            background-color: #e0a800 !important;
            border-color: #d39e00 !important;
            box-shadow: 0 0 0 0.2rem rgba(255,193,7,0.5);
        }

        /* Botón Buscar (Azul claro) */
        .btn-info {
            background-color: #17a2b8 !important;
            color: white !important;
            border: 2px solid #17a2b8 !important;
        }
        .btn-info:hover, .btn-info:focus {
            background-color: #138496 !important;
            border-color: #117a8b !important;
            box-shadow: 0 0 0 0.2rem rgba(23,162,184,0.5);
        }

        /* Botón Limpiar (Gris) */
        .btn-secondary {
            background-color: #6c757d !important;
            color: white !important;
            border: 2px solid #6c757d !important;
        }
        .btn-secondary:hover, .btn-secondary:focus {
            background-color: #5a6268 !important;
            border-color: #545b62 !important;
            box-shadow: 0 0 0 0.2rem rgba(108,117,125,0.5);
        }
        
        /* Paginación mejorada */
        .pagination {
            display: flex;
            padding-left: 0;
            list-style: none;
            border-radius: 0.25rem;
            justify-content: center;
            margin-top: 20px;
        }
        
        .pagination a {
            padding: 6px 12px;
            margin: 0 3px;
            border: 1px solid #dee2e6;
            text-decoration: none;
            color: #007bff;
            border-radius: 4px;
            transition: all 0.3s;
        }
        
        .pagination a:hover {
            background-color: #e9ecef;
        }
        
        .pagination span {
            padding: 6px 12px;
            margin: 0 3px;
            border: 1px solid #007bff;
            background-color: #007bff;
            color: white;
            border-radius: 4px;
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
        
        .edit-mode {
            background-color: #fff3cd;
            padding: 15px;
            margin-bottom: 20px;
            border-radius: 4px;
            border-left: 4px solid #ffc107;
        }

        /* Estilo para el grupo de inputs */
        .input-group {
            display: flex;
            margin-bottom: 15px;
        }
        
        .input-group-append {
            margin-left: -1px;
        }
        
        .input-group-append .btn {
            border-radius: 0 5px 5px 0;
            margin-left: 0;
        }
        
        .input-group .form-control {
            border-radius: 5px 0 0 5px;
            margin-bottom: 0;
        }
        
        /* Estilo para los botones de acción */
        .action-buttons {
            text-align: center;
            margin-top: 30px;
            padding-top: 20px;
            border-top: 1px solid #dee2e6;
        }
        
        /* Estilo para los botones de búsqueda inline */
        .search-buttons {
            display: flex;
            gap: 10px;
        }
        
        .search-buttons .btn {
            flex: 1;
            margin: 0;
        }

        /* ===== ESTILOS ESPECÍFICOS PARA BOTÓN SELECCIONAR (AZUL CLARO) ===== */
        /* Aplica solo al botón dentro del GridView */
        .table-responsive .btn.btn-outline-primary {
            border: 1px solid #0d6efd !important;
            color: #0d6efd !important;
            background-color: transparent !important;
            padding: 6px 12px !important;
            border-radius: 4px !important;
            transition: all 0.3s !important;
        }

        .table-responsive .btn.btn-outline-primary:hover,
        .table-responsive .btn.btn-outline-primary:focus {
            background-color: #0d6efd !important;
            color: white !important;
            box-shadow: 0 0 0 0.2rem rgba(13, 110, 253, 0.25) !important;
        }

        /* Sobrescribe estilos específicos de la tabla */
        .table-responsive .table .btn-outline-primary {
            border-color: #0d6efd !important;
            color: #0d6efd !important;
        }

        .table-responsive .table .btn-outline-primary:hover {
            background-color: #0d6efd !important;
            color: white !important;
        }
        /* ===== FIN DE ESTILOS PARA BOTÓN SELECCIONAR ===== */

        /* Clase para botones pequeños */
        .btn-sm {
            padding: 6px 12px;
            font-size: 0.875rem;
        }
    </style>

    <script>
        function validateForm() {
            var email = document.getElementById("<%= TBEmail.ClientID %>").value;
            var regex = /^[a-zA-Z0-9._%+-]+@gmail\.com$/;

            if (!regex.test(email)) {
                alert("Por favor, ingrese un correo electrónico válido de Gmail (ejemplo@gmail.com).");
                return false;
            }
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

        <%--Formulario con sistema de grid de 3 columnas--%>
        <div class="container">
            <%--Primera fila: Nombre, Apellido, Correo--%>
            <div class="row">
                <div class="col-4">
                    <div class="form-group">
                        <label for="TBFirstName">Nombre:</label>
                        <asp:TextBox ID="TBFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:Label ID="LblNombreMessage" runat="server" CssClass="error-message" Visible="false" Text="El campo 'Nombre' es obligatorio."></asp:Label>
                    </div>
                </div>
                <div class="col-4">
                    <div class="form-group">
                        <label for="TBLastName">Apellido:</label>
                        <asp:TextBox ID="TBLastName" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:Label ID="LblApellidoMessage" runat="server" CssClass="error-message" Visible="false" Text="El campo 'Apellido' es obligatorio."></asp:Label>
                    </div>
                </div>
                <div class="col-4">
                    <div class="form-group">
                        <label for="TBEmail">Correo Electrónico:</label>
                        <asp:TextBox ID="TBEmail" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:Label ID="LblCorreoMessage" runat="server" CssClass="error-message" Visible="false" Text="Por favor, ingrese un correo electrónico válido de Gmail (ejemplo@gmail.com)."></asp:Label>
                    </div>
                </div>
            </div>
            
            <%--Segunda fila: Contraseña, Rol, Estado--%>
            <div class="row">
                <div class="col-4">
                    <div class="form-group">
                        <label for="TBPassword">Contraseña:</label>
                        <asp:TextBox ID="TBPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                        <asp:Label ID="LblPasswordMessage" runat="server" CssClass="error-message" Visible="false" Text="Por favor ingrese la contraseña del usuario."></asp:Label>
                    </div>
                </div>
                <div class="col-4">
                    <div class="form-group">
                        <label for="DDLRole">Rol:</label>
                        <asp:DropDownList ID="DDLRole" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Seleccione un rol" Value="" />
                            <asp:ListItem Text="Administrador" Value="Administrador" />
                            <asp:ListItem Text="Docente" Value="Docente" />
                            <asp:ListItem Text="Estudiante" Value="Estudiante" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-4">
                    <div class="form-group">
                        <label for="DDLEstado">Estado del Usuario:</label>
                        <asp:DropDownList ID="DDLEstado" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Activo" Value="Activo" Selected="True" />
                            <asp:ListItem Text="Inactivo" Value="Inactivo" />
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>

        <%--Botones de acción centrados--%>
        <div class="action-buttons">
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClientClick="return validateForm();" OnClick="BtnSave_Click" CssClass="btn btn-save" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" CssClass="btn btn-update" />
            <asp:Button ID="BtnNew" runat="server" Text="Nuevo" OnClick="BtnNew_Click" CssClass="btn btn-new" />
        </div>
    </div>
    
    <%--Sección de búsqueda con sistema de grid simétrico--%>
    <div class="form-container">
        <h4>Buscar Usuarios</h4>
        
        <div class="container">
            <div class="row">
                <%--Campo de búsqueda ocupa 2 columnas (66.67%)--%>
                <div class="col-8">
                    <div class="form-group">
                        <label for="TxtBuscarCorreo">Buscar por Correo Electrónico:</label>
                        <asp:TextBox ID="TxtBuscarCorreo" runat="server" CssClass="form-control" 
                            placeholder="Escribe el correo del usuario" AutoPostBack="false"></asp:TextBox>
                    </div>
                </div>
                
                <%--Botones ocupan 1 columna (33.33%)--%>
                <div class="col-4">
                    <div class="form-group">
                        <label>&nbsp;</label>
                        <div class="search-buttons">
                            <asp:Button ID="BtnBuscar" runat="server" Text="Buscar" 
                                OnClick="BtnBuscar_Click" CssClass="btn btn-info" />
                            <asp:Button ID="BtnLimpiarBusqueda" runat="server" Text="Limpiar" 
                                OnClick="BtnLimpiarBusqueda_Click" CssClass="btn btn-secondary" />
                        </div>
                    </div>
                </div>
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
    <div class="table-responsive">
        <asp:GridView ID="GVUsers" runat="server" 
            CssClass="table table-bordered table-striped table-hover" 
            AutoGenerateColumns="False"
            DataKeyNames="usu_id" 
            OnSelectedIndexChanged="GVUsers_SelectedIndexChanged1"
            AllowPaging="true" 
            PageSize="10" 
            OnPageIndexChanging="GVUsers_PageIndexChanging"
            PagerStyle-CssClass="pagination" 
            PagerSettings-Mode="NumericFirstLast"
            PagerSettings-Position="Bottom"
            PagerSettings-PageButtonCount="5"
            EmptyDataText="No se encontraron usuarios con los criterios de búsqueda."
            aria-label="Lista de usuarios">
            <Columns>
                <asp:BoundField DataField="usu_id" HeaderText="ID" 
                    ItemStyle-CssClass="hidden-id-column" 
                    HeaderStyle-CssClass="hidden-id-column" />
                    
                <asp:BoundField DataField="usu_nombre" HeaderText="Nombre" 
                    HeaderStyle-CssClass="table-dark align-middle"
                    ItemStyle-CssClass="align-middle" />
                    
                <asp:BoundField DataField="usu_apellido" HeaderText="Apellido" 
                    HeaderStyle-CssClass="table-dark align-middle"
                    ItemStyle-CssClass="align-middle" />
                    
                <asp:BoundField DataField="usu_correo" HeaderText="Correo Electrónico" 
                    HeaderStyle-CssClass="table-dark align-middle"
                    ItemStyle-CssClass="align-middle" />
                    
                <asp:BoundField DataField="usu_rol" HeaderText="Rol" 
                    HeaderStyle-CssClass="table-dark align-middle"
                    ItemStyle-CssClass="align-middle" />
                    
                <asp:TemplateField HeaderText="Estado"
                    HeaderStyle-CssClass="table-dark align-middle text-center"
                    ItemStyle-CssClass="align-middle text-center">
                    <ItemTemplate>
                        <span class='<%# Eval("usu_estado").ToString() == "Activo" ? "status-active" : "status-inactive" %>'>
                            <%# Eval("usu_estado") %>
                        </span>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:CommandField
                    ShowSelectButton="True"
                    HeaderText="Acción"
                    SelectText="Seleccionar"
                    ButtonType="Button"
                    ControlStyle-CssClass="btn btn-sm btn-outline-primary"
                    HeaderStyle-CssClass="table-dark align-middle text-center"
                    ItemStyle-CssClass="align-middle text-center" />
            </Columns>
            <HeaderStyle CssClass="table-dark" />
            <RowStyle CssClass="align-middle" />
            <EmptyDataRowStyle CssClass="text-center p-4" />
        </asp:GridView>
    </div>

    <asp:Label ID="lblMesage" runat="server" CssClass="message"></asp:Label>
</asp:Content>