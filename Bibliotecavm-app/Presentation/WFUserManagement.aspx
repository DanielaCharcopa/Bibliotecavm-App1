<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFUserManagement.aspx.cs" Inherits="Presentation.WFUserManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="form-container">
        <h2>Gestión de Usuarios</h2>
        <asp:Label ID="LblMessage" runat="server" CssClass="message"></asp:Label>

        <!-- Campo oculto para almacenar el ID del usuario seleccionado -->
        <asp:HiddenField ID="HFUserId" runat="server" />

        <!-- Campos del formulario -->
        <div>
            <label for="TBFirstName">Nombre:</label>
            <asp:TextBox ID="TBFirstName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div>
            <label for="TBLastName">Apellido:</label>
            <asp:TextBox ID="TBLastName" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div>
            <label for="TBEmail">Correo Electrónico:</label>
            <asp:TextBox ID="TBEmail" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
        <div>
            <label for="TBPassword">Contraseña:</label>
            <asp:TextBox ID="TBPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
        </div>
        <div>
            <label for="TBSalt">Salt:</label>
            <asp:TextBox ID="TBSalt" runat="server" CssClass="form-control"></asp:TextBox>
        </div>
    <div>
        <label for="DDLRole">Rol:</label>
        <asp:DropDownList ID="DDLRole" runat="server" CssClass="form-control">
            <asp:ListItem Text="Seleccione un rol" Value="" />
            <asp:ListItem Text="Administrador" Value="Administrador" />
            <asp:ListItem Text="Docente" Value="Docente" />
            <asp:ListItem Text="Estudiante" Value="Estudiante" />
        </asp:DropDownList>
    </div>
    <div>
        <label for="DDLEducationLevel">Nivel Educativo:</label>
        <asp:DropDownList ID="DDLEducationLevel" runat="server" CssClass="form-control">
            <asp:ListItem Text="Seleccione un nivel" Value="" />
            <asp:ListItem Text="Primaria" Value="Primaria" />
            <asp:ListItem Text="Secundaria" Value="Secundaria" />
            <asp:ListItem Text="Bachillerato" Value="Bachillerato" />
            <asp:ListItem Text="Técnico" Value="Técnico" />
            <asp:ListItem Text="Tecnólogo" Value="Tecnólogo" />
            <asp:ListItem Text="Pregrado" Value="Pregrado" />
            <asp:ListItem Text="Especialización" Value="Especialización" />
            <asp:ListItem Text="Maestría" Value="Maestría" />
            <asp:ListItem Text="Doctorado" Value="Doctorado" />
            <asp:ListItem Text="Postdoctorado" Value="Postdoctorado" />
        </asp:DropDownList>
    </div>

         <%--Botones de acción--%> 
        <div style="margin-top: 20px;">
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" CssClass="btn btn-primary" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" CssClass="btn btn-warning" />
            <asp:Button ID="BtnDelete" runat="server" Text="Eliminar" OnClick="BtnDelete_Click" CssClass="btn btn-danger" />
            <asp:Button ID="BtnClear" runat="server" Text="Limpiar" OnClientClick="ClearForm()" CssClass="btn btn-secondary" />
        </div>
    </div>

    <%--GridView para mostrar los usuarios--%>
    <div class="grid-container">
        <asp:GridView ID="GVUsers" runat="server" CssClass="table table-bordered" AutoGenerateColumns="False" DataKeyNames="usu_id" OnSelectedIndexChanged="GVUsers_SelectedIndexChanged1">
            <Columns>
                <asp:BoundField DataField="usu_id" HeaderText="ID" />
                <asp:BoundField DataField="usu_nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="usu_apellido" HeaderText="Apellido" />
                <asp:BoundField DataField="usu_correo" HeaderText="Correo Electrónico" />
                <asp:BoundField DataField="usu_rol" HeaderText="Rol" />
                <asp:BoundField DataField="usu_nivel_estudios" HeaderText="Nivel Educativo" />
                <asp:CommandField ShowSelectButton="True" HeaderText="Opción" SelectText="Seleccionar" />
            </Columns>
        </asp:GridView>

    </div>
</asp:Content>
