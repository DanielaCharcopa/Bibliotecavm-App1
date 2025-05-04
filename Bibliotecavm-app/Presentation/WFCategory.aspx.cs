using Logic;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFCategory : System.Web.UI.Page
    {
        CategoryLog objCat = new CategoryLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showCategory();
            }
        }
       private void showCategory()
        {
            DataSet ds = objCat.showCategory();
            GVCategory.DataSource = ds;
            GVCategory.DataBind();
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (DDLName.SelectedIndex > 0 && !string.IsNullOrWhiteSpace(TBDescription.Text)) // Verificar si se seleccionó una categoría válida y la descripción no está vacía
            {
                string name = DDLName.SelectedValue;
                string description = TBDescription.Text;

                bool executed = objCat.saveCategory(name, description);

                if (executed)
                {
                    LblMessage.Text = "¡Categoría guardada exitosamente!";
                    clear();
                    showCategory();
                }
                else
                {
                    LblMessage.Text = "Error al guardar la categoría. Verifica los datos.";
                }
            }
            else
            {
                LblMessage.Text = "Por favor, complete todos los campos.";
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HFCategoryId.Value) && int.TryParse(HFCategoryId.Value, out int id))
            {
                if (DDLName.SelectedIndex > 0)
                {
                    string name = DDLName.SelectedValue;
                    string description = TBDescription.Text;

                    bool executed = objCat.updateCategory(id, name, description);

                    if (executed)
                    {
                        LblMessage.Text = "¡Categoría actualizada exitosamente!";
                        clear();
                        showCategory();
                    }
                    else
                    {
                        LblMessage.Text = "Error al actualizar la categoría.";
                    }
                }
                else
                {
                    LblMessage.Text = "Por favor, seleccione una categoría válida.";
                }
            }
            else
            {
                LblMessage.Text = "Error: No se ha seleccionado una categoría válida para actualizar.";
            }
        }
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HFCategoryId.Value) && int.TryParse(HFCategoryId.Value, out int id))
            {
                bool executed = objCat.deleteCategory(id);

                if (executed)
                {
                    LblMessage.Text = "¡Categoría eliminada exitosamente!";
                    clear();
                    showCategory();
                }
                else
                {
                    LblMessage.Text = "Error al eliminar la categoría.";
                }
            }
            else
            {
                LblMessage.Text = "Error: No se ha seleccionado una categoría válida para eliminar.";
            }
        }

        protected void GVCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GVCategory.SelectedRow;

            HFCategoryId.Value = row.Cells[0].Text;
            DDLName.SelectedValue = HttpUtility.HtmlDecode(row.Cells[1].Text.Trim());
            TBDescription.Text = HttpUtility.HtmlDecode(row.Cells[2].Text.Trim());
        }

        private void clear()
        {
            HFCategoryId.Value = string.Empty;
            DDLName.SelectedIndex = 0;
            TBDescription.Text = string.Empty;
        }
    }
}