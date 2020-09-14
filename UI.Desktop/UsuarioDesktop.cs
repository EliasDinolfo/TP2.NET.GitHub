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
    public partial class UsuarioDesktop : ApplicationForm
    {
        Persona.TiposPersonas _tipoPersona;
        public Persona.TiposPersonas TipoPersona
        {
            get { return _tipoPersona; }
            set { _tipoPersona = value; }
        }
        public UsuarioDesktop()
        {
            InitializeComponent();
        }
        public UsuarioDesktop(ModoForm modo) : this()
        {
            TipoPersona = Persona.TiposPersonas.Administrador;
            Modo = modo;
            MapearDeDatos();
        }
        public UsuarioDesktop(int ID, ModoForm modo, Persona.TiposPersonas tipoPersona) : this()
        {
            Modo = modo;
            TipoPersona = tipoPersona;
            UsuarioLogic uL = new UsuarioLogic();
            UsuarioActual = uL.GetOne(ID);
            MapearDeDatos();
        }
        public override void MapearDeDatos() 
        {
            Business.Logic.PersonaLogic per = new Business.Logic.PersonaLogic();
            cbPersona.DataSource = per.GetAll();
            cbPersona.DisplayMember = "FullData";
            cbPersona.ValueMember = "ID";
            cbPersona.DropDownStyle = ComboBoxStyle.DropDownList;
            if (TipoPersona == Persona.TiposPersonas.Alumno || TipoPersona == Persona.TiposPersonas.Docente)
            {
                this.chkHabilitado.Visible = false;
                this.cbPersona.Enabled = false;
            }
            else if (TipoPersona == Persona.TiposPersonas.Administrador && Modo != ModoForm.Alta)
            {
                this.txtClave.Visible = false;
                this.lblClave.Visible = false;
                this.lblConfirmarClave.Visible = false;
                this.txtConfirmarClave.Visible = false;
            }
            if (Modo != ModoForm.Alta)
            { 
                this.txtID.Text = this.UsuarioActual.ID.ToString();
                this.chkHabilitado.Checked = this.UsuarioActual.Habilitado;
                this.txtUsuario.Text = this.UsuarioActual.NombreUsuario;
                this.txtClave.Text = this.UsuarioActual.Clave;
                this.txtConfirmarClave.Text = this.UsuarioActual.Clave;
                for (int i = 0; i < cbPersona.Items.Count; i++)
                {
                    cbPersona.SelectedIndex = i;
                    if (cbPersona.SelectedValue.ToString().Equals(UsuarioActual.IDPersona.ToString()))
                    {
                        break;
                    }
                    else
                    {
                        cbPersona.SelectedIndex = 0;
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
                this.btnAceptar.TabIndex = 0;
                this.txtUsuario.ReadOnly = true;
                this.txtClave.ReadOnly = true;
                this.txtConfirmarClave.Hide();
                this.lblConfirmarClave.Hide();
                this.chkHabilitado.Enabled = false;
                this.cbPersona.Enabled = false;
            }
            else if (Modo == ModoForm.Consulta)
            {
                this.btnAceptar.Text = "Volver";
                this.btnAceptar.TabIndex = 0;
                this.btnCancelar.Hide();
                this.txtUsuario.ReadOnly = true;
                this.txtUsuario.ReadOnly = true;
                this.txtClave.ReadOnly = true;
                this.txtConfirmarClave.Hide();
                this.lblConfirmarClave.Hide();
                this.chkHabilitado.Enabled = false;
                this.cbPersona.Enabled = false;
                if (TipoPersona == Persona.TiposPersonas.Alumno || TipoPersona == Persona.TiposPersonas.Docente)
                {
                    this.chkVer.Visible = true;
                }
            }
        }
        public override void MapearADatos() 
        {
            if (Modo == ModoForm.Alta)
            {
                Usuario usuarioNegocio = new Usuario();
                UsuarioActual = usuarioNegocio;
                this.UsuarioActual.State = BusinessEntity.States.New;
            }
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                this.UsuarioActual.Habilitado = this.chkHabilitado.Checked;
                this.UsuarioActual.NombreUsuario = this.txtUsuario.Text;
                this.UsuarioActual.Clave = this.txtClave.Text;
                this.UsuarioActual.IDPersona = int.Parse(this.cbPersona.SelectedValue.ToString());
                if (Modo == ModoForm.Modificacion)
                {
                    this.UsuarioActual.ID = int.Parse(this.txtID.Text);
                    this.UsuarioActual.State = BusinessEntity.States.Modified;
                }
            }
            if (Modo == ModoForm.Consulta)
            {
                this.UsuarioActual.State = BusinessEntity.States.Unmodified;
            }
            if (Modo == ModoForm.Baja)
            {
                 this.UsuarioActual.State = BusinessEntity.States.Deleted;
            }
            
        }
        public override void GuardarCambios() 
        {
            MapearADatos();
            UsuarioLogic uL = new UsuarioLogic();
            if (Modo == ModoForm.Baja)
            {
                uL.Delete(UsuarioActual.ID);
                MessageBox.Show("Usuario dado de baja", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            uL.Save(UsuarioActual);
        }
        public override bool Validar() 
        {
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                if (this.txtUsuario.Text == "" || this.txtClave.Text == "" || this.txtConfirmarClave.Text == "")
                {
                    Notificar("Alerta", "Debe rellenar todos los campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                else if (this.txtConfirmarClave.Text != this.txtClave.Text)
                {
                    Notificar("Alerta", "Las claves ingresadas no coinciden", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else if (this.txtClave.Text.Length < 8)
                {
                    Notificar("Alerta", "La clave debe tener por lo menos 8 carácteres", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                UsuarioLogic ul = new UsuarioLogic();
                List<Usuario> usuarios = ul.GetAll();
                if (Modo == ModoForm.Alta)
                {
                    foreach (var usr in usuarios)
                    {
                        if (usr.NombreUsuario == this.txtUsuario.Text)
                        {
                            Notificar("Alerta", "El nombre de usuario ingresado ya está registrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
                else if (Modo==ModoForm.Modificacion)
                {
                    foreach (var usr in usuarios)
                    {
                        if (UsuarioActual.NombreUsuario!=this.txtUsuario.Text)
                        {
                            if (usr.NombreUsuario == this.txtUsuario.Text)
                            {
                                Notificar("Alerta", "El nombre de usuario ingresado ya está registrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                    }
                }
            }
            // validar que la persona seleccionada no posea ya un usuario creado
            UsuarioLogic ul2 = new UsuarioLogic();
            List<Usuario> usuarios2 = ul2.GetAll();
            if (Modo == ModoForm.Alta)
            {
                foreach (var usr in usuarios2)
                {
                    if (usr.IDPersona == Convert.ToInt32(this.cbPersona.SelectedValue.ToString()))
                    {
                        Notificar("Alerta", "La persona seleccionada ya tiene un usuario creado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            else if (Modo == ModoForm.Modificacion)
            {
                foreach (var usr in usuarios2)
                {
                    if (UsuarioActual.IDPersona != Convert.ToInt32(this.cbPersona.SelectedValue.ToString()))
                    {
                        if (usr.IDPersona == Convert.ToInt32(this.cbPersona.SelectedValue.ToString()))
                        {
                            Notificar("Alerta", "La persona seleccionada ya tiene un usuario creado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }
            if (Modo == ModoForm.Baja)
            {
                //Obtenemos la lista de modulos_usuarios asociados al usuario
                ModuloUsuarioLogic muL = new ModuloUsuarioLogic();
                List<ModuloUsuario> modulosUsuarios = muL.GetPorUsuario(UsuarioActual.ID);
                //vemos si existen modulos_usuarios asociados
                if (modulosUsuarios.Count != 0)
                {
                    Notificar("Alerta", "No puede eliminar el usuario. Hay " + modulosUsuarios.Count + " modulo/s de usuario/s asociado/s.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true; 
        }

        private Usuario _usuarioActual;
        public Usuario UsuarioActual
        {
            get => _usuarioActual;
            set
            {
                _usuarioActual = value;
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

        private void chkVer_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkVer.Checked)
            {
                this.txtClave.PasswordChar = '\0';
            }
            else
            {
                this.txtClave.PasswordChar = '*';
            }
        }
    }
}
