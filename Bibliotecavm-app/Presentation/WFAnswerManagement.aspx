<%@ Page Title="" Language="C#" MasterPageFile="~/MainAdmin.Master" AutoEventWireup="true" 
    CodeBehind="WFAnswerManagement.aspx.cs" Inherits="Presentation.WFAnswerManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /* Estilos para ocultar columnas pero mantener funcionalidad */
        .hidden-id-column {
            display: none !important;
            width: 0 !important;
            height: 0 !important;
            padding: 0 !important;
            margin: 0 !important;
            border: none !important;
        }
        
        /* Estilos de paginación */
        .pagination a, .pagination span {
            padding: 5px 10px;
            margin: 0 3px;
            border: 1px solid #ddd;
            text-decoration: none;
        }
        .pagination a:hover {
            background-color: #f5f5f5;
        }
        .pagination .active {
            background-color: #337ab7;
            color: white;
            border-color: #337ab7;
        }
        
        /* Estilos generales */
        .container-fluid {
            padding: 20px;
        }
        
        .table-hover {
            width: 100%;
            border-collapse: collapse;
            margin-top: 15px;
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
        
        .table-bordered {
            border: 1px solid #dee2e6;
        }
        
        #lblMessage {
            display: block;
            padding: 10px;
            margin-bottom: 15px;
            border-radius: 4px;
        }
        
        .text-muted {
            color: #6c757d;
        }
        
        .small {
            font-size: 80%;
            font-weight: 400;
        }
        
        .float-right {
            float: right;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Registro de Respuestas</h1>
    
    <div class="container-fluid">
        <div class="row">
            <div class="col">
                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
                <asp:Label ID="lblPaginationInfo" runat="server" CssClass="float-right text-muted small"></asp:Label>
            </div>
        </div>
        <br />

        <div class="row">
            <div class="col">
                <h3>Listado de Respuestas</h3>
                <asp:GridView ID="gvAnswers" runat="server" AutoGenerateColumns="False" 
                    CssClass="table table-hover table-bordered" AllowPaging="True" PageSize="10"
                    OnPageIndexChanging="gvAnswers_PageIndexChanging" PagerStyle-CssClass="pagination"
                    PagerSettings-Mode="NumericFirstLast" EmptyDataText="No se encontraron registros">
                    <Columns>
                        
                        <asp:BoundField DataField="res_id" HeaderText="ID Respuesta" 
                            ItemStyle-CssClass="hidden-id-column" HeaderStyle-CssClass="hidden-id-column" />
                            
                        <asp:BoundField DataField="tbl_encuesta_en_id" HeaderText="ID Pregunta" 
                            ItemStyle-CssClass="hidden-id-column" HeaderStyle-CssClass="hidden-id-column" />
                            
                        
                        <asp:BoundField DataField="en_descripcion_pregunta" HeaderText="Descripción Pregunta" />
                        <asp:BoundField DataField="res_respuesta" HeaderText="Respuesta" />
                        <asp:BoundField DataField="nombre_usuario" HeaderText="Usuario" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>