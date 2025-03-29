
using Logic; // Asegúrate de importar la capa lógica
using Model; // Asegúrate de importar la capa de modelos
using System;
using System.Web;
using System.Web.Security;

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
            string correo = TBCorreo.Text.Trim();
            string contrasena = TBContrasena.Text.Trim();

            // Validar que los campos no estén vacíos
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(contrasena))
            {
                LblMsg.Text = "El correo y la contraseña son obligatorios.";
                LblMsg.ForeColor = System.Drawing.Color.Red;
                return; // Detener la ejecución si hay campos vacíos
            }

            try
            {
                UserLogic userLogic = new UserLogic();
                User usuario = userLogic.validateUserLogin(correo, contrasena);

                if (usuario != null)
                {
                    // Asignar valores a la sesión
                    Session["UserId"] = usuario.UsuId;
                    Session["Username"] = usuario.Correo;
                    Session["UserRole"] = usuario.Rol;

                    // Crear el ticket de autenticación
                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1, // Versión
                        usuario.Correo, // Nombre de usuario
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
                            Response.Redirect("WFUserManagement.aspx");
                            break;
                        case "Docente":
                            Response.Redirect("WFPresentationMaterial.aspx");
                            break;
                        case "Estudiante":
                            Response.Redirect("WFPresentationMaterial.aspx");
                            break;
                        default:
                            Response.Redirect("Default.aspx");
                            break;
                    }
                }
                else
                {
                    LblMsg.Text = "Correo no registrado o contraseña incorrecta.";
                    LblMsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                LblMsg.Text = "Ocurrió un error al iniciar sesión. Por favor, inténtelo de nuevo.";
                LblMsg.ForeColor = System.Drawing.Color.Red;
                // Registrar el error en un log
                // Logger.Log(ex);
            }
        }
    }
}


