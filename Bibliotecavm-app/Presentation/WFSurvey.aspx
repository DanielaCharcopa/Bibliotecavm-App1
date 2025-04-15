<%@ Page Title="Gestion de encuesta" Language="C#" MasterPageFile="~/MainAdmin.Master" AutoEventWireup="true" CodeBehind="WFSurvey.aspx.cs" Inherits="Presentation.WFSurvey" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /* Estilos para ocultar la columna ID pero mantener funcionalidad */
        .hidden-id-column {
            display: none !important;
            width: 0 !important;
            height: 0 !important;
            padding: 0 !important;
            margin: 0 !important;
            border: none !important;
        }
        
        /* Estilos generales para la página */
        .container-fluid {
            padding: 20px;
        }
        
        .form-control {
            margin-bottom: 10px;
        }
        
        .btn {
            margin-right: 8px;
            min-width: 100px;
        }
        
        .table-hover {
            margin-top: 20px;
            width: 100%;
            border-collapse: collapse;
        }
        
        .table-hover th {
            background-color: #f8f9fa;
            padding: 12px;
            text-align: left;
            border-bottom: 2px solid #dee2e6;
        }
        
        .table-hover td {
            padding: 10px;
            border-bottom: 1px solid #dee2e6;
        }
        
        .table-hover tr:hover {
            background-color: #f1f1f1;
        }
        
        .pagination a {
            padding: 6px 12px;
            margin: 0 3px;
            border: 1px solid #ddd;
            text-decoration: none;
            color: #007bff;
        }
        
        .pagination span {
            padding: 6px 12px;
            margin: 0 3px;
            border: 1px solid #007bff;
            background-color: #007bff;
            color: white;
        }
        
        #lblMessage {
            display: block;
            padding: 10px;
            margin-bottom: 15px;
            border-radius: 4px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <h1>Gestión de Encuestas</h1>
        
        <%-- Mensaje de alerta o éxito --%>
        <div class="row">
            <div class="col">
                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
            </div>
        </div>
        <br />

        <%-- Formulario para agregar o editar encuestas --%>
        <div class="row">
            <div class="col-md-6">
                <%-- Campo ID oculto --%>
                <asp:HiddenField ID="TBCode" runat="server" />
                
                <%-- Pregunta --%>
                <div class="form-group">
                    <asp:Label ID="lblPregunta" runat="server" Text="Pregunta:"></asp:Label>
                    <asp:TextBox ID="txtDescripcionPregunta" CssClass="form-control" runat="server" Width="100%" />
                </div>
                
                <%-- Botones para guardar, actualizar y eliminar encuesta --%>
                <div class="form-group">
                    <asp:Button ID="btnGuardarEncuesta" runat="server" CssClass="btn btn-success" Text="Guardar" OnClick="btnGuardarEncuesta_Click" />
                    <asp:Button ID="btnActualizarEncuesta" runat="server" CssClass="btn btn-primary" Text="Actualizar" OnClick="btnActualizarEncuesta_Click" />
                    <asp:Button ID="btnEliminarEncuesta" runat="server" CssClass="btn btn-danger" Text="Eliminar" OnClick="btnEliminarEncuesta_Click" />
                </div>
            </div>
        </div>
        <br />

        <%-- Listado de encuestas --%>
        <div class="row">
            <div class="col">
                <h3>Listado de Encuestas</h3>
                <asp:GridView ID="gvSurveys" CssClass="table table-hover" runat="server" 
                    AutoGenerateColumns="False" 
                    OnSelectedIndexChanged="gvSurveys_SelectedIndexChanged"
                    AllowPaging="True" 
                    PageSize="10" 
                    OnPageIndexChanging="gvSurveys_PageIndexChanging"
                    PagerStyle-CssClass="pagination" 
                    PagerSettings-Mode="NumericFirstLast"
                    PagerSettings-Position="Bottom"
                    PagerSettings-PageButtonCount="5">
                    <Columns>
                        <%-- Columna ID oculta visualmente pero funcional --%>
                        <asp:BoundField DataField="en_id" HeaderText="ID" 
                            ItemStyle-CssClass="hidden-id-column" 
                            HeaderStyle-CssClass="hidden-id-column" />
                            
                        <asp:BoundField DataField="en_descripcion_pregunta" HeaderText="Descripción" />
                        <asp:CommandField HeaderText="Acción" ShowSelectButton="True" SelectText="Seleccionar" 
                            ButtonType="Button" ControlStyle-CssClass="btn btn-sm btn-outline-primary" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>