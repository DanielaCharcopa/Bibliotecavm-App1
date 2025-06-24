<%@ Page Title="Iniciar Sesión" Language="C#" MasterPageFile="~/Main.Master"
         AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Presentation.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Incluir Font Awesome para los íconos -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css" />

    <style type="text/css">
        @import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@100;300;400;500;600;700&display=swap');
        body{font-family:'Montserrat',sans-serif;background:#f5f5f5;margin:0;padding:0;min-height:100vh;}
        .login-container{margin:10% auto;border-radius:5px;box-shadow:0 4px 8px rgba(0,0,0,.1);max-width:900px;background:#fff;display:flex;min-height:500px;overflow:hidden;}
        .welcome-section{background:#1a237e;border-radius:5px 0 0 5px;color:#fff;padding:40px 30px;width:45%;display:flex;flex-direction:column;justify-content:center;}
        .welcome-title{font-weight:700;font-size:2.2rem;margin-bottom:15px;line-height:1.3;}
        .welcome-subtitle{font-weight:300;font-size:1.3rem;line-height:1.5;}
        .login-form{padding:50px 40px;width:55%;display:flex;flex-direction:column;justify-content:center;}
        .login-form h3{text-align:center;color:#2c3e50;margin-bottom:30px;font-size:1.8rem;}
        .form-group{margin-bottom:25px;position:relative;}
        .form-control{width:100%;padding:12px 15px;border:1px solid #ddd;border-radius:4px;font-size:1rem;transition:border-color .3s;box-sizing:border-box;}
        .form-control.password-field{padding-right:45px !important;}
        .form-control:focus{border-color:#1a237e;outline:2px solid #1a237e;outline-offset:2px;}
        
        /* CORREGIR: Contenedor de contraseña con posición relativa clara */
        .password-container{position:relative;display:block;width:100%;}
        
        /* CORREGIR: Botón de toggle con posicionamiento absoluto mejorado */
        .toggle-password{
            position:absolute;
            right:12px;
            top:50%;
            transform:translateY(-50%);
            cursor:pointer;
            color:#1a237e;
            background:none;
            border:none;
            padding:8px;
            font-size:16px;
            z-index:10;
            width:auto;
            height:auto;
            display:flex;
            align-items:center;
            justify-content:center;
            line-height:1;
        }
        .toggle-password:hover,.toggle-password:focus{
            color:#303f9f;
            outline:2px solid #1a237e;
            outline-offset:2px;
            background-color:rgba(26, 35, 126, 0.1);
            border-radius:3px;
        }
        
        /* ASEGURAR que el ícono se muestre correctamente */
        .toggle-password i{
            font-size:16px;
            pointer-events:none;
            display:inline-block;
        }
        
        .btn-login{width:100%;padding:12px;background:#1a237e;color:#fff;border:none;border-radius:4px;font-weight:600;font-size:1rem;margin-top:10px;cursor:pointer;transition:background-color .3s;}
        .btn-login:hover,.btn-login:focus{background:#303f9f;outline:2px solid #5c6bc0;outline-offset:2px;}
        .forget-password{text-align:center;margin:20px 0;}
        .forget-password a,.form-links a{color:#1a237e;text-decoration:none;transition:color .3s;}
        .forget-password a:hover,.form-links a:hover{text-decoration:underline;}
        .error-message{color:#d32f2f;text-align:center;margin-bottom:20px;font-weight:500;}
        .sr-only{position:absolute;width:1px;height:1px;padding:0;margin:-1px;overflow:hidden;clip:rect(0,0,0,0);white-space:nowrap;border-width:0;}

        /* LOADER ESPIRAL */
        .overlay{position:fixed;top:0;left:0;width:100%;height:100%;background:rgba(255,255,255,.9);display:flex;justify-content:center;align-items:center;z-index:1000;opacity:0;visibility:hidden;transition:opacity .3s,visibility .3s;}
        .overlay.show{opacity:1;visibility:visible;}
        .spiral-loader-container{position:relative;width:200px;height:200px;display:flex;flex-direction:column;align-items:center;}
        .spiral-loader{position:relative;width:100%;height:100%;}
        .dot{position:absolute;width:10px;height:10px;border-radius:50%;background:#1a237e;transform-origin:center;opacity:0;}
        .loading-text{color:#1a237e;font-size:18px;font-weight:500;margin-top:20px;text-align:center;}
        @keyframes fadeInOut{0%,100%{opacity:.2;transform:scale(.8);}50%{opacity:1;transform:scale(1);}}
        @keyframes rotateLoader{0%{transform:rotate(0deg);}100%{transform:rotate(360deg);} }

        /* Responsividad */
        @media(max-width:768px){
            .login-container{flex-direction:column;max-width:90%;margin:5% auto;}
            .welcome-section,.login-form{width:100%;}
            .welcome-section{border-radius:5px 5px 0 0;padding:30px 20px;min-height:200px;}
            .login-form{padding:30px 25px;}
            .welcome-title{font-size:1.8rem;}
            .welcome-subtitle{font-size:1.1rem;}
        }

        /* DEBUG: Clase temporal para verificar posicionamiento */
        .debug-toggle{
            border:2px solid red !important;
            background-color:yellow !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="login-container">
        <!-- Sección izquierda -->
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
                <asp:TextBox ID="TBCorreo" runat="server" CssClass="form-control" placeholder="Correo electrónico"
                             TextMode="Email" required="" aria-label="Correo electrónico" aria-required="true" />
            </div>

            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="TBContrasena" CssClass="sr-only">Contraseña</asp:Label>
                <div class="password-container">
                    <asp:TextBox ID="TBContrasena" runat="server" CssClass="form-control password-field"
                                 placeholder="Contraseña" TextMode="Password" required=""
                                 aria-label="Contraseña" aria-required="true" />
                    <button type="button" class="toggle-password" id="togglePasswordBtn"
                            aria-label="Mostrar contraseña" aria-pressed="false"
                            tabindex="0">
                        <i class="fas fa-eye-slash" id="passwordIcon" aria-hidden="true"></i>
                    </button>
                </div>
            </div>

            <asp:Button ID="BtGuardar" runat="server" CssClass="btn-login" Text="Iniciar Sesión"
                        OnClick="BtGuardar_Click" OnClientClick="return validateAndShowLoader();"
                        aria-label="Iniciar sesión" />

            <div class="forget-password">
                <a href="#" aria-label="¿Olvidaste tu contraseña?">¿Olvidaste tu contraseña?</a>
            </div>

            <div class="form-links">
                <a href="WFUserRegistration.aspx" aria-label="Registrarse">Regístrate</a>
            </div>
        </div>
    </div>

    <!-- LOADER -->
    <div class="overlay" id="loaderOverlay">
        <div class="spiral-loader-container">
            <div class="spiral-loader" id="spiral-loader"></div>
            <div class="loading-text">Procesando, por favor espere...</div>
        </div>
    </div>

    <script type="text/javascript">
        document.addEventListener('DOMContentLoaded', function () {
            // Verificar que Font Awesome esté cargado
            console.log('Font Awesome loaded:', !!window.FontAwesome || !!document.querySelector('.fa'));

            /* Auto‑completar email */
            const emailInput = document.getElementById('<%= TBCorreo.ClientID %>');
            if (emailInput) {
                emailInput.addEventListener('blur', function () {
                    const val = this.value.trim();
                    if (val !== '' && !val.includes('@')) this.value = val + '@gmail.com';
                });
            }

            /* Toggle contraseña - MEJORADO */
            const toggleBtn = document.getElementById('togglePasswordBtn');
            const passwordField = document.getElementById('<%= TBContrasena.ClientID %>');
            
            if (toggleBtn && passwordField) {
                console.log('Toggle button found:', toggleBtn);
                console.log('Password field found:', passwordField);
                
                // Verificar que el ícono esté presente
                const eyeIcon = toggleBtn.querySelector('i') || document.getElementById('passwordIcon');
                console.log('Eye icon found:', eyeIcon);
                
                if (!eyeIcon) {
                    // Si no se encuentra el ícono, crearlo manualmente
                    const newIcon = document.createElement('i');
                    newIcon.className = 'fas fa-eye-slash';
                    newIcon.id = 'passwordIcon';
                    newIcon.setAttribute('aria-hidden', 'true');
                    toggleBtn.appendChild(newIcon);
                    console.log('Icon created manually');
                }
                
                const togglePassword = () => {
                    const currentIcon = toggleBtn.querySelector('i');
                    const isPasswordVisible = passwordField.type === 'text';
                    
                    // Cambiar tipo de input
                    passwordField.type = isPasswordVisible ? 'password' : 'text';
                    
                    // Cambiar ícono
                    if (currentIcon) {
                        currentIcon.className = isPasswordVisible ? 'fas fa-eye-slash' : 'fas fa-eye';
                    }
                    
                    // Actualizar atributos de accesibilidad
                    toggleBtn.setAttribute('aria-label', isPasswordVisible ? 'Mostrar contraseña' : 'Ocultar contraseña');
                    toggleBtn.setAttribute('aria-pressed', !isPasswordVisible);
                    
                    console.log('Password toggled. Visible:', !isPasswordVisible);
                };
                
                // Event listeners
                toggleBtn.addEventListener('click', function(e) {
                    e.preventDefault();
                    togglePassword();
                });
                
                toggleBtn.addEventListener('keydown', function(e) {
                    if (e.key === 'Enter' || e.key === ' ') {
                        e.preventDefault();
                        togglePassword();
                    }
                });
                
                // DEBUG: Agregar clase temporal para verificar posicionamiento
                // toggleBtn.classList.add('debug-toggle'); // Descomenta esta línea para debug
            } else {
                console.error('Toggle button or password field not found');
            }

            /* Inicializar loader una vez */
            initSpiralLoader('spiral-loader', 30, 10);
        });

        /* ========== LOADER ESPIRAL ========== */
        function initSpiralLoader(containerId, dotCount = 30, dotSize = 10) {
            const container = document.getElementById(containerId);
            if (!container) return;
            const centerX = container.offsetWidth / 2;
            const centerY = container.offsetHeight / 2;
            container.innerHTML = ''; // limpiar

            for (let i = 0; i < dotCount; i++) {
                const dot = document.createElement('div');
                dot.className = 'dot';
                container.appendChild(dot);

                const angle = 0.35 * i;
                const radius = 2 + (i * 3);
                const x = centerX + radius * Math.cos(angle);
                const y = centerY + radius * Math.sin(angle);

                dot.style.left = `${x}px`;
                dot.style.top = `${y}px`;
                dot.style.width = `${dotSize}px`;
                dot.style.height = `${dotSize}px`;
                dot.style.animation = `
                    fadeInOut 1.5s infinite ${i * 0.05}s,
                    rotateLoader 4s linear infinite
                `;
            }
        }

        /* Validar campos antes de mostrar loader y enviar formulario */
        function validateAndShowLoader() {
            const emailInput = document.getElementById('<%= TBCorreo.ClientID %>');
            const passwordInput = document.getElementById('<%= TBContrasena.ClientID %>');

            if (!emailInput || !passwordInput) return false;

            const emailVal = emailInput.value.trim();
            const passVal = passwordInput.value.trim();

            if (emailVal !== '' && passVal !== '') {
                // Mostrar loader y permitir post‑back
                document.getElementById('loaderOverlay').classList.add('show');
                return true;
            }

            // Focar el primer campo vacío y cancelar envío
            if (emailVal === '') { emailInput.focus(); }
            else { passwordInput.focus(); }

            return false;
        }

        /* Funciones auxiliares */
        function showLoader() { document.getElementById('loaderOverlay').classList.add('show'); }
        function hideLoader() { document.getElementById('loaderOverlay').classList.remove('show'); }
    </script>
</asp:Content>