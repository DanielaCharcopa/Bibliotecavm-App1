<%@ Page Title="" Language="C#" MasterPageFile="~/MainUsuario.Master" AutoEventWireup="true" CodeBehind="WFAnswerManagement.aspx.cs" Inherits="Presentation.WFAnswerManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Registro de Respuestas</h1>
    
    <div class="container-fluid">
        <div class="row">
            <div class="col">
                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
            </div>
        </div>
        <br />

        <div class="row">
            <div class="col">
                <h3>Listado de Respuestas</h3>
                <asp:GridView ID="gvAnswers" runat="server" AutoGenerateColumns="False" CssClass="table table-hover">
                    <Columns>
                        <asp:BoundField DataField="res_id" HeaderText="ID Respuesta" />
                        <asp:BoundField DataField="tbl_encuesta_en_id" HeaderText="ID Pregunta" />
                        <asp:BoundField DataField="en_descripcion_pregunta" HeaderText="Descripción Pregunta" />
                        <asp:BoundField DataField="res_respuesta" HeaderText="Respuesta" />
                        <asp:BoundField DataField="nombre_usuario" HeaderText="Usuario" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
