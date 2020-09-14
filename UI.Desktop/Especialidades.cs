using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.Entities;
using Business.Logic;

namespace UI.Desktop
{
    public partial class Especialidades : Form
    {
        public Especialidades(Persona.TiposPersonas tipo)
        {
            TipoPersona = tipo;
            InitializeComponent();
            this.dgvEspecialidades.AutoGenerateColumns = false;
            this.oEspecialidades = new Business.Entities.Especialidad();
        }
        private Persona.TiposPersonas _tipoPersona;
        public Business.Entities.Persona.TiposPersonas TipoPersona
        {
            get { return _tipoPersona; }
            set { _tipoPersona = value; }
        }
        private Business.Entities.Especialidad _especialidades;
        public Business.Entities.Especialidad oEspecialidades
        {
            get { return _especialidades; }
            set { _especialidades = value; }
        }
        public void Listar()
        {
            EspecialidadLogic el = new EspecialidadLogic();
            this.dgvEspecialidades.DataSource = el.GetAll();
        }

        private void Especialidades_Load(object sender, EventArgs e)
        {
            if (TipoPersona == Persona.TiposPersonas.Docente)
            {
                this.tsbEliminar.Visible = false;
                this.tsbNuevo.Visible = false;
                this.tsbEditar.Visible = false;
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
            int cantFilas = this.dgvEspecialidades.RowCount;
            EspecialidadDesktop formEspecialidad = new EspecialidadDesktop(ApplicationForm.ModoForm.Alta);
            formEspecialidad.ShowDialog();
            this.Listar();
            //si se agregó una fila, seleccionarla
            if (cantFilas != this.dgvEspecialidades.RowCount)
            {
                DataGridViewRow fila = this.dgvEspecialidades.Rows[cantFilas];
                this.dgvEspecialidades.CurrentCell = fila.Cells[0];
            }
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (this.dgvEspecialidades.SelectedRows.Count<1)
            {
                MessageBox.Show("Debe seleccionar la fila a modificar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int nroFilaSeleccionada = this.dgvEspecialidades.CurrentCell.RowIndex;
                int ID = ((Business.Entities.Especialidad)this.dgvEspecialidades.SelectedRows[0].DataBoundItem).ID;
                EspecialidadDesktop formEspecialidad = new EspecialidadDesktop(ID, ApplicationForm.ModoForm.Modificacion);
                formEspecialidad.ShowDialog();
                this.Listar();
                //volver a seleccionar la fila con la que se interactuó anteriormente
                DataGridViewRow fila = this.dgvEspecialidades.Rows[nroFilaSeleccionada];
                this.dgvEspecialidades.CurrentCell = fila.Cells[0];
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (this.dgvEspecialidades.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a eliminar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int cantFilas = this.dgvEspecialidades.RowCount;
                int indiceSeleccionado = this.dgvEspecialidades.CurrentCell.RowIndex;
                int ID = ((Business.Entities.Especialidad)this.dgvEspecialidades.SelectedRows[0].DataBoundItem).ID;
                EspecialidadDesktop formEspecialidad = new EspecialidadDesktop(ID, ApplicationForm.ModoForm.Baja);
                formEspecialidad.ShowDialog();
                this.Listar();
                //si no se eliminó la fila, volver a seleccionarla
                if (cantFilas == this.dgvEspecialidades.RowCount)
                {
                    DataGridViewRow fila = this.dgvEspecialidades.Rows[indiceSeleccionado];
                    this.dgvEspecialidades.CurrentCell = fila.Cells[0];
                }
            }
        }

        private void tsbVer_Click(object sender, EventArgs e)
        {
            if (this.dgvEspecialidades.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a inspeccionar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int indiceSeleccionado = this.dgvEspecialidades.CurrentCell.RowIndex;
                int ID = ((Business.Entities.Especialidad)this.dgvEspecialidades.SelectedRows[0].DataBoundItem).ID;
                EspecialidadDesktop formEspecialidad = new EspecialidadDesktop(ID, ApplicationForm.ModoForm.Consulta);
                formEspecialidad.ShowDialog();
                this.Listar();
                //volver a seleccionar la fila consultada
                DataGridViewRow fila = this.dgvEspecialidades.Rows[indiceSeleccionado];
                this.dgvEspecialidades.CurrentCell = fila.Cells[0];
            }
        }
    }
}
