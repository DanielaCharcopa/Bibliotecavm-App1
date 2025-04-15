using Logic;
using Model;
using SimpleCrypto;
using System;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Security;

namespace Presentation
{
    public partial class Default : System.Web.UI.Page
    {
        UserLogic objUserLog = new UserLogic();
        User objUser = new User();
        private string correo;
        private string contrasena;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Código que se ejecuta al cargar la página
        }

        protected void LnkLogout_Click(object sender, EventArgs e)
        {
            Response.Redirect("Logout.aspx");
        }

        protected void BtGuardar_Click(object sender, EventArgs e)
        {
            // 1. Validación inicial de campos
            string correo = TBCorreo.Text.Trim();
            string contrasena = TBContrasena.Text.Trim();

            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena))
            {
                MostrarError("El correo y la contraseña son obligatorios.");
                return;
            }

            try
            {
                // 2. Buscar usuario por correo
                ICryptoService cryptoService = new PBKDF2();
                User objUser = objUserLog.validateUserLogin(correo, contrasena);

                if (objUser == null)
                {
                    // Para seguridad, no revelar si el correo existe o no
                    MostrarError("Credenciales inválidas. Por favor verifique.");
                    return;
                }

                // 3. Validar contraseña con PBKDF2
                string passEncryp = cryptoService.Compute(contrasena, objUser.Salt);

                if (!cryptoService.Compare(objUser.Contrasena, passEncryp))
                {
                    MostrarError("Credenciales inválidas. Por favor verifique.");
                    return;
                }

                // 4. Configurar sesión segura
                ConfigurarSesionUsuario(objUser);

                // 5. Redirigir según rol
                RedirigirSegunRol(objUser.Rol);

                // Limpiar campos sensibles
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                // En producción, registrar el error (log)
                MostrarError("Ocurrió un error al iniciar sesión. Intente nuevamente.");
            }
        }



        // Métodos auxiliares para mejor legibilidad y reutilización

        private void ConfigurarSesionUsuario(User usuario)
        {
            // Configurar variables de sesión
            Session["UserId"] = usuario.UsuId;
            Session["Username"] = usuario.NombreCompleto;
            Session["UserRole"] = usuario.Rol;

            // Crear cookie de autenticación segura
            var ticket = new FormsAuthenticationTicket(
                version: 1,
                name: usuario.Correo,
                issueDate: DateTime.Now,
                expiration: DateTime.Now.AddMinutes(30),
                isPersistent: false,
                userData: usuario.Rol,
                cookiePath: FormsAuthentication.FormsCookiePath);

            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true, // Protección contra XSS
                Secure = FormsAuthentication.RequireSSL, // Solo HTTPS si está configurado
                Domain = FormsAuthentication.CookieDomain,
                Path = FormsAuthentication.FormsCookiePath
            };

            // Configurar SameSite según versión de .NET
            if (FormsAuthentication.RequireSSL)
            {
                authCookie.SameSite = SameSiteMode.None;
                authCookie.Secure = true;
            }

            Response.Cookies.Add(authCookie);
        }

        private void RedirigirSegunRol(string rol)
        {
            switch (rol?.ToLowerInvariant())
            {
                case "administrador":
                    Response.Redirect("WFStatisticReport2.aspx", false);
                    break;
                case "docente":
                case "estudiante":
                    Response.Redirect("WFPresentationMaterial.aspx", false);
                    break;
                default:
                    Response.Redirect("Default.aspx", false);
                    break;
            }

            Context.ApplicationInstance.CompleteRequest(); // Terminar la ejecución inmediatamente
        }

        private void LimpiarCampos()
        {
            TBCorreo.Text = string.Empty;
            TBContrasena.Text = string.Empty;
        }

        private void MostrarError(string mensaje)
        {
            LblMsg.Text = mensaje;
            LblMsg.ForeColor = System.Drawing.Color.Red;
        }



    }
}