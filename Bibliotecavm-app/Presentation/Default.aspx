<%@ Page Title="Iniciar Sesión" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Presentation.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Incluir Font Awesome para los íconos -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
    
    <style type="text/css">
        @import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@100;300;400;500;600;700&display=swap');
        
        body {
            font-family: 'Montserrat', sans-serif;
            background-color: #f5f5f5;
            margin: 0;
            padding: 0;
            min-height: 100vh;
        }
        
        .login-container {
            margin: 10% auto;
            border-radius: 5px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            max-width: 900px;
            background: white;
            display: flex;
            min-height: 500px;
            overflow: hidden;
        }
        
        /* Sección izquierda */
        .welcome-section {
            background-color: #1a237e;
            border-radius: 5px 0 0 5px;
            color: #fff;
            padding: 40px 30px;
            width: 45%;
            display: flex;
            flex-direction: column;
            justify-content: center;
        }
        
        .welcome-title {
            font-weight: 700;
            font-size: 2.2rem;
            margin-bottom: 15px;
            display: block;
            line-height: 1.3;
        }
        
        .welcome-subtitle {
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
            margin-bottom: 25px;
            position: relative;
        }
        
        .form-control {
            width: 100%;
            padding: 12px 15px;
            border: 1px solid #ddd;
            border-radius: 4px;
            font-size: 1rem;
            transition: border-color 0.3s;
        }
        
        .form-control.password-field {
            padding-right: 40px;
        }
        
        .form-control:focus {
            border-color: #1a237e;
            outline: 2px solid #1a237e;
            outline-offset: 2px;
        }
        
        /* Estilos para el toggle de contraseña */
        .password-container {
            position: relative;
        }
        
        .toggle-password {
            position: absolute;
            right: 12px;
            top: 50%;
            transform: translateY(-50%);
            cursor: pointer;
            color: #1a237e;
            background: none;
            border: none;
            padding: 5px;
            font-size: 1.1rem;
            z-index: 2;
        }
        
        .toggle-password:hover, 
        .toggle-password:focus {
            color: #303f9f;
            outline: 2px solid #1a237e;
            outline-offset: 2px;
        }
        
        .btn-login {
            width: 100%;
            padding: 12px;
            background-color: #1a237e;
            color: white;
            border: none;
            border-radius: 4px;
            font-weight: 600;
            font-size: 1rem;
            margin-top: 10px;
            cursor: pointer;
            transition: background-color 0.3s;
        }
        
        .btn-login:hover,
        .btn-login:focus {
            background-color: #303f9f;
            outline: 2px solid #5c6bc0;
            outline-offset: 2px;
        }
        
        .forget-password a,
        .form-links a {
            color: #1a237e;
            text-decoration: none;
            transition: color 0.3s;
        }
        
        .forget-password a:hover,
        .form-links a:hover {
            text-decoration: underline;
        }
        
        .forget-password {
            text-align: center;
            margin: 20px 0;
        }
        
        .error-message {
            color: #d32f2f;
            text-align: center;
            margin-bottom: 20px;
            font-weight: 500;
        }

        /* Estilos para lectores de pantalla */
        .sr-only {
            position: absolute;
            width: 1px;
            height: 1px;
            padding: 0;
            margin: -1px;
            overflow: hidden;
            clip: rect(0, 0, 0, 0);
            white-space: nowrap;
            border-width: 0;
        }

        /* ESTILOS DEL LOADER ESPIRAL - PANTALLA COMPLETA */
        .overlay {
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(255, 255, 255, 0.9);
            display: flex;
            justify-content: center;
            align-items: center;
            z-index: 1000;
            opacity: 0;
            visibility: hidden;
            transition: opacity 0.3s, visibility 0.3s;
        }
        
        .overlay.show {
            opacity: 1;
            visibility: visible;
        }
        
        .spiral-loader-container {
            position: relative;
            width: 200px;
            height: 200px;
            display: flex;
            flex-direction: column;
            align-items: center;
        }
        
        .spiral-loader {
            position: relative;
            width: 100%;
            height: 100%;
        }
        
        .dot {
            position: absolute;
            width: 10px;
            height: 10px;
            border-radius: 50%;
            background-color: #1a237e;
            transform-origin: center;
            opacity: 0;
        }
        
        .loading-text {
            color: #1a237e;
            font-size: 18px;
            font-weight: 500;
            margin-top: 20px;
            text-align: center;
        }
        
        @keyframes fadeInOut {
            0%, 100% { opacity: 0.2; transform: scale(0.8); }
            50% { opacity: 1; transform: scale(1); }
        }
        
        @keyframes rotateLoader {
            0% { transform: rotate(0deg); }
            100% { transform: rotate(360deg); }
        }

        /* Responsividad */
        @media (max-width: 768px) {
            .login-container {
                flex-direction: column;
                max-width: 90%;
                margin: 5% auto;
            }
            
            .welcome-section, .login-form {
                width: 100%;
            }
            
            .welcome-section {
                border-radius: 5px 5px 0 0;
                padding: 30px 20px;
                min-height: 200px;
            }
            
            .login-form {
                padding: 30px 25px;
            }
            
            .welcome-title {
                font-size: 1.8rem;
            }
            
            .welcome-subtitle {
                font-size: 1.1rem;
            }
        }
    </style> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-container">
        <!-- Sección izquierda con ARIA -->
        <div class="welcome-section" role="complementary" aria-label="Mensaje de bienvenida">
            <h2 class="welcome-title">Bienvenido</h2>
            <p class="welcome-subtitle">Amϴ umpusrϴ, lutϴ kutri kusrep pureintrik.</p>
        </div>
        
        <!-- Sección derecha (formulario) -->
        <div class="login-form" role="main" aria-labelledby="loginHeading">
            <h3 id="loginHeading">Iniciar Sesión</h3>
            <asp:Label ID="LblMsg" runat="server" CssClass="error-message" role="alert" aria-live="assertive" />
            
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="TBCorreo" CssClass="sr-only">Correo electrónico</asp:Label>
                <asp:TextBox ID="TBCorreo" runat="server" 
                    CssClass="form-control" 
                    placeholder="Correo electrónico" 
                    TextMode="Email" 
                    required=""
                    aria-label="Correo electrónico"
                    aria-required="true" />
            </div>
            
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="TBContrasena" CssClass="sr-only">Contraseña</asp:Label>
                <div class="password-container">
                    <asp:TextBox ID="TBContrasena" runat="server" 
                        CssClass="form-control password-field" 
                        placeholder="Contraseña" 
                        TextMode="Password" 
                        required=""
                        aria-label="Contraseña"
                        aria-required="true" />
                    <!-- Botón mejorado para accesibilidad -->
                    <button type="button" class="toggle-password" id="togglePasswordBtn" 
                        aria-label="Mostrar contraseña" 
                        aria-pressed="false"
                        aria-controls="<%= TBContrasena.ClientID %>">
                        <i class="far fa-eye-slash" aria-hidden="true"></i>  
                    </button>
                </div>
            </div>
            
            <asp:Button ID="BtGuardar" runat="server" 
                CssClass="btn-login" 
                Text="Iniciar Sesión" 
                OnClick="BtGuardar_Click"
                OnClientClick="showLoader(); return true;"
                aria-label="Iniciar sesión" />
            
            <div class="forget-password">
                <a href="#" aria-label="¿Olvidaste tu contraseña?">¿Olvidaste tu contraseña?</a> 
            </div>
            
            <div class="form-links">
                <a href="WFUserRegistration.aspx" aria-label="Registrarse">Registrate</a>
            </div>
        </div>
    </div>

    <!-- LOADER ESPIRAL PANTALLA COMPLETA -->
    <div class="overlay" id="loaderOverlay">
        <div class="spiral-loader-container">
            <div class="spiral-loader" id="spiral-loader"></div>
            <div class="loading-text">Procesando, por favor espere...</div>
        </div>
    </div>

    <script type="text/javascript">
        document.addEventListener('DOMContentLoaded', function () {
            // Auto-completar email
            const emailInput = document.getElementById('<%= TBCorreo.ClientID %>');
            if (emailInput) {
                emailInput.addEventListener("blur", function () {
                    let value = this.value.trim();
                    if (value !== "" && !value.includes("@")) {
                        this.value = value + "@gmail.com";
                    }
                });
            }

            // Toggle de contraseña
            const toggleBtn = document.getElementById('togglePasswordBtn');
            const passwordField = document.getElementById('<%= TBContrasena.ClientID %>');
            if (toggleBtn && passwordField) {
                const eyeIcon = toggleBtn.querySelector('i');

                function togglePassword() {
                    const isShowing = passwordField.type === "text";
                    passwordField.type = isShowing ? "password" : "text";
                    eyeIcon.classList.toggle('fa-eye-slash', !isShowing);
                    eyeIcon.classList.toggle('fa-eye', isShowing);
                    toggleBtn.setAttribute('aria-label', isShowing ? 'Mostrar contraseña' : 'Ocultar contraseña');
                    toggleBtn.setAttribute('aria-pressed', !isShowing);
                }

                toggleBtn.addEventListener('click', togglePassword);
                toggleBtn.addEventListener('keydown', function (e) {
                    if (e.key === 'Enter' || e.key === ' ') {
                        e.preventDefault();
                        togglePassword();
                    }
                });
            }

            // INICIALIZAR LOADER ESPIRAL
            initSpiralLoader('spiral-loader', 30, 10);
        });

        // FUNCIONES DEL LOADER ESPIRAL
        function initSpiralLoader(containerId, dotCount = 30, dotSize = 10) {
            const container = document.getElementById(containerId);
            if (!container) return;

            const centerX = container.offsetWidth / 2;
            const centerY = container.offsetHeight / 2;

            // Limpiar container
            container.innerHTML = '';

            // Crear los puntos de la espiral
            for (let i = 0; i < dotCount; i++) {
                const dot = document.createElement('div');
                dot.className = 'dot';
                container.appendChild(dot);

                // Calcular la posición en espiral
                const angle = 0.35 * i;
                const radius = 2 + (i * 3);
                const x = centerX + radius * Math.cos(angle);
                const y = centerY + radius * Math.sin(angle);

                // Aplicar estilo
                dot.style.left = `${x}px`;
                dot.style.top = `${y}px`;
                dot.style.width = `${dotSize}px`;
                dot.style.height = `${dotSize}px`;

                // Configurar la animación
                dot.style.animation = `
                    fadeInOut 1.5s infinite ${i * 0.05}s,
                    rotateLoader 4s linear infinite
                `;
            }
        }

        // Función para mostrar el loader (se ejecuta cuando se hace clic en el botón)
        function showLoader() {
            document.getElementById('loaderOverlay').classList.add('show');
        }

        // Función para ocultar el loader (puedes llamarla desde el código C#)
        function hideLoader() {
            document.getElementById('loaderOverlay').classList.remove('show');
        }
    </script>
</asp:Content>