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
    public partial class PlanDesktop : ApplicationForm
    {
        public PlanDesktop()
        {
            InitializeComponent();
        }

        public PlanDesktop(ModoForm modo) : this()
        {
            Modo = modo;
            MapearDeDatos();
        }
        public PlanDesktop(int ID, ModoForm modo) : this()
        {
            Modo = modo;
            PlanLogic eL = new PlanLogic();
            PlanActual = eL.GetOne(ID);
            MapearDeDatos();
        }
        public override void MapearDeDatos()
        {
            Business.Logic.EspecialidadLogic esp = new Business.Logic.EspecialidadLogic();
            cbEspecialidad.DataSource = esp.GetAll();
            cbEspecialidad.DisplayMember = "Descripcion";
            cbEspecialidad.ValueMember = "ID";
            cbEspecialidad.DropDownStyle = ComboBoxStyle.DropDownList;

            if (Modo != ModoForm.Alta)
            {
                this.txtID.Text = this.PlanActual.ID.ToString();
                this.txtDescripcion.Text = this.PlanActual.Descripcion;
                for (int i = 0; i < cbEspecialidad.Items.Count; i++)
                {
                    cbEspecialidad.SelectedIndex = i;
                    if (cbEspecialidad.SelectedValue.ToString().Equals(PlanActual.IDEspecialidad.ToString()))
                    {
                        break;
                    }
                    else
                    {
                        cbEspecialidad.SelectedIndex = 0;
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
                this.cbEspecialidad.Enabled = false;
            }
            else if (Modo==ModoForm.Consulta)
            {
                this.btnAceptar.Text = "Volver";
                this.btnAceptar.TabIndex = 1;
                this.btnCancelar.Hide();
                this.txtDescripcion.ReadOnly = true;
                this.cbEspecialidad.Enabled = false;
            }
        }
        public override void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                Plan planNegocio = new Plan();
                PlanActual = planNegocio;
                this.PlanActual.State = BusinessEntity.States.New;
            }
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                this.PlanActual.Descripcion = this.txtDescripcion.Text;
                this.PlanActual.IDEspecialidad = int.Parse(this.cbEspecialidad.SelectedValue.ToString());
                if (Modo == ModoForm.Modificacion)
                {
                    this.PlanActual.ID = int.Parse(this.txtID.Text);
                    this.PlanActual.State = BusinessEntity.States.Modified;
                }
            }
            if (Modo == ModoForm.Consulta)
            {
                this.PlanActual.State = BusinessEntity.States.Unmodified;
            }
            if (Modo == ModoForm.Baja)
            {
                this.PlanActual.State = BusinessEntity.States.Deleted;
            }

        }
        public override void GuardarCambios()
        {
            MapearADatos();
            PlanLogic pl = new PlanLogic();
            if (Modo == ModoForm.Baja)
            {
                pl.Delete(PlanActual.ID);
                MessageBox.Show("Plan dado de baja", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            pl.Save(PlanActual);
        }
        public override bool Validar()
        {
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                if (this.txtDescripcion.Text == "")
                {
                    Notificar("Alerta", "Debe rellenar todos los campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                PlanLogic pl = new PlanLogic();
                List<Plan> planes = pl.GetAll();
                if (Modo == ModoForm.Alta)
                {
                    foreach (var plan in planes)
                    {
                        if (plan.Descripcion == this.txtDescripcion.Text && plan.IDEspecialidad == Convert.ToInt32(this.cbEspecialidad.SelectedValue.ToString()))
                        {
                            Notificar("Alerta", "El plan ingresado ya está registrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
                else if (Modo == ModoForm.Modificacion)
                {
                    foreach (var plan in planes)
                    {
                        if (PlanActual.Descripcion != this.txtDescripcion.Text || PlanActual.IDEspecialidad != Convert.ToInt32(this.cbEspecialidad.SelectedValue.ToString()))
                        {
                            if (plan.Descripcion == this.txtDescripcion.Text && plan.IDEspecialidad == Convert.ToInt32(this.cbEspecialidad.SelectedValue.ToString()))
                            {
                                Notificar("Alerta", "El plan ingresado ya está registrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                    }
                }
            }
            //1- (baja)validar que el plan que se quiere eliminar no psea asociaciones con materias, personas y comisiones
            //2- (modificacion) validar que la especialidad asignada anteriormente no posea materias, personas y comisiones
            //asociadas antes de modificar la especialidad del plan actual
            if (Modo == ModoForm.Baja || (ModoForm.Modificacion == Modo && 
                !PlanActual.IDEspecialidad.ToString().Equals(cbEspecialidad.SelectedValue.ToString())))
            {
                //Obtenemos la lista de materias, comisiones y personas asociadas al plan
                string mensaje = null;
                MateriaLogic mL = new MateriaLogic();
                List<Materia> materias = mL.GetPorPlan(PlanActual.ID);
                ComisionLogic cL = new ComisionLogic();
                List<Comision> comisiones = cL.GetPorPlan(PlanActual.ID);
                PersonaLogic pL = new PersonaLogic();
                List<Persona> personas = pL.GetPorPlan(PlanActual.ID);
                //vemos si hay asociaciones
                if (personas.Count != 0 || comisiones.Count != 0 || materias.Count != 0)
                {
                    if (Modo == ModoForm.Baja)
                    {
                        mensaje += "No se puede eliminar el plan.\n";
                    }
                    else
                    {
                        mensaje += "No se puede modificar la especialidad del plan.\n";
                    }
                    if (personas.Count != 0)
                    {
                        mensaje += "Hay " + personas.Count + " persona/s asociada/s.\n";
                    }
                    if (comisiones.Count != 0)
                    {
                        mensaje += "Hay " + comisiones.Count + " comision/es asociada/s.\n";
                    }
                    if (materias.Count != 0)
                    {
                        mensaje += "Hay " + materias.Count + " materia/s asociada/s.";
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

        private Plan _planActual;
        public Plan PlanActual
        {
            get => _planActual;
            set
            {
                _planActual = value;
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
