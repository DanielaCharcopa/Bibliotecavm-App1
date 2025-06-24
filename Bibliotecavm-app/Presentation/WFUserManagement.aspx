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
        
        /* ESTILOS CORREGIDOS PARA LOS BOTONES CON ICONOS */
        .btn {
            padding: 12px 25px;
            margin: 5px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            font-weight: 600;
            font-size: 14px;
            transition: all 0.3s ease;
            display: inline-flex;
            align-items: center;
            gap: 8px;
            text-align: center;
            text-decoration: none;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
        }

        /* Iconos en los botones */
        .btn i {
            font-size: 16px;
            margin-right: 0;
        }

        /* Botón Guardar (Verde) - Contraste mejorado */
        .btn-save, input[type="submit"].btn-save {
            background-color: #218838 !important;
            color: white !important;
            border: 2px solid #1e7e34 !important;
        }
        .btn-save:hover, .btn-save:focus,
        input[type="submit"].btn-save:hover, 
        input[type="submit"].btn-save:focus {
            background-color: #1c7430 !important;
            border-color: #186127 !important;
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

        /* Botón Buscar (Azul claro) - Contraste mejorado */
        .btn-info {
            background-color: #117a8b !important;
            color: white !important;
            border: 2px solid #0c5460 !important;
        }
        .btn-info:hover, .btn-info:focus {
            background-color: #0d6674 !important;
            border-color: #0a4e5a !important;
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
        
        /* Estilos para el estado del usuario - Contraste mejorado */
        .status-active {
            color: #1e7e34 !important;
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

        /* Estilo especial para LinkButton con icono */
        .btn-with-icon {
            display: inline-flex !important;
            align-items: center;
            gap: 5px;
            text-decoration: none;
        }

        /* Texto de ayuda para campos de formulario */
        .form-text {
            display: block;
            margin-top: 0.25rem;
            font-size: 0.875em;
            color: #6c757d;
        }

        /* Estilos para accesibilidad */
        .sr-only {
            position: absolute;
            width: 1px;
            height: 1px;
            padding: 0;
            margin: -1px;
            overflow: hidden;
            clip: rect(0, 0, 0, 0);
            white-space: nowrap;
            border: 0;
        }
    </style>

    <script>
        function validateForm() {
            var email = document.getElementById("<%= TBEmail.ClientID %>").value;
            var celular = document.getElementById("<%= TBCelular.ClientID %>").value;
            var regex = /^[a-zA-Z0-9._%+-]+@gmail\.com$/;

            if (!regex.test(email)) {
                alert("Por favor, ingrese un correo electrónico válido de Gmail (ejemplo@gmail.com).");
                return false;
            }

            // Validar celular
            if (celular && (celular.length !== 10 || !celular.startsWith('3'))) {
                alert("El número celular debe tener 10 dígitos y comenzar con 3.");
                return false;
            }
            return true;
        }

        // Función para agregar iconos a los botones - Compatible con ASP.NET
        function addIconsToButtons() {
            // Agregar icono al botón Guardar
            var btnSave = document.getElementById('<%= BtnSave.ClientID %>');
            if (btnSave && btnSave.value && !btnSave.value.includes('💾')) {
                btnSave.value = '💾 ' + btnSave.value;
            }

            // Agregar icono al botón Actualizar
            var btnUpdate = document.getElementById('<%= BtnUpdate.ClientID %>');
            if (btnUpdate && btnUpdate.value && !btnUpdate.value.includes('✏️')) {
                btnUpdate.value = '✏️ ' + btnUpdate.value;
            }

            // Agregar icono al botón Nuevo
            var btnNew = document.getElementById('<%= BtnNew.ClientID %>');
            if (btnNew && btnNew.value && !btnNew.value.includes('➕')) {
                btnNew.value = '➕ ' + btnNew.value;
            }

            // Agregar icono al botón Buscar
            var btnBuscar = document.getElementById('<%= BtnBuscar.ClientID %>');
            if (btnBuscar && btnBuscar.value && !btnBuscar.value.includes('🔍')) {
                btnBuscar.value = '🔍 ' + btnBuscar.value;
            }

            // Agregar icono al botón Limpiar
            var btnLimpiar = document.getElementById('<%= BtnLimpiarBusqueda.ClientID %>');
            if (btnLimpiar && btnLimpiar.value && !btnLimpiar.value.includes('🧹')) {
                btnLimpiar.value = '🧹 ' + btnLimpiar.value;
            }

            // Agregar icono al LinkButton Cancelar (este sí usa innerHTML)
            var lbCancel = document.getElementById('<%= LBCancelEdit.ClientID %>');
            if (lbCancel && !lbCancel.innerHTML.includes('fa-times')) {
                lbCancel.innerHTML = '<i class="fas fa-times"></i> ' + lbCancel.innerHTML;
                lbCancel.className += ' btn-with-icon';
            }

            // Agregar accesibilidad al GridView
            addGridAccessibility();
        }

        // Función para agregar accesibilidad al GridView
        function addGridAccessibility() {
            var grid = document.getElementById('<%= GVUsers.ClientID %>');
            if (grid) {
                // Agregar role="grid" y otros atributos ARIA
                grid.setAttribute('role', 'grid');
                grid.setAttribute('aria-readonly', 'true');
                
                // Agregar roles a las filas y celdas
                var headers = grid.getElementsByTagName('th');
                for (var i = 0; i < headers.length; i++) {
                    headers[i].setAttribute('role', 'columnheader');
                    headers[i].setAttribute('scope', 'col');
                }

                var rows = grid.getElementsByTagName('tr');
                for (var i = 0; i < rows.length; i++) {
                    rows[i].setAttribute('role', 'row');
                    
                    var cells = rows[i].getElementsByTagName('td');
                    for (var j = 0; j < cells.length; j++) {
                        cells[j].setAttribute('role', 'gridcell');
                    }
                }
            }
        }

        // Ejecutar cuando el DOM esté listo
        document.addEventListener('DOMContentLoaded', addIconsToButtons);

        // También ejecutar en window.onload para mayor compatibilidad
        window.onload = addIconsToButtons;

        // Para casos de postback de ASP.NET
        if (typeof (Sys) !== 'undefined') {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(addIconsToButtons);
        }
    </script>


    <script>
        // Función para manejar el evento keypress en el campo de búsqueda
        function handleEnterKey(event) {
            if (event.keyCode === 13) { // 13 es el código de la tecla Enter
                event.preventDefault(); // Prevenir el comportamiento por defecto
                var btnBuscar = document.getElementById('<%= BtnBuscar.ClientID %>');
                if (btnBuscar) {
                    btnBuscar.click(); // Simular clic en el botón Buscar
                }
            }
        }

        // Asignar el evento al campo de búsqueda cuando el DOM esté listo
        document.addEventListener('DOMContentLoaded', function () {
            var txtBuscar = document.getElementById('<%= TxtBuscarCorreo.ClientID %>');
            if (txtBuscar) {
                txtBuscar.addEventListener('keypress', handleEnterKey);
            }
        });

        // También asignar el evento después de un postback de ASP.NET
        if (typeof(Sys) !== 'undefined') {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function() {
                var txtBuscar = document.getElementById('<%= TxtBuscarCorreo.ClientID %>');
                if (txtBuscar) {
                    txtBuscar.addEventListener('keypress', handleEnterKey);
                }
            });
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-container">
        <h1 class="sr-only">Gestión de Usuarios</h1>
        <h2 aria-hidden="true"><i class="fas fa-users"></i> Gestión de Usuarios</h2>
                
        <asp:Panel ID="PanelEditMode" runat="server" CssClass="edit-mode" Visible="false" role="alert">
            <i class="fas fa-edit"></i> <strong>Modo Edición:</strong> Está editando un usuario existente. 
            Use <strong>Actualizar</strong> para guardar cambios o 
            <asp:LinkButton ID="LBCancelEdit" runat="server" OnClick="LBCancelEdit_Click" CssClass="btn btn-sm btn-secondary">
                Cancelar
            </asp:LinkButton> para volver al modo nuevo usuario.
        </asp:Panel>
        
        <asp:Label ID="LblMessage" runat="server" CssClass="message" role="alert" aria-live="polite"></asp:Label>

        <asp:HiddenField ID="HFUserId" runat="server" />

        <div class="container">
            <div class="row">
                <div class="col-4">
                    <div class="form-group">
                        <label for="ContentPlaceHolder1_TBFirstName" id="label_TBFirstName"><i class="fas fa-user"></i> Nombre:</label>
                        <asp:TextBox ID="TBFirstName" runat="server" CssClass="form-control" aria-labelledby="label_TBFirstName" aria-required="true"></asp:TextBox>
                        <asp:Label ID="LblNombreMessage" runat="server" CssClass="error-message" Visible="false" Text="El campo 'Nombre' es obligatorio."></asp:Label>
                    </div>
                </div>
                <div class="col-4">
                    <div class="form-group">
                        <label for="ContentPlaceHolder1_TBLastName" id="label_TBLastName"><i class="fas fa-user"></i> Apellido:</label>
                        <asp:TextBox ID="TBLastName" runat="server" CssClass="form-control" aria-labelledby="label_TBLastName" aria-required="true"></asp:TextBox>
                        <asp:Label ID="LblApellidoMessage" runat="server" CssClass="error-message" Visible="false" Text="El campo 'Apellido' es obligatorio."></asp:Label>
                    </div>
                </div>
                <div class="col-4">
                    <div class="form-group">
                        <label for="ContentPlaceHolder1_TBEmail" id="label_TBEmail"><i class="fas fa-envelope"></i> Correo Electrónico:</label>
                        <asp:TextBox ID="TBEmail" runat="server" CssClass="form-control" aria-labelledby="label_TBEmail" aria-describedby="emailHelp" aria-required="true"></asp:TextBox>
                        <small id="emailHelp" class="form-text">Debe ser una dirección de Gmail válida (ejemplo@gmail.com)</small>
                        <asp:Label ID="LblCorreoMessage" runat="server" CssClass="error-message" Visible="false" Text="Por favor, ingrese un correo electrónico válido de Gmail (ejemplo@gmail.com)."></asp:Label>
                    </div>
                </div>
            </div>
            
            <div class="row">
                <div class="col-4">
                    <div class="form-group">
                        <label for="ContentPlaceHolder1_TBCelular" id="label_TBCelular"><i class="fas fa-mobile-alt"></i> Número Celular:</label>
                        <asp:TextBox ID="TBCelular" runat="server" CssClass="form-control" MaxLength="10" aria-labelledby="label_TBCelular" aria-describedby="celularHelp"></asp:TextBox>
                        <small id="celularHelp" class="form-text">Debe tener 10 dígitos y comenzar con 3</small>
                        <asp:Label ID="LblCelularMessage" runat="server" CssClass="error-message" Visible="false" Text="El número celular debe tener 10 dígitos y comenzar con 3."></asp:Label>
                    </div>
                </div>
                <div class="col-4">
                    <div class="form-group">
                        <label for="ContentPlaceHolder1_TBPassword" id="label_TBPassword"><i class="fas fa-lock"></i> Contraseña:</label>
                        <asp:TextBox ID="TBPassword" runat="server" TextMode="Password" CssClass="form-control" aria-labelledby="label_TBPassword" aria-required="true"></asp:TextBox>
                        <asp:Label ID="LblPasswordMessage" runat="server" CssClass="error-message" Visible="false" Text="Por favor ingrese la contraseña del usuario."></asp:Label>
                    </div>
                </div>
                <div class="col-4">
                    <div class="form-group">
                        <label for="ContentPlaceHolder1_DDLRole" id="label_DDLRole"><i class="fas fa-user-tag"></i> Rol:</label>
                        <asp:DropDownList ID="DDLRole" runat="server" CssClass="form-control" aria-labelledby="label_DDLRole" aria-required="true">
                            <asp:ListItem Text="Seleccione un rol" Value="" />
                            <asp:ListItem Text="Administrador" Value="Administrador" />
                            <asp:ListItem Text="Docente" Value="Docente" />
                            <asp:ListItem Text="Estudiante" Value="Estudiante" />
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            
            <div class="row">
                <div class="col-4">
                    <div class="form-group">
                        <label for="ContentPlaceHolder1_DDLEstado" id="label_DDLEstado"><i class="fas fa-toggle-on"></i> Estado del Usuario:</label>
                        <asp:DropDownList ID="DDLEstado" runat="server" CssClass="form-control" aria-labelledby="label_DDLEstado">
                            <asp:ListItem Text="Activo" Value="Activo" Selected="True" />
                            <asp:ListItem Text="Inactivo" Value="Inactivo" />
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>

        <div class="action-buttons">
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClientClick="return validateForm();" OnClick="BtnSave_Click" CssClass="btn btn-save" aria-label="Guardar usuario" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" CssClass="btn btn-update" aria-label="Actualizar usuario" />
            <asp:Button ID="BtnNew" runat="server" Text="Nuevo" OnClick="BtnNew_Click" CssClass="btn btn-new" aria-label="Crear nuevo usuario" />
        </div>
    </div>
    
   <div class="form-container">
    <h3><i class="fas fa-search"></i> Buscar Usuarios</h3>
    
    <div class="container">
        <div class="row">
            <div class="col-8">
                 <%--Envolvemos el TextBox y el botón en un Panel con DefaultButton--%> 
                <asp:Panel ID="PanelBusqueda" runat="server" DefaultButton="BtnBuscar">
                    <div class="form-group">
                        <label for="ContentPlaceHolder1_TxtBuscarCorreo" id="label_TxtBuscarCorreo"><i class="fas fa-envelope"></i> Buscar por Correo Electrónico:</label>
                        <asp:TextBox ID="TxtBuscarCorreo" runat="server" CssClass="form-control" 
                            placeholder="Escribe el correo del usuario" AutoPostBack="false"
                            aria-labelledby="label_TxtBuscarCorreo"></asp:TextBox>
                    </div>
                </asp:Panel>
            </div>
            
            <div class="col-4">
                <div class="form-group">
                    <label class="sr-only" for="ContentPlaceHolder1_BtnBuscar">Botones de búsqueda</label>
                    <div class="search-buttons">
                        <asp:Button ID="BtnBuscar" runat="server" Text="Buscar" 
                            OnClick="BtnBuscar_Click" CssClass="btn btn-info" aria-label="Buscar usuario" />
                        <asp:Button ID="BtnLimpiarBusqueda" runat="server" Text="Limpiar" 
                            OnClick="BtnLimpiarBusqueda_Click" CssClass="btn btn-secondary" aria-label="Limpiar búsqueda" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>

    <div id="loading" style="display:none; margin: 10px 0;">
        <i class="fa fa-spinner fa-spin"></i> Buscando usuarios...
    </div>

    <asp:Label ID="lblmesaje2" runat="server" CssClass="message"></asp:Label>

    <div id="mensajeNoResultados" runat="server" 
         style="display: none; margin: 10px 0; padding: 10px; 
                background-color: #f8f9fa; border-radius: 4px;"
         role="alert" aria-live="polite">
    </div>

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
                    
                <asp:BoundField DataField="usu_celular" HeaderText="Celular" 
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
                            <i class='<%# Eval("usu_estado").ToString() == "Activo" ? "fas fa-check-circle" : "fas fa-times-circle" %>' aria-hidden="true"></i>
                            <span class="sr-only"><%# Eval("usu_estado") %></span>
                            <span aria-hidden="true"><%# Eval("usu_estado") %></span>
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