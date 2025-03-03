using System;
using System.Web.Security;
using System.Web;
using Logic; // Asegúrate de importar la capa lógica
using Model; // Asegúrate de importar la capa de modelos

namespace Presentation
{
    public partial class Default : System.Web.UI.Page
    {
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
            string correo = TBCorreo.Text;
            string contrasena = TBContrasena.Text;

            // Validar las credenciales usando la capa lógica
            UserLogic userLogic = new UserLogic();
            User usuario = userLogic.validateUserLogin(correo, contrasena);

            if (usuario != null)
            {
                // Crear el ticket de autenticación
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                    1, // Versión
                    usuario.Correo, // Nombre de usuario (puede ser el correo)
                    DateTime.Now, // Fecha de creación
                    DateTime.Now.AddMinutes(30), // Fecha de expiración
                    false, // ¿Persistente?
                    usuario.Rol // Información adicional (el rol del usuario)
                );

                // Encriptar el ticket
                string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                // Crear la cookie de autenticación
                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                Response.Cookies.Add(authCookie);

                // Redirigir al usuario según su rol
                switch (usuario.Rol)
                {
                    case "Administrador":
                        Response.Redirect("WFUserManagement.aspx"); // Redirigir a la gestión de usuarios
                        break;
                    case "Docente":
                        Response.Redirect("WFSurvey.aspx"); // Redirigir a las encuestas
                        break;
                    case "Estudiante":
                        Response.Redirect("WFUserRegistration.aspx"); // Redirigir al registro de usuario
                        break;
                    default:
                        Response.Redirect("Default.aspx"); // Redirigir a una página por defecto si el rol no coincide
                        break;
                }
            }
            else
            {
                // Mostrar mensaje de error
                LblMsg.Text = "Correo o contraseña incorrectos.";
                LblMsg.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}