using System;
using System.Data;
using Logic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;

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
                    Response.Redirect("Login.aspx");
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

                if (dsUnansweredQuestions != null && dsUnansweredQuestions.Tables.Count > 0 && dsUnansweredQuestions.Tables[0].Rows.Count > 0)
                {
                    ddlSurvey.DataSource = dsUnansweredQuestions.Tables[0];
                    ddlSurvey.DataTextField = "en_descripcion_pregunta";
                    ddlSurvey.DataValueField = "en_id";
                    ddlSurvey.DataBind();

                    ddlSurvey.Items.Insert(0, new ListItem("-- Seleccione una Pregunta --", "0"));
                }
                else
                {
                    ddlSurvey.Items.Clear();
                    ddlSurvey.Items.Insert(0, new ListItem("No hay preguntas disponibles", "0"));
                }
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
                }
                else
                {
                    gvAnswers.DataSource = null;
                    gvAnswers.DataBind();
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
            if (ddlSurvey.SelectedValue == "0" || string.IsNullOrWhiteSpace(txtResponse.Text))
            {
                lblMessage.Text = "Seleccione una pregunta y escriba una respuesta.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                string response = txtResponse.Text.Trim();
                int questionId = Convert.ToInt32(ddlSurvey.SelectedValue);
                int userId = Convert.ToInt32(Session["UserID"]);

                bool result = answersLog.saveAnswer(response, questionId, userId);
                lblMessage.Text = result ? "Respuesta guardada exitosamente." : "Error al guardar la respuesta.";
                lblMessage.ForeColor = result ? System.Drawing.Color.Green : System.Drawing.Color.Red;
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
            if (ViewState["SelectedResId"] == null || string.IsNullOrWhiteSpace(txtResponse.Text))
            {
                lblMessage.Text = "Seleccione una respuesta para actualizar.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                int answerId = (int)ViewState["SelectedResId"];
                string response = txtResponse.Text.Trim();
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
            if (ViewState["SelectedResId"] == null || ddlSurvey.SelectedValue == "0")
            {
                lblMessage.Text = "Seleccione una respuesta válida para eliminar.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                int answerId = (int)ViewState["SelectedResId"];
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
                    if (row.Cells.Count >= 4)
                    {
                        string questionId = row.Cells[1].Text.Trim();
                        string response = HttpUtility.HtmlDecode(row.Cells[3].Text.Trim());

                        ddlSurvey.SelectedValue = questionId;
                        txtResponse.Text = response;
                        ViewState["SelectedResId"] = int.Parse(row.Cells[0].Text.Trim());

                        lblMessage.Text = "Respuesta seleccionada para edición.";
                        lblMessage.ForeColor = System.Drawing.Color.Blue;
                    }
                    else
                    {
                        lblMessage.Text = "Error al seleccionar la respuesta.";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
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
