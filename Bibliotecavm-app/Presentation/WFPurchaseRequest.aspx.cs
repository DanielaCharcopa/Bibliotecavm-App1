using Logic;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
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
                TBQuantity.Text = "1";

                // Mostrar fecha en formato día/mes/año
                LblFechaMostrar.Text = DateTime.Now.ToString("dd/MM/yyyy");
                TBFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");

                // Generar ticket inicial
                TBTicket.Text = GenerateTicket();

                // Cargar material si viene de la página de materiales
                if (!string.IsNullOrEmpty(Request.QueryString["matId"]))
                {
                    CargarMaterialSeleccionado();
                }

                showPurchaseRequestsByUser(loggedInUserId);

                // Configurar mensaje inicial
                LblMsj.Text = "Modo de creación de nuevo registro. Completa los campos y haz clic en 'Comprar'.";
                LblMsj.CssClass = "message info-message";
            }
        }

        private void CargarMaterialSeleccionado()
        {
            try
            {
                int matId = Convert.ToInt32(Request.QueryString["matId"]);
                string nombre = HttpUtility.UrlDecode(Request.QueryString["nombre"]);
                decimal precio = decimal.Parse(Request.QueryString["precio"], CultureInfo.InvariantCulture);
                string formato = HttpUtility.UrlDecode(Request.QueryString["formato"]);

                // Asignar valores a los controles
                HdnMaterialId.Value = matId.ToString();
                TxtMaterialSeleccionado.Text = $"{nombre} ({formato})";
                TBUnitPrice.Text = precio.ToString("C2", new CultureInfo("es-CO"));

                // Calcular total inicial
                UpdateTotal();
            }
            catch (Exception ex)
            {
                LblMsj.Text = $"Error al cargar el material: {ex.Message}";
                LblMsj.CssClass = "message error-message";
            }
        }

        private string GenerateTicket()
        {
            Random rnd = new Random();
            return $"T-{DateTime.Now:yyyyMMdd-HHmmss}-{rnd.Next(100, 999)}";
        }

        private void showPurchaseRequestsByUser(int userId)
        {
            try
            {
                var data = objPur.showPurchaseRequestsByUser(userId);

                if (data == null || data.Tables.Count == 0)
                {
                    LblMsj.Text = "No se obtuvo información de la base de datos.";
                    LblMsj.CssClass = "message error-message";
                    return;
                }

                if (data.Tables[0].Rows.Count > 0)
                {
                    ViewState["PurchaseRequests"] = data.Tables[0];
                    GVRequests.DataSource = data.Tables[0];
                    GVRequests.DataBind();
                }
                else
                {
                    ViewState["PurchaseRequests"] = null;
                    GVRequests.DataSource = null;
                    GVRequests.DataBind();
                    LblMsj.Text = "No hay solicitudes de compra registradas.";
                    LblMsj.CssClass = "message info-message";
                }
            }
            catch (Exception ex)
            {
                LblMsj.Text = "Error inesperado: " + ex.Message;
                LblMsj.CssClass = "message error-message";
            }
        }

        private void clear()
        {
            HFPurchaId.Value = "";
            TBTicket.Text = GenerateTicket();
            TBFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TBQuantity.Text = "1";
            TxtMaterialSeleccionado.Text = "";
            HdnMaterialId.Value = "";
            TBUnitPrice.Text = "";
            TBTotal.Text = "";

            LblMsj.Text = "Modo de creación de nuevo registro. Completa los campos y haz clic en 'Comprar'.";
            LblMsj.CssClass = "message info-message";

            ScriptManager.RegisterStartupScript(this, GetType(), "ToggleButtons", "onFormCleared();", true);
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que no estamos intentando guardar un registro existente como nuevo
                if (!string.IsNullOrEmpty(HFPurchaId.Value))
                {
                    LblMsj.Text = "Error: Estás intentando guardar un registro existente como nuevo. Usa el botón 'Actualizar' en su lugar.";
                    LblMsj.CssClass = "message error-message";
                    ScriptManager.RegisterStartupScript(this, GetType(), "ToggleButtons", "toggleSaveUpdateButtons(true);", true);
                    return;
                }

                int userId = Convert.ToInt32(Session["UserID"]);
                if (string.IsNullOrEmpty(TBQuantity.Text) || string.IsNullOrEmpty(HdnMaterialId.Value))
                {
                    LblMsj.Text = "Por favor, completa todos los campos.";
                    LblMsj.CssClass = "message error-message";
                    return;
                }

                if (!int.TryParse(TBQuantity.Text, out int cantidad) || cantidad <= 0)
                {
                    LblMsj.Text = "La cantidad debe ser un número entero positivo.";
                    LblMsj.CssClass = "message error-message";
                    return;
                }

                string errorMessage;
                bool result = objPur.savePurchaseRequest(
                    TBTicket.Text.Trim(),
                    DateTime.Parse(TBFecha.Text),
                    userId,
                    cantidad,
                    int.Parse(HdnMaterialId.Value),
                    out errorMessage);

                if (result)
                {
                    LblMsj.Text = $"Solicitud guardada exitosamente. Ticket: {TBTicket.Text}";
                    LblMsj.CssClass = "message info-message";
                    showPurchaseRequestsByUser(userId);

                    // Obtener datos para WhatsApp
                    string nombreUsuario = Session["UserName"]?.ToString() ?? "Cliente";
                    string telefonoUsuario = objUser.GetUserFormattedPhone(userId);
                    string material = TxtMaterialSeleccionado.Text;
                    string precioUnitario = TBUnitPrice.Text;
                    string total = TBTotal.Text;
                    string ticket = TBTicket.Text;

                    // Validar teléfono
                    if (string.IsNullOrEmpty(telefonoUsuario))
                    {
                        LblMsj.Text += " No se pudo iniciar WhatsApp (teléfono no registrado).";
                        clear();
                        return;
                    }

                    // Crear mensaje para WhatsApp
                    string mensaje = HttpUtility.UrlEncode(
                        $"📋 *Nueva Solicitud de Compra - Biblioteca VM*\n\n" +
                        $"🧑 *Cliente:* {nombreUsuario}\n" +
                        $"📞 *Teléfono:* {telefonoUsuario}\n" +
                        $"🛒 *Material:* {material}\n" +
                        $"🔢 *Cantidad:* {cantidad}\n" +
                        $"💰 *Precio Unitario:* {precioUnitario}\n" +
                        $"💵 *Total:* {total}\n" +
                        $"🎫 *Ticket:* {ticket}\n\n" +
                        $"Por favor contactar al cliente para confirmar disponibilidad y forma de pago.");

                    // Número del vendedor (asegúrate que este número tenga WhatsApp activo)
                    string numeroVendedor = "573218921973";

                    // Abrir WhatsApp
                    string script = $"window.open('https://wa.me/{numeroVendedor}?text={mensaje}', '_blank');";
                    ScriptManager.RegisterStartupScript(this, GetType(), "OpenWhatsApp", script, true);

                    clear();
                }
                else
                {
                    LblMsj.Text = errorMessage;
                    LblMsj.CssClass = "message error-message";
                }
            }
            catch (Exception ex)
            {
                LblMsj.Text = "Error inesperado: " + ex.Message;
                LblMsj.CssClass = "message error-message";
            }
        }

        // [Los demás métodos se mantienen igual]
        protected void BtnUpdate_Click(object sender, EventArgs e) { /* ... */ }
        protected void BtnDelete_Click(object sender, EventArgs e) { /* ... */ }
        protected void TBQuantity_TextChanged(object sender, EventArgs e) { UpdateTotal(); }
        protected void GVRequests_SelectedIndexChanged(object sender, EventArgs e) { /* ... */ }
        protected void GVRequests_PageIndexChanging(object sender, GridViewPageEventArgs e) { /* ... */ }
        protected void BtnBuscarMaterial_Click(object sender, EventArgs e) { Response.Redirect("WFPresentationMaterial.aspx"); }

        private void UpdateTotal()
        {
            if (string.IsNullOrEmpty(TBQuantity.Text) || string.IsNullOrEmpty(TBUnitPrice.Text))
            {
                TBTotal.Text = "$0,00";
                return;
            }

            var cultureInfo = new System.Globalization.CultureInfo("es-CO");
            string precioUnitarioTexto = TBUnitPrice.Text.Replace("$", "").Trim();

            if (int.TryParse(TBQuantity.Text, out int cantidad) &&
                decimal.TryParse(precioUnitarioTexto, System.Globalization.NumberStyles.Currency, cultureInfo, out decimal precioUnitario))
            {
                decimal total = cantidad * precioUnitario;
                TBTotal.Text = total.ToString("C2", cultureInfo).Replace("$", "$ ");
            }
            else
            {
                TBTotal.Text = "$0,00";
            }
        }
    }
}