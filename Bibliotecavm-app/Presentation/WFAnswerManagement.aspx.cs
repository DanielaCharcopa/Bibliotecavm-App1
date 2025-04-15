using Logic;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFAnswerManagement : System.Web.UI.Page
    {
        private readonly AnswersLog answersLog = new AnswersLog();
        private DataTable AnswersData
        {
            get => ViewState["AnswersData"] as DataTable;
            set => ViewState["AnswersData"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null || Session["UserRole"] == null)
                {
                    Response.Redirect("Default.aspx");
                    return;
                }

                LoadAnswersData();
                ConfigureGridView();
            }
        }

        private void LoadAnswersData()
        {
            try
            {
                DataSet ds = Session["UserRole"].ToString() == "Administrador"
                    ? answersLog.showAnswers()
                    : answersLog.showAnswersByUser(Convert.ToInt32(Session["UserID"]));

                AnswersData = ds?.Tables.Count > 0 ? ds.Tables[0] : CreateEmptyDataTable();
            }
            catch (Exception ex)
            {
                lblMessage.Text = $"Error al cargar respuestas: {ex.Message}";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                AnswersData = CreateEmptyDataTable();
            }
        }

        private DataTable CreateEmptyDataTable()
        {
            var dt = new DataTable();
            dt.Columns.Add("res_id");
            dt.Columns.Add("tbl_encuesta_en_id");
            dt.Columns.Add("en_descripcion_pregunta");
            dt.Columns.Add("res_respuesta");
            dt.Columns.Add("nombre_usuario");
            return dt;
        }

        private void ConfigureGridView()
        {
            gvAnswers.DataSource = AnswersData;
            gvAnswers.DataBind();
            UpdatePaginationInfo();
        }

        protected void gvAnswers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAnswers.PageIndex = e.NewPageIndex;
            ConfigureGridView();
        }

        private void UpdatePaginationInfo()
        {
            if (AnswersData.Rows.Count == 0) return;

            int currentPage = gvAnswers.PageIndex + 1;
            int totalPages = gvAnswers.PageCount;
            int startRecord = (gvAnswers.PageIndex * gvAnswers.PageSize) + 1;
            int endRecord = Math.Min(startRecord + gvAnswers.PageSize - 1, AnswersData.Rows.Count);

            lblPaginationInfo.Text = $"Mostrando {startRecord}-{endRecord} de {AnswersData.Rows.Count} registros | Página {currentPage} de {totalPages}";
        }

        protected void gvAnswers_PreRender(object sender, EventArgs e)
        {
            if (gvAnswers.Rows.Count > 0)
            {
                gvAnswers.UseAccessibleHeader = true;
                gvAnswers.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
    }
}