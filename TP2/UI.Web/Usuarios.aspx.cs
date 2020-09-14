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
    public partial class Usuarios : Default
    {
        private Usuario Entity
        {
            get;
            set;
        }
        UsuarioLogic _logic;
        private UsuarioLogic Logic
        {
            get
            {
                if (_logic ==null)
                {
                    _logic = new UsuarioLogic();
                }
                return _logic;
            }
        }
        protected new void Page_Load(object sender, EventArgs e)
        {
            if (Default._tipoPersona == Persona.TiposPersonas.Docente || Default._tipoPersona == Persona.TiposPersonas.Alumno)
            {
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
            if (Default._tipoPersona == Persona.TiposPersonas.Docente || Default._tipoPersona == Persona.TiposPersonas.Alumno)
            {
                this.eliminarLinkButton.Visible = false;
                this.nuevoLinkButton.Visible = false;
            }
            if (Default._tipoPersona == Persona.TiposPersonas.Docente || Default._tipoPersona == Persona.TiposPersonas.Alumno)
            {
                List<Usuario> usuario = new List<Usuario>();
                usuario.Add(this.Logic.GetOne(Default._usuarioActual.ID));
                this.gridView.DataSource = usuario;
            }
            else
            {
                this.gridView.DataSource = this.Logic.GetAll();
            }
            this.gridView.DataBind();
        }
        private void LoadForm(int id)
        {
            this.Entity = this.Logic.GetOne(id);
            this.nombreUsuarioTextBox.Text = this.Entity.NombreUsuario;
            this.habilitadoCheckBox.Checked = this.Entity.Habilitado;
            if (FormMode == FormModes.Consulta && this.Entity.ID ==Default._usuarioActual.ID)
            {
                this.ClaveLabel.Visible = true;
                this.ClaveTextBox.Visible = true;
                this.ClaveTextBox.ReadOnly = true;
                this.ClaveTextBox.TextMode = TextBoxMode.SingleLine;
                this.ClaveTextBox.Text = this.Entity.Clave;
            }
            cargarPersonas();
            //seleccionar la persona que corresponde al usuario seleccionado
            for (int i = 0; i < this.personaDropDownList.Items.Count; i++)
            {
                this.personaDropDownList.SelectedIndex = i;
                if (int.Parse(this.personaDropDownList.SelectedValue)==Entity.IDPersona)
                {
                    break;
                }
                else
                {
                    this.personaDropDownList.SelectedIndex = 0;
                }
            }
        }
        private void LoadEntity(Usuario usuario)
        {
            usuario.NombreUsuario = this.nombreUsuarioTextBox.Text;
            usuario.Clave = this.ClaveTextBox.Text;
            usuario.Habilitado = this.habilitadoCheckBox.Checked;
            usuario.IDPersona = int.Parse(this.personaDropDownList.SelectedValue);
        }
        private void DeleteEntity(int id)
        {
            this.Logic.Delete(id);
        }
        private void SaveEntity(Usuario usuario)
        {
            this.Logic.Save(usuario);
        }

        private void EnableForm (bool enable)
        {
            this.nombreUsuarioTextBox.Enabled = enable;
            this.personaDropDownList.Enabled = enable;
            this.habilitadoCheckBox.Enabled = enable;
            this.ClaveLabel.Visible = enable;
            this.ClaveTextBox.Visible = enable;
            this.repetirClaveLabel.Visible = enable;
            this.repetirClaveTextBox.Visible = enable;
        }
        private void ClearForm()
        {
            this.nombreUsuarioTextBox.Text = string.Empty;
            this.habilitadoCheckBox.Checked = false;
            this.ClaveTextBox.Text = string.Empty;
            cargarPersonas();
        }
        private void cargarPersonas()
        {
            PersonaLogic pl = new PersonaLogic();
            this.personaDropDownList.DataSource = pl.GetAll();
            this.personaDropDownList.DataTextField = "FullData";
            this.personaDropDownList.DataValueField = "ID";
            this.personaDropDownList.DataBind();
        }

        protected void gridView_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedID = (int)this.gridView.SelectedValue;
        }
        protected void consultaLinkButton_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                if (Default._tipoPersona == Persona.TiposPersonas.Administrador)
                {
                    this.eliminarLinkButton.Visible = true;
                    this.nuevoLinkButton.Visible = true;
                }
                this.consultaLinkButton.Visible = false;
                this.editarLinkButton.Visible = true;
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
                if (Default._tipoPersona == Persona.TiposPersonas.Administrador)
                {
                    this.eliminarLinkButton.Visible = true;
                    this.nuevoLinkButton.Visible = true;
                }
                deshacerConsulta();
                this.consultaLinkButton.Visible = true;
                this.editarLinkButton.Visible = false;
                this.cancelarLinkButton.Visible = true;
                this.lblMensaje.Text = "";
                EnableForm(true);
                this.formPanel.Visible = true;
                this.operacionActualH2.InnerText = "Operación Actual: EDICIÓN";
                this.gridView.Columns[4].Visible = false;
                this.FormMode = FormModes.Modificacion;
                this.LoadForm(this.SelectedID);
            }
        }

        protected void eliminarLinkButton_Click(object sender, EventArgs e)
        {
            if (this.IsEntitySelected)
            {
                deshacerConsulta();
                this.eliminarLinkButton.Visible = false;
                this.editarLinkButton.Visible = true;
                this.nuevoLinkButton.Visible = true;
                this.consultaLinkButton.Visible = true;
                this.formPanel.Visible = true;
                this.operacionActualH2.InnerText = "Operación Actual: ELIMINACIÓN";
                this.gridView.Columns[4].Visible = false;
                this.cancelarLinkButton.Visible = true;
                this.lblMensaje.Text = "";
                this.FormMode = FormModes.Baja;
                this.EnableForm(false);
                this.LoadForm(this.SelectedID);
            }
        }
        protected void nuevoLinkButton_Click(object sender, EventArgs e)
        {
            deshacerConsulta();
            this.nuevoLinkButton.Visible = false;
            this.editarLinkButton.Visible = true;
            this.eliminarLinkButton.Visible = true;
            this.consultaLinkButton.Visible = true;
            this.formPanel.Visible = true;
            this.cancelarLinkButton.Visible = true;
            this.lblMensaje.Text = "";
            this.operacionActualH2.InnerText = "Operación Actual: INSERCIÓN";
            this.FormMode = FormModes.Alta;
            this.ClearForm();
            this.EnableForm(true);
        }
        protected void aceptarLinkButton_Click(object sender, EventArgs e)
        {
            switch (this.FormMode)
            {
                case FormModes.Baja:
                    this.DeleteEntity(this.SelectedID);
                    this.eliminarLinkButton.Visible = true;
                    this.LoadGrid();
                    this.ViewState["SelectedID"] = null;
                    break;
                case FormModes.Modificacion:
                    this.Entity = new Usuario();
                    this.Entity.ID = this.SelectedID;
                    this.Entity.State = BusinessEntity.States.Modified;
                    this.LoadEntity(this.Entity);
                    this.SaveEntity(this.Entity);
                    this.LoadGrid();
                    this.editarLinkButton.Visible = true;
                    break;
                case FormModes.Alta:
                    this.Entity = new Usuario();
                    this.LoadEntity(this.Entity);
                    this.SaveEntity(this.Entity);
                    this.nuevoLinkButton.Visible = true;
                    this.LoadGrid();
                    //seleccionar ultimo registro insertado y llamar al evento que se ejecuta
                    //al haber una seleccion nueva a una fila
                    this.gridView.SelectedIndex = this.gridView.Rows.Count-1;
                    gridView_SelectedIndexChanged(new object(), new EventArgs());
                    break;
                case FormModes.Consulta:
                    this.LoadGrid();
                    deshacerConsulta();
                    this.consultaLinkButton.Visible = true;
                    this.cancelarLinkButton.Visible = true;
                    break;
                default:
                    break;
            }
            this.gridView.Columns[4].Visible = true;
            this.formPanel.Visible = false;
        }

        protected void cancelarLinkButton_Click(object sender, EventArgs e)
        {
            deshacerConsulta();
            this.formPanel.Visible = false;
            this.gridView.Columns[4].Visible = true;
            this.nuevoLinkButton.Visible = true;
            this.editarLinkButton.Visible = true;
            this.eliminarLinkButton.Visible = true;
            this.consultaLinkButton.Visible = true;
            this.LoadGrid();
            this.lblMensaje.Text = "";
        }
        private void deshacerConsulta()
        {
            this.ClaveTextBox.ReadOnly = false;
            this.ClaveTextBox.TextMode = TextBoxMode.Password;
            this.ClaveTextBox.Text = string.Empty;
        }
    }
}