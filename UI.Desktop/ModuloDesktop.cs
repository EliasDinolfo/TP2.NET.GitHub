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
    public partial class ModuloDesktop : ApplicationForm
    {
        public ModuloDesktop()
        {
            InitializeComponent();
        }
        public ModuloDesktop(ModoForm modo) : this()
        {
            Modo = modo;
            MapearDeDatos();
        }
        public ModuloDesktop(int ID, ModoForm modo) : this()
        {
            Modo = modo;
            ModuloLogic mL = new ModuloLogic();
            ModuloActual = mL.GetOne(ID);
            MapearDeDatos();
        }
        public override void MapearDeDatos()
        {
            if (Modo != ModoForm.Alta)
            {
                this.txtID.Text = this.ModuloActual.ID.ToString();
                this.txtDescripcion.Text = this.ModuloActual.Descripcion;
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
            }
            else if (Modo == ModoForm.Consulta)
            {
                this.btnAceptar.Text = "Volver";
                this.btnAceptar.TabIndex = 1;
                this.btnCancelar.Hide();
                this.txtDescripcion.ReadOnly = true;
            }
        }
        public override void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                Modulo moduloNegocio = new Modulo();
                ModuloActual = moduloNegocio;
                this.ModuloActual.State = BusinessEntity.States.New;
            }
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                this.ModuloActual.Descripcion = this.txtDescripcion.Text;
                if (Modo == ModoForm.Modificacion)
                {
                    this.ModuloActual.ID = int.Parse(this.txtID.Text);
                    this.ModuloActual.State = BusinessEntity.States.Modified;
                }
            }
            if (Modo == ModoForm.Consulta)
            {
                this.ModuloActual.State = BusinessEntity.States.Unmodified;
            }
            if (Modo == ModoForm.Baja)
            {
                this.ModuloActual.State = BusinessEntity.States.Deleted;
            }

        }
        public override void GuardarCambios()
        {
            MapearADatos();
            ModuloLogic mL = new ModuloLogic();
            if (Modo == ModoForm.Baja)
            {
                mL.Delete(ModuloActual.ID);
                MessageBox.Show("Modulo dado de baja", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            mL.Save(ModuloActual);
        }
        public override bool Validar()
        {
            if(Modo==ModoForm.Alta || Modo == ModoForm.Baja)
            {
                if (this.txtDescripcion.Text == "")
                {
                    Notificar("Alerta", "Debe rellenar todos los campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            ModuloLogic ml = new ModuloLogic();
            List<Modulo> modulos = ml.GetAll();
            if (Modo == ModoForm.Alta)
            {
                foreach (var mod in modulos)
                {
                    if (mod.Descripcion == this.txtDescripcion.Text)
                    {
                        Notificar("Alerta", "El modulo ingresado ya está registrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            else if (Modo == ModoForm.Modificacion)
            {
                foreach (var mod in modulos)
                {
                    if (ModuloActual.Descripcion != this.txtDescripcion.Text)
                    {
                        if (mod.Descripcion == this.txtDescripcion.Text)
                        {
                            Notificar("Alerta", "El modulo ingresado ya está registrado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
            }
            if (Modo == ModoForm.Baja)
            {
                //Obtenemos la lista de modulos_usuarios asociados al modulo
                ModuloUsuarioLogic muL = new ModuloUsuarioLogic();
                List<ModuloUsuario> modulosUsuarios = muL.GetPorModulo(ModuloActual.ID);
                //vemos si existen modulos_usuarios asociados
                if (modulosUsuarios.Count != 0)
                {
                    Notificar("Alerta", "No puede eliminar el módulo. Hay " + modulosUsuarios.Count + " módulo/s de usuario/s asociado/s.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }


        private Modulo _moduloActual;
        public Modulo ModuloActual
        {
            get => _moduloActual;
            set
            {
                _moduloActual = value;
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
