using Logic;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Presentation
{
    public partial class WFAuthors : System.Web.UI.Page
    {
        AuthorsLog objAut = new AuthorsLog();  // Instancia de la capa lógica de autores

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                showAuthors();  // Carga los autores en la GridView
            }
        }

        // Evento para mostrar un autor
        private void showAuthors()
        {
            try
            {
                DataSet ds = objAut.showAuthors();  // Obtiene los autores desde la capa lógica

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GVAuthors.DataSource = ds;
                    GVAuthors.DataBind();
                }
                else
                {
                    GVAuthors.DataSource = null;
                    GVAuthors.DataBind();
                    LblMessage.Text = "No hay autores registrados.";
                    LblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                LblMessage.Text = "Error al cargar autores: " + ex.Message;
                LblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Evento para seleccionar un autor
        protected void GVAuthors_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = GVAuthors.SelectedRow;

                // Depuración: mostrar valores seleccionados en consola
                Console.WriteLine("ID Autor: " + row.Cells[0].Text);
                Console.WriteLine("Nombre: " + row.Cells[1].Text);
                Console.WriteLine("Apellido: " + row.Cells[2].Text);
                Console.WriteLine("Municipio: " + row.Cells[3].Text);

                // Asignar valores a los campos del formulario
                HFAuthorsId.Value = row.Cells[0].Text;
                TBNombre.Text = HttpUtility.HtmlDecode(row.Cells[1].Text.Trim());
                TBApellido.Text = HttpUtility.HtmlDecode(row.Cells[2].Text.Trim());
                TBMunicipio.Text = HttpUtility.HtmlDecode(row.Cells[3].Text.Trim());

                // Mostrar mensaje de éxito
                LblMessage.Text = "Autor seleccionado correctamente. Puedes actualizar o eliminar.";
                LblMessage.ForeColor = System.Drawing.Color.Green;
                LblMessage.Visible = true;

                // Ejecutar script para ocultar mensaje después de un tiempo
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideMessage", "hideMessageAfterDelay();", true);
            }
            catch (Exception ex)
            {
                // Manejar y mostrar errores de forma segura
                Console.WriteLine("Error al seleccionar autor: " + ex.Message);
                LblMessage.Text = "Error al seleccionar el autor. Por favor, intente de nuevo.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                LblMessage.Visible = true;
            }
        }

        // Evento para paginación 
        protected void GVAuthors_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVAuthors.PageIndex = e.NewPageIndex;

            // Si hay texto de búsqueda, aplicar el filtro
            string searchText = TBSearch.Text.Trim().ToLower();
            DataSet ds = objAut.showAuthors();

            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                dt.Columns.Add("nombreCompleto", typeof(string), "au_nombre + ' ' + au_apellido");

                DataView dv = dt.DefaultView;

                if (!string.IsNullOrEmpty(searchText))
                {
                    dv.RowFilter = $"nombreCompleto LIKE '%{searchText}%'";
                }

                GVAuthors.DataSource = dv;
                GVAuthors.DataBind();
            }
        }

        // Evento para limpiar formularios
        private void clear()
        {
            HFAuthorsId.Value = string.Empty;
            TBNombre.Text = "";
            TBApellido.Text = "";
            TBMunicipio.Text = "";
        }

        // Evento para guardar un autor
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = TBNombre.Text.Trim();
                string apellido = TBApellido.Text.Trim();
                string municipio = TBMunicipio.Text.Trim();

                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(municipio))
                {
                    LblMessage.Text = "Por favor, complete todos los campos.";
                    LblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                bool executed = objAut.saveAuthor(nombre, apellido, municipio);

                if (executed)
                {
                    LblMessage.Text = "El autor se guardó exitosamente!";
                    LblMessage.ForeColor = System.Drawing.Color.Green;
                    showAuthors();
                    clear();
                }
                else
                {
                    LblMessage.Text = "Error al guardar el autor.";
                    LblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                LblMessage.Text = "Error inesperado: " + ex.Message;
                LblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Evento para actualizar un autor 
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HFAuthorsId.Value) && int.TryParse(HFAuthorsId.Value, out int id))
            {
                string nombre = TBNombre.Text;
                string apellido = TBApellido.Text;
                string municipio = TBMunicipio.Text;

                bool executed = objAut.updateAuthor(id, nombre, apellido, municipio);

                if (executed)
                {
                    LblMessage.Text = "¡Autor actualizado exitosamente!";
                    clear();
                    showAuthors();
                }
                else
                {
                    LblMessage.Text = "Error al actualizar el autor .";
                }
            }
            else
            {
                LblMessage.Text = "Error: No se ha seleccionado una editorial válida para actualizar.";
            }

        }

        // Evento para eliminar un autor
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HFAuthorsId.Value) && int.TryParse(HFAuthorsId.Value, out int id))
            {
                bool executed = objAut.deleteAuthor(id);

                if (executed)
                {
                    LblMessage.Text = "¡Autor eliminado exitosamente!";
                    clear();
                    showAuthors();
                }
                else
                {
                    LblMessage.Text = "Error al eliminar un autor. Verifica que el autor exista.";
                }
            }
            else
            {
                LblMessage.Text = "Error: No se ha seleccionado un actor válido para eliminar.";
            }
        }

        // Evento para buscar materiales
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchText = TBSearch.Text.Trim().ToLower();

            DataSet ds = objAut.showAuthors();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                dt.Columns.Add("nombreCompleto", typeof(string), "au_nombre + ' ' + au_apellido");

                DataView dv = dt.DefaultView;

                if (!string.IsNullOrEmpty(searchText))
                {
                    dv.RowFilter = $"nombreCompleto LIKE '%{searchText}%'";
                }

                GVAuthors.DataSource = dv;
                GVAuthors.DataBind();

                if (dv.Count > 0)
                {
                    LblSearchResult.Text = $"Registros encontrados: {dv.Count}";
                    LblSearchResult.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    LblSearchResult.Text = "No se encontraron autores registrados.";
                    LblSearchResult.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                GVAuthors.DataSource = null;
                GVAuthors.DataBind();
                LblSearchResult.Text = "No se encontraron autores registrados.";
                LblSearchResult.ForeColor = System.Drawing.Color.Red;
            }
        }
  }

}


