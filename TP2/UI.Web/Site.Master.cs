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
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Default._usuarioActual!=null)
            {
                this.menu.Visible = true;
                this.btnCerrarSesion.Visible = true;
                if (this.menu.Items.Count==0)
                {
                    cargarMenu();
                }
            }
        }
        protected void cargarMenu()
        {
            Persona.TiposPersonas tipo = getTipoPersona();
            if (tipo == Persona.TiposPersonas.Administrador)
            {
                MenuItem mi = new MenuItem();
                mi.NavigateUrl = "~/Usuarios.aspx";
                mi.Value = "1";
                mi.Text = "Usuarios ";
                this.menu.Items.Add(mi);
                mi = new MenuItem();
                mi.NavigateUrl = "~/Planes.aspx";
                mi.Value = "2";
                mi.Text = "Planes ";
                this.menu.Items.Add(mi);
                mi = new MenuItem();
                mi.NavigateUrl = "#";
                mi.Value = "3";
                mi.Text = "Materias ";
                this.menu.Items.Add(mi);
                mi = new MenuItem();
                mi.NavigateUrl = "#";
                mi.Value = "4";
                mi.Text = "Comisiones ";
                this.menu.Items.Add(mi);
                mi = new MenuItem();
                mi.NavigateUrl = "#";
                mi.Value = "5";
                mi.Text = "Personas ";
                this.menu.Items.Add(mi);
                mi = new MenuItem();
                mi.NavigateUrl = "~/Especialidades.aspx";
                mi.Value = "6";
                mi.Text = "Especialidades ";
                this.menu.Items.Add(mi);
                mi = new MenuItem();
                mi.NavigateUrl = "#";
                mi.Value = "7";
                mi.Text = "Cursos ";
                this.menu.Items.Add(mi);
                mi = new MenuItem();
                mi.NavigateUrl = "#";
                mi.Value = "8";
                mi.Text = "AlumnosInscripciones ";
                this.menu.Items.Add(mi);
                mi = new MenuItem();
                mi.NavigateUrl = "#";
                mi.Value = "9";
                mi.Text = "DocentesCursos ";
                this.menu.Items.Add(mi);
            }
            else if (tipo == Persona.TiposPersonas.Alumno)
            {
                MenuItem mi = new MenuItem();
                mi.NavigateUrl = "~/Usuarios.aspx";
                mi.Value = "1";
                mi.Text = "Configuración de Usuario ";
                this.menu.Items.Add(mi);
                mi = new MenuItem();
                mi.NavigateUrl = "#";
                mi.Value = "2";
                mi.Text = "InscripciónACurso ";
                this.menu.Items.Add(mi);
                mi = new MenuItem();
                mi.NavigateUrl = "#";
                mi.Value = "3";
                mi.Text = "Cursos ";
                this.menu.Items.Add(mi);
                mi = new MenuItem();
                mi.NavigateUrl = "#";
                mi.Value = "4";
                mi.Text = "Materias ";
                this.menu.Items.Add(mi);
            }
            else
            {
                MenuItem mi = new MenuItem();
                mi.NavigateUrl = "~/Usuarios.aspx";
                mi.Value = "1";
                mi.Text = "Configuración de Usuario ";
                this.menu.Items.Add(mi);
                mi = new MenuItem();
                mi.NavigateUrl = "#";
                mi.Value = "2";
                mi.Text = "EstadoAlumnos ";
                this.menu.Items.Add(mi);
                mi = new MenuItem();
                mi.NavigateUrl = "#";
                mi.Value = "3";
                mi.Text = "Cursos ";
                this.menu.Items.Add(mi);
                mi = new MenuItem();
                mi.NavigateUrl = "#";
                mi.Value = "4";
                mi.Text = "Materias ";
                this.menu.Items.Add(mi);
                mi = new MenuItem();
                mi.NavigateUrl = "~/Planes.aspx";
                mi.Value = "5";
                mi.Text = "Planes ";
                this.menu.Items.Add(mi);
                mi = new MenuItem();
                mi.NavigateUrl = "~/Especialidades.aspx";
                mi.Value = "6";
                mi.Text = "Especialidades ";
                this.menu.Items.Add(mi);
                mi = new MenuItem();
                mi.NavigateUrl = "#";
                mi.Value = "7";
                mi.Text = "InscripciónCurso ";
                this.menu.Items.Add(mi);
            }
        }
        protected void vaciarMenu()
        {
            int cantidad = menu.Items.Count;
            for (int i = 0; i < cantidad; i++)
            {
                this.menu.Items.RemoveAt(0);
            }
        }
        protected Persona.TiposPersonas getTipoPersona()
        {
            PersonaLogic pl = new PersonaLogic();
            Persona per = pl.GetOne(Default._usuarioActual.IDPersona);
            return per.TipoPersona;
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Default._usuarioActual = null;
            vaciarMenu();
            this.menu.Visible = false;
            this.btnCerrarSesion.Visible = false;
            Response.Redirect("~/Login.aspx");
        }
    }
}