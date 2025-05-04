<%@ Page Title="" Language="C#" MasterPageFile="~/MainUsuario.Master" 
    AutoEventWireup="true" CodeBehind="WFAnswer.aspx.cs" Inherits="Presentation.WFAnswer" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-4">
        <h3 class="mb-4">Responder Encuestas</h3>
        
        <asp:Panel ID="pnlNoQuestions" runat="server" Visible="false" CssClass="alert alert-info">
            <i class="fa fa-info-circle"></i> No hay preguntas pendientes por responder.
        </asp:Panel>
        
        <asp:Panel ID="pnlQuestions" runat="server">
            <!-- Repeater para listar todas las preguntas no respondidas -->
            <asp:Repeater ID="rptQuestions" runat="server" OnItemDataBound="rptQuestions_ItemDataBound">
                <ItemTemplate>
                    <div class="card mb-3">
                        <div class="card-body">
                            <h5 class="card-title">Pregunta #<%# Eval("en_id") %></h5>
                            <p class="card-text"><%# Eval("en_descripcion_pregunta") %></p>
                            
                            <asp:HiddenField ID="hfQuestionId" runat="server" Value='<%# Eval("en_id") %>' />
                            
                            <div class="form-check form-check-inline">
                                <asp:RadioButton ID="rbSi" runat="server" GroupName='<%# "respuesta_" + Eval("en_id") %>' Text="Sí" CssClass="form-check-input" />
                                <label class="form-check-label" for='<%# Container.FindControl("rbSi").ClientID %>'>Sí</label>
                            </div>
                            <div class="form-check form-check-inline">
                                <asp:RadioButton ID="rbNo" runat="server" GroupName='<%# "respuesta_" + Eval("en_id") %>' Text="No" CssClass="form-check-input" />
                                <label class="form-check-label" for='<%# Container.FindControl("rbNo").ClientID %>'>No</label>
                            </div>
                            
                            <div class="mt-2">
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
</asp:Content>