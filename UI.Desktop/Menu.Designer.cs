namespace UI.Desktop
{
    partial class Menu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCerrarSesion = new System.Windows.Forms.Button();
            this.lblBienvenido = new System.Windows.Forms.Label();
            this.msMenu = new System.Windows.Forms.MenuStrip();
            this.tsiOpciones = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPersonas = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEspecialidades = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPlanes = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiComisiones = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMaterias = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCursos = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUsuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiModulos = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiModulosUsuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAlumnosInscripciones = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDocentesCursos = new System.Windows.Forms.ToolStripMenuItem();
            this.btnVacio = new System.Windows.Forms.Button();
            this.msMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCerrarSesion
            // 
            this.btnCerrarSesion.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCerrarSesion.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCerrarSesion.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrarSesion.ForeColor = System.Drawing.Color.Red;
            this.btnCerrarSesion.Location = new System.Drawing.Point(723, 28);
            this.btnCerrarSesion.Name = "btnCerrarSesion";
            this.btnCerrarSesion.Size = new System.Drawing.Size(84, 221);
            this.btnCerrarSesion.TabIndex = 3;
            this.btnCerrarSesion.Text = "Cerrar Sesión";
            this.btnCerrarSesion.UseVisualStyleBackColor = false;
            this.btnCerrarSesion.Click += new System.EventHandler(this.btnSalir_Click);
            this.btnCerrarSesion.MouseEnter += new System.EventHandler(this.btnCerrarSesion_MouseEnter);
            this.btnCerrarSesion.MouseLeave += new System.EventHandler(this.btnCerrarSesion_MouseLeave);
            // 
            // lblBienvenido
            // 
            this.lblBienvenido.AutoSize = true;
            this.lblBienvenido.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.lblBienvenido.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblBienvenido.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBienvenido.ForeColor = System.Drawing.Color.Black;
            this.lblBienvenido.Location = new System.Drawing.Point(0, 218);
            this.lblBienvenido.Name = "lblBienvenido";
            this.lblBienvenido.Size = new System.Drawing.Size(76, 31);
            this.lblBienvenido.TabIndex = 4;
            this.lblBienvenido.Text = "label";
            // 
            // msMenu
            // 
            this.msMenu.BackColor = System.Drawing.SystemColors.Control;
            this.msMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsiOpciones});
            this.msMenu.Location = new System.Drawing.Point(0, 0);
            this.msMenu.MdiWindowListItem = this.tsiOpciones;
            this.msMenu.Name = "msMenu";
            this.msMenu.Size = new System.Drawing.Size(807, 28);
            this.msMenu.TabIndex = 10;
            this.msMenu.Text = "menuStrip1";
            // 
            // tsiOpciones
            // 
            this.tsiOpciones.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPersonas,
            this.tsmiEspecialidades,
            this.tsmiPlanes,
            this.tsmiComisiones,
            this.tsmiMaterias,
            this.tsmiCursos,
            this.tsmiUsuarios,
            this.tsmiModulos,
            this.tsmiModulosUsuarios,
            this.tsmiAlumnosInscripciones,
            this.tsmiDocentesCursos});
            this.tsiOpciones.Name = "tsiOpciones";
            this.tsiOpciones.Size = new System.Drawing.Size(167, 24);
            this.tsiOpciones.Text = "Opciones Disponibles";
            // 
            // tsmiPersonas
            // 
            this.tsmiPersonas.Name = "tsmiPersonas";
            this.tsmiPersonas.Size = new System.Drawing.Size(231, 26);
            this.tsmiPersonas.Text = "Personas";
            this.tsmiPersonas.Click += new System.EventHandler(this.tsmiPersonas_Click);
            // 
            // tsmiEspecialidades
            // 
            this.tsmiEspecialidades.Name = "tsmiEspecialidades";
            this.tsmiEspecialidades.Size = new System.Drawing.Size(231, 26);
            this.tsmiEspecialidades.Text = "Especialidades";
            this.tsmiEspecialidades.Click += new System.EventHandler(this.tsmiEspecialidades_Click);
            // 
            // tsmiPlanes
            // 
            this.tsmiPlanes.Name = "tsmiPlanes";
            this.tsmiPlanes.Size = new System.Drawing.Size(231, 26);
            this.tsmiPlanes.Text = "Planes";
            this.tsmiPlanes.Click += new System.EventHandler(this.tsmiPlanes_Click);
            // 
            // tsmiComisiones
            // 
            this.tsmiComisiones.Name = "tsmiComisiones";
            this.tsmiComisiones.Size = new System.Drawing.Size(231, 26);
            this.tsmiComisiones.Text = "Comisiones";
            this.tsmiComisiones.Click += new System.EventHandler(this.tsmiComisiones_Click);
            // 
            // tsmiMaterias
            // 
            this.tsmiMaterias.Name = "tsmiMaterias";
            this.tsmiMaterias.Size = new System.Drawing.Size(231, 26);
            this.tsmiMaterias.Text = "Materias";
            this.tsmiMaterias.Click += new System.EventHandler(this.tsmiMaterias_Click);
            // 
            // tsmiCursos
            // 
            this.tsmiCursos.Name = "tsmiCursos";
            this.tsmiCursos.Size = new System.Drawing.Size(231, 26);
            this.tsmiCursos.Text = "Cursos";
            this.tsmiCursos.Click += new System.EventHandler(this.tsmiiCursos_Click);
            // 
            // tsmiUsuarios
            // 
            this.tsmiUsuarios.Name = "tsmiUsuarios";
            this.tsmiUsuarios.Size = new System.Drawing.Size(231, 26);
            this.tsmiUsuarios.Text = "Usuarios";
            this.tsmiUsuarios.Click += new System.EventHandler(this.tsmiUsuarios_Click);
            // 
            // tsmiModulos
            // 
            this.tsmiModulos.Name = "tsmiModulos";
            this.tsmiModulos.Size = new System.Drawing.Size(231, 26);
            this.tsmiModulos.Text = "Módulos";
            this.tsmiModulos.Click += new System.EventHandler(this.tsmiModulos_Click);
            // 
            // tsmiModulosUsuarios
            // 
            this.tsmiModulosUsuarios.Name = "tsmiModulosUsuarios";
            this.tsmiModulosUsuarios.Size = new System.Drawing.Size(231, 26);
            this.tsmiModulosUsuarios.Text = "Módulos de Usuarios";
            this.tsmiModulosUsuarios.Click += new System.EventHandler(this.tsmiModulosUsuarios_Click);
            // 
            // tsmiAlumnosInscripciones
            // 
            this.tsmiAlumnosInscripciones.Name = "tsmiAlumnosInscripciones";
            this.tsmiAlumnosInscripciones.Size = new System.Drawing.Size(231, 26);
            this.tsmiAlumnosInscripciones.Text = "Inscripción Alumno";
            this.tsmiAlumnosInscripciones.Click += new System.EventHandler(this.tsmiAlumnosInscripciones_Click);
            // 
            // tsmiDocentesCursos
            // 
            this.tsmiDocentesCursos.Name = "tsmiDocentesCursos";
            this.tsmiDocentesCursos.Size = new System.Drawing.Size(231, 26);
            this.tsmiDocentesCursos.Text = "Inscripción Docente";
            this.tsmiDocentesCursos.Click += new System.EventHandler(this.tsmiDocentesCursos_Click);
            // 
            // btnVacio
            // 
            this.btnVacio.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnVacio.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnVacio.Location = new System.Drawing.Point(0, 249);
            this.btnVacio.Name = "btnVacio";
            this.btnVacio.Size = new System.Drawing.Size(807, 50);
            this.btnVacio.TabIndex = 12;
            this.btnVacio.UseVisualStyleBackColor = false;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(807, 299);
            this.Controls.Add(this.lblBienvenido);
            this.Controls.Add(this.btnCerrarSesion);
            this.Controls.Add(this.msMenu);
            this.Controls.Add(this.btnVacio);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.msMenu;
            this.MaximizeBox = false;
            this.Name = "Menu";
            this.Text = "Menu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Menu_FormClosed);
            this.Load += new System.EventHandler(this.Menu_Load);
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnCerrarSesion;
        private System.Windows.Forms.Label lblBienvenido;
        private System.Windows.Forms.MenuStrip msMenu;
        private System.Windows.Forms.ToolStripMenuItem tsiOpciones;
        private System.Windows.Forms.ToolStripMenuItem tsmiPersonas;
        private System.Windows.Forms.ToolStripMenuItem tsmiEspecialidades;
        private System.Windows.Forms.ToolStripMenuItem tsmiPlanes;
        private System.Windows.Forms.ToolStripMenuItem tsmiComisiones;
        private System.Windows.Forms.ToolStripMenuItem tsmiMaterias;
        private System.Windows.Forms.ToolStripMenuItem tsmiCursos;
        private System.Windows.Forms.ToolStripMenuItem tsmiUsuarios;
        private System.Windows.Forms.ToolStripMenuItem tsmiModulos;
        private System.Windows.Forms.ToolStripMenuItem tsmiModulosUsuarios;
        private System.Windows.Forms.ToolStripMenuItem tsmiAlumnosInscripciones;
        private System.Windows.Forms.ToolStripMenuItem tsmiDocentesCursos;
        private System.Windows.Forms.Button btnVacio;
    }
}