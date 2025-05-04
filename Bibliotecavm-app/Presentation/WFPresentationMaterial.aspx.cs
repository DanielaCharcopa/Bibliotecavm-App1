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
    public partial class WFPresentationMaterial : System.Web.UI.Page
    {
        VisitsLog VisitsLog = new VisitsLog();
        UserLogic objUser = new UserLogic();
        PurchaseRequestLog objPur = new PurchaseRequestLog();
        CategoryLog objCat = new CategoryLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar si el usuario está logueado
                if (Session["UserID"] == null || Session["UserRole"] == null)
                {
                    Response.Redirect("Default.aspx");
                    return;
                }

                // Cargar todos los materiales educativos
                CargarMaterialesEducativos();
            }
        }

        private void CargarMaterialesEducativos(string filtroTitulo = "", string filtroFormato = "", string categoria = "")
        {
            try
            {
                // Instancia de la capa de lógica
                MaterialEducativoLog logica = new MaterialEducativoLog();
                CategoryLog objCat = new CategoryLog();

                // Obtener los materiales educativos y categorías
                DataSet dsMateriales = logica.showMaterialEdu();
                DataSet dsCategory = objCat.showCategory();

                // Verificar si hay datos
                if (dsMateriales != null && dsMateriales.Tables.Count > 0 && dsMateriales.Tables[0].Rows.Count > 0)
                {
                    DataView dv = dsMateriales.Tables[0].DefaultView;
                    List<string> filtros = new List<string>();

                    // Aplicar filtro por título si existe
                    if (!string.IsNullOrEmpty(filtroTitulo))
                    {
                        filtros.Add($"mat_titulo LIKE '%{filtroTitulo}%'");
                    }

                    // Aplicar filtro por formato si existe
                    if (!string.IsNullOrEmpty(filtroFormato))
                    {
                        filtros.Add($"mat_formato = '{filtroFormato}'");
                    }

                    // Aplicar filtro por categoría si existe
                    if (!string.IsNullOrEmpty(categoria))
                    {
                        // Verificar si la columna categoria_nombre existe en el DataTable
                        if (dsMateriales.Tables[0].Columns.Contains("categoria_nombre"))
                        {
                            filtros.Add($"categoria_nombre = '{categoria}'");
                        }
                        else if (dsMateriales.Tables[0].Columns.Contains("cat_nombre"))
                        {
                            filtros.Add($"cat_nombre = '{categoria}'");
                        }
                        else
                        {
                            throw new Exception("No se encontró la columna de categoría en los datos");
                        }
                    }

                    // Combinar todos los filtros
                    if (filtros.Count > 0)
                    {
                        dv.RowFilter = string.Join(" AND ", filtros);
                    }

                    // Enlazar los datos al GridView
                    GVMateriales.DataSource = dv;
                    GVMateriales.DataBind();

                    // Mostrar mensaje si no hay resultados después de filtrar
                    if (dv.Count == 0)
                    {
                        LblMensaje.Text = "No se encontraron materiales con los criterios de búsqueda.";
                    }
                    else
                    {
                        LblMensaje.Text = $"Mostrando {dv.Count} materiales";
                    }
                }
                else
                {
                    // Mostrar mensaje si no hay datos
                    LblMensaje.Text = "No hay materiales educativos disponibles.";
                    GVMateriales.DataSource = null;
                    GVMateriales.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Manejar errores
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
            if (e.CommandName == "Comprar")
            {
                int matId = Convert.ToInt32(e.CommandArgument);

                // 1. Validar sesión
                if (Session["UserID"] == null)
                {
                    Response.Redirect("Login.aspx?returnUrl=" + Server.UrlEncode(Request.Url.ToString()));
                    return;
                }

                // 2. Obtener datos del material usando el SP
                DataTable dtMaterial = objPur.GetMaterialById(matId);

                if (dtMaterial.Rows.Count > 0)
                {
                    DataRow row = dtMaterial.Rows[0];
                    string nombreMaterial = row["mat_titulo"].ToString();
                    decimal precioMaterial = Convert.ToDecimal(row["mat_precio"]);
                    string formatoMaterial = row["mat_formato"].ToString();

                    // 3. Redireccionar con todos los parámetros
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

        [System.Web.Services.WebMethod]
        public static Dictionary<string, object> RegistrarVisitaInicial(int materialId)
        {
            try
            {
                var page = new WFPresentationMaterial();
                int userId = Convert.ToInt32(HttpContext.Current.Session["UserID"]);

                // Registrar visita con duración inicial cero
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
            { "urlMaterial", url }
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
            // Lógica para obtener la URL del material educativo
            MaterialEducativoLog logica = new MaterialEducativoLog();
            DataSet ds = logica.showMaterialEdu();
            DataRow[] rows = ds.Tables[0].Select($"mat_id = {matId}");
            if (rows.Length > 0)
            {
                return rows[0]["mat_url_descarga"].ToString();
            }
            else
            {
                throw new Exception("No se encontró el material educativo.");
            }
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