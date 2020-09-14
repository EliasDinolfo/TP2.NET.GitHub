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
    public partial class Cursos : Form
    {
        public Cursos(Persona.TiposPersonas tipo, Persona personaActual)
        {
            TipoPersona = tipo;
            PersonaActual = personaActual;
            InitializeComponent();
            this.dgvCursos.AutoGenerateColumns = false;
            this.oCursos = new Business.Entities.Curso();
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
        private Business.Entities.Curso _cursos;
        public Business.Entities.Curso oCursos
        {
            get { return _cursos; }
            set { _cursos = value; }
        }

        public void Listar()
        {
            CursoLogic cl = new CursoLogic();
            if (TipoPersona == Persona.TiposPersonas.Docente || TipoPersona == Persona.TiposPersonas.Alumno)
            {
                MateriaLogic ml = new MateriaLogic();
                List<Materia> materias = ml.GetPorPlan(PersonaActual.IDPlan);
                this.dgvCursos.DataSource = cl.GetPorMaterias(materias);
            }
            else
            {
                this.dgvCursos.DataSource = cl.GetAll();
            }   
        }

        private void Cursos_Load(object sender, EventArgs e)
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
            int cantFilas = this.dgvCursos.RowCount;
            if (Validar())
            {
                CursoDesktop formCurso = new CursoDesktop(ApplicationForm.ModoForm.Alta);
                formCurso.ShowDialog();
            }
            this.Listar();
            //si se agregó una fila, seleccionarla
            if (cantFilas != this.dgvCursos.RowCount)
            {
                DataGridViewRow fila = this.dgvCursos.Rows[cantFilas];
                this.dgvCursos.CurrentCell = fila.Cells[0];
            }
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (this.dgvCursos.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a modificar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int nroFilaSeleccionada = this.dgvCursos.CurrentCell.RowIndex;
                int ID = ((Business.Entities.Curso)this.dgvCursos.SelectedRows[0].DataBoundItem).ID;
                CursoDesktop formCurso = new CursoDesktop(ID, ApplicationForm.ModoForm.Modificacion);
                formCurso.ShowDialog();
                this.Listar();
                //volver a seleccionar la fila con la que se interactuó anteriormente
                DataGridViewRow fila = this.dgvCursos.Rows[nroFilaSeleccionada];
                this.dgvCursos.CurrentCell = fila.Cells[0];
            }
        }
        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (this.dgvCursos.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a eliminar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int cantFilas = this.dgvCursos.RowCount;
                int indiceSeleccionado = this.dgvCursos.CurrentCell.RowIndex;
                int ID = ((Business.Entities.Curso)this.dgvCursos.SelectedRows[0].DataBoundItem).ID;
                CursoDesktop formCurso = new CursoDesktop(ID, ApplicationForm.ModoForm.Baja);
                formCurso.ShowDialog();
                this.Listar();
                //si no se eliminó la fila, volver a seleccionarla
                if (cantFilas == this.dgvCursos.RowCount)
                {
                    DataGridViewRow fila = this.dgvCursos.Rows[indiceSeleccionado];
                    this.dgvCursos.CurrentCell = fila.Cells[0];
                }
            }
        }
        private void tsbVer_Click(object sender, EventArgs e)
        {
            if (this.dgvCursos.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a inspeccionar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int indiceSeleccionado = this.dgvCursos.CurrentCell.RowIndex;
                int ID = ((Business.Entities.Curso)this.dgvCursos.SelectedRows[0].DataBoundItem).ID;
                CursoDesktop formCurso = new CursoDesktop(ID, ApplicationForm.ModoForm.Consulta);
                formCurso.ShowDialog();
                this.Listar();
                //volver a seleccionar la fila consultada
                DataGridViewRow fila = this.dgvCursos.Rows[indiceSeleccionado];
                this.dgvCursos.CurrentCell = fila.Cells[0];
            }
        }

        private bool Validar()
        {
            string mensaje = null;
            ComisionLogic cl = new ComisionLogic();
            if (cl.GetAll().Count == 0)
            {
                mensaje += "Debe cargar antes una comisión por lo menos\n";
            }
            MateriaLogic ml = new MateriaLogic();
            if (ml.GetAll().Count == 0)
            {
                mensaje += "Debe cargar antes una materia por lo menos\n";
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
