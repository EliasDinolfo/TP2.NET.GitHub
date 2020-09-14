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
    public partial class MateriaDesktop : ApplicationForm
    {
        public MateriaDesktop()
        {
            InitializeComponent();
        }

        public MateriaDesktop(ModoForm modo) : this()
        {
            Modo = modo;
            MapearDeDatos();
        }
        public MateriaDesktop(int ID, ModoForm modo) : this()
        {
            Modo = modo;
            MateriaLogic mL = new MateriaLogic();
            MateriaActual = mL.GetOne(ID);
            MapearDeDatos();
        }
        public override void MapearDeDatos()
        {
            Business.Logic.PlanLogic plan = new Business.Logic.PlanLogic();
            cbPlan.DataSource = plan.GetAll();
            cbPlan.DisplayMember = "Descripcion";
            cbPlan.ValueMember = "ID";
            cbPlan.DropDownStyle = ComboBoxStyle.DropDownList;

            if (Modo != ModoForm.Alta)
            {
                this.txtID.Text = this.MateriaActual.ID.ToString();
                this.txtDescripcion.Text = this.MateriaActual.Descripcion;
                this.txtHSSem.Text = this.MateriaActual.HSSemanales.ToString();
                this.txtHSTot.Text = this.MateriaActual.HSTotales.ToString();
                for (int i = 0; i < cbPlan.Items.Count; i++)
                {
                    cbPlan.SelectedIndex = i;
                    if (cbPlan.SelectedValue.ToString().Equals(MateriaActual.IDPlan.ToString()))
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
            else if (Modo == ModoForm.Baja)
            {
                this.btnAceptar.Text = "Eliminar";
                this.btnAceptar.TabIndex = 1;
                this.txtDescripcion.ReadOnly = true;
                this.txtHSSem.ReadOnly = true;
                this.txtHSTot.ReadOnly = true;
                this.cbPlan.Enabled = false;
            }
            else if (Modo == ModoForm.Consulta)
            {
                this.btnAceptar.Text = "Volver";
                this.btnAceptar.TabIndex = 1;
                this.btnCancelar.Hide();
                this.txtDescripcion.ReadOnly = true;
                this.txtHSSem.ReadOnly = true;
                this.txtHSTot.ReadOnly = true;
                this.cbPlan.Enabled = false;
            }
        }
        public override void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                Materia materiaNegocio = new Materia();
                MateriaActual = materiaNegocio;
                this.MateriaActual.State = BusinessEntity.States.New;
            }
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                this.MateriaActual.Descripcion = this.txtDescripcion.Text;
                this.MateriaActual.HSSemanales = int.Parse(this.txtHSSem.Text);
                this.MateriaActual.HSTotales = int.Parse(this.txtHSTot.Text);
                this.MateriaActual.IDPlan = int.Parse(this.cbPlan.SelectedValue.ToString());
                if (Modo == ModoForm.Modificacion)
                {
                    this.MateriaActual.ID = int.Parse(this.txtID.Text);
                    this.MateriaActual.State = BusinessEntity.States.Modified;
                }
            }
            if (Modo == ModoForm.Consulta)
            {
                this.MateriaActual.State = BusinessEntity.States.Unmodified;
            }
            if (Modo == ModoForm.Baja)
            {
                this.MateriaActual.State = BusinessEntity.States.Deleted;
            }

        }
        public override void GuardarCambios()
        {
            MapearADatos();
            MateriaLogic ml = new MateriaLogic();
            if (Modo == ModoForm.Baja)
            {
                ml.Delete(MateriaActual.ID);
                MessageBox.Show("Materia dada de baja", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            ml.Save(MateriaActual);
        }
        public override bool Validar()
        {
            if (Modo==ModoForm.Alta || Modo==ModoForm.Modificacion)
            {
                if (this.txtDescripcion.Text == "" || this.txtHSSem.Text == "" || this.txtHSTot.Text == "")
                {
                    Notificar("Alerta", "Debe rellenar todos los campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (!int.TryParse(this.txtHSSem.Text, out _) || int.Parse(this.txtHSSem.Text) < 1) // out _ es una variable numérica que se descarta
                {
                    Notificar("Alerta", "Debe ingresar cantidad de horas semanales válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (!int.TryParse(this.txtHSTot.Text, out _) || int.Parse(this.txtHSTot.Text) < 1) // out _ es una variable numérica que se descarta
                {
                    Notificar("Alerta", "Debe ingresar cantidad de horas totales válida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                MateriaLogic ml = new MateriaLogic();
                List<Materia> materias = ml.GetAll();
                if (Modo == ModoForm.Alta)
                {
                    foreach (var mat in materias)
                    {
                        if (mat.Descripcion == this.txtDescripcion.Text && mat.IDPlan == Convert.ToInt32(this.cbPlan.SelectedValue.ToString()))
                        {
                            Notificar("Alerta", "La  materia ingresada ya está registrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
                else if (Modo == ModoForm.Modificacion)
                {
                    foreach (var mat in materias)
                    {
                        if (MateriaActual.Descripcion != this.txtDescripcion.Text || MateriaActual.IDPlan != Convert.ToInt32(this.cbPlan.SelectedValue.ToString()))
                        {
                            if (mat.Descripcion == this.txtDescripcion.Text && mat.IDPlan == Convert.ToInt32(this.cbPlan.SelectedValue.ToString()))
                            {
                                Notificar("Alerta", "La materia ingresada ya está registrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                    }
                }
            }
            //1- (baja)validar que la materia que se quiere eliminar no psea asociaciones con cursos
            //2- (modificacion) validar que el plan asignado anteriormente no posea cursos
            //asociadas antes de modificar el plan de la materia actual
            if (Modo == ModoForm.Baja || (ModoForm.Modificacion == Modo &&
                !MateriaActual.IDPlan.ToString().Equals(cbPlan.SelectedValue.ToString())))
            {
                //Obtenemos la lista de cursos asociados a la materia
                CursoLogic cL = new CursoLogic();
                List<Curso> cursos = cL.GetPorMateria(MateriaActual.ID);
                //vemos si existen cursos asociados
                if (cursos.Count != 0)
                {
                    if (ModoForm.Baja == Modo)
                    {
                        Notificar("Alerta", "No puede eliminar la materia. Hay " + cursos.Count + " curso/s asociado/s.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Notificar("Alerta", "No puede modificar el plan de la materia. Hay " + cursos.Count + " curso/s asociado/s.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return false;
                }
            }
            return true;
        }

        private Materia _materiaActual;
        public Materia MateriaActual
        {
            get => _materiaActual;
            set
            {
                _materiaActual = value;
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
    }
}
