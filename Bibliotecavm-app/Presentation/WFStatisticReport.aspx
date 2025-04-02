<%@ Page Title="Reporte de Estadísticas" Language="C#" MasterPageFile="~/MainUsuario.Master" AutoEventWireup="true" CodeBehind="WFStatisticReport.aspx.cs" Inherits="Presentation.WFStatisticReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .stats-container {
            padding: 20px;
            background-color: #f9f9f9;
            border-radius: 10px;
            margin-bottom: 20px;
        }
        .grid-container {
            margin-top: 20px;
        }
        h2 {
            color: #333;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="stats-container">
        <h2>Bienvenido, <asp:Label ID="LblUsuario" runat="server" Text="Usuario"></asp:Label></h2>
    </div>

    <div class="stats-container">
        <h2>Estadísticas Generales</h2>
        <p><strong>Total de visitas:</strong> <asp:Label ID="lblTotalVisits" runat="server" Text="0"></asp:Label></p>
        <p><strong>Visitas por docentes:</strong> <asp:Label ID="lblVisitsByTeachers" runat="server" Text="0"></asp:Label></p>
        <p><strong>Visitas por estudiantes:</strong> <asp:Label ID="lblVisitsByStudents" runat="server" Text="0"></asp:Label></p>
    </div>

    <div class="grid-container">
        <h2>Estadísticas de Materiales y Visitas</h2>
        <asp:GridView ID="gvMaterialVisitStats" runat="server" CssClass="table" AutoGenerateColumns="true"></asp:GridView>
    </div>

    <div class="grid-container">
        <h2>Materiales Más Visitados</h2>
        <asp:GridView ID="gvMostVisitedMaterials" runat="server" CssClass="table" AutoGenerateColumns="true"></asp:GridView>
    </div>

   <div class="grid-container">
    <h2>Visitas del Usuario</h2>
    
    <!-- Contenedor de filtros -->
    <div class="search-container" style="display: flex; gap: 15px; flex-wrap: wrap;">
        <!-- Filtro por correo (existente) -->
        <div>
            <asp:TextBox ID="txtSearchEmail" runat="server" CssClass="form-control" 
                placeholder="Correo electrónico" Width="200px"></asp:TextBox>
        </div>
        
        <!-- Nuevos filtros por fecha -->
        <div>
            <label>Desde:</label>
            <asp:TextBox ID="txtFechaInicio" runat="server" CssClass="form-control" 
                TextMode="Date" Width="150px"></asp:TextBox>
        </div>
        
        <div>
            <label>Hasta:</label>
            <asp:TextBox ID="txtFechaFin" runat="server" CssClass="form-control" 
                TextMode="Date" Width="150px"></asp:TextBox>
        </div>
        
         <%--Botones--%> 
        <div style="display: flex; gap: 5px;">
            <asp:Button ID="btnSearch" runat="server" Text="Buscar" 
                OnClick="btnSearch_Click" CssClass="btn btn-primary"/>
            <asp:Button ID="btnClearSearch" runat="server" Text="Limpiar" 
                OnClick="btnClearSearch_Click" CssClass="btn btn-secondary"/>
        </div>
    </div>
    
     <%--GridView (existente)--%> 
    <asp:GridView ID="gvUserVisits" runat="server" CssClass="table" 
        AutoGenerateColumns="true" AllowPaging="true" PageSize="10"
        OnPageIndexChanging="gvUserVisits_PageIndexChanging"
        PagerStyle-CssClass="pagination" PagerSettings-Mode="NumericFirstLast">
    </asp:GridView>
    
    <asp:Label ID="lblSearchMessage" runat="server" CssClass="message"></asp:Label>
</div>
    <asp:Label ID="LblMsj" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>


