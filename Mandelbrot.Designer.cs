namespace MandelbrotSet
{
    partial class Mandelbrot
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
            lblMousePos = new Label();
            btnPanMode = new Button();
            btnUndoView = new Button();
            btnSaveJPG = new Button();
            btnClear = new Button();
            SuspendLayout();
            // 
            // lblMousePos
            // 
            lblMousePos.AutoSize = true;
            lblMousePos.Font = new Font("Bahnschrift", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblMousePos.Location = new Point(0, 627);
            lblMousePos.Name = "lblMousePos";
            lblMousePos.Size = new Size(90, 19);
            lblMousePos.TabIndex = 0;
            lblMousePos.Text = "MousePos: ";
            // 
            // btnPanMode
            // 
            btnPanMode.Font = new Font("Bahnschrift", 8F, FontStyle.Regular, GraphicsUnit.Point);
            btnPanMode.Location = new Point(642, 619);
            btnPanMode.Name = "btnPanMode";
            btnPanMode.Size = new Size(80, 27);
            btnPanMode.TabIndex = 4;
            btnPanMode.Text = "Pan Mode";
            btnPanMode.UseVisualStyleBackColor = true;
            btnPanMode.Click += btnPanMode_Click;
            // 
            // btnUndoView
            // 
            btnUndoView.Font = new Font("Bahnschrift", 8F, FontStyle.Regular, GraphicsUnit.Point);
            btnUndoView.Location = new Point(728, 619);
            btnUndoView.Name = "btnUndoView";
            btnUndoView.Size = new Size(84, 27);
            btnUndoView.TabIndex = 5;
            btnUndoView.Text = "Undo/Go Back";
            btnUndoView.UseVisualStyleBackColor = true;
            btnUndoView.Click += btnUndoView_Click;
            // 
            // btnSaveJPG
            // 
            btnSaveJPG.Font = new Font("Bahnschrift", 8F, FontStyle.Regular, GraphicsUnit.Point);
            btnSaveJPG.Location = new Point(556, 619);
            btnSaveJPG.Name = "btnSaveJPG";
            btnSaveJPG.Size = new Size(80, 27);
            btnSaveJPG.TabIndex = 6;
            btnSaveJPG.Text = "Save to JPG";
            btnSaveJPG.UseVisualStyleBackColor = true;
            btnSaveJPG.Click += btnSaveJPG_Click;
            // 
            // btnClear
            // 
            btnClear.Font = new Font("Bahnschrift", 8F, FontStyle.Regular, GraphicsUnit.Point);
            btnClear.Location = new Point(470, 619);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(80, 27);
            btnClear.TabIndex = 7;
            btnClear.Text = "Clear Screen";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // Mandelbrot
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(825, 650);
            Controls.Add(btnClear);
            Controls.Add(btnSaveJPG);
            Controls.Add(btnUndoView);
            Controls.Add(btnPanMode);
            Controls.Add(lblMousePos);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            Name = "Mandelbrot";
            Text = "Mandelbrot";
            Load += Mandelbrot_Load;
            Click += Mandelbrot_Click;
            MouseDown += Mandelbrot_MouseDown;
            MouseMove += Mandelbrot_MouseMove;
            MouseUp += Mandelbrot_MouseUp;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMousePos;
        private Button button1;
        private Label label1;
        private Button btnPanMode;
        private Button btnUndoView;
        private Button btnSaveJPG;
        private Button btnClear;
    }
}