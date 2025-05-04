<%@ Page Title="Materiales Educativos" Language="C#" MasterPageFile="~/MainUsuario.Master" AutoEventWireup="true" CodeBehind="WFPresentationMaterial.aspx.cs" Inherits="Presentation.WFPresentationMaterial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
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
                    <asp:BoundField DataField="mat_ano_publicacion" HeaderText="Año de Publicación" />
                    <asp:BoundField DataField="mat_precio" HeaderText="Precio" DataFormatString="{0:C}" HtmlEncode="false" />
                    <asp:BoundField DataField="mat_formato" HeaderText="Formato" />
                    <asp:BoundField DataField="editorial_nombre" HeaderText="Editorial" />
                    <asp:BoundField DataField="categoria_nombre" HeaderText="Categoría" />

                    <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="text-center">
                        <ItemTemplate>
                            <div class="btn-group" role="group">
                                <%-- Botón para abrir el material en una nueva ventana y registrar la visita --%>
                                <asp:Button ID="btnVer" runat="server" Text="Ver" CommandName="Ver"
                                    CommandArgument='<%# Eval("mat_id") %>' CssClass="btn btn-primary btn-sm"
                                    OnClientClick='<%# "ManejarClicVer(" + Eval("mat_id") + "); return false;" %>' />

                                <%-- Botón para comprar --%>
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

    <style>
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
    </style>
    
    <script type="text/javascript">
        // Función unificada para manejar el clic en "Ver"
        function ManejarClicVer(materialId) {
            // 1. Prevenir el postback
            event.preventDefault();

            // 2. Registrar la visita y obtener datos
            $.ajax({
                type: "POST",
                url: "WFPresentationMaterial.aspx/RegistrarVisitaInicial",
                data: JSON.stringify({ materialId: materialId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    var result = response.d;
                    if (result.success) {
                        // 3. Iniciar seguimiento
                        IniciarSeguimientoVisita(
                            result.visitaId,
                            result.urlMaterial,
                            new Date(result.timestamp)
                        );
                    } else {
                        console.error("Error al registrar visita:", result.error);
                        // Fallback: abrir sin seguimiento
                        window.open('Materiales/' + materialId, '_blank');
                    }
                },
                error: function (error) {
                    console.error("Error en la solicitud:", error);
                    // Fallback: abrir sin seguimiento
                    window.open('Materiales/' + materialId, '_blank');
                }
            });
        }

        // Función mejorada para seguimiento
        function IniciarSeguimientoVisita(visitaId, urlMaterial, fechaInicio) {
            console.log("Iniciando seguimiento para visita:", visitaId);

            // Almacenar en sessionStorage (más seguro para pestañas)
            sessionStorage.setItem('visita_' + visitaId + '_inicio', fechaInicio.getTime());

            // Abrir ventana del material
            var ventanaMaterial = window.open(urlMaterial, '_blank', 'width=1024,height=768');

            // Configurar intervalo de verificación
            var intervalo = setInterval(function () {
                try {
                    if (ventanaMaterial.closed) {
                        clearInterval(intervalo);
                        FinalizarYRegistrarVisita(visitaId);
                    }
                } catch (e) {
                    clearInterval(intervalo);
                    FinalizarYRegistrarVisita(visitaId);
                }
            }, 1000);

            // Manejar cierre de la pestaña principal
            window.addEventListener('beforeunload', function () {
                if (ventanaMaterial && !ventanaMaterial.closed) {
                    FinalizarYRegistrarVisita(visitaId);
                    ventanaMaterial.close();
                }
            });
        }

        // Función para registrar la duración final
        function FinalizarYRegistrarVisita(visitaId) {
            var inicio = sessionStorage.getItem('visita_' + visitaId + '_inicio');
            if (!inicio) return;

            var duracionMs = new Date() - new Date(parseInt(inicio));
            var duracionFormateada = formatarDuracion(duracionMs);

            // Debug esencial (quitar en producción)
            console.log("Duración calculada:", {
                visitaId: visitaId,
                inicio: new Date(parseInt(inicio)),
                fin: new Date(),
                duracion: duracionFormateada
            });

            $.ajax({
                type: "POST",
                url: "WFPresentationMaterial.aspx/ActualizarDuracionVisita",
                data: JSON.stringify({ visitaId: visitaId, duracion: duracionFormateada }),
                contentType: "application/json"
            });
        }

        function formatarDuracion(ms) {
            var segundos = Math.floor(ms / 1000);
            var horas = Math.floor(segundos / 3600);
            segundos %= 3600;
            var minutos = Math.floor(segundos / 60);
            segundos %= 60;

            return [
                horas.toString().padStart(2, '0'),
                minutos.toString().padStart(2, '0'),
                segundos.toString().padStart(2, '0')
            ].join(':');
        }

        function enviarDuracionAlServidor(visitaId, duracion) {
            console.log("Enviando duración:", duracion, "para visita:", visitaId);

            $.ajax({
                type: "POST",
                url: "WFPresentationMaterial.aspx/ActualizarDuracionVisita",
                data: JSON.stringify({
                    visitaId: visitaId,
                    duracion: duracion
                }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    console.log("Respuesta del servidor:", response.d);
                },
                error: function (xhr, status, error) {
                    console.error("Error en AJAX:", error);
                    // Reintentar con fetch
                    fetch('WFPresentationMaterial.aspx/ActualizarDuracionVisita', {
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        body: JSON.stringify({ visitaId: visitaId, duracion: duracion }),
                        keepalive: true
                    }).then(r => console.log("Respuesta fetch:", r))
                        .catch(e => console.error("Error fetch:", e));
                }
            });
        }
    </script>
</asp:Content>