using Logic;
using System;
using System.Data;

namespace Presentation
{
    public partial class WFAnswerManagement : System.Web.UI.Page
    {
        AnswersLog answersLog = new AnswersLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null || Session["UserRole"] == null)
                {
                    Response.Redirect("Default.aspx");
                    return;
                }

                string userRole = Session["UserRole"].ToString();

                if (userRole == "Administrador")
                {
                    LoadAllAnswers(); // Cargar todas las respuestas si el usuario es Administrador
                }
                else
                {
                    int loggedInUserId = Convert.ToInt32(Session["UserID"]);
                    LoadAnswersByUser(loggedInUserId); // Cargar solo las respuestas del usuario actual
                }
            }
        }

        private void LoadAnswersByUser(int userId)
        {
            try
            {
                DataSet ds = answersLog.showAnswersByUser(userId);

                gvAnswers.DataSource = (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    ? ds.Tables[0]
                    : null;

                gvAnswers.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error al cargar respuestas: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void LoadAllAnswers()
        {
            try
            {
                DataSet ds = answersLog.showAnswers(); // Obtener todas las respuestas

                gvAnswers.DataSource = (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    ? ds.Tables[0]
                    : null;

                gvAnswers.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error al cargar todas las respuestas: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
