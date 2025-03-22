using Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFMatEducativoSecondary : System.Web.UI.Page
    {
        MaterialEducativoLog objMatEdu = new MaterialEducativoLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadMaterials();
            }
        }

        private void LoadMaterials()
        {
            try
            {
                DataSet ds = objMatEdu.showMaterialEdu();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    RptMateriales.DataSource = ds;
                    RptMateriales.DataBind();
                    LblNoData.Visible = false;
                }
                else
                {
                    LblNoData.Visible = true;
                }
            }
            catch (Exception ex)
            {
                LblNoData.Text = "Error al cargar los materiales: " + ex.Message;
                LblNoData.Visible = true;
            }
        }

        protected void RptMateriales_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "ViewMaterial")
            {
                string fileUrl = e.CommandArgument.ToString();
                string fileExtension = System.IO.Path.GetExtension(fileUrl).ToLower();

                string viewerHtml = "";

                // Mostrar contenido según el tipo de archivo
                if (fileExtension == ".pdf")
                {
                    viewerHtml = $"<iframe src='{fileUrl}' style='width:100%; height:600px;' frameborder='0'></iframe>";
                }
                else if (fileExtension == ".mp4" || fileExtension == ".webm")
                {
                    viewerHtml = $"<video controls style='width:100%; max-height:600px;'><source src='{fileUrl}' type='video/mp4'>Tu navegador no soporta la reproducción de video.</video>";
                }
                else if (fileExtension == ".mp3" || fileExtension == ".wav")
                {
                    viewerHtml = $"<audio controls style='width:100%;'><source src='{fileUrl}' type='audio/mpeg'>Tu navegador no soporta la reproducción de audio.</audio>";
                }
                else
                {
                    viewerHtml = $"<p>No se puede visualizar este tipo de archivo. Por favor, descárgalo para verlo.</p>";
                }

                litViewerContent.Text = viewerHtml;
                PnlViewer.Style["display"] = "block";
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            PnlViewer.Style["display"] = "none";
            litViewerContent.Text = "";  // Limpiar el contenido del visor
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            // Obtener el texto de búsqueda y convertirlo a minúsculas
            string searchText = TBSearch.Text.Trim().ToLower();

            // Obtener todos los materiales desde la lógica de negocio
            DataSet ds = objMatEdu.showMaterialEdu();

            // Verificar si el DataSet tiene datos
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // Crear un DataView para aplicar el filtro
                DataView dv = ds.Tables[0].DefaultView;

                // Aplicar el filtro si hay texto de búsqueda
                if (!string.IsNullOrEmpty(searchText))
                {
                    dv.RowFilter = $"mat_titulo LIKE '%{searchText}%' OR mat_keywords LIKE '%{searchText}%'";
                    dv.RowFilter = $"CONVERT(mat_ano_publicacion, 'System.String') LIKE '%{searchText}%' OR mat_keywords LIKE '%{searchText}%'";
                    dv.RowFilter = $"mat_formato LIKE '%{searchText}%' OR mat_keywords LIKE '%{searchText}%'";
                }
            

                // Enlazar el DataView al GridView
                RptMateriales.DataSource = dv;
            }
            else
            {
                // Si no hay datos, enlazar un DataSet vacío al GridView
                RptMateriales.DataSource = ds;
            }

            // Actualizar el GridView
            RptMateriales.DataBind();
        }
    }
}
