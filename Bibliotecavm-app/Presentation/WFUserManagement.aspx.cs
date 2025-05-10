using Logic;
using SimpleCrypto;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFUserManagement : System.Web.UI.Page
    {
        UserLogic objUser = new UserLogic();
        string nombre, apellido, correo, contrasena, salt, rol, estado;
        int userId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadUsers();
                CargarRoles();
                PanelEditMode.Visible = false;
            }
        }

        private void CargarRoles()
        {
            bool existeAdministrador = objUser.CheckAdminExists();
            DDLRole.Items.Clear();
            DDLRole.Items.Add(new ListItem("Seleccione un rol", ""));

            if (!existeAdministrador)
            {
                DDLRole.Items.Add(new ListItem("Administrador", "Administrador"));
            }

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
            if (!string.IsNullOrEmpty(HFUserId.Value))
            {
                LblMessage.Text = "⚠️ Está intentando guardar un usuario que ya existe. Por favor, use el botón 'Actualizar' para modificar este usuario o haga clic en 'Nuevo' para crear uno diferente.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (!ValidarCampos())
                return;

            estado = DDLEstado.SelectedValue;

            ICryptoService cryptoService = new PBKDF2();
            salt = cryptoService.GenerateSalt();
            string encryptedPassword = cryptoService.Compute(contrasena, salt);

            try
            {
                if (objUser.isEmailRegistered(correo))
                {
                    LblMessage.Text = $"❌ El correo electrónico {correo} ya está registrado. Si desea modificar este usuario, búsquelo en la tabla y selecciónelo para editar.";
                    LblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                bool success = objUser.saveUser(nombre, apellido, correo, encryptedPassword, salt, rol);

                if (success)
                {
                    LblMessage.Text = "✅ Usuario registrado exitosamente.";
                    LblMessage.ForeColor = System.Drawing.Color.Green;
                    ClearForm();
                    LoadUsers();
                    CargarRoles();
                }
                else
                {
                    LblMessage.Text = "❌ Error al guardar el usuario. Por favor, verifique los datos e intente nuevamente.";
                    LblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Duplicate entry") && ex.Message.Contains("usu_correo"))
                {
                    LblMessage.Text = $"❌ El correo electrónico {correo} ya está registrado en el sistema. No se pueden crear usuarios duplicados.";
                }
                else
                {
                    LblMessage.Text = $"❌ Error inesperado: {ex.Message}";
                }
                LblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(HFUserId.Value))
            {
                LblMessage.Text = "⚠️ No hay ningún usuario seleccionado para actualizar. Por favor, seleccione un usuario de la lista.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            userId = int.Parse(HFUserId.Value);

            nombre = HttpUtility.HtmlDecode(TBFirstName.Text.Trim());
            apellido = HttpUtility.HtmlDecode(TBLastName.Text.Trim());
            correo = HttpUtility.HtmlDecode(TBEmail.Text.Trim());
            contrasena = TBPassword.Text.Trim();
            rol = DDLRole.SelectedValue;
            estado = DDLEstado.SelectedValue;

            if (string.IsNullOrEmpty(nombre))
            {
                LblMessage.Text = "❌ El campo 'Nombre' es obligatorio.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (string.IsNullOrEmpty(apellido))
            {
                LblMessage.Text = "❌ El campo 'Apellido' es obligatorio.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (string.IsNullOrEmpty(correo))
            {
                LblMessage.Text = "❌ El campo 'Correo Electrónico' es obligatorio.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (!IsGmailEmail(correo))
            {
                LblMessage.Text = "❌ Por favor, ingrese un correo electrónico válido de Gmail (ejemplo@gmail.com).";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (string.IsNullOrEmpty(contrasena))
            {
                LblMessage.Text = "❌ El campo 'Contraseña' es obligatorio.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (string.IsNullOrEmpty(rol))
            {
                LblMessage.Text = "❌ Por favor seleccione un rol.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (objUser.isEmailRegistered(correo) && !IsCurrentUserEmail(correo))
            {
                LblMessage.Text = "❌ El correo electrónico ya está registrado en otro usuario. Por favor, use un correo diferente.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }

            try
            {
                ICryptoService cryptoService = new PBKDF2();
                salt = cryptoService.GenerateSalt();
                string encryptedPassword = cryptoService.Compute(contrasena, salt);

                bool success = objUser.updateUser(userId, nombre, apellido, correo, encryptedPassword, salt, rol, estado);

                if (success)
                {
                    LblMessage.Text = "✅ Usuario actualizado con éxito.";
                    LblMessage.ForeColor = System.Drawing.Color.Green;
                    ClearForm();
                    LoadUsers();
                    CargarRoles();
                    PanelEditMode.Visible = false;
                }
                else
                {
                    LblMessage.Text = "❌ Error al actualizar el usuario. Verifique que: \n" +
                                     "1. El correo no esté duplicado\n" +
                                     "2. Los datos sean válidos\n" +
                                     "3. La conexión a la base de datos esté activa";
                    LblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("El correo electrónico ya está registrado"))
                {
                    LblMessage.Text = "❌ El correo electrónico ya está registrado en otro usuario.";
                }
                else
                {
                    LblMessage.Text = "❌ Error inesperado al actualizar: " + ex.Message;
                }
                LblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void BtnNew_Click(object sender, EventArgs e)
        {
            ClearForm();
            LblMessage.Text = "📝 Listo para crear un nuevo usuario. Complete los datos y haga clic en Guardar.";
            LblMessage.ForeColor = System.Drawing.Color.Blue;
            PanelEditMode.Visible = false;
        }

        protected void LBCancelEdit_Click(object sender, EventArgs e)
        {
            ClearForm();
            LblMessage.Text = "✖️ Edición cancelada. Puede crear un nuevo usuario o seleccionar otro para editar.";
            LblMessage.ForeColor = System.Drawing.Color.Blue;
            PanelEditMode.Visible = false;
        }

        private bool ValidarCampos()
        {
            nombre = HttpUtility.HtmlDecode(TBFirstName.Text.Trim());
            apellido = HttpUtility.HtmlDecode(TBLastName.Text.Trim());
            correo = HttpUtility.HtmlDecode(TBEmail.Text.Trim());
            contrasena = TBPassword.Text.Trim();
            rol = DDLRole.SelectedValue;

            if (string.IsNullOrEmpty(nombre))
            {
                LblMessage.Text = "❌ El campo 'Nombre' es obligatorio.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return false;
            }

            if (string.IsNullOrEmpty(apellido))
            {
                LblMessage.Text = "❌ El campo 'Apellido' es obligatorio.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return false;
            }

            if (string.IsNullOrEmpty(correo) || !IsGmailEmail(correo))
            {
                LblMessage.Text = "❌ Por favor, ingrese un correo electrónico válido de Gmail (ejemplo@gmail.com).";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return false;
            }

            if (string.IsNullOrEmpty(contrasena) || contrasena.Length < 6)
            {
                LblMessage.Text = "❌ La contraseña debe tener al menos 6 caracteres.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return false;
            }

            if (string.IsNullOrEmpty(rol))
            {
                LblMessage.Text = "❌ Por favor seleccione un rol.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return false;
            }

            if (rol == "Administrador" && objUser.CheckAdminExists() && !IsCurrentUserAdmin())
            {
                LblMessage.Text = "❌ Ya existe un Administrador. Por favor, designe otro rol.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            HFUserId.Value = string.Empty;
            TBFirstName.Text = string.Empty;
            TBLastName.Text = string.Empty;
            TBEmail.Text = string.Empty;
            TBPassword.Text = string.Empty;
            DDLRole.SelectedIndex = 0;
            DDLEstado.SelectedValue = "Activo";
            TxtBuscarCorreo.Text = string.Empty;
            BtnSave.Text = "Guardar";
            BtnSave.ToolTip = "Guardar nuevo usuario";
        }

        protected void GVUsers_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                GridViewRow selectedRow = GVUsers.SelectedRow;
                HFUserId.Value = GVUsers.DataKeys[selectedRow.RowIndex].Value.ToString();

                TBFirstName.Text = HttpUtility.HtmlDecode(selectedRow.Cells[1].Text.Trim());
                TBLastName.Text = HttpUtility.HtmlDecode(selectedRow.Cells[2].Text.Trim());
                TBEmail.Text = HttpUtility.HtmlDecode(selectedRow.Cells[3].Text.Trim());
                TxtBuscarCorreo.Text = HttpUtility.HtmlDecode(selectedRow.Cells[3].Text.Trim());

                string selectedRole = HttpUtility.HtmlDecode(selectedRow.Cells[4].Text.Trim());
                if (!string.IsNullOrEmpty(selectedRole) && DDLRole.Items.FindByValue(selectedRole) != null)
                {
                    DDLRole.SelectedValue = selectedRole;
                }

                string userStatus = HttpUtility.HtmlDecode(selectedRow.Cells[5].Text.Trim());
                DDLEstado.SelectedValue = userStatus;

                BtnSave.Text = "Guardar como nuevo";
                BtnSave.ToolTip = "Crear un nuevo usuario con estos datos (el usuario actual no se modificará)";

                PanelEditMode.Visible = true;

                LblMessage.Text = $"✏️ Editando usuario: {TBFirstName.Text} {TBLastName.Text}. Use 'Actualizar' para guardar cambios.";
                LblMessage.ForeColor = System.Drawing.Color.Blue;
            }
            catch (Exception ex)
            {
                LblMessage.Text = "❌ Error al seleccionar el usuario: " + ex.Message;
                LblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private bool IsGmailEmail(string email)
        {
            return Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@gmail\.com$");
        }



        private bool IsCurrentUserEmail(string email)
        {
            if (string.IsNullOrEmpty(HFUserId.Value))
                return false;

            string currentEmail = HttpUtility.HtmlDecode(TBEmail.Text.Trim());
            return email.Equals(currentEmail, StringComparison.OrdinalIgnoreCase);
        }

        private bool IsCurrentUserAdmin()
        {
            if (string.IsNullOrEmpty(HFUserId.Value))
                return false;

            string currentRole = DDLRole.SelectedValue;
            return currentRole == "Administrador";
        }

        protected void GVUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GVUsers.PageIndex = e.NewPageIndex;
            LoadUsers();
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                string correoBusqueda = TxtBuscarCorreo.Text.Trim();

                if (string.IsNullOrWhiteSpace(correoBusqueda))
                {
                    lblmesaje2.Text = "❌ Por favor ingrese un correo electrónico para buscar.";
                    lblmesaje2.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (!IsValidEmail(correoBusqueda))
                {
                    lblmesaje2.Text = "❌ Por favor ingrese un correo electrónico válido.";
                    lblmesaje2.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "ShowLoading", "$('#loading').show();", true);

                DataSet ds = objUser.SearchUsersByEmail(correoBusqueda);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    GVUsers.DataSource = ds.Tables[0];
                    GVUsers.DataBind();

                    lblmesaje2.Text = $"✅ Se encontraron {ds.Tables[0].Rows.Count} usuario(s) con ese criterio.";
                    lblmesaje2.ForeColor = System.Drawing.Color.Green;

                    ScriptManager.RegisterStartupScript(this, GetType(), "HideNoResults", "$('#mensajeNoResultados').hide();", true);
                }
                else
                {
                    GVUsers.DataSource = null;
                    GVUsers.DataBind();

                    lblmesaje2.Text = "❌ No se encontraron usuarios con ese correo.";
                    lblmesaje2.ForeColor = System.Drawing.Color.Red;

                    ScriptManager.RegisterStartupScript(this, GetType(), "ShowNoResults",
                        "$('#mensajeNoResultados').text('No se encontraron resultados para: \"" + correoBusqueda + "\"').show();", true);
                }
            }
            catch (Exception ex)
            {
                lblmesaje2.Text = "❌ Error en la búsqueda: " + ex.Message;
                lblmesaje2.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "HideLoading", "$('#loading').hide();", true);
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        protected void BtnLimpiarBusqueda_Click(object sender, EventArgs e)
        {
            try
            {
                TxtBuscarCorreo.Text = string.Empty;
                LoadUsers();

                lblmesaje2.Text = "🔍 Mostrando todos los usuarios registrados.";
                lblmesaje2.ForeColor = System.Drawing.Color.Blue;

                ScriptManager.RegisterStartupScript(this, GetType(), "HideNoResults",
                    "$('#mensajeNoResultados').hide();", true);
            }
            catch (Exception ex)
            {
                lblmesaje2.Text = "❌ Error al limpiar la búsqueda: " + ex.Message;
                lblmesaje2.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}