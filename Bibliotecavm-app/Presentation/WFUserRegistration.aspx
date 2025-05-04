<%@ Page Title="Registro de Usuarios" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFUserRegistration.aspx.cs" Inherits="Presentation.WFUserRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /* Estilos para campos obligatorios */
        .required-field::after {
            content: " *";
            color: #e53935;
            font-weight: bold;
        }
        
        .required-note {
            text-align: right;
            font-size: 12px;
            color: #666;
            margin-bottom: 20px;
        }
        
        /* Estilos para mensajes de validación */
        .error-message {
            color: #e53935;
            font-size: 14px;
            margin-top: 5px;
            display: block;
            transition: all 0.3s ease;
        }
        
        .success-message {
            color: #4CAF50;
            font-size: 14px;
            margin-top: 5px;
            display: block;
            transition: all 0.3s ease;
        }
        
        .password-hint {
            font-size: 12px;
            color: #666;
            margin-top: 5px;
            display: block;
        }
        
        /* Estilos para indicador de fortaleza de contraseña */
        .password-strength {
            height: 5px;
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

        /* Estilo para el placeholder */
        ::-webkit-input-placeholder { /* Chrome/Opera/Safari */
            color: #999;
            font-style: italic;
        }

        ::-moz-placeholder { /* Firefox 19+ */
            color: #999;
            font-style: italic;
        }

        :-ms-input-placeholder { /* IE 10+ */
            color: #999;
            font-style: italic;
        }

        :-moz-placeholder { /* Firefox 18- */
            color: #999;
            font-style: italic;
        }

        /* Estilo cuando el campo está enfocado */
        .form-control:focus::-webkit-input-placeholder {
            color: #ddd;
        }


    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-container">
        <h2>Registro de Usuarios</h2>
        
        <!-- Nota sobre campos obligatorios -->
        <div class="required-note">Los campos marcados con <span style="color: #e53935; font-weight: bold;">*</span> son obligatorios</div>
        
        <!-- Mensaje de retroalimentación general -->
        <asp:Label ID="LblMessage" runat="server" CssClass="message"></asp:Label>

        <!-- Campos del formulario -->
        <div class="form-group">
            <label for="TBFirstName" class="required-field">Nombre:</label>
            <asp:TextBox ID="TBFirstName" runat="server" CssClass="form-control" aria-required="true"></asp:TextBox>
            <asp:Label ID="LblNombreMessage" runat="server" CssClass="error-message" Visible="false"></asp:Label>
        </div>
        
        <div class="form-group">
            <label for="TBLastName" class="required-field">Apellido:</label>
            <asp:TextBox ID="TBLastName" runat="server" CssClass="form-control" aria-required="true"></asp:TextBox>
            <asp:Label ID="LblApellidoMessage" runat="server" CssClass="error-message" Visible="false"></asp:Label>
        </div>

    <div class="form-group">
        <label for="TBEmail" class="required-field">Correo Electrónico:</label>
        <asp:TextBox ID="TBEmail" runat="server" CssClass="form-control"
            aria-required="true" placeholder="ejemplo@gmail.com"
            autocomplete="email"></asp:TextBox>
        <asp:Label ID="LblCorreoMessage" runat="server" CssClass="error-message" Visible="false"></asp:Label>
    </div>
        
        <div class="form-group">
            <label for="TBPassword" class="required-field">Contraseña:</label>
            <asp:TextBox ID="TBPassword" runat="server" TextMode="Password" CssClass="form-control" 
                aria-required="true" aria-describedby="passwordHelp"></asp:TextBox>
            <!-- Barra de fortaleza de contraseña -->
            <div class="password-strength">
                <div id="strengthBar" class="strength-bar"></div>
            </div>
            <!-- Mensaje de validación -->
            <asp:Label ID="LblPasswordMessage" runat="server" CssClass="error-message" 
                Text="La contraseña debe tener al menos 6 caracteres."></asp:Label>
            <small id="passwordHelp" class="password-hint">Mínimo 6 caracteres</small>
        </div>
        
        <div class="form-group">
            <label for="DDLRole" class="required-field">Rol:</label>
            <asp:DropDownList ID="DDLRole" runat="server" CssClass="form-control" aria-required="true">
                <asp:ListItem Text="Seleccione un rol" Value="" />
                <asp:ListItem Text="Docente" Value="Docente" />
                <asp:ListItem Text="Estudiante" Value="Estudiante" />
            </asp:DropDownList>
        </div>
        
        <div class="form-group">
            <label for="DDLEducationLevel" class="required-field">Nivel Educativo:</label>
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

        <!-- Botón de Guardar -->
        <div class="form-group" style="margin-top: 30px;">
            <asp:Button ID="BtnSave" runat="server" Text="Registrarse" OnClick="BtnSave_Click" CssClass="btn-primary" />
        </div>
    </div>
    
    <script>
        // Validación en tiempo real de la contraseña
        document.getElementById("<%= TBPassword.ClientID %>").addEventListener('input', function () {
            var password = this.value;
            var message = document.getElementById("<%= LblPasswordMessage.ClientID %>");
            var strengthBar = document.getElementById("strengthBar");

            // Validar longitud mínima
            if (password.length > 0 && password.length < 6) {
                message.textContent = "❌ La contraseña es muy corta (mínimo 6 caracteres)";
                message.className = "error-message";
                strengthBar.style.width = (password.length * 10) + "%";
                strengthBar.style.backgroundColor = "#e53935";
            }
            else if (password.length >= 6) {
                // Calcular fortaleza de la contraseña
                var strength = calculatePasswordStrength(password);

                // Actualizar mensaje y barra de fortaleza
                if (strength < 40) {
                    message.textContent = "⚠️ Contraseña débil";
                    message.className = "error-message";
                    strengthBar.style.backgroundColor = "#ff9800";
                } else if (strength < 70) {
                    message.textContent = "👍 Contraseña aceptable";
                    message.className = "success-message";
                    strengthBar.style.backgroundColor = "#ffc107";
                } else {
                    message.textContent = "✅ Contraseña fuerte";
                    message.className = "success-message";
                    strengthBar.style.backgroundColor = "#4CAF50";
                }
                strengthBar.style.width = strength + "%";
            }
            else {
                message.textContent = "La contraseña debe tener al menos 6 caracteres.";
                message.className = "error-message";
                strengthBar.style.width = "0%";
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

        // Validación del formulario antes de enviar
        document.getElementById("<%= BtnSave.ClientID %>").addEventListener('click', function (e) {
            var password = document.getElementById("<%= TBPassword.ClientID %>").value;
            var email = document.getElementById("<%= TBEmail.ClientID %>").value;
            var regex = /^[a-zA-Z0-9._%+-]+@gmail\.com$/;
            var isValid = true;

            // Validar correo
            if (!regex.test(email)) {
                alert("Por favor, ingrese un correo electrónico válido de Gmail (ejemplo@gmail.com).");
                isValid = false;
            }

            // Validar contraseña
            if (password.length < 6) {
                document.getElementById("<%= LblPasswordMessage.ClientID %>").textContent = "❌ La contraseña debe tener al menos 6 caracteres.";
                document.getElementById("<%= LblPasswordMessage.ClientID %>").style.display = "block";
                isValid = false;
            }

            if (!isValid) {
                e.preventDefault(); // Evitar envío del formulario
            }
        });

        // Autocompletado para correo Gmail
        document.getElementById("<%= TBEmail.ClientID %>").addEventListener('input', function (e) {
            var emailInput = this;
            var value = emailInput.value.trim();

            // Solo autocompletar si no contiene @
            if (!value.includes('@') && value.length > 0) {
                emailInput.value = value + '@gmail.com';

                // Colocar el cursor antes del @gmail.com
                var cursorPos = value.length;
                emailInput.setSelectionRange(cursorPos, cursorPos);
            }

            // Validación visual en tiempo real
            var message = document.getElementById("<%= LblCorreoMessage.ClientID %>");
    if (value.includes('@') && !value.endsWith('@gmail.com')) {
        message.textContent = "❌ Solo se permiten correos de Gmail";
        message.style.display = "block";
    } else {
        message.style.display = "none";
    }
});

// Evitar que el usuario modifique el dominio @gmail.com
        document.getElementById("<%= TBEmail.ClientID %>").addEventListener('keydown', function (e) {
            var emailInput = this;
            var value = emailInput.value;
            var cursorPos = emailInput.selectionStart;

            // Si el cursor está después del @ y presiona backspace o delete
            if ((e.key === 'Backspace' || e.key === 'Delete') &&
                cursorPos > value.indexOf('@') && value.includes('@')) {
                e.preventDefault();
            }

            // Si intenta pegar texto
            if (e.ctrlKey && e.key === 'v') {
                setTimeout(function () {
                    if (!emailInput.value.endsWith('@gmail.com')) {
                        var userPart = emailInput.value.split('@')[0];
                        if (userPart) {
                            emailInput.value = userPart + '@gmail.com';
                        }
                    }
                }, 10);
            }
        });

    </script>
    
    <style type="text/css">
        /* Estilos generales */
        .form-container {
            max-width: 800px;
            margin: 3% auto;
            padding: 3%;
            background: #fff;
            border-radius: 10px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
        }
        
        h2 {
            text-align: center;
            color: #1a237e;
            margin-bottom: 30px;
        }
        
        /* Campos del formulario */
        .form-group {
            margin-bottom: 20px;
        }
        
        label {
            display: block;
            margin-bottom: 8px;
            color: #1a237e;
            font-weight: 500;
        }
        
        .form-control {
            width: 100%;
            padding: 12px;
            border: 1px solid #ddd;
            border-radius: 5px;
            font-size: 16px;
            transition: border-color 0.3s;
        }
        
        .form-control:focus {
            border-color: #1a237e;
            outline: none;
        }
        
        /* Botones */
        .btn-primary {
            background-color: #1a237e;
            color: white;
            border: none;
            padding: 12px 25px;
            border-radius: 5px;
            font-size: 16px;
            cursor: pointer;
            transition: background-color 0.3s;
            width: 100%;
        }
        
        .btn-primary:hover {
            background-color: #303f9f;
        }
        
        /* Mensajes */
        .message {
            display: block;
            padding: 10px;
            margin-bottom: 20px;
            border-radius: 5px;
            text-align: center;
        }
        
        /* Dropdowns */
        select.form-control {
            height: 45px;
            appearance: none;
            -webkit-appearance: none;
            -moz-appearance: none;
            background-image: url("data:image/svg+xml;charset=UTF-8,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 24 24' fill='%231a237e'%3e%3cpath d='M7 10l5 5 5-5z'/%3e%3c/svg%3e");
            background-repeat: no-repeat;
            background-position: right 10px center;
            background-size: 20px;
        }
        
        /* Responsividad */
        @media (max-width: 768px) {
            .form-container {
                margin: 5% auto;
                padding: 20px;
            }
            
            h2 {
                font-size: 24px;
            }
        }
    </style>
</asp:Content>