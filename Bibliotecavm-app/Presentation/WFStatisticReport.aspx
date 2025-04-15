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
        .survey-stats {
            display: grid;
            grid-template-columns: repeat(2, 1fr);
            gap: 15px;
            margin-top: 15px;
        }
        .survey-stat-item {
            background-color: #e9f7fe;
            padding: 10px;
            border-radius: 5px;
        }
        .form-group {
            margin-bottom: 15px;
        }
        .form-control {
            display: block;
            width: 100%;
            padding: 0.375rem 0.75rem;
            font-size: 1rem;
            line-height: 1.5;
            color: #495057;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            border-radius: 0.25rem;
            transition: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
        }
        .message {
            display: block;
            margin-top: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!-- Sección de estadísticas generales -->
    <div class="stats-container">
        <h2>Estadísticas Generales</h2>
        <p><strong>Total de visitas:</strong> <asp:Label ID="lblTotalVisits" runat="server" Text="0"></asp:Label></p>
        <p><strong>Visitas por docentes:</strong> <asp:Label ID="lblVisitsByTeachers" runat="server" Text="0"></asp:Label></p>
        <p><strong>Visitas por estudiantes:</strong> <asp:Label ID="lblVisitsByStudents" runat="server" Text="0"></asp:Label></p>
    </div>

     <%--Sección de estadísticas de encuestas--%> 
    <div class="stats-container">
        <h2>Estadísticas de Encuestas</h2>
        
         <%--Selector de encuestas--%> 
        <div class="form-group">
            <label for="ddlSurveyQuestions">Seleccione una pregunta:</label>
            <asp:DropDownList ID="ddlSurveyQuestions" runat="server" 
                CssClass="form-control" 
                AutoPostBack="true"
                OnSelectedIndexChanged="ddlSurveyQuestions_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
        
         <%--Panel de estadísticas--%> 
            <asp:Panel ID="pnlSurveyStats" runat="server" Visible="false">
            <asp:Label ID="lblQuestionText" runat="server" Font-Bold="true" Font-Size="Medium"></asp:Label>
            
            <div class="survey-stats">
                <div class="survey-stat-item">
                    <strong>Respuestas 'Sí':</strong> 
                    <asp:Label ID="lblYesCount" runat="server"></asp:Label>
                    (<asp:Label ID="lblYesPercent" runat="server"></asp:Label>)
                </div>
                
                <div class="survey-stat-item">
                    <strong>Respuestas 'No':</strong> 
                    <asp:Label ID="lblNoCount" runat="server"></asp:Label>
                    (<asp:Label ID="lblNoPercent" runat="server"></asp:Label>)
                </div>
                
                <div class="survey-stat-item" style="grid-column: span 2;">
                    <strong>Total respuestas:</strong> 
                    <asp:Label ID="lblTotalResponses" runat="server"></asp:Label>
                </div>
            </div>
        </asp:Panel>
        
        <asp:Label ID="lblSurveyMessage" runat="server" CssClass="message"></asp:Label>
    </div>

     <%--Sección de estadísticas de materiales y visitas--%> 
    <div class="grid-container">
        <h2>Estadísticas de Materiales y Visitas</h2>
        <asp:GridView ID="gvMaterialVisitStats" runat="server" CssClass="table" AutoGenerateColumns="true"></asp:GridView>
    </div>

    <!-- Sección de materiales más visitados -->
    <div class="grid-container">
        <h2>Materiales Más Visitados</h2>
        <asp:GridView ID="gvMostVisitedMaterials" runat="server" CssClass="table" AutoGenerateColumns="true"></asp:GridView>
    </div>

    <!-- Sección de visitas del usuario -->
    <div class="grid-container">
        <h2>Visitas del Usuario</h2>
        
        <!-- Contenedor de filtros -->
        <div class="search-container" style="display: flex; gap: 15px; flex-wrap: wrap;">
            <!-- Filtro por correo -->
            <div>
                <asp:TextBox ID="txtSearchEmail" runat="server" CssClass="form-control" 
                    placeholder="Correo electrónico" Width="200px"></asp:TextBox>
            </div>
            
            <!-- Filtros por fecha -->
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
            
            <!-- Botones -->
            <div style="display: flex; gap: 5px;">
                <asp:Button ID="btnSearch" runat="server" Text="Buscar" 
                    OnClick="btnSearch_Click" CssClass="btn btn-primary"/>
                <asp:Button ID="btnClearSearch" runat="server" Text="Limpiar" 
                    OnClick="btnClearSearch_Click" CssClass="btn btn-secondary"/>
            </div>
        </div>
        
        <!-- GridView de visitas -->
        <asp:GridView ID="gvUserVisits" runat="server" CssClass="table" 
            AutoGenerateColumns="true" AllowPaging="true" PageSize="8"
            OnPageIndexChanging="gvUserVisits_PageIndexChanging"
            PagerStyle-CssClass="pagination" PagerSettings-Mode="NumericFirstLast">
        </asp:GridView>
        
        <asp:Label ID="lblSearchMessage" runat="server" CssClass="message"></asp:Label>
    </div>
    
    <asp:Label ID="LblMsj" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>