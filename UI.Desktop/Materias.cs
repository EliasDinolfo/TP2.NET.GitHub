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
    public partial class Materias : Form
    {
        public Materias(Persona.TiposPersonas tipo, Persona personaActual)
        {
            TipoPersona = tipo;
            PersonaActual = personaActual;
            InitializeComponent();
            this.dgvMaterias.AutoGenerateColumns = false;
            this.oMaterias = new Business.Entities.Materia();
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
        private Business.Entities.Materia _materias;
        public Business.Entities.Materia oMaterias
        {
            get { return _materias; }
            set { _materias = value; }
        }

        public void Listar()
        {
            MateriaLogic ml = new MateriaLogic();
            if (TipoPersona == Persona.TiposPersonas.Docente || TipoPersona == Persona.TiposPersonas.Alumno)
            {
                this.dgvMaterias.DataSource = ml.GetPorPlan(PersonaActual.IDPlan);
            }
            else
            {
                this.dgvMaterias.DataSource = ml.GetAll();
            }
            
        }

        private void Materias_Load(object sender, EventArgs e)
        {
            if (TipoPersona == Persona.TiposPersonas.Docente || TipoPersona == Persona.TiposPersonas.Alumno)
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
            int cantFilas = this.dgvMaterias.RowCount;
            if (Validar())
            {
                MateriaDesktop formMateria = new MateriaDesktop(ApplicationForm.ModoForm.Alta);
                formMateria.ShowDialog();
            }
            this.Listar();
            //si se agregó una fila, seleccionarla
            if (cantFilas != this.dgvMaterias.RowCount)
            {
                DataGridViewRow fila = this.dgvMaterias.Rows[cantFilas];
                this.dgvMaterias.CurrentCell = fila.Cells[0];
            }
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (this.dgvMaterias.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a modificar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int nroFilaSeleccionada = this.dgvMaterias.CurrentCell.RowIndex;
                int ID = ((Business.Entities.Materia)this.dgvMaterias.SelectedRows[0].DataBoundItem).ID;
                MateriaDesktop formMateria = new MateriaDesktop(ID, ApplicationForm.ModoForm.Modificacion);
                formMateria.ShowDialog();
                this.Listar();
                //volver a seleccionar la fila con la que se interactuó anteriormente
                DataGridViewRow fila = this.dgvMaterias.Rows[nroFilaSeleccionada];
                this.dgvMaterias.CurrentCell = fila.Cells[0];
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (this.dgvMaterias.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a eliminar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int cantFilas = this.dgvMaterias.RowCount;
                int indiceSeleccionado = this.dgvMaterias.CurrentCell.RowIndex;
                int ID = ((Business.Entities.Materia)this.dgvMaterias.SelectedRows[0].DataBoundItem).ID;
                MateriaDesktop formMateria = new MateriaDesktop(ID, ApplicationForm.ModoForm.Baja);
                formMateria.ShowDialog();
                this.Listar();
                //si no se eliminó la fila, volver a seleccionarla
                if (cantFilas == this.dgvMaterias.RowCount)
                {
                    DataGridViewRow fila = this.dgvMaterias.Rows[indiceSeleccionado];
                    this.dgvMaterias.CurrentCell = fila.Cells[0];
                }
            }
        }
        private void tsbVer_Click(object sender, EventArgs e)
        {
            if (this.dgvMaterias.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a inspeccionar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int indiceSeleccionado = this.dgvMaterias.CurrentCell.RowIndex;
                int ID = ((Business.Entities.Materia)this.dgvMaterias.SelectedRows[0].DataBoundItem).ID;
                MateriaDesktop formMateria = new MateriaDesktop(ID, ApplicationForm.ModoForm.Consulta);
                formMateria.ShowDialog();
                this.Listar();
                //volver a seleccionar la fila consultada
                DataGridViewRow fila = this.dgvMaterias.Rows[indiceSeleccionado];
                this.dgvMaterias.CurrentCell = fila.Cells[0];
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
