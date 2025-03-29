using Logic;
using System;
using System.Data;

namespace Presentation
{
    public partial class WFStatisticReport : System.Web.UI.Page
    {
        VisitsLog objVis = new VisitsLog(); // Instancia para lógica de visitas
        UserLogic objUser = new UserLogic();
        MaterialEducativoLog objMat = new MaterialEducativoLog();

        private int _usu_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Obtener el ID del usuario logueado
                _usu_id = GetLoggedUserId();

                // Mostrar el nombre del usuario logueado en un Label
                ShowLoggedInUserName();

                // Cargar estadísticas
                LoadStatistics();
            }
        }

        // Obtener el ID del usuario logueado
        private int GetLoggedUserId()
        {
            return Convert.ToInt32(Session["UserID"]); // ⚠️ Ajusta esto según tu autenticación
        }

        // Mostrar el nombre del usuario logueado en un Label
        private void ShowLoggedInUserName()
        {
            if (Session["UserName"] != null)
            {
                LblUsuario.Text = Session["UserName"].ToString(); // Asignar el nombre al Label
            }
            else
            {
                LblUsuario.Text = "Usuario no identificado"; // Mensaje por defecto si no hay nombre
            }
        }

        // Cargar estadísticas
        private void LoadStatistics()
        {
            try
            {
                // Obtener el total de visitas
                lblTotalVisits.Text = objVis.countTotalVisits().ToString();

                // Obtener visitas por docentes
                lblVisitsByTeachers.Text = objVis.countVisitsByTeacher().ToString();

                // Obtener visitas por estudiantes
                lblVisitsByStudents.Text = objVis.countVisitsByStudent().ToString();

                // Obtener estadísticas de materiales y visitas
                gvMaterialVisitStats.DataSource = objVis.GetMaterialAndVisitStats();
                gvMaterialVisitStats.DataBind();

                // Obtener materiales más visitados
                gvMostVisitedMaterials.DataSource = objVis.GetMostVisitedMaterials();
                gvMostVisitedMaterials.DataBind();

                // Obtener visitas del usuario logueado
                DataSet ds = objVis.showVisits();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvUserVisits.DataSource = ds.Tables[0];
                    gvUserVisits.DataBind();
                }
                else
                {
                    LblMsj.Text = "No hay visitas registradas para este usuario.";
                    gvUserVisits.DataSource = null;
                    gvUserVisits.DataBind();
                }
            }
            catch (Exception ex)
            {
                LblMsj.Text = "Error al cargar estadísticas: " + ex.Message;
            }
        }
    }
}
