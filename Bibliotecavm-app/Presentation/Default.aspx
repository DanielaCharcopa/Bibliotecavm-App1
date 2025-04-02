<%@ Page Title="Iniciar Sesión" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Presentation.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-container">
        <!-- Sección izquierda -->
        <div class="ads">
            <h1>
                <span id="fl">Bienvenido</span>
                <span id="sl">Asha mϴrϴp paya kusrep puinuk (AshMϴr)</span>
            </h1>
        </div>
        
        <!-- Sección derecha (formulario) -->
        <div class="login-form">
            <h3>Iniciar Sesión</h3>
            <asp:Label ID="LblMsg" runat="server" CssClass="error-message" />
            
            <div class="form-group">
                <asp:TextBox ID="TBCorreo" runat="server" 
                    CssClass="form-control" 
                    placeholder="Correo electrónico" 
                    TextMode="Email" 
                    required="" />
            </div>
            
            <div class="form-group">
                <asp:TextBox ID="TBContrasena" runat="server" 
                    CssClass="form-control" 
                    placeholder="Contraseña" 
                    TextMode="Password" 
                    required="" />
            </div>
            
            <asp:Button ID="BtGuardar" runat="server" 
                CssClass="btn-login" 
                Text="Iniciar Sesión" 
                OnClick="BtGuardar_Click" />
            
            <div class="forget-password">
                <a href="#">¿Olvidaste tu contraseña?</a> 
            </div>
            
            <div class="form-links">
                <a href="WFUserRegistration.aspx">Registrate</a>
            </div>
        </div>
    </div>

    <style type="text/css">
    @import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@100;300;400;500;600;700&display=swap');
    
    body {
            font-family: 'Montserrat', sans-serif;
            background-color: #f5f5f5;
            margin: 0;
            padding: 0;
        }
        
        .login-container {
            margin: 10% auto;
            border-radius: 5px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            max-width: 900px;
            background: white;
            display: flex;
            min-height: 500px;
        }
        
        /* Sección izquierda - Cambiado a azul oscuro (#1a237e) */
        .ads {
            background-color: #1a237e;  /* Azul oscuro */
            border-radius: 5px 0 0 5px;
            color: #fff;
            padding: 40px 30px;
            width: 45%;
            display: flex;
            flex-direction: column;
            justify-content: center;
        }
        
        .ads h1 {
            margin: 0;
            line-height: 1.4;
        }
        
        #fl {
            font-weight: 700;
            font-size: 2.2rem;
            margin-bottom: 15px;
            display: block;
        }
        
        #sl {
            font-weight: 300;
            font-size: 1.3rem;
            display: block;
            line-height: 1.5;
        }
        
        /* Sección derecha (formulario) */
        .login-form {
            padding: 50px 40px;
            width: 55%;
            display: flex;
            flex-direction: column;
            justify-content: center;
        }
        
        .login-form h3 {
            text-align: center;
            color: #2c3e50;
            margin-bottom: 30px;
            font-size: 1.8rem;
        }
        
        .form-group {
            margin-bottom: 25px;  /* Espacio aumentado entre campos */
        }
        
        .form-control {
            width: 100%;
            padding: 12px 15px;
            border: 1px solid #ddd;
            border-radius: 4px;
            font-size: 1rem;
        }
        
        /* Botón cambiado a azul oscuro (#1a237e) */
        .btn-login {
            width: 100%;
            padding: 12px;
            background-color: #1a237e;  /* Azul oscuro */
            color: white;
            border: none;
            border-radius: 4px;
            font-weight: 600;
            font-size: 1rem;
            margin-top: 10px;
        }
        
        /* Enlace cambiado a azul oscuro (#1a237e) */
        .forget-password a,
        .form-links a {
            color: #1a237e;  /* Azul oscuro */
            text-decoration: none;
        }
        
        .forget-password {
            text-align: center;
            margin: 20px 0;
        }
        
        .error-message {
            color: #e74c3c;
            text-align: center;
            margin-bottom: 20px;
        }

        /* Responsividad */
        @media (max-width: 768px) {
            .login-container {
                flex-direction: column;
                max-width: 90%;
                margin: 5% auto;
            }
            
            .ads, .login-form {
                width: 100%;
            }
            
            .ads {
                border-radius: 5px 5px 0 0;
                padding: 30px 20px;
            }
            
            .login-form {
                padding: 30px 25px;
            }
            
            #fl {
                font-size: 1.8rem;
            }
            
            #sl {
                font-size: 1.1rem;
            }
        }
    </style>
</asp:Content>