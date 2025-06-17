<%@ Page Title="" Language="C#" MasterPageFile="~/MainUsuario.Master" AutoEventWireup="true" CodeBehind="WFStatisticReport.aspx.cs" Inherits="Presentation.WFStatisticReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Añadir Font Awesome para los iconos--%> 
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />

    <%--Añadir Chart.js--%>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <style>
        .dashboard-section {
            background-color: #fff;
            border-radius: 12px;
            box-shadow: 0 4px 12px rgba(0,0,0,0.08);
            padding: 25px;
            margin-bottom: 30px;
            transition: all 0.3s ease;
        }
        
        .dashboard-section:hover {
            box-shadow: 0 6px 18px rgba(0,0,0,0.15);
            transform: translateY(-2px);
        }
        
        .dashboard-section h2, .dashboard-section h3 {
            margin-bottom: 20px;
            color: #2c3e50;
            font-weight: 600;
            border-bottom: 2px solid #f1f1f1;
            padding-bottom: 10px;
        }
        
        .stat-card {
            background-color: #f8f9fa;
            border-left: 5px solid #4e73df;
            padding: 20px;
            margin-bottom: 20px;
            border-radius: 8px;
            transition: all 0.3s ease;
            display: flex;
            flex-direction: column;
            position: relative;
            overflow: hidden;
        }
        
        .stat-card:hover {
            transform: translateY(-5px);
            box-shadow: 0 8px 16px rgba(78, 115, 223, 0.2);
        }
        
        .stat-card .stat-icon {
            font-size: 32px;
            color: #4e73df;
            margin-bottom: 15px;
            display: flex;
            align-items: center;
        }
        
        .stat-card .stat-title {
            font-size: 16px;
            color: #5a5c69;
            margin-bottom: 5px;
            font-weight: 500;
        }
        
        .stat-card .stat-value {
            font-size: 28px;
            font-weight: bold;
            color: #4e73df;
            margin-top: 10px;
            transition: all 0.3s ease;
        }
        
        .stat-card:hover .stat-value {
            transform: scale(1.05);
        }
        
        /* Diferentes colores para las tarjetas */
        .stat-card.primary {
            border-left-color: #4e73df;
        }
        .stat-card.primary .stat-icon, .stat-card.primary .stat-value {
            color: #4e73df;
        }
        
        .stat-card.success {
            border-left-color: #1cc88a;
        }
        .stat-card.success .stat-icon, .stat-card.success .stat-value {
            color: #1cc88a;
        }
        
        .stat-card.info {
            border-left-color: #36b9cc;
        }
        .stat-card.info .stat-icon, .stat-card.info .stat-value {
            color: #36b9cc;
        }
        
        .stat-card.warning {
            border-left-color: #f6c23e;
        }
        .stat-card.warning .stat-icon, .stat-card.warning .stat-value {
            color: #f6c23e;
        }
        
        .stat-card.danger {
            border-left-color: #e74a3b;
        }
        .stat-card.danger .stat-icon, .stat-card.danger .stat-value {
            color: #e74a3b;
        }
        
        .stat-card::after {
            content: '';
            position: absolute;
            top: 0;
            right: 0;
            width: 100px;
            height: 100%;
            background: linear-gradient(to right, rgba(255,255,255,0), rgba(255,255,255,0.5));
            transform: skewX(-20deg) translateX(90px);
            transition: all 0.6s ease;
        }
        
        .stat-card:hover::after {
            transform: skewX(-20deg) translateX(180px);
        }
        
        .survey-stats {
            margin-top: 15px;
        }
        
        .survey-stat-item {
            background-color: #f8f9fa;
            padding: 15px;
            border-radius: 5px;
            margin-bottom: 15px;
            display: flex;
            align-items: center;
            transition: all 0.3s ease;
        }
        
        .survey-stat-item:hover {
            transform: translateX(5px);
            box-shadow: 0 4px 8px rgba(0,0,0,0.1);
        }
        
        .survey-stat-item i {
            font-size: 24px;
            margin-right: 15px;
        }
        
        .progress-container {
            display: flex;
            align-items: center;
            margin-bottom: 10px;
            padding: 5px 0;
        }
        
        .progress {
            flex-grow: 1;
            height: 20px;
            background-color: #e9ecef;
            border-radius: 4px;
            overflow: hidden;
            margin: 0 10px;
        }
        
        .progress-bar {
            height: 100%;
            transition: width 1s ease-out;
        }
        
        .yes-bar {
            background-color: #1cc88a;
        }
        
        .no-bar {
            background-color: #e74a3b;
        }
        
        .search-filters {
            background-color: #f8f9fa;
            padding: 20px;
            border-radius: 10px;
            margin-bottom: 25px;
            box-shadow: 0 2px 15px rgba(0,0,0,0.05);
            border: 1px solid #eaeaea;
        }
        
        .search-filters .input-group {
            width: 100%;
        }
        
        .search-filters .input-group-append {
            display: none;
        }
        
        .search-filters .form-control {
            border-radius: 6px !important;
        }
        
        .form-label {
            font-weight: 500;
            color: #495057;
            margin-bottom: 5px;
            display: block;
        }
        
        .grid-container {
            margin-top: 20px;
        }
        
        .message {
            padding: 10px;
            border-radius: 4px;
            margin-top: 10px;
        }
        
        .error-message {
            background-color: #f8d7da;
            color: #721c24;
            border: 1px solid #f5c6cb;
        }
        
        .info-message {
            background-color: #d1ecf1;
            color: #0c5460;
            border: 1px solid #bee5eb;
        }
        
        /* Estilos para la gráfica */
        .chart-container {
            position: relative;
            height: 300px;
            width: 100%;
            margin: 20px 0;
        }
        
        @media (max-width: 768px) {
            .chart-container {
                height: 250px;
            }
        }

        /* Nuevos estilos para las tablas */
        .table-responsive {
            margin-top: 20px;
            overflow-x: auto;
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
        
        .text-end {
            text-align: right !important;
        }
        
        .text-center {
            text-align: center !important;
        }
        
        .material-title {
            font-weight: 500;
            color: #4e73df;
        }
        
        /* Barras de progreso mejoradas */
        .visits-container {
            width: 100%;
        }
        
        .progress-bar-bg {
            width: 100%;
            height: 25px;
            background-color: #f0f3f7;
            border-radius: 20px;
            overflow: hidden;
            position: relative;
        }
        
        .progress-bar-fill {
            height: 100%;
            border-radius: 20px;
            transition: width 1s ease-in-out;
            display: flex;
            align-items: center;
            justify-content: flex-end;
            padding-right: 10px;
            min-width: 30px;
            background-color: #4e73df;
        }
        
        .visits-count {
            color: white;
            font-weight: bold;
            font-size: 12px;
            text-shadow: 0 0 2px rgba(0,0,0,0.3);
        }
        
        /* Paginación mejorada */
        .pagination {
            display: flex;
            padding-left: 0;
            list-style: none;
            border-radius: 0.25rem;
            justify-content: center;
            margin-top: 20px;
        }
        
        .pagination a {
            padding: 6px 12px;
            margin: 0 3px;
            border: 1px solid #dee2e6;
            text-decoration: none;
            color: #007bff;
            border-radius: 4px;
            transition: all 0.3s;
        }
        
        .pagination a:hover {
            background-color: #e9ecef;
        }
        
        .pagination span {
            padding: 6px 12px;
            margin: 0 3px;
            border: 1px solid #007bff;
            background-color: #007bff;
            color: white;
            border-radius: 4px;
        }
        
        /* Responsive */
        @media (max-width: 768px) {
            .search-filters .form-group {
                margin-bottom: 15px;
            }
            
            .search-filters .col-md-2 {
                margin-top: 10px;
            }
            
            .btn-with-icon {
                width: 100%;
                margin-bottom: 10px;
            }
            
            .btn-with-icon i {
                margin-right: 5px;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%-- Estadísticas Generales --%> 
    <div class="dashboard-section">
        <h3><i class="fas fa-chart-bar mr-2"></i> Estadísticas Generales</h3>
        <div class="row">
            <div class="col-md-4">
                <div class="stat-card primary">
                    <div class="stat-icon">
                        <i class="fas fa-eye"></i>
                    </div>
                    <div class="stat-title">Total de visitas</div>
                    <div class="stat-value"><asp:Label ID="lblTotalVisits" runat="server" Text="0"></asp:Label></div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="stat-card info">
                    <div class="stat-icon">
                        <i class="fas fa-chalkboard-teacher"></i>
                    </div>
                    <div class="stat-title">Visitas por docentes</div>
                    <div class="stat-value"><asp:Label ID="lblVisitsByTeachers" runat="server" Text="0"></asp:Label></div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="stat-card success">
                    <div class="stat-icon">
                        <i class="fas fa-user-graduate"></i>
                    </div>
                    <div class="stat-title">Visitas por estudiantes</div>
                    <div class="stat-value"><asp:Label ID="lblVisitsByStudents" runat="server" Text="0"></asp:Label></div>
                </div>
            </div>
        </div>
    </div>

    <%-- Estadísticas de Encuestas --%> 
    <div class="dashboard-section">
        <h3><i class="fas fa-poll mr-2"></i> Estadísticas de Encuestas</h3>
        <div class="form-group">
            <label for="ddlSurveyQuestions"><i class="fas fa-question-circle"></i> Seleccione una pregunta:</label>
            <asp:DropDownList ID="ddlSurveyQuestions" runat="server" 
                CssClass="form-control" 
                AutoPostBack="true"
                OnSelectedIndexChanged="ddlSurveyQuestions_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
        
        <asp:Panel ID="pnlSurveyStats" runat="server" Visible="false">
            <h4><i class="fas fa-clipboard-question mr-2"></i><asp:Label ID="lblQuestionText" runat="server"></asp:Label></h4>
            
            <div class="survey-stats">
                <div class="survey-stat-item">
                    <i class="fas fa-thumbs-up text-success"></i>
                    <div>
                        <strong>Respuestas 'Sí':</strong> 
                        <asp:Label ID="lblYesPercent" runat="server"></asp:Label>
                    </div>
                </div>
                
                <div class="survey-stat-item">
                    <i class="fas fa-thumbs-down text-danger"></i>
                    <div>
                        <strong>Respuestas 'No':</strong> 
                        <asp:Label ID="lblNoPercent" runat="server"></asp:Label>
                    </div>
                </div>
                
                <div class="survey-stat-item">
                    <i class="fas fa-users text-primary"></i>
                    <div>
                        <strong>Total respuestas:</strong> 
                        <asp:Label ID="lblTotalResponses" runat="server"></asp:Label>
                    </div>
                </div>
            </div>
        </asp:Panel>
        
        <asp:Label ID="Label1" runat="server" CssClass="message error-message" Visible="false"></asp:Label>
    </div>

    <%-- Estadísticas de Materiales --%> 
    <div class="dashboard-section">
        <h3><i class="fas fa-file-alt mr-2"></i> Estadísticas de Materiales registrados</h3>
        
        <%-- Contenedor del gráfico --%>
        <div style="width: 100%; max-width: 600px; margin: 20px auto; text-align: center;">
            <div style="display: flex; justify-content: center; align-items: flex-end; height: 250px; margin-bottom: 20px; gap: 30px;">
                <div style="display: flex; flex-direction: column; align-items: center;">
                    <div id="materialBar" style="width: 80px; background: linear-gradient(to top, #4e73df, #224abe); border-radius: 8px 8px 0 0; transition: height 1s ease; height: 0;"></div>
                    <div style="margin-top: 15px; font-weight: bold; font-size: 16px;">Materiales Educativos</div>
                </div>
            </div>
            
            <div style="font-size: 28px; margin-top: 20px; padding: 15px; background-color: #f8f9fa; border-radius: 8px;">
                Total registrados: <span id="totalMateriales" style="font-weight: bold; color: #4e73df;">0</span>
            </div>
        </div>
        
        <%-- GridView oculto para obtener datos --%>
        <div style="display: none;">
            <asp:GridView ID="gvMaterialesEducativos" runat="server" AutoGenerateColumns="true"></asp:GridView>
        </div>
    </div>

    <%-- Materiales Más Visitados --%>
    <div class="dashboard-section">
        <h3><i class="fas fa-trophy mr-2"></i> Materiales Más Visitados</h3>
        <div class="table-responsive">
            <asp:GridView 
                ID="gvMostVisitedMaterials" 
                runat="server" 
                AutoGenerateColumns="false"
                CssClass="table table-bordered table-striped table-hover"
                EmptyDataText="No se encontraron materiales con visitas registradas."
                GridLines="None"
                aria-label="Materiales más visitados">
                <Columns>
                    <asp:BoundField DataField="mat_id" HeaderText="ID" Visible="false" />
                    <asp:BoundField 
                        DataField="mat_titulo" 
                        HeaderText="Material" 
                        HeaderStyle-CssClass="table-dark align-middle"
                        ItemStyle-CssClass="align-middle material-title" />
                    <asp:TemplateField 
                        HeaderText="Visitas" 
                        HeaderStyle-CssClass="table-dark align-middle text-center"
                        ItemStyle-CssClass="align-middle">
                        <ItemTemplate>
                            <div class="visits-container">
                                <div class="progress-bar-bg">
                                    <div 
                                        class="progress-bar-fill" 
                                        data-visits='<%# Eval("total_visitas") %>'
                                        style="width: 0%;">
                                        <span class="visits-count"><%# Eval("total_visitas") %></span>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="table-dark" />
                <RowStyle CssClass="align-middle" />
                <EmptyDataRowStyle CssClass="text-center p-4" />
            </asp:GridView>
        </div>
    </div>

    <%-- Visitas del Usuario --%>
    <div class="dashboard-section">
        <h3><i class="fas fa-user-clock mr-2"></i> Visitas del Usuario</h3>

        <div class="search-filters">
            <div class="row align-items-end">
                <div class="col-md-5">
                    <div class="form-group mb-0">
                        <label for="txtSearchEmail" class="form-label">Buscar por correo:</label>
                        <asp:TextBox ID="txtSearchEmail" runat="server" 
                            CssClass="form-control" 
                            placeholder="ejemplo@gmail.com"></asp:TextBox>
                    </div>
                </div>
                
                <div class="col-md-2">
                    <div class="form-group mb-0">
                        <label for="txtFechaInicio" class="form-label">Desde:</label>
                        <asp:TextBox ID="txtFechaInicio" runat="server" 
                            CssClass="form-control" 
                            TextMode="Date"></asp:TextBox>
                    </div>
                </div>
                
                <div class="col-md-2">
                    <div class="form-group mb-0">
                        <label for="txtFechaFin" class="form-label">Hasta:</label>
                        <asp:TextBox ID="txtFechaFin" runat="server" 
                            CssClass="form-control" 
                            TextMode="Date"></asp:TextBox>
                    </div>
                </div>
                
                <div class="col-md-3">
                    <div class="form-group mb-0 d-flex justify-content-end" style="gap: 10px;">
                        <asp:LinkButton ID="btnSearch" runat="server"
                            OnClick="btnSearch_Click"
                            CssClass="btn btn-primary btn-with-icon"
                            style="min-width: auto; flex: 1;">
                            <i class="fas fa-search mr-2"></i> Buscar
                        </asp:LinkButton>
                        
                        <asp:LinkButton ID="btnClearSearch" runat="server"
                            OnClick="btnClearSearch_Click"
                            CssClass="btn btn-outline-secondary btn-with-icon"
                            style="min-width: auto; flex: 1;">
                            <i class="fas fa-broom mr-2"></i> Limpiar
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
        
        <div class="table-responsive">
            <asp:GridView 
                ID="gvUserVisits" 
                runat="server" 
                AutoGenerateColumns="false" 
                AllowPaging="true" 
                PageSize="10"
                OnPageIndexChanging="gvUserVisits_PageIndexChanging"
                CssClass="table table-bordered table-striped table-hover"
                EmptyDataText="No se encontraron visitas con los criterios de búsqueda."
                PagerStyle-CssClass="pagination"
                PagerSettings-Mode="NumericFirstLast"
                PagerSettings-Position="Bottom"
                PagerSettings-PageButtonCount="5"
                aria-label="Visitas de usuarios">
                <Columns>
                    <asp:BoundField 
                        DataField="usuario_nombre" 
                        HeaderText="Usuario"
                        HeaderStyle-CssClass="table-dark align-middle"
                        ItemStyle-CssClass="align-middle" />
                    <asp:BoundField 
                        DataField="usuario_correo" 
                        HeaderText="Correo"
                        HeaderStyle-CssClass="table-dark align-middle"
                        ItemStyle-CssClass="align-middle" />
                    <asp:BoundField 
                        DataField="material_titulo" 
                        HeaderText="Material"
                        HeaderStyle-CssClass="table-dark align-middle"
                        ItemStyle-CssClass="align-middle" />
                    <asp:BoundField 
                        DataField="vis_fecha_ingreso" 
                        HeaderText="Fecha Visita"
                        DataFormatString="{0:dd/MM/yyyy HH:mm}"
                        HeaderStyle-CssClass="table-dark align-middle"
                        ItemStyle-CssClass="align-middle" />
                    <asp:BoundField 
                        DataField="vis_duracion" 
                        HeaderText="Duración (min)"
                        HeaderStyle-CssClass="table-dark align-middle text-end"
                        ItemStyle-CssClass="align-middle text-end" />
                </Columns>
                <HeaderStyle CssClass="table-dark" />
                <RowStyle CssClass="align-middle" />
                <EmptyDataRowStyle CssClass="text-center p-4" />
            </asp:GridView>
        </div>
        
        <asp:Label ID="lblSearchMessage" runat="server" CssClass="message info-message"></asp:Label>
    </div>

    <asp:Label ID="LblMsj" runat="server" CssClass="message error-message" Visible="false"></asp:Label>

    <%-- Script para la gráfica y efectos visuales --%>  
    <script>
        document.addEventListener('DOMContentLoaded', function () {
            // Obtener datos del GridView
            var grid = document.getElementById('<%= gvMaterialesEducativos.ClientID %>');
            var totalMateriales = grid ? grid.rows.length - 1 : 0; // Restar 1 por el encabezado

            // Actualizar el total
            document.getElementById('totalMateriales').textContent = totalMateriales;

            // Animación de la barra (altura proporcional al total)
            setTimeout(function () {
                var bar = document.getElementById('materialBar');
                if (bar) {
                    // Altura base + 10px por cada material (ajustable)
                    var altura = 50 + (totalMateriales * 10);
                    bar.style.height = altura + 'px';
                }
            }, 300);

            // Configurar búsqueda con Enter
            var searchEmailField = document.getElementById('<%= txtSearchEmail.ClientID %>');
            var searchButton = document.getElementById('<%= btnSearch.ClientID %>');

            if (searchEmailField && searchButton) {
                searchEmailField.addEventListener('keypress', function (e) {
                    if (e.key === 'Enter') {
                        e.preventDefault();
                        searchButton.click();
                    }
                });
            }

            // Validación de fechas en el cliente
            document.getElementById('<%= btnSearch.ClientID %>').addEventListener('click', function (e) {
                var fechaInicio = document.getElementById('<%= txtFechaInicio.ClientID %>').value;
                var fechaFin = document.getElementById('<%= txtFechaFin.ClientID %>').value;

                if (fechaInicio && fechaFin && new Date(fechaInicio) > new Date(fechaFin)) {
                    alert('La fecha de inicio no puede ser mayor a la fecha fin');
                    e.preventDefault();
                }
            });

            // Animar barras de progreso
            setTimeout(function () {
                animateProgressBars();
            }, 300);
        });

        function animateProgressBars() {
            const bars = document.querySelectorAll('.progress-bar-fill');

            // Encontrar el valor máximo para escalar las barras
            let maxVisits = 0;
            bars.forEach(bar => {
                const visits = parseInt(bar.getAttribute('data-visits')) || 0;
                if (visits > maxVisits) maxVisits = visits;
            });

            // Animar cada barra
            bars.forEach(bar => {
                const visits = parseInt(bar.getAttribute('data-visits')) || 0;
                const percentage = maxVisits > 0 ? (visits / maxVisits) * 100 : 0;

                // Aplicar color basado en el porcentaje (azul a verde)
                const hue = 210 * (percentage / 100);
                bar.style.backgroundColor = `hsl(${hue}, 70%, 50%)`;
                bar.style.width = percentage + '%';
                bar.setAttribute('title', visits + ' visitas');
            });
        }

        // Manejar postbacks de ASP.NET
        if (typeof (Sys) !== 'undefined') {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                setTimeout(animateProgressBars, 300);
            });
        }
    </script>
</asp:Content>