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
        
        /* GridView Mejorado */
        .gridview-container {
            margin-top: 30px;
            overflow-x: auto;
            -webkit-overflow-scrolling: touch;
        }

        .gridview {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
            min-width: 600px;
        }

        .gridview th {
            background-color: #343a40;
            color: white;
            padding: 12px;
            text-align: left;
            vertical-align: middle;
            border: 1px solid #454d55;
        }

        .gridview td {
            padding: 12px;
            border-bottom: 1px solid #dee2e6;
            vertical-align: middle;
        }

        .gridview tr:nth-child(even) {
            background-color: rgba(0, 0, 0, 0.05);
        }

        .gridview tr:hover {
            background-color: rgba(0, 0, 0, 0.075);
        }

        /* Ajustes responsivos de columnas */
        .ticket-column {
            width: auto;
            min-width: 150px;
            max-width: 180px;
        }

        .date-column {
            width: auto;
            min-width: 80px;
            max-width: 90px;
        }

        .user-column {
            width: auto;
            min-width: 120px;
            max-width: 200px;
        }

        .quantity-column {
            width: auto;
            min-width: 60px;
            max-width: 70px;
            text-align: center !important;
        }

        .material-column {
            width: auto;
            min-width: 150px;
            max-width: 300px;
            word-break: break-word;
        }

        .total-column {
            width: auto;
            min-width: 90px;
            max-width: 110px;
            text-align: right !important;
        }

        .completed-column {
            width: auto;
            min-width: 70px;
            max-width: 90px;
            text-align: center !important;
        }

        /* Paginación mejorada */
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

        /* Checkbox responsivo */
        .checkbox-large {
            width: clamp(16px, 3vw, 18px);
            height: clamp(16px, 3vw, 18px);
            margin: 0 auto;
            display: block;
        }
        
        /* Estilos para accesibilidad */
        .visually-hidden {
            position: absolute;
            width: 1px;
            height: 1px;
            padding: 0;
            margin: -1px;
            overflow: hidden;
            clip: rect(0, 0, 0, 0);
            white-space: nowrap;
            border: 0;
        }

        .checkbox-container {
            position: relative;
            display: inline-block;
        }
        
        /* Estilos para móviles */
        @media (max-width: 768px) {
            .container {
                padding: 10px;
            }
            
            .gridview th, 
            .gridview td {
                padding: 8px;
            }
            
            .ticket-column, 
            .user-column, 
            .material-column {
                word-break: break-all;
            }
            
            .pagination a, 
            .pagination span {
                padding: 0.25rem 0.5rem;
                margin: 0 2px;
                font-size: 0.9em;
            }
        }
        
        /* Estilos para tablets */
        @media (min-width: 769px) and (max-width: 1024px) {
            .container {
                padding: 15px;
            }
            
            .gridview th, 
            .gridview td {
                padding: 10px 6px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h3>Gestión de Solicitudes de Compra</h3>
        
        <asp:Label ID="LblMsj" runat="server" ForeColor="Red"></asp:Label>

        <asp:ScriptManager runat="server"></asp:ScriptManager>
        
        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="gridview-container">
                    <asp:GridView ID="GVRequests" runat="server" AutoGenerateColumns="False" CssClass="gridview"
                        OnRowDataBound="GVRequests_RowDataBound" DataKeyNames="solic_id"
                        AllowPaging="True" PageSize="8" OnPageIndexChanging="GVRequests_PageIndexChanging"
                        PagerStyle-CssClass="pagination"
                        PagerSettings-Mode="NumericFirstLast"
                        PagerSettings-Position="Bottom"
                        PagerSettings-PageButtonCount="5">
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
                                    <div class="checkbox-container">
                                        <asp:Label ID="LblCompleted" runat="server" 
                                            AssociatedControlID="CbCompleted"
                                            CssClass="visually-hidden"
                                            Text='<%# "Marcar solicitud " + Eval("solic_ticket") + " como completada" %>' />
                                        <asp:CheckBox ID="CbCompleted" runat="server" CssClass="checkbox-large" 
                                            AutoPostBack="true" OnCheckedChanged="CbCompleted_CheckedChanged"
                                            Checked='<%# IsCompleted(Eval("solic_ticket")) %>' 
                                            Enabled='<%# !IsCompleted(Eval("solic_ticket")) %>'
                                            ToolTip='<%# "Marcar solicitud " + Eval("solic_ticket") + " como completada" %>' />
                                    </div>
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