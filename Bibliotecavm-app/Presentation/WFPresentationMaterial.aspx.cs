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
            if (e.CommandName == "Ver")
            {
                int matId = Convert.ToInt32(e.CommandArgument);
                int usuId = Convert.ToInt32(Session["UserID"]);

                // 1. Registrar la visita y obtener el ID
                int visitaId = RegistrarVisita(usuId, matId);

                // 2. Obtener URL del material
                string url = ObtenerUrlMaterial(matId);

                // 3. Script que abre la ventana Y configura el evento de cierre
                string script = @"
            var ventanaMaterial = window.open('" + url + @"', '_blank');
            
            ventanaMaterial.onload = function() {
                var inicio = new Date();
                
                ventanaMaterial.onbeforeunload = function() {
                    var fin = new Date();
                    var duracionMs = fin - inicio;
                    var duracion = new Date(duracionMs).toISOString().substr(11, 8); // Formato HH:MM:SS
                    
                    // AJAX síncrono para asegurar el envío
                    var xhr = new XMLHttpRequest();
                    xhr.open('POST', 'WFPresentationMaterial.aspx/ActualizarDuracionVisita', false);
                    xhr.setRequestHeader('Content-Type', 'application/json');
                    xhr.send(JSON.stringify({ 
                        visitaId: " + visitaId + @", 
                        duracion: duracion 
                    }));
                };
            };";

                ScriptManager.RegisterStartupScript(this, GetType(), "ControlDuracion", script, true);
            }
            else if (e.CommandName == "Comprar")
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
        public static Dictionary<string, string> RegistrarVisita(int matId)
        {
            try
            {
                WFPresentationMaterial page = new WFPresentationMaterial();
                int usuId = Convert.ToInt32(HttpContext.Current.Session["UserID"]);

                // Registrar la visita y obtener el ID
                int visitaId = page.RegistrarVisita(usuId, matId);

                return new Dictionary<string, string> {
            { "visitaId", visitaId.ToString() },
            { "fechaIngreso", DateTime.Now.ToString("o") } // ISO 8601 format
        };
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar la visita: " + ex.Message);
            }
        }

        private int RegistrarVisita(int usuId, int matId)
        {
            try
            {
                // Obtener el nombre del usuario logueado
                string usuarioNombre = Session["UserName"].ToString();

                // Obtener el título del material
                MaterialEducativoLog logicaMaterial = new MaterialEducativoLog();
                DataSet dsMaterial = logicaMaterial.showMaterialEdu();
                DataRow[] rows = dsMaterial.Tables[0].Select($"mat_id = {matId}");
                string materialTitulo = rows.Length > 0 ? rows[0]["mat_titulo"].ToString() : "Desconocido";

                // Registrar la visita
                VisitsLog logica = new VisitsLog();
                DateTime fechaIngreso = DateTime.Now;
                TimeSpan duracion = TimeSpan.Zero;

                bool resultado = logica.saveVisits(fechaIngreso, duracion, usuId, matId);

                if (resultado)
                {
                    // Obtener el ID de la visita recién insertada
                    int visitaId = logica.ObtenerUltimaVisitaId(usuId, matId);
                    return visitaId;
                }
                else
                {
                    throw new Exception("Error al registrar la visita.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
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
        public static void ActualizarDuracionVisita(int visitaId, string duracion)
        {
            try
            {
                VisitsLog logica = new VisitsLog();
                logica.ActualizarDuracionVisita(visitaId, duracion);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la duración de la visita: " + ex.Message);
            }
        }



    }
}