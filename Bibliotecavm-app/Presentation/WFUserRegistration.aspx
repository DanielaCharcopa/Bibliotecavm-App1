<%@ Page Title="Registro de Usuarios" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFUserRegistration.aspx.cs" Inherits="Presentation.WFUserRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /* Estilos con contraste adecuado */
        .required-field::after {
            content: " *";
            color: #d32f2f;
            font-weight: bold;
        }
        
        .required-note {
            text-align: right;
            font-size: 14px;
            color: #333;
            margin-bottom: 20px;
        }
        
        /* Mensajes accesibles */
        .error-message {
            color: #d32f2f;
            font-size: 14px;
            margin-top: 5px;
            display: block;
            transition: all 0.3s ease;
        }
        
        .success-message {
            color: #388e3c;
            font-size: 14px;
            margin-top: 5px;
            display: block;
            transition: all 0.3s ease;
        }
        
        .password-hint {
            font-size: 13px;
            color: #424242;
            margin-top: 5px;
            display: block;
        }
        
        /* Barra de progreso accesible */
        .password-strength {
            height: 8px;
            margin-top: 5px;
            background-color: #e0e0e0;
            border-radius: 3px;
            overflow: hidden;
        }
        
        .strength-bar {
            height: 100%;
            width: 0%;
            transition: width 0.3s, background-color 0.3s;
        }

        /* Placeholders accesibles */
        ::placeholder {
            color: #616161;
            font-style: italic;
            opacity: 1;
        }

        /* Estilos de enfoque mejorados */
        .form-control:focus {
            outline: 2px solid #1a237e;
            outline-offset: 2px;
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
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-container">
        <h1>Registro de Usuarios</h1>
        
        <%--Nota accesible sobre campos obligatorios--%>  
        <div class="required-note" aria-hidden="true">
            Los campos marcados con <span style="color: #d32f2f; font-weight: bold;">*</span> son obligatorios
        </div>
        <div class="sr-only">Todos los campos marcados con asterisco son obligatorios</div>
        
        <%--Mensaje de retroalimentación general con ARIA--%>  
        <asp:Label ID="LblMessage" runat="server" CssClass="message" role="alert" aria-live="polite"></asp:Label>

        <%--Campos del formulario con correcciones de accesibilidad--%>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="TBFirstName" CssClass="required-field">Nombre:</asp:Label>
            <asp:TextBox ID="TBFirstName" runat="server" CssClass="form-control" 
                aria-required="true" required="required"></asp:TextBox>
            <asp:Label ID="LblNombreMessage" runat="server" CssClass="error-message" 
                Visible="false" role="alert" aria-live="polite"></asp:Label>
        </div>
        
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="TBLastName" CssClass="required-field">Apellido:</asp:Label>
            <asp:TextBox ID="TBLastName" runat="server" CssClass="form-control" 
                aria-required="true" required="required"></asp:TextBox>
            <asp:Label ID="LblApellidoMessage" runat="server" CssClass="error-message" 
                Visible="false" role="alert" aria-live="polite"></asp:Label>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="TBEmail" CssClass="required-field">Correo Electrónico:</asp:Label>
            <asp:TextBox ID="TBEmail" runat="server" CssClass="form-control"
                aria-required="true" required="required" placeholder="ejemplo@gmail.com"
                autocomplete="email" inputmode="email" aria-describedby="emailHelp"></asp:TextBox>
            <span id="emailHelp" class="password-hint">Debe ser una dirección de Gmail válida</span>
            <asp:Label ID="LblCorreoMessage" runat="server" CssClass="error-message" 
                Visible="false" role="alert" aria-live="polite"></asp:Label>
        </div>
        
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="TBPassword" CssClass="required-field">Contraseña:</asp:Label>
            <asp:TextBox ID="TBPassword" runat="server" TextMode="Password" CssClass="form-control" 
                aria-required="true" required="required" aria-describedby="passwordHelp passwordStrengthDesc"></asp:TextBox>
            <%--Barra de fortaleza accesible--%> 
            <div class="password-strength" aria-hidden="true">
                <div id="strengthBar" class="strength-bar"></div>
            </div>
            <div id="passwordStrengthDesc" class="sr-only" aria-live="polite"></div>
            <%--Mensaje de validación--%> 
            <asp:Label ID="LblPasswordMessage" runat="server" CssClass="error-message" 
                Text="La contraseña debe tener al menos 6 caracteres." role="alert"></asp:Label>
            <small id="passwordHelp" class="password-hint">Mínimo 6 caracteres, debe incluir letras mayúsculas, minúsculas y números</small>
        </div>

        <%--Botón accesible--%> 
        <div class="form-group" style="margin-top: 30px;">
            <asp:Button ID="BtnSave" runat="server" Text="Registrarse" OnClick="BtnSave_Click" 
                CssClass="btn-primary" AccessKey="s" />
        </div>
    </div>
    
    <script>
        // Validación accesible de contraseña
        document.getElementById("<%= TBPassword.ClientID %>").addEventListener('input', function () {
            var password = this.value;
            var message = document.getElementById("<%= LblPasswordMessage.ClientID %>");
            var strengthBar = document.getElementById("strengthBar");
            var strengthDesc = document.getElementById("passwordStrengthDesc");

            // Validar longitud mínima
            if (password.length > 0 && password.length < 6) {
                message.textContent = "Error: La contraseña es muy corta (mínimo 6 caracteres)";
                message.className = "error-message";
                strengthBar.style.width = (password.length * 10) + "%";
                strengthBar.style.backgroundColor = "#d32f2f";
                strengthDesc.textContent = "Fortaleza de contraseña: muy débil";
            }
            else if (password.length >= 6) {
                // Calcular fortaleza de la contraseña
                var strength = calculatePasswordStrength(password);

                // Actualizar mensaje y barra de fortaleza
                if (strength < 40) {
                    message.textContent = "Advertencia: Contraseña débil";
                    message.className = "error-message";
                    strengthBar.style.backgroundColor = "#ff9800";
                    strengthDesc.textContent = "Fortaleza de contraseña: débil";
                } else if (strength < 70) {
                    message.textContent = "Contraseña aceptable";
                    message.className = "success-message";
                    strengthBar.style.backgroundColor = "#ffc107";
                    strengthDesc.textContent = "Fortaleza de contraseña: moderada";
                } else {
                    message.textContent = "Contraseña fuerte";
                    message.className = "success-message";
                    strengthBar.style.backgroundColor = "#388e3c";
                    strengthDesc.textContent = "Fortaleza de contraseña: fuerte";
                }
                strengthBar.style.width = strength + "%";
            }
            else {
                message.textContent = "La contraseña debe tener al menos 6 caracteres.";
                message.className = "error-message";
                strengthBar.style.width = "0%";
                strengthDesc.textContent = "";
            }
        });

        // Función para calcular fortaleza de contraseña
        function calculatePasswordStrength(password) {
            var strength = 0;

            // Longitud
            strength += Math.min(password.length * 5, 30);

            // Contiene números
            if (password.match(/\d/)) strength += 10;

            // Contiene mayúsculas y minúsculas
            if (password.match(/[a-z]/) && password.match(/[A-Z]/)) strength += 15;

            // Contiene caracteres especiales
            if (password.match(/[^a-zA-Z0-9]/)) strength += 15;

            // No es una palabra común
            if (!password.match(/password|123456|qwerty/i)) strength += 10;

            return Math.min(strength, 100);
        }

        // Validación accesible del formulario
        document.getElementById("<%= BtnSave.ClientID %>").addEventListener('click', function (e) {
            var password = document.getElementById("<%= TBPassword.ClientID %>");
            var email = document.getElementById("<%= TBEmail.ClientID %>");
            var regex = /^[a-zA-Z0-9._%+-]+@gmail\.com$/;
            var isValid = true;

            // Validar correo
            if (!regex.test(email.value)) {
                document.getElementById("<%= LblCorreoMessage.ClientID %>").textContent = "Error: Por favor, ingrese un correo electrónico válido de Gmail (ejemplo@gmail.com).";
                document.getElementById("<%= LblCorreoMessage.ClientID %>").style.display = "block";
                email.focus();
                email.setAttribute("aria-invalid", "true");
                isValid = false;
            } else {
                email.setAttribute("aria-invalid", "false");
            }

            // Validar contraseña
            if (password.value.length < 6) {
                document.getElementById("<%= LblPasswordMessage.ClientID %>").textContent = "Error: La contraseña debe tener al menos 6 caracteres.";
                document.getElementById("<%= LblPasswordMessage.ClientID %>").style.display = "block";
                password.focus();
                password.setAttribute("aria-invalid", "true");
                isValid = false;
            } else {
                password.setAttribute("aria-invalid", "false");
            }

            if (!isValid) {
                e.preventDefault();
                // Mover foco al primer error
                var firstError = document.querySelector('[aria-invalid="true"]');
                if (firstError) {
                    firstError.focus();
                }
            }
        });

        // Manejo accesible del campo de email
        document.getElementById("<%= TBEmail.ClientID %>").addEventListener('blur', function () {
            var email = this.value;
            var message = document.getElementById("<%= LblCorreoMessage.ClientID %>");

            if (email && !email.endsWith('@gmail.com')) {
                message.textContent = "Error: Solo se permiten correos de Gmail (ejemplo@gmail.com)";
                message.style.display = "block";
                this.setAttribute("aria-invalid", "true");
            } else {
                message.style.display = "none";
                this.setAttribute("aria-invalid", "false");
            }
        });
    </script>
    
    <style type="text/css">
        /* Estilos generales accesibles */
        .form-container {
            max-width: 800px;
            margin: 3% auto;
            padding: 3%;
            background: #fff;
            border-radius: 10px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
        }
        
        h1 {
            text-align: center;
            color: #1a237e;
            margin-bottom: 30px;
            font-size: 2rem;
        }
        
        /* Campos accesibles */
        .form-group {
            margin-bottom: 20px;
        }
        
        label {
            display: block;
            margin-bottom: 8px;
            color: #1a237e;
            font-weight: 600;
            font-size: 1rem;
        }
        
        .form-control {
            width: 100%;
            padding: 12px;
            border: 2px solid #ddd;
            border-radius: 5px;
            font-size: 16px;
            transition: border-color 0.3s;
        }
        
        .form-control:focus {
            border-color: #1a237e;
            box-shadow: 0 0 0 2px rgba(26, 35, 126, 0.2);
        }
        
        /* Botones accesibles */
        .btn-primary {
            background-color: #1a237e;
            color: white;
            border: none;
            padding: 14px 25px;
            border-radius: 5px;
            font-size: 1rem;
            font-weight: 600;
            cursor: pointer;
            transition: background-color 0.3s;
            width: 100%;
        }
        
        .btn-primary:hover, .btn-primary:focus {
            background-color: #303f9f;
            outline: 2px solid #5c6bc0;
            outline-offset: 2px;
        }
        
        /* Mensajes accesibles */
        .message {
            display: block;
            padding: 12px;
            margin-bottom: 20px;
            border-radius: 5px;
            text-align: center;
            font-weight: 600;
        }
        
        /* Responsividad accesible */
        @media (max-width: 768px) {
            .form-container {
                margin: 5% 3%;
                padding: 20px;
            }
            
            h1 {
                font-size: 1.5rem;
            }
            
            .btn-primary {
                padding: 12px 20px;
            }
        }
        
        @media (prefers-reduced-motion: reduce) {
            * {
                transition: none !important;
            }
        }
    </style>
</asp:Content>