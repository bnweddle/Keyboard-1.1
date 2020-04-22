namespace NoteDetection
{
    partial class Piano
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
            this.pianoControl = new Sanford.Multimedia.Midi.UI.PianoControl();
            this.SuspendLayout();
            // 
            // pianoControl
            // 
            this.pianoControl.HighNoteID = 109;
            this.pianoControl.Location = new System.Drawing.Point(29, 3);
            this.pianoControl.LowNoteID = 21;
            this.pianoControl.Margin = new System.Windows.Forms.Padding(4);
            this.pianoControl.Name = "pianoControl";
            this.pianoControl.NoteOnColor = System.Drawing.Color.AliceBlue;
            this.pianoControl.Size = new System.Drawing.Size(1689, 201);
            this.pianoControl.TabIndex = 6;
            this.pianoControl.PianoKeyDown += new System.EventHandler<Sanford.Multimedia.Midi.UI.PianoKeyEventArgs>(this.PianoControl_PianoKeyDown);
            this.pianoControl.PianoKeyUp += new System.EventHandler<Sanford.Multimedia.Midi.UI.PianoKeyEventArgs>(this.PianoControl_PianoKeyUp);
            this.pianoControl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pianoControl_KeyDown);
            this.pianoControl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.pianoControl_KeyUp);
            // 
            // Piano
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1924, 205);
            this.Controls.Add(this.pianoControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Piano";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Piano";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Piano_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private Sanford.Multimedia.Midi.UI.PianoControl pianoControl;
    }
}