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
    public partial class DocentesCursos : Form
    {
        public DocentesCursos(Persona.TiposPersonas tipo, Persona personaActual)
        {
            TipoPersona = tipo;
            PersonaActual = personaActual;
            InitializeComponent();
            this.dgvDocentesCursos.AutoGenerateColumns = false;
            this.oDocentesCursos = new Business.Entities.DocenteCurso();
        }
        private Business.Entities.DocenteCurso _docentesCursos;
        public Business.Entities.DocenteCurso oDocentesCursos
        {
            get { return _docentesCursos; }
            set { _docentesCursos = value; }
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

        public void Listar()
        {
            DocenteCursoLogic dcl = new DocenteCursoLogic();
            if (TipoPersona == Persona.TiposPersonas.Docente)
            {
                List<DocenteCurso> docentesCursos = dcl.GetAll();
                List<DocenteCurso> docentesCursos2 = new List<DocenteCurso>();
                //listar todos los cursos inscriptos que pertenecen a cursos del plan del docente actual
                CursoLogic cl = new CursoLogic();
                MateriaLogic ml = new MateriaLogic();
                //obtener las materias pertenecientes al plan de la persona
                List<Materia> materias = ml.GetPorPlan(PersonaActual.IDPlan);
                //obtener los cursos pertenecientes a esas materias
                List<Curso> cursos = cl.GetPorMaterias(materias);
                //obtener los docentesCursos asociados a cada curso de la lista anterior
                foreach (DocenteCurso dc in docentesCursos)
                {
                    foreach (Curso cur in cursos)
                    {
                        if (dc.IDCurso == cur.ID)
                        {
                            docentesCursos2.Add(dc);
                        }
                    }
                }
                //finalmente se lo asigno al datagridview
                this.dgvDocentesCursos.DataSource = docentesCursos2;
            }
            else
            {
                this.dgvDocentesCursos.DataSource = dcl.GetAll();
            }
        }

        private void DocentesCursos_Load(object sender, EventArgs e)
        {
            if (TipoPersona == Persona.TiposPersonas.Docente)
            {
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
            int cantFilas = this.dgvDocentesCursos.RowCount;
            if (Validar())
            {
                DocenteCursoDesktop formDocenteCurso = new DocenteCursoDesktop(PersonaActual.ID,ApplicationForm.ModoForm.Alta, TipoPersona, PersonaActual);
                formDocenteCurso.ShowDialog();
            }
            this.Listar();
            //si se agregó una fila, seleccionarla
            if (cantFilas != this.dgvDocentesCursos.RowCount)
            {
                DataGridViewRow fila = this.dgvDocentesCursos.Rows[cantFilas];
                this.dgvDocentesCursos.CurrentCell = fila.Cells[0];
            }
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (this.dgvDocentesCursos.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a modificar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int nroFilaSeleccionada = this.dgvDocentesCursos.CurrentCell.RowIndex;
                int ID = ((Business.Entities.DocenteCurso)this.dgvDocentesCursos.SelectedRows[0].DataBoundItem).ID;
                DocenteCursoDesktop formDocenteCurso = new DocenteCursoDesktop(ID, ApplicationForm.ModoForm.Modificacion, TipoPersona, PersonaActual);
                formDocenteCurso.ShowDialog();
                this.Listar();
                //volver a seleccionar la fila con la que se interactuó anteriormente
                DataGridViewRow fila = this.dgvDocentesCursos.Rows[nroFilaSeleccionada];
                this.dgvDocentesCursos.CurrentCell = fila.Cells[0];
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (this.dgvDocentesCursos.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a eliminar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int cantFilas = this.dgvDocentesCursos.RowCount;
                int indiceSeleccionado = this.dgvDocentesCursos.CurrentCell.RowIndex;
                int ID = ((Business.Entities.DocenteCurso)this.dgvDocentesCursos.SelectedRows[0].DataBoundItem).ID;
                DocenteCursoDesktop formDocenteCurso = new DocenteCursoDesktop(ID, ApplicationForm.ModoForm.Baja, TipoPersona, PersonaActual);
                formDocenteCurso.ShowDialog();
                this.Listar();
                //si no se eliminó la fila, volver a seleccionarla
                if (cantFilas == this.dgvDocentesCursos.RowCount)
                {
                    DataGridViewRow fila = this.dgvDocentesCursos.Rows[indiceSeleccionado];
                    this.dgvDocentesCursos.CurrentCell = fila.Cells[0];
                }
            }
        }
        private void tsbVer_Click(object sender, EventArgs e)
        {
            if (this.dgvDocentesCursos.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a inspeccionar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int indiceSeleccionado = this.dgvDocentesCursos.CurrentCell.RowIndex;
                int ID = ((Business.Entities.DocenteCurso)this.dgvDocentesCursos.SelectedRows[0].DataBoundItem).ID;
                DocenteCursoDesktop formDocenteCurso = new DocenteCursoDesktop(ID, ApplicationForm.ModoForm.Consulta, TipoPersona, PersonaActual);
                formDocenteCurso.ShowDialog();
                this.Listar();
                //volver a seleccionar la fila consultada
                DataGridViewRow fila = this.dgvDocentesCursos.Rows[indiceSeleccionado];
                this.dgvDocentesCursos.CurrentCell = fila.Cells[0];
            }
        }

        private bool Validar()
        {
            string mensaje = null;
            CursoLogic cl = new CursoLogic();
            if (cl.GetAll().Count == 0)
            {
                mensaje += "Debe cargar antes un curso por lo menos\n";
            }
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
