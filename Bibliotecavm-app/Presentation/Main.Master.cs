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
                if (Session["UserId"] != null)
                {
                    lblUsername.Text = "Bienvenido, " + Session["Username"].ToString();
                    lnkLogout.Visible = true;
                }
                else
                {
                    lblUsername.Text = "Bienvenido, Invitado";
                    lnkLogout.Visible = false;
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