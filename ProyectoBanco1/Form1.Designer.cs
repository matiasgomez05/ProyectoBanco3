namespace ProyectoBanco1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonRegistrar = new System.Windows.Forms.Button();
            this.textBoxNombre = new System.Windows.Forms.TextBox();
            this.textBoxDNI = new System.Windows.Forms.TextBox();
            this.textBoxApellido = new System.Windows.Forms.TextBox();
            this.textBoxMail = new System.Windows.Forms.TextBox();
            this.textBoxPass = new System.Windows.Forms.TextBox();
            this.textBoxPass2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxIngresoDni = new System.Windows.Forms.TextBox();
            this.buttonIngresar = new System.Windows.Forms.Button();
            this.textBoxIngresoPass = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonRegistrar
            // 
            this.buttonRegistrar.Location = new System.Drawing.Point(635, 493);
            this.buttonRegistrar.Name = "buttonRegistrar";
            this.buttonRegistrar.Size = new System.Drawing.Size(198, 35);
            this.buttonRegistrar.TabIndex = 7;
            this.buttonRegistrar.Text = "Registrar";
            this.buttonRegistrar.UseVisualStyleBackColor = true;
            this.buttonRegistrar.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxNombre
            // 
            this.textBoxNombre.Location = new System.Drawing.Point(616, 282);
            this.textBoxNombre.Name = "textBoxNombre";
            this.textBoxNombre.PlaceholderText = "Nombre";
            this.textBoxNombre.Size = new System.Drawing.Size(245, 23);
            this.textBoxNombre.TabIndex = 2;
            // 
            // textBoxDNI
            // 
            this.textBoxDNI.Location = new System.Drawing.Point(616, 244);
            this.textBoxDNI.Name = "textBoxDNI";
            this.textBoxDNI.PlaceholderText = "DNI";
            this.textBoxDNI.Size = new System.Drawing.Size(245, 23);
            this.textBoxDNI.TabIndex = 1;
            this.textBoxDNI.Tag = "";
            // 
            // textBoxApellido
            // 
            this.textBoxApellido.Location = new System.Drawing.Point(616, 320);
            this.textBoxApellido.Name = "textBoxApellido";
            this.textBoxApellido.PlaceholderText = "Apellido";
            this.textBoxApellido.Size = new System.Drawing.Size(245, 23);
            this.textBoxApellido.TabIndex = 3;
            // 
            // textBoxMail
            // 
            this.textBoxMail.Location = new System.Drawing.Point(616, 359);
            this.textBoxMail.Name = "textBoxMail";
            this.textBoxMail.PlaceholderText = "Mail";
            this.textBoxMail.Size = new System.Drawing.Size(245, 23);
            this.textBoxMail.TabIndex = 4;
            // 
            // textBoxPass
            // 
            this.textBoxPass.Location = new System.Drawing.Point(616, 398);
            this.textBoxPass.Name = "textBoxPass";
            this.textBoxPass.PasswordChar = '*';
            this.textBoxPass.PlaceholderText = "Password";
            this.textBoxPass.Size = new System.Drawing.Size(245, 23);
            this.textBoxPass.TabIndex = 5;
            // 
            // textBoxPass2
            // 
            this.textBoxPass2.Location = new System.Drawing.Point(616, 436);
            this.textBoxPass2.Name = "textBoxPass2";
            this.textBoxPass2.PasswordChar = '*';
            this.textBoxPass2.PlaceholderText = "Repetir Password";
            this.textBoxPass2.Size = new System.Drawing.Size(245, 23);
            this.textBoxPass2.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(635, 189);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Registrar usuario";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(166, 189);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 37);
            this.label2.TabIndex = 8;
            this.label2.Text = "Ingresar";
            // 
            // textBoxIngresoDni
            // 
            this.textBoxIngresoDni.Location = new System.Drawing.Point(103, 244);
            this.textBoxIngresoDni.Name = "textBoxIngresoDni";
            this.textBoxIngresoDni.PlaceholderText = "DNI";
            this.textBoxIngresoDni.Size = new System.Drawing.Size(245, 23);
            this.textBoxIngresoDni.TabIndex = 9;
            this.textBoxIngresoDni.Tag = "";
            // 
            // buttonIngresar
            // 
            this.buttonIngresar.Location = new System.Drawing.Point(124, 320);
            this.buttonIngresar.Name = "buttonIngresar";
            this.buttonIngresar.Size = new System.Drawing.Size(198, 35);
            this.buttonIngresar.TabIndex = 11;
            this.buttonIngresar.Text = "Ingresar";
            this.buttonIngresar.UseVisualStyleBackColor = true;
            this.buttonIngresar.Click += new System.EventHandler(this.buttonIngresar_Click);
            // 
            // textBoxIngresoPass
            // 
            this.textBoxIngresoPass.Location = new System.Drawing.Point(103, 282);
            this.textBoxIngresoPass.Name = "textBoxIngresoPass";
            this.textBoxIngresoPass.PasswordChar = '*';
            this.textBoxIngresoPass.PlaceholderText = "Password";
            this.textBoxIngresoPass.Size = new System.Drawing.Size(245, 23);
            this.textBoxIngresoPass.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.textBoxIngresoPass);
            this.Controls.Add(this.buttonIngresar);
            this.Controls.Add(this.textBoxIngresoDni);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPass);
            this.Controls.Add(this.textBoxPass2);
            this.Controls.Add(this.textBoxApellido);
            this.Controls.Add(this.textBoxMail);
            this.Controls.Add(this.textBoxDNI);
            this.Controls.Add(this.textBoxNombre);
            this.Controls.Add(this.buttonRegistrar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ingreso";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        
        private TextBox textBoxDNI;
        private TextBox textBoxNombre;
        private TextBox textBoxApellido;
        private TextBox textBoxMail;
        private TextBox textBoxPass;
        private TextBox textBoxPass2;
        private Button buttonRegistrar;
        private Label label1;
        private Label label2;
        private TextBox textBoxIngresoDni;
        private Button buttonIngresar;
        private TextBox textBoxIngresoPass;
    }
}