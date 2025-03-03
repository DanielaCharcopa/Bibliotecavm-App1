<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logout.aspx.cs" Inherits="Presentation.Logout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Cerrar Sesión</title>
    <style>
        .loading-message {
            font-family: Arial, sans-serif;
            font-size: 18px;
            color: #333;
            text-align: center;
            margin-top: 20%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="loading-message">
            <p>Cerrando sesión...</p>
            <p>Por favor, espere un momento.</p>
        </div>
    </form>
</body>
</html>