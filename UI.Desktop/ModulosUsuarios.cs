using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business.Entities;
using Business.Logic;

namespace UI.Desktop
{
    public partial class ModulosUsuarios : Form
    {
        public ModulosUsuarios()
        {
            InitializeComponent();
            this.dgvModulosUsuarios.AutoGenerateColumns = false;
            this.oModulosUsuarios = new Business.Entities.ModuloUsuario();
        }
        private Business.Entities.ModuloUsuario _modulosUsuarios;
        public Business.Entities.ModuloUsuario oModulosUsuarios
        {
            get { return _modulosUsuarios; }
            set { _modulosUsuarios = value; }
        }

        public void Listar()
        {
            ModuloUsuarioLogic mul = new ModuloUsuarioLogic();
            this.dgvModulosUsuarios.DataSource = mul.GetAll();
        }

        private void ModulosUsuarios_Load(object sender, EventArgs e)
        {
            Listar();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Listar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            int cantFilas = this.dgvModulosUsuarios.RowCount;
            if (Validar())
            {
                ModuloUsuarioDesktop formModuloUsuario = new ModuloUsuarioDesktop(ApplicationForm.ModoForm.Alta);
                formModuloUsuario.ShowDialog();
            }
            this.Listar();
            //si se agregó una fila, seleccionarla
            if (cantFilas != this.dgvModulosUsuarios.RowCount)
            {
                DataGridViewRow fila = this.dgvModulosUsuarios.Rows[cantFilas];
                this.dgvModulosUsuarios.CurrentCell = fila.Cells[0];
            }
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (this.dgvModulosUsuarios.SelectedRows.Count<1)
            {
                MessageBox.Show("Debe seleccionar la fila a modificar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int nroFilaSeleccionada = this.dgvModulosUsuarios.CurrentCell.RowIndex;
                int ID = ((Business.Entities.ModuloUsuario)this.dgvModulosUsuarios.SelectedRows[0].DataBoundItem).ID;
                ModuloUsuarioDesktop formModuloUsuario = new ModuloUsuarioDesktop(ID, ApplicationForm.ModoForm.Modificacion);
                formModuloUsuario.ShowDialog();
                this.Listar();
                //volver a seleccionar la fila con la que se interactuó anteriormente
                DataGridViewRow fila = this.dgvModulosUsuarios.Rows[nroFilaSeleccionada];
                this.dgvModulosUsuarios.CurrentCell = fila.Cells[0];
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (this.dgvModulosUsuarios.SelectedRows.Count< 1)
            {
                MessageBox.Show("Debe seleccionar la fila a eliminar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int cantFilas = this.dgvModulosUsuarios.RowCount;
                int indiceSeleccionado = this.dgvModulosUsuarios.CurrentCell.RowIndex;
                int ID = ((Business.Entities.ModuloUsuario)this.dgvModulosUsuarios.SelectedRows[0].DataBoundItem).ID;
                ModuloUsuarioDesktop formModuloUsuario = new ModuloUsuarioDesktop(ID, ApplicationForm.ModoForm.Baja);
                formModuloUsuario.ShowDialog();
                this.Listar();
                //si no se eliminó la fila, volver a seleccionarla
                if (cantFilas == this.dgvModulosUsuarios.RowCount)
                {
                    DataGridViewRow fila = this.dgvModulosUsuarios.Rows[indiceSeleccionado];
                    this.dgvModulosUsuarios.CurrentCell = fila.Cells[0];
                }
            }
        }
        private void tsbVer_Click(object sender, EventArgs e)
        {
            if (this.dgvModulosUsuarios.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a inspeccionar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int indiceSeleccionado = this.dgvModulosUsuarios.CurrentCell.RowIndex;
                int ID = ((Business.Entities.ModuloUsuario)this.dgvModulosUsuarios.SelectedRows[0].DataBoundItem).ID;
                ModuloUsuarioDesktop formModuloUsuario = new ModuloUsuarioDesktop(ID, ApplicationForm.ModoForm.Consulta);
                formModuloUsuario.ShowDialog();
                this.Listar();
                //volver a seleccionar la fila consultada
                DataGridViewRow fila = this.dgvModulosUsuarios.Rows[indiceSeleccionado];
                this.dgvModulosUsuarios.CurrentCell = fila.Cells[0];
            }
        }
        private bool Validar()
        {
            string mensaje = null;
            ModuloLogic ml = new ModuloLogic();
            if (ml.GetAll().Count == 0)
            {
                mensaje += "Debe cargar antes un modulo por lo menos\n";
            }
            UsuarioLogic ul = new UsuarioLogic();
            if (ul.GetAll().Count == 0)
            {
                mensaje += "Debe cargar antes un usuario por lo menos\n";
            }
            if (mensaje != null)
            {
                MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }
            return true;
        }
    }
}
