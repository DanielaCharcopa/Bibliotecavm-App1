using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Logic;

namespace Presentation
{
    public partial class WFUserManagement : System.Web.UI.Page
    {
        // Instancia de la clase de lógica de usuarios
        UserLogic objUser = new UserLogic();
        string nombre, apellido, correo, contrasena, salt, rol, nivelEstudios;
        int userId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUsers(); // Carga inicial de usuarios
            }
        }

        private void LoadUsers()
        {
            DataSet ds = objUser.showUsers();
            if (ds != null && ds.Tables.Count > 0)
            {
                GVUsers.DataSource = ds.Tables[0];
                GVUsers.DataBind();
            }
            else
            {
                GVUsers.DataSource = null;
                GVUsers.DataBind();
            }
        }


        protected void BtnSave_Click(object sender, EventArgs e)
        {
            // Decodificar los datos ingresados antes de procesarlos
            nombre = HttpUtility.HtmlDecode(TBFirstName.Text.Trim());
            apellido = HttpUtility.HtmlDecode(TBLastName.Text.Trim());
            correo = HttpUtility.HtmlDecode(TBEmail.Text.Trim());
            contrasena = TBPassword.Text.Trim();
            salt = TBSalt.Text.Trim();
            rol = DDLRole.SelectedValue;
            nivelEstudios = DDLEducationLevel.SelectedValue;

            if (string.IsNullOrEmpty(rol) || string.IsNullOrEmpty(nivelEstudios))
            {
                LblMessage.Text = "Por favor seleccione un rol y un nivel educativo.";
                return;
            }

            // Verificar si ya existe un administrador
            if (rol == "Administrador" && objUser.CheckAdminExists())
            {
                LblMessage.Text = "Ya existe un Administrador. Por favor, designe otro rol o elimine el actual administrador.";
                return;
            }

            // Guardar usuario con los datos decodificados
            bool success = objUser.saveUser(nombre, apellido, correo, contrasena, salt, rol, nivelEstudios);

            LblMessage.Text = success ? "Usuario guardado exitosamente." : "Error al guardar el usuario.";
            ClearForm();
            LoadUsers();
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            userId = int.Parse(HFUserId.Value);
            nombre = TBFirstName.Text.Trim();
            apellido = TBLastName.Text.Trim();
            correo = TBEmail.Text.Trim();
            contrasena = TBPassword.Text.Trim();
            salt = TBSalt.Text.Trim();
            rol = DDLRole.SelectedValue;
            nivelEstudios = DDLEducationLevel.SelectedValue;

            if (string.IsNullOrEmpty(rol) || string.IsNullOrEmpty(nivelEstudios))
            {
                LblMessage.Text = "Por favor seleccione un rol y un nivel educativo.";
                return;
            }

            // Verificar si ya existe un administrador (excepto si el usuario actual es el administrador)
            if (rol == "Administrador" && objUser.CheckAdminExists() && rol != "Administrador del usuario actual")
            {
                LblMessage.Text = "Ya existe un Administrador. Por favor, designe otro rol o elimine el actual administrador.";
                return;
            }

            bool success = objUser.updateUser(userId, nombre, apellido, correo, contrasena, salt, rol, nivelEstudios);
            LblMessage.Text = success ? "Usuario actualizado con éxito." : "Error al actualizar el usuario.";
            ClearForm();
            LoadUsers();
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            // Eliminar un usuario
            userId = int.Parse(HFUserId.Value);

            bool success = objUser.deleteUser(userId);
            LblMessage.Text = success ? "Usuario eliminado con éxito." : "Error al eliminar el usuario.";

            ClearForm();
            LoadUsers();
        }
        private void ClearForm()
        {
            // Limpiar los campos del formulario
            HFUserId.Value = string.Empty;
            TBFirstName.Text = string.Empty;
            TBLastName.Text = string.Empty;
            TBEmail.Text = string.Empty;
            TBPassword.Text = string.Empty;
            TBSalt.Text = string.Empty;
            DDLRole.SelectedIndex = 0;
            DDLEducationLevel.SelectedIndex = 0;
            LblMessage.Text = string.Empty;
        }

        protected void GVUsers_SelectedIndexChanged1(object sender, EventArgs e)
        {
            // Obtener la fila seleccionada
            GridViewRow selectedRow = GVUsers.SelectedRow;

            // Obtener el ID del usuario desde DataKeyNames
            HFUserId.Value = GVUsers.DataKeys[selectedRow.RowIndex].Value.ToString();

            // Decodificar valores antes de asignarlos
            TBFirstName.Text = HttpUtility.HtmlDecode(selectedRow.Cells[1].Text.Trim()); // Nombre
            TBLastName.Text = HttpUtility.HtmlDecode(selectedRow.Cells[2].Text.Trim());  // Apellido
            TBEmail.Text = HttpUtility.HtmlDecode(selectedRow.Cells[3].Text.Trim());     // Correo Electrónico

            // Validar y asignar el Rol
            string selectedRole = HttpUtility.HtmlDecode(selectedRow.Cells[4].Text.Trim());
            if (!string.IsNullOrEmpty(selectedRole) && DDLRole.Items.FindByValue(selectedRole) != null)
            {
                DDLRole.SelectedValue = selectedRole;
            }
            else
            {
                DDLRole.SelectedIndex = 0; // Seleccionar "Seleccione un rol" si no coincide
            }

            // Validar y asignar el Nivel Educativo
            string selectedEducationLevel = HttpUtility.HtmlDecode(selectedRow.Cells[5].Text.Trim());
            if (!string.IsNullOrEmpty(selectedEducationLevel) && DDLEducationLevel.Items.FindByValue(selectedEducationLevel) != null)
            {
                DDLEducationLevel.SelectedValue = selectedEducationLevel;
            }
            else
            {
                DDLEducationLevel.SelectedIndex = 0; // Seleccionar "Seleccione un nivel" si no coincide
            }

            // Mensaje de confirmación (opcional)
            LblMessage.Text = $"Usuario {TBFirstName.Text} seleccionado para edición.";
        }
        protected void GVUsers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    e.Row.Cells[i].Text = HttpUtility.HtmlDecode(e.Row.Cells[i].Text);
                }
            }
        }

    }
}
