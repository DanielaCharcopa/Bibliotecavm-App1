<%@ Page Title="Gestión de Compras" Language="C#" MasterPageFile="~/MainUsuario.Master" 
    AutoEventWireup="true" CodeBehind="WFPurchaseRequestManagement.aspx.cs" 
    Inherits="Presentation.WFPurchaseRequestManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Gestión de Solicitudes de Compra</h1>
    
    <asp:Label ID="LblMsj" runat="server" ForeColor="Red"></asp:Label>

   <asp:GridView ID="GVRequests" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
    <Columns>
        <asp:BoundField DataField="solic_id" HeaderText="ID" />
        <asp:BoundField DataField="solic_ticket" HeaderText="Ticket" />
        <asp:BoundField DataField="solic_fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}" />
        <asp:BoundField DataField="usuario_nombre" HeaderText="Usuario" />
        <asp:BoundField DataField="solic_cantidad" HeaderText="Cantidad" />
        <asp:BoundField DataField="material_titulo" HeaderText="Material" />
        <asp:BoundField DataField="solic_valor_total" HeaderText="Total" DataFormatString="{0:C2}" />

         <%--Botón para imprimir individualmente cada fila--%> 
        <asp:TemplateField HeaderText="Acción">
            <ItemTemplate>
                <asp:Button ID="BtnPrint" runat="server" Text="Imprimir" CommandName="Print" CommandArgument='<%# Eval("solic_id") %>' CssClass="btn btn-success" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

</asp:Content>
