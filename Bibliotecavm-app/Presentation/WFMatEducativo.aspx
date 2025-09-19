<%@ Page Title="" Language="C#" MasterPageFile="~/MainAdmin.Master" AutoEventWireup="true" CodeBehind="WFMatEducativo.aspx.cs" Inherits="Presentation.WFMatEducativo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <style>
        .form-label {
            font-weight: bold;
        }
        .btn-container {
    display: flex;
    justify-content: center; /* Centra horizontalmente */
    gap: 15px;               /* Espacio entre botones */
    margin: 30px 0;
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

   
     <h2 class="mb-4 text-center">Gestión De Material Educativo</h2>


   

    <!-- Formulario para Crear o Editar Material Educativo -->
    <asp:HiddenField ID="HFMaterialID" runat="server" />

    <div class="container">
        <div class="row mb-3">
            <div class="col-md-3">
                <label class="form-label">Título del Material</label>
                <asp:TextBox ID="TBTitulo" runat="server" CssClass="form-control" placeholder="Título del material educativo" />
                <asp:RequiredFieldValidator ID="rfvTitulo" runat="server" ControlToValidate="TBTitulo" ErrorMessage="Requerido" ForeColor="Red" Display="Dynamic" ValidationGroup="FormGroup" />
            </div>

            <div class="col-md-3">
                <label class="form-label">Año de Publicación</label>
                <asp:TextBox ID="TBAnopublicado" runat="server" CssClass="form-control" placeholder="Ej: 2024" />
                <asp:RequiredFieldValidator ID="rfvAnopublicado" runat="server" ControlToValidate="TBAnopublicado" ErrorMessage="Requerido" ForeColor="Red" Display="Dynamic" ValidationGroup="FormGroup" />
                <asp:RegularExpressionValidator ID="revAnopublicado" runat="server" ControlToValidate="TBAnopublicado" ValidationExpression="^\d{4}$" ErrorMessage="4 dígitos" ForeColor="Red" Display="Dynamic" ValidationGroup="FormGroup" />
                <asp:RangeValidator ID="rvAnopublicado" runat="server" ControlToValidate="TBAnopublicado" MinimumValue="1000" MaximumValue="2100" Type="Integer" ErrorMessage="Entre 1000 y 2100" ForeColor="Red" Display="Dynamic" ValidationGroup="FormGroup" />
            </div>

            <div class="col-md-3">
                <label class="form-label">URL</label>
                <asp:TextBox ID="TBUrl" runat="server" CssClass="form-control" placeholder="URL" />
            </div>

            <div class="col-md-3">
                <label class="form-label">Precio</label>
                <asp:TextBox ID="TBPrecio" runat="server" CssClass="form-control" placeholder="Precio" />
            </div>
        </div>

        <div class="row mb-3">
            <div class="col-md-3">
                <label class="form-label">Palabras Clave</label>
                <asp:TextBox ID="TBKeywords" runat="server" CssClass="form-control" placeholder="Palabras clave" />
            </div>

            <div class="col-md-3">
                <label class="form-label">Formato</label>
                <asp:DropDownList ID="DDLFormato" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Seleccione un formato" Value="0" />
                    <asp:ListItem Text="PDF" Value="PDF" />
                    <asp:ListItem Text="Epub" Value="Epub" />
                    <asp:ListItem Text="Video" Value="Video" />
                    <asp:ListItem Text="Audio" Value="Audio" />
                    <asp:ListItem Text="Otro" Value="Otro" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvFormato" runat="server" ControlToValidate="DDLFormato" InitialValue="0" ErrorMessage="Seleccione un formato." ForeColor="Red" Display="Dynamic" ValidationGroup="FormGroup" />
            </div>

            <div class="col-md-3">
                <label class="form-label">Editorial</label>
                <asp:DropDownList ID="DDLEditorial" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Seleccione una editorial" Value="0" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvEditorial" runat="server" ControlToValidate="DDLEditorial" InitialValue="0" ErrorMessage="Seleccione una editorial." ForeColor="Red" Display="Dynamic" ValidationGroup="FormGroup" />
            </div>

            <div class="col-md-3">
                <label class="form-label">Categoría</label>
                <asp:DropDownList ID="DDLCategories" runat="server" CssClass="form-control">
                    <asp:ListItem Text="Seleccione una categoría" Value="0" />
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfvCategoria" runat="server" ControlToValidate="DDLCategories" InitialValue="0" ErrorMessage="Seleccione una categoría." ForeColor="Red" Display="Dynamic" ValidationGroup="FormGroup" />
            </div>
        </div>

        <!-- Botones de acción -->
           <div class="btn-container">
    <asp:LinkButton ID="BtnSave" runat="server" OnClick="BtnSave_Click" CssClass= "btn btn-success" OnClientClick="return confirm('¿Confirmar Guardar?');">
        <i class="fas fa-save"></i> Guardar
    </asp:LinkButton>
    <asp:LinkButton ID="BtnUpdate" runat="server" OnClick="BtnUpdate_Click" CssClass="btn btn-primary btn-hidden" OnClientClick="return confirm('¿Confirmar actualización?');">
        <i class="fas fa-sync-alt"></i> Actualizar
    </asp:LinkButton>
    <asp:LinkButton ID="BtnDelete" runat="server" OnClick="BtnDelete_Click" CssClass="btn btn-danger btn-hidden" OnClientClick="return confirm('¿Eliminar esta solicitud?');" Visible="false">
        <i class="fas fa-trash-alt"></i> Eliminar
    </asp:LinkButton>
</div>

        <!-- Mensaje -->
         <asp:Label ID="LblMsj" runat="server" ForeColor="Red" CssClass="d-block text-center fw-bold mb-3" />

                                      <!-- Campo de búsqueda y botón -->
<div class="d-flex align-items-center mb-4">
    <asp:TextBox ID="TBSearch" runat="server" CssClass="form-control me-2 w-80" placeholder="Buscar Material Educativo" />
   <asp:Button ID="BtnSearch" runat="server" Text="Buscar" OnClick="BtnSearch_Click" CssClass="btn btn-primary" ValidationGroup="SearchGroup" />
<br />
    <asp:Label ID="LblSearchResult" runat="server" CssClass="text-info fw-bold mb-3" />

</div>
    <!-- Tabla de Material Educativo -->
    <br />
    <h3 class="text-center">Materiales Educativos Registrados</h3>
        <br /> 
    <div class="table-responsive">
        <asp:GridView
            ID="GVMaterial"
            runat="server"
            AutoGenerateColumns="False"
            OnSelectedIndexChanged="GVMaterial_SelectedIndexChanged"
              OnPageIndexChanging="GVMaterial_PageIndexChanging"
            DataKeyNames="mat_id" AllowPaging="true" PageSize="8"
               CssClass="table table-bordered table-hover align-middle text-center table-striped"
HeaderStyle-CssClass="table-dark text-white text-center fw-bold"
PagerStyle-CssClass="text-center pager-custom grid-pager"
            PagerStyle-HorizontalAling="Center"
            PagerSettings-Mode="Numeric">
            


            <Columns>
                <asp:BoundField DataField="mat_id" HeaderText="ID" SortExpression="mat_id" />
                <asp:BoundField DataField="mat_titulo" HeaderText="Título" SortExpression="mat_titulo" />
                <asp:BoundField DataField="mat_ano_publicacion" HeaderText="Año de Publicación" SortExpression="mat_ano_publicacion" />
                <asp:BoundField DataField="mat_url_descarga" HeaderText="URL" SortExpression="mat_url_descarga" />
                <asp:BoundField DataField="mat_precio" HeaderText="Precio" SortExpression="mat_precio" />
                <asp:BoundField DataField="mat_keywords" HeaderText="Palabras Clave" SortExpression="mat_keywords" />
                <asp:BoundField DataField="mat_formato" HeaderText="Formato" SortExpression="mat_formato" />
                <asp:BoundField DataField="editorial_nombre" HeaderText="Editorial" SortExpression="edi_nombre" />
                <asp:BoundField DataField="categoria_nombre" HeaderText="Categoría" SortExpression="cat_nombre" />
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

</asp:Content>
