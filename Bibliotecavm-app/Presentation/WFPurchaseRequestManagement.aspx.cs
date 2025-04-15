using Logic;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFPurchaseRequestManagement : System.Web.UI.Page
    {
        PurchaseRequestLog objPur = new PurchaseRequestLog();

        private DataTable DataSource
        {
            get => ViewState["RequestsData"] as DataTable;
            set => ViewState["RequestsData"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserID"] == null || Session["UserRole"] == null)
                {
                    Response.Redirect("Default.aspx");
                }
                LoadRequests();
            }
        }

        private void LoadRequests()
        {
            try
            {
                int loggedInUserId = Convert.ToInt32(Session["UserID"]);
                string loggedInUserRole = Session["UserRole"].ToString();

                DataSet data = (loggedInUserRole == "Administrador")
                    ? objPur.showPurchaseRequest()
                    : objPur.showPurchaseRequestsByUser(loggedInUserId);

                if (data?.Tables.Count > 0)
                {
                    DataSource = data.Tables[0];
                    BindGridView();
                    LblMsj.Text = "";
                }
                else
                {
                    DataSource = null;
                    GVRequests.DataSource = null;
                    GVRequests.DataBind();
                    LblMsj.Text = (loggedInUserRole == "Administrador")
                        ? "No hay solicitudes de compra registradas."
                        : "No tienes solicitudes de compra registradas.";
                }
            }
            catch (Exception ex)
            {
                LblMsj.Text = "Error al cargar solicitudes: " + ex.Message;
            }
        }

        private void BindGridView()
        {
            GVRequests.DataSource = DataSource;
            GVRequests.DataBind();
        }

        protected void GVRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVRequests.PageIndex = e.NewPageIndex;
            BindGridView();
        }

        protected void CbCompleted_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            GridViewRow row = (GridViewRow)cb.NamingContainer;
            int solicId = Convert.ToInt32(GVRequests.DataKeys[row.RowIndex].Value);

            if (cb.Checked)
            {
                try
                {
                    bool success = objPur.ToggleCompletada(solicId);
                    if (success)
                    {
                        // Actualizar el DataSource
                        UpdateDataSource(solicId, true);

                        // Forzar la actualización de la fila
                        UpdateRowAppearance(row, true);

                        LblMsj.Text = "Solicitud marcada como completada";
                        LblMsj.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        cb.Checked = false;
                        LblMsj.Text = "Error al marcar la solicitud";
                        LblMsj.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    cb.Checked = false;
                    LblMsj.Text = "Error: " + ex.Message;
                    LblMsj.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private void UpdateDataSource(int solicId, bool isCompleted)
        {
            if (DataSource != null)
            {
                DataRow[] rows = DataSource.Select($"solic_id = {solicId}");
                if (rows.Length > 0)
                {
                    string currentTicket = rows[0]["solic_ticket"].ToString();
                    rows[0]["solic_ticket"] = isCompleted ? "✓" + GetCleanTicket(currentTicket) : GetCleanTicket(currentTicket);
                }
            }
        }

        private void UpdateRowAppearance(GridViewRow row, bool isCompleted)
        {
            // Actualizar checkbox
            CheckBox cb = (CheckBox)row.FindControl("CbCompleted");
            if (cb != null)
            {
                cb.Checked = isCompleted;
                cb.Enabled = !isCompleted;
                cb.CssClass = isCompleted ? "checkbox-large locked-checkbox" : "checkbox-large";
            }

            // Actualizar estilo de fila
            row.CssClass = isCompleted ? "completed-row" : "";

            // Actualizar texto del ticket
            Label lblTicket = (Label)row.FindControl("LblTicket");
            if (lblTicket != null)
            {
                lblTicket.Text = isCompleted ? "✓" + GetCleanTicket(lblTicket.Text) : GetCleanTicket(lblTicket.Text);
            }
        }

        protected void GVRequests_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string ticket = DataBinder.Eval(e.Row.DataItem, "solic_ticket")?.ToString();
                bool isCompleted = IsCompleted(ticket);

                CheckBox cbCompleted = (CheckBox)e.Row.FindControl("CbCompleted");
                if (cbCompleted != null)
                {
                    cbCompleted.Checked = isCompleted;
                    cbCompleted.Enabled = !isCompleted;
                    cbCompleted.CssClass = isCompleted ? "checkbox-large locked-checkbox" : "checkbox-large";
                }

                e.Row.CssClass = isCompleted ? "completed-row" : "";
            }
        }

        public string GetCleanTicket(string ticket)
        {
            return ticket?.StartsWith("✓") == true ? ticket.Substring(1) : ticket;
        }

        public bool IsCompleted(object ticketObj)
        {
            return ticketObj?.ToString().StartsWith("✓") == true;
        }
    }
}