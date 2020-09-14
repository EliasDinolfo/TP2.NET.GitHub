using Business.Entities;
using Business.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Desktop
{
    public partial class Menu : Form
    {
        private Form actual=null;
        protected static Usuario usuarioActual;
        protected static Persona personaActual;
        protected static Persona.TiposPersonas tipoPersona;
        public Menu()
        {
            InitializeComponent();
        }
        public Menu(Usuario usr)
        {
            usuarioActual = usr;
            InitializeComponent();
        }
        private void Menu_Load(object sender, EventArgs e)
        {
            PersonaLogic pl = new PersonaLogic();
            Persona per = pl.GetOne(usuarioActual.IDPersona);
            personaActual = per;
            tipoPersona = per.TipoPersona;
            if (tipoPersona==Persona.TiposPersonas.Alumno)
            {
                this.tsmiEspecialidades.Visible = false;
                this.tsmiDocentesCursos.Visible = false;
                this.tsmiPlanes.Visible = false;
                this.tsmiDocentesCursos.Visible = false;
                this.tsmiPersonas.Visible = false;
                this.tsmiModulosUsuarios.Visible = false;
                this.tsmiModulosUsuarios.Visible = false;
                this.tsmiDocentesCursos.Visible = false;
                this.tsmiModulos.Visible = false;
                this.tsmiComisiones.Visible = false;
                this.tsmiUsuarios.Text = "Configuración de Usuario";
                this.tsmiAlumnosInscripciones.Text = "Inscripción a curso";
                this.tsmiMaterias.Text = "Materias del plan";
            }
            else if (tipoPersona == Persona.TiposPersonas.Docente)
            {
                this.tsmiPersonas.Visible = false;
                this.tsmiModulosUsuarios.Visible = false;
                this.tsmiModulosUsuarios.Visible = false;
                this.tsmiModulos.Visible = false;
                this.tsmiUsuarios.Text = "Configuración de Usuario";
                this.tsmiAlumnosInscripciones.Text = "Condición Alumnos";
                this.tsmiDocentesCursos.Text = "Inscripción a curso";
                this.tsmiMaterias.Text = "Materias del plan";
                this.tsmiComisiones.Text = "Comisiones del plan";
            }
            this.lblBienvenido.Text = "Bienvenido " +per.TipoPersona.ToString() +": "+ per.Nombre + " " + per.Apellido
                +"\nID: " + per.ID;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       private void tsmiPersonas_Click(object sender, EventArgs e)
       {
            if (actual!=null)
            {
                actual.Close();
                actual = null;
            }
            Personas f = new Personas();
            actual = f;
            f.MdiParent = this;
            f.Show();
            
        }
        private void tsmiEspecialidades_Click(object sender, EventArgs e)
        {
            if (actual != null)
            {
                actual.Close();
                actual = null;
            }
            Especialidades f = new Especialidades(tipoPersona);
            actual = f;
            f.MdiParent = this;
            f.Show();
        }
        private void tsmiPlanes_Click(object sender, EventArgs e)
        {
            if (actual != null)
            {
                actual.Close();
                actual = null;
            }
            Planes f = new Planes(tipoPersona);
            actual = f;
            f.MdiParent = this;
            f.Show();
        }
        private void tsmiComisiones_Click(object sender, EventArgs e)
        {
            if (actual != null)
            {
                actual.Close();
                actual = null;
            }
            Comisiones f = new Comisiones(tipoPersona, personaActual);
            actual = f;
            f.MdiParent = this;
            f.Show();
        }
        private void tsmiMaterias_Click(object sender, EventArgs e)
        {
            if (actual != null)
            {
                actual.Close();
                actual = null;
            }
            Materias f = new Materias(tipoPersona, personaActual);
            actual = f;
            f.MdiParent = this;
            f.Show();
        }
        private void tsmiiCursos_Click(object sender, EventArgs e)
        {
            if (actual != null)
            {
                actual.Close();
                actual = null;
            }
            Cursos f = new Cursos(tipoPersona, personaActual);
            actual = f;
            f.MdiParent = this;
            f.Show();
        }
        private void tsmiUsuarios_Click(object sender, EventArgs e)
        {
            if (actual != null)
            {
                actual.Close();
                actual = null;
            }
            Usuarios f = new Usuarios(tipoPersona, usuarioActual);
            actual = f;
            f.MdiParent = this;
            f.Show();
        }
        private void tsmiModulos_Click(object sender, EventArgs e)
        {
            if (actual != null)
            {
                actual.Close();
                actual = null;
            }
            Modulos f = new Modulos();
            actual = f;
            f.MdiParent = this;
            f.Show();
        }

        private void tsmiModulosUsuarios_Click(object sender, EventArgs e)
        {
            if (actual != null)
            {
                actual.Close();
                actual = null;
            }
            ModulosUsuarios f = new ModulosUsuarios();
            actual = f;
            f.MdiParent = this;
            f.Show();
        }
        private void tsmiAlumnosInscripciones_Click(object sender, EventArgs e)
        {
            if (actual != null)
            {
                actual.Close();
                actual = null;
            }
            AlumnosInscripciones f = new AlumnosInscripciones(tipoPersona, personaActual);
            actual = f;
            f.MdiParent = this;
            f.Show();
        }

        private void tsmiDocentesCursos_Click(object sender, EventArgs e)
        {
            if (actual != null)
            {
                actual.Close();
                actual = null;
            }
            DocentesCursos f = new DocentesCursos(tipoPersona, personaActual);
            actual = f;
            f.MdiParent = this;
            f.Show();
        }

        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Login login = new Login();
            login.Show();
        }
        private void btnCerrarSesion_MouseEnter(object sender, EventArgs e)
        {
            this.btnCerrarSesion.BackColor = SystemColors.ActiveCaption;
            this.btnCerrarSesion.Text = "CERRAR SESIÓN";
        }

        private void btnCerrarSesion_MouseLeave(object sender, EventArgs e)
        {
            this.btnCerrarSesion.BackColor = SystemColors.ButtonHighlight;
            this.btnCerrarSesion.Text = "Cerrar Sesión";
        }
    }
}
