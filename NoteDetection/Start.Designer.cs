namespace NoteDetection
{
    partial class Start
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
            this.label1 = new System.Windows.Forms.Label();
            this.uxOpen = new System.Windows.Forms.Button();
            this.BPM = new System.Windows.Forms.NumericUpDown();
            this.uxSharp = new System.Windows.Forms.RadioButton();
            this.uxFlats = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.BPM)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Set the BPM";
            // 
            // uxOpen
            // 
            this.uxOpen.Location = new System.Drawing.Point(82, 143);
            this.uxOpen.Name = "uxOpen";
            this.uxOpen.Size = new System.Drawing.Size(104, 31);
            this.uxOpen.TabIndex = 2;
            this.uxOpen.Text = "Open Piano";
            this.uxOpen.UseVisualStyleBackColor = true;
            this.uxOpen.Click += new System.EventHandler(this.uxOpen_Click);
            // 
            // BPM
            // 
            this.BPM.Location = new System.Drawing.Point(34, 83);
            this.BPM.Maximum = new decimal(new int[] {
            220,
            0,
            0,
            0});
            this.BPM.Minimum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.BPM.Name = "BPM";
            this.BPM.ReadOnly = true;
            this.BPM.Size = new System.Drawing.Size(83, 22);
            this.BPM.TabIndex = 3;
            this.BPM.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // uxSharp
            // 
            this.uxSharp.AutoSize = true;
            this.uxSharp.Checked = true;
            this.uxSharp.Location = new System.Drawing.Point(170, 67);
            this.uxSharp.Name = "uxSharp";
            this.uxSharp.Size = new System.Drawing.Size(74, 21);
            this.uxSharp.TabIndex = 4;
            this.uxSharp.TabStop = true;
            this.uxSharp.Text = "Sharps";
            this.uxSharp.UseVisualStyleBackColor = true;
            // 
            // uxFlats
            // 
            this.uxFlats.AutoSize = true;
            this.uxFlats.Location = new System.Drawing.Point(170, 94);
            this.uxFlats.Name = "uxFlats";
            this.uxFlats.Size = new System.Drawing.Size(59, 21);
            this.uxFlats.TabIndex = 5;
            this.uxFlats.Text = "Flats";
            this.uxFlats.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(167, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Set Type";
            // 
            // Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 186);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.uxFlats);
            this.Controls.Add(this.uxSharp);
            this.Controls.Add(this.BPM);
            this.Controls.Add(this.uxOpen);
            this.Controls.Add(this.label1);
            this.Name = "Start";
            this.Text = "Main";
            ((System.ComponentModel.ISupportInitialize)(this.BPM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button uxOpen;
        private System.Windows.Forms.NumericUpDown BPM;
        private System.Windows.Forms.RadioButton uxSharp;
        private System.Windows.Forms.RadioButton uxFlats;
        private System.Windows.Forms.Label label2;
    }
}