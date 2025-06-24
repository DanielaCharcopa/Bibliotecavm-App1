using Logic;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFAnswer : System.Web.UI.Page
    {
        private AnswersLog objAnswerLog = new AnswersLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUnansweredQuestions();
            }
        }

        private void LoadUnansweredQuestions()
        {
            try
            {
                // Aquí deberías obtener el ID del usuario de la sesión
                // Por ejemplo: int userId = Convert.ToInt32(Session["UserId"]);
                // Para pruebas, usaremos un ID fijo (reemplázalo con el código real)
                int userId = GetCurrentUserId();

                DataSet dsQuestions = objAnswerLog.showUnansweredQuestionsByUser(userId);

                if (dsQuestions != null && dsQuestions.Tables.Count > 0 && dsQuestions.Tables[0].Rows.Count > 0)
                {
                    rptQuestions.DataSource = dsQuestions;
                    rptQuestions.DataBind();
                    pnlQuestions.Visible = true;
                    pnlNoQuestions.Visible = false;
                }
                else
                {
                    pnlQuestions.Visible = false;
                    pnlNoQuestions.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ShowError("Error al cargar las preguntas: " + ex.Message);
            }
        }

        protected void rptQuestions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            // Este método se puede usar para configurar controles adicionales durante el databinding
            // Por ahora está vacío, pero puede ser útil si necesitas más funcionalidad
        }

        protected void btnResponder_Click(object sender, EventArgs e)
        {
            // Obtener información del botón que se hizo clic
            Button btnClicked = (Button)sender;
            int questionId = Convert.ToInt32(btnClicked.CommandArgument);

            // Encontrar el contenedor del ítem del Repeater
            RepeaterItem item = (RepeaterItem)btnClicked.NamingContainer;

            // Encontrar los RadioButtons en el contenedor
            RadioButton rbSi = (RadioButton)item.FindControl("rbSi");
            RadioButton rbNo = (RadioButton)item.FindControl("rbNo");
            Label lblMessage = (Label)item.FindControl("lblMessage");

            // Verificar si se seleccionó una respuesta
            if (!rbSi.Checked && !rbNo.Checked)
            {
                lblMessage.Text = "Por favor seleccione una respuesta";
                lblMessage.CssClass = "text-danger";
                return;
            }

            // Determinar la respuesta seleccionada
            string respuesta = rbSi.Checked ? "Sí" : "No";

            try
            {
                // Guardar la respuesta
                int userId = GetCurrentUserId();
                bool result = objAnswerLog.saveAnswer(respuesta, questionId, userId);

                if (result)
                {
                    // Deshabilitar los controles después de responder
                    rbSi.Enabled = false;
                    rbNo.Enabled = false;
                    btnClicked.Enabled = false;
                    btnClicked.Text = "Respondido";
                    btnClicked.CssClass = "btn btn-success";
                    lblMessage.Text = "¡Respuesta guardada!";
                    lblMessage.CssClass = "text-success";

                    // También puedes mostrar un mensaje global
                    ShowSuccess("La respuesta ha sido guardada exitosamente.");
                }
                else
                {
                    lblMessage.Text = "No se pudo guardar la respuesta";
                    lblMessage.CssClass = "text-danger";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error: " + ex.Message;
                lblMessage.CssClass = "text-danger";
                ShowError("Error al guardar la respuesta: " + ex.Message);
            }
        }

        private int GetCurrentUserId()
        {

            if (Session["UserId"] != null)
            {
                return Convert.ToInt32(Session["UserId"]);
            }
            else
            {
                // Si no hay usuario en sesión, puedes redirigir al login
                // Response.Redirect("Login.aspx");
                // O usar un valor por defecto para pruebas
                return 1; // Usuario de prueba
            }
        }

        private void ShowSuccess(string message)
        {
            lblSuccess.Text = message;
            pnlSuccess.Visible = true;
            pnlError.Visible = false;
        }

        private void ShowError(string message)
        {
            lblError.Text = message;
            pnlError.Visible = true;
            pnlSuccess.Visible = false;
        }
    }
}