<%@ Page Title="Solicitudes de compras" Language="C#" MasterPageFile="~/MainUsuario.Master" AutoEventWireup="true" CodeBehind="WFPurchaseRequest.aspx.cs" Inherits="Presentation.WFPurchaseRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet" />
    <style type="text/css">
        /* Estilos generales */
        body {
            font-family: 'Montserrat', sans-serif;
            background-color: #f5f5f5;
            color: #333;
            margin: 0;
            padding: 0;
        }
        
        /* CONTENEDOR PRINCIPAL AHORA EXPANDIDO */
        .form-container {
            width: 100%;
            margin: 0 auto;
            padding: 30px;
            background: white;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
            box-sizing: border-box;
        }
        
        /* Contenedor interno para centrar el contenido */
        .content-wrapper {
            max-width: 1200px;
            margin: 0 auto;
        }
        
        h1 {
            color: #1a237e;
            text-align: center;
            margin-bottom: 25px;
            font-weight: 600;
        }
        
        /* NUEVOS ESTILOS PARA LAYOUT EN COLUMNAS */
        .form-layout {
            display: grid;
            grid-template-columns: 1fr 1fr;
            gap: 30px;
            margin-bottom: 30px;
        }
        
        .form-column {
            background: #fafafa;
            padding: 25px;
            border-radius: 8px;
            border: 1px solid #e9ecef;
        }
        
        .form-column h4 {
            color: #1a237e;
            margin-bottom: 20px;
            font-weight: 600;
            border-bottom: 2px solid #1a237e;
            padding-bottom: 10px;
        }
        
        /* Estructura de formulario accesible */
        .form-group {
            margin-bottom: 20px;
            display: flex;
            flex-direction: column;
        }
        
        .form-group-horizontal {
            display: flex;
            align-items: center;
            gap: 10px;
        }
        
        .form-group-horizontal .form-control {
            flex: 1;
        }
        
        .form-label {
            font-weight: 500;
            color: #2c3e50;
            margin-bottom: 5px;
            display: block;
        }
        
        .form-control {
            width: 100%;
            padding: 10px 12px;
            border: 1px solid #ddd;
            border-radius: 4px;
            font-family: 'Montserrat';
            transition: all 0.3s;
            box-sizing: border-box;
        }
        
        .form-control:focus {
            border-color: #1a237e;
            outline: none;
            box-shadow: 0 0 0 3px rgba(26,35,126,0.1);
        }
        
        /* Estilos para campos de solo lectura */
        .form-control[readonly] {
            background-color: #f8f9fa;
            color: #6c757d;
        }
        
        /* Estilos para botones */
        .btn {
            padding: 10px 20px;
            border: none;
            border-radius: 4px;
            font-family: 'Montserrat';
            font-weight: 500;
            cursor: pointer;
            transition: all 0.3s;
            display: inline-flex;
            align-items: center;
            justify-content: center;
            gap: 8px;
            text-decoration: none !important;
        }

        .btn-primary {
            background-color: #1a237e;
            color: white;
        }
        
        .btn-primary:hover {
            background-color: #303f9f;
        }
        
        .btn-secondary {
            background-color: #7f8c8d;
            color: white;
        }
        
        .btn-secondary:hover {
            background-color: #95a5a6;
        }
        
        .btn-danger {
            background-color: #e74c3c;
            color: white;
        }
        
        .btn-danger:hover {
            background-color: #c0392b;
        }
        
        .btn-buscar {
            background-color: #1a237e;
            color: white;
            padding: 12px 18px;
            min-width: 160px;
            white-space: nowrap;
            font-size: 0.95rem;
        }
        
        .btn-buscar:hover {
            background-color: #303f9f;
            transform: translateY(-2px);
            box-shadow: 0 4px 8px rgba(26,35,126,0.3);
        }
        
        .btn-outline-primary {
            border: 1px solid #0d6efd;
            color: #0d6efd;
            background-color: transparent;
            padding: 6px 12px;
            border-radius: 4px;
            transition: all 0.3s;
        }

        .btn-outline-primary:hover,
        .btn-outline-primary:focus {
            background-color: #0d6efd;
            color: white;
            box-shadow: 0 0 0 0.2rem rgba(13, 110, 253, 0.25);
        }

        .grid-view-improved .btn-outline-primary {
            border-color: #0d6efd;
            color: #0d6efd;
        }
        
        .grid-view-improved .btn-outline-primary:hover {
            background-color: #0d6efd;
            color: white;
        }

        /* Clase para botones pequeños */
        .btn-sm {
            padding: 6px 12px;
            font-size: 0.875rem;
        }
        
        /* Estilo añadido para íconos */
        .btn i {
            margin-right: 8px;
            display: inline-block;   
        }
        
        /* Contenedor de botones - ahora spans across columns */
        .btn-container {
            grid-column: 1 / -1;
            display: flex;
            justify-content: center;
            gap: 15px;
            margin: 30px 0;
            padding: 20px;
            background: #f8f9fa;
            border-radius: 8px;
            border: 1px solid #e9ecef;
        }
        
        /* Material selector destacado */
        .material-selector {
            background: linear-gradient(135deg, #e3f2fd 0%, #f3e5f5 100%);
            border: 2px solid #1a237e;
            border-radius: 10px;
            padding: 20px;
        }
        
        .material-selector h4 {
            color: #1a237e;
            margin-bottom: 15px;
            font-weight: 600;
            text-align: center;
        }
        
        .material-selector .form-group-horizontal {
            margin-top: 10px;
        }
        
        /* GridView Mejorado */
        .grid-container {
            margin-top: 30px;
            overflow-x: auto;
        }
        
        .grid-view-improved {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }
        
        .grid-view-improved th {
            background-color: #343a40;
            color: white;
            padding: 12px;
            text-align: left;
            vertical-align: middle;
        }
        
        .grid-view-improved td {
            padding: 12px;
            border-bottom: 1px solid #dee2e6;
            vertical-align: middle;
        }
        
        .grid-view-improved tr:nth-child(even) {
            background-color: rgba(0, 0, 0, 0.05);
        }
        
        .grid-view-improved tr:hover {
            background-color: rgba(0, 0, 0, 0.075);
        }
        
        /* Paginación mejorada */
        .pagination-improved {
            display: flex;
            justify-content: center;
            padding-left: 0;
            list-style: none;
            border-radius: 0.25rem;
        }
        
        .pagination-improved a {
            position: relative;
            display: block;
            padding: 0.5rem 0.75rem;
            margin-left: -1px;
            line-height: 1.25;
            color: #1a237e;
            background-color: #fff;
            border: 1px solid #dee2e6;
            text-decoration: none;
        }
        
        .pagination-improved a:hover {
            color: #0d1533;
            background-color: #e9ecef;
            border-color: #dee2e6;
        }
        
        .pagination-improved span {
            position: relative;
            display: block;
            padding: 0.5rem 0.75rem;
            margin-left: -1px;
            line-height: 1.25;
            color: #fff;
            background-color: #1a237e;
            border: 1px solid #1a237e;
        }
        
        /* Estilo para ocultar columnas */
        .hidden-column, 
        .grid-view-improved th.hidden-column, 
        .grid-view-improved td.hidden-column {
            display: none !important;
            width: 0 !important;
            height: 0 !important;
            padding: 0 !important;
            margin: 0 !important;
            border: none !important;
        }
        
        /* Mensajes */
        .message-container {
            margin: 20px 0 !important;
            padding: 0 !important;
            grid-column: 1 / -1;
        }
        
        .message {
            padding: 15px 20px !important;
            margin: 0 0 25px 0 !important;
            border-radius: 6px !important;
            text-align: center;
            line-height: 1.6;
            font-size: 15px;
            display: block;
            width: 100%;
            box-sizing: border-box;
        }
        
        .error-message {
            background-color: #ffebee !important;
            color: #c62828 !important;
            border: 1px solid #ef9a9a !important;
        }

        .info-message {
            background-color: #e3f2fd !important;
            color: #1565c0 !important;
            border: 1px solid #90caf9 !important;
            box-shadow: 0 2px 5px rgba(0,0,0,0.05) !important;
        }
        
        /* Estilos para los botones ocultos */
        .btn-hidden {
            display: none !important;
            visibility: hidden !important;
            opacity: 0 !important;
            pointer-events: none !important;
        }
        
        /* Campos ocultos accesibles */
        .sr-only {
            position: absolute !important;
            width: 1px !important;
            height: 1px !important;
            padding: 0 !important;
            margin: -1px !important;
            overflow: hidden !important;
            clip: rect(0, 0, 0, 0) !important;
            white-space: nowrap !important;
            border: 0 !important;
        }
        
        /* Responsive */
        @media (max-width: 992px) {
            .form-layout {
                grid-template-columns: 1fr;
                gap: 20px;
            }
            
            .form-container {
                padding: 20px;
            }
            
            .btn-container {
                flex-direction: column;
            }
            
            .btn {
                width: 100%;
            }
            
            .form-group-horizontal {
                flex-direction: column;
                align-items: stretch;
                gap: 10px;
            }
            
            .btn-buscar {
                min-width: auto;
                width: 100%;
            }
        }
        
        @media (max-width: 768px) {
            .form-column {
                padding: 15px;
            }
            
            /* Ajustes para paginación en móviles */
            .pagination-improved a, 
            .pagination-improved span {
                padding: 3px 6px;
                margin: 0 2px;
                font-size: 0.9em;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-container">
        <div class="content-wrapper">
            <h3>Registro de Compras</h3>
            
            <!-- Campos ocultos con labels para accesibilidad -->
            <asp:HiddenField ID="HFPurchaId" runat="server" />
            
            <div class="sr-only">
                <asp:Label ID="LblTicketHidden" runat="server" Text="Ticket interno:" AssociatedControlID="TBTicket"></asp:Label>
                <asp:TextBox ID="TBTicket" runat="server" TabIndex="-1" aria-hidden="true"></asp:TextBox>
                
                <asp:Label ID="LblFechaHidden" runat="server" Text="Fecha interna:" AssociatedControlID="TBFecha"></asp:Label>
                <asp:TextBox ID="TBFecha" runat="server" TabIndex="-1" aria-hidden="true"></asp:TextBox>
            </div>

            <!-- Mensaje informativo -->
            <div class="message-container">
                <asp:Label ID="LblMsj" runat="server" CssClass="message info-message" role="status" aria-live="polite"></asp:Label>
            </div>

            <!-- Formulario principal dividido en columnas -->
            <form role="form" aria-label="Formulario de registro de compras">
                <div class="form-layout">
                    <!-- COLUMNA IZQUIERDA: Selección de Material -->
                    <div class="form-column material-selector">
                        <h4><i class="fas fa-search"></i> Selección de Material</h4>
                        <fieldset>
                            <legend class="sr-only">Selección de material</legend>
                            
                            <!-- Material -->
                            <div class="form-group">
                                <asp:Label ID="LblMaterial" runat="server" Text="Material Seleccionado:" CssClass="form-label" AssociatedControlID="TxtMaterialSeleccionado"></asp:Label>
                                <asp:TextBox ID="TxtMaterialSeleccionado" runat="server" ReadOnly="true" CssClass="form-control" 
                                             aria-describedby="MaterialHelp" placeholder="Ningún material seleccionado"></asp:TextBox>
                                <asp:HiddenField ID="HdnMaterialId" runat="server" />
                                <small id="MaterialHelp" class="form-text text-muted">Haz clic en "Buscar Material" para seleccionar un producto.</small>
                            </div>
                            
                            <!-- Botón de búsqueda destacado -->
                            <div class="form-group">
                                <asp:LinkButton ID="BtnBuscarMaterial" runat="server" OnClick="BtnBuscarMaterial_Click" 
                                                CssClass="btn btn-primary btn-buscar" aria-label="Buscar y seleccionar material"
                                                style="width: 100%; justify-content: center;">
                                    <i class="fas fa-search" aria-hidden="true"></i> Buscar Material
                                </asp:LinkButton>
                            </div>
                            
                            <!-- Precio Unitario -->
                            <div class="form-group">
                                <asp:Label ID="LblUnitPrice" runat="server" Text="Precio Unitario:" CssClass="form-label" AssociatedControlID="TBUnitPrice"></asp:Label>
                                <asp:TextBox ID="TBUnitPrice" runat="server" ReadOnly="true" CssClass="form-control" 
                                             aria-describedby="UnitPriceHelp" placeholder="Selecciona un material para ver el precio"></asp:TextBox>
                                <small id="UnitPriceHelp" class="form-text text-muted">El precio se actualiza automáticamente según el material seleccionado.</small>
                            </div>
                        </fieldset>
                    </div>

                    <!-- COLUMNA DERECHA: Información de Compra -->
                    <div class="form-column">
                        <h4><i class="fas fa-shopping-cart"></i> Información de Compra</h4>
                        <fieldset>
                            <legend class="sr-only">Información de la compra</legend>
                            
                            <!-- Fecha -->
                            <div class="form-group">
                                <asp:Label ID="LblFecha" runat="server" Text="Fecha:" CssClass="form-label" AssociatedControlID="LblFechaMostrar"></asp:Label>
                                <asp:Label ID="LblFechaMostrar" runat="server" CssClass="form-control" role="textbox" aria-readonly="true"></asp:Label>
                            </div>
                            
                            <!-- Cantidad -->
                            <div class="form-group">
                                <asp:Label ID="LblQuantity" runat="server" Text="Cantidad:" CssClass="form-label" AssociatedControlID="TBQuantity"></asp:Label>
                                <asp:TextBox ID="TBQuantity" runat="server" TextMode="Number" 
                                             min="1" value="1" AutoPostBack="true" 
                                             OnTextChanged="TBQuantity_TextChanged" CssClass="form-control"
                                             aria-describedby="QuantityHelp" aria-required="true"></asp:TextBox>
                                <small id="QuantityHelp" class="form-text text-muted">Ingresa la cantidad deseada (mínimo 1).</small>
                            </div>
                            
                            <!-- Total -->
                            <div class="form-group">
                                <asp:Label ID="LblTotal" runat="server" Text="Total:" CssClass="form-label" AssociatedControlID="TBTotal"></asp:Label>
                                <asp:TextBox ID="TBTotal" runat="server" ReadOnly="true" CssClass="form-control"
                                             aria-describedby="TotalHelp" placeholder="El total se calcula automáticamente"
                                             style="font-weight: bold; font-size: 1.1em; color: #1a237e;"></asp:TextBox>
                                <small id="TotalHelp" class="form-text text-muted">Total calculado: cantidad × precio unitario.</small>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </form>
            
            <!-- Botones de acción -->
            <div class="btn-container" role="group" aria-label="Acciones del formulario">
                <asp:LinkButton ID="BtnSave" runat="server" OnClick="BtnSave_Click" CssClass="btn btn-primary" 
                                OnClientClick="return confirm('¿Confirmar compra?');" aria-describedby="SaveHelp">
                    <i class="fas fa-shopping-cart" aria-hidden="true"></i> Comprar
                </asp:LinkButton>
                <asp:LinkButton ID="BtnUpdate" runat="server" OnClick="BtnUpdate_Click" CssClass="btn btn-secondary btn-hidden" 
                                OnClientClick="return confirm('¿Confirmar actualización?');" aria-describedby="UpdateHelp">
                    <i class="fas fa-sync-alt" aria-hidden="true"></i> Actualizar
                </asp:LinkButton>
                <asp:LinkButton ID="BtnDelete" runat="server" OnClick="BtnDelete_Click" CssClass="btn btn-danger btn-hidden" 
                                OnClientClick="return confirm('¿Eliminar esta solicitud?');" aria-describedby="DeleteHelp">
                    <i class="fas fa-trash-alt" aria-hidden="true"></i> Eliminar
                </asp:LinkButton>
            </div>
            
            <!-- Textos de ayuda para botones -->
            <div class="sr-only">
                <div id="SaveHelp">Guardar nueva solicitud de compra y contactar al vendedor por WhatsApp.</div>
                <div id="UpdateHelp">Actualizar solicitud de compra existente.</div>
                <div id="DeleteHelp">Eliminar solicitud de compra seleccionada.</div>
            </div>

            <!-- Lista de solicitudes -->
            <div class="grid-container">
                <h4 class="mb-3">Solicitudes de Compra Registradas</h4>
                <asp:GridView ID="GVRequests" runat="server" AutoGenerateColumns="False" 
                    OnSelectedIndexChanged="GVRequests_SelectedIndexChanged" 
                    DataKeyNames="solic_id,tbl_material_edu_mat_id"
                    AllowPaging="True" PageSize="5" OnPageIndexChanging="GVRequests_PageIndexChanging"
                    CssClass="table table-bordered table-striped table-hover grid-view-improved" 
                    PagerStyle-CssClass="pagination-improved" GridLines="None"
                    aria-label="Lista de solicitudes de compra registradas"
                    role="table">
                    <Columns>
                        <asp:BoundField DataField="solic_id" HeaderText="ID" 
                            HeaderStyle-CssClass="hidden-column table-dark" 
                            ItemStyle-CssClass="hidden-column" />
                            
                        <asp:BoundField DataField="solic_ticket" HeaderText="Ticket" 
                            HeaderStyle-CssClass="table-dark align-middle" 
                            ItemStyle-CssClass="align-middle" />
                            
                        <asp:BoundField DataField="solic_fecha" HeaderText="Fecha" 
                            DataFormatString="{0:dd/MM/yyyy}"
                            HeaderStyle-CssClass="table-dark align-middle" 
                            ItemStyle-CssClass="align-middle" />
                            
                        <asp:BoundField DataField="usuario_nombre" HeaderText="Usuario" 
                            HeaderStyle-CssClass="hidden-column table-dark" 
                            ItemStyle-CssClass="hidden-column" />
                            
                        <asp:BoundField DataField="material_titulo" HeaderText="Material" 
                            HeaderStyle-CssClass="table-dark align-middle" 
                            ItemStyle-CssClass="align-middle" 
                            HtmlEncode="false" />
                            
                        <asp:BoundField DataField="solic_cantidad" HeaderText="Cantidad" 
                            HeaderStyle-CssClass="table-dark align-middle text-center" 
                            ItemStyle-CssClass="align-middle text-center" />
                            
                        <asp:BoundField DataField="precio_unitario" HeaderText="Precio Unitario" 
                            DataFormatString="{0:C2}"
                            HeaderStyle-CssClass="table-dark align-middle text-end" 
                            ItemStyle-CssClass="align-middle text-end" />
                            
                        <asp:BoundField DataField="solic_valor_total" HeaderText="Total" 
                            DataFormatString="{0:C2}"
                            HeaderStyle-CssClass="table-dark align-middle text-end" 
                            ItemStyle-CssClass="align-middle text-end" />
                            
                        <asp:CommandField
                            ShowSelectButton="True"
                            HeaderText="Acciones"
                            SelectText="Seleccionar"
                            ButtonType="Button"
                            ControlStyle-CssClass="btn btn-sm btn-outline-primary"
                            HeaderStyle-CssClass="table-dark text-center"
                            ItemStyle-CssClass="text-center align-middle" />
                    </Columns>
                    <HeaderStyle CssClass="table-dark" />
                    <RowStyle CssClass="align-middle" />
                    <PagerSettings Mode="NumericFirstLast" Position="Bottom" 
                        PageButtonCount="5" FirstPageText="Primera" LastPageText="Última" 
                        NextPageText="Siguiente" PreviousPageText="Anterior" />
                    <EmptyDataTemplate>
                        <div class="text-center p-4">
                            <i class="fas fa-inbox fa-2x text-muted mb-2"></i>
                            <p class="text-muted">No hay solicitudes de compra registradas.</p>
                        </div>
                    </EmptyDataTemplate>
                </asp:GridView>
            </div>
        </div>
    </div>


    <script type="text/javascript">
        function toggleSaveUpdateButtons(isEditing) {
            var btnSave = document.getElementById('<%= BtnSave.ClientID %>');
            var btnUpdate = document.getElementById('<%= BtnUpdate.ClientID %>');
            var btnDelete = document.getElementById('<%= BtnDelete.ClientID %>');
            var lblMsj = document.getElementById('<%= LblMsj.ClientID %>');

            if (isEditing) {
                // Modo edición
                btnSave.classList.add('btn-hidden');
                btnUpdate.classList.remove('btn-hidden');
                btnDelete.classList.remove('btn-hidden');

                // Actualizar atributos ARIA
                btnSave.setAttribute('aria-hidden', 'true');
                btnUpdate.setAttribute('aria-hidden', 'false');
                btnDelete.setAttribute('aria-hidden', 'false');

                // Mostrar mensaje al usuario
                lblMsj.textContent = 'Estás editando un registro existente. Usa el botón "Actualizar" para guardar los cambios.';
                lblMsj.className = 'message info-message';
                lblMsj.setAttribute('aria-live', 'polite');
            } else {
                // Modo nuevo registro
                btnSave.classList.remove('btn-hidden');
                btnUpdate.classList.add('btn-hidden');
                btnDelete.classList.add('btn-hidden');
                
                // Actualizar atributos ARIA
                btnSave.setAttribute('aria-hidden', 'false');
                btnUpdate.setAttribute('aria-hidden', 'true');
                btnDelete.setAttribute('aria-hidden', 'true');

                // Mostrar mensaje al usuario
                lblMsj.textContent = 'Modo de creación de nuevo registro. Completa los campos y haz clic en "Comprar".';
                lblMsj.className = 'message info-message';
                lblMsj.setAttribute('aria-live', 'polite');
            }
        }

        // Llamar a esta función cuando se selecciona un registro
        function onRecordSelected() {
            toggleSaveUpdateButtons(true);
            // Anunciar a lectores de pantalla que se ha seleccionado un registro
            var announcement = document.createElement('div');
            announcement.setAttribute('aria-live', 'assertive');
            announcement.setAttribute('aria-atomic', 'true');
            announcement.className = 'sr-only';
            announcement.textContent = 'Registro seleccionado para edición.';
            document.body.appendChild(announcement);
            setTimeout(function() {
                document.body.removeChild(announcement);
            }, 3000);
        }

        // Llamar a esta función cuando se limpia el formulario
        function onFormCleared() {
            toggleSaveUpdateButtons(false);
        }

        // Verificar estado al cargar la página
        window.onload = function() {
            var purchaseId = document.getElementById('<%= HFPurchaId.ClientID %>').value;
            toggleSaveUpdateButtons(purchaseId !== '');

            // Configurar eventos de teclado para navegación mejorada
            var form = document.querySelector('form[role="form"]');
            if (form) {
                form.addEventListener('keydown', function (e) {
                    // Permitir navegación con Tab y Enter
                    if (e.key === 'Tab') {
                        // Comportamiento normal del Tab
                        return;
                    }
                    if (e.key === 'Enter' && e.target.type !== 'textarea') {
                        // Prevenir submit accidental en campos que no son textarea
                        if (e.target.type !== 'submit' && e.target.tagName !== 'BUTTON') {
                            e.preventDefault();
                        }
                    }
                });
            }
        };
    </script>
</asp:Content>