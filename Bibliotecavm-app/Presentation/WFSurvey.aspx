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
        
        /* GridView Mejorado - ESTILO ACTUALIZADO (igual al WFUserManagement) */
        .table-responsive {
            margin-top: 20px;
            overflow-x: auto;
            -webkit-overflow-scrolling: touch;
        }

        .table {
            width: 100%;
            margin-bottom: 1rem;
            color: #212529;
            border-collapse: collapse;
        }
        
        .table-bordered {
            border: 1px solid #dee2e6;
        }
        
        .table-bordered th,
        .table-bordered td {
            border: 1px solid #dee2e6;
        }
        
        .table-striped tbody tr:nth-of-type(odd) {
            background-color: rgba(0, 0, 0, 0.02);
        }
        
        .table-hover tbody tr:hover {
            background-color: rgba(78, 115, 223, 0.05);
        }
        
        .table-dark {
            background-color: #343a40;
            color: white;
        }
        
        .table-dark th {
            border-color: #454d55;
        }
        
        .align-middle {
            vertical-align: middle !important;
        }
        
        .text-center {
            text-align: center !important;
        }

        /* Estilo del botón Seleccionar (igual al WFUserManagement) */
        .btn-outline-primary {
            border: 1px solid #1a237e;
            color: #1a237e;
            background-color: transparent;
            padding: 6px 12px;
            border-radius: 4px;
            transition: all 0.3s;
        }
        
        .btn-outline-primary:hover {
            background-color: #1a237e;
            color: white;
            box-shadow: 0 0 0 0.2rem rgba(26,35,126,0.25);
        }
        
        .btn-sm {
            padding: 6px 12px;
            font-size: 0.875rem;
        }

        /* Paginación mejorada (igual al WFUserManagement) */
        .pagination {
            display: flex;
            justify-content: center;
            padding-left: 0;
            list-style: none;
            border-radius: 0.25rem;
            margin-top: 20px;
        }

        .pagination a {
            position: relative;
            display: block;
            padding: 0.5rem 0.75rem;
            margin-left: -1px;
            line-height: 1.25;
            color: #1a237e;
            background-color: #fff;
            border: 1px solid #dee2e6;
            text-decoration: none;
        }

        .pagination a:hover {
            color: #0d1533;
            background-color: #e9ecef;
            border-color: #dee2e6;
        }

        .pagination span {
            position: relative;
            display: block;
            padding: 0.5rem 0.75rem;
            margin-left: -1px;
            line-height: 1.25;
            color: #fff;
            background-color: #1a237e;
            border: 1px solid #1a237e;
        }
        
        #lblMessage {
            display: block;
            padding: 10px;
            margin-bottom: 15px;
            border-radius: 4px;
        }
        
        /* Responsive */
        @media (max-width: 768px) {
            .table th, 
            .table td {
                padding: 8px;
            }
            
            .pagination a, 
            .pagination span {
                padding: 0.25rem 0.5rem;
                margin: 0 2px;
                font-size: 0.9em;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <h3>Gestión de Encuestas</h3>
        
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
                <div class="table-responsive">
                    <asp:GridView ID="gvSurveys" runat="server" 
                        CssClass="table table-bordered table-striped table-hover" 
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
                                
                            <asp:BoundField DataField="en_descripcion_pregunta" HeaderText="Descripción" 
                                HeaderStyle-CssClass="table-dark align-middle"
                                ItemStyle-CssClass="align-middle" />
                                
                            <asp:CommandField HeaderText="Acción" ShowSelectButton="True" SelectText="Seleccionar" 
                                ButtonType="Button" 
                                ControlStyle-CssClass="btn btn-sm btn-outline-primary"
                                HeaderStyle-CssClass="table-dark align-middle text-center"
                                ItemStyle-CssClass="align-middle text-center" />
                        </Columns>
                        <HeaderStyle CssClass="table-dark" />
                        <RowStyle CssClass="align-middle" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>