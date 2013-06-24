namespace FrbaBus
{
    partial class Inicio
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
            this.labelBienvenida = new System.Windows.Forms.Label();
            this.labelInicioSesion = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelBienvenida
            // 
            this.labelBienvenida.AutoSize = true;
            this.labelBienvenida.Location = new System.Drawing.Point(20, 19);
            this.labelBienvenida.Name = "labelBienvenida";
            this.labelBienvenida.Size = new System.Drawing.Size(171, 13);
            this.labelBienvenida.TabIndex = 0;
            this.labelBienvenida.Text = "Bienvenido. Elija que quiere hacer.";
            // 
            // labelInicioSesion
            // 
            this.labelInicioSesion.AutoSize = true;
            this.labelInicioSesion.Location = new System.Drawing.Point(156, 236);
            this.labelInicioSesion.Name = "labelInicioSesion";
            this.labelInicioSesion.Size = new System.Drawing.Size(35, 13);
            this.labelInicioSesion.TabIndex = 1;
            this.labelInicioSesion.Text = "label1";
            this.labelInicioSesion.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnLogin
            // 
            this.btnLogin.AutoSize = true;
            this.btnLogin.Location = new System.Drawing.Point(198, 226);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(75, 23);
            this.btnLogin.TabIndex = 2;
            this.btnLogin.Text = "button1";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.labelInicioSesion);
            this.Controls.Add(this.labelBienvenida);
            this.Name = "Inicio";
            this.Text = "FrbaBus";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelBienvenida;
        private System.Windows.Forms.Label labelInicioSesion;
        private System.Windows.Forms.Button btnLogin;
    }
}

