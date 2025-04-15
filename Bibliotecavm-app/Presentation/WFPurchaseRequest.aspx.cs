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

                // Cargar material si viene de la página de materiales
                if (!string.IsNullOrEmpty(Request.QueryString["matId"]))
                {
                    CargarMaterialSeleccionado();
                }

                showPurchaseRequestsByUser(loggedInUserId);
                //showLoggedInUser(loggedInUserName);

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
                LblMsj.ForeColor = System.Drawing.Color.Red;
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
                    return;
                }

                if (data.Tables[0].Rows.Count > 0)
                {
                    // Guardar los datos en ViewState para la paginación
                    ViewState["PurchaseRequests"] = data.Tables[0];

                    GVRequests.DataSource = data.Tables[0];
                    GVRequests.DataBind();
                    LblMsj.Text = "";
                }
                else
                {
                    ViewState["PurchaseRequests"] = null;
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
            TBQuantity.Text = "1"; // Restablecer a 1 al limpiar el formulario
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
                if (string.IsNullOrEmpty(TBQuantity.Text) ||
                    string.IsNullOrEmpty(HdnMaterialId.Value))
                {
                    LblMsj.Text = "Por favor, completa todos los campos.";
                    return;
                }

                // Generar el ticket justo antes de guardar
                TBTicket.Text = GenerateTicket();

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
                    LblMsj.Text = $"Solicitud guardada exitosamente. Ticket: {TBTicket.Text}";
                    showPurchaseRequestsByUser(userId);

                    // Obtener información para el mensaje de WhatsApp
                    string material = TxtMaterialSeleccionado.Text;
                    string cantidad = TBQuantity.Text;
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
                }
            }
            catch (Exception ex)
            {
                LblMsj.Text = "Error inesperado: " + ex.Message;
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
                    LblMsj.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // 3. Validar campos críticos
                if (string.IsNullOrEmpty(TBQuantity.Text) || string.IsNullOrEmpty(HdnMaterialId.Value))
                {
                    LblMsj.Text = "Debe especificar la cantidad y seleccionar un material.";
                    LblMsj.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // 4. Validar formato de cantidad
                if (!int.TryParse(TBQuantity.Text, out int cantidad) || cantidad <= 0)
                {
                    LblMsj.Text = "La cantidad debe ser un número entero positivo.";
                    LblMsj.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // 5. Validar ID de material
                if (!int.TryParse(HdnMaterialId.Value, out int materialId))
                {
                    LblMsj.Text = "El material seleccionado no es válido.";
                    LblMsj.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // 6. Generar NUEVO ticket para la actualización
                string nuevoTicket = GenerateTicket(); // Generamos un ticket nuevo

                // 7. Obtener fecha (usar fecha actual si hay error)
                DateTime fecha = DateTime.TryParse(TBFecha.Text, out DateTime f) ? f : DateTime.Now;

                // 8. Ejecutar actualización con el nuevo ticket
                bool resultado = objPur.updatePurchaseRequest(
                    int.Parse(HFPurchaId.Value),
                    nuevoTicket, // Usamos el ticket recién generado
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
                    LblMsj.ForeColor = System.Drawing.Color.Green;

                    // Refrescar la lista para mostrar el nuevo ticket
                    showPurchaseRequestsByUser(userId);
                }
                else
                {
                    LblMsj.Text = "No se pudo completar la actualización. Intente nuevamente.";
                    LblMsj.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                LblMsj.Text = $"Error inesperado: {ex.Message}";
                LblMsj.ForeColor = System.Drawing.Color.Red;
            }
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
            try
            {
                if (GVRequests.SelectedRow != null)
                {
                    GridViewRow row = GVRequests.SelectedRow;

                    // 1. Validar estructura básica
                    if (row.Cells.Count < 8 || GVRequests.DataKeys[row.RowIndex] == null)
                    {
                        LblMsj.Text = "Estructura de datos no válida. No se pueden cargar los detalles.";
                        LblMsj.ForeColor = System.Drawing.Color.Red;
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
                        TBFecha.Text = fecha.ToString("yyyy-MM-dd"); // Formato para BD
                        LblFechaMostrar.Text = fecha.ToString("dd/MM/yyyy"); // Formato visual
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
                        LblMsj.ForeColor = System.Drawing.Color.Red;
                        return;
                    }

                    // 4. Cantidad con valor por defecto seguro
                    if (int.TryParse(row.Cells[5].Text, out int cantidad) && cantidad > 0)
                    {
                        TBQuantity.Text = cantidad.ToString();
                    }
                    else
                    {
                        TBQuantity.Text = "1"; // Valor por defecto seguro
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

                    // 6. Limpiar mensajes de error si todo fue bien
                    LblMsj.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                // 7. Manejo centralizado de errores
                LblMsj.Text = $"Error al cargar detalles: {ex.Message}";
                LblMsj.ForeColor = System.Drawing.Color.Red;

                // Registrar el error completo para diagnóstico
                System.Diagnostics.Debug.WriteLine($"Error en GVRequests_SelectedIndexChanged: {ex.ToString()}");
            }
        }

        // Método para manejar el cambio de página
        protected void GVRequests_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVRequests.PageIndex = e.NewPageIndex;
            int loggedInUserId = Convert.ToInt32(Session["UserID"]);
            showPurchaseRequestsByUser(loggedInUserId);
        }

        protected void BtnBuscarMaterial_Click(object sender, EventArgs e)
        {
            // Redirecciona al formulario de búsqueda de materiales
            Response.Redirect("WFPresentationMaterial.aspx");
        }

    }
}
