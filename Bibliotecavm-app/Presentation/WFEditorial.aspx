<%@ Page Title="" Language="C#" MasterPageFile="~/MainAdmin.Master" AutoEventWireup="true" CodeBehind="WFEditorial.aspx.cs" Inherits="Presentation.WFEditorial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .container {
            max-width: 900px; /* Limita ancho máximo para que quede centrado */
            margin: 0 auto;
        }

        .form-section {
            display: flex;
            flex-wrap: wrap;
            justify-content: center; /* Centra todos los campos */
            gap: 20px;
            margin-bottom: 40px;
        }

        .form-group {
            flex: 1 1 250px; /* Cada campo ocupa mínimo 250px y se adapta */
        }

        .form-label {
            font-weight: bold;
        }

        .btn-container {
            display: flex;
            justify-content: center;
            gap: 15px;
            margin: 30px 0;
        }

        .btn {
            display: inline-flex;
            align-items: center;
            justify-content: center;
            gap: 5px;
            width: 150px;
        }

        .table thead {
            background-color: #343a40;
            color: white;
        }

        h2, h3 {
            margin-top: 30px;
            text-align: center;
        }

           /* Diseño de paginación */
  /* Estilo para centrar los controles de paginación */
.grid-pager table {
    margin: 0 auto; /* Centra el pager table */
}

/* Estilo de los botones del paginador */
.grid-pager td {
    padding: 10px 8px;
}

.grid-pager a, .grid-pager span {
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

/* Hover */
.grid-pager a:hover {
    background-color: #007bff;
    color: white;
}

/* Página activa */
.grid-pager span {
    background-color: #007bff;
    color: white;
    border-color: #007bff;
}

/* Deshabilitados */
.grid-pager .aspNetDisabled {
    background-color: #e9ecef;
    color: #999;
    pointer-events: none;
}
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">

        <h2>Gestión De Editoriales</h2>

        <!-- Formulario centrado -->
        <asp:HiddenField ID="HFEditorialId" runat="server" />
        <div class="form-section" runat="server">
            <div class="form-group">
                <label class="form-label">Nombre</label>
                <asp:TextBox ID="TBName" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label class="form-label">Ciudad</label>
                <asp:TextBox ID="TBCity" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label class="form-label">Teléfono</label>
                <asp:TextBox ID="TBPhone" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label class="form-label">Correo</label>
                <asp:TextBox ID="TBEmail" runat="server" CssClass="form-control" />
            </div>
        </div>

        <!-- Botones -->
        <div class="btn-container">
            <asp:LinkButton ID="BtnSave" runat="server"
                CssClass="btn btn-success"
                OnClick="BtnSave_Click"
                OnClientClick="return confirm('¿Confirmar Guardar?');">
                <i class="fas fa-save"></i> Guardar
            </asp:LinkButton>

            <asp:LinkButton ID="BtnUpdate" runat="server"
                CssClass="btn btn-primary btn-hidden"
                OnClick="BtnUpdate_Click"
                OnClientClick="return confirm('¿Confirmar actualización?');">
                <i class="fas fa-sync-alt"></i> Actualizar
            </asp:LinkButton>

            <asp:LinkButton ID="BtnEliminar" runat="server"
                CssClass="btn btn-danger btn-hidden"
                OnClick="BtnDelete_Click"
                OnClientClick="return confirm('¿Eliminar esta solicitud?');">
                <i class="fas fa-trash-alt"></i> Eliminar
            </asp:LinkButton>
        </div>

        <!-- Mensaje de estado -->
        <asp:Label ID="LblMessage" runat="server" CssClass="text-success fw-bold d-block mb-2 text-center" />

                                    <!-- Campo de búsqueda y botón -->
<div class="d-flex align-items-center mb-4">
    <asp:TextBox ID="TBSearch" runat="server" CssClass="form-control me-2 w-80" placeholder="Buscar Editoriales" />
   <asp:Button ID="BtnSearch" runat="server" Text="Buscar" OnClick="BtnSearch_Click" CssClass="btn btn-primary" ValidationGroup="SearchGroup" />
<br />
    <asp:Label ID="LblSearchResult" runat="server" CssClass="text-info fw-bold mb-3" />

</div>

        <!-- Tabla de editoriales -->
        <h3>Editoriales Registradas</h3>
        <div class="table-responsive mt-4">
            <asp:GridView ID="GVEditorial" runat="server" AutoGenerateColumns="False"
                DataKeyNames="edi_id"
                AllowPaging="true" PageSize="8"
                CssClass="table table-bordered table-hover align-middle text-center table-striped grid-view-improved"
 HeaderStyle-CssClass="table-dark text-white text-center fw-bold"
PagerStyle-CssClass="text-center pager-custom grid-pager"
PagerStyle-HorizontalAlign="Center"
PagerSettings-Mode="Numeric"
                OnSelectedIndexChanged="GVEditorial_SelectedIndexChanged"
                 OnPageIndexChanging="GVEditorial_PageIndexChanging">

                <Columns>
                    <asp:BoundField DataField="edi_id" HeaderText="ID" />
                    <asp:BoundField DataField="edi_nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="edi_ciudad" HeaderText="Ciudad" />
                    <asp:BoundField DataField="edi_telefono" HeaderText="Teléfono" />
                    <asp:BoundField DataField="edi_correo" HeaderText="Correo" />
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
        </div>

    </div>
</asp:Content>
