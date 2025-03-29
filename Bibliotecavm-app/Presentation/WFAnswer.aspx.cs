using Logic;
using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFAnswer : System.Web.UI.Page
    {
        AnswersLog answersLog = new AnswersLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("Default.aspx");
                    return;
                }

                int loggedInUserId = Convert.ToInt32(Session["UserID"]);
                ShowLoggedInUserName();
                LoadUnansweredQuestions(loggedInUserId);
                LoadAnswers(loggedInUserId);
            }
        }

        private void ShowLoggedInUserName()
        {
            LblUsuario.Text = Session["UserName"]?.ToString() ?? "Usuario no identificado";
        }

        private void LoadUnansweredQuestions(int userId)
        {
            try
            {
                DataSet dsUnansweredQuestions = answersLog.showUnansweredQuestionsByUser(userId);
                ddlSurvey.DataSource = dsUnansweredQuestions.Tables[0];
                ddlSurvey.DataTextField = "en_descripcion_pregunta";
                ddlSurvey.DataValueField = "en_id";
                ddlSurvey.DataBind();
                ddlSurvey.Items.Insert(0, new ListItem("-- Seleccione una Pregunta --", "0"));
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error al cargar preguntas: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void LoadAnswers(int userId)
        {
            try
            {
                DataSet ds = answersLog.showAnswersByUser(userId);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    gvAnswers.DataSource = ds.Tables[0];
                    gvAnswers.DataBind();
                    lblMessage.Text = "Respuestas cargadas correctamente.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    gvAnswers.DataSource = null;
                    gvAnswers.DataBind();
                    lblMessage.Text = "No hay respuestas registradas.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error al cargar respuestas: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
        protected void btnSaveAnswer_Click(object sender, EventArgs e)
        {
            if (ddlSurvey.SelectedValue == "0")
            {
                lblMessage.Text = "Debe seleccionar una pregunta válida.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (string.IsNullOrWhiteSpace(ddlResponse.SelectedValue) || ddlResponse.SelectedValue == "0")
            {
                lblMessage.Text = "Debe seleccionar una respuesta.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                string response = ddlResponse.SelectedValue;
                int questionId = Convert.ToInt32(ddlSurvey.SelectedValue);
                int userId = Convert.ToInt32(Session["UserID"]);

                Console.WriteLine($"Intentando guardar: Respuesta={response}, PreguntaID={questionId}, UsuarioID={userId}");

                bool result = answersLog.saveAnswer(response, questionId, userId);

                if (result)
                {
                    lblMessage.Text = "Respuesta guardada exitosamente.";
                    lblMessage.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblMessage.Text = "Error al guardar la respuesta.";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }

                LoadUnansweredQuestions(userId);
                LoadAnswers(userId);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error al guardar la respuesta: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }


        protected void btnUpdateAnswer_Click(object sender, EventArgs e)
        {
            if (ViewState["SelectedResId"] == null || !int.TryParse(ViewState["SelectedResId"].ToString(), out int answerId) ||
                string.IsNullOrWhiteSpace(ddlResponse.SelectedValue))
            {
                lblMessage.Text = "Seleccione una respuesta válida para actualizar.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                string response = ddlResponse.SelectedValue;
                int questionId = Convert.ToInt32(ddlSurvey.SelectedValue);
                int userId = Convert.ToInt32(Session["UserID"]);

                bool result = answersLog.updateAnswer(answerId, response, questionId, userId);
                lblMessage.Text = result ? "Respuesta actualizada exitosamente." : "Error al actualizar la respuesta.";
                lblMessage.ForeColor = result ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                LoadUnansweredQuestions(userId);
                LoadAnswers(userId);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error al actualizar la respuesta: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnDeleteAnswer_Click(object sender, EventArgs e)
        {
            if (ViewState["SelectedResId"] == null || !int.TryParse(ViewState["SelectedResId"].ToString(), out int answerId))
            {
                lblMessage.Text = "Seleccione una respuesta válida para eliminar.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                int questionId = Convert.ToInt32(ddlSurvey.SelectedValue);
                int userId = Convert.ToInt32(Session["UserID"]);

                bool result = answersLog.deleteAnswer(answerId, questionId, userId);
                lblMessage.Text = result ? "Respuesta eliminada exitosamente." : "Error al eliminar la respuesta.";
                lblMessage.ForeColor = result ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                LoadUnansweredQuestions(userId);
                LoadAnswers(userId);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error al eliminar la respuesta: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void gvAnswers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvAnswers.SelectedRow != null)
            {
                GridViewRow row = gvAnswers.SelectedRow;
                try
                {
                    string questionId = HttpUtility.HtmlDecode(row.Cells[1].Text.Trim());
                    string response = HttpUtility.HtmlDecode(row.Cells[3].Text.Trim());

                    ddlSurvey.SelectedValue = questionId;
                    ddlResponse.SelectedValue = response;
                    ViewState["SelectedResId"] = int.Parse(row.Cells[0].Text.Trim());

                    lblMessage.Text = "Respuesta seleccionada para edición.";
                    lblMessage.ForeColor = System.Drawing.Color.Blue;
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error al seleccionar la respuesta: " + ex.Message;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
}
