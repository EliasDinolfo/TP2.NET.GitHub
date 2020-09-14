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
    public partial class Personas : Form
    {
        public Personas()
        {
            InitializeComponent();
            this.dgvPersonas.AutoGenerateColumns = false;
            this.oPersonas = new Business.Entities.Persona();
        }
        private Business.Entities.Persona _personas;
        public Business.Entities.Persona oPersonas
        {
            get { return _personas; }
            set { _personas = value; }
        }

        public void Listar()
        {
            PersonaLogic perl = new PersonaLogic();
            this.dgvPersonas.DataSource = perl.GetAll();
        }

        private void Personas_Load(object sender, EventArgs e)
        {
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
            int cantFilas = this.dgvPersonas.RowCount;
            if (Validar())
            {
                PersonaDesktop formPersona = new PersonaDesktop(ApplicationForm.ModoForm.Alta);
                formPersona.ShowDialog();
            }
            this.Listar();
            //si se agregó una fila, seleccionarla
            if (cantFilas != this.dgvPersonas.RowCount)
            {
                DataGridViewRow fila = this.dgvPersonas.Rows[cantFilas];
                this.dgvPersonas.CurrentCell = fila.Cells[0];
            }
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (this.dgvPersonas.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a modificar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int nroFilaSeleccionada = this.dgvPersonas.CurrentCell.RowIndex;
                int ID = ((Business.Entities.Persona)this.dgvPersonas.SelectedRows[0].DataBoundItem).ID;
                PersonaDesktop formPersona = new PersonaDesktop(ID, ApplicationForm.ModoForm.Modificacion);
                formPersona.ShowDialog();
                this.Listar();
                //volver a seleccionar la fila con la que se interactuó anteriormente
                DataGridViewRow fila = this.dgvPersonas.Rows[nroFilaSeleccionada];
                this.dgvPersonas.CurrentCell = fila.Cells[0];
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (this.dgvPersonas.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a eliminar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int cantFilas = this.dgvPersonas.RowCount;
                int indiceSeleccionado = this.dgvPersonas.CurrentCell.RowIndex;
                int ID = ((Business.Entities.Persona)this.dgvPersonas.SelectedRows[0].DataBoundItem).ID;
                PersonaDesktop formPersona = new PersonaDesktop(ID, ApplicationForm.ModoForm.Baja);
                formPersona.ShowDialog();
                this.Listar();
                //si no se eliminó la fila, volver a seleccionarla
                if (cantFilas == this.dgvPersonas.RowCount)
                {
                    DataGridViewRow fila = this.dgvPersonas.Rows[indiceSeleccionado];
                    this.dgvPersonas.CurrentCell = fila.Cells[0];
                }
            }
        }
        private void tsbVer_Click(object sender, EventArgs e)
        {
            if (this.dgvPersonas.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a inspeccionar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int indiceSeleccionado = this.dgvPersonas.CurrentCell.RowIndex;
                int ID = ((Business.Entities.Persona)this.dgvPersonas.SelectedRows[0].DataBoundItem).ID;
                PersonaDesktop formPersona = new PersonaDesktop(ID, ApplicationForm.ModoForm.Consulta);
                formPersona.ShowDialog();
                this.Listar();
                //volver a seleccionar la fila consultada
                DataGridViewRow fila = this.dgvPersonas.Rows[indiceSeleccionado];
                this.dgvPersonas.CurrentCell = fila.Cells[0];
            }
        }

        private bool Validar()
        {
            string mensaje = null;
            PlanLogic pl = new PlanLogic();
            if (pl.GetAll().Count == 0)
            {
                mensaje += "Debe cargar antes un plan por lo menos\n";
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
