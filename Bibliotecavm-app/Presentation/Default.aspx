<%@ Page Title="Iniciar Sesión" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Presentation.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Incluir Font Awesome para los íconos -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
    

    <script type="text/javascript">
        document.addEventListener('DOMContentLoaded', function () {
            const emailInput = document.getElementById('<%= TBCorreo.ClientID %>');

            emailInput.addEventListener("blur", function () {
                let value = emailInput.value.trim();

                // Si está vacío o ya contiene un dominio, no hacemos nada
                if (value === "" || value.includes("@")) return;

                // Si no tiene dominio, le añadimos @gmail.com al perder foco
                emailInput.value = value + "@gmail.com";
            });
        });
</script>



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
            position: relative;
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
            padding-right: 40px; /* Espacio para el ícono */
        }
        
        .form-control:focus {
            border-color: #1a237e;
            outline: none;
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
        
        .toggle-password:hover {
            color: #303f9f;
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
        
        .btn-login:hover {
            background-color: #303f9f;
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
            color: #e74c3c;
            text-align: center;
            margin-bottom: 20px;
            font-weight: 500;
        }
        
        /* Estilos para el cargador en espiral */
        .overlay {
            position: absolute;
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
            border-radius: 5px;
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
             
            /* Estilos mejorados para el toggle de contraseña */
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
            
            .toggle-password:hover {
                color: #303f9f;
            }
            
            .form-control.password-field {
                padding-right: 40px; /* Espacio para el ícono */
            }
        }
    </style> 
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-container">
        <!-- Sección izquierda -->
        <div class="welcome-section">
            <span class="welcome-title">Bienvenido</span>
            <span class="welcome-subtitle">Amϴ umpusrϴ, lutϴ kutri kusrep pureintrik.</span>
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
                <div class="password-container">
                    <asp:TextBox ID="TBContrasena" runat="server" 
                        CssClass="form-control password-field" 
                        placeholder="Contraseña" 
                        TextMode="Password" 
                        required="" />
                    <button type="button" class="toggle-password" id="togglePasswordBtn" aria-label="Mostrar contraseña">
                        <i class="far fa-eye-slash"></i>  
                    </button>
                </div>
            </div>
            
            <asp:Button ID="BtGuardar" runat="server" 
                CssClass="btn-login" 
                Text="Iniciar Sesión" 
                OnClick="BtGuardar_Click" 
                OnClientClick="showLoader(); return true;" />
            
            <div class="forget-password">
                <a href="#">¿Olvidaste tu contraseña?</a> 
            </div>
            
            <div class="form-links">
                <a href="WFUserRegistration.aspx">Registrate</a>
            </div>
        </div>
        
        <!-- Overlay del cargador en espiral -->
        <div class="overlay" id="loaderOverlay">
            <div class="spiral-loader-container">
                <div class="spiral-loader" id="spiral-loader"></div>
                <div class="loading-text">Procesando, por favor espere...</div>
            </div>
        </div>
    </div>

    <!-- Script para el toggle de contraseña y cargador -->
    <script type="text/javascript">
        document.addEventListener('DOMContentLoaded', function () {
            // Toggle de contraseña
            const toggleBtn = document.getElementById('togglePasswordBtn');
            const passwordField = document.getElementById('<%= TBContrasena.ClientID %>');
            const eyeIcon = toggleBtn.querySelector('i');

            // Función para alternar la visibilidad de la contraseña
            function togglePassword() {
                if (passwordField.type === "password") {
                    passwordField.type = "text";
                    eyeIcon.classList.replace('fa-eye-slash', 'fa-eye');
                    toggleBtn.setAttribute('aria-label', 'Ocultar contraseña');
                } else {
                    passwordField.type = "password";
                    eyeIcon.classList.replace('fa-eye', 'fa-eye-slash');
                    toggleBtn.setAttribute('aria-label', 'Mostrar contraseña');
                }
                passwordField.focus();
            }

            // Asignar evento click
            toggleBtn.addEventListener('click', togglePassword);

            // Manejar evento de teclado para accesibilidad
            toggleBtn.addEventListener('keydown', function (e) {
                if (e.key === 'Enter' || e.key === ' ') {
                    e.preventDefault();
                    togglePassword();
                }
            });

            // Inicializar el cargador en espiral
            initSpiralLoader();
        });

        // Función para mostrar el cargador
        function showLoader() {
            document.getElementById('loaderOverlay').classList.add('show');
        }

        // Función para inicializar el cargador en espiral
        function initSpiralLoader() {
            const container = document.getElementById('spiral-loader');
            const dotCount = 30;
            const centerX = 100;
            const centerY = 100;

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

                // Configurar la animación
                dot.style.animation = `
                    fadeInOut 1.5s infinite ${i * 0.05}s,
                    rotateLoader 4s linear infinite
                `;
            }
        }
    </script>
    

</asp:Content>