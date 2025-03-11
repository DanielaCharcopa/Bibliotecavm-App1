using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System;
using System.Web;
using System.Web.Security;

namespace Presentation
{
    public partial class MainUsuario : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar si el usuario está autenticado
                if (Session["UserRole"] != null)
                {
                    string userRole = Session["UserRole"].ToString();

                    // Mostrar el panel correspondiente según el rol
                    switch (userRole)
                    {
                        case "Administrador":
                            pnlAdmin.Visible = true;
                            pnlDocente.Visible = false;
                            pnlEstudiante.Visible = false;
                            break;
                        case "Docente":
                            pnlAdmin.Visible = false;
                            pnlDocente.Visible = true;
                            pnlEstudiante.Visible = false;
                            break;
                        case "Estudiante":
                            pnlAdmin.Visible = false;
                            pnlDocente.Visible = false;
                            pnlEstudiante.Visible = true;
                            break;
                        default:
                            // Si no tiene un rol válido, ocultar todos los paneles
                            pnlAdmin.Visible = false;
                            pnlDocente.Visible = false;
                            pnlEstudiante.Visible = false;
                            break;
                    }

                    // Mostrar el nombre del usuario
                    lblUsername.Text = "Bienvenido, " + Session["Username"].ToString();
                }
                else
                {
                    // Si no hay sesión, redirigir al login
                    Response.Redirect("~/Default.aspx");
                }
            }
        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            // Limpiar la sesión
            Session.Clear();
            Session.Abandon();

            // Eliminar la cookie de autenticación
            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
                authCookie.Expires = DateTime.Now.AddYears(-1); // Expirar la cookie
                Response.Cookies.Add(authCookie);
            }

            // Redirigir a la página de inicio de sesión
            Response.Redirect("~/Default.aspx");
        }
    }
}