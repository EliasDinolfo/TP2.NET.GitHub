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
    public partial class Especialidades : Default
    {
        Especialidad _entity;
        private Especialidad Entity
        {
            get { return _entity; }
            set { _entity = value; }
        }
        EspecialidadLogic _logic;
        private EspecialidadLogic Logic
        {
            get
            {
                if (_logic == null)
                {
                    _logic = new EspecialidadLogic();
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
        }
        private void LoadEntity(Especialidad especialidad)
        {
            especialidad.Descripcion = this.txtDescripcion.Text;
        }
        private void DeleteEntity(int id)
        {
            this.Logic.Delete(id);
        }
        private void SaveEntity(Especialidad especialidad)
        {
            this.Logic.Save(especialidad);
        }

        private void EnableForm(bool enable)
        {
            this.txtDescripcion.Enabled = enable;
        }
        private void ClearForm()
        {
            this.txtDescripcion.Text = string.Empty;
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
                this.gridView.Columns[2].Visible = false;
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
                this.gridView.Columns[2].Visible = false;
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
                this.gridView.Columns[2].Visible = false;
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
                        this.Entity = new Especialidad();
                        this.Entity.ID = this.SelectedID;
                        this.Entity.State = BusinessEntity.States.Modified;
                        this.LoadEntity(this.Entity);
                        this.SaveEntity(this.Entity);
                        this.LoadGrid();
                        this.editarLinkButton.Visible = true;
                        break;
                    case FormModes.Alta:
                        this.Entity = new Especialidad();
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
                this.gridView.Columns[2].Visible = true;
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
            this.gridView.Columns[2].Visible = true;
            this.lblMensaje.Text = "";
        }
        public bool validar()
        {
            if (FormMode == FormModes.Alta || FormMode == FormModes.Modificacion)
            {
                if (txtDescripcion.Text == "")
                {
                    this.lblMensaje.Text = "Debe rellenar todos los campos";
                    return false;
                }
                EspecialidadLogic el = new EspecialidadLogic();
                List<Especialidad> especialidades = el.GetAll();
                if (FormMode == FormModes.Alta)
                {
                    foreach (var esp in especialidades)
                    {
                        if (esp.Descripcion == this.txtDescripcion.Text)
                        {
                            this.lblMensaje.Text = "La especialidad ingresada ya está registrada";
                            return false;
                        }
                    }
                }
                else if (FormMode == FormModes.Modificacion)
                {
                    Especialidad espActual = el.GetOne(this.SelectedID);
                    foreach (var esp in especialidades)
                    {
                        if (espActual.Descripcion != this.txtDescripcion.Text)
                        {
                            if (esp.Descripcion == this.txtDescripcion.Text)
                            {
                                this.lblMensaje.Text = "La especialidad ingresada ya está registrada";
                                return false;
                            }
                        }
                    }
                }
            }
            if (FormMode == FormModes.Baja)
            {
                //Obtenemos la lista de planes asociados a la especialidad
                PlanLogic pL = new PlanLogic();
                List<Plan> planes = pL.GetPorEspecialidad(this.SelectedID);
                //vemos si existen planes asociados
                if (planes.Count != 0)
                {
                    this.lblMensaje.Text = "No puede eliminar la especialidad. Hay " + planes.Count + " plan/es asociado/s.";
                    return false;
                }
            }
            return true;
        }

        
    }
}