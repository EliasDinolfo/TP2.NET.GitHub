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
    public partial class Planes : Form
    {
        public Planes(Persona.TiposPersonas tipo)
        {
            TipoPersona = tipo;
            InitializeComponent();
            this.dgvPlanes.AutoGenerateColumns = false;
            this.oPlanes = new Business.Entities.Plan();
        }
        private Persona.TiposPersonas _tipoPersona;
        public Business.Entities.Persona.TiposPersonas TipoPersona
        {
            get { return _tipoPersona; }
            set { _tipoPersona = value; }
        }
        private Business.Entities.Plan _planes;
        public Business.Entities.Plan oPlanes
        {
            get { return _planes; }
            set { _planes = value; }
        }

        public void Listar()
        {
            PlanLogic pl = new PlanLogic();
            this.dgvPlanes.DataSource = pl.GetAll();
        }

        private void Planes_Load(object sender, EventArgs e)
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
            int cantFilas = this.dgvPlanes.RowCount;
            if (Validar())
            {
                PlanDesktop formPlan = new PlanDesktop(ApplicationForm.ModoForm.Alta);
                formPlan.ShowDialog();
            }
            this.Listar();
            //si se agregó una fila, seleccionarla
            if (cantFilas != this.dgvPlanes.RowCount)
            {
                DataGridViewRow fila = this.dgvPlanes.Rows[cantFilas];
                this.dgvPlanes.CurrentCell = fila.Cells[0];
            }
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (this.dgvPlanes.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a modificar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int nroFilaSeleccionada = this.dgvPlanes.CurrentCell.RowIndex;
                int ID = ((Business.Entities.Plan)this.dgvPlanes.SelectedRows[0].DataBoundItem).ID;
                PlanDesktop formPlan = new PlanDesktop(ID, ApplicationForm.ModoForm.Modificacion);
                formPlan.ShowDialog();
                this.Listar();
                //volver a seleccionar la fila con la que se interactuó anteriormente
                DataGridViewRow fila = this.dgvPlanes.Rows[nroFilaSeleccionada];
                this.dgvPlanes.CurrentCell = fila.Cells[0];
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (this.dgvPlanes.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a eliminar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int cantFilas = this.dgvPlanes.RowCount;
                int indiceSeleccionado = this.dgvPlanes.CurrentCell.RowIndex;
                int ID = ((Business.Entities.Plan)this.dgvPlanes.SelectedRows[0].DataBoundItem).ID;
                PlanDesktop formPlan = new PlanDesktop(ID, ApplicationForm.ModoForm.Baja);
                formPlan.ShowDialog();
                this.Listar();
                //si no se eliminó la fila, volver a seleccionarla
                if (cantFilas == this.dgvPlanes.RowCount)
                {
                    DataGridViewRow fila = this.dgvPlanes.Rows[indiceSeleccionado];
                    this.dgvPlanes.CurrentCell = fila.Cells[0];
                }
            }
        }
        private void tsbVer_Click(object sender, EventArgs e)
        {
            if (this.dgvPlanes.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a inspeccionar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int indiceSeleccionado = this.dgvPlanes.CurrentCell.RowIndex;
                int ID = ((Business.Entities.Plan)this.dgvPlanes.SelectedRows[0].DataBoundItem).ID;
                PlanDesktop formPlan = new PlanDesktop(ID, ApplicationForm.ModoForm.Consulta);
                formPlan.ShowDialog();
                this.Listar();
                //volver a seleccionar la fila consultada
                DataGridViewRow fila = this.dgvPlanes.Rows[indiceSeleccionado];
                this.dgvPlanes.CurrentCell = fila.Cells[0];
            }
        }

        private bool Validar()
        {
            string mensaje = null;
            EspecialidadLogic el = new EspecialidadLogic();
            if (el.GetAll().Count == 0)
            {
                mensaje += "Debe cargar antes una especialidad por lo menos\n";
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
