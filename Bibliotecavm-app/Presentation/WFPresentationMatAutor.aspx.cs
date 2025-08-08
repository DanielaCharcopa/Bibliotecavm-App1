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
    public partial class WFPresentationMatAutor : System.Web.UI.Page
    {
        MaterialAutorLog objMat = new MaterialAutorLog();

        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (!IsPostBack)
            {
                CargarMaterialAutores();
            }
        }

        private DataTable ObtenerMaterialAutor()
        {
            return objMat.showMaterialAutorReport().Tables[0];
        }


        private void CargarMaterialAutores(string filtro = "")
        {
            var datos = ObtenerMaterialAutor();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                DataView dv = new DataView(datos);
                dv.RowFilter = $"materiales LIKE '%{filtro}%' OR nombre_autor LIKE '%{filtro}%'";
                GVMaterialAutor.DataSource = dv;
            }
            else
            {
                GVMaterialAutor.DataSource = datos;
            }

            GVMaterialAutor.DataBind();
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            string filtro = TBSearch.Text.Trim();
            CargarMaterialAutores(filtro);
        }
    }
}
