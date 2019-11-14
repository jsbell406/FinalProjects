namespace Task2.Views
{
    partial class SelectAccountType
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
            this.newEmployerButton = new System.Windows.Forms.Button();
            this.newStudentButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // newEmployerButton
            // 
            this.newEmployerButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newEmployerButton.Location = new System.Drawing.Point(68, 139);
            this.newEmployerButton.Name = "newEmployerButton";
            this.newEmployerButton.Size = new System.Drawing.Size(265, 120);
            this.newEmployerButton.TabIndex = 0;
            this.newEmployerButton.Text = "New Employer";
            this.newEmployerButton.UseVisualStyleBackColor = true;
            this.newEmployerButton.Click += new System.EventHandler(this.newEmployerButton_Click);
            // 
            // newStudentButton
            // 
            this.newStudentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newStudentButton.Location = new System.Drawing.Point(435, 139);
            this.newStudentButton.Name = "newStudentButton";
            this.newStudentButton.Size = new System.Drawing.Size(265, 120);
            this.newStudentButton.TabIndex = 1;
            this.newStudentButton.TabStop = false;
            this.newStudentButton.Text = "New Student";
            this.newStudentButton.UseVisualStyleBackColor = true;
            // 
            // SelectAccountType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.newStudentButton);
            this.Controls.Add(this.newEmployerButton);
            this.Name = "SelectAccountType";
            this.Text = "SelectAccountType";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button newEmployerButton;
        private System.Windows.Forms.Button newStudentButton;
    }
}