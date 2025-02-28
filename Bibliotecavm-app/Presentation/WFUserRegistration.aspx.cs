using System;
using Logic;
using System.Collections.Generic;

namespace Presentation
{
    public partial class WFUserRegistration : System.Web.UI.Page
    {
        UserLogic objUserLogic = new UserLogic();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bool adminExists = objUserLogic.CheckAdminExists(); // Llamamos a la capa lógica

                // Si ya existe un administrador, deshabilitamos la opción de "Administrador"
                if (adminExists)
                {
                    DDLRole.Items.FindByValue("Administrador").Enabled = false;
                }
            }
        }

        // Método que se ejecuta al hacer clic en el botón "Guardar"
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            string nombre = TBFirstName.Text.Trim();
            string apellido = TBLastName.Text.Trim();
            string correo = TBEmail.Text.Trim();
            string contrasena = TBPassword.Text.Trim();
            string salt = TBSalt.Text.Trim();
            string rol = DDLRole.SelectedValue;
            string nivelEstudios = DDLEducationLevel.SelectedValue;

            // Verificar si ya existe un administrador antes de permitir la selección de ese rol
            bool adminExists = objUserLogic.CheckAdminExists();
            if (rol == "Administrador" && adminExists)
            {
                LblMessage.Text = "Ya existe un administrador. Solo puede haber uno.";
                return;
            }

            // Validación básica de los campos
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(apellido) || string.IsNullOrEmpty(correo) ||
                string.IsNullOrEmpty(contrasena) || string.IsNullOrEmpty(salt) || string.IsNullOrEmpty(rol) ||
                string.IsNullOrEmpty(nivelEstudios))
            {
                LblMessage.Text = "Por favor, complete todos los campos.";
                return;
            }

            // Llamada al método para guardar el usuario
            bool resultado = objUserLogic.saveUser(nombre, apellido, correo, contrasena, salt, rol, nivelEstudios);
            if (resultado)
            {
                LblMessage.Text = "Usuario registrado exitosamente.";
                clearFields();
            }
            else
            {
                LblMessage.Text = "Error al registrar el usuario.";
            }
        }


        // Método para limpiar campos
        private void clearFields()
        {
            TBFirstName.Text = "";
            TBLastName.Text = "";
            TBEmail.Text = "";
            TBPassword.Text = "";
            TBSalt.Text = "";
            DDLRole.SelectedIndex = 0;
            DDLEducationLevel.SelectedIndex = 0;
        }
    }
}
