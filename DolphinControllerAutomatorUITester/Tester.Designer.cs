namespace DolphinControllerAutomatorUITester {
    partial class Tester {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.pressUpButton = new System.Windows.Forms.Button();
            this.pressDownButton = new System.Windows.Forms.Button();
            this.pressAButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // pressUpButton
            // 
            this.pressUpButton.Location = new System.Drawing.Point(13, 13);
            this.pressUpButton.Name = "pressUpButton";
            this.pressUpButton.Size = new System.Drawing.Size(75, 23);
            this.pressUpButton.TabIndex = 0;
            this.pressUpButton.Text = "Press Up";
            this.pressUpButton.UseVisualStyleBackColor = true;
            this.pressUpButton.Click += new System.EventHandler(this.pressUp);
            // 
            // pressDownButton
            // 
            this.pressDownButton.Location = new System.Drawing.Point(13, 42);
            this.pressDownButton.Name = "pressDownButton";
            this.pressDownButton.Size = new System.Drawing.Size(75, 23);
            this.pressDownButton.TabIndex = 0;
            this.pressDownButton.Text = "Press Down";
            this.pressDownButton.UseVisualStyleBackColor = true;
            this.pressDownButton.Click += new System.EventHandler(this.pressDown);
            // 
            // pressAButton
            // 
            this.pressAButton.Location = new System.Drawing.Point(13, 71);
            this.pressAButton.Name = "pressAButton";
            this.pressAButton.Size = new System.Drawing.Size(75, 23);
            this.pressAButton.TabIndex = 0;
            this.pressAButton.Text = "Press A";
            this.pressAButton.UseVisualStyleBackColor = true;
            this.pressAButton.Click += new System.EventHandler(this.pressA);
            // 
            // Tester
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.pressAButton);
            this.Controls.Add(this.pressDownButton);
            this.Controls.Add(this.pressUpButton);
            this.Name = "Tester";
            this.Text = "Tester";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button pressUpButton;
        private System.Windows.Forms.Button pressDownButton;
        private System.Windows.Forms.Button pressAButton;
    }
}

