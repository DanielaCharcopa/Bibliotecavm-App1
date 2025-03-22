<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFMatEducativoSecondary.aspx.cs" Inherits="Presentation.WFMatEducativoSecondary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <h2>Material Educativo Disponible</h2>

 <!-- Campo de búsqueda -->
   <asp:TextBox ID="TBSearch" runat="server" placeholder="Buscar Material Educativo Disponible" />
  <asp:Button ID="BtnSearch" runat="server" Text="Buscar" OnClick="BtnSearch_Click" 
      CssClass="btn btn-primary" ValidationGroup="SearchGroup" />

 <asp:Repeater ID="RptMateriales" runat="server" OnItemCommand="RptMateriales_ItemCommand">
     <HeaderTemplate>
         <table id="materialTable" border="1" style="width: 100%; border-collapse: collapse;">
             <thead>
                 <tr>
                     <th>Título</th>
                     <th>Año de Publicación</th>
                     <th>Precio</th>
                     <th>Formato</th>
                     <th>Acciones</th>
                 </tr>
             </thead>
             <tbody>
     </HeaderTemplate>

     <ItemTemplate>
         <tr>
             <td class="titulo"><%# Eval("mat_titulo") %></td>
             <td><%# Eval("mat_ano_publicacion") %></td>
             <td><%# Eval("mat_precio", "{0:C}") %></td>
             <td><%# Eval("mat_formato") %></td>
             <td>
                 <!-- Ver -->
               <div style="display: inline-block; border: 1px solid #ccc; padding: 10px; margin-right: 10px;">
    <asp:HyperLink ID="lnkView" runat="server" NavigateUrl='<%# Eval("mat_url_descarga") %>' Text="Ver" Target="_blank"></asp:HyperLink>
</div>
<div style="display: inline-block; border: 1px solid #ccc; padding: 10px;">
    <asp:HyperLink ID="lnkBuy" runat="server" NavigateUrl='<%# "WFPurchaseRequest.aspx?materialId=" + Eval("mat_id") %>' Text="Comprar"></asp:HyperLink>
</div>
             </td>
         </tr>
     </ItemTemplate>

     <FooterTemplate>
             </tbody>
         </table>
     </FooterTemplate>
 </asp:Repeater>

 <asp:Label ID="LblNoData" runat="server" Text="No hay materiales disponibles en este momento." 
     ForeColor="Red" Visible="false" />

 <!-- Modal para visualizar el material -->
 <asp:Panel ID="PnlViewer" runat="server" CssClass="modal" Style="display:none;">
     <asp:Button ID="btnClose" runat="server" Text="Cerrar" OnClick="btnClose_Click" />
     <asp:Literal ID="litViewerContent" runat="server"></asp:Literal>
 </asp:Panel>

 
 

</asp:Content>
