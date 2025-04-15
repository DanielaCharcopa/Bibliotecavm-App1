
using Logic;
using System;
using System.Web;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFSurvey : System.Web.UI.Page
    {
        SurveyLogic surveyLogic = new SurveyLogic();

        string descripcionPregunta;
        int surveyId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showSurveys();   // Cargar encuestas en el GridView
            }
        }

        private void showSurveys(int pageIndex = 0)
        {
            try
            {
                // Obtener todas las encuestas (considera implementar paginación en la capa lógica si hay muchos registros)
                var encuestas = surveyLogic.showSurveys();

                // Configurar el GridView
                gvSurveys.DataSource = encuestas;
                gvSurveys.PageIndex = pageIndex;
                gvSurveys.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error al cargar las encuestas: {ex.Message}";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnGuardarEncuesta_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que el campo no esté vacío
                if (string.IsNullOrWhiteSpace(txtDescripcionPregunta.Text))
                {
                    lblMessage.Text = "Por favor, ingrese una descripción para la pregunta.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return; // Salir del método si la validación falla
                }

                descripcionPregunta = txtDescripcionPregunta.Text;
                int usuId = Convert.ToInt32(Session["UserId"]); // Obtener el ID del usuario autenticado

                // Guardar la encuesta
                if (surveyLogic.saveSurvey(descripcionPregunta, usuId))
                {
                    lblMessage.Text = "Encuesta guardada exitosamente.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    clearForm();
                    showSurveys();  // Recargar las encuestas
                }
                else
                {
                    lblMessage.Text = "Error al guardar la encuesta.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnActualizarEncuesta_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que el campo no esté vacío
                if (string.IsNullOrWhiteSpace(txtDescripcionPregunta.Text))
                {
                    lblMessage.Text = "Por favor, ingrese una descripción para la pregunta.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return; // Salir del método si la validación falla
                }

                surveyId = int.Parse(TBCode.Value); // Obtener el ID de la encuesta seleccionada
                descripcionPregunta = txtDescripcionPregunta.Text;
                int usuId = Convert.ToInt32(Session["UserId"]); // Obtener el ID del usuario autenticado

                // Actualizar la encuesta
                if (surveyLogic.updateSurvey(surveyId, descripcionPregunta, usuId))
                {
                    lblMessage.Text = "Encuesta actualizada exitosamente.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    clearForm();
                    showSurveys();  // Recargar las encuestas
                }
                else
                {
                    lblMessage.Text = "Error al actualizar la encuesta.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnEliminarEncuesta_Click(object sender, EventArgs e)
        {
            try
            {
                surveyId = int.Parse(TBCode.Value); // Obtener el ID de la encuesta seleccionada

                // Eliminar la encuesta
                if (surveyId > 0 && surveyLogic.deleteSurvey(surveyId))
                {
                    lblMessage.Text = "Encuesta eliminada exitosamente.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                    clearForm();
                    showSurveys();  // Recargar las encuestas
                }
                else
                {
                    lblMessage.Text = "Error al eliminar la encuesta.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void clearForm()
        {
            TBCode.Value = string.Empty; // Limpiar el ID de la encuesta
            txtDescripcionPregunta.Text = string.Empty; // Limpiar la descripción
        }

        protected void gvSurveys_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow selectedRow = gvSurveys.SelectedRow;

                // Asignar los valores de la fila seleccionada al formulario
                TBCode.Value = selectedRow.Cells[0].Text; // ID de la encuesta
                txtDescripcionPregunta.Text = HttpUtility.HtmlDecode(selectedRow.Cells[1].Text); // Descripción
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error: {ex.Message}";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        // Nuevo método para manejar el cambio de página
        protected void gvSurveys_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSurveys.PageIndex = e.NewPageIndex;
            showSurveys(e.NewPageIndex);
        }
    }
}