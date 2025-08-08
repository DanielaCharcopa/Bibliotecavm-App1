using Logic;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFEditorial : System.Web.UI.Page
    {
        PublishersLog objEdit = new PublishersLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showEditorials(); // Mostrar todas las editoriales al cargar la página
            }
        }

        // Evento para mostrar editoriales
        private void showEditorials()
        {
            DataSet ds = objEdit.showEditorials();
            GVEditorial.DataSource = ds;
            GVEditorial.DataBind();
        }

        // Evento para guardar editoriales
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string name = TBName.Text;
            string city = TBCity.Text;
            string phone = TBPhone.Text;
            string email = TBEmail.Text;

            if (!string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(city) && !string.IsNullOrWhiteSpace(phone) && !string.IsNullOrWhiteSpace(email))
           
            {
             
                bool executed = objEdit.saveEditorial(name, city, phone, email);

                if (executed)
                {
                    LblMessage.Text = "¡Editorial guardada exitosamente!";
                    clearFields();
                    showEditorials();
                }
                else
                {
                    LblMessage.Text = "Error al guardar la editorial. Verifica los datos.";
                }
            }
            else
            {
                LblMessage.Text = "Por favor, complete todos los campos.";
            }
        }

        // Evento para actualizar editoriales
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HFEditorialId.Value) && int.TryParse(HFEditorialId.Value, out int id))
            {
                string name = TBName.Text;
                string city = TBCity.Text;
                string phone = TBPhone.Text;
                string email = TBEmail.Text;

                bool executed = objEdit.updateEditorial(id, name, city, phone, email);

                if (executed)
                {
                    LblMessage.Text = "¡Editorial actualizada exitosamente!";
                    clearFields();
                    showEditorials();
                }
                else
                {
                    LblMessage.Text = "Error al actualizar la editorial.";
                }
            }
            else
            {
                LblMessage.Text = "Error: No se ha seleccionado una editorial válida para actualizar.";
            }
        }

        // Evento para eliminar editoriales
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HFEditorialId.Value) && int.TryParse(HFEditorialId.Value, out int id))
            {
                bool executed = objEdit.deleteEditorial(id);

                if (executed)
                {
                    LblMessage.Text = "¡Editorial eliminada exitosamente!";
                    clearFields();
                    showEditorials();
                }
                else
                {
                    LblMessage.Text = "Error al eliminar la editorial.";
                }
            }
            else
            {
                LblMessage.Text = "Error: No se ha seleccionado una editorial válida para eliminar.";
            }
        }

        // Evento para seleccionar editoriales
        protected void GVEditorial_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = GVEditorial.SelectedRow;

                // Depuración: mostrar valores seleccionados en consola
                Console.WriteLine("ID Editorial: " + row.Cells[0].Text);
                Console.WriteLine("Nombre: " + row.Cells[1].Text);
                Console.WriteLine("Ciudad: " + row.Cells[2].Text);
                Console.WriteLine("Teléfono: " + row.Cells[3].Text);
                Console.WriteLine("Correo: " + row.Cells[4].Text);

                // Asignar valores a los campos del formulario
                HFEditorialId.Value = row.Cells[0].Text;
                TBName.Text = HttpUtility.HtmlDecode(row.Cells[1].Text.Trim());
                TBCity.Text = HttpUtility.HtmlDecode(row.Cells[2].Text.Trim());
                TBPhone.Text = row.Cells[3].Text;
                TBEmail.Text = HttpUtility.HtmlDecode(row.Cells[4].Text.Trim());

                // Mostrar mensaje de éxito
                LblMessage.Text = "Editorial seleccionada correctamente. Puede actualizar o eliminar.";
                LblMessage.ForeColor = System.Drawing.Color.Green;
                LblMessage.Visible = true;

                // Ejecutar script para ocultar el mensaje después de un tiempo
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideMessage", "hideMessageAfterDelay();", true);
            }
            catch (Exception ex)
            {
                // Manejar y mostrar errores de forma segura
                Console.WriteLine("Error al seleccionar editorial: " + ex.Message);
                LblMessage.Text = "Error al seleccionar la editorial. Por favor, intente de nuevo.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                LblMessage.Visible = true;
            }
        }

        // Evento para limpiar formularios
        private void clearFields()
        {
            HFEditorialId.Value = string.Empty;
            TBName.Text = string.Empty;
            TBCity.Text = string.Empty;
            TBPhone.Text = string.Empty;
            TBEmail.Text = string.Empty;
        }

        // Evento para paginación
        protected void GVEditorial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVEditorial.PageIndex = e.NewPageIndex;
            showEditorials(); // este método debe volver a cargar los datos
        }

        // Evento para buscar Editoriales
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchText = TBSearch.Text.Trim().ToLower();

            DataSet ds = objEdit.showEditorials();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                DataView dv = dt.DefaultView;

                if (!string.IsNullOrEmpty(searchText))
                {
                    // Escapar comillas simples para evitar errores
                    searchText = searchText.Replace("'", "''");
                    dv.RowFilter = $"edi_nombre LIKE '%{searchText}%' OR edi_ciudad LIKE '%{searchText}%'";

                }

                GVEditorial.DataSource = dv;
                GVEditorial.DataBind();

                if (dv.Count > 0)
                {
                    LblSearchResult.Text = $"Registros Encontrados: {dv.Count}";
                    LblSearchResult.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    LblSearchResult.Text = "No Se Encontraron Editoriales Registradas.";
                    LblSearchResult.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
               GVEditorial.DataSource = null;
                GVEditorial.DataBind();
                LblSearchResult.Text = "No Se Encontraron Categorías Registradas.";
                LblSearchResult.ForeColor = System.Drawing.Color.Red;
            }
        }


    }
}
