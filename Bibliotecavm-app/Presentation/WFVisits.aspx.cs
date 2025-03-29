using Logic;
using System;
using System.Data;

namespace Presentation
{
    public partial class WFVisits : System.Web.UI.Page
    {
        VisitsLog objVis = new VisitsLog(); // Instancia para lógica de visitas
        UserLogic objUser = new UserLogic();
        MaterialEducativoLog objMat = new MaterialEducativoLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Obtener el ID del usuario logueado
                int userId = GetLoggedUserId();

                if (userId > 0) // Solo carga visitas si hay usuario logueado
                {
                    ShowLoggedInUserName();
                    LoadUserVisits();
                }
                else
                {
                    LblMsj.Text = "Error: Usuario no identificado.";
                }
            }
        }

        // Obtener el ID del usuario logueado de la sesión
        private int GetLoggedUserId()
        {
            if (Session["UserID"] != null && int.TryParse(Session["UserID"].ToString(), out int userId))
            {
                return userId;
            }
            return 0; // Retorna 0 si no hay usuario logueado
        }

        // Mostrar el nombre del usuario logueado en el Label
        private void ShowLoggedInUserName()
        {
            if (Session["UserName"] != null)
            {
                LblUsuario.Text = Session["UserName"].ToString();
            }
            else
            {
                LblUsuario.Text = "Usuario no identificado";
            }
        }

        // Cargar visitas del usuario logueado
        private void LoadUserVisits()
        {
            try
            {
                int userId = GetLoggedUserId(); // Asegurarse de obtenerlo siempre de la sesión
                if (userId == 0)
                {
                    LblMsj.Text = "Error: Usuario no identificado.";
                    return;
                }

                DataSet ds = objVis.GetVisitsByUser(userId);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GVVisitas.DataSource = ds.Tables[0];
                    GVVisitas.DataBind();
                }
                else
                {
                    LblMsj.Text = "No tienes visitas registradas.";
                    GVVisitas.DataSource = null;
                    GVVisitas.DataBind();
                }
            }
            catch (Exception ex)
            {
                LblMsj.Text = "Error al cargar visitas: " + ex.Message;
            }
        }
    }
}
