<%@ Page Title="Material Autor - Consulta" Language="C#" MasterPageFile="~/MainAdmin.Master" AutoEventWireup="true" CodeBehind="WFPresentationMatAutor.aspx.cs" Inherits="Presentation.WFPresentationMatAutor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        .search-panel {
            background-color: #f8f9fa;
            padding: 15px;
            border-radius: 5px;
            margin-bottom: 20px;
        }

        .grid-pager {
            padding: 2rem 0;
            font-size: 1rem;
            text-align: center;
        }

        .grid-pager table {
            margin: 0 auto;
        }

        .grid-pager a,
        .grid-pager span {
            margin: 0 8px;
            padding: 10px 18px;
            border-radius: 8px;
            border: 2px solid #007bff;
            color: #007bff;
            font-weight: bold;
            text-decoration: none;
            transition: background-color 0.3s ease;
            display: inline-block;
        }

        .grid-pager a:hover {
            background-color: #007bff;
            color: #fff;
        }

        .grid-pager span {
            background-color: #007bff;
            color: #fff;
            cursor: default;
        }

        /* Centrar títulos */
        h3 {
            text-align: center;
            margin-top: 2rem;
            margin-bottom: 1rem;
        }

        /* Buscador más grande y centrado */
        .buscador {
            max-width: 700px;
            margin: 0 auto 2rem auto;
        }

        .buscador .form-control {
            font-size: 1.2rem;
            padding: 12px 16px;
        }

        .buscador .btn {
            padding: 12px 20px;
            font-size: 1rem;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">

        <!-- Título principal -->
        <h3>Consulta de Material Autor</h3>

        
       <!-- Buscador más alto y menos ancho -->
<div class="d-flex justify-content-center align-items-center mb-4">
    <asp:TextBox ID="TBSearch" runat="server" CssClass="form-control text-center me-2" 
        Style="width: 5000px; height: 35px; font-size: 1.1rem;" placeholder="Buscar Material Autor" />
    <asp:Button ID="BtnSearch" runat="server" Text="Buscar" 
        OnClick="BtnSearch_Click" CssClass="btn btn-primary" ValidationGroup="SearchGroup"
        Style="height: 50px; font-size: 1rem;" />
</div>


        <!-- Mensaje -->
        <asp:Label ID="LblMensaje" runat="server" Text="" ForeColor="Red" CssClass="d-block mb-3 text-center fw-bold"></asp:Label>

        <!-- Título tabla -->
        <h3>Materiales Autor Agrupados</h3>

        <!-- Tabla -->
        <div class="table-responsive">
            <asp:GridView ID="GVMaterialAutor" runat="server" AutoGenerateColumns="False"
                AllowPaging="true" PageSize="8" DataKeyNames="ids_material_autores"
                CssClass="table table-bordered table-hover align-middle text-center table-striped"
                HeaderStyle-CssClass="table-dark text-white text-center fw-bold"
                PagerStyle-CssClass="text-center pager-custom" PagerStyle-HorizontalAlign="Center"
                PagerSettings-Mode="Numeric">
                <Columns>
                    <asp:BoundField DataField="nombre_autor" HeaderText="Autor" />
                    <asp:BoundField DataField="ids_material_autores" HeaderText="ID" Visible="False" />
                    <asp:BoundField DataField="materiales" HeaderText="Material Educativo" />
                    <asp:BoundField DataField="descripciones" HeaderText="Descripción" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
