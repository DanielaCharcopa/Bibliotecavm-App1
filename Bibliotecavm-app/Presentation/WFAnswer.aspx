<%@ Page Title="" Language="C#" MasterPageFile="~/MainUsuario.Master" AutoEventWireup="true" CodeBehind="WFAnswer.aspx.cs" Inherits="Presentation.WFAnswer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Registro de Respuestas</h1>
    
    <div class="container-fluid">
        <div class="row">
            <div class="col">
                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Green"></asp:Label>
                <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
            </div>
        </div>
        <br />

        <div class="row">
            <div class="col">
                <asp:Label ID="lblSurvey" runat="server" Text="Seleccionar Pregunta:"></asp:Label>
                <asp:DropDownList ID="ddlSurvey" runat="server" CssClass="form-select" Width="200px"></asp:DropDownList>
            </div>
            <div class="col">
                <asp:Label ID="lblResponse" runat="server" Text="Respuesta:"></asp:Label>
                <asp:DropDownList ID="ddlResponse" runat="server" CssClass="form-select" Width="200px">
                    <asp:ListItem Text="Seleccione una opción" Value="" />
                    <asp:ListItem Text="Sí" Value="Sí" />
                    <asp:ListItem Text="No" Value="No" />
                </asp:DropDownList>
              
            </div>
            <div class="col">
                <label class="form-label" for="LblUsuario">Usuario:</label>
                <asp:Label ID="LblUsuario" runat="server" CssClass="form-control" />
            </div>
        </div>
        <br />

        <div class="row">
            <div class="col">
                <asp:Button ID="btnSaveAnswer" runat="server" CssClass="btn btn-success" Text="Guardar Respuesta" OnClick="btnSaveAnswer_Click" />
                <asp:Button ID="btnUpdateAnswer" runat="server" CssClass="btn btn-primary" Text="Actualizar Respuesta" OnClick="btnUpdateAnswer_Click" />
                <asp:Button ID="btnDeleteAnswer" runat="server" CssClass="btn btn-danger" Text="Eliminar Respuesta" OnClick="btnDeleteAnswer_Click" />
            </div>
        </div>
        <br />

    <div class="row">
    <div class="col">
        <h3>Listado de Respuestas</h3>
        <asp:GridView ID="gvAnswers" runat="server" AutoGenerateColumns="False" CssClass="table table-hover" OnSelectedIndexChanged="gvAnswers_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="res_id" HeaderText="ID Respuesta" />
                <asp:BoundField DataField="tbl_encuesta_en_id" HeaderText="ID Pregunta" />
                <asp:BoundField DataField="en_descripcion_pregunta" HeaderText="Descripción Pregunta" />
                <asp:BoundField DataField="res_respuesta" HeaderText="Respuesta" />
                <asp:BoundField DataField="nombre_usuario" HeaderText="Usuario" />
                <asp:CommandField ShowSelectButton="True" SelectText="Seleccionar" />
            </Columns>
        </asp:GridView>
    </div>
    </div>
    </div>
</asp:Content>


