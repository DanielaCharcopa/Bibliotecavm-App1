<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFEditorial.aspx.cs" Inherits="Presentation.WFEditorial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Gestión de Editoriales</h2>

    <%-- Formulario para agregar o editar Editorial --%>
    <div>
        <asp:HiddenField ID="HFEditorialId" runat="server" />
        <asp:Label ID="LblName" runat="server" Text="Nombre:"></asp:Label>
        <asp:TextBox ID="TBName" CssClass="form-control" runat="server"></asp:TextBox>

        <asp:Label ID="LblCity" runat="server" Text="Ciudad:"></asp:Label>
        <asp:TextBox ID="TBCity" CssClass="form-control" runat="server"></asp:TextBox>

        <asp:Label ID="LblPhone" runat="server" Text="Teléfono:"></asp:Label>
        <asp:TextBox ID="TBPhone" CssClass="form-control" runat="server"></asp:TextBox>

        <asp:Label ID="LblEmail" runat="server" Text="Correo:"></asp:Label>
        <asp:TextBox ID="TBEmail" CssClass="form-control" runat="server"></asp:TextBox>
        <br />

        <asp:Button ID="BtnSave" CssClass="btn btn-primary" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" CssClass="btn btn-warning" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
        <asp:Button ID="BtnDelete" CssClass="btn btn-danger" runat="server" Text="Eliminar" OnClick="BtnDelete_Click" />
        <asp:Label ID="LblMessage" runat="server" Text="" ForeColor="green"></asp:Label>
    </div>

    <div>
        <%-- Grid para mostrar Editoriales --%>
        <asp:GridView ID="GVEditorial" runat="server" AutoGenerateColumns="False"
            OnSelectedIndexChanged="GVEditorial_SelectedIndexChanged"
            DataKeyNames="edi_id">
            <Columns>
                <asp:BoundField DataField="edi_id" HeaderText="ID" />
                <asp:BoundField DataField="edi_nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="edi_ciudad" HeaderText="Ciudad" />
                <asp:BoundField DataField="edi_telefono" HeaderText="Teléfono" />
                <asp:BoundField DataField="edi_correo" HeaderText="Correo" />
                <asp:CommandField HeaderText="Opción" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
