using Logic;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFAnswer : System.Web.UI.Page
    {
        private readonly AnswersLog answersLog = new AnswersLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidateUserSession();
                InitializeControls();
                LoadUserData();
            }
        }

        private void ValidateUserSession()
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("~/Default.aspx", true);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        private void InitializeControls()
        {
            successAlert.Visible = false;
            errorAlert.Visible = false;
            btnSaveAnswer.Enabled = true;
        }

        private void LoadUserData()
        {
            int userId = Convert.ToInt32(Session["UserID"]);
            LoadUnansweredQuestions(userId);
        }

        private void LoadUnansweredQuestions(int userId)
        {
            try
            {
                DataSet ds = answersLog.showUnansweredQuestionsByUser(userId);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ddlSurvey.DataSource = ds.Tables[0];
                    ddlSurvey.DataTextField = "en_descripcion_pregunta";
                    ddlSurvey.DataValueField = "en_id";
                    ddlSurvey.DataBind();
                    ddlSurvey.Items.Insert(0, new ListItem("-- Seleccione una Pregunta --", "0"));

                    ToggleFormControls(true);
                }
                else
                {
                    ddlSurvey.Items.Clear();
                    ddlSurvey.Items.Add(new ListItem("-- No hay preguntas disponibles --", "0"));
                    ToggleFormControls(false);
                    ShowInfoMessage("No hay preguntas disponibles para responder o ya has contestado todas.");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error al cargar preguntas: {ex.Message}");
                ddlSurvey.Items.Add(new ListItem("-- Error al cargar --", "0"));
                ToggleFormControls(false);
            }
        }

        private void ToggleFormControls(bool enabled)
        {
            ddlSurvey.Enabled = enabled;
            ddlResponse.Enabled = enabled;
            btnSaveAnswer.Enabled = enabled;
        }

        protected void btnSaveAnswer_Click(object sender, EventArgs e)
        {
            if (!ValidateForm())
                return;

            try
            {
                SaveAnswer();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Error inesperado: {ex.Message}");
                ResetSaveButton();
            }
        }

        private bool ValidateForm()
        {
            if (ddlSurvey.SelectedValue == "0")
            {
                ShowErrorMessage("Por favor, seleccione una pregunta válida");
                ScriptManager.RegisterStartupScript(this, GetType(), "focusQuestion",
                    $"document.getElementById('{ddlSurvey.ClientID}').focus();", true);
                return false;
            }

            if (string.IsNullOrWhiteSpace(ddlResponse.SelectedValue))
            {
                ShowErrorMessage("Debe seleccionar una respuesta");
                ScriptManager.RegisterStartupScript(this, GetType(), "focusResponse",
                    $"document.getElementById('{ddlResponse.ClientID}').focus();", true);
                return false;
            }

            return true;
        }

        private void SaveAnswer()
        {
            string response = ddlResponse.SelectedValue;
            int questionId = Convert.ToInt32(ddlSurvey.SelectedValue);
            int userId = Convert.ToInt32(Session["UserID"]);

            bool result = answersLog.saveAnswer(response, questionId, userId);

            if (result)
            {
                ShowSuccessMessage("¡Respuesta guardada exitosamente!");
                LoadUserData();
                ResetForm();
            }
            else
            {
                ShowErrorMessage("No se pudo guardar la respuesta. Intente nuevamente.");
            }

            ResetSaveButton();
        }

        private void ResetForm()
        {
            ddlResponse.SelectedIndex = 0;
            ScriptManager.RegisterStartupScript(this, GetType(), "resetAnimation",
                $"$('#{ddlResponse.ClientID}').addClass('animate__animated animate__pulse');", true);
        }

        private void ResetSaveButton()
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "resetButton",
                $"document.getElementById('{btnSaveAnswer.ClientID}').innerHTML = '<i class=\"fas fa-save me-1\"></i> Guardar Respuesta';", true);
        }

        #region Message Handlers
        private void ShowSuccessMessage(string message)
        {
            litSuccessMessage.Text = message;
            successAlert.Visible = true;
            errorAlert.Visible = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "hideSuccess",
                $"setTimeout(function() {{ $('#{successAlert.ClientID}').fadeOut('slow'); }}, 5000);", true);
        }

        private void ShowErrorMessage(string message)
        {
            litErrorMessage.Text = message;
            errorAlert.Visible = true;
            successAlert.Visible = false;
        }

        private void ShowInfoMessage(string message)
        {
            litSuccessMessage.Text = message;
            successAlert.Attributes["class"] = successAlert.Attributes["class"].Replace("alert-success", "alert-info");
            successAlert.Visible = true;
            errorAlert.Visible = false;
        }
        #endregion
    }
}