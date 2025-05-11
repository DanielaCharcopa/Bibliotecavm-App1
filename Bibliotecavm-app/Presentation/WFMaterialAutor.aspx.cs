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
    public partial class WFMaterialAutor : System.Web.UI.Page
    {
        MaterialAutorLog objMat = new MaterialAutorLog();
        MaterialEducativoLog objMatEdu = new MaterialEducativoLog();
        AuthorsLog objAut = new AuthorsLog();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                showMaterialAutor();
              
            }

        }

        private void showMaterialAutor()
        {
            DataSet ds = objMat.showMaterialAutor();
            GVMaterialAutor.DataSource = ds;
            GVMaterialAutor.DataBind();

        }

       
            public class MaterialAutorEntry
            {
                public int MaterialID { get; set; }
                public string MaterialNombre { get; set; }
                public int AutorID { get; set; }
                public string AutorNombre { get; set; }
                public string Descripcion { get; set; }
            }



        protected void BtnAddEntry_Click(object sender, EventArgs e)
            {
                // Obtener la lista desde ViewState o inicializarla
                List<MaterialAutorEntry> entries = ViewState["Entries"] as List<MaterialAutorEntry> ?? new List<MaterialAutorEntry>();

                // Obtener los textos/nombres de los seleccionados
                string materialNombre = DDLMatEdu.SelectedItem.Text;
                string autorNombre = DDLAutor.SelectedItem.Text;

                // Agregar nueva entrada
                entries.Add(new MaterialAutorEntry
                {
                    MaterialID = int.Parse(DDLMatEdu.SelectedValue),
                    MaterialNombre = materialNombre,
                    AutorID = int.Parse(DDLAutor.SelectedValue),
                    AutorNombre = autorNombre,
                    Descripcion = TBDescription.Text
                });

                // Guardar en ViewState
                ViewState["Entries"] = entries;

                // Mostrar en GridView
                GVMaterialAutor.DataSource = entries;
                GVMaterialAutor.DataBind();

                // Limpiar los campos
                DDLMatEdu.SelectedIndex = 0;
                DDLAutor.SelectedIndex = 0;
                TBDescription.Text = "";
            }

        



        protected void GVMaterialAutor_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = GVMaterialAutor.SelectedIndex;

            if (rowIndex >= 0)
            {
                try
                {
                    // Material
                    string MaterialSeleccionado = HttpUtility.HtmlDecode(GVMaterialAutor.SelectedRow.Cells[1].Text.Trim());
                    Console.WriteLine("MaterialSeleccionado: " + MaterialSeleccionado); // Depuración

                    bool MaterialEncontrado = false;
                    foreach (ListItem item in DDLMatEdu.Items)
                    {
                        Console.WriteLine("DDLMatEdu Item Text: " + item.Text.Trim() + ", Value: " + item.Value); // Depuración
                        if (item.Text.Trim().Equals(MaterialSeleccionado, StringComparison.OrdinalIgnoreCase))
                        {
                            DDLMatEdu.SelectedValue = item.Value;
                            MaterialEncontrado = true;
                            break;
                        }
                    }
                    if (!MaterialEncontrado)
                    {
                        DDLMatEdu.SelectedIndex = 0;
                    }

                    // Autor
                    string AutorSeleccionado = HttpUtility.HtmlDecode(GVMaterialAutor.SelectedRow.Cells[2].Text.Trim());
                    Console.WriteLine("AutorSeleccionado: " + AutorSeleccionado); // Depuración

                    bool AutorEncontrado = false;
                    foreach (ListItem item in DDLAutor.Items)
                    {
                        Console.WriteLine("DDLAutor Item Text: " + item.Text.Trim() + ", Value: " + item.Value); // Depuración
                        if (item.Text.Trim().Equals(AutorSeleccionado, StringComparison.OrdinalIgnoreCase))
                        {
                            DDLAutor.SelectedValue = item.Value;
                            AutorEncontrado = true;
                            break;
                        }
                    }
                    if (!AutorEncontrado)
                    {
                        DDLAutor.SelectedIndex = 0;
                    }

                    TBDescription.Text = GVMaterialAutor.Rows[rowIndex].Cells[3].Text;
                    LblMessage.Text = "Material seleccionado correctamente.";
                    LblMessage.ForeColor = System.Drawing.Color.Green;
                }
                catch (Exception ex)
                {
                    // Manejar la excepción (mostrar un mensaje de error, registrar, etc.)
                    Console.WriteLine("Error al seleccionar: " + ex.Message);
                }
            }
        }
        private void clear()
        {
            HFMaterialAutorID.Value = "";
            DDLMatEdu.SelectedIndex = 0;
            DDLAutor.SelectedIndex = 0;
            TBDescription.Text = string.Empty;
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(HFMaterialAutorID.Value) && int.TryParse(HFMaterialAutorID.Value, out int id))
            {

                int Material = Convert.ToInt32(DDLMatEdu.SelectedValue);
                int Autor = Convert.ToInt32(DDLAutor.SelectedValue);
                string description = TBDescription.Text;

                bool executed = objMat.updateMaterialAutor(id, Material, Autor, description);
                if (executed)
                {
                    LblMessage.Text = "¡Material Autor actualizado exitosamente!";
                    clear();
                    showMaterialAutor();
                }
                else
                {
                    LblMessage.Text = "Error al actualizar el Material Autor.";
                }
            }
            else
            {
                LblMessage.Text = "Error: No se ha seleccionado un Material Autor válido para actualizar.";
            }


        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(HFMaterialAutorID.Value) && int.TryParse(HFMaterialAutorID.Value, out int id))
            {
                bool executed = objMat.deleteMaterialAutor(id);

                if (executed)
                {
                    LblMessage.Text = "¡Material Autor eliminada exitosamente!";
                    clear();
                    showMaterialAutor();
                }
                else
                {
                    LblMessage.Text = "Error al eliminar el material autor.";
                }
            }
            else
            {
                LblMessage.Text = "Error: No se ha seleccionado un Material Autor válida para eliminar.";
            }

        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            // Obtener el texto de búsqueda y convertirlo a minúsculas
            string searchText = TBSearch.Text.Trim().ToLower();

            // Obtener todos los materiales desde la lógica de negocio
            DataSet ds = objMat.showMaterialAutor();

            // Verificar si el DataSet tiene datos
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // Crear un DataView para aplicar el filtro
                DataView dv = ds.Tables[0].DefaultView;

                // Aplicar el filtro si hay texto de búsqueda
                if (!string.IsNullOrEmpty(searchText))
                {
                    dv.RowFilter = $"material_titulo LIKE '%{searchText}%' OR nombre_autor LIKE '%{searchText}%'";



                }

                // Enlazar el DataView al GridView
                GVMaterialAutor.DataSource = dv;
            }
            else
            {
                // Si no hay datos, enlazar un DataSet vacío al GridView
                GVMaterialAutor.DataSource = ds;
            }

            // Actualizar el GridView
            GVMaterialAutor.DataBind();
        }

        protected void BtnSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                int Material = Convert.ToInt32(DDLMatEdu.SelectedValue);
                int Autor = Convert.ToInt32(DDLAutor.SelectedValue);
                string description = TBDescription.Text;

                if (Material == 0 || Autor == 0 || string.IsNullOrEmpty(description))
                {


                    LblMessage.Text = "Debe ingresar todos los datos correctamente.";
                    LblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                bool executed = objMat.saveMaterialAutor(Material, Autor, description);
                if (executed)
                {
                    LblMessage.Text = "El material autor se guardó exitosamente!";
                    LblMessage.ForeColor = System.Drawing.Color.Green;
                    showMaterialAutor(); // Método para actualizar la lista
                    clear(); // Método para limpiar los inputs
                }
                else
                {
                    LblMessage.Text = "Error al guardar el material.";
                    LblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                LblMessage.Text = "Error inesperado: " + ex.Message;
                LblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
