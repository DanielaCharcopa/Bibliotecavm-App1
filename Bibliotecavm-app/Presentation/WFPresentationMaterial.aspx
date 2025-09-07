<%@ Page Title="Materiales" Language="C#" MasterPageFile="~/MainUsuario.Master" 
    AutoEventWireup="true" CodeBehind="WFPresentationMaterial.aspx.cs" 
    Inherits="Presentation.WFPresentationMaterial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    
    <style>
        /* Estilos para el modal mejorado */
        .modal-overlay {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: rgba(0, 0, 0, 0.6);
            z-index: 9998;
            backdrop-filter: blur(2px);
            display: none;
        }

        .modal-container {
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            width: 90%;
            max-width: 900px;
            max-height: 85vh;
            background: #ffffff;
            border-radius: 12px;
            box-shadow: 0 25px 50px rgba(0, 0, 0, 0.25);
            z-index: 9999;
            display: flex;
            flex-direction: column;
            overflow: hidden;
            display: none;
        }

        .modal-header {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 20px 25px;
            display: flex;
            justify-content: space-between;
            align-items: center;
            border-bottom: 1px solid #e2e8f0;
        }

        .modal-title {
            font-size: 1.5rem;
            font-weight: 600;
            margin: 0;
            text-shadow: 0 1px 2px rgba(0, 0, 0, 0.1);
        }

        .btn-close-modal {
            background: rgba(255, 255, 255, 0.2);
            border: 1px solid rgba(255, 255, 255, 0.3);
            color: white;
            padding: 8px 16px;
            border-radius: 8px;
            cursor: pointer;
            font-size: 0.9rem;
            font-weight: 500;
            transition: all 0.3s ease;
        }

        .btn-close-modal:hover {
            background: rgba(255, 255, 255, 0.3);
            transform: translateY(-1px);
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.2);
        }

        .modal-body {
            padding: 25px;
            flex: 1;
            overflow: hidden;
            display: flex;
            flex-direction: column;
            background: #f8fafc;
        }

        .iframe-container {
            position: relative;
            flex: 1;
            background: white;
            border-radius: 8px;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.05);
            overflow: hidden;
            min-height: 400px;
        }

        .material-frame {
            width: 100%;
            height: 100%;
            border: none;
            border-radius: 8px;
            min-height: 400px;
        }
        
        /* Estilos originales del formulario */
        .search-panel {
            background-color: #f8f9fa;
            padding: 15px;
            border-radius: 5px;
            margin-bottom: 20px;
        }
        .btn-group .btn {
            margin-right: 5px;
        }
        .btn-sm {
            padding: 0.25rem 0.5rem;
            font-size: 0.875rem;
        }
        
        /* Ocultar completamente el cronómetro */
        .hidden-timer {
            display: none !important;
            visibility: hidden !important;
            height: 0 !important;
            width: 0 !important;
            overflow: hidden !important;
            position: absolute !important;
            left: -9999px !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <h3 class="mt-4">Materiales Educativos</h3>
        
        <%-- Panel de búsqueda --%>
        <div class="search-panel mb-4">
            <div class="row g-3 align-items-center">
                <div class="col-md-5">
                    <asp:Label ID="LblBuscarTitulo" runat="server" Text="Buscar por título:" CssClass="form-label"></asp:Label>
                    <asp:TextBox ID="TxtBuscarTitulo" runat="server" CssClass="form-control" placeholder="Ingrese título..."></asp:TextBox>
                </div>
                <div class="col-md-5">
                    <asp:Label ID="LblFiltrarFormato" runat="server" Text="Filtrar por formato:" CssClass="form-label"></asp:Label>
                    <asp:DropDownList ID="DdlFormato" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Todos los formatos" Value=""></asp:ListItem>
                        <asp:ListItem Text="PDF" Value="PDF"></asp:ListItem>
                        <asp:ListItem Text="Video" Value="Video"></asp:ListItem>
                        <asp:ListItem Text="Audio" Value="Audio"></asp:ListItem>
                        <asp:ListItem Text="Libro" Value="Libro"></asp:ListItem>
                        <asp:ListItem Text="ePub" Value="ePub"></asp:ListItem>
                        <asp:ListItem Text="Otro" Value="Otro"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="row g-3 align-items-center mt-2">
                <div class="col-md-5">
                    <asp:Label ID="LblFiltrarCategoria" runat="server" Text="Filtrar por categoría:" CssClass="form-label"></asp:Label>
                    <asp:DropDownList ID="DdlCategoria" runat="server" CssClass="form-select">
                        <asp:ListItem Text="Todas las categorías" Value=""></asp:ListItem>
                        <asp:ListItem Text="Libro" Value="Libro"></asp:ListItem>
                        <asp:ListItem Text="Cartilla" Value="Cartilla"></asp:ListItem>
                        <asp:ListItem Text="Folleto" Value="Folleto"></asp:ListItem>
                        <asp:ListItem Text="Guía Didactica" Value="Guía Didactica"></asp:ListItem>
                        <asp:ListItem Text="Juego Lúdico" Value="Juego Lúdico"></asp:ListItem>
                        <asp:ListItem Text="Pendón" Value="Pendón"></asp:ListItem>
                        <asp:ListItem Text="Multimedia" Value="Multimedia"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col-md-2 d-flex align-items-end">
                    <asp:Button ID="BtnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary w-100" OnClick="BtnBuscar_Click" />
                </div>
            </div>
        </div>
        
        <asp:Label ID="LblMensaje" runat="server" Text="" ForeColor="Red" CssClass="d-block mb-3"></asp:Label>
        
        <div class="table-responsive">
            <asp:GridView ID="GVMateriales" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped table-hover" 
                EmptyDataText="No se encontraron materiales educativos con los criterios de búsqueda." OnRowCommand="GVMateriales_RowCommand" DataKeyNames="mat_id">
                <Columns>
                    <asp:BoundField DataField="mat_titulo" HeaderText="Título" />
                    <asp:BoundField DataField="mat_id" HeaderText="ID" Visible="false" />
                    <asp:BoundField DataField="mat_ano_publicacion" HeaderText="Año de Publicación" />
                    <asp:BoundField DataField="mat_precio" HeaderText="Precio" DataFormatString="{0:C}" HtmlEncode="false" />
                    <asp:BoundField DataField="mat_formato" HeaderText="Formato" />
                    <asp:BoundField DataField="editorial_nombre" HeaderText="Editorial" />
                    <asp:BoundField DataField="categoria_nombre" HeaderText="Categoría" />

                    <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <div class="btn-group" role="group">
                                <asp:Button ID="btnVer" runat="server" Text="Ver" CommandName="VerMaterial" 
                                    CommandArgument='<%# Eval("mat_id") + "|" + Eval("mat_url_descarga") + "|" + Eval("mat_titulo") %>' 
                                    CssClass="btn btn-primary btn-sm" />
                                
                                <asp:Button ID="btnComprar" runat="server" Text="Comprar" CommandName="Comprar"
                                    CommandArgument='<%# Eval("mat_id") %>' CssClass="btn btn-success btn-sm"
                                    OnClientClick="return confirm('¿Confirmar compra de este material?');" />
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="table-dark" />
            </asp:GridView>
        </div>
    </div>

    <!-- Modal ASP.NET con diseño mejorado -->
    <asp:Panel ID="pnlModal" runat="server" Visible="false">
        <div class="modal-overlay" id="modalOverlay"></div>
        
        <div class="modal-container" id="modalContainer">
            <!-- Header del Modal -->
            <div class="modal-header">
                <h4 class="modal-title">
                    <asp:Label ID="lblTituloModal" runat="server" />
                </h4>
                <asp:Button ID="btnFinalizarVisita" runat="server" Text="✕ Cerrar Material" 
                    OnClick="btnFinalizarVisita_Click" CssClass="btn-close-modal" />
            </div>
            
            <!-- Contenido del Modal -->
            <div class="modal-body">
                <div class="iframe-container">
                    <iframe runat="server" id="frameMaterial" class="material-frame"></iframe>
                </div>
            </div>
        </div>
    </asp:Panel>

    <!-- CRONÓMETRO OCULTO (solo para el backend) -->
    <div class="hidden-timer">
        <asp:Label ID="lblTiempoTranscurrido" runat="server"></asp:Label>
        <asp:Label ID="Label1" runat="server"></asp:Label>
    </div>

    <script type="text/javascript">
        function actualizarContador() {
            var inicio = new Date('<%= ViewState["HoraInicio"] != null ? ((DateTime)ViewState["HoraInicio"]).ToString("yyyy-MM-ddTHH:mm:ss") : DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") %>');
            var ahora = new Date();
            var diferencia = new Date(ahora - inicio);

            var horas = diferencia.getUTCHours().toString().padStart(2, '0');
            var minutos = diferencia.getUTCMinutes().toString().padStart(2, '0');
            var segundos = diferencia.getUTCSeconds().toString().padStart(2, '0');

            // Actualizar el label oculto
            var tiempoLabel = document.getElementById('<%= lblTiempoTranscurrido.ClientID %>');
            if (tiempoLabel) {
                tiempoLabel.innerText = horas + ':' + minutos + ':' + segundos;
            }

            setTimeout(actualizarContador, 1000);
        }

        // Mostrar u ocultar el modal según el estado del servidor
        function toggleModal() {
            var modalPanel = document.getElementById('<%= pnlModal.ClientID %>');
            var modalOverlay = document.getElementById('modalOverlay');
            var modalContainer = document.getElementById('modalContainer');

            if (modalPanel && modalOverlay && modalContainer) {
                // Si el panel está visible en el servidor, mostrar el modal
                if (<%= pnlModal.Visible.ToString().ToLower() %>) {
                    modalOverlay.style.display = 'block';
                    modalContainer.style.display = 'flex';

                    // Iniciar el contador (oculto)
                    actualizarContador();
                } else {
                    modalOverlay.style.display = 'none';
                    modalContainer.style.display = 'none';
                }
            }
        }

        // Ejecutar cuando se cargue la página
        document.addEventListener('DOMContentLoaded', function () {
            toggleModal();

            // Configurar el botón de cerrar
            var btnCerrar = document.getElementById('<%= btnFinalizarVisita.ClientID %>');
            if (btnCerrar) {
                btnCerrar.addEventListener('click', function () {
                    var modalOverlay = document.getElementById('modalOverlay');
                    var modalContainer = document.getElementById('modalContainer');

                    if (modalOverlay && modalContainer) {
                        modalOverlay.style.display = 'none';
                        modalContainer.style.display = 'none';
                    }
                });
            }
        });

        // También ejecutar después de postbacks de ASP.NET
        if (typeof (Sys) !== 'undefined') {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                toggleModal();
            });
        }
    </script>
</asp:Content>