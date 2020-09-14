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
    public partial class EspecialidadDesktop : ApplicationForm
    {
        public EspecialidadDesktop()
        {
            InitializeComponent();
        }

        public EspecialidadDesktop(ModoForm modo) : this()
        {
            Modo = modo;
            MapearDeDatos();
        }
        public EspecialidadDesktop(int ID, ModoForm modo) : this()
        {
            Modo = modo;
            EspecialidadLogic eL = new EspecialidadLogic();
            EspecialidadActual = eL.GetOne(ID);

            MapearDeDatos();
        }
        public override void MapearDeDatos()
        {
            if (Modo != ModoForm.Alta)
            {
                this.txtID.Text = this.EspecialidadActual.ID.ToString();
                this.txtDescripcion.Text = this.EspecialidadActual.Descripcion;
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
            else if (Modo==ModoForm.Consulta)
            {
                this.btnAceptar.Text = "Volver";
                this.btnCancelar.Hide();
                this.btnAceptar.TabIndex = 1;
                this.txtDescripcion.ReadOnly = true;
            }
        }
        public override void MapearADatos()
        {
            if (Modo == ModoForm.Alta)
            {
                Especialidad especialidadNegocio = new Especialidad();
                EspecialidadActual = especialidadNegocio;
                this.EspecialidadActual.State = BusinessEntity.States.New;
            }
            if (Modo == ModoForm.Alta || Modo == ModoForm.Modificacion)
            {
                this.EspecialidadActual.Descripcion = this.txtDescripcion.Text;
                if (Modo == ModoForm.Modificacion)
                {
                    this.EspecialidadActual.ID = int.Parse(this.txtID.Text);
                    this.EspecialidadActual.State = BusinessEntity.States.Modified;
                }
            }
            if (Modo == ModoForm.Consulta)
            {
                this.EspecialidadActual.State = BusinessEntity.States.Unmodified;
            }
            if (Modo == ModoForm.Baja)
            {
                this.EspecialidadActual.State = BusinessEntity.States.Deleted;
            }

        }
        public override void GuardarCambios()
        {
            MapearADatos();
            EspecialidadLogic eL = new EspecialidadLogic();
            if (Modo == ModoForm.Baja)
            {
                eL.Delete(EspecialidadActual.ID);
                MessageBox.Show("Especialidad dada de baja", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            eL.Save(EspecialidadActual);
        }
        public override bool Validar()
        {
            if (Modo==ModoForm.Alta || Modo==ModoForm.Modificacion)
            {
                if (this.txtDescripcion.Text == "")
                {
                    Notificar("Alerta", "Debe rellenar todos los campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                EspecialidadLogic el = new EspecialidadLogic();
                List<Especialidad> especialidades = el.GetAll();
                if (Modo == ModoForm.Alta)
                {
                    foreach (var esp in especialidades)
                    {
                        if (esp.Descripcion == this.txtDescripcion.Text)
                        {
                            Notificar("Alerta", "La especialidad ingresada ya está registrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
                else if (Modo == ModoForm.Modificacion)
                {
                    foreach (var esp in especialidades)
                    {
                        if (EspecialidadActual.Descripcion != this.txtDescripcion.Text)
                        {
                            if (esp.Descripcion == this.txtDescripcion.Text)
                            {
                                Notificar("Alerta", "La especialidad ingresada ya está registrada", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                        }
                    }
                }
            }
            if (Modo==ModoForm.Baja)
            {
                //Obtenemos la lista de planes asociados a la especialidad
                PlanLogic pL = new PlanLogic();
                List<Plan> planes = pL.GetPorEspecialidad(EspecialidadActual.ID);
                //vemos si existen planes asociados
                if (planes.Count!=0)
                {
                    Notificar("Alerta", "No puede eliminar la especialidad. Hay "+planes.Count+" plan/es asociado/s.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        private Especialidad _especialidadActual;
        public Especialidad EspecialidadActual
        {
            get => _especialidadActual;
            set
            {
                _especialidadActual = value;
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
