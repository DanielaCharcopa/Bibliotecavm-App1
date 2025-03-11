<%@ Page Title="" Language="C#" MasterPageFile="~/MainUsuario.Master" AutoEventWireup="true" CodeBehind="WFVisits.aspx.cs" Inherits="Presentation.WFVisits" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Agrega las dependencias de jQuery, jQuery UI y jTable -->
    <link href="https://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.12.1/jquery-ui.min.css" rel="stylesheet" />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/jtable/2.4.0/themes/metro/blue/jtable.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jtable/2.4.0/jquery.jtable.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Gestión de Visitas</h2>

    <!-- Contenedor para jTable -->
    <div id="MaterialTableContainer"></div>

    <!-- Campos ocultos para almacenar el ID y título del material seleccionado -->
    <asp:HiddenField ID="HdnMaterialId" runat="server" />
    <asp:HiddenField ID="HdnMaterialTitulo" runat="server" />

    <!-- Resto del formulario -->
    <div>
        <!-- Label para mostrar el nombre del usuario logueado -->
        <div class="form-group">
            <label for="LblUsuario">Usuario:</label>
            <asp:Label ID="LblUsuario" runat="server" CssClass="form-control"></asp:Label>
        </div>

        <asp:Label ID="LblMsj" runat="server" Text="" ForeColor="Red"></asp:Label>
        <div class="form-group">
            <label for="TxtFechaIngreso">Fecha de Ingreso:</label>
            <asp:TextBox ID="TxtFechaIngreso" runat="server" CssClass="form-control" placeholder="YYYY-MM-DD"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="TxtDuracion">Duración:</label>
            <asp:TextBox ID="TxtDuracion" runat="server" CssClass="form-control" placeholder="HH:MM:SS"></asp:TextBox>
        </div>
        <div class="form-group">
            <label for="TxtDispositivo">Dispositivo:</label>
            <asp:TextBox ID="TxtDispositivo" runat="server" CssClass="form-control" placeholder="Ej: Laptop"></asp:TextBox>
        </div>

        <!-- Campo de búsqueda para materiales educativos -->
        <div class="form-group">
            <label for="TxtBuscarMaterial">Buscar Material Educativo:</label>
            <asp:TextBox ID="TxtBuscarMaterial" runat="server" CssClass="form-control" placeholder="Escribe el título del material"></asp:TextBox>
        </div>

        <!-- Botones de acción -->
        <div>
            <asp:Button ID="BtnGuardar" runat="server" CssClass="btn btn-primary" Text="Guardar" OnClick="BtnGuardar_Click" />
            <asp:Button ID="BtnActualizar" runat="server" CssClass="btn btn-success" Text="Actualizar" OnClick="BtnActualizar_Click" />
            <asp:Button ID="BtnEliminar" runat="server" CssClass="btn btn-danger" Text="Eliminar" OnClick="BtnEliminar_Click" />
            <asp:Button ID="BtnLimpiar" runat="server" CssClass="btn btn-secondary" Text="Limpiar" OnClick="BtnLimpiar_Click" />
        </div>
    </div>

    <hr />

    <!-- GridView para mostrar las visitas del usuario logueado -->
    <asp:GridView ID="GVVisitas" runat="server" CssClass="table table-striped" AutoGenerateColumns="False" OnSelectedIndexChanged="GVVisitas_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="vis_id" HeaderText="ID" />
            <asp:BoundField DataField="vis_fecha_ingreso" HeaderText="Fecha Ingreso" />
            <asp:BoundField DataField="vis_duracion" HeaderText="Duración" />
            <asp:BoundField DataField="vis_dispositivo" HeaderText="Dispositivo" />
            <asp:BoundField DataField="mat_titulo" HeaderText="Material Educativo" />
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
    </asp:GridView>

    <!-- Script para inicializar el autocompletado -->
   <script type="text/javascript">
       $(document).ready(function () {
           // Configura el autocompletado
           $("#<%= TxtBuscarMaterial.ClientID %>").autocomplete({
            source: function (request, response) {
                // Llama a un WebMethod para obtener los materiales que coincidan con la búsqueda
                $.ajax({
                    url: "WFVisits.aspx/GetMateriales",
                    data: JSON.stringify({ term: request.term }),
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item.mat_titulo, // Texto que se muestra en la lista
                                value: item.mat_titulo, // Valor que se muestra en el campo de búsqueda
                                id: item.mat_id // ID que se asigna al campo oculto
                            };
                        }));
                    },
                    error: function (xhr, status, error) {
                        console.log("Error en la búsqueda: " + error);
                    }
                });
            },
            select: function (event, ui) {
                // Cuando se selecciona un material, asigna el ID al campo oculto y el nombre al campo de búsqueda
                $("#<%= HdnMaterialId.ClientID %>").val(ui.item.id); // Asignar el ID
                $("#<%= TxtBuscarMaterial.ClientID %>").val(ui.item.value); // Mostrar el nombre
            }
        });
    });
   </script>
</asp:Content>
