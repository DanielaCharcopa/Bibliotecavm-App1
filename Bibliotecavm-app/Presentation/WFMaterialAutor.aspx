<%@ Page Title="" Language="C#" MasterPageFile="~/MainAdmin.Master" AutoEventWireup="true" CodeBehind="WFMaterialAutor.aspx.cs" Inherits="Presentation.WFMaterialAutor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-section {
            margin-bottom: 2rem;
        }

        .form-label {
            font-weight: bold;
        }

        .form-group {
            flex: 1 1 350px;
            min-width: 350px;
        }

        .btn-container {
            display: flex;
            justify-content: center;
            gap: 15px;
            margin: 30px 0;
        }

        /* PAGINACIÓN */
        .grid-pager {
            padding: 2rem 0;
            font-size: 1rem;
            text-align: center;
        }

        .grid-pager table {
            margin: 0 auto;
        }

        .grid-pager td {
            padding: 10px 8px;
        }

        .grid-pager a,
        .grid-pager span {
            display: inline-block;
            padding: 10px 14px;
            background-color: #f0f0f0;
            border: 1px solid #ccc;
            color: #007bff;
            text-decoration: none;
            border-radius: 8px;
            font-weight: bold;
            min-width: 40px;
            text-align: center;
            transition: all 0.3s ease;
        }

        .grid-pager a:hover {
            background-color: #007bff;
            color: white;
        }

        .grid-pager span {
            background-color: #007bff;
            color: white;
            border-color: #007bff;
        }

        .grid-pager .aspNetDisabled {
            background-color: #e9ecef;
            color: #999;
            pointer-events: none;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h2 class="mb-4 text-center">Gestión De Material Autor</h2>

        <!-- FORMULARIO -->
        <asp:HiddenField ID="HFMaterialAutorID" runat="server" />
        <div class="form-section d-flex justify-content-center align-items-start gap-4 flex-wrap" runat="server">
            <div class="form-group">
                <label class="form-label">Material Educativo</label>
                <asp:DropDownList ID="DDLMatEdu" runat="server" CssClass="form-select form-select-lg" />
            </div>

            <div class="form-group">
                <label class="form-label">Autor</label>
                <asp:DropDownList ID="DDLAutor" runat="server" CssClass="form-select form-select-lg" />
            </div>

            <div class="form-group">
                <label class="form-label">Descripción</label>
                <asp:TextBox ID="TBDescription" runat="server" CssClass="form-control form-control-lg" />
            </div>
        </div>

        <!-- BOTONES DE ACCIÓN -->
        <div class="btn-container">
            <asp:LinkButton ID="BtnSave" runat="server" OnClick="BtnSave_Click" CssClass="btn btn-success" OnClientClick="return confirm('¿Confirmar Guardar?');">
                <i class="fas fa-save"></i> Guardar
            </asp:LinkButton>

            <asp:LinkButton ID="BtnUpdate" runat="server" OnClick="BtnUpdate_Click" CssClass="btn btn-primary btn-hidden" OnClientClick="return confirm('¿Confirmar actualización?');">
                <i class="fas fa-sync-alt"></i> Actualizar
            </asp:LinkButton>

            <asp:LinkButton ID="BtnDelete" runat="server" OnClick="BtnDelete_Click" CssClass="btn btn-danger btn-hidden" OnClientClick="return confirm('¿Eliminar esta solicitud?');">
                <i class="fas fa-trash-alt"></i> Eliminar
            </asp:LinkButton>

            <asp:LinkButton ID="BtnIrAPresentacion" runat="server" OnClick="BtnIrAPresentacion_Click" CssClass="btn btn-info">
                <i class="fas fa-eye"></i> Ver Presentación
            </asp:LinkButton>
        </div>

        <!-- MENSAJE -->
        <asp:Label ID="LblMessage" runat="server" ForeColor="Red" CssClass="d-block text-center fw-bold mb-3" />

        <!-- BÚSQUEDA -->
        <div class="d-flex align-items-center mb-4 justify-content-center">
            <asp:TextBox ID="TBSearch" runat="server" CssClass="form-control form-control-lg me-2 w-75" placeholder="Buscar Material Autor" />
            <asp:Button ID="BtnSearch" runat="server" Text="Buscar" OnClick="BtnSearch_Click" CssClass="btn btn-primary btn-lg" ValidationGroup="SearchGroup" />
        </div>

        <!-- PLACEHOLDER -->
        <asp:PlaceHolder ID="phContenedor" runat="server"></asp:PlaceHolder>

        <!-- TABLA -->
        <h3 class="text-center">Materiales Autor Registrados</h3>
        <div class="table-responsive">
            <asp:GridView ID="GVMaterialAutor" runat="server"
                AutoGenerateColumns="False"
                DataKeyNames="id_material_autores"
                AllowPaging="true" PageSize="8"
                OnSelectedIndexChanged="GVMaterialAutor_SelectedIndexChanged"
                OnPageIndexChanging="GVMaterialAutor_PageIndexChanging"
                CssClass="table table-bordered table-hover align-middle text-center table-striped"
                HeaderStyle-CssClass="table-dark text-white text-center fw-bold"
                PagerStyle-CssClass="text-center grid-pager"
                PagerStyle-HorizontalAlign="Center"
                PagerSettings-Mode="Numeric">

                <Columns>
                    <asp:BoundField DataField="id_material_autores" HeaderText="ID" />
                    <asp:BoundField DataField="material_titulo" HeaderText="Material Educativo" />
                    <asp:BoundField DataField="nombre_autor" HeaderText="Autores" />
                    <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                    <asp:CommandField 
                        ShowSelectButton="True"
                        HeaderText="Opción"
                        SelectText="Seleccionar"
                        ButtonType="Button"
                        ControlStyle-CssClass="btn btn-sm btn-outline-primary"
                        HeaderStyle-CssClass="table-dark text-center"
                        ItemStyle-CssClass="text-center align-middle" />
                </Columns>
            </asp:GridView>
            <asp:Label ID="LblSearchResult" runat="server" CssClass="fw-bold d-block text-center mt-3" />

        </div>
    </div>
</asp:Content>
