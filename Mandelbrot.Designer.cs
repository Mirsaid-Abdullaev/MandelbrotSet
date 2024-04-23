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
            btnZoomMode = new Button();
            btnDefaultView = new Button();
            SuspendLayout();
            // 
            // lblMousePos
            // 
            lblMousePos.AutoSize = true;
            lblMousePos.Font = new Font("Bahnschrift", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblMousePos.Location = new Point(3, 609);
            lblMousePos.Name = "lblMousePos";
            lblMousePos.Size = new Size(90, 19);
            lblMousePos.TabIndex = 0;
            lblMousePos.Text = "MousePos: ";
            // 
            // btnPanMode
            // 
            btnPanMode.Font = new Font("Bahnschrift", 8F, FontStyle.Regular, GraphicsUnit.Point);
            btnPanMode.Location = new Point(819, 76);
            btnPanMode.Name = "btnPanMode";
            btnPanMode.Size = new Size(84, 27);
            btnPanMode.TabIndex = 4;
            btnPanMode.Text = "Pan Mode";
            btnPanMode.UseVisualStyleBackColor = true;
            btnPanMode.Click += btnPanMode_Click;
            // 
            // btnUndoView
            // 
            btnUndoView.Font = new Font("Bahnschrift", 8F, FontStyle.Regular, GraphicsUnit.Point);
            btnUndoView.Location = new Point(819, 175);
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
            btnSaveJPG.Location = new Point(819, 43);
            btnSaveJPG.Name = "btnSaveJPG";
            btnSaveJPG.Size = new Size(84, 27);
            btnSaveJPG.TabIndex = 6;
            btnSaveJPG.Text = "Save to JPG";
            btnSaveJPG.UseVisualStyleBackColor = true;
            btnSaveJPG.Click += btnSaveJPG_Click;
            // 
            // btnClear
            // 
            btnClear.Font = new Font("Bahnschrift", 8F, FontStyle.Regular, GraphicsUnit.Point);
            btnClear.Location = new Point(819, 10);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(84, 27);
            btnClear.TabIndex = 7;
            btnClear.Text = "Clear Screen";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // btnZoomMode
            // 
            btnZoomMode.Font = new Font("Bahnschrift", 8F, FontStyle.Regular, GraphicsUnit.Point);
            btnZoomMode.Location = new Point(819, 109);
            btnZoomMode.Name = "btnZoomMode";
            btnZoomMode.Size = new Size(84, 27);
            btnZoomMode.TabIndex = 8;
            btnZoomMode.Text = "Zoom Mode";
            btnZoomMode.UseVisualStyleBackColor = true;
            btnZoomMode.Click += btnZoomMode_Click;
            // 
            // btnDefaultView
            // 
            btnDefaultView.Font = new Font("Bahnschrift", 8F, FontStyle.Regular, GraphicsUnit.Point);
            btnDefaultView.Location = new Point(819, 142);
            btnDefaultView.Name = "btnDefaultView";
            btnDefaultView.Size = new Size(84, 27);
            btnDefaultView.TabIndex = 9;
            btnDefaultView.Text = "Default View";
            btnDefaultView.UseVisualStyleBackColor = true;
            btnDefaultView.Click += btnDefaultView_Click;
            // 
            // Mandelbrot
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(911, 630);
            Controls.Add(btnDefaultView);
            Controls.Add(btnZoomMode);
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
        private Button btnZoomMode;
        private Button btnDefaultView;
    }
}