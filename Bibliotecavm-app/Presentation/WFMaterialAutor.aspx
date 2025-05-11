<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFMaterialAutor.aspx.cs" Inherits="Presentation.WFMaterialAutor" %>
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
        <h2 class="mb-4">Material - Autor</h2>
</div>
        <%-- Búsqueda --%>
        <div class="mb-4 d-flex">
            <asp:TextBox ID="TBSearch" runat="server" CssClass="form-control me-2" placeholder="Buscar Material Educativo" />
            <asp:Button ID="BtnSearch" runat="server" Text="Buscar" OnClick="BtnSearch_Click" 
                        CssClass="btn btn-primary" ValidationGroup="SearchGroup" />
        </div>

        <%-- Formulario --%>
        <div class="form-section">
            <asp:HiddenField ID="HFMaterialAutorID" runat="server" />

            <div class="mb-3">
                <label class="form-label">Material Educativo</label>
                <asp:DropDownList ID="DDLMatEdu" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Seleccione un Material" Value="0" />
                </asp:DropDownList>
            </div>

            <div class="mb-3">
                <label class="form-label">Autor</label>
                <asp:DropDownList ID="DDLAutor" runat="server" CssClass="form-select">
                    <asp:ListItem Text="Seleccione un Autor" Value="0" />
                </asp:DropDownList>
            </div>

            <div class="mb-3">
                <label class="form-label" for="TBDescription">Descripción</label>
                <asp:TextBox ID="TBDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" />
            </div>
        </div>

        <%-- Botones de Acción --%>
        <div class="mb-4 action-buttons d-flex flex-wrap gap-2">
            <asp:Button ID="BtnAddEntry" runat="server" Text="Agregar Autor" OnClick="BtnAddEntry_Click"
                        CssClass="btn btn-secondary" />
            <br />

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
                    <asp:BoundField DataField="material_titulo" HeaderText="Material Educativo" />
                    <asp:BoundField DataField="nombre_autor" HeaderText="Autores" />
                    <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                    <asp:CommandField ShowSelectButton="True" HeaderText="Opción"  ControlStyle-CssClass="btn btn-info" /> 
                </Columns>
        </asp:GridView>
    </div>
</asp:Content>
