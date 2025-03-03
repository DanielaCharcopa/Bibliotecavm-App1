using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using System;
using System.Web;
using System.Web.Security;

namespace Presentation
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar si el usuario está autenticado
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    // Obtener el rol del usuario desde el ticket de autenticación
                    FormsIdentity identity = (FormsIdentity)HttpContext.Current.User.Identity;
                    FormsAuthenticationTicket ticket = identity.Ticket;
                    string userRole = ticket.UserData;

                    // Mostrar el nombre del usuario
                    lblUsername.Text = "Bienvenido, " + HttpContext.Current.User.Identity.Name;

                    // Mostrar el botón de cerrar sesión
                    lnkLogout.Visible = true;

                    // Mostrar el panel correspondiente al rol del usuario
                    switch (userRole)
                    {
                        case "Administrador":
                            pnlAdmin.Visible = true;
                            break;
                        case "Docente":
                            pnlDocente.Visible = true;
                            break;
                        case "Estudiante":
                            pnlEstudiante.Visible = true;
                            break;
                        default:
                            // Si el rol no coincide, ocultar todos los paneles
                            pnlAdmin.Visible = false;
                            pnlDocente.Visible = false;
                            pnlEstudiante.Visible = false;
                            break;
                    }
                }
                else
                {
                    // Si el usuario no está autenticado, ocultar los paneles y mostrar "Invitado"
                    lblUsername.Text = "Bienvenido, Invitado";
                    lnkLogout.Visible = false;
                    pnlAdmin.Visible = false;
                    pnlDocente.Visible = false;
                    pnlEstudiante.Visible = false;
                }
            }
        }

        // Método para cerrar sesión
        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            // Cerrar la sesión
            FormsAuthentication.SignOut();
            Session.Abandon();

            // Redirigir al usuario a la página de inicio
            Response.Redirect("Default.aspx");
        }
    }
}