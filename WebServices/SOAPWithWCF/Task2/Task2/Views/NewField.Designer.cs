namespace Task2.Views
{
    partial class NewField
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
            this.newFieldLbl = new System.Windows.Forms.Label();
            this.fieldLbl = new System.Windows.Forms.Label();
            this.fieldTxt = new System.Windows.Forms.TextBox();
            this.addFieldBtn = new System.Windows.Forms.Button();
            this.focusLbl = new System.Windows.Forms.Label();
            this.focusTxt = new System.Windows.Forms.TextBox();
            this.addBtn = new System.Windows.Forms.Button();
            this.displayFieldLbl = new System.Windows.Forms.Label();
            this.displayFociLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // newFieldLbl
            // 
            this.newFieldLbl.AutoSize = true;
            this.newFieldLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newFieldLbl.Location = new System.Drawing.Point(329, 9);
            this.newFieldLbl.Name = "newFieldLbl";
            this.newFieldLbl.Size = new System.Drawing.Size(171, 39);
            this.newFieldLbl.TabIndex = 0;
            this.newFieldLbl.Text = "Add Field";
            // 
            // fieldLbl
            // 
            this.fieldLbl.AutoSize = true;
            this.fieldLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fieldLbl.Location = new System.Drawing.Point(248, 80);
            this.fieldLbl.Name = "fieldLbl";
            this.fieldLbl.Size = new System.Drawing.Size(131, 25);
            this.fieldLbl.TabIndex = 1;
            this.fieldLbl.Text = "Field of Study";
            // 
            // fieldTxt
            // 
            this.fieldTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fieldTxt.Location = new System.Drawing.Point(385, 80);
            this.fieldTxt.Name = "fieldTxt";
            this.fieldTxt.Size = new System.Drawing.Size(258, 26);
            this.fieldTxt.TabIndex = 2;
            this.fieldTxt.TextChanged += new System.EventHandler(this.fieldTxt_TextChanged);
            // 
            // addFieldBtn
            // 
            this.addFieldBtn.Location = new System.Drawing.Point(449, 112);
            this.addFieldBtn.Name = "addFieldBtn";
            this.addFieldBtn.Size = new System.Drawing.Size(104, 32);
            this.addFieldBtn.TabIndex = 3;
            this.addFieldBtn.Text = "Submit";
            this.addFieldBtn.UseVisualStyleBackColor = true;
            this.addFieldBtn.Click += new System.EventHandler(this.addFieldBtn_Click);
            // 
            // focusLbl
            // 
            this.focusLbl.AutoSize = true;
            this.focusLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.focusLbl.Location = new System.Drawing.Point(313, 192);
            this.focusLbl.Name = "focusLbl";
            this.focusLbl.Size = new System.Drawing.Size(66, 25);
            this.focusLbl.TabIndex = 4;
            this.focusLbl.Text = "Focus";
            // 
            // focusTxt
            // 
            this.focusTxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.focusTxt.Location = new System.Drawing.Point(385, 191);
            this.focusTxt.Name = "focusTxt";
            this.focusTxt.Size = new System.Drawing.Size(258, 26);
            this.focusTxt.TabIndex = 5;
            this.focusTxt.TextChanged += new System.EventHandler(this.focusTxt_TextChanged);
            // 
            // addBtn
            // 
            this.addBtn.Location = new System.Drawing.Point(449, 233);
            this.addBtn.Name = "addBtn";
            this.addBtn.Size = new System.Drawing.Size(93, 30);
            this.addBtn.TabIndex = 6;
            this.addBtn.Text = "Add";
            this.addBtn.UseVisualStyleBackColor = true;
            this.addBtn.Click += new System.EventHandler(this.addBtn_Click);
            // 
            // displayFieldLbl
            // 
            this.displayFieldLbl.AutoSize = true;
            this.displayFieldLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayFieldLbl.Location = new System.Drawing.Point(38, 29);
            this.displayFieldLbl.Name = "displayFieldLbl";
            this.displayFieldLbl.Size = new System.Drawing.Size(0, 31);
            this.displayFieldLbl.TabIndex = 7;
            // 
            // displayFociLbl
            // 
            this.displayFociLbl.AutoSize = true;
            this.displayFociLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.displayFociLbl.Location = new System.Drawing.Point(41, 80);
            this.displayFociLbl.Name = "displayFociLbl";
            this.displayFociLbl.Size = new System.Drawing.Size(0, 17);
            this.displayFociLbl.TabIndex = 8;
            // 
            // NewField
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.displayFociLbl);
            this.Controls.Add(this.displayFieldLbl);
            this.Controls.Add(this.addBtn);
            this.Controls.Add(this.focusTxt);
            this.Controls.Add(this.focusLbl);
            this.Controls.Add(this.addFieldBtn);
            this.Controls.Add(this.fieldTxt);
            this.Controls.Add(this.fieldLbl);
            this.Controls.Add(this.newFieldLbl);
            this.Name = "NewField";
            this.Text = "NewField";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label newFieldLbl;
        private System.Windows.Forms.Label fieldLbl;
        private System.Windows.Forms.TextBox fieldTxt;
        private System.Windows.Forms.Button addFieldBtn;
        private System.Windows.Forms.Label focusLbl;
        private System.Windows.Forms.TextBox focusTxt;
        private System.Windows.Forms.Button addBtn;
        private System.Windows.Forms.Label displayFieldLbl;
        private System.Windows.Forms.Label displayFociLbl;
    }
}