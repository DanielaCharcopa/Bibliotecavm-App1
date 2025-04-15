<%@ Page Title="" Language="C#" MasterPageFile="~/MainAdmin.Master" AutoEventWireup="true" CodeBehind="WFAuthors.aspx.cs" Inherits="Presentation.WFAuthors" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <h2>Registro de Autor</h2>

         <asp:HiddenField ID="HFAuthorsId" runat="server" />

        <asp:Label ID="LblMsj" runat="server" ForeColor="Green" />


        <div class="form-group">
            <label for="TBNombre">Nombre:</label>
            <asp:TextBox ID="TBNombre" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="TBApellido">Apellido:</label>
            <asp:TextBox ID="TBApellido" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label for="TBMunicipio">Municipio:</label>
            <asp:TextBox ID="TBMunicipio" runat="server" CssClass="form-control" />
        </div>

        <asp:Button ID="BtnSave" runat="server" Text="Guardar" CssClass="btn" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" CssClass="btn" OnClick="BtnUpdate_Click" />
        <asp:Button ID="BtnEliminar" runat="server" Text="Eliminar" CssClass="btn" OnClick="BtnDelete_Click" />
         <asp:Label ID="LblMessage" runat="server" Text="" ForeColor="green"></asp:Label>


        <h3>Autores Registrados</h3>
        <asp:GridView ID="GVAuthors" runat="server" AutoGenerateColumns="False" 
            OnSelectedIndexChanged="GVAuthors_SelectedIndexChanged" DataKeyNames="au_id">
            <Columns>

                <asp:BoundField DataField="au_id" HeaderText="ID Autor" SortExpression="au_id" />
                <asp:BoundField DataField="au_nombre" HeaderText="Nombre" SortExpression="au_nombre" />
                <asp:BoundField DataField="au_apellido" HeaderText="Apellido" SortExpression="au_apellido" />
                <asp:BoundField DataField="au_municipio" HeaderText="Municipio" SortExpression="au_municipio" />
                <asp:CommandField HeaderText="Opción" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>