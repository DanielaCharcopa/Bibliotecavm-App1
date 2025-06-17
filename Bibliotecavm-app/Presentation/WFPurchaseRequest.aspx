<%@ Page Title="Solicitudes de compras" Language="C#" MasterPageFile="~/MainUsuario.Master" AutoEventWireup="true" CodeBehind="WFPurchaseRequest.aspx.cs" Inherits="Presentation.WFPurchaseRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" rel="stylesheet" />
    <style type="text/css">
        /* Estilos generales */
        body {
            font-family: 'Montserrat', sans-serif;
            background-color: #f5f5f5;
            color: #333;
        }
        
        .form-container {
            max-width: 800px;
            margin: 20px auto;
            padding: 30px;
            background: white;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0,0,0,0.1);
        }
        
        h1 {
            color: #1a237e;
            text-align: center;
            margin-bottom: 25px;
            font-weight: 600;
        }
        
        /* Estilos para la tabla de formulario */
        .form-table {
            width: 100%;
            border-collapse: separate;
            border-spacing: 0 15px;
        }
        
        .form-table td {
            padding: 8px 0;
        }
        
        .form-label {
            font-weight: 500;
            color: #2c3e50;
            display: block;
            margin-bottom: 5px;
        }
        
        .form-control {
            width: 100%;
            padding: 10px 12px;
            border: 1px solid #ddd;
            border-radius: 4px;
            font-family: 'Montserrat';
            transition: all 0.3s;
        }
        
        .form-control:focus {
            border-color: #1a237e;
            outline: none;
            box-shadow: 0 0 0 3px rgba(26,35,126,0.1);
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
            padding: 10px 15px;
            margin-left: 10px;
            min-width: 150px;
        }
        
        .btn-buscar:hover {
            background-color: #303f9f;
        }

        /* ===== ESTILOS MODIFICADOS PARA BOTÓN SELECCIONAR (AZUL CLARO) ===== */
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
        /* ===== FIN DE ESTILOS MODIFICADOS ===== */

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
        
        /* Contenedor de botones */
        .btn-container {
            display: flex;
            justify-content: center;
            gap: 15px;
            margin: 30px 0;
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
        .message {
            padding: 10px;
            margin: 15px 0;
            border-radius: 4px;
            text-align: center;
        }
        
        .error-message {
            background-color: #ffebee;
            color: #c62828;
            border: 1px solid #ef9a9a;
        }

        .info-message {
            background-color: #e3f2fd;
            color: #1565c0;
            border: 1px solid #90caf9;
        }
        
        /* Estilos para los botones ocultos */
        .btn-hidden {
            display: none !important;
            visibility: hidden !important;
            opacity: 0 !important;
            pointer-events: none !important;
        }
        
        /* Responsive */
        @media (max-width: 768px) {
            .form-container {
                padding: 20px;
            }
            
            .btn-container {
                flex-direction: column;
            }
            
            .btn {
                width: 100%;
            }
            
            .btn-buscar {
                background-color: #1a237e;
                color: white;
                padding: 10px 15px;
                margin-left: 10px;
                min-width: auto;
                display: inline-flex;
                align-items: center;
                justify-content: center;
                white-space: nowrap;
            }

            .btn-buscar i {
                margin-right: 8px;
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
        <h3>Registro de Compras</h3>
        
        <asp:Label ID="LblMsj" runat="server" CssClass="message info-message"></asp:Label>
        <asp:HiddenField ID="HFPurchaId" runat="server" />
        <asp:TextBox ID="TBTicket" runat="server" style="display:none;"></asp:TextBox>

        <table class="form-table">
            <tr>
                <td>
                    <asp:TextBox ID="TBFecha" runat="server" style="display:none;"></asp:TextBox>
                    <asp:Label ID="LblFecha" runat="server" Text="Fecha:" CssClass="form-label"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="LblFechaMostrar" runat="server" CssClass="form-control"></asp:Label>
                </td>
            </tr>
           <tr>
                <td>
                    <asp:Label ID="LblMaterial" runat="server" Text="Material Seleccionado:" CssClass="form-label"></asp:Label>
                </td>
                <td>
                    <div style="display:flex; align-items:center;">
                        <asp:TextBox ID="TxtMaterialSeleccionado" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                        <asp:HiddenField ID="HdnMaterialId" runat="server" />
                        <asp:LinkButton ID="BtnBuscarMaterial" runat="server" OnClick="BtnBuscarMaterial_Click" 
                            CssClass="btn btn-primary" style="margin-left: 10px; white-space: nowrap;">
                            <i class="fas fa-search"></i> Buscar Material
                        </asp:LinkButton>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LblQuantity" runat="server" Text="Cantidad:" CssClass="form-label"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TBQuantity" runat="server" TextMode="Number" 
                        min="1" value="1" AutoPostBack="true" 
                        OnTextChanged="TBQuantity_TextChanged" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LblUnitPrice" runat="server" Text="Precio Unitario:" CssClass="form-label"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TBUnitPrice" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LblTotal" runat="server" Text="Total:" CssClass="form-label"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TBTotal" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
        </table>
        
        <div class="btn-container">
            <asp:LinkButton ID="BtnSave" runat="server" OnClick="BtnSave_Click" CssClass="btn btn-primary" OnClientClick="return confirm('¿Confirmar compra?');">
                <i class="fas fa-shopping-cart"></i> Comprar
            </asp:LinkButton>
            <asp:LinkButton ID="BtnUpdate" runat="server" OnClick="BtnUpdate_Click" CssClass="btn btn-secondary btn-hidden" OnClientClick="return confirm('¿Confirmar actualización?');">
                <i class="fas fa-sync-alt"></i> Actualizar
            </asp:LinkButton>
            <asp:LinkButton ID="BtnDelete" runat="server" OnClick="BtnDelete_Click" CssClass="btn btn-danger btn-hidden" OnClientClick="return confirm('¿Eliminar esta solicitud?');">
                <i class="fas fa-trash-alt"></i> Eliminar
            </asp:LinkButton>
        </div>

        <div class="grid-container">
            <asp:GridView ID="GVRequests" runat="server" AutoGenerateColumns="False" 
                OnSelectedIndexChanged="GVRequests_SelectedIndexChanged" 
                DataKeyNames="solic_id,tbl_material_edu_mat_id"
                AllowPaging="True" PageSize="5" OnPageIndexChanging="GVRequests_PageIndexChanging"
                CssClass="table table-bordered table-striped table-hover grid-view-improved" 
                PagerStyle-CssClass="pagination-improved" GridLines="None"
                aria-label="Lista de solicitudes de compra">
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
                        ItemStyle-CssClass="align-middle" />
                        
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
                        HeaderText="Opción"
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
            </asp:GridView>
        </div>
    </div>

    <script type="text/javascript">
        function toggleSaveUpdateButtons(isEditing) {
            var btnSave = document.getElementById('<%= BtnSave.ClientID %>');
            var btnUpdate = document.getElementById('<%= BtnUpdate.ClientID %>');
            var btnDelete = document.getElementById('<%= BtnDelete.ClientID %>');

            if (isEditing) {
                // Modo edición
                btnSave.classList.add('btn-hidden');
                btnUpdate.classList.remove('btn-hidden');
                btnDelete.classList.remove('btn-hidden');

                // Mostrar mensaje al usuario
                document.getElementById('<%= LblMsj.ClientID %>').textContent = 'Estás editando un registro existente. Usa el botón "Actualizar" para guardar los cambios.';
                document.getElementById('<%= LblMsj.ClientID %>').className = 'message info-message';
            } else {
                // Modo nuevo registro
                btnSave.classList.remove('btn-hidden');
                btnUpdate.classList.add('btn-hidden');
                btnDelete.classList.add('btn-hidden');
                
                // Mostrar mensaje al usuario
                document.getElementById('<%= LblMsj.ClientID %>').textContent = 'Modo de creación de nuevo registro. Completa los campos y haz clic en "Comprar".';
                document.getElementById('<%= LblMsj.ClientID %>').className = 'message info-message';
            }
        }

        // Llamar a esta función cuando se selecciona un registro
        function onRecordSelected() {
            toggleSaveUpdateButtons(true);
        }

        // Llamar a esta función cuando se limpia el formulario
        function onFormCleared() {
            toggleSaveUpdateButtons(false);
        }

        // Verificar estado al cargar la página
        window.onload = function() {
            var purchaseId = document.getElementById('<%= HFPurchaId.ClientID %>').value;
            toggleSaveUpdateButtons(purchaseId !== '');
        };
    </script>
</asp:Content>