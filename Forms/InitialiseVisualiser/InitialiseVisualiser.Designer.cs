using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
namespace MandelbrotSet
{
    partial class InitialiseVisualiser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InitialiseVisualiser));
            label1 = new Label();
            btnInitialiseApp = new Button();
            label2 = new Label();
            label5 = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Bahnschrift", 20F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(619, 33);
            label1.TabIndex = 0;
            label1.Text = "Welcome to the Mandelbrot Visualiser application!";
            // 
            // btnInitialiseApp
            // 
            btnInitialiseApp.BackColor = Color.DodgerBlue;
            btnInitialiseApp.FlatStyle = FlatStyle.Popup;
            btnInitialiseApp.Font = new Font("Bahnschrift", 14F, FontStyle.Regular, GraphicsUnit.Point);
            btnInitialiseApp.Location = new Point(423, 330);
            btnInitialiseApp.Name = "btnInitialiseApp";
            btnInitialiseApp.Size = new Size(203, 81);
            btnInitialiseApp.TabIndex = 1;
            btnInitialiseApp.Text = "Proceed to Visualiser";
            btnInitialiseApp.UseVisualStyleBackColor = false;
            btnInitialiseApp.Click += BtnInitialiseApp_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Bahnschrift", 14F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(12, 388);
            label2.Name = "label2";
            label2.Size = new Size(294, 23);
            label2.TabIndex = 2;
            label2.Text = "Made by Mirsaid Abdullaev (2024)";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(17, 487);
            label5.Name = "label5";
            label5.Size = new Size(0, 15);
            label5.TabIndex = 8;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.ProjIcon1;
            pictureBox1.Location = new Point(12, 88);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(250, 247);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // InitialiseVisualiser
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(638, 422);
            Controls.Add(pictureBox1);
            Controls.Add(label5);
            Controls.Add(label2);
            Controls.Add(btnInitialiseApp);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "InitialiseVisualiser";
            Text = "Mandelbrot Visualiser - made by Mirsaid Abdullaev";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnInitialiseApp;
        private Label label2;
        private Label label5;
        private PictureBox pictureBox1;
    }
}
