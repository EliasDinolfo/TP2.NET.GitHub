using Business.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.Logic;

namespace UI.Desktop
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtUsuario.Text == "" || txtClave.Text == "")
            {
                MessageBox.Show("Debe rellenar todos los campos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                UsuarioLogic ul = new UsuarioLogic();
                List<Usuario> usuarios = ul.GetAll();
                int bandera = 0;
                foreach (var usr in usuarios)
                {
                    if (txtClave.Text == usr.Clave && txtUsuario.Text == usr.NombreUsuario)
                    {
                        if (!usr.Habilitado)
                        {
                            MessageBox.Show("Su usuario está deshabilitado. Consulte a un administrador.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            bandera = 1;
                            break;
                        }
                        Menu menu = new Menu(usr);
                        menu.Show();
                        this.Close();
                        bandera = 1;
                        break;
                    }
                    bandera = 0;
                }
                if (bandera ==0 )
                MessageBox.Show("Los datos ingresados son incorrectos", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
