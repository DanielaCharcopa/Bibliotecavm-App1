<%@ Page Title="Registro de respuesta" Language="C#" MasterPageFile="~/MainUsuario.Master" AutoEventWireup="true" CodeBehind="WFAnswer.aspx.cs" Inherits="Presentation.WFAnswer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Agregar estas referencias en el head -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" />
    <style>
        .card {
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0,0,0,0.1);
        }
        .form-floating label {
            padding: 1rem 0.75rem;
        }
        .btn i {
            margin-right: 5px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid py-4">
        <div class="row justify-content-center">
            <div class="col-lg-8">
                <div class="card shadow">
                    <div class="card-header bg-primary text-white">
                        <h1 class="h4 mb-0"><i class="fas fa-comment-dots me-2"></i>Registro de Preferencias</h1>
                    </div>
                    <div class="card-body">
                        <!-- Mensajes mejorados -->
                        <div class="alert alert-success alert-dismissible fade show" id="successAlert" runat="server" visible="false">
                            <i class="fas fa-check-circle me-2"></i>
                            <asp:Literal ID="litSuccessMessage" runat="server"></asp:Literal>
                            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                        </div>
                        <div class="alert alert-danger alert-dismissible fade show" id="errorAlert" runat="server" visible="false">
                            <i class="fas fa-exclamation-circle me-2"></i>
                            <asp:Literal ID="litErrorMessage" runat="server"></asp:Literal>
                            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                        </div>

                        <!-- Formulario mejorado -->
                        <div class="row g-3">
                            <div class="col-md-6">
                                <div class="form-floating">
                                    <asp:DropDownList ID="ddlSurvey" runat="server" CssClass="form-select" AutoPostBack="false">
                                    </asp:DropDownList>
                                    <label for="ddlSurvey">Seleccionar Pregunta</label>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-floating">
                                    <asp:DropDownList ID="ddlResponse" runat="server" CssClass="form-select">
                                        <asp:ListItem Text="Seleccione una opción" Value="" />
                                        <asp:ListItem Text="Sí" Value="Sí" />
                                        <asp:ListItem Text="No" Value="No" />
                                    </asp:DropDownList>
                                    <label for="ddlResponse">Respuesta</label>
                                </div>
                            </div>
                        </div>

                        <!-- Botón con animación -->
                        <div class="d-grid gap-2 d-md-flex justify-content-md-end mt-4">
                            <asp:Button ID="btnSaveAnswer" runat="server" CssClass="btn btn-success me-md-2"
                            OnClick="btnSaveAnswer_Click" Text="Guardar preferencia" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Script para validación cliente -->
    <script type="text/javascript">
        function validateForm() {
            const ddlSurvey = document.getElementById('<%= ddlSurvey.ClientID %>');
            const ddlResponse = document.getElementById('<%= ddlResponse.ClientID %>');

            if (ddlSurvey.value === "0") {
                showToast('Por favor seleccione una pregunta', 'warning');
                ddlSurvey.focus();
                return false;
            }

            if (ddlResponse.value === "") {
                showToast('Por favor seleccione una respuesta', 'warning');
                ddlResponse.focus();
                return false;
            }

            // Mostrar spinner de carga
            const btn = document.getElementById('<%= btnSaveAnswer.ClientID %>');
            btn.innerHTML = '<i class="fas fa-spinner fa-spin me-1"></i> Guardando...';
            btn.disabled = true;

            return true;
        }

        function showToast(message, type) {
            // Implementación básica de toast notification
            const toast = document.createElement('div');
            toast.className = position - fixed bottom - 0 end - 0 p - 3 text - bg - ${ type } rounded m - 3;
            toast.innerHTML = message;
            document.body.appendChild(toast);

            setTimeout(() => {
                toast.remove();
            }, 3000);
        }
    </script>
</asp:Content>