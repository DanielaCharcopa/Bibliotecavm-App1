using Logic;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFStatisticReport2 : System.Web.UI.Page
    {
        // Instancias para lógicas 
        VisitsLog objVis = new VisitsLog();
        UserLogic objUser = new UserLogic();
        MaterialEducativoLog objMat = new MaterialEducativoLog();
        AnswersLog objAnswers = new AnswersLog();

        private int _usu_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Obtener el ID del usuario logueado
                _usu_id = GetLoggedUserId();

                // Mostrar el nombre del usuario logueado en un Label
                //ShowLoggedInUserName();

                // Cargar estadísticas
                LoadStatistics();
                LoadUserVisits();
                LoadSurveyQuestions();

            }
        }
        // Método para cargar el dropdown de preguntas
        private void LoadSurveyQuestions()
        {
            try
            {
                DataTable dtQuestions = objAnswers.GetAllSurveyQuestions();
                ddlSurveyQuestions.DataSource = dtQuestions;
                ddlSurveyQuestions.DataTextField = "question_text";
                ddlSurveyQuestions.DataValueField = "question_id";
                ddlSurveyQuestions.DataBind();
                ddlSurveyQuestions.Items.Insert(0, new ListItem("-- Seleccione una pregunta --", "0"));
            }
            catch (Exception ex)
            {
                lblSurveyMessage.Text = "Error al cargar preguntas: " + ex.Message;
                lblSurveyMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Método para cargar estadísticas de una pregunta seleccionada
        private void LoadSurveyStatistics(int questionId)
        {
            try
            {
                DataSet dsStats = objAnswers.GetSurveyStatistics(questionId);

                if (dsStats != null && dsStats.Tables.Count > 0 && dsStats.Tables[0].Rows.Count > 0)
                {
                    DataRow row = dsStats.Tables[0].Rows[0];

                    // Mostrar los resultados
                    lblQuestionText.Text = row["Pregunta"].ToString();
                    lblYesCount.Text = row["Total Sí"].ToString();
                    lblNoCount.Text = row["Total No"].ToString();
                    lblTotalResponses.Text = row["Total Respuestas"].ToString();
                    lblYesPercent.Text = row["Porcentaje Sí"].ToString() + "%";
                    lblNoPercent.Text = row["Porcentaje No"].ToString() + "%";

                    pnlSurveyStats.Visible = true;
                    lblSurveyMessage.Text = "";
                }
                else
                {
                    pnlSurveyStats.Visible = false;
                    lblSurveyMessage.Text = "No hay respuestas para esta pregunta.";
                    lblSurveyMessage.ForeColor = System.Drawing.Color.Blue;
                }
            }
            catch (Exception ex)
            {
                pnlSurveyStats.Visible = false;
                lblSurveyMessage.Text = "Error al cargar estadísticas: " + ex.Message;
                lblSurveyMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Evento cuando se selecciona una pregunta del dropdown
        protected void ddlSurveyQuestions_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSurveyQuestions.SelectedValue != "0")
            {
                int questionId = Convert.ToInt32(ddlSurveyQuestions.SelectedValue);
                LoadSurveyStatistics(questionId);
            }
            else
            {
                pnlSurveyStats.Visible = false;
                lblSurveyMessage.Text = "Seleccione una pregunta para ver las estadísticas";
                lblSurveyMessage.ForeColor = System.Drawing.Color.Blue;
            }
        }


        // Obtener el ID del usuario logueado
        private int GetLoggedUserId()
        {
            return Convert.ToInt32(Session["UserID"]); // ⚠️ Ajusta esto según tu autenticación
        }

        // Mostrar el nombre del usuario logueado en un Label
        //private void ShowLoggedInUserName()
        //{
        //    if (Session["UserName"] != null)
        //    {
        //        LblUsuario.Text = Session["UserName"].ToString(); // Asignar el nombre al Label
        //    }
        //    else
        //    {
        //        LblUsuario.Text = "Usuario no identificado"; // Mensaje por defecto si no hay nombre
        //    }
        //}

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
        private void LoadUserVisits(string emailFilter = "")
        {
            try
            {
                DataSet ds;

                if (string.IsNullOrEmpty(emailFilter))
                {
                    ds = objVis.showVisits(); // Cargar todas las visitas
                }
                else
                {
                    ds = objVis.searchVisitsByEmail(emailFilter); // Buscar por correo
                }

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvUserVisits.DataSource = ds.Tables[0];
                    gvUserVisits.DataBind();
                    lblSearchMessage.Text = $"Mostrando {ds.Tables[0].Rows.Count} registros";
                    lblSearchMessage.ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    gvUserVisits.DataSource = null;
                    gvUserVisits.DataBind();
                    lblSearchMessage.Text = string.IsNullOrEmpty(emailFilter)
                        ? "No hay visitas registradas"
                        : $"No se encontraron visitas para '{emailFilter}'";
                    lblSearchMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblSearchMessage.Text = "Error al cargar visitas: " + ex.Message;
                lblSearchMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtSearchEmail.Text.Trim();

                DateTime? fechaInicio = null;
                if (!string.IsNullOrEmpty(txtFechaInicio.Text))
                {
                    fechaInicio = DateTime.Parse(txtFechaInicio.Text);
                }

                DateTime? fechaFin = null;
                if (!string.IsNullOrEmpty(txtFechaFin.Text))
                {
                    fechaFin = DateTime.Parse(txtFechaFin.Text);
                }

                // Validación adicional de fechas
                if (fechaInicio.HasValue && fechaFin.HasValue && fechaInicio > fechaFin)
                {
                    throw new ArgumentException("La fecha de inicio no puede ser mayor a la fecha fin");
                }

                // Guardar filtros en ViewState
                ViewState["CurrentEmailFilter"] = email;
                ViewState["CurrentFechaInicio"] = fechaInicio;
                ViewState["CurrentFechaFin"] = fechaFin;

                DataSet ds = objVis.SearchVisitsByDateRange(email, fechaInicio, fechaFin);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvUserVisits.DataSource = ds.Tables[0];
                    gvUserVisits.DataBind();
                    lblSearchMessage.Text = $"Mostrando {ds.Tables[0].Rows.Count} registros filtrados";
                    lblSearchMessage.ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    gvUserVisits.DataSource = null;
                    gvUserVisits.DataBind();
                    lblSearchMessage.Text = "No se encontraron resultados con los filtros aplicados";
                    lblSearchMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblSearchMessage.Text = "Error al filtrar: " + ex.Message;
                lblSearchMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void gvUserVisits_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUserVisits.PageIndex = e.NewPageIndex;
            // Recuperar filtros del ViewState
            string email = ViewState["CurrentEmailFilter"]?.ToString() ?? "";
            DateTime? fechaInicio = ViewState["CurrentFechaInicio"] as DateTime?;
            DateTime? fechaFin = ViewState["CurrentFechaFin"] as DateTime?;

            DataSet ds = objVis.SearchVisitsByDateRange(email, fechaInicio, fechaFin);
            gvUserVisits.DataSource = ds;
            gvUserVisits.DataBind();
        }

        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            // Limpiar controles
            txtSearchEmail.Text = "";
            txtFechaInicio.Text = "";
            txtFechaFin.Text = "";

            // Limpiar ViewState
            ViewState["CurrentEmailFilter"] = "";
            ViewState["CurrentFechaInicio"] = null;
            ViewState["CurrentFechaFin"] = null;

            // Recargar datos sin filtros
            LoadUserVisits();
            lblSearchMessage.Text = "Mostrando todas las visitas";
            lblSearchMessage.ForeColor = System.Drawing.Color.Blue;
        }
        // Nuevo método para cargar estadísticas de respuestas a encuestas


    }
}

