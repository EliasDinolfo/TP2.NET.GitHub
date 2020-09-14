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
    public partial class DocenteCursoDesktop : ApplicationForm
    {
        protected Persona.TiposPersonas tipoPersonaActual;
        protected Persona PersonaActual;
        protected int comboPersonaInicialidado = 0;
        public DocenteCursoDesktop()
        {
            InitializeComponent();
        }
        public DocenteCursoDesktop(int ID, ModoForm modo, Persona.TiposPersonas tipoPersona, Persona persona) : this()
        {
            Modo = modo;
            tipoPersonaActual = tipoPersona;
            PersonaLogic pl = new PersonaLogic();
            PersonaActual = pl.GetOne(persona.ID);
            if (Modo != ModoForm.Alta)
            {
                DocenteCursoLogic dcL = new DocenteCursoLogic();
                DocenteCursoActual = dcL.GetOne(ID);
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
            cbDocente.DataSource = per.GetAllDocentes();
            cbDocente.DisplayMember = "FullData";
            cbDocente.ValueMember = "ID";
            cbDocente.DropDownStyle = ComboBoxStyle.DropDownList;
            if (tipoPersonaActual == Persona.TiposPersonas.Docente)
            {
                this.cbDocente.Enabled = false;
            }
            if (Modo != ModoForm.Alta)
            {
                this.txtID.Text = this.DocenteCursoActual.ID.ToString();
                if (DocenteCursoActual.Cargo == DocenteCurso.TiposCargos.Docente)
                {
                    this.rbDocente.Checked = true;
                }
                else
                {
                    this.rbAuxiliar.Checked = true;
                }
                for (int i = 0; i < cbCurso.Items.Count; i++)
                {
                    cbCurso.SelectedIndex = i;
                    if (cbCurso.SelectedValue.ToString().Equals(DocenteCursoActual.IDCurso.ToString()))
                    {
                        break;
                    }
                    else
                    {
                        cbCurso.SelectedIndex = 0;
                    }
                }
                // hacemos lo mismo con docente pero evitando invocar reiteradas veces el evento
                // de cbAlumno_SelectedIndexChanged
                List<Persona> docentes = new List<Persona>();
                int flag = 0;
                foreach (var item in cbDocente.Items)
                {
                    docentes.Add((Persona)item);
                }
                for (int i = 0; i < cbDocente.Items.Count; i++)
                {

                    if (docentes[i].ID.ToString().Equals(DocenteCursoActual.IDDocente.ToString()))
                    {
                        flag = 1;
                        cbDocente.SelectedIndex = i;
                        break;
                    }
                }
                if (flag == 0)
                {
                    cbDocente.SelectedIndex = 0;
                }
            }
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                this.btnAceptar.Text = "Guardar";
            }
            if (Modo == ModoForm.Alta)
            {
                this.rbDocente.Checked = true;
            }
            else if (Modo == ModoForm.Baja)
            {
                this.btnAceptar.Text = "Eliminar";
                this.btnAceptar.TabIndex = 1;
                this.cbCurso.Enabled = false;
                this.cbDocente.Enabled = false;
                this.gbCargo.Enabled = false;
            }
            else if (Modo == ModoForm.Consulta)
            {
                this.btnAceptar.Text = "Volver";
                this.btnAceptar.TabIndex = 1;
                this.btnCancelar.Hide();
                this.cbCurso.Enabled = false;
                this.cbDocente.Enabled = false;
                this.gbCargo.Enabled = false;
            }
            //para altas y modificaciones de administradores; y altas de docentes
            // definimos los cursos que se listan segun el docente(su plan) seleccionado en el combobox
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                if (Modo == ModoForm.Alta && tipoPersonaActual == Persona.TiposPersonas.Docente)
                {
                    //si es docente, seleccionamos su persona en el combobox para que sólo pueda
                    //insertar registros con respecto a él
                    List<Persona> docentes = new List<Persona>();
                    int flag = 0;
                    foreach (var item in cbDocente.Items)
                    {
                        docentes.Add((Persona)item);
                    }
                    for (int i = 0; i < cbDocente.Items.Count; i++)
                    {

                        if (docentes[i].ID.ToString().Equals(PersonaActual.ID.ToString()))
                        {
                            flag = 1;
                            cbDocente.SelectedIndex = i;
                            break;
                        }
                    }
                    if (flag == 0)
                    {
                        cbDocente.SelectedIndex = 0;
                    }
                }
                //y despues sea docente o no, fijamos las opciones que se muestran en el combobox
                //cursos al seleccionar cierto alumno; llamamos al evento encargado y seteamos la bandera
                //para que pase las condiciones
                comboPersonaInicialidado = 1;
                cbDocente_SelectedIndexChanged(new Object(), new EventArgs());
            }
        }
        public override void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                DocenteCurso docenteCursoNegocio = new DocenteCurso();
                DocenteCursoActual = docenteCursoNegocio;
                this.DocenteCursoActual.State = BusinessEntity.States.New;
            }
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                this.DocenteCursoActual.IDCurso = int.Parse(this.cbCurso.SelectedValue.ToString());
                this.DocenteCursoActual.IDDocente = int.Parse(this.cbDocente.SelectedValue.ToString());
                if (rbDocente.Checked)
                {
                    this.DocenteCursoActual.Cargo = DocenteCurso.TiposCargos.Docente;
                }
                else
                {
                    this.DocenteCursoActual.Cargo = DocenteCurso.TiposCargos.Auxiliar;
                }
                if (Modo == ModoForm.Modificacion)
                {
                    this.DocenteCursoActual.ID = int.Parse(this.txtID.Text);
                    this.DocenteCursoActual.State = BusinessEntity.States.Modified;
                }
            }
            if (Modo == ModoForm.Consulta)
            {
                this.DocenteCursoActual.State = BusinessEntity.States.Unmodified;
            }
            if (Modo == ModoForm.Baja)
            {
                this.DocenteCursoActual.State = BusinessEntity.States.Deleted;
            }

        }
        public override void GuardarCambios()
        {
            MapearADatos();
            DocenteCursoLogic dcl = new DocenteCursoLogic();

            if (Modo == ModoForm.Baja)
            {
                dcl.Delete(DocenteCursoActual.ID);
                MessageBox.Show("Docente curso dado de baja", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            dcl.Save(DocenteCursoActual);
        }
        public override bool Validar()
        {
            DocenteCursoLogic dcl = new DocenteCursoLogic();
            List<DocenteCurso> docentesCursos = dcl.GetAll();
            if (Modo == ModoForm.Alta)
            {
                foreach (var dc in docentesCursos)
                {
                    if (dc.IDDocente == Convert.ToInt32(this.cbDocente.SelectedValue.ToString()) && dc.IDCurso == Convert.ToInt32(this.cbCurso.SelectedValue.ToString()))
                    {
                        Notificar("Alerta", "El docente ya está asignado a ese curso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            else if (Modo == ModoForm.Modificacion)
            {
                foreach (var ai in docentesCursos)
                {
                    if (DocenteCursoActual.IDDocente != Convert.ToInt32(this.cbDocente.SelectedValue.ToString()) || DocenteCursoActual.IDCurso != Convert.ToInt32(this.cbCurso.SelectedValue.ToString()))
                    {
                        if (ai.IDDocente == Convert.ToInt32(this.cbDocente.SelectedValue.ToString()) && ai.IDCurso == Convert.ToInt32(this.cbCurso.SelectedValue.ToString()))
                        {
                            Notificar("Alerta", "El docente ya está asignado a ese curso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }
            //validar que un docente no elimine la inscripción de otro docente
            else if (Modo == ModoForm.Baja && tipoPersonaActual==Persona.TiposPersonas.Docente &&
                !this.cbDocente.SelectedValue.ToString().Equals(PersonaActual.ID.ToString()))
            {
                Notificar("Alerta", "No puede eliminar la inscripción realizada por otro docente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            //validar que el puesto (auxiliar o docente) no esté ocupado para ese curso
            DocenteCurso.TiposCargos tipoDoc; 
            if (rbAuxiliar.Checked)
            {
                tipoDoc = DocenteCurso.TiposCargos.Auxiliar;
            }
            else
            {
                tipoDoc = DocenteCurso.TiposCargos.Docente;
            }
            if (Modo == ModoForm.Alta)
            {
                foreach (var dc in docentesCursos)
                {
                    if (dc.IDCurso == Convert.ToInt32(this.cbCurso.SelectedValue.ToString()) && dc.Cargo==tipoDoc )
                    {
                        Notificar("Alerta", "Ese puesto ya está ocupado para ese curso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            else if (Modo == ModoForm.Modificacion)
            {
                foreach (var dc in docentesCursos)
                {
                    if (DocenteCursoActual.IDDocente != Convert.ToInt32(this.cbDocente.SelectedValue.ToString()) || DocenteCursoActual.IDCurso != Convert.ToInt32(this.cbCurso.SelectedValue.ToString())
                        || DocenteCursoActual.Cargo!=tipoDoc)
                    {
                        if (dc.IDCurso == Convert.ToInt32(this.cbCurso.SelectedValue.ToString()) && dc.Cargo==tipoDoc)
                        {
                            Notificar("Alerta", "Ese puesto ya está ocupado para ese curso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private DocenteCurso _docenteCursoActual;
        public DocenteCurso DocenteCursoActual
        {
            get => _docenteCursoActual;
            set
            {
                _docenteCursoActual = value;
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

        private void cbDocente_SelectedIndexChanged(object sender, EventArgs e)
        {
            //para altas y modificaciones de adminitrador; y altas de docentes
            // definimos los cursos que se listan al seleccionar el plan del docente (al producirse cambios en el cbDocente)
            if (this.comboPersonaInicialidado == 1 && (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion))
            {
                //cursos al seleccionar cierto alumno
                CursoLogic cl = new CursoLogic();
                List<Curso> cursos = new List<Curso>();
                int idDocenteSeleccionado = int.Parse(cbDocente.SelectedValue.ToString());
                PersonaLogic pl = new PersonaLogic();
                Persona persona = pl.GetOne(idDocenteSeleccionado);
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
