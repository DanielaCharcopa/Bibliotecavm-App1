<%@ Page Title="" Language="C#" MasterPageFile="~/MainUsuario.Master" AutoEventWireup="true" CodeBehind="WFPurchaseRequest.aspx.cs" Inherits="Presentation.WFPurchaseRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Registro de Compras</h1>
    
    <asp:Label ID="LBLUser" runat="server" Text="Usuario:"></asp:Label>
    <br />
    <asp:Label ID="LblMsj" runat="server" ForeColor="Red"></asp:Label>
    <asp:HiddenField ID="HFPurchaId" runat="server" />

    <table>
        <tr>
            <td><asp:Label ID="LblTicket" runat="server" Text="Ticket:"></asp:Label></td>
            <td><asp:TextBox ID="TBTicket" runat="server" ReadOnly="true" CssClass="disabled-input"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Label ID="LblFecha" runat="server" Text="Fecha:"></asp:Label></td>
            <td><asp:TextBox ID="TBFecha" runat="server" TextMode="Date"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Label ID="LblMaterial" runat="server" Text="Material Seleccionado:"></asp:Label></td>
            <td>
                <asp:TextBox ID="TxtMaterialSeleccionado" runat="server" ReadOnly="true"></asp:TextBox>
                <asp:HiddenField ID="HdnMaterialId" runat="server" />
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="LblQuantity" runat="server" Text="Cantidad:"></asp:Label></td>
            <td>
                <asp:TextBox ID="TBQuantity" runat="server" TextMode="Number" AutoPostBack="true" OnTextChanged="TBQuantity_TextChanged"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td><asp:Label ID="LblUnitPrice" runat="server" Text="Precio Unitario:"></asp:Label></td>
            <td><asp:TextBox ID="TBUnitPrice" runat="server" ReadOnly="true"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Label ID="LblTotal" runat="server" Text="Total:"></asp:Label></td>
            <td><asp:TextBox ID="TBTotal" runat="server" ReadOnly="true"></asp:TextBox></td>
        </tr>
    </table>
    
    <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
    <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
    <asp:Button ID="BtnDelete" runat="server" Text="Eliminar" OnClick="BtnDelete_Click" />

  <asp:GridView ID="GVRequests" runat="server" AutoGenerateColumns="False" 
       OnSelectedIndexChanged="GVRequests_SelectedIndexChanged" DataKeyNames="solic_id">
    <Columns>
        <asp:BoundField DataField="solic_id" HeaderText="ID" />
        <asp:BoundField DataField="solic_ticket" HeaderText="Ticket" />
        <asp:BoundField DataField="solic_fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
        <asp:BoundField DataField="usuario_nombre" HeaderText="Usuario" />
        <asp:BoundField DataField="solic_cantidad" HeaderText="Cantidad" />
        <asp:BoundField DataField="material_titulo" HeaderText="Material" />
        <%--<asp:BoundField DataField="precio_unitario" HeaderText="Precio Unitario" DataFormatString="{0:C2}" />--%>
        <asp:BoundField DataField="solic_valor_total" HeaderText="Total" DataFormatString="{0:C2}" />
        <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar" />
    </Columns>
</asp:GridView>

    <hr />

    <!-- Campo de búsqueda para materiales educativos -->
    <div class="form-group">
        <label for="TxtBuscarMaterial">Buscar Material Educativo:</label>
        <asp:TextBox ID="TxtBuscarMaterial" runat="server" CssClass="form-control"
            placeholder="Escribe el título del material y presiona Enter" AutoPostBack="true" 
            OnTextChanged="TxtBuscarMaterial_TextChanged"></asp:TextBox>
    </div>

    <!-- GridView para mostrar los materiales educativos -->
  <asp:GridView ID="GVMateriales" runat="server" AutoGenerateColumns="False" 
    OnSelectedIndexChanged="GVMateriales_SelectedIndexChanged" DataKeyNames="mat_id">
    <Columns>
        <asp:BoundField DataField="mat_id" HeaderText="ID" />
        <asp:BoundField DataField="mat_titulo" HeaderText="Título" /> 
        <asp:BoundField DataField="mat_precio" HeaderText="Precio" DataFormatString="{0:C2}" />
        <asp:CommandField ShowSelectButton="True" />
    </Columns>
</asp:GridView>

</asp:Content>
