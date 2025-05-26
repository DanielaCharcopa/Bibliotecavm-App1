using Logic;
using System;
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






        protected void BtnSaveAll_Click(object sender, EventArgs e)
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
            DataSet ds = objMat.showMaterialAutor();

            if (ds != null && ds.Tables.Count > 0)
            {
                DataView dv = ds.Tables[0].DefaultView;
                if (!string.IsNullOrEmpty(searchText))
                {
                    dv.RowFilter = $"material_titulo LIKE '%{searchText}%' OR nombre_autor LIKE '%{searchText}%'";
                }
                GVMaterialAutor.DataSource = dv;
                GVMaterialAutor.DataBind();
            }
        }

        protected void GVMaterialAutor_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = GVMaterialAutor.SelectedIndex;
            if (rowIndex >= 0)
            {
                string matTitulo = HttpUtility.HtmlDecode(GVMaterialAutor.SelectedRow.Cells[1].Text.Trim());
                string autorNombre = HttpUtility.HtmlDecode(GVMaterialAutor.SelectedRow.Cells[2].Text.Trim());

                DDLMatEdu.SelectedIndex = DDLMatEdu.Items.IndexOf(DDLMatEdu.Items.FindByText(matTitulo)) != -1
                    ? DDLMatEdu.Items.IndexOf(DDLMatEdu.Items.FindByText(matTitulo)) : 0;

                DDLAutor.SelectedIndex = DDLAutor.Items.IndexOf(DDLAutor.Items.FindByText(autorNombre)) != -1
                    ? DDLAutor.Items.IndexOf(DDLAutor.Items.FindByText(autorNombre)) : 0;

                TBDescription.Text = GVMaterialAutor.Rows[rowIndex].Cells[3].Text;
                HFMaterialAutorID.Value = GVMaterialAutor.DataKeys[rowIndex].Value.ToString();
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (int.TryParse(HFMaterialAutorID.Value, out int idMaterial))
            {
                bool allUpdated = true;

                // 1. Eliminar registros actuales del material educativo
                bool deleted = objMat.deleteMaterialAutor(idMaterial);
                if (!deleted)
                {
                    LblMessage.Text = "Error al limpiar registros anteriores.";
                    LblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // 2. Guardar el formulario estático
                int autEstatico = int.Parse(DDLAutor.SelectedValue);
                string descEstatico = TBDescription.Text;
                if (autEstatico > 0)
                {
                    if (string.IsNullOrWhiteSpace(descEstatico)) descEstatico = "N/A";
                    allUpdated &= objMat.saveMaterialAutor(idMaterial, autEstatico, descEstatico);
                }

                // 3. Guardar los formularios dinámicos
                for (int i = 1; i <= FormularioCount; i++)
                {
                    DropDownList ddlAut = (DropDownList)phContenedor.FindControl($"DDLAutor_{i}");
                    TextBox tbDesc = (TextBox)phContenedor.FindControl($"TBDescripcion_{i}");

                    if (ddlAut != null && tbDesc != null)
                    {
                        int aut = int.Parse(ddlAut.SelectedValue);
                        string desc = tbDesc.Text;

                        if (aut > 0)
                        {
                            if (string.IsNullOrWhiteSpace(desc)) desc = "N/A";
                            allUpdated &= objMat.saveMaterialAutor(idMaterial, aut, desc);
                        }
                    }
                }

                // 4. Mostrar resultado
                LblMessage.Text = allUpdated ? "¡Actualizado correctamente!" : "Error al actualizar uno o más registros.";
                LblMessage.ForeColor = allUpdated ? System.Drawing.Color.Green : System.Drawing.Color.Red;

                if (allUpdated)
                {
                    clear();
                    showMaterialAutor();
                }
            }
        }



       

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            if (int.TryParse(HFMaterialAutorID.Value, out int id))
            {
                bool deleted = objMat.deleteMaterialAutor(id);
                LblMessage.Text = deleted ? "¡Eliminado correctamente!" : "Error al eliminar.";
                LblMessage.ForeColor = deleted ? System.Drawing.Color.Green : System.Drawing.Color.Red;
                if (deleted)
                {
                    clear();
                    showMaterialAutor();
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
    }
}
