using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

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