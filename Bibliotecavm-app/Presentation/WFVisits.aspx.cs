
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using Logic;

namespace Presentation
{
    public partial class WFVisits : System.Web.UI.Page
    {
        VisitsLog objVis = new VisitsLog(); // Instancia para lógica de visitas
        UserLogic objUser = new UserLogic();
        MaterialEducativoLog objMat = new MaterialEducativoLog();

        private int _usu_id, _idVisits;
        private DateTime _fecha_ingreso;
        private TimeSpan _duracion;
        private string _dispositivo;
        private bool executed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Obtener el ID del usuario logueado
                _usu_id = GetLoggedUserId();

                // Mostrar el nombre del usuario logueado en un Label
                ShowLoggedInUserName();

                // Cargar las visitas del usuario logueado
                LoadUserVisits();
            }
        }

        // Obtener el ID del usuario logueado
        private int GetLoggedUserId()
        {
            return Convert.ToInt32(Session["UserID"]); // ⚠️ Ajusta esto según tu autenticación
        }

        // Mostrar el nombre del usuario logueado en un Label
        private void ShowLoggedInUserName()
        {
            if (Session["UserName"] != null)
            {
                LblUsuario.Text = Session["UserName"].ToString(); // Asignar el nombre al Label
            }
            else
            {
                LblUsuario.Text = "Usuario no identificado"; // Mensaje por defecto si no hay nombre
            }
        }

        // Cargar visitas del usuario logueado
        private void LoadUserVisits()
        {
            try
            {
                _usu_id = GetLoggedUserId();
                DataSet ds = objVis.GetVisitsByUser(_usu_id);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GVVisitas.DataSource = ds.Tables[0];
                    GVVisitas.DataBind();
                }
                else
                {
                    LblMsj.Text = "No hay visitas registradas para este usuario.";
                    GVVisitas.DataSource = null;
                    GVVisitas.DataBind();
                }
            }
            catch (Exception ex)
            {
                LblMsj.Text = "Error al cargar visitas: " + ex.Message;
            }
        }

        // Guardar una nueva visita
        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                _fecha_ingreso = DateTime.Parse(TxtFechaIngreso.Text);
                _duracion = TimeSpan.Parse(TxtDuracion.Text);
                _dispositivo = TxtDispositivo.Text.Trim();
                _usu_id = GetLoggedUserId();
                int matId = int.Parse(HdnMaterialId.Value); // Obtener el ID del material seleccionado desde el campo oculto

                executed = objVis.saveVisits(_fecha_ingreso, _duracion, _dispositivo, _usu_id, matId);

                if (executed)
                {
                    LblMsj.Text = "Visita guardada correctamente.";
                    LoadUserVisits();
                    ClearFields();
                }
                else
                {
                    LblMsj.Text = "Error al guardar la visita.";
                }
            }
            catch (Exception ex)
            {
                LblMsj.Text = "Error: " + ex.Message;
            }
        }

        // Actualizar una visita existente
        protected void BtnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                _idVisits = int.Parse(GVVisitas.SelectedRow.Cells[0].Text);
                _fecha_ingreso = DateTime.Parse(TxtFechaIngreso.Text);
                _duracion = TimeSpan.Parse(TxtDuracion.Text);
                _dispositivo = TxtDispositivo.Text.Trim();
                _usu_id = GetLoggedUserId();
                int matId = int.Parse(HdnMaterialId.Value); // Obtener el ID del material seleccionado desde el campo oculto

                executed = objVis.updateVisits(_idVisits, _fecha_ingreso, _duracion, _dispositivo, _usu_id, matId);

                if (executed)
                {
                    LblMsj.Text = "Visita actualizada correctamente.";
                    LoadUserVisits();
                    ClearFields();
                }
                else
                {
                    LblMsj.Text = "Error al actualizar la visita.";
                }
            }
            catch (Exception ex)
            {
                LblMsj.Text = "Error: " + ex.Message;
            }
        }

        // Eliminar una visita
        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                _idVisits = int.Parse(GVVisitas.SelectedRow.Cells[0].Text);
                _usu_id = GetLoggedUserId();

                executed = objVis.deleteVisits(_idVisits);

                if (executed)
                {
                    LblMsj.Text = "Visita eliminada correctamente.";
                    LoadUserVisits();
                    ClearFields();
                }
                else
                {
                    LblMsj.Text = "Error al eliminar la visita.";
                }
            }
            catch (Exception ex)
            {
                LblMsj.Text = "Error: " + ex.Message;
            }
        }

        // Seleccionar una visita en el GridView
        protected void GVVisitas_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TxtFechaIngreso.Text = GVVisitas.SelectedRow.Cells[1].Text;
                TxtDuracion.Text = GVVisitas.SelectedRow.Cells[2].Text;
                TxtDispositivo.Text = GVVisitas.SelectedRow.Cells[3].Text;
                HdnMaterialId.Value = GVVisitas.SelectedRow.Cells[4].Text; // Asignar el ID del material seleccionado al campo oculto
                TxtBuscarMaterial.Text = GVVisitas.SelectedRow.Cells[5].Text; // Mostrar el nombre del material en el campo de búsqueda
            }
            catch (Exception ex)
            {
                LblMsj.Text = "Error al seleccionar la visita: " + ex.Message;
            }
        }

        // Limpiar los campos de texto
        private void ClearFields()
        {
            TxtFechaIngreso.Text = "";
            TxtDuracion.Text = "";
            TxtDispositivo.Text = "";
            TxtBuscarMaterial.Text = ""; // Limpiar el campo de búsqueda
            HdnMaterialId.Value = ""; // Limpiar el campo oculto del ID del material
            HdnMaterialTitulo.Value = ""; // Limpiar el campo oculto del título del material
        }

        // Botón de limpiar
        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        // WebMethod para obtener materiales educativos (autocompletado)
        [System.Web.Services.WebMethod]
        public static List<MaterialEducativo> GetMateriales(string term)
        {
            MaterialEducativoLog objMat = new MaterialEducativoLog();
            DataSet ds = objMat.showMaterialEdu(); // Obtener todos los materiales

            List<MaterialEducativo> materiales = new List<MaterialEducativo>();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // Filtrar los materiales que coincidan con el término de búsqueda
                var resultados = ds.Tables[0].AsEnumerable()
                    .Where(row => row.Field<string>("mat_titulo").IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0)
                    .Select(row => new MaterialEducativo
                    {
                        mat_id = row.Field<int>("mat_id"),
                        mat_titulo = row.Field<string>("mat_titulo")
                    }).ToList();

                materiales = resultados;
            }

            return materiales;
        }

        // Clase auxiliar para representar un material educativo
        public class MaterialEducativo
        {
            public int mat_id { get; set; }
            public string mat_titulo { get; set; }
        }
    }
}

