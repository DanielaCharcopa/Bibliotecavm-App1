<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Presentation.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Iniciar Sesión</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <!-- Título de la página -->
            <asp:Label ID="Label2" runat="server" Text="Iniciar Sesión" Font-Bold="true" Font-Size="Large"></asp:Label>
            <br /><br />

            <!-- Campo para el correo -->
            <asp:Label ID="Label1" runat="server" Text="Correo:"></asp:Label>
            <asp:TextBox ID="TBCorreo" runat="server"></asp:TextBox>
            <br /><br />

            <!-- Campo para la contraseña -->
            <asp:Label ID="Label3" runat="server" Text="Contraseña:"></asp:Label>
            <asp:TextBox ID="TBContrasena" runat="server" TextMode="Password"></asp:TextBox>
            <br /><br />

            <!-- Botón para iniciar sesión -->
            <asp:Button ID="BtGuardar" runat="server" Text="Iniciar Sesión" OnClick="BtGuardar_Click" />
            <br /><br />

            <!-- Mensaje de retroalimentación -->
            <asp:Label ID="LblMsg" runat="server" Text="" ForeColor="Red"></asp:Label>
        </div>



        <asp:LinkButton ID="LnkLogout" runat="server" OnClick="LnkLogout_Click">Cerrar Sesión</asp:LinkButton>

    </form>
</body>
</html>