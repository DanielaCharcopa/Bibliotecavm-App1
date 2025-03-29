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
            // Decodificar los datos ingresados antes de procesarlos
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
            }
            else
            {
                LblMessage.Text = "Error al guardar el usuario.";
                LblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        // Método para validar si el correo es de Gmail
        private bool IsGmailEmail(string email)
        {
            // Expresión regular para validar correos de Gmail
            string pattern = @"^[a-zA-Z0-9._%+-]+@gmail\.com$";
            return Regex.IsMatch(email, pattern);
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
        }
    }
}