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
    public partial class ModuloUsuarioDesktop : ApplicationForm
    {
        public ModuloUsuarioDesktop()
        {
            InitializeComponent();
        }

        public ModuloUsuarioDesktop(ModoForm modo) : this()
        {
            Modo = modo;
            MapearDeDatos();
        }
        public ModuloUsuarioDesktop(int ID, ModoForm modo) : this()
        {
            Modo = modo;
            ModuloUsuarioLogic muL = new ModuloUsuarioLogic();
            ModuloUsuarioActual = muL.GetOne(ID);
            MapearDeDatos();
        }
        public override void MapearDeDatos()
        {
            Business.Logic.ModuloLogic mod = new Business.Logic.ModuloLogic();
            cbModulo.DataSource = mod.GetAll();
            cbModulo.DisplayMember = "Descripcion";
            cbModulo.ValueMember = "ID";
            cbModulo.DropDownStyle = ComboBoxStyle.DropDownList;
            Business.Logic.UsuarioLogic usr = new Business.Logic.UsuarioLogic();
            cbUsuario.DataSource = usr.GetAll();
            cbUsuario.DisplayMember = "NombreUsuario";
            cbUsuario.ValueMember = "ID";
            cbUsuario.DropDownStyle = ComboBoxStyle.DropDownList;

            if (Modo != ModoForm.Alta)
            {
                this.txtID.Text = this.ModuloUsuarioActual.ID.ToString();
                this.chkAlta.Checked = this.ModuloUsuarioActual.PermiteAlta;
                this.chkBaja.Checked = this.ModuloUsuarioActual.PermiteBaja;
                this.chkModificacion.Checked = this.ModuloUsuarioActual.PermiteModificacion;
                this.chkConsulta.Checked = this.ModuloUsuarioActual.PermiteConsulta;
                for (int i = 0; i < cbModulo.Items.Count; i++)
                {
                    cbModulo.SelectedIndex = i;
                    if (cbModulo.SelectedValue.ToString().Equals(ModuloUsuarioActual.IdModulo.ToString()))
                    {
                        break;
                    }
                    else
                    {
                        cbModulo.SelectedIndex = 0;
                    }
                }
                for (int i = 0; i < cbUsuario.Items.Count; i++)
                {
                    cbUsuario.SelectedIndex = i;
                    if (cbUsuario.SelectedValue.ToString().Equals(ModuloUsuarioActual.IdUsuario.ToString()))
                    {
                        break;
                    }
                    else
                    {
                        cbUsuario.SelectedIndex = 0;
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
                this.cbModulo.Enabled = false;
                this.cbUsuario.Enabled = false;
                this.chkAlta.Enabled = false;
                this.chkBaja.Enabled = false;
                this.chkModificacion.Enabled = false;
                this.chkConsulta.Enabled = false;
            }
            else if (Modo == ModoForm.Consulta)
            {
                this.btnAceptar.Text = "Volver";
                this.btnAceptar.TabIndex = 1;
                this.btnCancelar.Hide();
                this.cbModulo.Enabled = false;
                this.cbUsuario.Enabled = false;
                this.chkAlta.Enabled = false;
                this.chkBaja.Enabled = false;
                this.chkModificacion.Enabled = false;
                this.chkConsulta.Enabled = false;
            }
        }
        public override void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                ModuloUsuario moduloUsuarioNegocio = new ModuloUsuario();
                ModuloUsuarioActual = moduloUsuarioNegocio;
                this.ModuloUsuarioActual.State = BusinessEntity.States.New;
            }
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                this.ModuloUsuarioActual.PermiteAlta = this.chkAlta.Checked;
                this.ModuloUsuarioActual.PermiteBaja = this.chkBaja.Checked;
                this.ModuloUsuarioActual.PermiteModificacion = this.chkModificacion.Checked;
                this.ModuloUsuarioActual.PermiteConsulta = this.chkConsulta.Checked;
                this.ModuloUsuarioActual.IdModulo = int.Parse(this.cbModulo.SelectedValue.ToString());
                this.ModuloUsuarioActual.IdUsuario = int.Parse(this.cbUsuario.SelectedValue.ToString());
                if (Modo == ModoForm.Modificacion)
                {
                    this.ModuloUsuarioActual.ID = int.Parse(this.txtID.Text);
                    this.ModuloUsuarioActual.State = BusinessEntity.States.Modified;
                }
            }
            if (Modo == ModoForm.Consulta)
            {
                this.ModuloUsuarioActual.State = BusinessEntity.States.Unmodified;
            }
            if (Modo == ModoForm.Baja)
            {
                this.ModuloUsuarioActual.State = BusinessEntity.States.Deleted;
            }

        }
        public override void GuardarCambios()
        {
            MapearADatos();
            ModuloUsuarioLogic mul = new ModuloUsuarioLogic();
            mul.Save(ModuloUsuarioActual);
            if (Modo == ModoForm.Baja)
            {
                mul.Delete(ModuloUsuarioActual.ID);
                MessageBox.Show("Modulo de usuario dado de baja", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public override bool Validar()
        {
            ModuloUsuarioLogic mul = new ModuloUsuarioLogic();
            List<ModuloUsuario> modulosUsuarios = mul.GetAll();
            if (Modo == ModoForm.Alta)
            {
                foreach (var mu in modulosUsuarios)
                {
                    if (mu.IdUsuario == Convert.ToInt32(this.cbUsuario.SelectedValue.ToString()))
                    {
                        Notificar("Alerta", "El usuario seleccionado ya tiene un modulo de usuario registrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            else if (Modo == ModoForm.Modificacion)
            {
                foreach (var mu in modulosUsuarios)
                {
                    if (ModuloUsuarioActual.IdUsuario != Convert.ToInt32(this.cbUsuario.SelectedValue.ToString()))
                    {
                        if (mu.IdUsuario == Convert.ToInt32(this.cbUsuario.SelectedValue.ToString()))
                        {
                            Notificar("Alerta", "El usuario seleccionado ya tiene un modulo de usuario registrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private ModuloUsuario _moduloUsuarioActual;
        public ModuloUsuario ModuloUsuarioActual
        {
            get => _moduloUsuarioActual;
            set
            {
                _moduloUsuarioActual = value;
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
