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

                // También mantener el valor en el campo oculto para el procesamiento
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
                TxtMaterialSeleccionado.Text = $"{nombre} ({formato})"; // Ej: "Matemáticas Básicas (PDF)"
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
            // Genera un ticket único con formato: T-AAAAMMDD-HHMMSS-NNN
            // Donde NNN es un número aleatorio de 3 dígitos
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
                    // Guardar los datos en ViewState para la paginación
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
            TBQuantity.Text = "1"; // Restablecer a 1 al limpiar el formulario
            TxtMaterialSeleccionado.Text = "";
            HdnMaterialId.Value = "";
            TBUnitPrice.Text = "";
            TBTotal.Text = "";

            // Configurar mensaje
            LblMsj.Text = "Modo de creación de nuevo registro. Completa los campos y haz clic en 'Comprar'.";
            LblMsj.CssClass = "message info-message";

            // Registrar script para cambiar los botones
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

                    // Forzar modo edición
                    ScriptManager.RegisterStartupScript(this, GetType(), "ToggleButtons", "toggleSaveUpdateButtons(true);", true);
                    return;
                }

                int userId = Convert.ToInt32(Session["UserID"]);
                if (string.IsNullOrEmpty(TBQuantity.Text) ||
                    string.IsNullOrEmpty(HdnMaterialId.Value))
                {
                    LblMsj.Text = "Por favor, completa todos los campos.";
                    LblMsj.CssClass = "message error-message";
                    return;
                }

                // Validar que la cantidad sea un número positivo
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

                    // Obtener información para el mensaje de WhatsApp
                    string material = TxtMaterialSeleccionado.Text;
                    string precioUnitario = TBUnitPrice.Text;
                    string total = TBTotal.Text;
                    string ticket = TBTicket.Text;

                    // Crear mensaje para WhatsApp
                    string mensaje = System.Web.HttpUtility.UrlEncode(
                        $"Nueva solicitud de compra:\n" +
                        $"Material: {material}\n" +
                        $"Cantidad: {cantidad}\n" +
                        $"Precio Unitario: {precioUnitario}\n" +
                        $"Total: {total}\n" +
                        $"Ticket: {ticket}\n\n" +
                        $"Por favor revisar esta solicitud y espere a ser contactado");

                    // Número del vendedor (código de país + número)
                    string numeroVendedor = "573218921973"; // Colombia es +57

                    // Crear enlace de WhatsApp
                    string urlWhatsApp = $"https://wa.me/{numeroVendedor}?text={mensaje}";

                    // Registrar enlace en consola para depuración
                    System.Diagnostics.Debug.WriteLine("Enlace WhatsApp: " + urlWhatsApp);

                    // Abrir WhatsApp en una nueva pestaña
                    ClientScript.RegisterStartupScript(
                        this.GetType(),
                        "OpenWhatsApp",
                        $"window.open('{urlWhatsApp}', '_blank');",
                        true);

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

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Validar sesión
                if (Session["UserID"] == null)
                {
                    Response.Redirect("Default.aspx");
                    return;
                }

                int userId = Convert.ToInt32(Session["UserID"]);

                // 2. Validar que hay una solicitud seleccionada
                if (string.IsNullOrEmpty(HFPurchaId.Value))
                {
                    LblMsj.Text = "Debe seleccionar una solicitud de la lista para actualizar.";
                    LblMsj.CssClass = "message error-message";
                    return;
                }

                // 3. Validar campos críticos
                if (string.IsNullOrEmpty(TBQuantity.Text) || string.IsNullOrEmpty(HdnMaterialId.Value))
                {
                    LblMsj.Text = "Debe especificar la cantidad y seleccionar un material.";
                    LblMsj.CssClass = "message error-message";
                    return;
                }

                // 4. Validar formato de cantidad
                if (!int.TryParse(TBQuantity.Text, out int cantidad) || cantidad <= 0)
                {
                    LblMsj.Text = "La cantidad debe ser un número entero positivo.";
                    LblMsj.CssClass = "message error-message";
                    return;
                }

                // 5. Validar ID de material
                if (!int.TryParse(HdnMaterialId.Value, out int materialId))
                {
                    LblMsj.Text = "El material seleccionado no es válido.";
                    LblMsj.CssClass = "message error-message";
                    return;
                }

                // 6. Generar NUEVO ticket para la actualización
                string nuevoTicket = GenerateTicket();

                // 7. Obtener fecha (usar fecha actual si hay error)
                DateTime fecha = DateTime.TryParse(TBFecha.Text, out DateTime f) ? f : DateTime.Now;

                // 8. Ejecutar actualización con el nuevo ticket
                bool resultado = objPur.updatePurchaseRequest(
                    int.Parse(HFPurchaId.Value),
                    nuevoTicket,
                    fecha,
                    userId,
                    cantidad,
                    materialId);

                // 9. Manejar resultado
                if (resultado)
                {
                    // Actualizar el ticket mostrado en el formulario
                    TBTicket.Text = nuevoTicket;

                    LblMsj.Text = "¡Solicitud actualizada correctamente! Nuevo ticket generado.";
                    LblMsj.CssClass = "message info-message";

                    // Refrescar la lista para mostrar el nuevo ticket
                    showPurchaseRequestsByUser(userId);
                }
                else
                {
                    LblMsj.Text = "No se pudo completar la actualización. Intente nuevamente.";
                    LblMsj.CssClass = "message error-message";
                }
            }
            catch (Exception ex)
            {
                LblMsj.Text = $"Error inesperado: {ex.Message}";
                LblMsj.CssClass = "message error-message";
            }
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int userId = Convert.ToInt32(Session["UserID"]);
                if (string.IsNullOrEmpty(HFPurchaId.Value))
                {
                    LblMsj.Text = "Selecciona una solicitud válida para eliminar.";
                    LblMsj.CssClass = "message error-message";
                    return;
                }

                bool result = objPur.deletePurchaseRequest(int.Parse(HFPurchaId.Value));
                if (result)
                {
                    LblMsj.Text = "¡Solicitud eliminada correctamente!";
                    LblMsj.CssClass = "message info-message";
                    showPurchaseRequestsByUser(userId);
                    clear();
                }
                else
                {
                    LblMsj.Text = "Error al eliminar la solicitud. Intente nuevamente.";
                    LblMsj.CssClass = "message error-message";
                }
            }
            catch (Exception ex)
            {
                LblMsj.Text = $"Error inesperado al eliminar: {ex.Message}";
                LblMsj.CssClass = "message error-message";
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
                TBTotal.Text = "$0,00";
                return;
            }

            // Crear un CultureInfo para Colombia
            var cultureInfo = new System.Globalization.CultureInfo("es-CO");

            // Limpiar el símbolo de moneda del Precio Unitario
            string precioUnitarioTexto = TBUnitPrice.Text.Replace("$", "").Trim();

            // Intentar parsear los valores
            if (int.TryParse(TBQuantity.Text, out int cantidad) &&
                decimal.TryParse(precioUnitarioTexto, System.Globalization.NumberStyles.Currency, cultureInfo, out decimal precioUnitario))
            {
                // Realizar la multiplicación
                decimal total = cantidad * precioUnitario;

                // Formatear el total con separadores de miles, dos decimales y el signo de pesos
                TBTotal.Text = total.ToString("C2", cultureInfo).Replace("$", "$ ");
            }
            else
            {
                TBTotal.Text = "$0,00";
            }
        }

        protected void GVRequests_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (GVRequests.SelectedRow != null)
                {
                    GridViewRow row = GVRequests.SelectedRow;

                    // 1. Validar estructura básica
                    if (row.Cells.Count < 8 || GVRequests.DataKeys[row.RowIndex] == null)
                    {
                        LblMsj.Text = "Estructura de datos no válida. No se pueden cargar los detalles.";
                        LblMsj.CssClass = "message error-message";
                        return;
                    }

                    // 2. Asignar valores con validaciones
                    // ID de la solicitud
                    HFPurchaId.Value = row.Cells[0].Text;

                    // Ticket
                    TBTicket.Text = row.Cells[1].Text;

                    // Fecha (formato dual)
                    if (DateTime.TryParse(row.Cells[2].Text, out DateTime fecha))
                    {
                        TBFecha.Text = fecha.ToString("yyyy-MM-dd");
                        LblFechaMostrar.Text = fecha.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        TBFecha.Text = DateTime.Now.ToString("yyyy-MM-dd");
                        LblFechaMostrar.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    }

                    // Material (nombre e ID)
                    TxtMaterialSeleccionado.Text = row.Cells[4].Text?.Trim() ?? string.Empty;

                    // ID del material desde DataKeys con validación
                    var materialId = GVRequests.DataKeys[row.RowIndex].Values["tbl_material_edu_mat_id"]?.ToString();
                    HdnMaterialId.Value = !string.IsNullOrEmpty(materialId) ? materialId : string.Empty;

                    // 3. Validación crítica del ID del material
                    if (string.IsNullOrEmpty(HdnMaterialId.Value))
                    {
                        LblMsj.Text = "No se pudo obtener el ID del material. Seleccione nuevamente.";
                        LblMsj.CssClass = "message error-message";
                        return;
                    }

                    // 4. Cantidad con valor por defecto seguro
                    if (int.TryParse(row.Cells[5].Text, out int cantidad) && cantidad > 0)
                    {
                        TBQuantity.Text = cantidad.ToString();
                    }
                    else
                    {
                        TBQuantity.Text = "1";
                    }

                    // 5. Valores monetarios con formato
                    // Precio Unitario
                    if (decimal.TryParse(row.Cells[6].Text.Replace("$", "").Trim(),
                        System.Globalization.NumberStyles.Currency,
                        System.Globalization.CultureInfo.CurrentCulture,
                        out decimal precioUnitario))
                    {
                        TBUnitPrice.Text = precioUnitario.ToString("C2");
                    }

                    // Total
                    if (decimal.TryParse(row.Cells[7].Text.Replace("$", "").Trim(),
                        System.Globalization.NumberStyles.Currency,
                        System.Globalization.CultureInfo.CurrentCulture,
                        out decimal total))
                    {
                        TBTotal.Text = total.ToString("C2");
                    }

                    // 6. Configurar mensaje y botones
                    LblMsj.Text = "Estás editando un registro existente. Usa el botón 'Actualizar' para guardar los cambios.";
                    LblMsj.CssClass = "message info-message";

                    // Registrar script para cambiar los botones
                    ScriptManager.RegisterStartupScript(this, GetType(), "ToggleButtons", "onRecordSelected();", true);
                }
            }
            catch (Exception ex)
            {
                LblMsj.Text = $"Error al cargar detalles: {ex.Message}";
                LblMsj.CssClass = "message error-message";
                System.Diagnostics.Debug.WriteLine($"Error en GVRequests_SelectedIndexChanged: {ex.ToString()}");
            }
        }

        protected void GVRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVRequests.PageIndex = e.NewPageIndex;
            int loggedInUserId = Convert.ToInt32(Session["UserID"]);
            showPurchaseRequestsByUser(loggedInUserId);
        }

        protected void BtnBuscarMaterial_Click(object sender, EventArgs e)
        {
            Response.Redirect("WFPresentationMaterial.aspx");
        }
    }
}