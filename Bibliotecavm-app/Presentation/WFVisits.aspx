<%@ Page Title="" Language="C#" MasterPageFile="~/MainUsuario.Master" AutoEventWireup="true" CodeBehind="WFVisits.aspx.cs" Inherits="Presentation.WFVisits" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <%--Dependencias de jQuery (si las necesitas para otras funcionalidades)--%> 
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Gestión de Visitas</h2>

     <%--Campos ocultos para almacenar información--%> 

    <asp:HiddenField ID="HdnMaterialId" runat="server" />
    <asp:HiddenField ID="HdnMaterialTitulo" runat="server" />

     <%--Sección para mostrar el usuario logueado--%> 
    <div class="form-group">
        <label for="LblUsuario">Usuario:</label>
        <asp:Label ID="LblUsuario" runat="server" CssClass="form-control"></asp:Label>
    </div>

     <%--Mensajes de error o información--%> 
    <asp:Label ID="LblMsj" runat="server" Text="" ForeColor="Red"></asp:Label>

     Tabla para mostrar las visitas del usuario 
    <div class="table-responsive">
        <asp:GridView ID="GVVisitas" runat="server" CssClass="table table-striped table-bordered"
            AutoGenerateColumns="False" DataKeyNames="vis_id"
            EmptyDataText="No hay visitas registradas.">
            <Columns>
                <%--asp:BoundField DataField="vis_id" HeaderText="ID"--%> 
                <asp:BoundField DataField="vis_fecha_ingreso" HeaderText="Fecha Ingreso" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="vis_duracion" HeaderText="Duración" />
                <asp:BoundField DataField="usuario_nombre" HeaderText="Nombre Usuario" />
                <asp:BoundField DataField="material_titulo" HeaderText="Material Educativo" />
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
