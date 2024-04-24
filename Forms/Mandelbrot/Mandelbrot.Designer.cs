using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mandelbrot));
            lblMousePos = new Label();
            btnPanMode = new Button();
            btnUndoView = new Button();
            btnSaveJPG = new Button();
            btnClear = new Button();
            btnZoomMode = new Button();
            btnDefaultView = new Button();
            lblTimer = new Label();
            lblShowTime = new Label();
            label1 = new Label();
            txtIterCount = new TextBox();
            checkBox1 = new CheckBox();
            checkBox2 = new CheckBox();
            checkBox3 = new CheckBox();
            SuspendLayout();
            // 
            // lblMousePos
            // 
            lblMousePos.AutoSize = true;
            lblMousePos.BorderStyle = BorderStyle.FixedSingle;
            lblMousePos.Font = new Font("Bahnschrift", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblMousePos.Location = new Point(12, 614);
            lblMousePos.Name = "lblMousePos";
            lblMousePos.Size = new Size(92, 21);
            lblMousePos.TabIndex = 0;
            lblMousePos.Text = "MousePos: ";
            // 
            // btnPanMode
            // 
            btnPanMode.BackColor = Color.DodgerBlue;
            btnPanMode.Font = new Font("Bahnschrift", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnPanMode.Location = new Point(820, 199);
            btnPanMode.Name = "btnPanMode";
            btnPanMode.Size = new Size(90, 57);
            btnPanMode.TabIndex = 4;
            btnPanMode.Text = "Toggle Pan Mode";
            btnPanMode.UseVisualStyleBackColor = false;
            btnPanMode.Click += BtnPanMode_Click;
            // 
            // btnUndoView
            // 
            btnUndoView.BackColor = Color.DodgerBlue;
            btnUndoView.Font = new Font("Bahnschrift", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnUndoView.Location = new Point(820, 325);
            btnUndoView.Name = "btnUndoView";
            btnUndoView.Size = new Size(90, 57);
            btnUndoView.TabIndex = 5;
            btnUndoView.Text = "Undo/Go Back";
            btnUndoView.UseVisualStyleBackColor = false;
            btnUndoView.Click += BtnUndoView_Click;
            // 
            // btnSaveJPG
            // 
            btnSaveJPG.BackColor = Color.DodgerBlue;
            btnSaveJPG.Font = new Font("Bahnschrift", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnSaveJPG.Location = new Point(820, 74);
            btnSaveJPG.Name = "btnSaveJPG";
            btnSaveJPG.Size = new Size(90, 57);
            btnSaveJPG.TabIndex = 6;
            btnSaveJPG.Text = "Save Current to JPG";
            btnSaveJPG.UseVisualStyleBackColor = false;
            btnSaveJPG.Click += BtnSaveJPG_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.DodgerBlue;
            btnClear.Font = new Font("Bahnschrift", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnClear.Location = new Point(820, 11);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(90, 57);
            btnClear.TabIndex = 7;
            btnClear.Text = "Clear Screen";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += BtnClear_Click;
            // 
            // btnZoomMode
            // 
            btnZoomMode.BackColor = Color.DodgerBlue;
            btnZoomMode.Font = new Font("Bahnschrift", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnZoomMode.Location = new Point(820, 137);
            btnZoomMode.Name = "btnZoomMode";
            btnZoomMode.Size = new Size(90, 57);
            btnZoomMode.TabIndex = 8;
            btnZoomMode.Text = "Toggle Zoom Mode";
            btnZoomMode.UseVisualStyleBackColor = false;
            btnZoomMode.Click += BtnZoomMode_Click;
            // 
            // btnDefaultView
            // 
            btnDefaultView.BackColor = Color.DodgerBlue;
            btnDefaultView.Font = new Font("Bahnschrift", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btnDefaultView.Location = new Point(820, 262);
            btnDefaultView.Name = "btnDefaultView";
            btnDefaultView.Size = new Size(90, 57);
            btnDefaultView.TabIndex = 9;
            btnDefaultView.Text = "Back to Default View";
            btnDefaultView.UseVisualStyleBackColor = false;
            btnDefaultView.Click += BtnDefaultView_Click;
            // 
            // lblTimer
            // 
            lblTimer.BorderStyle = BorderStyle.FixedSingle;
            lblTimer.Font = new Font("Bahnschrift SemiLight Condensed", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblTimer.Location = new Point(817, 520);
            lblTimer.Name = "lblTimer";
            lblTimer.Size = new Size(90, 43);
            lblTimer.TabIndex = 10;
            lblTimer.Text = "Last render time (in s):";
            // 
            // lblShowTime
            // 
            lblShowTime.BorderStyle = BorderStyle.FixedSingle;
            lblShowTime.Font = new Font("Bahnschrift", 8F, FontStyle.Regular, GraphicsUnit.Point);
            lblShowTime.Location = new Point(817, 567);
            lblShowTime.Name = "lblShowTime";
            lblShowTime.Size = new Size(90, 23);
            lblShowTime.TabIndex = 11;
            // 
            // label1
            // 
            label1.BorderStyle = BorderStyle.FixedSingle;
            label1.Font = new Font("Bahnschrift SemiLight Condensed", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(817, 468);
            label1.Name = "label1";
            label1.Size = new Size(90, 23);
            label1.TabIndex = 12;
            label1.Text = "Max Iterations";
            // 
            // txtIterCount
            // 
            txtIterCount.Location = new Point(817, 494);
            txtIterCount.Name = "txtIterCount";
            txtIterCount.Size = new Size(90, 23);
            txtIterCount.TabIndex = 13;
            // 
            // checkBox1
            // 
            checkBox1.BackColor = Color.Transparent;
            checkBox1.ForeColor = Color.Orange;
            checkBox1.Location = new Point(820, 388);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(87, 19);
            checkBox1.TabIndex = 14;
            checkBox1.Text = "Palette 1";
            checkBox1.UseVisualStyleBackColor = false;
            checkBox1.CheckedChanged += CheckBox1_CheckedChanged;
            // 
            // checkBox2
            // 
            checkBox2.BackColor = Color.Transparent;
            checkBox2.ForeColor = Color.Orange;
            checkBox2.Location = new Point(820, 413);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new Size(87, 19);
            checkBox2.TabIndex = 15;
            checkBox2.Text = "Palette 2";
            checkBox2.UseVisualStyleBackColor = false;
            checkBox2.CheckedChanged += CheckBox2_CheckedChanged;
            // 
            // checkBox3
            // 
            checkBox3.BackColor = Color.Transparent;
            checkBox3.ForeColor = Color.Orange;
            checkBox3.Location = new Point(820, 438);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new Size(87, 19);
            checkBox3.TabIndex = 16;
            checkBox3.Text = "Palette 3";
            checkBox3.UseVisualStyleBackColor = false;
            checkBox3.CheckedChanged += CheckBox3_CheckedChanged;
            // 
            // Mandelbrot
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(925, 642);
            Controls.Add(checkBox3);
            Controls.Add(checkBox2);
            Controls.Add(checkBox1);
            Controls.Add(txtIterCount);
            Controls.Add(label1);
            Controls.Add(lblShowTime);
            Controls.Add(lblTimer);
            Controls.Add(btnDefaultView);
            Controls.Add(btnZoomMode);
            Controls.Add(btnClear);
            Controls.Add(btnSaveJPG);
            Controls.Add(btnUndoView);
            Controls.Add(btnPanMode);
            Controls.Add(lblMousePos);
            Font = new Font("Bahnschrift", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Mandelbrot";
            Text = "Mandelbrot Visualiser - by Mirsaid Abdullaev";
            Shown += Mandelbrot_Shown;
            Paint += Mandelbrot_Paint;
            MouseDown += Mandelbrot_MouseDown;
            MouseMove += Mandelbrot_MouseMove;
            MouseUp += Mandelbrot_MouseUp;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblMousePos;
        private Button btnPanMode;
        private Button btnUndoView;
        private Button btnSaveJPG;
        private Button btnClear;
        private Button btnZoomMode;
        private Button btnDefaultView;
        private Label lblTimer;
        private Label lblShowTime;
        private Label label1;
        private TextBox txtIterCount;
        private CheckBox checkBox1;
        private CheckBox checkBox2;
        private CheckBox checkBox3;
    }
}