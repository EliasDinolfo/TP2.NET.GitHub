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
    public partial class Comisiones : Form
    {
        public Comisiones(Persona.TiposPersonas tipo, Persona personaActual)
        {
            TipoPersona = tipo;
            PersonaActual = personaActual;
            InitializeComponent();
            this.dgvComisiones.AutoGenerateColumns = false;
            this.oComisiones = new Business.Entities.Comision();
        }
        private Persona.TiposPersonas _tipoPersona;
        public Business.Entities.Persona.TiposPersonas TipoPersona
        {
            get { return _tipoPersona; }
            set { _tipoPersona = value; }
        }
        private Persona _personaActual;
        public Persona PersonaActual
        {
            get { return _personaActual; }
            set { _personaActual = value; }
        }
        private Business.Entities.Comision _comisiones;
        public Business.Entities.Comision oComisiones
        {
            get { return _comisiones; }
            set { _comisiones = value; }
        }

        public void Listar()
        {
            ComisionLogic cl = new ComisionLogic();
            if (TipoPersona == Persona.TiposPersonas.Docente)
            {
                this.dgvComisiones.DataSource = cl.GetPorPlan(PersonaActual.IDPlan);
            }
            else
            {
                this.dgvComisiones.DataSource = cl.GetAll();
            }
        }

        private void Comisiones_Load(object sender, EventArgs e)
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
            int cantFilas = this.dgvComisiones.RowCount;
            if (Validar())
            {
                ComisionDesktop formComision = new ComisionDesktop(ApplicationForm.ModoForm.Alta);
                formComision.ShowDialog();
            }
            this.Listar();
            //si se agregó una fila, seleccionarla
            if (cantFilas != this.dgvComisiones.RowCount)
            {
                DataGridViewRow fila = this.dgvComisiones.Rows[cantFilas];
                this.dgvComisiones.CurrentCell = fila.Cells[0];
            }
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (this.dgvComisiones.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a modificar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int nroFilaSeleccionada = this.dgvComisiones.CurrentCell.RowIndex;
                int ID = ((Business.Entities.Comision)this.dgvComisiones.SelectedRows[0].DataBoundItem).ID;
                ComisionDesktop formComision = new ComisionDesktop(ID, ApplicationForm.ModoForm.Modificacion);
                formComision.ShowDialog();
                this.Listar();
                //volver a seleccionar la fila con la que se interactuó anteriormente
                DataGridViewRow fila = this.dgvComisiones.Rows[nroFilaSeleccionada];
                this.dgvComisiones.CurrentCell = fila.Cells[0];
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (this.dgvComisiones.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a eliminar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int cantFilas = this.dgvComisiones.RowCount;
                int indiceSeleccionado = this.dgvComisiones.CurrentCell.RowIndex;
                int ID = ((Business.Entities.Comision)this.dgvComisiones.SelectedRows[0].DataBoundItem).ID;
                ComisionDesktop formComision = new ComisionDesktop(ID, ApplicationForm.ModoForm.Baja);
                formComision.ShowDialog();
                this.Listar();
                //si no se eliminó la fila, volver a seleccionarla
                if (cantFilas == this.dgvComisiones.RowCount)
                {
                    DataGridViewRow fila = this.dgvComisiones.Rows[indiceSeleccionado];
                    this.dgvComisiones.CurrentCell = fila.Cells[0];
                }
            }
        }
        private void tsbVer_Click(object sender, EventArgs e)
        {
            if (this.dgvComisiones.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a inspeccionar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int indiceSeleccionado = this.dgvComisiones.CurrentCell.RowIndex;
                int ID = ((Business.Entities.Comision)this.dgvComisiones.SelectedRows[0].DataBoundItem).ID;
                ComisionDesktop formComision = new ComisionDesktop(ID, ApplicationForm.ModoForm.Consulta);
                formComision.ShowDialog();
                this.Listar();
                //volver a seleccionar la fila consultada
                DataGridViewRow fila = this.dgvComisiones.Rows[indiceSeleccionado];
                this.dgvComisiones.CurrentCell = fila.Cells[0];
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
