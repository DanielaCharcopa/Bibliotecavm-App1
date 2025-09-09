<%@ Page Title="" Language="C#" MasterPageFile="~/MainAdmin.Master" AutoEventWireup="true" CodeBehind="WFCategory.aspx.cs" Inherits="Presentation.WFCategory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .form-section {
            max-width: 800px;
            margin: 0 auto 40px auto;
        }

        .form-label {
            font-weight: bold;
        }

        textarea.form-control {
            resize: none;
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
        }
       
        .form-group {
    flex: 4 4 350px; /* Cada campo ocupa mínimo 250px y se adapta */
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

  <h2 class="mb-4 text-center">Gestión De Categorías</h2>

        <!-- Formulario -->
        <div class="form-section d-flex justify-content-center gap-3 flex-wrap">
            <asp:HiddenField ID="HFCategoryId" runat="server" />

            <div class="form-group">
                <label class="form-label">Nombre</label>
                <asp:DropDownList ID="DDLName" CssClass="form-select" runat="server">
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
                <label class="form-label">Descripción</label>
                <asp:TextBox ID="TBDescription" runat="server" CssClass="form-control" />
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

            <asp:LinkButton ID="BtnDelete" runat="server"
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
    <asp:TextBox ID="TBSearch" runat="server" CssClass="form-control me-2 w-80" placeholder="Buscar Categoria" />
   <asp:Button ID="BtnSearch" runat="server" Text="Buscar" OnClick="BtnSearch_Click" CssClass="btn btn-primary" ValidationGroup="SearchGroup" />
<br />
    <asp:Label ID="LblSearchResult" runat="server" CssClass="text-info fw-bold mb-3" />

</div>

        <!-- Tabla de categorías -->
        <h3  class="text-center">Categorías Registradas</h3>

        <div class="table-responsive">
            <asp:GridView ID="GVCategory" runat="server"
        AutoGenerateColumns="False"
        DataKeyNames="cat_id"
        AllowPaging="true"
        PageSize="8"
        CssClass="table table-bordered table-hover align-middle text-center table-striped grid-view-improved"
        HeaderStyle-CssClass="table-dark text-white text-center fw-bold"
        PagerStyle-CssClass="text-center pager-custom grid-pager"
        PagerStyle-HorizontalAlign="Center"
        PagerSettings-Mode="Numeric"
        OnSelectedIndexChanged="GVCategory_SelectedIndexChanged"
        OnPageIndexChanging="GVCategory_PageIndexChanging">

                <Columns>
                    <asp:BoundField DataField="cat_id" HeaderText="ID" />
                    <asp:BoundField DataField="cat_nombre" HeaderText="Nombre" />
                    <asp:BoundField DataField="cat_descripcion" HeaderText="Descripción" />
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
