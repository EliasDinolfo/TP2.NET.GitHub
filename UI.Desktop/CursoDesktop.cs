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
    public partial class CursoDesktop : ApplicationForm
    {
        private int comboMateriaInicialidado = 0;
        public CursoDesktop()
        {
            InitializeComponent();
        }

        public CursoDesktop(ModoForm modo) : this()
        {
            Modo = modo;
            MapearDeDatos();
        }
        public CursoDesktop(int ID, ModoForm modo) : this()
        {
            Modo = modo;
            CursoLogic cuL = new CursoLogic();
            CursoActual = cuL.GetOne(ID);
            MapearDeDatos();
        }
        public override void MapearDeDatos()
        {
            
            Business.Logic.ComisionLogic cl = new Business.Logic.ComisionLogic();
            Business.Logic.MateriaLogic ml = new Business.Logic.MateriaLogic();
            cbMateria.DisplayMember = "Descripcion";
            cbMateria.ValueMember = "ID";
            cbMateria.DataSource = ml.GetAll();
            cbMateria.DropDownStyle = ComboBoxStyle.DropDownList;
            cbComision.DataSource = cl.GetAll();
            cbComision.DisplayMember = "DataFull";
            cbComision.ValueMember = "ID";
            cbComision.DropDownStyle = ComboBoxStyle.DropDownList;
            //si no se seleccionó el botón de alta, fijamos la selección realizada en cada combo
            if (Modo != ModoForm.Alta)
            {
                this.txtID.Text = this.CursoActual.ID.ToString();
                this.txtAnio.Text = this.CursoActual.AnioCalendario.ToString();
                this.txtCupo.Text = this.CursoActual.Cupo.ToString();
                for (int i = 0; i < cbComision.Items.Count; i++)
                {
                    cbComision.SelectedIndex = i;
                    if (cbComision.SelectedValue.ToString().Equals(CursoActual.IDComision.ToString()))
                    {
                        break;
                    }
                    else
                    {
                        cbComision.SelectedIndex = 0;
                    }
                }
                // hacemos lo mismo con materia pero evitando invocar reiteradas veces el evento
                // de cbMateria_SelectedIndexChanged
                List<Materia> materias = new List<Materia>();
                int flag = 0;
                foreach (var item in cbMateria.Items)
                {
                    materias.Add((Materia)item);
                }
                for (int i = 0; i < cbMateria.Items.Count; i++)
                {

                    if (materias[i].ID.ToString().Equals(CursoActual.IDMateria.ToString()))
                    {
                        flag = 1;
                        cbMateria.SelectedIndex = i;
                        break;
                    }
                }
                if (flag == 0)
                {
                    cbMateria.SelectedIndex = 0;
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
                this.txtAnio.ReadOnly = true;
                this.txtCupo.ReadOnly = true;
                this.cbComision.Enabled = false;
                this.cbMateria.Enabled = false;
            }
            else if (Modo == ModoForm.Consulta)
            {
                this.btnAceptar.Text = "Volver";
                this.btnAceptar.TabIndex = 1;
                this.btnCancelar.Hide();
                this.txtAnio.ReadOnly = true;
                this.txtCupo.ReadOnly = true;
                this.cbComision.Enabled = false;
                this.cbMateria.Enabled = false;
            }

            //para altas y modificaciones de adminitrador
            // definimos la comisiones que se listan segun el plan de la materia seleccionada
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                int idMateriaSeleccionada = int.Parse(cbMateria.SelectedValue.ToString());
                Materia materia = ml.GetOne(idMateriaSeleccionada);
                comboMateriaInicialidado = 1;
                cbComision.DataSource = cl.GetPorPlan(materia.IDPlan);
                //para modificacion luego de asignar las comisiones que se lista, seleccionar la comision actual
                if (Modo == ModoForm.Modificacion)
                {
                    for (int i = 0; i < cbComision.Items.Count; i++)
                    {
                        cbComision.SelectedIndex = i;
                        if (cbComision.SelectedValue.ToString().Equals(CursoActual.IDComision.ToString()))
                        {
                            break;
                        }
                        else
                        {
                            cbComision.SelectedIndex = 0;
                        }
                    }
                }
            }
        }
        public override void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                Curso cursoNegocio = new Curso();
                CursoActual = cursoNegocio;
                this.CursoActual.State = BusinessEntity.States.New;
            }
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                this.CursoActual.AnioCalendario = int.Parse(this.txtAnio.Text);
                this.CursoActual.Cupo = int.Parse(this.txtCupo.Text);
                this.CursoActual.IDComision = int.Parse(this.cbComision.SelectedValue.ToString());
                this.CursoActual.IDMateria = int.Parse(this.cbMateria.SelectedValue.ToString());
                if (Modo == ModoForm.Modificacion)
                {
                    this.CursoActual.ID = int.Parse(this.txtID.Text);
                    this.CursoActual.State = BusinessEntity.States.Modified;
                }
            }
            if (Modo == ModoForm.Consulta)
            {
                this.CursoActual.State = BusinessEntity.States.Unmodified;
            }
            if (Modo == ModoForm.Baja)
            {
                this.CursoActual.State = BusinessEntity.States.Deleted;
            }

        }
        public override void GuardarCambios()
        {
            MapearADatos();
            CursoLogic cl = new CursoLogic();
            if (Modo == ModoForm.Baja)
            {
                cl.Delete(CursoActual.ID);
                MessageBox.Show("Curso dado de baja", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            cl.Save(CursoActual);
        }
        public override bool Validar()
        {
            if (Modo==ModoForm.Alta || Modo==ModoForm.Modificacion)
            {
                if (this.txtAnio.Text == "" || this.txtCupo.Text == "")
                {
                    Notificar("Alerta", "Debe rellenar todos los campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (!int.TryParse(this.txtAnio.Text, out _) || int.Parse(this.txtAnio.Text) < 0) // out _ es una variable numérica que se descarta
                {
                    Notificar("Alerta", "Debe ingresar un año válido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (!int.TryParse(this.txtCupo.Text, out _)) // out _ es una variable numérica que se descarta
                {
                    Notificar("Alerta", "Debe ingresar un cupo válido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (int.Parse(this.txtCupo.Text) < 0)
                {
                    Notificar("Alerta", "Debe ingresar un cupo mayor o igual que 0", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (this.cbComision.SelectedItem==null)
                {
                    Notificar("Alerta", "No hay comisiones disponibles para esa materia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                CursoLogic cl = new CursoLogic();
                List<Curso> cursos = cl.GetAll();
                if (Modo == ModoForm.Alta)
                {
                    foreach (var curso in cursos)
                    {
                        if (curso.AnioCalendario == int.Parse(this.txtAnio.Text) && curso.IDComision == Convert.ToInt32(this.cbComision.SelectedValue.ToString())
                            && curso.IDMateria == Convert.ToInt32(this.cbMateria.SelectedValue.ToString()))
                        {
                            Notificar("Alerta", "El curso ingresado ya está registrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
                else if (Modo == ModoForm.Modificacion)
                {
                    foreach (var curso in cursos)
                    {
                        if (CursoActual.AnioCalendario != int.Parse(this.txtAnio.Text) || CursoActual.IDComision != Convert.ToInt32(this.cbComision.SelectedValue.ToString())
                            || CursoActual.IDMateria != Convert.ToInt32(this.cbMateria.SelectedValue.ToString()))
                        {
                            if (curso.AnioCalendario == int.Parse(this.txtAnio.Text) && curso.IDComision == Convert.ToInt32(this.cbComision.SelectedValue.ToString())
                            && curso.IDMateria == Convert.ToInt32(this.cbMateria.SelectedValue.ToString()))
                            {
                                Notificar("Alerta", "El curso ingresado ya está registrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                    }
                }
            }
            //1- (baja)validar que el curso que se quiere eliminar no posea asociaciones con docentes_cursos y alumnos_inscripciones
            //2- (modificacion) validar que la materia y comision asignadas anteriormente no posean docentes_cursos y
            //akumnos_inscripciones asociados antes de modificar la materia y comision del curso actual
            if (Modo == ModoForm.Baja || (ModoForm.Modificacion == Modo &&
                (!CursoActual.IDMateria.ToString().Equals(cbMateria.SelectedValue.ToString())
                || !CursoActual.IDComision.ToString().Equals(cbComision.SelectedValue.ToString()))))
            {
                //Obtenemos la lista de comisiones y materias asociadas al curso
                string mensaje = null;
                DocenteCursoLogic dcL = new DocenteCursoLogic();
                List<DocenteCurso> docentesCursos = dcL.GetPorCurso(CursoActual.ID);
                AlumnoInscripcionLogic aiL = new AlumnoInscripcionLogic();
                List<AlumnoInscripcion> alumnosInscripciones = aiL.GetPorCurso(CursoActual.ID);
                //vemos si hay asociaciones
                if (alumnosInscripciones.Count != 0 || docentesCursos.Count != 0)
                {
                    if (ModoForm.Baja == Modo)
                    {
                        mensaje += "No se puede eliminar el curso.\n";
                    }
                    else
                    {
                        mensaje += "No se puede modificar la comisión ni la materia del curso.\n";
                    }
                    if (alumnosInscripciones.Count != 0)
                    {
                        mensaje += "Hay " + alumnosInscripciones.Count + " inscripcion/es alumno/s asociado/s.\n";
                    }
                    if (docentesCursos.Count != 0)
                    {
                        mensaje += "Hay " + docentesCursos.Count + " curso/s docente/s asociado/s.\n";
                    }
                }
                if (mensaje != null)
                {
                    Notificar("Alerta", mensaje, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        private Curso _cursoActual;
        public Curso CursoActual
        {
            get => _cursoActual;
            set
            {
                _cursoActual = value;
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

        private void cbMateria_SelectedIndexChanged(object sender, EventArgs e)
        {
            //para altas y modificaciones de adminitrador
            // definimos la comisiones que se listan segun el plan de la materia seleccionada
            if (this.comboMateriaInicialidado ==1 && (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion))
            {
                MateriaLogic ml = new MateriaLogic();
                ComisionLogic cl = new ComisionLogic();
                int idMateriaSeleccionada = int.Parse(cbMateria.SelectedValue.ToString());
                Materia materia = ml.GetOne(idMateriaSeleccionada);
                cbComision.DataSource = cl.GetPorPlan(materia.IDPlan);
            }
        }
    }
}
