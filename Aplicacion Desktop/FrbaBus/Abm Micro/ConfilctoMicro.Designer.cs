namespace FrbaBus.Abm_Micro
{
    partial class ConfilctoMicro
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
            this.labelConflicto = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnCrearReemplazar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelConflicto
            // 
            this.labelConflicto.AutoSize = true;
            this.labelConflicto.Location = new System.Drawing.Point(13, 13);
            this.labelConflicto.Name = "labelConflicto";
            this.labelConflicto.Size = new System.Drawing.Size(258, 13);
            this.labelConflicto.TabIndex = 0;
            this.labelConflicto.Text = "¿Qué desea hacer con los viajes asignados al micro?";
            // 
            // btnCancelar
            // 
            this.btnCancelar.AutoSize = true;
            this.btnCancelar.Location = new System.Drawing.Point(12, 40);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(156, 23);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar los viajes asignados";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnCrearReemplazar
            // 
            this.btnCrearReemplazar.AutoSize = true;
            this.btnCrearReemplazar.Location = new System.Drawing.Point(12, 79);
            this.btnCrearReemplazar.Name = "btnCrearReemplazar";
            this.btnCrearReemplazar.Size = new System.Drawing.Size(178, 23);
            this.btnCrearReemplazar.TabIndex = 2;
            this.btnCrearReemplazar.Text = "Dar de alta un micro de reemplazo";
            this.btnCrearReemplazar.UseVisualStyleBackColor = true;
            this.btnCrearReemplazar.Click += new System.EventHandler(this.btnCrearReemplazar_Click);
            // 
            // ConfilctoMicro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 124);
            this.Controls.Add(this.btnCrearReemplazar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.labelConflicto);
            this.Name = "ConfilctoMicro";
            this.Text = "ConfilctoMicro";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelConflicto;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnCrearReemplazar;
    }
}