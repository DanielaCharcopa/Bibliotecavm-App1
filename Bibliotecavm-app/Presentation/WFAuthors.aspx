<%@ Page Title="" Language="C#" MasterPageFile="~/MainAdmin.Master" AutoEventWireup="true" CodeBehind="WFAuthors.aspx.cs" Inherits="Presentation.WFAuthors" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .btn-container {
            display: flex;
            justify-content: center;
            gap: 15px;
            margin: 30px 0;
        }

        .form-section {
            max-width: 1000px;
            margin: 0 auto 40px auto;
        }

        .form-label {
            font-weight: bold;
        }

        .table thead {
            background-color: #443a80;
            color: white;
        }

        h2, h3 {
            margin-top: 30px;
        }
        .form-group {
            flex: 4 4 320px; /* Cada campo ocupa mínimo 320px y se adapta */
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
    <script>
    function hideMessageAfterDelay() {
        setTimeout(function () {
            var lbl = document.getElementById('<%= LblMessage.ClientID %>');
            if (lbl) {
                lbl.style.display = 'none';
            }
        }, 1000); // 3 segundos (ajusta el tiempo si quieres)
    }
</script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">

        <h2 class="mb-4 text-center">Gestión De Autor</h2>


        <asp:HiddenField ID="HFAuthorsId" runat="server" />

        <!-- Formulario de campos -->
        <div class="form-section d-flex justify-content-center align-items-start gap-3 flex-wrap">
            <div class="form-group">
                <label class="form-label">Nombre</label>
                <asp:TextBox ID="TBNombre" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label class="form-label">Apellido</label>
                <asp:TextBox ID="TBApellido" runat="server" CssClass="form-control" />
            </div>

            <div class="form-group">
                <label class="form-label">Municipio</label>
                <asp:TextBox ID="TBMunicipio" runat="server" CssClass="form-control" />
            </div>
        </div>

        <!-- Botones de acción -->
        <div class="btn-container">
            <asp:LinkButton ID="BtnSave" runat="server" OnClick="BtnSave_Click"
                CssClass="btn btn-success"
                OnClientClick="return confirm('¿Deseas Guardar Este Autor?');">
                <i class="fas fa-save"></i> Guardar
            </asp:LinkButton>

            <asp:LinkButton ID="BtnUpdate" runat="server" OnClick="BtnUpdate_Click"
                CssClass="btn btn-primary btn-hidden"
                OnClientClick="return confirm('¿Deseas Actualizar Este Autor?');">
                <i class="fas fa-sync-alt"></i> Actualizar
            </asp:LinkButton>

            <asp:LinkButton ID="BtnEliminar" runat="server" OnClick="BtnDelete_Click"
                CssClass="btn btn-danger btn-hidden"
                OnClientClick="return confirm('¿Deseas Eliminar Este Autor?');">
                <i class="fas fa-trash-alt"></i> Eliminar
            </asp:LinkButton>
        </div>

        <!-- Mensaje de estado -->
<asp:Label ID="LblMessage" runat="server" CssClass="fw-bold d-block mb-2 text-center" />

         <!-- Campo de búsqueda y botón -->
 <div class="d-flex align-items-center mb-4">
     <asp:TextBox ID="TBSearch" runat="server" CssClass="form-control me-2 w-80" placeholder="Buscar Autores" />
    <asp:Button ID="BtnSearch" runat="server" Text="Buscar" OnClick="BtnSearch_Click" CssClass="btn btn-primary" ValidationGroup="SearchGroup" />
 <br />
     <asp:Label ID="LblSearchResult" runat="server" CssClass="text-info fw-bold mb-3" />

 </div>

        <!-- Tabla de autores -->
       <h3 class="text-center">Autores Registrados</h3>
        <div class="table-responsive">
<asp:GridView ID="GVAuthors" runat="server"
    AutoGenerateColumns="False"
    DataKeyNames="au_id"
    AllowPaging="true"
    PageSize="8"
    CssClass="table table-bordered table-hover align-middle text-center table-striped grid-view-improved"
    HeaderStyle-CssClass="table-dark text-white text-center fw-bold"
    PagerStyle-CssClass="grid-pager"
    PagerStyle-HorizontalAlign="Center"
    PagerSettings-Mode="Numeric"
    OnSelectedIndexChanged="GVAuthors_SelectedIndexChanged"
    OnPageIndexChanging="GVAuthors_PageIndexChanging">

         <Columns>
                    <asp:BoundField DataField="au_id" HeaderText="ID Autor" SortExpression="au_id" />
                    <asp:BoundField DataField="au_nombre" HeaderText="Nombre" SortExpression="au_nombre" />
                    <asp:BoundField DataField="au_apellido" HeaderText="Apellido" SortExpression="au_apellido" />
                    <asp:BoundField DataField="au_municipio" HeaderText="Municipio" SortExpression="au_municipio" />
                    <asp:CommandField ShowSelectButton="True"
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
