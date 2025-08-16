using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logic;

namespace Presentation
{
    public partial class WFPresentationMaterial : Page
    {
        VisitsLog VisitsLog = new VisitsLog();
        UserLogic objUser = new UserLogic();
        PurchaseRequestLog objPur = new PurchaseRequestLog();
        CategoryLog objCat = new CategoryLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserID"] == null || Session["UserRole"] == null)
                {
                    Response.Redirect("Default.aspx");
                    return;
                }

                CargarMaterialesEducativos();
            }
        }

        private void CargarMaterialesEducativos(string filtroTitulo = "", string filtroFormato = "", string categoria = "")
        {
            try
            {
                var logica = new MaterialEducativoLog();
                var dsMateriales = logica.showMaterialEdu();

                if (dsMateriales?.Tables.Count > 0 && dsMateriales.Tables[0].Rows.Count > 0)
                {
                    DataView dv = dsMateriales.Tables[0].DefaultView;
                    List<string> filtros = new List<string>();

                    if (!string.IsNullOrEmpty(filtroTitulo))
                    {
                        filtros.Add($"mat_titulo LIKE '%{filtroTitulo}%'");
                    }

                    if (!string.IsNullOrEmpty(filtroFormato))
                    {
                        filtros.Add($"mat_formato = '{filtroFormato}'");
                    }

                    if (!string.IsNullOrEmpty(categoria))
                    {
                        if (dsMateriales.Tables[0].Columns.Contains("categoria_nombre"))
                        {
                            filtros.Add($"categoria_nombre = '{categoria}'");
                        }
                        else if (dsMateriales.Tables[0].Columns.Contains("cat_nombre"))
                        {
                            filtros.Add($"cat_nombre = '{categoria}'");
                        }
                    }

                    if (filtros.Count > 0)
                    {
                        dv.RowFilter = string.Join(" AND ", filtros);
                    }

                    GVMateriales.DataSource = dv;
                    GVMateriales.DataBind();

                    LblMensaje.Text = dv.Count == 0
                        ? "No se encontraron materiales con los criterios de búsqueda."
                        : $"Mostrando {dv.Count} materiales";
                }
                else
                {
                    LblMensaje.Text = "No hay materiales educativos disponibles.";
                    GVMateriales.DataSource = null;
                    GVMateriales.DataBind();
                }
            }
            catch (Exception ex)
            {
                LblMensaje.Text = "Error al cargar los materiales educativos: " + ex.Message;
                GVMateriales.DataSource = null;
                GVMateriales.DataBind();
            }
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            string filtroTitulo = TxtBuscarTitulo.Text.Trim();
            string filtroFormato = DdlFormato.SelectedValue;
            string filtroCategoria = DdlCategoria.SelectedValue;

            CargarMaterialesEducativos(filtroTitulo, filtroFormato, filtroCategoria);
        }

        protected void GVMateriales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "VerMaterial")
            {
                string[] args = e.CommandArgument.ToString().Split('|');
                string fileUrl = args[1];

                if (fileUrl.Contains("drive.google.com"))
                {
                    string fileId = ObtenerFileIdDeUrl(fileUrl);
                    fileUrl = $"https://drive.google.com/file/d/{fileId}/preview";
                }

                ViewState["MaterialActual"] = args[0];
                ViewState["HoraInicio"] = DateTime.Now;

                lblTituloModal.Text = args[2];
                frameMaterial.Attributes["src"] = fileUrl;
                pnlModal.Visible = true;
                btnFinalizarVisita.Visible = true;
                //lblMensaje.Text = "Tiempo iniciado automáticamente";

                ScriptManager.RegisterStartupScript(this, GetType(), "actualizarTiempo", "actualizarContador();", true);
            }
            else if (e.CommandName == "Comprar")
            {
                int matId = Convert.ToInt32(e.CommandArgument);

                // Validar sesión
                if (Session["UserID"] == null)
                {
                    Response.Redirect("Login.aspx?returnUrl=" + Server.UrlEncode(Request.Url.ToString()));
                    return;
                }

                // Obtener datos del material
                DataTable dtMaterial = objPur.GetMaterialById(matId);

                if (dtMaterial.Rows.Count > 0)
                {
                    DataRow row = dtMaterial.Rows[0];
                    string nombreMaterial = row["mat_titulo"].ToString();
                    decimal precioMaterial = Convert.ToDecimal(row["mat_precio"]);
                    string formatoMaterial = row["mat_formato"].ToString();

                    // Redireccionar a la página de compra con los parámetros
                    string url = $"WFPurchaseRequest.aspx?" +
                                $"matId={matId}" +
                                $"&nombre={HttpUtility.UrlEncode(nombreMaterial)}" +
                                $"&precio={precioMaterial.ToString("F2", CultureInfo.InvariantCulture)}" +
                                $"&formato={HttpUtility.UrlEncode(formatoMaterial)}";

                    Response.Redirect(url);
                }
                else
                {
                    LblMensaje.Text = "El material seleccionado no está disponible.";
                    LblMensaje.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private string ObtenerFileIdDeUrl(string url)
        {
            int start = url.IndexOf("/d/") + 3;
            int end = url.IndexOf("/", start);
            if (end == -1) end = url.Length;

            return url.Substring(start, end - start);
        }

        protected void btnFinalizarVisita_Click(object sender, EventArgs e)
        {
            if (ViewState["HoraInicio"] != null && ViewState["MaterialActual"] != null)
            {
                DateTime inicio = (DateTime)ViewState["HoraInicio"];
                TimeSpan duracion = DateTime.Now - inicio;
                int matId = int.Parse(ViewState["MaterialActual"].ToString());

                int visitaId = new VisitsLog().saveVisits(
                    inicio,
                    duracion,
                    Convert.ToInt32(Session["UserId"]),
                    matId
                );

                //lblMensaje.Text = visitaId > 0
                //    ? $"Visita registrada! Duración: {duracion.ToString(@"hh\:mm\:ss")}"
                //    : "Error al registrar visita";
            }

            pnlModal.Visible = false;
            ViewState.Remove("HoraInicio");
            ViewState.Remove("MaterialActual");
        }

        [System.Web.Services.WebMethod]
        public static Dictionary<string, object> RegistrarVisitaInicial(int materialId)
        {
            try
            {
                var page = new WFPresentationMaterial();
                int userId = Convert.ToInt32(HttpContext.Current.Session["UserID"]);

                int visitaId = page.VisitsLog.saveVisits(
                    DateTime.Now,
                    TimeSpan.Zero,
                    userId,
                    materialId
                );

                string url = page.ObtenerUrlMaterial(materialId);

                return new Dictionary<string, object> {
                    { "success", true },
                    { "visitaId", visitaId },
                    { "urlMaterial", url },
                    { "timestamp", DateTime.Now.ToString("o") }
                };
            }
            catch (Exception ex)
            {
                return new Dictionary<string, object> {
                    { "success", false },
                    { "error", ex.Message }
                };
            }
        }

        private string ObtenerUrlMaterial(int matId)
        {
            MaterialEducativoLog logica = new MaterialEducativoLog();
            DataSet ds = logica.showMaterialEdu();
            DataRow[] rows = ds.Tables[0].Select($"mat_id = {matId}");
            if (rows.Length > 0)
            {
                return rows[0]["mat_url_descarga"].ToString();
            }
            throw new Exception("No se encontró el material educativo.");
        }

        [System.Web.Services.WebMethod]
        public static Dictionary<string, object> ActualizarDuracionVisita(int visitaId, string duracion)
        {
            try
            {
                bool exito = new VisitsLog().ActualizarDuracionVisita(visitaId, duracion);
                return new Dictionary<string, object> {
                    { "success", exito },
                    { "visitaId", visitaId }
                };
            }
            catch (Exception ex)
            {
                return new Dictionary<string, object> {
                    { "success", false },
                    { "error", ex.Message }
                };
            }
        }
    }
}