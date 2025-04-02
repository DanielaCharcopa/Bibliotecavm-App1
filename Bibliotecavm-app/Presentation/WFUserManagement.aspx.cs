using Logic;
using SimpleCrypto;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

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
                CargarRoles(); // Cargar roles dinámicamente
            }
        }

        private void CargarRoles()
        {
            // Verificar si ya existe un administrador
            bool existeAdministrador = objUser.CheckAdminExists();

            // Limpiar el DropDownList de roles
            DDLRole.Items.Clear();

            // Agregar la opción por defecto
            DDLRole.Items.Add(new ListItem("Seleccione un rol", ""));

            // Agregar roles según la existencia de un administrador
            if (!existeAdministrador)
            {
                DDLRole.Items.Add(new ListItem("Administrador", "Administrador"));
            }

            // Agregar los demás roles
            DDLRole.Items.Add(new ListItem("Docente", "Docente"));
            DDLRole.Items.Add(new ListItem("Estudiante", "Estudiante"));
        }

        private void LoadUsers()
        {
            DataSet ds = objUser.showUsers();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                GVUsers.DataSource = ds.Tables[0];
                GVUsers.DataBind();

                // Configurar el mensaje de paginación
                int totalRecords = ds.Tables[0].Rows.Count;
                int pageSize = GVUsers.PageSize;
                int currentPage = GVUsers.PageIndex + 1;
                int totalPages = (int)Math.Ceiling((double)totalRecords / pageSize);

                lblMesage.Text = $"Mostrando página {currentPage} de {totalPages} - Total de usuarios: {totalRecords}";
                lblMesage.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                GVUsers.DataSource = null;
                GVUsers.DataBind();
                lblMesage.Text = "No se encontraron usuarios registrados.";
                lblMesage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            // Obtener los valores del formulario
            nombre = HttpUtility.HtmlDecode(TBFirstName.Text.Trim());
            apellido = HttpUtility.HtmlDecode(TBLastName.Text.Trim());
            correo = HttpUtility.HtmlDecode(TBEmail.Text.Trim());
            contrasena = TBPassword.Text.Trim();
            rol = DDLRole.SelectedValue;
            nivelEstudios = DDLEducationLevel.SelectedValue;

            // Validar campos obligatorios
            if (string.IsNullOrEmpty(nombre))
            {
                LblMessage.Text = "El campo 'Nombre' es obligatorio.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (string.IsNullOrEmpty(apellido))
            {
                LblMessage.Text = "El campo 'Apellido' es obligatorio.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (string.IsNullOrEmpty(correo))
            {
                LblMessage.Text = "El campo 'Correo Electrónico' es obligatorio.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Validar que el correo sea de Gmail
            if (!IsGmailEmail(correo))
            {
                LblMessage.Text = "Por favor, ingrese un correo electrónico válido de Gmail (ejemplo@gmail.com).";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (string.IsNullOrEmpty(contrasena))
            {
                LblMessage.Text = "El campo 'Contraseña' es obligatorio.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (string.IsNullOrEmpty(rol) || string.IsNullOrEmpty(nivelEstudios))
            {
                LblMessage.Text = "Por favor seleccione un rol y un nivel educativo.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Verificar si ya existe un administrador
            if (rol == "Administrador" && objUser.CheckAdminExists())
            {
                LblMessage.Text = "Ya existe un Administrador. Por favor, designe otro rol o elimine el actual administrador.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Verificar si el correo ya está registrado
            if (objUser.isEmailRegistered(correo))
            {
                LblMessage.Text = "El correo electrónico ya está registrado. Por favor, use otro correo.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Generar el salt y encriptar la contraseña
            ICryptoService cryptoService = new PBKDF2(); // Asume que tienes una clase PBKDF2 para encriptación
            salt = cryptoService.GenerateSalt();
            string encryptedPassword = cryptoService.Compute(contrasena, salt);

            // Guardar usuario con los datos decodificados
            bool success = objUser.saveUser(nombre, apellido, correo, encryptedPassword, salt, rol, nivelEstudios);

            if (success)
            {
                LblMessage.Text = "Usuario guardado exitosamente.";
                LblMessage.ForeColor = System.Drawing.Color.Green;
                ClearForm(); // Limpiar el formulario después de guardar
                LoadUsers(); // Recargar la lista de usuarios
                CargarRoles(); // Recargar roles dinámicamente
            }
            else
            {
                LblMessage.Text = "Error al guardar el usuario.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            // Obtener el ID del usuario
            userId = int.Parse(HFUserId.Value);

            // Obtener los valores del formulario
            nombre = HttpUtility.HtmlDecode(TBFirstName.Text.Trim());
            apellido = HttpUtility.HtmlDecode(TBLastName.Text.Trim());
            correo = HttpUtility.HtmlDecode(TBEmail.Text.Trim());
            contrasena = TBPassword.Text.Trim();
            rol = DDLRole.SelectedValue;
            nivelEstudios = DDLEducationLevel.SelectedValue;

            // Validar campos obligatorios
            if (string.IsNullOrEmpty(nombre))
            {
                LblMessage.Text = "El campo 'Nombre' es obligatorio.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (string.IsNullOrEmpty(apellido))
            {
                LblMessage.Text = "El campo 'Apellido' es obligatorio.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (string.IsNullOrEmpty(correo))
            {
                LblMessage.Text = "El campo 'Correo Electrónico' es obligatorio.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Validar que el correo sea de Gmail
            if (!IsGmailEmail(correo))
            {
                LblMessage.Text = "Por favor, ingrese un correo electrónico válido de Gmail (ejemplo@gmail.com).";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (string.IsNullOrEmpty(contrasena))
            {
                LblMessage.Text = "El campo 'Contraseña' es obligatorio.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (string.IsNullOrEmpty(rol) || string.IsNullOrEmpty(nivelEstudios))
            {
                LblMessage.Text = "Por favor seleccione un rol y un nivel educativo.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Verificar si el correo ya está registrado en otro usuario
            if (objUser.isEmailRegistered(correo) && !IsCurrentUserEmail(correo))
            {
                LblMessage.Text = "El correo electrónico ya está registrado en otro usuario.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            // Generar el salt y encriptar la contraseña
            ICryptoService cryptoService = new PBKDF2(); // Asume que tienes una clase PBKDF2 para encriptación
            salt = cryptoService.GenerateSalt();
            string encryptedPassword = cryptoService.Compute(contrasena, salt);

            // Actualizar el usuario con los datos decodificados
            bool success = objUser.updateUser(userId, nombre, apellido, correo, encryptedPassword, salt, rol, nivelEstudios);

            if (success)
            {
                LblMessage.Text = "Usuario actualizado con éxito.";
                LblMessage.ForeColor = System.Drawing.Color.Green;
                ClearForm(); // Limpiar el formulario después de actualizar
                LoadUsers(); // Recargar la lista de usuarios
                CargarRoles(); // Recargar roles dinámicamente
            }
            else
            {
                LblMessage.Text = "Error al actualizar el usuario.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            // Eliminar un usuario
            userId = int.Parse(HFUserId.Value);

            bool success = objUser.deleteUser(userId);
            LblMessage.Text = success ? "Usuario eliminado con éxito." : "Error al eliminar el usuario.";
            LblMessage.ForeColor = success ? System.Drawing.Color.Green : System.Drawing.Color.Red;

            ClearForm();
            LoadUsers(); // Recargar la lista de usuarios
            CargarRoles(); // Recargar roles dinámicamente
        }

        private void ClearForm()
        {
            // Limpiar los campos del formulario
            HFUserId.Value = string.Empty;
            TBFirstName.Text = string.Empty;
            TBLastName.Text = string.Empty;
            TBEmail.Text = string.Empty;
            TBPassword.Text = string.Empty;
            DDLRole.SelectedIndex = 0;
            DDLEducationLevel.SelectedIndex = 0;
            TxtBuscarCorreo.Text = string.Empty; // Limpiar el campo de búsqueda
            LblMessage.Text = string.Empty;
        }

        protected void GVUsers_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                // Obtener la fila seleccionada
                GridViewRow selectedRow = GVUsers.SelectedRow;

                // Obtener el ID del usuario desde DataKeyNames
                HFUserId.Value = GVUsers.DataKeys[selectedRow.RowIndex].Value.ToString();

                // Decodificar valores antes de asignarlos
                TBFirstName.Text = HttpUtility.HtmlDecode(selectedRow.Cells[1].Text.Trim()); // Nombre
                TBLastName.Text = HttpUtility.HtmlDecode(selectedRow.Cells[2].Text.Trim());  // Apellido
                TBEmail.Text = HttpUtility.HtmlDecode(selectedRow.Cells[3].Text.Trim());     // Correo Electrónico

                // Mostrar el correo en el campo de búsqueda
                TxtBuscarCorreo.Text = HttpUtility.HtmlDecode(selectedRow.Cells[3].Text.Trim());

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
            catch (Exception ex)
            {
                LblMessage.Text = "Error al seleccionar el usuario: " + ex.Message;
            }
        }

        // Método para validar si el correo es de Gmail
        private bool IsGmailEmail(string email)
        {
            // Expresión regular para validar correos de Gmail
            string pattern = @"^[a-zA-Z0-9._%+-]+@gmail\.com$";
            return Regex.IsMatch(email, pattern);
        }

        // Método para verificar si el correo pertenece al usuario actual
        private bool IsCurrentUserEmail(string email)
        {
            if (string.IsNullOrEmpty(HFUserId.Value))
                return false;

            // Obtener el correo del usuario actual
            string currentEmail = HttpUtility.HtmlDecode(TBEmail.Text.Trim());
            return email.Equals(currentEmail, StringComparison.OrdinalIgnoreCase);
        }
        protected void GVUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVUsers.PageIndex = e.NewPageIndex;
            LoadUsers(); // Vuelve a cargar los datos con la nueva página
        }
        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            string correoBusqueda = TxtBuscarCorreo.Text.Trim();

            if (!string.IsNullOrEmpty(correoBusqueda))
            {
                DataSet ds = objUser.SearchUsersByEmail(correoBusqueda);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GVUsers.DataSource = ds.Tables[0];
                    GVUsers.DataBind();
                    lblmesaje2.Text = $"Se encontraron {ds.Tables[0].Rows.Count} resultados.";
                    lblmesaje2.ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    GVUsers.DataSource = null;
                    GVUsers.DataBind();
                    lblmesaje2.Text = "No se encontraron usuarios con ese correo.";
                    lblmesaje2.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                LoadUsers(); // Si el campo está vacío, cargar todos los usuarios
            }
        }

        protected void BtnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            TxtBuscarCorreo.Text = string.Empty;
            LoadUsers(); // Cargar todos los usuarios
            lblmesaje2.Text = "Mostrando todos los usuarios.";
            lblmesaje2.ForeColor = System.Drawing.Color.Blue;
        }
    }

}