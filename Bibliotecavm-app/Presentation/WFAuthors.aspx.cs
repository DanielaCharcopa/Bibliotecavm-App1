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
                    LblMsj.Text = "No hay autores registrados.";
                    LblMsj.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                LblMsj.Text = "Error al cargar autores: " + ex.Message;
                LblMsj.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void GVAuthors_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GVAuthors.SelectedRow;

            HFAuthorsId.Value = row.Cells[0].Text;
            TBNombre.Text = HttpUtility.HtmlDecode(row.Cells[1].Text.Trim());
            TBApellido.Text = HttpUtility.HtmlDecode(row.Cells[2].Text.Trim());
            TBMunicipio.Text = HttpUtility.HtmlDecode(row.Cells[3].Text.Trim());

       }

        private void clear()
        {
            HFAuthorsId.Value = string.Empty;
            TBNombre.Text = "";
            TBApellido.Text = "";
            TBMunicipio.Text = "";
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string nombre = TBNombre.Text.Trim();
                string apellido = TBApellido.Text.Trim();
                string municipio = TBMunicipio.Text.Trim();

                if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(municipio))
                {
                    LblMsj.Text = "Por favor, complete todos los campos.";
                    LblMsj.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                bool executed = objAut.saveAuthor(nombre, apellido, municipio);

                if (executed)
                {
                    LblMsj.Text = "El autor se guardó exitosamente!";
                    LblMsj.ForeColor = System.Drawing.Color.Green;
                    showAuthors();
                    clear();
                }
                else
                {
                    LblMsj.Text = "Error al guardar el autor.";
                    LblMsj.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                LblMsj.Text = "Error inesperado: " + ex.Message;
                LblMsj.ForeColor = System.Drawing.Color.Red;
            }
        }
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

    }
}


