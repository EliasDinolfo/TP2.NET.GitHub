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
using Business.Entities;

namespace UI.Desktop
{
    public partial class Usuarios : Form
    {
        public Usuarios(Persona.TiposPersonas tipo, Usuario usuarioActual)
        {
            TipoPersona = tipo;
            UsuarioActual = usuarioActual;
            InitializeComponent();
            this.dgvUsuarios.AutoGenerateColumns = false;
            this.oUsuarios = new Business.Entities.Usuario();
        }
        private Business.Entities.Usuario _usuarios;
        public Business.Entities.Usuario oUsuarios
        {
            get { return _usuarios; }
            set { _usuarios = value; }
        }
        private Persona.TiposPersonas _tipoPersona;
        public Business.Entities.Persona.TiposPersonas TipoPersona
        {
            get { return _tipoPersona; }
            set { _tipoPersona = value; }
        }
        private Usuario _usuarioActual;
        public Usuario UsuarioActual
        {
            get { return _usuarioActual; }
            set { _usuarioActual = value; }
        }
        public void Listar()
        {
            UsuarioLogic ul = new UsuarioLogic();
            if (TipoPersona==Persona.TiposPersonas.Alumno || TipoPersona == Persona.TiposPersonas.Docente)
            {
                List<Usuario>usuarioUnico = new List<Usuario>();
                usuarioUnico.Add(UsuarioActual);
                this.dgvUsuarios.DataSource=usuarioUnico;
            }
            else
            {
                this.dgvUsuarios.DataSource = ul.GetAll();
            }
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            if (TipoPersona == Persona.TiposPersonas.Alumno || TipoPersona == Persona.TiposPersonas.Docente)
            {
                this.tsbEliminar.Visible = false;
                this.tsbNuevo.Visible = false;
            }
            Listar();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Listar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            int cantFilas = this.dgvUsuarios.RowCount;
            if (Validar())
            {
                UsuarioDesktop formUsuario = new UsuarioDesktop(ApplicationForm.ModoForm.Alta);
                formUsuario.ShowDialog();
            }
            this.Listar();
            //si se agregó una fila, seleccionarla
            if (cantFilas != this.dgvUsuarios.RowCount)
            {
                DataGridViewRow fila = this.dgvUsuarios.Rows[cantFilas];
                this.dgvUsuarios.CurrentCell = fila.Cells[0];
            }
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (this.dgvUsuarios.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a modificar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int nroFilaSeleccionada = this.dgvUsuarios.CurrentCell.RowIndex;
                int ID = ((Business.Entities.Usuario)this.dgvUsuarios.SelectedRows[0].DataBoundItem).ID;
                UsuarioDesktop formUsuario = new UsuarioDesktop(ID, ApplicationForm.ModoForm.Modificacion, TipoPersona);
                formUsuario.ShowDialog();
                this.Listar();
                //volver a seleccionar la fila con la que se interactuó anteriormente
                DataGridViewRow fila = this.dgvUsuarios.Rows[nroFilaSeleccionada];
                this.dgvUsuarios.CurrentCell = fila.Cells[0];
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (this.dgvUsuarios.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a eliminar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int cantFilas = this.dgvUsuarios.RowCount;
                int indiceSeleccionado = this.dgvUsuarios.CurrentCell.RowIndex;
                int ID = ((Business.Entities.Usuario)this.dgvUsuarios.SelectedRows[0].DataBoundItem).ID;
                UsuarioDesktop formUsuario = new UsuarioDesktop(ID, ApplicationForm.ModoForm.Baja, TipoPersona);
                formUsuario.ShowDialog();
                this.Listar();
                //si no se eliminó la fila, volver a seleccionarla
                if (cantFilas == this.dgvUsuarios.RowCount)
                {
                    DataGridViewRow fila = this.dgvUsuarios.Rows[indiceSeleccionado];
                    this.dgvUsuarios.CurrentCell = fila.Cells[0];
                }
            }
        }
        private void tsbVer_Click(object sender, EventArgs e)
        {
            if (this.dgvUsuarios.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a inspeccionar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int indiceSeleccionado = this.dgvUsuarios.CurrentCell.RowIndex;
                int ID = ((Business.Entities.Usuario)this.dgvUsuarios.SelectedRows[0].DataBoundItem).ID;
                UsuarioDesktop formUsuario = new UsuarioDesktop(ID, ApplicationForm.ModoForm.Consulta,TipoPersona);
                formUsuario.ShowDialog();
                this.Listar();
                //volver a seleccionar la fila consultada
                DataGridViewRow fila = this.dgvUsuarios.Rows[indiceSeleccionado];
                this.dgvUsuarios.CurrentCell = fila.Cells[0];
            }
        }

        private bool Validar()
        {
            string mensaje = null;
            PersonaLogic pl = new PersonaLogic();
            if (pl.GetAll().Count == 0)
            {
                mensaje += "Debe cargar antes una persona por lo menos\n";
            }
            if (mensaje != null)
            {
                MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }
    }
}
