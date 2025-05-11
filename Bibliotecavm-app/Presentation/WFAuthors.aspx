<%@ Page Title="" Language="C#" MasterPageFile="~/MainAdmin.Master" AutoEventWireup="true" CodeBehind="WFAuthors.aspx.cs" Inherits="Presentation.WFAuthors" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
       .btn-custom {
            margin-right: 8px;
        }

        .table thead {
            background-color: #343a40;
            color: white;
        }

        .form-section {
            max-width: 8000px;
            margin-bottom: 40px;
        }

        h2, h3 {
            margin-top: 30px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">

        <h2 class="mb-4">Registro de Autor</h2>

        <asp:HiddenField ID="HFAuthorsId" runat="server" />
        <asp:Label ID="LblMsj" runat="server" CssClass="text-success font-weight-bold" />

        <div class="form-section">
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

            <div class="mt-3">
                <asp:Button ID="BtnSave" runat="server" Text="Guardar" CssClass="btn btn-success btn-custom" OnClick="BtnSave_Click" />
                <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" CssClass="btn btn-warning btn-custom" OnClick="BtnUpdate_Click" />
                <asp:Button ID="BtnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger btn-custom" OnClick="BtnDelete_Click" />
            </div>

            <asp:Label ID="LblMessage" runat="server" CssClass="text-success mt-2" />
        </div>

        <h3>Autores Registrados</h3>

        <asp:GridView ID="GVAuthors" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False"
            OnSelectedIndexChanged="GVAuthors_SelectedIndexChanged" DataKeyNames="au_id">
            <Columns>
                <asp:BoundField DataField="au_id" HeaderText="ID Autor" SortExpression="au_id" />
                <asp:BoundField DataField="au_nombre" HeaderText="Nombre" SortExpression="au_nombre" />
                <asp:BoundField DataField="au_apellido" HeaderText="Apellido" SortExpression="au_apellido" />
                <asp:BoundField DataField="au_municipio" HeaderText="Municipio" SortExpression="au_municipio" />
                <asp:CommandField HeaderText="Opción" ShowSelectButton="True" SelectText="Seleccionar" ButtonType="Button" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
