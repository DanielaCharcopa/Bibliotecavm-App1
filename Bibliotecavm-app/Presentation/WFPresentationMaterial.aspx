<%@ Page Title="Materiales Educativos" Language="C#" MasterPageFile="~/MainUsuario.Master" AutoEventWireup="true" CodeBehind="WFPresentationMaterial.aspx.cs" Inherits="Presentation.WFPresentationMaterial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <h1 class="mt-4">Materiales Educativos</h1>
        
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
                                    OnClientClick='<%# "RegistrarVisita(" + Eval("mat_id") + "); return true;" %>' />
                                
                                <%-- Nuevo botón para comprar --%>
                                <asp:Button ID="btnComprar" runat="server" Text="Comprar" CommandName="Comprar" 
                                    CommandArgument='<%# Eval("mat_id") %>' CssClass="btn btn-success btn-sm" />
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
        function MonitorearDuracionVisita(visitaId) {
            var inicio = new Date(); // Tiempo de inicio

            window.onbeforeunload = function () {
                var fin = new Date(); // Tiempo de finalización
                var duracion = fin - inicio; // Duración en milisegundos

                // Convertir la duración a formato HH:MM:SS
                var horas = Math.floor(duracion / 3600000);
                var minutos = Math.floor((duracion % 3600000) / 60000);
                var segundos = Math.floor((duracion % 60000) / 1000);
                var duracionFormateada = `${horas}:${minutos}:${segundos}`;

                // Enviar la duración al servidor
                $.ajax({
                    type: "POST",
                    url: "WFPresentationMaterial.aspx/ActualizarDuracionVisita",
                    data: JSON.stringify({ visitaId: visitaId, duracion: duracionFormateada }),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        console.log("Duración actualizada correctamente.");
                    },
                    error: function (error) {
                        console.log("Error al actualizar la duración: " + error.responseText);
                    }
                });
            };
        }

        function RegistrarVisita(matId) {
            // Registrar la visita mediante una solicitud AJAX
            $.ajax({
                type: "POST",
                url: "WFPresentationMaterial.aspx/RegistrarVisita",
                data: JSON.stringify({ matId: matId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    console.log("Visita registrada correctamente.");
                },
                error: function (error) {
                    console.log("Error al registrar la visita: " + error.responseText);
                }
            });
        }
    </script>
</asp:Content>