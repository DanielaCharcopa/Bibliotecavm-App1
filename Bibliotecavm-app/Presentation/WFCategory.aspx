<%@ Page Title="" Language="C#" MasterPageFile="~/MainUsuario.Master" AutoEventWireup="true" CodeBehind="WFCategory.aspx.cs" Inherits="Presentation.WFCategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Gestión de Categorías</h2>

  <%-- Formulario para Agregar o Editar Categoría --%>
    <div>
        <asp:HiddenField ID="HFCategoryId" runat="server" />
        <%-- ID oculto para categorías --%>
        <asp:Label ID="LblName" runat="server" Text="Nombre:"></asp:Label>
        <asp:DropDownList ID="DDLName" CssClass="form-control" runat="server">
            <asp:ListItem Text="Seleccione una categoría" Value="" Selected="True"></asp:ListItem>
            <asp:ListItem Text="Libro" Value="Libro"></asp:ListItem>
            <asp:ListItem Text="Cartilla" Value="Cartilla"></asp:ListItem>
            <asp:ListItem Text="Folleto" Value="Folleto"></asp:ListItem>
            <asp:ListItem Text="Guía Didactica" Value="Guía Didactica"></asp:ListItem>
            <asp:ListItem Text="Juego Lúdico" Value="Juego Lúdico"></asp:ListItem>
            <asp:ListItem Text="Pendón" Value="Pendón"></asp:ListItem>
            <asp:ListItem Text="Multimedia" Value="Multimedia"></asp:ListItem>
        </asp:DropDownList>

        <asp:Label ID="LblDescription" runat="server" Text="Descripción:"></asp:Label>
        <asp:TextBox ID="TBDescription" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
        <br />
        <asp:Button ID="BtnSave" CssClass="form-control" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" CssClass="form-control" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
        <asp:Button ID="BtnDelete" CssClass="form-control" runat="server" Text="Eliminar" OnClick="BtnDelete_Click" />
        <asp:Label ID="LblMessage" runat="server" Text="" ForeColor="green"></asp:Label>
    </div>

    <div>
        <asp:GridView ID="GVCategory" runat="server" AutoGenerateColumns="False" 
            OnSelectedIndexChanged="GVCategory_SelectedIndexChanged" DataKeyNames="cat_id">
            <Columns>
                <asp:BoundField DataField="cat_id" HeaderText="ID" />
                <asp:BoundField DataField="cat_nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="cat_descripcion" HeaderText="Descripción" />
                <asp:CommandField HeaderText="Opción" ShowSelectButton="True" />

            </Columns>
        </asp:GridView>
    </div>
</asp:Content>