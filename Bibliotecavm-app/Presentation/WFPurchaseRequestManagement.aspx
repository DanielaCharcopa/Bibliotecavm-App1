<%@ Page Title="Gestión de Compras" Language="C#" MasterPageFile="~/MainAdmin.Master" AutoEventWireup="true" 
    CodeBehind="WFPurchaseRequestManagement.aspx.cs" Inherits="Presentation.WFPurchaseRequestManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>

    <style type="text/css">
        /* Estilo para ocultar la columna ID */
        .hidden-id-column {
            display: none !important;
        }
        
        /* Color para filas completadas */
        .completed-row {
            background-color: #c8e6c9 !important;
        }
        
        /* Estructura principal responsiva */
        .container {
            padding: 15px;
            width: 100%;
            margin: 0 auto;
            box-sizing: border-box;
        }
        
        h1 {
            margin: 0 0 20px 0;
            font-size: clamp(22px, 4vw, 28px);
            color: #333;
        }
        
        /* Mensajes responsivos */
        #LblMsj {
            display: block;
            padding: 10px;
            margin: 0 0 15px 0;
            font-size: clamp(12px, 2vw, 14px);
            background-color: #f8d7da;
            color: #721c24;
            border-radius: 4px;
            border: 1px solid #f5c6cb;
        }
        
        /* Tabla responsiva */
        .gridview-container {
            width: 100%;
            overflow-x: auto;
            margin: 20px 0;
            -webkit-overflow-scrolling: touch;
        }
        
        .gridview {
            width: 100%;
            min-width: 600px; /* Ancho mínimo para evitar compresión extrema */
            border-collapse: collapse;
            font-size: clamp(12px, 2vw, 14px);
        }
        
        .gridview th {
            background-color: #f8f9fa;
            padding: 10px 8px;
            text-align: left;
            border-bottom: 2px solid #dee2e6;
            font-weight: 600;
            white-space: nowrap;
        }
        
        .gridview td {
            padding: 10px 8px;
            border-bottom: 1px solid #e9ecef;
            vertical-align: middle;
        }
        
        /* Ajustes responsivos por columna */
        .ticket-column {
            width: auto;
            min-width: 150px;
            max-width: 180px;
            padding-right: 8px !important;
        }
        
        .date-column {
            width: auto;
            min-width: 80px;
            max-width: 90px;
            padding-right: 10px !important;
        }
        
        .user-column {
            width: auto;
            min-width: 120px;
            max-width: 200px;
            padding-right: 15px !important;
        }
        
        .quantity-column {
            width: auto;
            min-width: 60px;
            max-width: 70px;
            padding-right: 10px !important;
            text-align: center !important;
        }
        
        .material-column {
            width: auto;
            min-width: 150px;
            max-width: 300px;
            padding-right: 15px !important;
            word-break: break-word;
        }
        
        .total-column {
            width: auto;
            min-width: 90px;
            max-width: 110px;
            padding-right: 10px !important;
            text-align: right !important;
        }
        
        .completed-column {
            width: auto;
            min-width: 70px;
            max-width: 90px;
            text-align: center !important;
        }
        
        /* Efecto hover */
        .gridview tr:not(.completed-row):hover {
            background-color: #f8f9fa;
        }
        
        /* Checkbox responsivo */
        .checkbox-large {
            width: clamp(16px, 3vw, 18px);
            height: clamp(16px, 3vw, 18px);
            margin: 0 auto;
            display: block;
        }
        
        /* Paginación responsiva */
        .pagination {
            display: flex;
            justify-content: center;
            flex-wrap: wrap;
            margin: 20px 0;
            padding: 0;
            gap: 5px;
        }
        
        .pagination a, .pagination span {
            padding: 6px 10px;
            margin: 0;
            border: 1px solid #dee2e6;
            color: #007bff;
            border-radius: 4px;
            font-size: clamp(12px, 2vw, 14px);
        }
        
        .pagination .active {
            background-color: #007bff;
            color: white;
            border-color: #007bff;
        }
        
        /* Estilos para móviles */
        @media (max-width: 768px) {
            .container {
                padding: 10px;
            }
            
            .gridview th, .gridview td {
                padding: 8px 5px;
            }
            
            .ticket-column, .user-column, .material-column {
                word-break: break-all;
            }
        }
        
        /* Estilos para tablets */
        @media (min-width: 769px) and (max-width: 1024px) {
            .container {
                padding: 15px;
            }
            
            .gridview th, .gridview td {
                padding: 10px 6px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h1>Gestión de Solicitudes de Compra</h1>
        
        <asp:Label ID="LblMsj" runat="server" ForeColor="Red"></asp:Label>

        <asp:ScriptManager runat="server"></asp:ScriptManager>
        
        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="gridview-container">
                    <asp:GridView ID="GVRequests" runat="server" AutoGenerateColumns="False" CssClass="gridview"
                        OnRowDataBound="GVRequests_RowDataBound" DataKeyNames="solic_id"
                        AllowPaging="True" PageSize="8" OnPageIndexChanging="GVRequests_PageIndexChanging"
                        PagerStyle-CssClass="pagination">
                        <Columns>
                            <asp:BoundField DataField="solic_id" HeaderText="ID" 
                                ItemStyle-CssClass="hidden-id-column" 
                                HeaderStyle-CssClass="hidden-id-column" />
                                
                            <asp:TemplateField HeaderText="Ticket" ItemStyle-CssClass="ticket-column" HeaderStyle-CssClass="ticket-column">
                                <ItemTemplate>
                                    <asp:Label ID="LblTicket" runat="server" Text='<%# GetCleanTicket(Eval("solic_ticket").ToString()) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="solic_fecha" HeaderText="Fecha soli." DataFormatString="{0:dd/MM/yyyy}" 
                                ItemStyle-CssClass="date-column" HeaderStyle-CssClass="date-column" />
                                
                            <asp:BoundField DataField="usuario_nombre" HeaderText="Nombre usuario" 
                                ItemStyle-CssClass="user-column" HeaderStyle-CssClass="user-column" />
                                
                            <asp:BoundField DataField="solic_cantidad" HeaderText="Cantidad" 
                                ItemStyle-CssClass="quantity-column" HeaderStyle-CssClass="quantity-column" />
                                
                            <asp:BoundField DataField="material_titulo" HeaderText="Material educativo" 
                                ItemStyle-CssClass="material-column" HeaderStyle-CssClass="material-column" />
                                
                            <asp:BoundField DataField="solic_valor_total" HeaderText="Valor total" DataFormatString="{0:C2}" 
                                ItemStyle-CssClass="total-column" HeaderStyle-CssClass="total-column" />
                            
                            <asp:TemplateField HeaderText="Completar" ItemStyle-CssClass="completed-column" HeaderStyle-CssClass="completed-column">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CbCompleted" runat="server" CssClass="checkbox-large" 
                                        AutoPostBack="true" OnCheckedChanged="CbCompleted_CheckedChanged"
                                        Checked='<%# IsCompleted(Eval("solic_ticket")) %>' 
                                        Enabled='<%# !IsCompleted(Eval("solic_ticket")) %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <script type="text/javascript">
        $(document).ready(function () {
            $(document).on('change', '#<%= GVRequests.ClientID %> input[type="checkbox"]', function () {
                setTimeout(function () {
                    location.reload();
                }, 1000);
            });
        });
    </script>
</asp:Content>