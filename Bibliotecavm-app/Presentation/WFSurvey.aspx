<%@ Page Title="Gestion de encuesta" Language="C#" MasterPageFile="~/MainUsuario.Master" AutoEventWireup="true" CodeBehind="WFSurvey.aspx.cs" Inherits="Presentation.WFSurvey" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <h1>Gestión de Encuestas</h1>
    <div class="container-fluid">
        <%--Mensaje de alerta o éxito--%>
        <div class="row">
            <div class="col">
                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
            </div>
        </div>
        <br />

        <%--Formulario para agregar o editar encuestas--%>
        <div class="row">
            <div class="col">
                <%--Id--%>
                <asp:HiddenField ID="TBCode" runat="server" />
            </div>
            <div class="col">
                <%--Pregunta--%>
                <asp:Label ID="lblPregunta" runat="server" Text="Pregunta:"></asp:Label>
                <asp:TextBox ID="txtDescripcionPregunta" CssClass="form-control" runat="server" Width="300px" />
            </div>
        </div>
        <br />

        <%--Botones para guardar, actualizar y eliminar encuesta--%>
        <div class="row">
            <div class="col">
                <asp:Button ID="btnGuardarEncuesta" runat="server" CssClass="btn btn-success" Text="Guardar" OnClick="btnGuardarEncuesta_Click" />
                <asp:Button ID="btnActualizarEncuesta" runat="server" CssClass="btn btn-primary" Text="Actualizar" OnClick="btnActualizarEncuesta_Click" />
                <asp:Button ID="btnEliminarEncuesta" runat="server" CssClass="btn btn-danger" Text="Eliminar" OnClick="btnEliminarEncuesta_Click" />
            </div>
        </div>
        <br />

        <%--Listado de encuestas--%>
        <div class="row">
            <div class="col">
                <h3>Listado de Encuestas</h3>
                <asp:GridView ID="gvSurveys" CssClass="table table-hover" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gvSurveys_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="en_id" HeaderText="ID" />
                        <asp:BoundField DataField="en_descripcion_pregunta" HeaderText="Descripción" />
                        <asp:CommandField HeaderText="Opción" ShowSelectButton="True" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>