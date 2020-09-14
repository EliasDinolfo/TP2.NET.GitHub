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
    public partial class AlumnoInscripcionDesktop : ApplicationForm
    {
        protected AlumnoInscripcion alumnoInscripcionAnterior;
        protected Persona.TiposPersonas tipoPersonaActual;
        protected Persona PersonaActual;
        protected int comboPersonaInicialidado = 0;
        public AlumnoInscripcionDesktop()
        {
            InitializeComponent();
        }
        public AlumnoInscripcionDesktop(int ID, ModoForm modo, Persona.TiposPersonas tipoPersona) : this()
        {
            Modo = modo;
            tipoPersonaActual = tipoPersona;
            PersonaLogic pl = new PersonaLogic();
            PersonaActual = pl.GetOne(ID);

            if (Modo!=ModoForm.Alta)
            {
                AlumnoInscripcionLogic aiL = new AlumnoInscripcionLogic();
                AlumnoInscripcionActual = aiL.GetOne(ID);
                this.alumnoInscripcionAnterior = AlumnoInscripcionActual;
            }
            MapearDeDatos();
        }
        public override void MapearDeDatos()
        {
            Business.Logic.CursoLogic cur = new Business.Logic.CursoLogic();
            cbCurso.DataSource = cur.GetAll();
            cbCurso.DisplayMember = "DatosCompletos";
            cbCurso.ValueMember = "ID";
            cbCurso.DropDownStyle = ComboBoxStyle.DropDownList;
            Business.Logic.PersonaLogic per = new Business.Logic.PersonaLogic();
            cbAlumno.DataSource = per.GetAllAlumnos();
            cbAlumno.DisplayMember = "FullData";
            cbAlumno.ValueMember = "ID";
            cbAlumno.DropDownStyle = ComboBoxStyle.DropDownList;
            if (tipoPersonaActual==Persona.TiposPersonas.Alumno)
            {
                this.cbAlumno.Enabled = false;
                this.mtxtNota.ReadOnly = true;
                this.txtCondicion.ReadOnly = true;
            }
            if (tipoPersonaActual == Persona.TiposPersonas.Docente)
            {
                this.cbAlumno.Enabled = false;
                this.cbCurso.Enabled = false;
            }
            //si no se seleccionó el botón de alta, fijamos la selección realizada en cada combo
            if (Modo != ModoForm.Alta)
            {
                this.txtID.Text = this.AlumnoInscripcionActual.ID.ToString();
                this.mtxtNota.Text = this.AlumnoInscripcionActual.Nota.ToString();
                this.txtCondicion.Text = this.AlumnoInscripcionActual.Condicion;
                for (int i = 0; i < cbCurso.Items.Count; i++)
                {
                    cbCurso.SelectedIndex = i;
                    if (cbCurso.SelectedValue.ToString().Equals(AlumnoInscripcionActual.IDCurso.ToString()))
                    {
                        break;
                    }
                    else
                    {
                        cbCurso.SelectedIndex = 0;
                    }
                }
                // hacemos lo mismo con alumno pero evitando invocar reiteradas veces el evento
                // de cbAlumno_SelectedIndexChanged
                List<Persona> alumnos = new List<Persona>();
                int flag = 0;
                foreach (var item in cbAlumno.Items)
                {
                    alumnos.Add((Persona)item);
                }
                for (int i = 0; i < cbAlumno.Items.Count; i++)
                {

                    if (alumnos[i].ID.ToString().Equals(AlumnoInscripcionActual.IDAlumno.ToString()))
                    {
                        flag = 1;
                        cbAlumno.SelectedIndex = i;
                        break;
                    }
                }
                if (flag == 0)
                {
                    cbAlumno.SelectedIndex = 0;
                }
            }


            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                this.btnAceptar.Text = "Guardar";
            }
            else if (Modo == ModoForm.Baja)
            {
                this.btnAceptar.Text = "Eliminar";
                this.btnAceptar.TabIndex = 1;
                this.cbCurso.Enabled = false;
                this.cbAlumno.Enabled = false;
                this.mtxtNota.ReadOnly = true;
                this.txtCondicion.ReadOnly = true;
            }
            else if (Modo == ModoForm.Consulta)
            {
                this.btnAceptar.Text = "Volver";
                this.btnAceptar.TabIndex = 1;
                this.btnCancelar.Hide();
                this.cbCurso.Enabled = false;
                this.cbAlumno.Enabled = false;
                this.mtxtNota.ReadOnly = true;
                this.txtCondicion.ReadOnly = true;
            }
            //para altas y modificaciones de administradores; y altas de alumnos
            // definimos los cursos que se listan segun el alumno(su plan) seleccionado en el combobox
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                if (Modo == ModoForm.Alta && tipoPersonaActual == Persona.TiposPersonas.Alumno)
                {
                    //si es alumno, seleccionamos su persona en el combobox para que sólo pueda
                    //insertar registros con respecto a él
                    List<Persona> alumnos = new List<Persona>();
                    int flag = 0;
                    foreach (var item in cbAlumno.Items)
                    {
                        alumnos.Add((Persona)item);
                    }
                    for (int i = 0; i < cbAlumno.Items.Count; i++)
                    {

                        if (alumnos[i].ID.ToString().Equals(PersonaActual.ID.ToString()))
                        {
                            flag = 1;
                            cbAlumno.SelectedIndex = i;
                            break;
                        }
                    }
                    if (flag == 0)
                    {
                        cbAlumno.SelectedIndex = 0;
                    }
                }
                //y despues sea alumno o no, fijamos las opciones que se muestran en el combobox
                //cursos al seleccionar cierto alumno; llamamos al evento encargado y seteamos la bandera
                //para que pase las condiciones
                comboPersonaInicialidado = 1;
                cbAlumno_SelectedIndexChanged(new Object(), new EventArgs());
            }
        }
        public override void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                AlumnoInscripcion alumnoInscripcionNegocio = new AlumnoInscripcion();
                AlumnoInscripcionActual = alumnoInscripcionNegocio;
                this.AlumnoInscripcionActual.State = BusinessEntity.States.New;
            }
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                this.AlumnoInscripcionActual.IDCurso = int.Parse(this.cbCurso.SelectedValue.ToString());
                this.AlumnoInscripcionActual.IDAlumno = int.Parse(this.cbAlumno.SelectedValue.ToString());
                this.AlumnoInscripcionActual.Condicion = this.txtCondicion.Text;
                if (this.mtxtNota.Text == "")
                {
                    this.AlumnoInscripcionActual.Nota = 0;
                }
                else 
                {
                    this.AlumnoInscripcionActual.Nota = int.Parse(this.mtxtNota.Text);
                }
                
                if (Modo == ModoForm.Modificacion)
                {
                    this.AlumnoInscripcionActual.ID = int.Parse(this.txtID.Text);
                    this.AlumnoInscripcionActual.State = BusinessEntity.States.Modified;
                }
            }
            if (Modo == ModoForm.Consulta)
            {
                this.AlumnoInscripcionActual.State = BusinessEntity.States.Unmodified;
            }
            if (Modo == ModoForm.Baja)
            {
                this.AlumnoInscripcionActual.State = BusinessEntity.States.Deleted;
            }

        }
        public override void GuardarCambios()
        {
            MapearADatos();
            AlumnoInscripcionLogic ail = new AlumnoInscripcionLogic();

            if (Modo == ModoForm.Baja)
            {
                ail.Delete(AlumnoInscripcionActual.ID);
                MessageBox.Show("Alumno inscripcion dado de baja", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            CursoLogic cl = new CursoLogic();
            Curso cursoSeleccionado = cl.GetOne(int.Parse(this.cbCurso.SelectedValue.ToString()));
            if (ModoForm.Alta == Modo)
            {
                ail.Save(AlumnoInscripcionActual, cursoSeleccionado.Cupo-1);
            }
            else if (ModoForm.Baja == Modo)
            {
                ail.Save(AlumnoInscripcionActual, cursoSeleccionado.Cupo+1);
            }
            else if (ModoForm.Modificacion == Modo)
            {
                Curso cursoAnterior = cl.GetOne(alumnoInscripcionAnterior.IDCurso);
                //actualizar curso anterior y nuevo curso seleccionado
                ail.Save(AlumnoInscripcionActual, alumnoInscripcionAnterior, cursoSeleccionado.Cupo - 1, cursoAnterior.Cupo + 1);
            }
        }
        public override bool Validar()
        {
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                if (this.mtxtNota.Text !="")
                {
                    if (int.Parse(this.mtxtNota.Text) < 0 || int.Parse(this.mtxtNota.Text) > 10)
                    {
                        MessageBox.Show("La nota ingresada no es válida", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                if (this.cbCurso.SelectedItem == null)
                {
                    Notificar("Alerta", "No hay cursos disponibles para ese alumno", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                AlumnoInscripcionLogic ail = new AlumnoInscripcionLogic();
                List<AlumnoInscripcion> alumnosInscripciones = ail.GetAll();
                if (Modo == ModoForm.Alta)
                {
                    foreach (var ai in alumnosInscripciones)
                    {
                        if (ai.IDAlumno == Convert.ToInt32(this.cbAlumno.SelectedValue.ToString()) && ai.IDCurso == Convert.ToInt32(this.cbCurso.SelectedValue.ToString()))
                        {
                            Notificar("Alerta", "El alumno ya está inscripto en ese curso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
                else if (Modo == ModoForm.Modificacion)
                {
                    foreach (var ai in alumnosInscripciones)
                    {
                        if (AlumnoInscripcionActual.IDAlumno != Convert.ToInt32(this.cbAlumno.SelectedValue.ToString()) || AlumnoInscripcionActual.IDCurso != Convert.ToInt32(this.cbCurso.SelectedValue.ToString()))
                        {
                            if (ai.IDAlumno == Convert.ToInt32(this.cbAlumno.SelectedValue.ToString()) && ai.IDCurso == Convert.ToInt32(this.cbCurso.SelectedValue.ToString()))
                            {
                                Notificar("Alerta", "El alumno ya está inscripto en ese curso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                    }
                }
                //validar cupo
                CursoLogic cl = new CursoLogic();
                int cupoCurso =cl.GetOne(Convert.ToInt32(this.cbCurso.SelectedValue.ToString())).Cupo;
                if (cupoCurso == 0)
                {
                    Notificar("Alerta", "No hay cupo disponible para ese curso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            if (this.txtCondicion.Text == "")
            {
                this.txtCondicion.Text = "Cursando";
            }
            return true;

        }

        private AlumnoInscripcion _alumnoInscripcionActual;
        public AlumnoInscripcion AlumnoInscripcionActual
        {
            get => _alumnoInscripcionActual;
            set
            {
                _alumnoInscripcionActual = value;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (Validar())
            {
                GuardarCambios();
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mtxtNota_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            if (this.mtxtNota.Text == "")
            {
                this.mtxtNota.Text = "";
            }
        }

        private void cbAlumno_SelectedIndexChanged(object sender, EventArgs e)
        {
            //para altas y modificaciones de adminitrador; y altas de docente
            // definimos los cursos que se listan al seleccionar el plan del alumno (al producirse cambios en el cbAlumno)
            if (this.comboPersonaInicialidado == 1 && (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion))
            {
                //cursos al seleccionar cierto alumno
                CursoLogic cl = new CursoLogic();
                List<Curso> cursos = new List<Curso>();
                int idAlumnoSeleccionado = int.Parse(cbAlumno.SelectedValue.ToString());
                PersonaLogic pl = new PersonaLogic();
                Persona persona = pl.GetOne(idAlumnoSeleccionado);
                //Lista de materias asociadas al plan de la persona
                MateriaLogic ml = new MateriaLogic();
                List<Materia> materias = ml.GetPorPlan(persona.IDPlan);
                cursos = cl.GetPorMaterias(materias);
                comboPersonaInicialidado = 1;
                cbCurso.DataSource = cursos;
            }
        }
    }
}
