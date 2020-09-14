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
    public partial class Modulos : Form
    {
        public Modulos()
        {
            InitializeComponent();
            this.dgvModulos.AutoGenerateColumns = false;
            this.oModulos = new Business.Entities.Modulo();
        }

        private Business.Entities.Modulo _modulos;
        public Business.Entities.Modulo oModulos
        {
            get { return _modulos; }
            set { _modulos = value; }
        }

        public void Listar()
        {
            ModuloLogic ml = new ModuloLogic();
            this.dgvModulos.DataSource = ml.GetAll();
        }

        private void Modulos_Load(object sender, EventArgs e)
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
            int cantFilas = this.dgvModulos.RowCount;
            ModuloDesktop formModulo = new ModuloDesktop(ApplicationForm.ModoForm.Alta);
            formModulo.ShowDialog();
            this.Listar();
            //si se agregó una fila, seleccionarla
            if (cantFilas != this.dgvModulos.RowCount)
            {
                DataGridViewRow fila = this.dgvModulos.Rows[cantFilas];
                this.dgvModulos.CurrentCell = fila.Cells[0];
            }
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (this.dgvModulos.SelectedRows.Count<1)
            {
                MessageBox.Show("Debe seleccionar la fila a modificar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int nroFilaSeleccionada = this.dgvModulos.CurrentCell.RowIndex;
                int ID = ((Business.Entities.Modulo)this.dgvModulos.SelectedRows[0].DataBoundItem).ID;
                ModuloDesktop formModulo = new ModuloDesktop(ID, ApplicationForm.ModoForm.Modificacion);
                formModulo.ShowDialog();
                this.Listar();
                //volver a seleccionar la fila con la que se interactuó anteriormente
                DataGridViewRow fila = this.dgvModulos.Rows[nroFilaSeleccionada];
                this.dgvModulos.CurrentCell = fila.Cells[0];
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (this.dgvModulos.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a eliminar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int cantFilas = this.dgvModulos.RowCount;
                int indiceSeleccionado = this.dgvModulos.CurrentCell.RowIndex;
                int ID = ((Business.Entities.Modulo)this.dgvModulos.SelectedRows[0].DataBoundItem).ID;
                ModuloDesktop formModulo = new ModuloDesktop(ID, ApplicationForm.ModoForm.Baja);
                formModulo.ShowDialog();
                this.Listar();
                //si no se eliminó la fila, volver a seleccionarla
                if (cantFilas == this.dgvModulos.RowCount)
                {
                    DataGridViewRow fila = this.dgvModulos.Rows[indiceSeleccionado];
                    this.dgvModulos.CurrentCell = fila.Cells[0];
                }
            }
        }
        private void tsbVer_Click(object sender, EventArgs e)
        {
            if (this.dgvModulos.SelectedRows.Count < 1)
            {
                MessageBox.Show("Debe seleccionar la fila a inspeccionar", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                int indiceSeleccionado = this.dgvModulos.CurrentCell.RowIndex;
                int ID = ((Business.Entities.Modulo)this.dgvModulos.SelectedRows[0].DataBoundItem).ID;
                ModuloDesktop formModulo = new ModuloDesktop(ID, ApplicationForm.ModoForm.Consulta);
                formModulo.ShowDialog();
                this.Listar();
                //volver a seleccionar la fila consultada
                DataGridViewRow fila = this.dgvModulos.Rows[indiceSeleccionado];
                this.dgvModulos.CurrentCell = fila.Cells[0];
            }
        }
    }
}
