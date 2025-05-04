using System;
using System.Web;
using System.Web.Security;

namespace Presentation
{
    public partial class MainUsuario : System.Web.UI.MasterPage
    {
        public string CurrentPage { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Determinar la página actual para resaltar en el menú
                CurrentPage = System.IO.Path.GetFileNameWithoutExtension(Request.Url.AbsolutePath);

                // Verificar si el usuario está autenticado
                if (Session["UserRole"] != null && Session["Username"] != null)
                {
                    string userRole = Session["UserRole"].ToString();

                    // Mostrar el panel correspondiente según el rol
                    switch (userRole)
                    {
                        case "Administrador":
                            // Redirigir a la página de admin si es administrador
                            Response.Redirect("~/WFStatisticReport2.aspx");
                            break;
                        case "Docente":
                            pnlDocente.Visible = true;
                            pnlEstudiante.Visible = false;
                            break;
                        case "Estudiante":
                            pnlDocente.Visible = false;
                            pnlEstudiante.Visible = true;
                            break;
                        default:
                            // Si no tiene un rol válido, ocultar todos los paneles
                            pnlDocente.Visible = false;
                            pnlEstudiante.Visible = false;
                            break;
                    }

                    // Mostrar el nombre del usuario
                    lblUsername.Text = "Bienvenid@, " + Session["Username"].ToString();
                    lblUsernameMobile.Text = "Bienvenid@, " + Session["Username"].ToString();
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

            // Limpiar caché del navegador
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();

            // Redirigir a la página de inicio de sesión
            Response.Redirect("~/Default.aspx");
        }

        // Método para verificar si la página actual está en un submenú
        public bool IsInSubmenu(string pageNames)
        {
            if (string.IsNullOrEmpty(pageNames)) return false;

            string[] pages = pageNames.Split('|');
            foreach (string page in pages)
            {
                if (CurrentPage.Equals(page, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}