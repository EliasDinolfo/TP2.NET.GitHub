using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Entities;
using Business.Logic;

namespace UI.Web
{
    public partial class Planes : Default
    {
        private Plan Entity
        {
            get;
            set;
        }
        PlanLogic _logic;
        private PlanLogic Logic
        {
            get
            {
                if (_logic == null)
                {
                    _logic = new PlanLogic();
                }
                return _logic;
            }
        }
        protected new void Page_Load(object sender, EventArgs e)
        {
            if (Default._tipoPersona == Persona.TiposPersonas.Docente)
            {
                this.editarLinkButton.Visible = false;
                this.eliminarLinkButton.Visible = false;
                this.nuevoLinkButton.Visible = false;
            }
            //if (this.IsPostBack)
            //{
            LoadGrid();
            //}
        }
        private void LoadGrid()
        {
            this.gridView.DataSource = this.Logic.GetAll();
            this.gridView.DataBind();
        }
        private void LoadForm(int id)
        {
            this.Entity = this.Logic.GetOne(id);
            this.txtDescripcion.Text = this.Entity.Descripcion;
            cargarEspecialidades();
            //seleccionar la especialidad que corresponde al plan seleccionado
            for (int i = 0; i < this.ddlEspecialidad.Items.Count; i++)
            {
                this.ddlEspecialidad.SelectedIndex = i;
                if (int.Parse(this.ddlEspecialidad.SelectedValue) == Entity.IDEspecialidad)
                {
                    break;
                }
                else
                {
                    this.ddlEspecialidad.SelectedIndex = 0;
                }
            }
        }
        private void LoadEntity(Plan plan)
        {
            plan.Descripcion = this.txtDescripcion.Text;
            plan.IDEspecialidad = int.Parse(this.ddlEspecialidad.SelectedValue);
        }
        private void DeleteEntity(int id)
        {
            this.Logic.Delete(id);
        }
        private void SaveEntity(Plan plan)
        {
            this.Logic.Save(plan);
        }

        private void EnableForm(bool enable)
        {
            this.txtDescripcion.Enabled = enable;
            this.ddlEspecialidad.Enabled = enable;
        }

        private void ClearForm()
        {
            this.txtDescripcion.Text = string.Empty;
            cargarEspecialidades();
        }
        private void cargarEspecialidades()
        {
            EspecialidadLogic el = new EspecialidadLogic();
            this.ddlEspecialidad.DataSource = el.GetAll();
            this.ddlEspecialidad.DataTextField = "Descripcion";
            this.ddlEspecialidad.DataValueField = "ID";
            this.ddlEspecialidad.DataBind();
        }

        protected void gridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedID = (int)this.gridView.SelectedValue;
        }

        protected void consultaLinkButton_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                this.consultaLinkButton.Visible = false;
                this.editarLinkButton.Visible = true;
                this.eliminarLinkButton.Visible = true;
                this.nuevoLinkButton.Visible = true;
                this.lblMensaje.Text = "";
                this.EnableForm(false);
                this.cancelarLinkButton.Visible = false;
                this.operacionActualH2.InnerText = "Operación Actual: CONSULTA";
                this.gridView.Columns[3].Visible = false;
                this.FormMode = FormModes.Consulta;
                this.formPanel.Visible = true;
                this.LoadForm(this.SelectedID);
            }
        }
        protected void editarLinkButton_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                this.editarLinkButton.Visible = false;
                this.eliminarLinkButton.Visible = true;
                this.nuevoLinkButton.Visible = true;
                this.consultaLinkButton.Visible = true;
                this.cancelarLinkButton.Visible = true;
                this.lblMensaje.Text = "";
                EnableForm(true);
                this.formPanel.Visible = true;
                this.operacionActualH2.InnerText = "Operación Actual: EDICIÓN";
                this.gridView.Columns[3].Visible = false;
                this.FormMode = FormModes.Modificacion;
                this.LoadForm(this.SelectedID);
            }
        }

        protected void eliminarLinkButton_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                this.eliminarLinkButton.Visible = false;
                this.editarLinkButton.Visible = true;
                this.nuevoLinkButton.Visible = true;
                this.consultaLinkButton.Visible = true;
                this.cancelarLinkButton.Visible = true;
                this.lblMensaje.Text = "";
                this.formPanel.Visible = true;
                this.operacionActualH2.InnerText = "Operación Actual: ELIMINACIÓN";
                this.gridView.Columns[3].Visible = false;
                this.FormMode = FormModes.Baja;
                this.EnableForm(false);
                this.LoadForm(this.SelectedID);
            }
        }
        protected void nuevoLinkButton_Click(object sender, EventArgs e)
        {
            this.nuevoLinkButton.Visible = false;
            this.editarLinkButton.Visible = true;
            this.eliminarLinkButton.Visible = true;
            this.consultaLinkButton.Visible = true;
            this.cancelarLinkButton.Visible = true;
            this.lblMensaje.Text = "";
            this.formPanel.Visible = true;
            this.operacionActualH2.InnerText = "Operación Actual: INSERCIÓN";
            this.FormMode = FormModes.Alta;
            this.ClearForm();
            this.EnableForm(true);
        }
        protected void aceptarLinkButton_Click(object sender, EventArgs e)
        {
            if (validar())
            {
                switch (this.FormMode)
                {
                    case FormModes.Baja:
                        this.DeleteEntity(this.SelectedID);
                        this.LoadGrid();
                        this.ViewState["SelectedID"] = null;
                        this.eliminarLinkButton.Visible = true;
                        break;
                    case FormModes.Modificacion:
                        this.Entity = new Plan();
                        this.Entity.ID = this.SelectedID;
                        this.Entity.State = BusinessEntity.States.Modified;
                        this.LoadEntity(this.Entity);
                        this.SaveEntity(this.Entity);
                        this.LoadGrid();
                        this.editarLinkButton.Visible = true;
                        break;
                    case FormModes.Alta:
                        this.Entity = new Plan();
                        this.LoadEntity(this.Entity);
                        this.SaveEntity(this.Entity);
                        this.LoadGrid();
                        //seleccionar ultimo registro insertado y llamar al evento que se ejecuta
                        //al haber una seleccion nueva a una fila
                        this.gridView.SelectedIndex = this.gridView.Rows.Count - 1;
                        gridView_SelectedIndexChanged(new object(), new EventArgs());
                        this.nuevoLinkButton.Visible = true;
                        break;
                    case FormModes.Consulta:
                        this.LoadGrid();
                        this.consultaLinkButton.Visible = true;
                        this.cancelarLinkButton.Visible = true;
                        break;
                    default:
                        break;
                }
                this.gridView.Columns[3].Visible = true;
                this.formPanel.Visible = false;
            }
        }

        protected void cancelarLinkButton_Click(object sender, EventArgs e)
        {
            this.formPanel.Visible = false;
            this.nuevoLinkButton.Visible = true;
            this.editarLinkButton.Visible = true;
            this.eliminarLinkButton.Visible = true;
            this.consultaLinkButton.Visible = true;
            this.gridView.Columns[3].Visible = true;
            this.lblMensaje.Text = "";
        }

        public bool validar()
        {
            PlanLogic pl = new PlanLogic();
            if (FormMode == FormModes.Alta || FormMode == FormModes.Modificacion)
            {
                if (this.txtDescripcion.Text == "")
                {
                    this.lblMensaje.Text = "Debe rellenar todos los campos";
                    return false;
                }
                List<Plan> planes = pl.GetAll();
                if (FormMode == FormModes.Alta)
                {
                    foreach (var plan in planes)
                    {
                        if (plan.Descripcion == this.txtDescripcion.Text && plan.IDEspecialidad == Convert.ToInt32(this.ddlEspecialidad.SelectedValue.ToString()))
                        {
                            this.lblMensaje.Text = "El plan ingresado ya está registrado";
                            return false;
                        }
                    }
                }
                else if (FormMode == FormModes.Modificacion)
                {
                    Plan planAct = pl.GetOne(this.SelectedID);
                    foreach (var plan in planes)
                    {
                        if (planAct.Descripcion != this.txtDescripcion.Text || planAct.IDEspecialidad != Convert.ToInt32(this.ddlEspecialidad.SelectedValue.ToString()))
                        {
                            if (plan.Descripcion == this.txtDescripcion.Text && plan.IDEspecialidad == Convert.ToInt32(this.ddlEspecialidad.SelectedValue.ToString()))
                            {
                                this.lblMensaje.Text = "El plan ingresado ya está registrado";
                                return false;
                            }
                        }
                    }
                }
            }
            Plan planActual = pl.GetOne(this.SelectedID);
            //1- (baja)validar que el plan que se quiere eliminar no psea asociaciones con materias, personas y comisiones
            //2- (modificacion) validar que la especialidad asignada anteriormente no posea materias, personas y comisiones
            //asociadas antes de modificar la especialidad del plan actual
            if (FormMode == FormModes.Baja || (FormModes.Modificacion == FormMode &&
                !planActual.IDEspecialidad.ToString().Equals(ddlEspecialidad.SelectedValue.ToString())))
            {
                //Obtenemos la lista de materias, comisiones y personas asociadas al plan
                string mensaje = null;
                MateriaLogic mL = new MateriaLogic();
                List<Materia> materias = mL.GetPorPlan(planActual.ID);
                ComisionLogic cL = new ComisionLogic();
                List<Comision> comisiones = cL.GetPorPlan(planActual.ID);
                PersonaLogic pL = new PersonaLogic();
                List<Persona> personas = pL.GetPorPlan(planActual.ID);
                //vemos si hay asociaciones
                if (personas.Count != 0 || comisiones.Count != 0 || materias.Count != 0)
                {
                    if (FormMode == FormModes.Baja)
                    {
                        mensaje += "No se puede eliminar el plan.<br>";
                    }
                    else
                    {
                        mensaje += "No se puede modificar la especialidad del plan.<br>";
                    }
                    if (personas.Count != 0)
                    {
                        mensaje += "Hay " + personas.Count + " persona/s asociada/s.<br>";
                    }
                    if (comisiones.Count != 0)
                    {
                        mensaje += "Hay " + comisiones.Count + " comision/es asociada/s.<br>";
                    }
                    if (materias.Count != 0)
                    {
                        mensaje += "Hay " + materias.Count + " materia/s asociada/s.";
                    }
                }
                if (mensaje != null)
                {
                    this.lblMensaje.Text = mensaje;
                    return false;
                }
            }
            return true;
        }
    }
}