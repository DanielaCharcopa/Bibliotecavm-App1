using Logic;
using Model;
using SimpleCrypto;
using System;
using System.Diagnostics;
using System.Web;
using System.Web.Security;

namespace Presentation
{
    public partial class Default : System.Web.UI.Page
    {
        UserLogic objUserLog = new UserLogic();

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
                // 2. Obtener usuario para extraer el salt
                User objUser = objUserLog.GetUserForLogin(correo);

                if (objUser == null)
                {
                    MostrarError("Credenciales inválidas. Por favor verifique.");
                    return;
                }

                // 3. Validar estado del usuario
                if (objUser.Estado != "Activo")
                {
                    MostrarError("No es posible iniciar sesión en este momento. Por favor contacte al administrador.");
                    return;
                }

                // 4. Encriptar contraseña ingresada con el salt del usuario
                ICryptoService cryptoService = new PBKDF2();
                string contrasenaEncriptada = cryptoService.Compute(contrasena, objUser.Salt);

                // 5. Validar credenciales con la capa lógica
                User usuarioAutenticado = objUserLog.validateUserLogin(correo, contrasenaEncriptada);

                // 6. Configurar sesión segura
                ConfigurarSesionUsuario(usuarioAutenticado);

                // 7. Redirigir según rol
                RedirigirSegunRol(usuarioAutenticado.Rol);

                // 8. Limpiar campos sensibles
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en login: {ex.Message}");

                if (ex.Message.Contains("inactivo") || ex.Message.Contains("inactiva"))
                {
                    MostrarError("Su cuenta está inactiva. Contacte al administrador.");
                }
                else if (ex.Message.Contains("Credenciales") || ex.Message.Contains("incorrectas"))
                {
                    MostrarError("Credenciales inválidas. Por favor verifique.");
                }
                else
                {
                    MostrarError("Ocurrió un error al iniciar sesión. Intente nuevamente.");
                }
            }
        }
        private void ConfigurarSesionUsuario(User usuario)
        {
            // Configurar variables de sesión
            Session["UserId"] = usuario.UsuId;
            Session["Username"] = usuario.NombreCompleto;
            Session["UserRole"] = usuario.Rol;
            Session["UserStatus"] = usuario.Estado;

            // Crear cookie de autenticación segura
            var ticket = new FormsAuthenticationTicket(
                version: 1,
                name: usuario.Correo,
                issueDate: DateTime.Now,
                expiration: DateTime.Now.AddMinutes(30),
                isPersistent: false,
                userData: $"{usuario.Rol}|{usuario.Estado}",
                cookiePath: FormsAuthentication.FormsCookiePath);

            string encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true,
                Secure = FormsAuthentication.RequireSSL,
                Domain = FormsAuthentication.CookieDomain,
                Path = FormsAuthentication.FormsCookiePath
            };

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
            Context.ApplicationInstance.CompleteRequest();
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