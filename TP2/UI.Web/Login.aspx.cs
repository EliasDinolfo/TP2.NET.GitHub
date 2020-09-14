using Business.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Entities;
namespace UI.Web
{
    public partial class Login : System.Web.UI.Page
    {
        private int _idUsuario;
        public int IDUsuario
        {
            get { return _idUsuario; }
            set { _idUsuario = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                Response.Redirect("~/Default.aspx?id=" + IDUsuario.ToString());
            }
            else
            {
                this.lblMensaje.Text = "Los datos ingresados son incorrectos";
            }
        }
        private bool validar()
        {
            UsuarioLogic ul = new UsuarioLogic();
            List<Usuario> usuarios = ul.GetAll();
            foreach (Usuario usr in usuarios)
            {
                if (usr.Clave.Equals(this.txtClave.Text)&&usr.NombreUsuario.Equals(this.txtUsuario.Text))
                {
                    IDUsuario = usr.ID;
                    return true;
                }
            }
            return false;
        }
    }
}