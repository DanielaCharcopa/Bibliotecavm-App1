<%@ Page Title="" Language="C#" MasterPageFile="~/MainUsuario.Master" 
    AutoEventWireup="true" CodeBehind="WFAnswer.aspx.cs" Inherits="Presentation.WFAnswer" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h3 class="mb-4">Responder Encuestas</h3>
        
        <asp:Panel ID="pnlNoQuestions" runat="server" Visible="false" CssClass="alert alert-info">
            <i class="fa fa-info-circle"></i> No hay preguntas pendientes por responder.
        </asp:Panel>
        
        <asp:Panel ID="pnlQuestions" runat="server">
             <%--Repeater para listar todas las preguntas no respondidas--%> 
            <asp:Repeater ID="rptQuestions" runat="server" OnItemDataBound="rptQuestions_ItemDataBound">
                <ItemTemplate>
                    <div class="card mb-3">
                        <div class="card-body">
                            <h5 class="card-title">Pregunta #<%# Eval("en_id") %></h5>
                            <p class="card-text"><%# Eval("en_descripcion_pregunta") %></p>
                            
                            <asp:HiddenField ID="hfQuestionId" runat="server" Value='<%# Eval("en_id") %>' />
                            
                             <%--Fieldset para agrupar radio buttons relacionados--%> 
                            <fieldset class="mt-3">
                                <legend class="sr-only">Opciones de respuesta para pregunta <%# Eval("en_id") %></legend>
                                
                                <div class="form-check form-check-inline">
                                    <asp:RadioButton ID="rbSi" runat="server" 
                                        GroupName='<%# "respuesta_" + Eval("en_id") %>' 
                                        CssClass="form-check-input" />
                                    <label class="form-check-label" for='<%# Container.FindControl("rbSi").ClientID %>'>
                                        Sí
                                    </label>
                                </div>
                                
                                <div class="form-check form-check-inline">
                                    <asp:RadioButton ID="rbNo" runat="server" 
                                        GroupName='<%# "respuesta_" + Eval("en_id") %>' 
                                        CssClass="form-check-input" />
                                    <label class="form-check-label" for='<%# Container.FindControl("rbNo").ClientID %>'>
                                        No
                                    </label>
                                </div>
                            </fieldset>
                            
                            <div class="mt-3">
                                <asp:Button ID="btnResponder" runat="server" Text="Confirmar Respuesta" 
                                    CssClass="btn btn-primary" OnClick="btnResponder_Click" 
                                    CommandArgument='<%# Eval("en_id") %>' />
                                <asp:Label ID="lblMessage" runat="server" CssClass="ml-2"></asp:Label>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </asp:Panel>
        
        <asp:Panel ID="pnlSuccess" runat="server" Visible="false" CssClass="alert alert-success mt-3">
            <asp:Label ID="lblSuccess" runat="server"></asp:Label>
        </asp:Panel>
        
        <asp:Panel ID="pnlError" runat="server" Visible="false" CssClass="alert alert-danger mt-3">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </asp:Panel>
    </div>
    
     <%--CSS adicional para sr-only--%> 
    <style>
        .sr-only {
            position: absolute !important;
            width: 1px !important;
            height: 1px !important;
            padding: 0 !important;
            margin: -1px !important;
            overflow: hidden !important;
            clip: rect(0, 0, 0, 0) !important;
            white-space: nowrap !important;
            border: 0 !important;
        }
    </style>
</asp:Content>