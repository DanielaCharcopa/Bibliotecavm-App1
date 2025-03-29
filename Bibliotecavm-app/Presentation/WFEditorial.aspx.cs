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

        private void showEditorials()
        {
            DataSet ds = objEdit.showEditorials();
            GVEditorial.DataSource = ds;
            GVEditorial.DataBind();
        }

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

        protected void GVEditorial_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GVEditorial.SelectedRow;

            HFEditorialId.Value = row.Cells[0].Text; // ID oculto
            TBName.Text = HttpUtility.HtmlDecode(row.Cells[1].Text.Trim());
            TBCity.Text = HttpUtility.HtmlDecode(row.Cells[2].Text.Trim());
            TBPhone.Text = row.Cells[3].Text;
            TBEmail.Text = HttpUtility.HtmlDecode(row.Cells[4].Text.Trim());
        }


        private void clearFields()
        {
            HFEditorialId.Value = string.Empty;
            TBName.Text = string.Empty;
            TBCity.Text = string.Empty;
            TBPhone.Text = string.Empty;
            TBEmail.Text = string.Empty;
        }
    }
}
