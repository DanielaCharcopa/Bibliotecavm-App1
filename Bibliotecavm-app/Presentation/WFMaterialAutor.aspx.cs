using Logic;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public class Datosformulario
    {
        public int idDropAutor { get; set; }
        public int idDropMaterial { get; set; }
        public int idMaterial { get; set; }
        public int idAutor { get; set; }
        public string descripcion { get; set; }
    }

    public partial class WFMaterialAutor : System.Web.UI.Page
    {
        MaterialAutorLog objMat = new MaterialAutorLog();
        MaterialEducativoLog objMatEdu = new MaterialEducativoLog();
        AuthorsLog objAut = new AuthorsLog();

        public static List<Datosformulario> datosformularios = new List<Datosformulario>();
        private int id;
        private bool executed;

        private int FormularioCount
        {
            get => ViewState["FormularioCount"] != null ? (int)ViewState["FormularioCount"] : 0;
            set => ViewState["FormularioCount"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["FormularioCount"] != null)
            {
                FormularioCount = (int)Session["FormularioCount"];

                for (int i = 1; i <= FormularioCount; i++)
                {
                    AgregarFormularioDinamico(i); // Función que agrega los controles al placeholder
                }
            }

            if (!IsPostBack)
            {
                CargarDDLEstaticos();
                showMaterialAutor();
            }
        }

        // Evento para manejar el cambio de página del GridView
        protected void GVMaterialAutor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVMaterialAutor.PageIndex = e.NewPageIndex;
            showMaterialAutor(); // Recarga los datos para la nueva página
        }

        private void CargarDDLEstaticos()
        {
            DDLMatEdu.DataSource = objMatEdu.showMaterialEduDDL();
            DDLMatEdu.DataValueField = "mat_id";
            DDLMatEdu.DataTextField = "mat_titulo";
            DDLMatEdu.DataBind();
            DDLMatEdu.Items.Insert(0, new ListItem("Seleccione un Material", "0"));

            DDLAutor.DataSource = objAut.showAuthorsDDL();
            DDLAutor.DataValueField = "au_id";
            DDLAutor.DataTextField = "nombre_completo";
            DDLAutor.DataBind();
            DDLAutor.Items.Insert(0, new ListItem("Seleccione un Autor", "0"));
        }

        private void showMaterialAutor()
        {
            GVMaterialAutor.DataSource = objMat.showMaterialAutor();
            GVMaterialAutor.DataBind();
        }

        protected void BtnAgregarFormulario_Click(object sender, EventArgs e)
        {
            FormularioCount++;
            AgregarFormularioDinamico(FormularioCount);

            Session["FormularioCount"] = FormularioCount; // ← ¡IMPORTANTE!
        }

        private void AgregarFormularioDinamico(int index)
        {
            Panel panel = new Panel { CssClass = "form-section d-flex align-items-start gap-3 flex-wrap" };

            DropDownList ddlMaterial = new DropDownList
            {
                ID = $"DDLMatEdu_{index}",
                CssClass = "form-select"
            };
            ddlMaterial.DataSource = objMatEdu.showMaterialEduDDL();
            ddlMaterial.DataValueField = "mat_id";
            ddlMaterial.DataTextField = "mat_titulo";
            ddlMaterial.DataBind();
            ddlMaterial.Items.Insert(0, new ListItem("Seleccione un Material", "0"));

            DropDownList ddlAutor = new DropDownList
            {
                ID = $"DDLAutor_{index}",
                CssClass = "form-select"
            };
            ddlAutor.DataSource = objAut.showAuthorsDDL();
            ddlAutor.DataValueField = "au_id";
            ddlAutor.DataTextField = "nombre_completo";
            ddlAutor.DataBind();
            ddlAutor.Items.Insert(0, new ListItem("Seleccione un Autor", "0"));

            TextBox tbDescripcion = new TextBox
            {
                ID = $"TBDescripcion_{index}",
                CssClass = "form-control"
            };

            panel.Controls.Add(new Literal { Text = "<div class='form-group'><label class='form-label'>Material Educativo</label>" });
            panel.Controls.Add(ddlMaterial);
            panel.Controls.Add(new Literal { Text = "</div>" });

            panel.Controls.Add(new Literal { Text = "<div class='form-group'><label class='form-label'>Autor</label>" });
            panel.Controls.Add(ddlAutor);
            panel.Controls.Add(new Literal { Text = "</div>" });

            panel.Controls.Add(new Literal { Text = "<div class='form-group'><label class='form-label'>Descripción</label>" });
            panel.Controls.Add(tbDescripcion);
            panel.Controls.Add(new Literal { Text = "</div><hr>" });

            phContenedor.Controls.Add(panel);


        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool allSaved = true;

                // 1. Guardar formulario ESTÁTICO
                if (int.TryParse(DDLMatEdu.SelectedValue, out int matEstatico) &&
                    int.TryParse(DDLAutor.SelectedValue, out int autEstatico))
                {
                    string descEstatico = TBDescription.Text;
                    if (matEstatico > 0 && autEstatico > 0)
                    {
                        if (string.IsNullOrWhiteSpace(descEstatico))
                        {
                            descEstatico = "N/A";
                        }

                        if (!objMat.saveMaterialAutor(matEstatico, autEstatico, descEstatico))
                        {
                            allSaved = false;
                        }
                    }
                    else
                    {
                        allSaved = false;
                    }
                }
                else
                {
                    allSaved = false;
                }

                // 2. Guardar formularios DINÁMICOS
                for (int i = 1; i <= FormularioCount; i++)
                {
                    DropDownList ddlMat = phContenedor.FindControl($"DDLMatEdu_{i}") as DropDownList;
                    DropDownList ddlAut = phContenedor.FindControl($"DDLAutor_{i}") as DropDownList;
                    TextBox tbDesc = phContenedor.FindControl($"TBDescripcion_{i}") as TextBox;

                    if (ddlMat != null && ddlAut != null && tbDesc != null)
                    {
                        if (int.TryParse(ddlMat.SelectedValue, out int mat) &&
                            int.TryParse(ddlAut.SelectedValue, out int aut))
                        {
                            string desc = tbDesc.Text;

                            if (mat > 0 && aut > 0)
                            {
                                if (string.IsNullOrWhiteSpace(desc))
                                {
                                    desc = "N/A";
                                }

                                if (!objMat.saveMaterialAutor(mat, aut, desc))
                                {
                                    allSaved = false;
                                }
                            }
                            else
                            {
                                allSaved = false;
                            }
                        }
                        else
                        {
                            allSaved = false;
                        }
                    }
                }

                // Mensaje final
                LblMessage.Text = allSaved
                    ? "¡Todos los registros fueron guardados correctamente!"
                    : "Hubo errores al guardar uno o más formularios.";
                LblMessage.ForeColor = allSaved ? System.Drawing.Color.Green : System.Drawing.Color.Red;

                if (allSaved)
                {
                    showMaterialAutor();

                    // Limpiar controles estáticos
                    DDLMatEdu.SelectedIndex = 0;
                    DDLAutor.SelectedIndex = 0;
                    TBDescription.Text = "";

                    // Limpiar controles dinámicos
                    phContenedor.Controls.Clear();

                    // Reiniciar contador y sesión
                    FormularioCount = 0;
                    ViewState["FormularioCount"] = 0;
                    Session["FormularioCount"] = 0;
                }
            }
            catch (Exception ex)
            {
                LblMessage.Text = "Error inesperado al guardar: " + ex.Message;
                LblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchText = TBSearch.Text.Trim().ToLower();
            DataSet ds = objMat.showMaterialAutor(); // o el método que uses para traer los datos

            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                DataView dv = dt.DefaultView;

                if (!string.IsNullOrEmpty(searchText))
                {
                    searchText = searchText.Replace("'", "''"); // Evitar errores con comillas
                    dv.RowFilter = $"material_titulo LIKE '%{searchText}%' OR nombre_autor LIKE '%{searchText}%' OR descripcion LIKE '%{searchText}%'";
                }

                GVMaterialAutor.DataSource = dv;
                GVMaterialAutor.DataBind();

                if (dv.Count > 0)
                {
                    LblSearchResult.Text = $"Resultados encontrados: {dv.Count}";
                    LblSearchResult.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    LblSearchResult.Text = "No se encontraron resultados.";
                    LblSearchResult.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                GVMaterialAutor.DataSource = null;
                GVMaterialAutor.DataBind();
                LblSearchResult.Text = "No se encontraron registros.";
                LblSearchResult.ForeColor = System.Drawing.Color.Red;
            }
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
                    LblMessage.Text = "Material Autor seleccionado correctamente. Puedes actualizar o eliminar.";
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
            TBDescription.Text = "";
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
            if (string.IsNullOrEmpty(HFMaterialAutorID.Value))
            {
                LblMessage.Text = "No se seleccionó un Material Autor para eliminar.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return;

            }

             id = Convert.ToInt32(HFMaterialAutorID.Value);

            bool executed = objMat.deleteMaterialAutor(id);
            
                 

                    if (executed)
                    {
                        LblMessage.Text = "¡Material-Autor eliminado exitosamente!";
                        LblMessage.ForeColor = System.Drawing.Color.Red;
                        clear();
                        showMaterialAutor();
                    }
                    else
                    {
                        LblMessage.Text = "Error al eliminar el Material-Autor.";
                        LblMessage.ForeColor = System.Drawing.Color.Red;
             
        
                    }
               
        }

        protected void BtnIrAPresentacion_Click(object sender, EventArgs e)
        {
            Response.Redirect("WFPresentationMatAutor.aspx");
        }


    }

}

