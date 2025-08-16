<%@ Page Title="Materiales" Language="C#" MasterPageFile="~/MainUsuario.Master" 
    AutoEventWireup="true" CodeBehind="WFPresentationMaterial.aspx.cs" 
    Inherits="Presentation.WFPresentationMaterial" %>

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

    <!-- Modal ASP.NET -->
    <asp:Panel ID="pnlModal" runat="server" Visible="false" style="position:fixed; top:50px; left:50px; width:80%; height:80%; background:white; z-index:1000; padding:20px;">
        <h2><asp:Label ID="lblTituloModal" runat="server" /></h2>
        <iframe runat="server" id="frameMaterial" style="width:100%; height:80%; border:none;"></iframe>
        
        <!-- Botón de finalización -->
        <asp:Button ID="btnFinalizarVisita" runat="server" Text="Finalizar Visita" OnClick="btnFinalizarVisita_Click" CssClass="btn btn-primary" />
        <asp:Label ID="Label1" runat="server" style="display:block; margin-top:10px;"></asp:Label>
        
        <!-- Contador de tiempo (opcional) -->
        <asp:Label ID="lblTiempoTranscurrido" runat="server" style="display:block; font-weight:bold;"></asp:Label>
    </asp:Panel>

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
        function actualizarContador() {
            var inicio = new Date('<%= ViewState["HoraInicio"] != null ? ((DateTime)ViewState["HoraInicio"]).ToString("yyyy-MM-ddTHH:mm:ss") : DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") %>');
            var ahora = new Date();
            var diferencia = new Date(ahora - inicio);

            var horas = diferencia.getUTCHours().toString().padStart(2, '0');
            var minutos = diferencia.getUTCMinutes().toString().padStart(2, '0');
            var segundos = diferencia.getUTCSeconds().toString().padStart(2, '0');

            document.getElementById('<%= lblTiempoTranscurrido.ClientID %>').innerText =
                'Tiempo transcurrido: ' + horas + ':' + minutos + ':' + segundos;

            setTimeout(actualizarContador, 1000);
        }
    </script>
</asp:Content>