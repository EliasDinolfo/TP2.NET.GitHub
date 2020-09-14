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
    public partial class ComisionDesktop : ApplicationForm
    {
        public ComisionDesktop()
        {
            InitializeComponent();
        }

        public ComisionDesktop(ModoForm modo) : this()
        {
            Modo = modo;
            MapearDeDatos();
        }
        public ComisionDesktop(int ID, ModoForm modo) : this()
        {
            Modo = modo;
            ComisionLogic cL = new ComisionLogic();
            ComisionActual = cL.GetOne(ID);
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
                this.txtID.Text = this.ComisionActual.ID.ToString();
                this.txtDescripcion.Text = this.ComisionActual.Descripcion;
                this.txtAnio.Text = this.ComisionActual.AnioEspecialidad.ToString();
                for (int i = 0; i < cbPlan.Items.Count; i++)
                {
                    cbPlan.SelectedIndex = i;
                    if (cbPlan.SelectedValue.ToString().Equals(ComisionActual.IDPlan.ToString()))
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
                this.txtAnio.ReadOnly = true;
                this.cbPlan.Enabled = false;
            }
            else if (Modo==ModoForm.Consulta)
            {
                this.btnAceptar.Text = "Volver";
                this.btnCancelar.Hide();
                this.btnAceptar.TabIndex = 1;
                this.txtDescripcion.ReadOnly = true;
                this.txtAnio.ReadOnly = true;
                this.cbPlan.Enabled = false;
            }
        }
        public override void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                Comision comisionNegocio = new Comision();
                ComisionActual = comisionNegocio;
                this.ComisionActual.State = BusinessEntity.States.New;
            }
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                this.ComisionActual.Descripcion = this.txtDescripcion.Text;
                this.ComisionActual.AnioEspecialidad = int.Parse(this.txtAnio.Text);
                this.ComisionActual.IDPlan = int.Parse(this.cbPlan.SelectedValue.ToString());
                if (Modo == ModoForm.Modificacion)
                {
                    this.ComisionActual.ID = int.Parse(this.txtID.Text);
                    this.ComisionActual.State = BusinessEntity.States.Modified;
                }
            }
            if (Modo == ModoForm.Consulta)
            {
                this.ComisionActual.State = BusinessEntity.States.Unmodified;
            }
            if (Modo == ModoForm.Baja)
            {
                this.ComisionActual.State = BusinessEntity.States.Deleted;
            }

        }
        public override void GuardarCambios()
        {
            MapearADatos();
            ComisionLogic coml = new ComisionLogic();
            if (Modo == ModoForm.Baja)
            {
                coml.Delete(ComisionActual.ID);
                MessageBox.Show("Comision dada de baja", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            coml.Save(ComisionActual);
        }
        public override bool Validar()
        {
            if(Modo==ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                if (this.txtDescripcion.Text == "" || this.txtAnio.Text == "")
                {
                    Notificar("Alerta", "Debe rellenar todos los campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                if (!int.TryParse(this.txtAnio.Text, out _) || int.Parse(this.txtAnio.Text) < 0) // out _ es una variable numérica que se descarta
                {
                    Notificar("Alerta", "Debe ingresar un año válido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                ComisionLogic cl = new ComisionLogic();
                List<Comision> comisiones = cl.GetAll();
                if (Modo == ModoForm.Alta)
                {
                    foreach (var com in comisiones)
                    {
                        if (com.Descripcion == this.txtDescripcion.Text && com.IDPlan == Convert.ToInt32(this.cbPlan.SelectedValue.ToString())
                            && com.AnioEspecialidad == int.Parse(txtAnio.Text))
                        {
                            Notificar("Alerta", "La comisión ingresada ya está registrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
                else if (Modo == ModoForm.Modificacion)
                {
                    foreach (var com in comisiones)
                    {
                        if (ComisionActual.Descripcion != this.txtDescripcion.Text || ComisionActual.IDPlan != Convert.ToInt32(this.cbPlan.SelectedValue.ToString())
                            || ComisionActual.AnioEspecialidad != int.Parse(this.txtAnio.Text))
                        {
                            if (com.Descripcion == this.txtDescripcion.Text && com.IDPlan == Convert.ToInt32(this.cbPlan.SelectedValue.ToString())
                            && com.AnioEspecialidad == int.Parse(txtAnio.Text))
                            {
                                Notificar("Alerta", "La comisión ingresada ya está registrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                    }
                }
            }
            //1- (baja)validar que la comision que se quiere eliminar no posea asociaciones con cursos
            //2- (modificacion) validar que el plan asignado anteriormente no posea cursos
            //asociados antes de modificar el plan de la comision actual
            if (Modo == ModoForm.Baja || (ModoForm.Modificacion == Modo &&
                !ComisionActual.IDPlan.ToString().Equals(cbPlan.SelectedValue.ToString())))
            {
                //Obtenemos la lista de cursos asociados a la comision
                CursoLogic cL = new CursoLogic();
                List<Curso> cursos = cL.GetPorComision(ComisionActual.ID);
                //vemos si existen cursos asociados
                if (cursos.Count != 0)
                {
                    if (Modo == ModoForm.Baja)
                    {
                        Notificar("Alerta", "No puede eliminar la comisión. Hay " + cursos.Count + " curso/s asociado/s.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Notificar("Alerta", "No se puede modificar el plan de la comisión. Hay " + cursos.Count + " curso/s asociado/s.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return false;
                }
            }
            return true;
        }

        private Comision _comisionActual;
        public Comision ComisionActual
        {
            get => _comisionActual;
            set
            {
                _comisionActual = value;
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
