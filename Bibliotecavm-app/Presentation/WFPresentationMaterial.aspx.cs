using System;
using System.Data;
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

        private void CargarMaterialesEducativos(string filtroTitulo = "", string filtroFormato = "")
        {
            try
            {
                // Instancia de la capa de lógica
                MaterialEducativoLog logica = new MaterialEducativoLog();

                // Obtener los materiales educativos
                DataSet dsMateriales = logica.showMaterialEdu();

                // Aplicar filtros si existen
                if (dsMateriales != null && dsMateriales.Tables.Count > 0 && dsMateriales.Tables[0].Rows.Count > 0)
                {
                    DataView dv = dsMateriales.Tables[0].DefaultView;

                    // Aplicar filtro por título si existe
                    if (!string.IsNullOrEmpty(filtroTitulo))
                    {
                        dv.RowFilter = $"mat_titulo LIKE '%{filtroTitulo}%'";
                    }

                    // Aplicar filtro por formato si existe
                    if (!string.IsNullOrEmpty(filtroFormato))
                    {
                        if (!string.IsNullOrEmpty(dv.RowFilter))
                            dv.RowFilter += " AND ";
                        dv.RowFilter += $"mat_formato = '{filtroFormato}'";
                    }

                    // Enlazar los datos al GridView
                    GVMateriales.DataSource = dv;
                    GVMateriales.DataBind();
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

            CargarMaterialesEducativos(filtroTitulo, filtroFormato);
        }

        protected void GVMateriales_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ver")
            {
                int matId = Convert.ToInt32(e.CommandArgument);
                int usuId = Convert.ToInt32(Session["UserID"]);

                // Registrar una nueva visita con duración inicial de 00:00:00
                int visitaId = RegistrarVisita(usuId, matId);

                // Abrir el contenido en una nueva ventana
                string url = ObtenerUrlMaterial(matId);
                string script = $"window.open('{url}', '_blank');";
                ScriptManager.RegisterStartupScript(this, GetType(), "OpenWindow", script, true);

                // Guardar el ID de la visita en la sesión
                Session["VisitaActiva"] = visitaId;
            }
            else if (e.CommandName == "Comprar")
            {
                int matId = Convert.ToInt32(e.CommandArgument);
                // Redirigir al formulario de compra con el ID del material
                Response.Redirect($"WFPurchaseRequest.aspx?id={matId}");
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