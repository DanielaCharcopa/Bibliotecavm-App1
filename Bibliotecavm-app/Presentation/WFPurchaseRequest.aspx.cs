using Logic;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

namespace Presentation
{
    public partial class WFPurchaseRequest : System.Web.UI.Page
    {
        PurchaseRequestLog objPur = new PurchaseRequestLog();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                if (Session["UserID"] == null)
                {
                    Response.Redirect("Default.aspx");
                }

                int loggedInUserId = Convert.ToInt32(Session["UserID"]);
                string loggedInUserName = Session["UserName"]?.ToString();

                TBFecha.Attributes["min"] = DateTime.Now.ToString("yyyy-MM-dd");
                TBFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");

                showPurchaseRequestsByUser(loggedInUserId);
                showLoggedInUser(loggedInUserName);
                LoadMaterials();

            }
        }

        private void LoadMaterials()
        {
            DataSet dsMaterials = objPur.showMaterialEducativos();

            if (dsMaterials.Tables.Count > 0 && dsMaterials.Tables[0].Rows.Count > 0)
            {
                DDLMaterial.Items.Clear();
                DDLMaterial.Items.Add(new ListItem("-- Seleccione un material --", "0"));

                foreach (DataRow row in dsMaterials.Tables[0].Rows)
                {
                    string material = row["mat_titulo"].ToString();
                    string materialId = row["mat_id"].ToString();
                    string price = row["mat_precio"].ToString();

                    ListItem item = new ListItem(material, materialId);
                    item.Attributes.Add("data-price", price);
                    DDLMaterial.Items.Add(item);
                }
            }
        }

        private void showLoggedInUser(string userName)
        {
            LBLUser.Text = userName;
        }

        private void showPurchaseRequestsByUser(int userId)
        {
            try
            {
                var data = objPur.showPurchaseRequestsByUser(userId);

                if (data == null)
                {
                    LblMsj.Text = "Error: No se obtuvo información de la base de datos.";
                    return;
                }

                // Verificar si el DataSet contiene datos
                if (data.Tables.Count > 0 && data.Tables[0].Rows.Count > 0)
                {
                    GVRequests.DataSource = data.Tables[0]; // Asignar solo la tabla con datos
                    GVRequests.DataBind();
                    LblMsj.Text = ""; // Limpiar el mensaje de error
                }
                else
                {
                    GVRequests.DataSource = null; // Limpiar el DataSource del GridView
                    GVRequests.DataBind();
                    LblMsj.Text = "No hay solicitudes de compra registradas.";
                }
            }
            catch (Exception ex)
            {
                LblMsj.Text = "Error inesperado: " + ex.Message;
            }
        }


        private void clear()
        {
            HFPurchaId.Value = "";
            TBTicket.Text = "";
            TBFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TBQuantity.Text = "";
            DDLMaterial.SelectedIndex = 0;
            TBUnitPrice.Text = "";
            TBTotal.Text = "";
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int userId = Convert.ToInt32(Session["UserID"]);
                if (string.IsNullOrEmpty(TBTicket.Text) || string.IsNullOrEmpty(TBQuantity.Text) || DDLMaterial.SelectedValue == "0")
                {
                    LblMsj.Text = "Por favor, completa todos los campos.";
                    return;
                }

                string errorMessage;
                bool result = objPur.savePurchaseRequest(
                    TBTicket.Text.Trim(),
                    DateTime.Parse(TBFecha.Text),
                    userId,
                    int.Parse(TBQuantity.Text),
                    int.Parse(DDLMaterial.SelectedValue),
                    out errorMessage);

                if (result)
                {
                    LblMsj.Text = "Solicitud guardada exitosamente.";
                    showPurchaseRequestsByUser(userId); // Actualizar el GridView
                    clear();
                }
                else
                {
                    LblMsj.Text = errorMessage; // Mostrar mensaje de error
                }
            }
            catch (Exception ex)
            {
                LblMsj.Text = "Error inesperado: " + ex.Message;
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserID"]);
            if (string.IsNullOrEmpty(HFPurchaId.Value))
            {
                LblMsj.Text = "Selecciona una solicitud para actualizar.";
                return;
            }

            bool result = objPur.updatePurchaseRequest(
                int.Parse(HFPurchaId.Value),
                TBTicket.Text.Trim(),
                DateTime.Parse(TBFecha.Text),
                userId,
                int.Parse(TBQuantity.Text),
                int.Parse(DDLMaterial.SelectedValue));

            LblMsj.Text = result ? "¡Solicitud actualizada!" : "Error al actualizar.";
            if (result) showPurchaseRequestsByUser(userId);
            clear();
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            int userId = Convert.ToInt32(Session["UserID"]);
            if (string.IsNullOrEmpty(HFPurchaId.Value))
            {
                LblMsj.Text = "Selecciona una solicitud válida para eliminar.";
                return;
            }

            bool result = objPur.deletePurchaseRequest(int.Parse(HFPurchaId.Value));
            LblMsj.Text = result ? "¡Solicitud eliminada!" : "Error al eliminar.";
            if (result) showPurchaseRequestsByUser(userId);
            clear();
        }

        protected void GVRequests_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GVRequests.SelectedRow != null)
            {
                HFPurchaId.Value = GVRequests.SelectedRow.Cells[0].Text;
                TBTicket.Text = GVRequests.SelectedRow.Cells[1].Text.Trim();
                TBFecha.Text = DateTime.Parse(GVRequests.SelectedRow.Cells[2].Text).ToString("yyyy-MM-dd");
                TBQuantity.Text = GVRequests.SelectedRow.Cells[4].Text.Trim();

                string materialName = GVRequests.SelectedRow.Cells[5].Text.Trim();
                ListItem selectedMaterial = DDLMaterial.Items.FindByText(materialName);
                if (selectedMaterial != null)
                {
                    DDLMaterial.SelectedValue = selectedMaterial.Value;
                }

                decimal precioMaterial;
                if (decimal.TryParse(GVRequests.SelectedRow.Cells[6].Text, out precioMaterial))
                {
                    TBUnitPrice.Text = precioMaterial.ToString("F2");

                    int cantidad;
                    if (int.TryParse(TBQuantity.Text, out cantidad))
                    {
                        TBTotal.Text = (cantidad * precioMaterial).ToString("F2");
                    }
                }

                LblMsj.Text = "";
            }
        }

        protected void DDLMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            int materialId;
            if (int.TryParse(DDLMaterial.SelectedValue, out materialId) && materialId > 0)
            {
                DataSet ds = objPur.showMaterialEducativos();

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow[] rows = ds.Tables[0].Select($"mat_id = {materialId}");

                    if (rows.Length > 0)
                    {
                        TBUnitPrice.Text = rows[0]["mat_precio"].ToString();
                    }
                }
            }
            else
            {
                TBUnitPrice.Text = "0.00";
            }

            UpdateTotal();
        }

        protected void TBQuantity_TextChanged(object sender, EventArgs e)
        {
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            if (int.TryParse(TBQuantity.Text, out int cantidad) && decimal.TryParse(TBUnitPrice.Text, out decimal precio))
            {
                TBTotal.Text = (cantidad * precio).ToString("F2");
            }
            else
            {
                TBTotal.Text = "0.00";
            }
        }
    }
}
