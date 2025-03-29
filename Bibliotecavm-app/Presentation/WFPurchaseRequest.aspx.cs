using Logic;
using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFPurchaseRequest : System.Web.UI.Page
    {
        PurchaseRequestLog objPur = new PurchaseRequestLog();
        UserLogic objUser = new UserLogic();
        MaterialEducativoLog objMat = new MaterialEducativoLog();

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

                TBTicket.Text = GenerateTicket();

                showPurchaseRequestsByUser(loggedInUserId);
                showLoggedInUser(loggedInUserName);

            }
        }

        private string GenerateTicket()
        {
            return "T-" + DateTime.Now.ToString("yyyyMMddHHmmss");
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

                if (data == null || data.Tables.Count == 0)
                {
                    LblMsj.Text = "No se obtuvo información de la base de datos.";
                    return;
                }

                if (data.Tables[0].Rows.Count > 0)
                {
                    GVRequests.DataSource = data.Tables[0];
                    GVRequests.DataBind();
                    LblMsj.Text = "";
                }
                else
                {
                    GVRequests.DataSource = null;
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
            TBTicket.Text = GenerateTicket();
            TBFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TBQuantity.Text = "";
            TxtMaterialSeleccionado.Text = "";
            HdnMaterialId.Value = "";
            TBUnitPrice.Text = "";
            TBTotal.Text = "";
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int userId = Convert.ToInt32(Session["UserID"]);
                if (string.IsNullOrEmpty(TBTicket.Text) ||
                    string.IsNullOrEmpty(TBQuantity.Text) ||
                    string.IsNullOrEmpty(HdnMaterialId.Value))
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
                    int.Parse(HdnMaterialId.Value),
                    out errorMessage);

                if (result)
                {
                    LblMsj.Text = "Solicitud guardada exitosamente.";
                    showPurchaseRequestsByUser(userId);
                    clear();
                }
                else
                {
                    LblMsj.Text = errorMessage;
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
                int.Parse(HdnMaterialId.Value));

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


        protected void TxtBuscarMaterial_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string terminoBusqueda = TxtBuscarMaterial.Text.Trim().ToLower();

                DataSet ds = objPur.showGetMaterials();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable dtMateriales = ds.Tables[0];

                    if (!dtMateriales.Columns.Contains("mat_titulo"))
                    {
                        LblMsj.Text = "Error: La columna 'mat_titulo' no existe en la base de datos.";
                        return;
                    }

                    if (!string.IsNullOrEmpty(terminoBusqueda))
                    {
                        var resultados = dtMateriales.AsEnumerable()
                            .Where(row => row.Field<string>("mat_titulo").ToLower().Contains(terminoBusqueda));

                        if (resultados.Any())
                        {
                            GVMateriales.DataSource = resultados.CopyToDataTable();
                            LblMsj.Text = ""; // Ocultar el mensaje si hay resultados
                        }
                        else
                        {
                            GVMateriales.DataSource = null;
                            LblMsj.Text = "No se encontraron materiales educativos."; // Mostrar mensaje si no hay resultados
                        }
                    }
                    else
                    {
                        GVMateriales.DataSource = dtMateriales;
                        LblMsj.Text = ""; // Ocultar el mensaje si no hay término de búsqueda
                    }

                    GVMateriales.DataBind();
                }
                else
                {
                    GVMateriales.DataSource = null;
                    GVMateriales.DataBind();
                    LblMsj.Text = "No hay materiales educativos en la base de datos."; // Mostrar mensaje si no hay datos
                }

                // Recargar GVRequests después de la búsqueda
                int loggedInUserId = Convert.ToInt32(Session["UserID"]);
                showPurchaseRequestsByUser(loggedInUserId);
            }
            catch (Exception ex)
            {
                LblMsj.Text = "Error al buscar materiales educativos: " + ex.Message;
            }
        }


        protected void TBQuantity_TextChanged(object sender, EventArgs e)
        {
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            // Verificar que los campos no estén vacíos
            if (string.IsNullOrEmpty(TBQuantity.Text) || string.IsNullOrEmpty(TBUnitPrice.Text))
            {
                TBTotal.Text = "$0,00"; // Formato inicial con signo de pesos y coma decimal
                return;
            }

            // Crear un CultureInfo para Colombia
            var cultureInfo = new System.Globalization.CultureInfo("es-CO");

            // Limpiar el símbolo de moneda del Precio Unitario
            string precioUnitarioTexto = TBUnitPrice.Text.Replace("$", "").Trim();

            // Intentar parsear los valores
            if (int.TryParse(TBQuantity.Text, out int cantidad) && decimal.TryParse(precioUnitarioTexto, System.Globalization.NumberStyles.Currency, cultureInfo, out decimal precioUnitario))
            {
                // Realizar la multiplicación
                decimal total = cantidad * precioUnitario;

                // Formatear el total con separadores de miles, dos decimales y el signo de pesos
                TBTotal.Text = total.ToString("C2", cultureInfo).Replace("$", "$ "); // Agregar espacio después del signo de pesos
            }
            else
            {
                TBTotal.Text = "$0,00"; // Mostrar $0,00 si no se pueden parsear los valores
            }
        }
        protected void GVRequests_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtiene la fila seleccionada
            GridViewRow row = GVRequests.SelectedRow;
            if (row != null)
            {
                // Asignar valores a los controles
                HFPurchaId.Value = row.Cells[0].Text; // ID
                TBTicket.Text = row.Cells[1].Text; // Ticket
                TBFecha.Text = Convert.ToDateTime(row.Cells[2].Text).ToString("yyyy-MM-dd"); // Fecha
                TBQuantity.Text = row.Cells[4].Text; // Cantidad
                TxtMaterialSeleccionado.Text = row.Cells[5].Text; // Material
                TBTotal.Text = row.Cells[6].Text; // Total
            }
        }

        protected void GVMateriales_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Obtiene la fila seleccionada
            GridViewRow row = GVMateriales.SelectedRow;
            if (row != null)
            {
                // Asigna valores a los controles
                HdnMaterialId.Value = row.Cells[0].Text; // ID del material
                TxtMaterialSeleccionado.Text = row.Cells[1].Text; // Nombre del material
                TBUnitPrice.Text = row.Cells[2].Text; // Precio unitario

                // Actualizar el total después de asignar el precio unitario
                UpdateTotal();
            }
        }

    }
}
