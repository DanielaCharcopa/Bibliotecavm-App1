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
            LblMessage.Text = string.Empty;
            if (!IsPostBack)
            {
                showCategory();
            }
        }

        // Evento para mostrar categoria
        private void showCategory()
        {
            DataSet ds = objCat.showCategory();
            GVCategory.DataSource = ds;
            GVCategory.DataBind();
        }

        // Evento para guardar una categoria
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

        // Evento para actualizar una categoria
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

        // Evento para eliminar una categoria
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

        // Evento para seleccionar categorias
        protected void GVCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = GVCategory.SelectedRow;

                // Depuración: mostrar valores seleccionados en consola
                Console.WriteLine("ID Categoría: " + row.Cells[0].Text);
                Console.WriteLine("Nombre: " + row.Cells[1].Text);
                Console.WriteLine("Descripción: " + row.Cells[2].Text);

                // Asignar valores a los campos del formulario
                HFCategoryId.Value = row.Cells[0].Text;
                DDLName.SelectedValue = HttpUtility.HtmlDecode(row.Cells[1].Text.Trim());
                TBDescription.Text = HttpUtility.HtmlDecode(row.Cells[2].Text.Trim());

                // Mostrar mensaje de éxito
                LblMessage.Text = "Categoría seleccionada correctamente. Puede actualizar o eliminar.";
                LblMessage.ForeColor = System.Drawing.Color.Green;
                LblMessage.Visible = true;

                // Ejecutar script para ocultar el mensaje después de un tiempo
                ScriptManager.RegisterStartupScript(this, this.GetType(), "HideMessage", "hideMessageAfterDelay();", true);
            }
            catch (Exception ex)
            {
                // Manejar y mostrar errores de forma segura
                Console.WriteLine("Error al seleccionar categoría: " + ex.Message);
                LblMessage.Text = "Error al seleccionar la categoría. Por favor, intente de nuevo.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                LblMessage.Visible = true;
            }
        }

        // Evento para limpiar formularios
        private void clear()
        {
            HFCategoryId.Value = string.Empty;
            DDLName.SelectedIndex = 0;
            TBDescription.Text = string.Empty;
        }

        //Evento para paginación 
        protected void GVCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVCategory.PageIndex = e.NewPageIndex;
            showCategory(); // este método debe volver a cargar los datos
        }

        // Evento para buscar categorias
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchText = TBSearch.Text.Trim().ToLower();

            DataSet ds = objCat.showCategory();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                DataView dv = dt.DefaultView;

                if (!string.IsNullOrEmpty(searchText))
                {
                    // Escapar comillas simples para evitar errores
                    searchText = searchText.Replace("'", "''");
                    dv.RowFilter = $"cat_nombre LIKE '%{searchText}%'";
                }

                GVCategory.DataSource = dv;
                GVCategory.DataBind();

                if (dv.Count > 0)
                {
                    LblSearchResult.Text = $"Registros encontrados: {dv.Count}";
                    LblSearchResult.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    LblSearchResult.Text = "No se encontraron categorías registradas.";
                    LblSearchResult.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                GVCategory.DataSource = null;
                GVCategory.DataBind();
                LblSearchResult.Text = "No se encontraron categorías registradas.";
                LblSearchResult.ForeColor = System.Drawing.Color.Red;
            }
        }


    }
}