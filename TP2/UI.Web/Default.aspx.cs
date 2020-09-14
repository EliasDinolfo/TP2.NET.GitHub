using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Entities;
using Business.Logic;

namespace UI.Web
{
    public partial class Default : System.Web.UI.Page
    {
        public static Usuario _usuarioActual;
        public static Persona.TiposPersonas _tipoPersona;
        public Usuario UsuarioActual
        {
            get { return _usuarioActual; }
            set { _usuarioActual = value; }
        }
        public Persona.TiposPersonas TipoPersona
        {
            get { return _tipoPersona; }
            set { _tipoPersona = value;}
        }
        public enum FormModes
        {
            Alta,
            Baja,
            Modificacion,
            Consulta
        }
        public FormModes FormMode
        {
            get { return (FormModes)this.ViewState["FormMode"]; }
            set { this.ViewState["FormMode"] = value; }
        }
        protected int SelectedID
        {
            get
            {
                if (this.ViewState["SelectedID"] != null)
                {
                    return (int)this.ViewState["SelectedID"];
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                this.ViewState["SelectedID"] = value;
            }
        }
        protected bool IsEntitySelected
        {
            get
            {
                return (this.SelectedID != 0);
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string valor = Convert.ToString(Request.QueryString["id"]);
            int id = int.Parse(valor);
            UsuarioLogic ul = new UsuarioLogic();
            UsuarioActual = ul.GetOne(id);
            PersonaLogic pl = new PersonaLogic();
            Persona per = pl.GetOne(UsuarioActual.IDPersona);
            TipoPersona = per.TipoPersona;
            this.lblBienvenida.Text += per.TipoPersona.ToString() + " " + per.Nombre + " " + per.Apellido;
            this.lblIDPersona.Text += per.ID.ToString();
        }
    }
}