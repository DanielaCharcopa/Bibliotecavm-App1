<%@ Page Title="" Language="C#" MasterPageFile="~/MainAdmin.Master" AutoEventWireup="true" CodeBehind="WFCategory.aspx.cs" Inherits="Presentation.WFCategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .btn-custom {
            margin-right: 8px;
            width: 150px;
        }

        .table thead {
            background-color: #343a40;
            color: white;
        }

        .form-section {
            max-width: 10000px;
            margin-bottom: 40px;
        }

        h2 {
            margin-top: 30px;
            margin-bottom: 25px;
        }

        textarea.form-control {
            resize: none;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">

        <h2>Gestión de Categorías</h2>

        <div class="form-section">
            <asp:HiddenField ID="HFCategoryId" runat="server" />

            <div class="form-group">
                <asp:Label ID="LblName" runat="server" Text="Nombre:"></asp:Label>
                <asp:DropDownList ID="DDLName" CssClass="form-control w-75" runat="server">
                    <asp:ListItem Text="Seleccione una categoría" Value="" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Libro" Value="Libro"></asp:ListItem>
                    <asp:ListItem Text="Cartilla" Value="Cartilla"></asp:ListItem>
                    <asp:ListItem Text="Folleto" Value="Folleto"></asp:ListItem>
                    <asp:ListItem Text="Guía Didactica" Value="Guía Didactica"></asp:ListItem>
                    <asp:ListItem Text="Juego Lúdico" Value="Juego Lúdico"></asp:ListItem>
                    <asp:ListItem Text="Pendón" Value="Pendón"></asp:ListItem>
                    <asp:ListItem Text="Multimedia" Value="Multimedia"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="form-group">
                <asp:Label ID="LblDescription" runat="server" Text="Descripción:"></asp:Label>
                <asp:TextBox ID="TBDescription" CssClass="form-control w-75" runat="server" TextMode="MultiLine" Rows="4"></asp:TextBox>
            </div>

            <div class="mt-3">
                <asp:Button ID="BtnSave" CssClass="btn btn-success btn-custom" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
                <asp:Button ID="BtnUpdate" CssClass="btn btn-warning btn-custom" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
                <asp:Button ID="BtnDelete" CssClass="btn btn-danger btn-custom" runat="server" Text="Eliminar" OnClick="BtnDelete_Click" />
            </div>

            <asp:Label ID="LblMessage" runat="server" CssClass="text-success mt-2 d-block" />
        </div>

        <h3>Categorías Registradas</h3>

        <asp:GridView ID="GVCategory" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False"
            OnSelectedIndexChanged="GVCategory_SelectedIndexChanged" DataKeyNames="cat_id">
            <Columns>
                <asp:BoundField DataField="cat_id" HeaderText="ID" />
                <asp:BoundField DataField="cat_nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="cat_descripcion" HeaderText="Descripción" />
                <asp:CommandField HeaderText="Opción" ShowSelectButton="True" SelectText="Seleccionar" ButtonType="Button" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
