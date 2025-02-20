
using System.Collections.Generic;
using System.IO;

namespace practica_7
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>

        List<string> palReservadas = new List<string>() { "auto", "break", "case", "char", "const", "continue", "default", "do",  "else", "enum", "extern", "for", "goto", "if", "include", "inline", "long", "register", "restrict", "return", "short", "signed", "sizeof", "static", "struct", "switch", "typedef", "union", "unsigned", "void", "volatile", "while", "private", "public", "int", "short", "long", "signed", "unsigned", "float", "double" };
        List<string> P_Res_Tipo = new List<string>() { "int", "char", "float", "bool", "void"};
        string[] tiposDeDatos = { "int", "short", "long", "signed", "unsigned", "float", "double", "char", "void" };
        string[] funciones = { "for", "do", "if", "switch", "case", "while" };
        StreamWriter Escribir, Escribirtok, EscribirBak;
        StreamReader Leer;
        int i_caracter, N_error;
        string archivo, archivobak, archivotok;
        int Numero_linea;
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.pantalla = new System.Windows.Forms.RichTextBox();
            this.Rtbx_salida = new System.Windows.Forms.RichTextBox();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.guardarComoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compilarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aLexicoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aSintacticoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aSemanticoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pantalla
            // 
            this.pantalla.Dock = System.Windows.Forms.DockStyle.Top;
            this.pantalla.Location = new System.Drawing.Point(0, 28);
            this.pantalla.Margin = new System.Windows.Forms.Padding(4);
            this.pantalla.Name = "pantalla";
            this.pantalla.Size = new System.Drawing.Size(834, 380);
            this.pantalla.TabIndex = 1;
            this.pantalla.Text = "";
            this.pantalla.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // Rtbx_salida
            // 
            this.Rtbx_salida.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Rtbx_salida.Location = new System.Drawing.Point(0, 422);
            this.Rtbx_salida.Name = "Rtbx_salida";
            this.Rtbx_salida.Size = new System.Drawing.Size(834, 132);
            this.Rtbx_salida.TabIndex = 2;
            this.Rtbx_salida.Text = "";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.abrirToolStripMenuItem,
            this.guardarToolStripMenuItem,
            this.guardarComoToolStripMenuItem,
            this.toolStripSeparator1,
            this.salirToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(73, 24);
            this.archivoToolStripMenuItem.Text = "Archivo";
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(187, 26);
            this.nuevoToolStripMenuItem.Text = "Nuevo";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(187, 26);
            this.abrirToolStripMenuItem.Text = "Abrir";
            this.abrirToolStripMenuItem.Click += new System.EventHandler(this.abrirToolStripMenuItem_Click);
            // 
            // guardarToolStripMenuItem
            // 
            this.guardarToolStripMenuItem.Name = "guardarToolStripMenuItem";
            this.guardarToolStripMenuItem.Size = new System.Drawing.Size(187, 26);
            this.guardarToolStripMenuItem.Text = "Guardar";
            this.guardarToolStripMenuItem.Click += new System.EventHandler(this.guardarToolStripMenuItem_Click);
            // 
            // guardarComoToolStripMenuItem
            // 
            this.guardarComoToolStripMenuItem.Name = "guardarComoToolStripMenuItem";
            this.guardarComoToolStripMenuItem.Size = new System.Drawing.Size(187, 26);
            this.guardarComoToolStripMenuItem.Text = "Guardar como";
            this.guardarComoToolStripMenuItem.Click += new System.EventHandler(this.guardarComoToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(184, 6);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(187, 26);
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // compilarToolStripMenuItem
            // 
            this.compilarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aLexicoToolStripMenuItem,
            this.aSintacticoToolStripMenuItem,
            this.aSemanticoToolStripMenuItem});
            this.compilarToolStripMenuItem.Name = "compilarToolStripMenuItem";
            this.compilarToolStripMenuItem.Size = new System.Drawing.Size(84, 24);
            this.compilarToolStripMenuItem.Text = "Compilar";
            this.compilarToolStripMenuItem.Click += new System.EventHandler(this.compilarToolStripMenuItem_Click);
            // 
            // aLexicoToolStripMenuItem
            // 
            this.aLexicoToolStripMenuItem.Name = "aLexicoToolStripMenuItem";
            this.aLexicoToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.aLexicoToolStripMenuItem.Text = "A. Lexico";
            this.aLexicoToolStripMenuItem.Click += new System.EventHandler(this.compilarSolucionToolStripMenuItem_Click);
            // 
            // aSintacticoToolStripMenuItem
            // 
            this.aSintacticoToolStripMenuItem.Name = "aSintacticoToolStripMenuItem";
            this.aSintacticoToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.aSintacticoToolStripMenuItem.Text = "A. Sintactico";
            this.aSintacticoToolStripMenuItem.Click += new System.EventHandler(this.aSintacticoToolStripMenuItem_Click);
            // 
            // aSemanticoToolStripMenuItem
            // 
            this.aSemanticoToolStripMenuItem.Name = "aSemanticoToolStripMenuItem";
            this.aSemanticoToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.aSemanticoToolStripMenuItem.Text = "A. Semantico";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.compilarToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(834, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 554);
            this.Controls.Add(this.Rtbx_salida);
            this.Controls.Add(this.pantalla);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox pantalla;
        private System.Windows.Forms.RichTextBox Rtbx_salida;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem guardarComoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem compilarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aLexicoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aSintacticoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aSemanticoToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
    }
}

