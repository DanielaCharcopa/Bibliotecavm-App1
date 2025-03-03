using System;
using System.Web;
using System.Web.Security; // Necesario para FormsAuthentication
using System.Web.UI;

namespace Presentation
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Destruir la sesión actual
            Session.Abandon(); // Libera todos los objetos almacenados en la sesión

            // Cerrar la autenticación de formularios
            FormsAuthentication.SignOut(); // Elimina el ticket de autenticación del navegador

            // Redirigir al usuario a la página de inicio (Default.aspx)
            HttpContext.Current.Response.Redirect("Default.aspx");
        }
    }
}