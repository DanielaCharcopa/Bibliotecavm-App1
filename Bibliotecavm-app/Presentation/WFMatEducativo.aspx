<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" 
    CodeBehind="WFMatEducativo.aspx.cs" Inherits="Presentation.WFMatEducativo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Material Educativo</h2>

    <%--Campo de búsqueda y botón--%>
    <div>
        <asp:TextBox ID="TBSearch" runat="server" placeholder="Buscar Material Educativo" />
        <asp:Button ID="BtnSearch" runat="server" Text="Buscar" OnClick="BtnSearch_Click" 
            CssClass="btn btn-primary" ValidationGroup="SearchGroup" />
    </div>

    <%--Formulario para Crear o Editar Material Educativo--%>
    <div>
        <%--Control HiddenField para almacenar el ID del Material--%>
        <asp:HiddenField ID="HFMaterialID" runat="server" />

        <asp:TextBox ID="TBTitulo" runat="server" placeholder="Título Del Material Educativo" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvTitulo" runat="server" ControlToValidate="TBTitulo"
            ErrorMessage="El título es obligatorio." ForeColor="Red" Display="Dynamic" 
            ValidationGroup="FormGroup" />

    <asp:TextBox ID="TBAnopublicado" runat="server" CssClass="form-control" placeholder="Año de Publicación" />
<asp:RequiredFieldValidator ID="rfvAnopublicado" runat="server" ControlToValidate="TBAnopublicado"
    ErrorMessage="El año de publicación es obligatorio." ForeColor="Red" Display="Dynamic" 
    ValidationGroup="FormGroup" />
<asp:RegularExpressionValidator ID="revAnopublicado" runat="server" ControlToValidate="TBAnopublicado"
    ValidationExpression="^\d{4}$" ErrorMessage="El año debe tener 4 dígitos."
    ForeColor="Red" Display="Dynamic" ValidationGroup="FormGroup" />
<asp:RangeValidator ID="rvAnopublicado" runat="server" ControlToValidate="TBAnopublicado"
    MinimumValue="1000" MaximumValue="2100" Type="Integer"
    ErrorMessage="El año debe estar entre 1000 y 2100." ForeColor="Red" Display="Dynamic"
    ValidationGroup="FormGroup" />

        <asp:TextBox ID="TBUrl" runat="server" placeholder="URL" CssClass="form-control" />
        <asp:TextBox ID="TBPrecio" runat="server" placeholder="Precio" CssClass="form-control" />
        <asp:TextBox ID="TBKeywords" runat="server" placeholder="Palabras Clave" CssClass="form-control" />

        <asp:DropDownList ID="DDLFormato" runat="server" CssClass="form-control">
            <asp:ListItem Text="Seleccione un formato" Value="0" />
            <asp:ListItem Text="PDF" Value="PDF" />
            <asp:ListItem Text="Epub" Value="Epub" />
            <asp:ListItem Text="Video" Value="Video" />
            <asp:ListItem Text="Audio" Value="Audio" />
            <asp:ListItem Text="Otro" Value="Otro" />
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvFormato" runat="server" ControlToValidate="DDLFormato"
            InitialValue="0" ErrorMessage="Debe seleccionar un formato." ForeColor="Red" Display="Dynamic" 
            ValidationGroup="FormGroup" />

          <asp:DropDownList ID="DDLEditorial" runat="server" CssClass="form-control">
      <asp:ListItem Text="Seleccione una editorial" Value="0" />
  </asp:DropDownList>
  <asp:RequiredFieldValidator ID="rfvEditorial" runat="server" ControlToValidate="DDLEditorial"
      InitialValue="0" ErrorMessage="Debe seleccionar una editorial." ForeColor="Red" Display="Dynamic" 
      ValidationGroup="FormGroup" />

        <asp:DropDownList ID="DDLCategories" runat="server" CssClass="form-control">
            <asp:ListItem Text="Seleccione una categoría" Value="0" />
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvCategoria" runat="server" ControlToValidate="DDLCategories"
            InitialValue="0" ErrorMessage="Debe seleccionar una categoría." ForeColor="Red" Display="Dynamic" 
            ValidationGroup="FormGroup" />

      

        
        <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" 
            CssClass="btn btn-warning" ValidationGroup="FormGroup" />
        <asp:Button ID="BtnDelete" runat="server" Text="Eliminar" OnClick="BtnDelete_Click" 
            CssClass="btn btn-danger" ValidationGroup="FormGroup" />

        <asp:Label ID="LblMsj" runat="server" ForeColor="Red" />
    </div>

    <%--GridView para mostrar todos los materiales educativos--%>
    <asp:GridView 
        ID="GVMaterial" 
        runat="server" 
        AutoGenerateColumns="False" 
        OnSelectedIndexChanged="GVMaterial_SelectedIndexChanged" 
        DataKeyNames="mat_id"
        CssClass="grid-view">
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
            <asp:CommandField ShowSelectButton="True" HeaderText="Opción" ControlStyle-CssClass="btn btn-info" />
        </Columns>
    </asp:GridView>
</asp:Content>