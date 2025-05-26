<%@ Page Title="" Language="C#" MasterPageFile="~/MainAdmin.Master" AutoEventWireup="true" CodeBehind="WFMaterialAutor.aspx.cs" Inherits="Presentation.WFMaterialAutor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-section {
            margin-bottom: 2rem;
        }

        .form-label {
            font-weight: bold;
        }

        .action-buttons .btn {
            margin-right: 0.5rem;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2 class="mb-4">Material Autor</h2>
    </div>
    <%-- Búsqueda --%>
    <div class="mb-4 d-flex">
        <asp:TextBox ID="TBSearch" runat="server" CssClass="form-control me-2" placeholder="Buscar Material Educativo" />
        <asp:Button ID="BtnSearch" runat="server" Text="Buscar" OnClick="BtnSearch_Click"
            CssClass="btn btn-primary" ValidationGroup="SearchGroup" />
    </div>

    <%-- Formulario --%>

  <div class="form-section d-flex align-items-start gap-3 flex-wrap" runat="server">
    <asp:HiddenField ID="HFMaterialAutorID" runat="server" />

    <div class="form-group">
        <label class="form-label">Material Educativo</label>
        <asp:DropDownList ID="DDLMatEdu" runat="server" CssClass="form-select">
            <asp:ListItem Text="Seleccione un Material" Value="0" />
        </asp:DropDownList>
    </div>

    <div class="form-group">
        <label class="form-label">Autor</label>
        <asp:DropDownList ID="DDLAutor" runat="server" CssClass="form-select">
            <asp:ListItem Text="Seleccione un Autor" Value="0" />
        </asp:DropDownList>
    </div>

    <div class="form-group">
        <label class="form-label">Descripción</label>
        <asp:TextBox ID="TBDescription" runat="server" CssClass="form-control" />
    </div>
</div>

<asp:PlaceHolder ID="phContenedor" runat="server"></asp:PlaceHolder>    








    <%-- Botones de Acción --%>

    <div>

      <asp:Button ID="BtnAgregarFormulario" runat="server" Text="Agregar Material Autor"
    CssClass="btn btn-primary " OnClick="BtnAgregarFormulario_Click" />

        <asp:Button ID="BtnSaveAll" runat="server" Text="Guardar " OnClick="BtnSaveAll_Click"
            CssClass="btn btn-success" />

        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click"
            CssClass="btn btn-warning text-white" />

        <asp:Button ID="BtnDelete" runat="server" Text="Eliminar" OnClick="BtnDelete_Click"
            CssClass="btn btn-danger" />
    </div>

    <asp:Label ID="LblMessage" runat="server" CssClass="text-success" />

    <%-- Tabla de registros --%>
    <div class="table-responsive">

        <asp:GridView ID="GVMaterialAutor" runat="server" AutoGenerateColumns="False"
            OnSelectedIndexChanged="GVMaterialAutor_SelectedIndexChanged"
            DataKeyNames="id_material_autores"
            CssClass="table table-striped table-bordered table-hover align-middle text-center">
            <Columns>
                <asp:BoundField DataField="id_material_autores" HeaderText="ID" />
                <asp:BoundField DataField="tbl_material_edu_mat_id" HeaderText="Material Educativo" />
                <asp:BoundField DataField="tbl_autores_au_id" HeaderText="Autores" />
                <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                <asp:CommandField ShowSelectButton="True" HeaderText="Opción" ControlStyle-CssClass="btn btn-info" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
