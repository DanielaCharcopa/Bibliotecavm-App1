using Logic;
using SimpleCrypto;
using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace Presentation
{
    public partial class WFUserRegistration : System.Web.UI.Page
    {
        // Instancia de la clase de lógica de usuarios
        UserLogic objUser = new UserLogic();
        string nombre, apellido, correo, contrasena, salt, rol, nivelEstudios;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            try
            {
                // Verificar si el correo ya está registrado
                if (objUser.isEmailRegistered(correo))
                {
                    LblMessage.Text = "❌ El correo electrónico ya está registrado. Por favor, use otro correo.";
                    LblMessage.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                // Generar el salt y encriptar la contraseña
                ICryptoService cryptoService = new PBKDF2();
                salt = cryptoService.GenerateSalt();
                string encryptedPassword = cryptoService.Compute(contrasena, salt);

                // Guardar usuario con los datos decodificados
                bool success = objUser.saveUser(nombre, apellido, correo, encryptedPassword, salt, rol, nivelEstudios);

                if (success)
                {
                    // Redirigir a Default.aspx con parámetro de éxito
                    Response.Redirect("~/Default.aspx?reg=success", false);
                }
                else
                {
                    LblMessage.Text = "❌ Error al guardar el usuario. Por favor, intente nuevamente.";
                    LblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                LblMessage.Text = $"❌ Error inesperado: {ex.Message}";
                LblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private bool ValidarCampos()
        {
            // Obtener y decodificar valores del formulario
            nombre = HttpUtility.HtmlDecode(TBFirstName.Text.Trim());
            apellido = HttpUtility.HtmlDecode(TBLastName.Text.Trim());
            correo = HttpUtility.HtmlDecode(TBEmail.Text.Trim());
            contrasena = TBPassword.Text.Trim();
            rol = DDLRole.SelectedValue;
            nivelEstudios = DDLEducationLevel.SelectedValue;

            // Resetear mensajes de error
            LblNombreMessage.Visible = false;
            LblApellidoMessage.Visible = false;
            LblCorreoMessage.Visible = false;
            LblPasswordMessage.Visible = false;
            LblMessage.Text = "";

            // Validar nombre
            if (string.IsNullOrEmpty(nombre))
            {
                LblNombreMessage.Text = "❌ El campo 'Nombre' es obligatorio.";
                LblNombreMessage.Visible = true;
                return false;
            }

            // Validar apellido
            if (string.IsNullOrEmpty(apellido))
            {
                LblApellidoMessage.Text = "❌ El campo 'Apellido' es obligatorio.";
                LblApellidoMessage.Visible = true;
                return false;
            }

            // Validar correo electrónico
            if (string.IsNullOrEmpty(correo))
            {
                LblCorreoMessage.Text = "❌ El campo 'Correo Electrónico' es obligatorio.";
                LblCorreoMessage.Visible = true;
                return false;
            }

            if (!IsGmailEmail(correo))
            {
                LblCorreoMessage.Text = "❌ Por favor, ingrese un correo electrónico válido de Gmail (ejemplo@gmail.com).";
                LblCorreoMessage.Visible = true;
                return false;
            }

            // Validar contraseña
            if (string.IsNullOrEmpty(contrasena))
            {
                LblPasswordMessage.Text = "❌ El campo 'Contraseña' es obligatorio.";
                LblPasswordMessage.Visible = true;
                return false;
            }

            if (contrasena.Length < 6)
            {
                LblPasswordMessage.Text = "❌ La contraseña debe tener al menos 6 caracteres.";
                LblPasswordMessage.Visible = true;
                return false;
            }

            // Validar rol
            if (string.IsNullOrEmpty(rol))
            {
                LblMessage.Text = "❌ Por favor seleccione un rol.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return false;
            }

            // Validar nivel educativo
            if (string.IsNullOrEmpty(nivelEstudios))
            {
                LblMessage.Text = "❌ Por favor seleccione un nivel educativo.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return false;
            }

            // Validar si ya existe administrador
            if (rol == "Administrador" && objUser.CheckAdminExists())
            {
                LblMessage.Text = "❌ Ya existe un Administrador. Por favor, designe otro rol.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
                return false;
            }

            return true;
        }

        // Método para validar si el correo es de Gmail
        private bool IsGmailEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            // Expresión regular mejorada para Gmail
            string pattern = @"^[a-zA-Z0-9]+(?:[._%+-][a-zA-Z0-9]+)*@gmail\.com$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        private void ClearForm()
        {
            // Limpiar los campos del formulario
            TBFirstName.Text = string.Empty;
            TBLastName.Text = string.Empty;
            TBEmail.Text = string.Empty;
            TBPassword.Text = string.Empty;
            DDLRole.SelectedIndex = 0;
            DDLEducationLevel.SelectedIndex = 0;

            // Limpiar mensajes
            LblNombreMessage.Visible = false;
            LblApellidoMessage.Visible = false;
            LblCorreoMessage.Visible = false;
            LblPasswordMessage.Visible = false;
            LblMessage.Text = string.Empty;
        }
    }
}