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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["UserID"] == null || Session["UserRole"] == null)
                {
                    Response.Redirect("Default.aspx");
                }

                int loggedInUserId = Convert.ToInt32(Session["UserID"]);
                string loggedInUserRole = Session["UserRole"].ToString();

                if (loggedInUserRole == "Administrador")
                {
                    showPurchaseRequest();
                }
                else
                {
                    showPurchaseRequestsByUser(loggedInUserId);
                }

                LoadMaterials();
            }
        }

        protected void showPurchaseRequest()
        {
            try
            {
                var data = objPur.showPurchaseRequest();
                GVRequests.DataSource = data?.Tables.Count > 0 && data.Tables[0].Rows.Count > 0 ? data.Tables[0] : null;
                GVRequests.DataBind();
                LblMsj.Text = GVRequests.DataSource == null ? "No hay solicitudes de compra registradas." : "";
            }
            catch (Exception ex)
            {
                LblMsj.Text = "Error inesperado: " + ex.Message;
            }
        }

        protected void showPurchaseRequestsByUser(int userId)
        {
            try
            {
                var data = objPur.showPurchaseRequestsByUser(userId);
                GVRequests.DataSource = data?.Tables.Count > 0 && data.Tables[0].Rows.Count > 0 ? data.Tables[0] : null;
                GVRequests.DataBind();
                LblMsj.Text = GVRequests.DataSource == null ? "No tienes solicitudes de compra registradas." : "";
            }
            catch (Exception ex)
            {
                LblMsj.Text = "Error inesperado: " + ex.Message;
            }
        }

        protected void LoadMaterials()
        {
            try
            {
                DataSet dsMaterials = objPur.showGetMaterials();
                if (dsMaterials.Tables.Count > 0 && dsMaterials.Tables[0].Rows.Count > 0)
                {
                    DropDownList ddlMaterial = (DropDownList)FindControl("DDLMaterial");
                    if (ddlMaterial != null)
                    {
                        ddlMaterial.Items.Clear();
                        foreach (DataRow row in dsMaterials.Tables[0].Rows)
                        {
                            ddlMaterial.Items.Add(new ListItem(row["mat_titulo"].ToString(), row["mat_id"].ToString()));
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                LblMsj.Text = "Error al cargar materiales: " + ex.Message;
            }
        }
    }
}
