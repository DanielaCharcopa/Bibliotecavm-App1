using Logic;
using System;
using System.Data;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text;


namespace Presentation

{
    public partial class WFMatEducativo : System.Web.UI.Page
    {
        // Instancias de clases para interactuar con la lógica de negocio.
        MaterialEducativoLog objMatEdu = new MaterialEducativoLog();
        PublishersLog objPub = new PublishersLog();
        CategoryLog objCat = new CategoryLog();


        private int _idMaterial;
        //private int _editorialId, _categoriaId;
        //private string _titulo, _urlDescarga, _keywords, _formato;
        //private int _anoPublicacion;
        //private decimal _precio;
        private bool executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Desactivar la validación no intrusiva
            this.UnobtrusiveValidationMode = System.Web.UI.UnobtrusiveValidationMode.None;
            if (!Page.IsPostBack)
            {
                showMaterialEdu();
                showEditorialDDL();  // Cargar editoriales
                ShowCategoriesDDL();
                LoadFormatoDDL(); // Cargar los valores del ENUM en el DropDownList
            }
        }

        // Mostrar las categorías en DropDownList 
        private void ShowCategoriesDDL()
        {
            DDLCategories.DataSource = objCat.showCategoryDDL();
            DDLCategories.DataValueField = "cat_id";
            DDLCategories.DataTextField = "cat_nombre";
            DDLCategories.DataBind();
            DDLCategories.Items.Insert(0, new ListItem("Seleccione la categoria", "0"));
        }

        // Mostrar editoriales en DropDownList
        private void showEditorialDDL()
        {
            DDLEditorial.DataSource = objPub.showEditorialsDDL();
            DDLEditorial.DataValueField = "edi_id";
            DDLEditorial.DataTextField = "edi_nombre";
            DDLEditorial.DataBind();
            DDLEditorial.Items.Insert(0, new ListItem("Seleccione la editorial", "0"));
        }
        // Cargar formatos del ENUM en DropDownList
        private void LoadFormatoDDL()
        {
            DDLFormato.Items.Clear();
            DDLFormato.Items.Add(new ListItem("Seleccione un formato", "0"));
            DDLFormato.Items.Add(new ListItem("PDF", "PDF"));
            DDLFormato.Items.Add(new ListItem("Epub", "Epub"));
            DDLFormato.Items.Add(new ListItem("Video", "Video"));
            DDLFormato.Items.Add(new ListItem("Audio", "Audio"));
            DDLFormato.Items.Add(new ListItem("Otro", "Otro"));
        }

        // Mostrar todo el material educativo en GridView
        private void showMaterialEdu()
        {
            DataSet ds = objMatEdu.showMaterialEdu();
            GVMaterial.DataSource = ds;
            GVMaterial.DataBind();
        }

        // Limpiar campos
        private void clear()
        {
            HFMaterialID.Value = "";
            TBTitulo.Text = "";
            TBUrl.Text = "";
            TBAnopublicado.Text = "";
            TBPrecio.Text = "";
            TBKeywords.Text = "";  // Campo para palabras clave
            DDLFormato.SelectedIndex = 0;   // Reiniciar DropDownList de formato
            DDLCategories.SelectedIndex = 0;
            DDLEditorial.SelectedIndex = 0;
        }

        // Guardar un nuevo material educativo
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Capturar valores desde la interfaz
                string titulo = HttpUtility.HtmlDecode(TBTitulo.Text.Trim());
                int anoPublicacion = Convert.ToInt32(TBAnopublicado.Text.Trim());
                string urlDescarga = TBUrl.Text.Trim();
                decimal precio = Convert.ToDecimal(TBPrecio.Text.Trim());
                string keywords = TBKeywords.Text.Trim();
                string formato = DDLFormato.SelectedValue;
                int editorialId = Convert.ToInt32(DDLEditorial.SelectedValue);
                int categoriaId = Convert.ToInt32(DDLCategories.SelectedValue);


                // Validar que todos los campos estén llenos correctamente
                if (string.IsNullOrEmpty(titulo) || anoPublicacion <= 0 ||
                    string.IsNullOrEmpty(urlDescarga) || precio <= 0 || string.IsNullOrEmpty(keywords) ||
                    DDLFormato.SelectedIndex == 0 || editorialId == 0 || categoriaId == 0)
                {
                    LblMsj.Text = "Debe ingresar todos los datos correctamente.";
                    LblMsj.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // Guardar material en la base de datos
                bool executed = objMatEdu.saveMaterialEducativo(titulo, anoPublicacion, urlDescarga, precio, keywords, formato, editorialId, categoriaId);

                // Mostrar mensaje según el resultado
                if (executed)
                {
                    LblMsj.Text = "El material educativo se guardó exitosamente!";
                    LblMsj.ForeColor = System.Drawing.Color.Green;
                    showMaterialEdu(); // Método para actualizar la lista
                    clear(); // Método para limpiar los inputs
                }
                else
                {
                    LblMsj.Text = "Error al guardar el material.";
                    LblMsj.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                LblMsj.Text = "Error inesperado: " + ex.Message;
                LblMsj.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Actualizar un material educativo existente
        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado un material para actualizar
            if (!string.IsNullOrEmpty(HFMaterialID.Value) && int.TryParse(HFMaterialID.Value, out int id))
            {
                string titulo = HttpUtility.HtmlDecode(TBTitulo.Text.Trim());
                string urlDescarga = TBUrl.Text.Trim();
                decimal precio = Convert.ToDecimal(TBPrecio.Text.Trim());
                string keywords = TBKeywords.Text.Trim();
                string formato = DDLFormato.SelectedValue;
                int editorialId = Convert.ToInt32(DDLEditorial.SelectedValue);
                int categoriaId = Convert.ToInt32(DDLCategories.SelectedValue);

                bool executed = objMatEdu.updateMaterialEducativo(id, titulo, Convert.ToInt32(TBAnopublicado.Text.Trim()),
                    urlDescarga, precio, keywords, formato, editorialId, categoriaId);

                if (executed)
                {
                    LblMsj.Text = "¡Material Educativo actualizado exitosamente!";
                    clear();
                    showMaterialEdu();
                }
                else
                {
                    LblMsj.Text = "Error al actualizar el Material Educativo.";
                }
            }
            else
            {
                LblMsj.Text = "Error: No se ha seleccionado un Material Educativo válido para actualizar.";
            }


        }

        // Evento para eliminar un material educativo
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            // Verificar si se ha seleccionado un material para eliminar
            if (string.IsNullOrEmpty(HFMaterialID.Value))
            {
                LblMsj.Text = "No se seleccionó un material para eliminar.";
                LblMsj.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Obtener el ID del material
            _idMaterial = Convert.ToInt32(HFMaterialID.Value);

            // Llamar a la lógica de negocio para eliminar el material
            executed = objMatEdu.deleteMaterialEducativo(_idMaterial);

            // Mostrar mensaje de éxito o error
            if (executed)
            {
                LblMsj.Text = "¡Material eliminado exitosamente!";
                LblMsj.ForeColor = System.Drawing.Color.Green;
                showMaterialEdu();
                clear();
            }
            else
            {
                LblMsj.Text = "¡Error al eliminar el material! Verifique los datos ingresados.";
                LblMsj.ForeColor = System.Drawing.Color.Red;

            }


        }

        // Seleccionar fila en GridView
        protected void GVMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowIndex = GVMaterial.SelectedIndex;

            if (rowIndex >= 0) // Verifica que se haya seleccionado una fila válida
            {
                // Obtener el ID del material seleccionado
                int matId = Convert.ToInt32(GVMaterial.DataKeys[rowIndex].Value);
                HFMaterialID.Value = matId.ToString(); // Almacenar el ID en el HiddenField

                // Cargar los datos del material seleccionado en los controles del formulario
                TBTitulo.Text = HttpUtility.HtmlDecode(GVMaterial.SelectedRow.Cells[1].Text.Trim()); // Título
                TBAnopublicado.Text = GVMaterial.SelectedRow.Cells[2].Text; // Año de publicación
                TBUrl.Text = GVMaterial.SelectedRow.Cells[3].Text; // URL de descarga
                TBPrecio.Text = GVMaterial.SelectedRow.Cells[4].Text; // Precio
                TBKeywords.Text = HttpUtility.HtmlDecode(GVMaterial.SelectedRow.Cells[5].Text.Trim()); // Palabras clave

                // Formato
                string formatoSeleccionado = GVMaterial.SelectedRow.Cells[6].Text;
                if (DDLFormato.Items.FindByValue(formatoSeleccionado) != null)
                {
                    DDLFormato.SelectedValue = formatoSeleccionado;
                }
                else
                {
                    DDLFormato.SelectedIndex = 0; // Selecciona el valor por defecto si no se encuentra
                }

                // Editorial
                string editorialSeleccionada = GVMaterial.SelectedRow.Cells[7].Text.Trim(); // Nombre de la editorial
                bool editorialEncontrada = false;
                foreach (ListItem item in DDLEditorial.Items)
                {
                    if (item.Text.Trim().Equals(editorialSeleccionada, StringComparison.OrdinalIgnoreCase))
                    {
                        DDLEditorial.SelectedValue = item.Value;
                        editorialEncontrada = true;
                        break;
                    }
                }
                if (!editorialEncontrada)
                {
                    DDLEditorial.SelectedIndex = 0; // Selecciona el valor por defecto si no se encuentra
                }

                // Categoria 

                string categoriaSeleccionada = HttpUtility.HtmlDecode(GVMaterial.SelectedRow.Cells[8].Text.Trim()); // Nombre de la categoría
                bool categoriaEncontrada = false;
                foreach (ListItem item in DDLCategories.Items)
                {
                    if (item.Text.Trim().Equals(categoriaSeleccionada, StringComparison.OrdinalIgnoreCase))
                    {
                        DDLCategories.SelectedValue = item.Value;
                        categoriaEncontrada = true;
                        break;
                    }
                }
                if (!categoriaEncontrada)
                {
                    DDLCategories.SelectedIndex = 0; // Selecciona el valor por defecto si no se encuentra
                }

                // Mostrar mensaje de éxito
                LblMsj.Text = "Material Educativo seleccionado correctamente. Puedes actualizar o eliminar.";
                LblMsj.ForeColor = System.Drawing.Color.Green;




            

            }
        }


        // Evento para paginación
        protected void GVMaterial_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVMaterial.PageIndex = e.NewPageIndex;
            showMaterialEdu(); // este método debe volver a cargar los datos
        }

        // Evento para buscar Editoriales
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchText = TBSearch.Text.Trim().ToLower();

            DataSet ds = objMatEdu.showMaterialEdu();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[0];
                DataView dv = dt.DefaultView;

                if (!string.IsNullOrEmpty(searchText))
                {
                    // Escapar comillas simples para evitar errores
                    searchText = searchText.Replace("'", "''");
                    dv.RowFilter = $"mat_titulo LIKE '%{searchText}%' OR mat_formato LIKE '%{searchText}%'";

                }

                GVMaterial.DataSource = dv;
                GVMaterial.DataBind();

                if (dv.Count > 0)
                {
                    LblSearchResult.Text = $"Registros Encontrados: {dv.Count}";
                    LblSearchResult.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    LblSearchResult.Text = "No Se Encontraron Materiales Educativos Registrados.";
                    LblSearchResult.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                GVMaterial.DataSource = null;
                GVMaterial.DataBind();
                LblSearchResult.Text = "No Se Encontraron Materiales Educativos Registrados.";
                LblSearchResult.ForeColor = System.Drawing.Color.Red;
            }
        }


    }
}