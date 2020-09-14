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
using System.Text.RegularExpressions;

namespace UI.Desktop
{
    public partial class PersonaDesktop : ApplicationForm
    {
        public PersonaDesktop()
        {
            InitializeComponent();
        }

        public PersonaDesktop(ModoForm modo) : this()
        {
            Modo = modo;
            MapearDeDatos();
        }
        public PersonaDesktop(int ID, ModoForm modo) : this()
        {
            Modo = modo;
            PersonaLogic perL = new PersonaLogic();
            PersonaActual = perL.GetOne(ID);
            MapearDeDatos();
        }
        public override void MapearDeDatos()
        {
            Business.Logic.PlanLogic pl = new Business.Logic.PlanLogic();
            cbPlan.DataSource = pl.GetAll();
            cbPlan.DisplayMember = "Descripcion";
            cbPlan.ValueMember = "ID";
            cbPlan.DropDownStyle = ComboBoxStyle.DropDownList;

            if (Modo != ModoForm.Alta)
            {
                this.txtID.Text = this.PersonaActual.ID.ToString();
                this.txtNombre.Text = this.PersonaActual.Nombre;
                this.txtApellido.Text = this.PersonaActual.Apellido;
                this.txtDireccion.Text = this.PersonaActual.Direccion;
                this.txtEmail.Text = this.PersonaActual.Email;
                this.mtxtLegajo.Text = this.PersonaActual.Legajo.ToString();
                this.mtxtTelefono.Text = this.PersonaActual.Telefono;
                this.mtxtFechaNacimiento.Text = this.PersonaActual.FechaNacimiento.ToString();
                if (PersonaActual.TipoPersona == Persona.TiposPersonas.Alumno)
                {
                    this.rbAlumno.Checked = true;
                }
                else if (PersonaActual.TipoPersona == Persona.TiposPersonas.Docente)
                {
                    this.rbDocente.Checked = true;
                }
                else
                {
                    this.rbAdministrador.Checked = true;
                }
                for (int i = 0; i < cbPlan.Items.Count; i++)
                {
                    cbPlan.SelectedIndex = i;
                    if (cbPlan.SelectedValue.ToString().Equals(PersonaActual.IDPlan.ToString()))
                    {
                        break;
                    }
                    else
                    {
                        cbPlan.SelectedIndex = 0;
                    }
                }
                
            }
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                this.btnAceptar.Text = "Guardar";
            }
            if (Modo == ModoForm.Alta)
            {
                this.rbAlumno.Checked = true;
            }
            else if (Modo == ModoForm.Baja)
            {
                this.btnAceptar.Text = "Eliminar";
                this.btnAceptar.TabIndex = 1;
                this.txtNombre.ReadOnly = true;
                this.txtApellido.ReadOnly = true;
                this.txtDireccion.ReadOnly = true;
                this.txtEmail.ReadOnly = true;
                this.mtxtLegajo.ReadOnly = true;
                this.mtxtTelefono.ReadOnly = true;
                this.mtxtFechaNacimiento.ReadOnly = true;
                this.cbPlan.Enabled = false;
                this.gbTipoPersona.Enabled = false;
            }
            else if (Modo == ModoForm.Consulta)
            {
                this.btnAceptar.Text = "Volver";
                this.btnCancelar.Hide();
                this.btnAceptar.TabIndex = 1;
                this.txtNombre.ReadOnly = true;
                this.txtApellido.ReadOnly = true;
                this.txtDireccion.ReadOnly = true;
                this.txtEmail.ReadOnly = true;
                this.mtxtLegajo.ReadOnly = true;
                this.mtxtTelefono.ReadOnly = true;
                this.mtxtFechaNacimiento.ReadOnly = true;
                this.cbPlan.Enabled = false;
                this.gbTipoPersona.Enabled = false;
            }
        }
        public override void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                Persona personaNegocio = new Persona();
                PersonaActual = personaNegocio;
                this.PersonaActual.State = BusinessEntity.States.New;
            }
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                this.PersonaActual.Nombre = this.txtNombre.Text;
                this.PersonaActual.Apellido = this.txtApellido.Text;
                this.PersonaActual.Direccion = this.txtDireccion.Text;
                this.PersonaActual.Email = this.txtEmail.Text;
                this.PersonaActual.Legajo = int.Parse(this.mtxtLegajo.Text);
                this.PersonaActual.Telefono = this.mtxtTelefono.Text;
                this.PersonaActual.FechaNacimiento = DateTime.Parse(this.mtxtFechaNacimiento.Text);
                this.PersonaActual.IDPlan = int.Parse(this.cbPlan.SelectedValue.ToString());
                if (rbDocente.Checked)
                {
                    this.PersonaActual.TipoPersona = Persona.TiposPersonas.Docente;
                }
                else if (rbAlumno.Checked)
                {
                    this.PersonaActual.TipoPersona = Persona.TiposPersonas.Alumno;
                }
                else
                {
                    this.PersonaActual.TipoPersona = Persona.TiposPersonas.Administrador;
                }
                if (Modo == ModoForm.Modificacion)
                {
                    this.PersonaActual.ID = int.Parse(this.txtID.Text);
                    this.PersonaActual.State = BusinessEntity.States.Modified;
                }
            }
            if (Modo == ModoForm.Consulta)
            {
                this.PersonaActual.State = BusinessEntity.States.Unmodified;
            }
            if (Modo == ModoForm.Baja)
            {
                this.PersonaActual.State = BusinessEntity.States.Deleted;
            }

        }
        public override void GuardarCambios()
        {
            MapearADatos();
            PersonaLogic perl = new PersonaLogic();

            if (Modo == ModoForm.Baja)
            {
                perl.Delete(PersonaActual.ID);
                MessageBox.Show("Persona dada de baja", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            perl.Save(PersonaActual);
        }
        public override bool Validar()
        {
            if (Modo==ModoForm.Alta || Modo==ModoForm.Modificacion)
            {
                if (this.txtNombre.Text == "" || this.txtApellido.Text == "" || this.txtEmail.Text == "" ||
                this.txtDireccion.Text == "" || this.mtxtLegajo.Text == "" || this.mtxtTelefono.Text == "" ||
                this.mtxtFechaNacimiento.Text == "")
                {
                    Notificar("Alerta", "Debe rellenar todos los campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                 @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                Regex re = new Regex(strRegex);
                if (!re.IsMatch(this.txtEmail.Text))
                {
                    Notificar("Alerta", "Debe ingresar un email válido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                DateTime fechaMinima = new DateTime(1753, 1, 2);
                DateTime fechaMaxima = new DateTime(9999,12,30);
                if (!DateTime.TryParse(this.mtxtFechaNacimiento.Text, out _) || DateTime.Parse(this.mtxtFechaNacimiento.Text)<fechaMinima ||
                    DateTime.Parse(this.mtxtFechaNacimiento.Text) > fechaMaxima)
                {
                    Notificar("Alerta", "Debe ingresar una fecha de nacimiento válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (int.Parse(this.mtxtLegajo.Text) <10000 || int.Parse(this.mtxtLegajo.Text)>99999)
                {
                    Notificar("Alerta", "El legajo ingresado no es válido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                strRegex = "^[0-9]{10}$";
                re = new Regex(strRegex);
                if (!re.IsMatch(this.mtxtTelefono.Text))
                {
                    Notificar("Alerta", "El teléfono ingresado no es válido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                PersonaLogic pl = new PersonaLogic();
                List<Persona> personas = pl.GetAll();
                Persona.TiposPersonas tipo = Persona.TiposPersonas.Administrador;
                if (this.rbDocente.Checked)
                {
                    tipo = Persona.TiposPersonas.Docente;
                }
                else if (this.rbAlumno.Checked)
                {
                    tipo = Persona.TiposPersonas.Alumno;
                }
                if (Modo == ModoForm.Alta)
                {
                    foreach (var per in personas)
                    {
                        if (per.Legajo == int.Parse(this.mtxtLegajo.Text) && per.IDPlan == Convert.ToInt32(this.cbPlan.SelectedValue.ToString()) && 
                            per.TipoPersona == tipo )
                        {
                            Notificar("Alerta", "La persona ingresada ya está registrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
                else if (Modo == ModoForm.Modificacion)
                {
                    foreach (var per in personas)
                    {
                        if (PersonaActual.Legajo != int.Parse(this.mtxtLegajo.Text) || PersonaActual.IDPlan != Convert.ToInt32(this.cbPlan.SelectedValue.ToString())
                            || PersonaActual.TipoPersona != tipo)
                        {
                            if (per.Legajo == int.Parse(this.mtxtLegajo.Text) && per.IDPlan == Convert.ToInt32(this.cbPlan.SelectedValue.ToString())
                                && per.TipoPersona == tipo)
                            {
                                Notificar("Alerta", "La persona ingresada ya está registrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                    }
                }
            }
            //1- (baja)validar que la persona que se quiere eliminar no posea asociaciones con docentes_cursos, usuarios y alumnos_inscripciones
            //2- (modificacion) validar que el plan asignado anteriormente no posea docentes_cursos y alumnos_inscripciones
            //asociados antes de modificar el plan de la persona actual
            if (Modo == ModoForm.Baja || (ModoForm.Modificacion == Modo &&
                !PersonaActual.IDPlan.ToString().Equals(cbPlan.SelectedValue.ToString())))
            {
                //Obtenemos la lista de usuarios, alumnos_inscripciones y docentes_cursos asociadas a la persona
                string mensaje = null;
                UsuarioLogic uL = new UsuarioLogic();
                List<Usuario> usuarios = new List<Usuario>();
                if (Modo == ModoForm.Baja)
                {
                    usuarios = uL.GetPorPersona(PersonaActual.ID);
                }
                AlumnoInscripcionLogic aiL = new AlumnoInscripcionLogic();
                List<AlumnoInscripcion> alumnosInscripciones = aiL.GetPorPersona(PersonaActual.ID);
                DocenteCursoLogic dcL = new DocenteCursoLogic();
                List<DocenteCurso> docentesCursos = dcL.GetPorPersona(PersonaActual.ID);
                //vemos si hay asociaciones
                if (alumnosInscripciones.Count != 0 || docentesCursos.Count != 0 || usuarios.Count != 0)
                {
                    if (Modo == ModoForm.Baja)
                    {
                        mensaje += "No se puede eliminar la persona.\n";
                    }
                    else
                    {
                        mensaje += "No se puede modificar el plan de la persona.\n";
                    }
                    if (usuarios.Count != 0)
                    {
                        mensaje += "Hay " + usuarios.Count + " usuario/s asociado/s.\n";
                    }
                    if (alumnosInscripciones.Count != 0)
                    {
                        mensaje += "Hay " + alumnosInscripciones.Count + " inscripcion/es alumno/s asociado/s.\n";
                    }
                    if (docentesCursos.Count != 0)
                    {
                        mensaje += "Hay " + docentesCursos.Count + " curso/s docente/s asociado/s.";
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

        private Persona _personaActual;
        public Persona PersonaActual
        {
            get => _personaActual;
            set
            {
                _personaActual = value;
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

        private void mtxtTelefono_Click(object sender, EventArgs e)
        {
            if (this.mtxtTelefono.Text=="")
            {
                this.mtxtTelefono.Text = "";
            }
        }

        private void mtxtLegajo_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            if (this.mtxtLegajo.Text == "")
            {
                this.mtxtLegajo.Text = "";
            }
        }
    }
}
