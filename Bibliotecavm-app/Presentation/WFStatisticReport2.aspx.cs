using Logic;
using System;
using System.Collections.Generic;
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
                ConfigureInitialGridView();
                // Cargar estadísticas
                LoadStatistics();
                LoadUserVisits();
                LoadSurveyQuestions();



            }
        }

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
            catch (Exception)
            {
                //lblSurveyMessage.Text = "Error al cargar preguntas: " + ex.Message;
                //lblSurveyMessage.CssClass = "message error-message";
            }
        }

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
                    lblTotalResponses.Text = row["Total Respuestas"].ToString();
                    lblYesPercent.Text = row["Porcentaje Sí"].ToString() + "% (" + row["Total Sí"].ToString() + ")";
                    lblNoPercent.Text = row["Porcentaje No"].ToString() + "% (" + row["Total No"].ToString() + ")";

                    pnlSurveyStats.Visible = true;

                }
                else
                {
                    pnlSurveyStats.Visible = false;
                    //lblSurveyMessage.Text = "No hay respuestas para esta pregunta.";
                    //lblSurveyMessage.CssClass = "message info-message";
                }
            }
            catch (Exception)
            {
                pnlSurveyStats.Visible = false;
                //lblSurveyMessage.Text = "Error al cargar estadísticas: " + ex.Message;
                //lblSurveyMessage.CssClass = "message error-message";
            }
        }

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
                //lblSurveyMessage.Text = "Seleccione una pregunta para ver las estadísticas";
                //lblSurveyMessage.CssClass = "message info-message";
            }
        }

        private int GetLoggedUserId()
        {
            return Convert.ToInt32(Session["UserID"]);
        }

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

                // Obtener estadísticas de materiales
                DataSet dsMateriales = objVis.ListarMaterialesEducativos();
                gvMaterialesEducativos.DataSource = dsMateriales;
                gvMaterialesEducativos.DataBind();

                // Obtener materiales más visitados
                DataSet dsMostVisited = objVis.GetMostVisitedMaterials();
                gvMostVisitedMaterials.DataSource = dsMostVisited;
                gvMostVisitedMaterials.DataBind();

                // Obtener visitas del usuario logueado
                LoadUserVisits();
            }
            catch (Exception ex)
            {
                LblMsj.Text = "Error al cargar estadísticas: " + ex.Message;
                LblMsj.CssClass = "message error-message";
                LblMsj.Visible = true;
            }
        }



        private void LoadUserVisits(string email = "", DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            try
            {
                DataSet ds;

                // 1. Verificar si hay filtros
                if (string.IsNullOrEmpty(email))
                {
                    // Cargar todas las visitas (SP procSelectVisits)
                    ds = objVis.showVisits();
                }
                else
                {
                    // Si hay filtro de email o fechas, usar SearchVisitsByDateRange (si existe)
                    ds = objVis.SearchVisitsByDateRange(email, fechaInicio, fechaFin);
                }

                // 2. Verificar si hay datos
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvUserVisits.DataSource = ds; // Usar el DataSet completo (no solo Tables[0])
                    gvUserVisits.DataBind(); // Aplicará paginación automáticamente

                    lblSearchMessage.Text = $"Mostrando {ds.Tables[0].Rows.Count} registros";
                    lblSearchMessage.CssClass = "message info-message";
                }
                else
                {
                    gvUserVisits.DataSource = null;
                    gvUserVisits.DataBind();

                    lblSearchMessage.Text = string.IsNullOrEmpty(email)
                        ? "No hay visitas registradas"
                        : "No se encontraron resultados con los filtros aplicados";
                    lblSearchMessage.CssClass = "message info-message";
                }
            }
            catch (Exception ex)
            {
                lblSearchMessage.Text = "Error al cargar visitas: " + ex.Message;
                lblSearchMessage.CssClass = "message error-message";
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtSearchEmail.Text.Trim();
                DateTime? fechaInicio = null;
                DateTime? fechaFin = null;

                if (!string.IsNullOrEmpty(txtFechaInicio.Text))
                    fechaInicio = DateTime.Parse(txtFechaInicio.Text);

                if (!string.IsNullOrEmpty(txtFechaFin.Text))
                    fechaFin = DateTime.Parse(txtFechaFin.Text);

                DataSet ds = GetFilteredDataSet(email, fechaInicio, fechaFin);

                // 1. Configurar el GridView ANTES de asignar los datos
                ConfigureGridView(ds);

                // 2. Asignar el DataSource
                gvUserVisits.DataSource = ds;

                // 3. Hacer el DataBind
                gvUserVisits.DataBind();

                // Mostrar mensaje adecuado
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    lblSearchMessage.Text = $"Se encontraron {ds.Tables[0].Rows.Count} registros";
                    lblSearchMessage.CssClass = "message info-message";
                }
                else
                {
                    lblSearchMessage.Text = "No se encontraron resultados con los filtros aplicados";
                    lblSearchMessage.CssClass = "message info-message";

                    // Limpiar el GridView si no hay resultados
                    gvUserVisits.DataSource = null;
                    gvUserVisits.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblSearchMessage.Text = "Error al buscar: " + ex.Message;
                lblSearchMessage.CssClass = "message error-message";

                // Limpiar el GridView en caso de error
                gvUserVisits.DataSource = null;
                gvUserVisits.DataBind();
            }
            finally
            {
                lblSearchMessage.Visible = true;
            }
        }

        private void ConfigureGridView(DataSet ds)
        {
            // Limpiar columnas existentes
            gvUserVisits.Columns.Clear();

            // Verificar si hay datos para determinar qué columnas están disponibles
            bool hasData = ds != null && ds.Tables.Count > 0 && ds.Tables[0].Columns.Count > 0;

            // Lista de posibles columnas en el orden deseado
            var columnDefinitions = new List<GridViewColumn>
    {
        new GridViewColumn { DataField = "usuario_nombre", HeaderText = "Usuario" },
        new GridViewColumn { DataField = "usuario_correo", HeaderText = "Correo" },
        new GridViewColumn { DataField = "material_titulo", HeaderText = "Material" },
        new GridViewColumn {
            DataField = "vis_fecha_ingreso",
            HeaderText = "Fecha Visita",
            DataFormatString = "{0:dd/MM/yyyy HH:mm}"
        },
        new GridViewColumn {
            DataField = "vis_duracion",
            HeaderText = "Duración (min)",
            DataFormatString = "{0} min"
        },
        // Columnas alternativas que pueden venir de diferentes consultas
        new GridViewColumn { DataField = "email", HeaderText = "Correo" },
        new GridViewColumn {
            DataField = "visit_date",
            HeaderText = "Fecha Visita",
            DataFormatString = "{0:dd/MM/yyyy HH:mm}"
        },
        new GridViewColumn {
            DataField = "visit_duration",
            HeaderText = "Duración (min)",
            DataFormatString = "{0} min"
        },
        new GridViewColumn { DataField = "material_name", HeaderText = "Material" }
    };

            // Agregar solo las columnas que existen en los datos
            foreach (var column in columnDefinitions)
            {
                if (!hasData || ds.Tables[0].Columns.Contains(column.DataField))
                {
                    var boundField = new BoundField
                    {
                        DataField = column.DataField,
                        HeaderText = column.HeaderText,
                        HtmlEncode = false
                    };

                    if (!string.IsNullOrEmpty(column.DataFormatString))
                    {
                        boundField.DataFormatString = column.DataFormatString;
                    }

                    gvUserVisits.Columns.Add(boundField);
                }
            }

            gvUserVisits.AutoGenerateColumns = false;
        }

        // Clase auxiliar para definir columnas
        public class GridViewColumn
        {
            public string DataField { get; set; }
            public string HeaderText { get; set; }
            public string DataFormatString { get; set; }
        }

        private void ConfigureInitialGridView()
        {
            gvUserVisits.Columns.Clear();

            // Definir columnas en el orden exacto que necesitas
            gvUserVisits.Columns.Add(new BoundField
            {
                DataField = "usuario_nombre",
                HeaderText = "Usuario"
            });

            gvUserVisits.Columns.Add(new BoundField
            {
                DataField = "usuario_correo",
                HeaderText = "Correo"
            });

            gvUserVisits.Columns.Add(new BoundField
            {
                DataField = "material_titulo",
                HeaderText = "Material"
            });

            gvUserVisits.Columns.Add(new BoundField
            {
                DataField = "vis_fecha_ingreso",
                HeaderText = "Fecha Visita",
                DataFormatString = "{0:dd/MM/yyyy HH:mm}",
                HtmlEncode = false
            });

            gvUserVisits.Columns.Add(new BoundField
            {
                DataField = "vis_duracion",
                HeaderText = "Duración (min)"
            });

            gvUserVisits.AutoGenerateColumns = false;
        }
        protected void btnClearSearch_Click(object sender, EventArgs e)
        {
            try
            {
                txtSearchEmail.Text = "";
                txtFechaInicio.Text = "";
                txtFechaFin.Text = "";

                // Configurar las columnas antes de cargar los datos
                ConfigureInitialGridView();

                DataSet ds = objVis.showVisits();
                gvUserVisits.DataSource = ds;
                gvUserVisits.DataBind();

                lblSearchMessage.Text = "Mostrando todas las visitas";
                lblSearchMessage.CssClass = "message info-message";
            }
            catch (Exception ex)
            {
                lblSearchMessage.Text = "Error al limpiar la búsqueda: " + ex.Message;
                lblSearchMessage.CssClass = "message error-message";
            }
        }
        protected void gvUserVisits_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvUserVisits.PageIndex = e.NewPageIndex;

                string email = txtSearchEmail.Text.Trim();
                DateTime? fechaInicio = null;
                DateTime? fechaFin = null;

                if (!string.IsNullOrEmpty(txtFechaInicio.Text))
                    fechaInicio = DateTime.Parse(txtFechaInicio.Text);

                if (!string.IsNullOrEmpty(txtFechaFin.Text))
                    fechaFin = DateTime.Parse(txtFechaFin.Text);

                DataSet ds = GetFilteredDataSet(email, fechaInicio, fechaFin);

                // Configurar el GridView con el orden correcto
                ConfigureGridView(ds);

                gvUserVisits.DataSource = ds;
                gvUserVisits.DataBind();
            }
            catch (Exception ex)
            {
                lblSearchMessage.Text = "Error al cambiar página: " + ex.Message;
                lblSearchMessage.CssClass = "message error-message";
            }
        }

        private DataSet GetFilteredDataSet(string email, DateTime? fechaInicio, DateTime? fechaFin)
        {
            if (!string.IsNullOrEmpty(email) && !fechaInicio.HasValue && !fechaFin.HasValue)
                return objVis.searchVisitsByEmail(email);

            if (string.IsNullOrEmpty(email) && (fechaInicio.HasValue || fechaFin.HasValue))
                return objVis.SearchVisitsByDateRange(null, fechaInicio, fechaFin);

            if (!string.IsNullOrEmpty(email) && (fechaInicio.HasValue || fechaFin.HasValue))
                return objVis.SearchVisitsByDateRange(email, fechaInicio, fechaFin);

            return objVis.showVisits();
        }
    }
}