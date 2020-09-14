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
    public partial class AlumnosInscripciones : Form
    {
        public AlumnosInscripciones(Persona.TiposPersonas tipo, Persona personaActual)
        {
            TipoPersona = tipo;
            PersonaActual = personaActual;
            InitializeComponent();
            this.dgvAlumnosInscripciones.AutoGenerateColumns = false;
            this.oAlumnosInscripciones = new Business.Entities.AlumnoInscripcion();
        }
        private Business.Entities.AlumnoInscripcion _alumnosInscripciones;
        public Business.Entities.AlumnoInscripcion oAlumnosInscripciones
        {
            get { return _alumnosInscripciones; }
            set { _alumnosInscripciones = value; }
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
            AlumnoInscripcionLogic ail = new AlumnoInscripcionLogic();
            if (TipoPersona == Persona.TiposPersonas.Alumno)
            {
                this.dgvAlumnosInscripciones.DataSource = ail.GetPorPersona(PersonaActual.ID);
            }
            else if (TipoPersona == Persona.TiposPersonas.Docente)
            {
                //obtengo la lista de alumnos inscripciones
                List<AlumnoInscripcion>alumnos=ail.GetAll();
                //obtengo la lista de inscripciones de docentes a cursos del profesor actual
                DocenteCursoLogic dcl = new DocenteCursoLogic();
                List<DocenteCurso> docentesCursos = dcl.GetPorPersona(PersonaActual.ID);
                //lista para alumnos que coinciden con el profesor
                List<AlumnoInscripcion> alumnosPorProfesor = new List<AlumnoInscripcion>();
                foreach (var inscDocente in docentesCursos)
                {
                    foreach (var alumno in alumnos)
                    {
                        if (alumno.IDCurso==inscDocente.IDCurso && inscDocente.IDDocente==PersonaActual.ID)
                        {
                            alumnosPorProfesor.Add(alumno);
                        }
                    }
                }
                this.dgvAlumnosInscripciones.DataSource = alumnosPorProfesor;
            }
            else
            {
                this.dgvAlumnosInscripciones.DataSource = ail.GetAll();
            }
        }
        private void AlumnosInscripciones_Load(object sender, EventArgs e)
        {
            if (TipoPersona == Persona.TiposPersonas.Alumno )
            {
                this.tsbEditar.Visible = false;
            }
            if (TipoPersona == Persona.TiposPersonas.Docente)
            {
                this.tsbNuevo.Visible = false;
                this.tsbEliminar.Visible = false;
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
            int cantFilas = this.dgvAlumnosInscripciones.RowCount;
            if (Validar())
            {
                AlumnoInscripcionDesktop formAlumnoInscripcion = new AlumnoInscripcionDesktop(PersonaActual.ID ,ApplicationForm.ModoForm.Alta,TipoPersona);
                formAlumnoInscripcion.ShowDialog();
            }
            this.Listar();
            //si se agregó una fila, seleccionarla
            if (cantFilas!=this.dgvAlumnosInscripciones.RowCount)
            {
                DataGridViewRow fila = this.dgvAlumnosInscripciones.Rows[cantFilas];
                this.dgvAlumnosInscripciones.CurrentCell = fila.Cells[0];
            }
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (this.dgvAlumnosInscripciones.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a modificar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int nroFilaSeleccionada = this.dgvAlumnosInscripciones.CurrentCell.RowIndex;
                int ID = ((Business.Entities.AlumnoInscripcion)this.dgvAlumnosInscripciones.SelectedRows[0].DataBoundItem).ID;
                AlumnoInscripcionDesktop formAlumnoInscripcion = new AlumnoInscripcionDesktop(ID, ApplicationForm.ModoForm.Modificacion,TipoPersona);
                formAlumnoInscripcion.ShowDialog();
                this.Listar();
                //volver a seleccionar la fila con la que se interactuó anteriormente
                DataGridViewRow fila = this.dgvAlumnosInscripciones.Rows[nroFilaSeleccionada];
                this.dgvAlumnosInscripciones.CurrentCell = fila.Cells[0];
            }
        }
        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (this.dgvAlumnosInscripciones.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a eliminar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int cantFilas = this.dgvAlumnosInscripciones.RowCount;
                int indiceSeleccionado = this.dgvAlumnosInscripciones.CurrentCell.RowIndex;
                int ID = ((Business.Entities.AlumnoInscripcion)this.dgvAlumnosInscripciones.SelectedRows[0].DataBoundItem).ID;
                AlumnoInscripcionDesktop formAlumnoInscripcion = new AlumnoInscripcionDesktop(ID, ApplicationForm.ModoForm.Baja,TipoPersona);
                formAlumnoInscripcion.ShowDialog();
                this.Listar();
                //si no se eliminó la fila, volver a seleccionarla
                if (cantFilas == this.dgvAlumnosInscripciones.RowCount)
                {
                    DataGridViewRow fila = this.dgvAlumnosInscripciones.Rows[indiceSeleccionado];
                    this.dgvAlumnosInscripciones.CurrentCell = fila.Cells[0];
                }
            }
        }
        private void tsbVer_Click(object sender, EventArgs e)
        {
            if (this.dgvAlumnosInscripciones.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a inspeccionar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int indiceSeleccionado = this.dgvAlumnosInscripciones.CurrentCell.RowIndex;
                int ID = ((Business.Entities.AlumnoInscripcion)this.dgvAlumnosInscripciones.SelectedRows[0].DataBoundItem).ID;
                AlumnoInscripcionDesktop formAlumnoInscripcion = new AlumnoInscripcionDesktop(ID, ApplicationForm.ModoForm.Consulta,TipoPersona);
                formAlumnoInscripcion.ShowDialog();
                this.Listar();
                //volver a seleccionar la fila consultada
                DataGridViewRow fila = this.dgvAlumnosInscripciones.Rows[indiceSeleccionado];
                this.dgvAlumnosInscripciones.CurrentCell = fila.Cells[0];
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
