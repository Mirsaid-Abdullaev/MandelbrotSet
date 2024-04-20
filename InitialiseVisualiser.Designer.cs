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
            label1 = new Label();
            btnInitialiseApp = new Button();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            lblCurrentRes = new Label();
            ResolutionChoice = new TrackBar();
            MaxIterChoice = new TrackBar();
            label6 = new Label();
            lblCurrentMaxIter = new Label();
            ((System.ComponentModel.ISupportInitialize)ResolutionChoice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MaxIterChoice).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Bahnschrift", 24F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(97, 9);
            label1.Name = "label1";
            label1.Size = new Size(731, 39);
            label1.TabIndex = 0;
            label1.Text = "Welcome to the Mandelbrot Visualiser application!";
            // 
            // btnInitialiseApp
            // 
            btnInitialiseApp.Font = new Font("Bahnschrift", 14F, FontStyle.Regular, GraphicsUnit.Point);
            btnInitialiseApp.Location = new Point(719, 396);
            btnInitialiseApp.Name = "btnInitialiseApp";
            btnInitialiseApp.Size = new Size(203, 81);
            btnInitialiseApp.TabIndex = 1;
            btnInitialiseApp.Text = "Proceed to Visualiser";
            btnInitialiseApp.UseVisualStyleBackColor = true;
            btnInitialiseApp.Click += btnInitialiseApp_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 537);
            label2.Name = "label2";
            label2.Size = new Size(186, 15);
            label2.TabIndex = 2;
            label2.Text = "Made by Mirsaid Abdullaev (2024)";
            // 
            // label3
            // 
            label3.Font = new Font("Bahnschrift", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(12, 396);
            label3.Name = "label3";
            label3.Size = new Size(401, 29);
            label3.TabIndex = 6;
            label3.Text = "Visualiser Resolution (higher = longer rendering time)";
            // 
            // label4
            // 
            label4.Font = new Font("Bahnschrift", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(17, 72);
            label4.Name = "label4";
            label4.Size = new Size(295, 67);
            label4.TabIndex = 7;
            label4.Text = "Please choose custom settings for the visualiser below or go with the default setting, then click Proceed to Visualiser";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(17, 487);
            label5.Name = "label5";
            label5.Size = new Size(0, 15);
            label5.TabIndex = 8;
            // 
            // lblCurrentRes
            // 
            lblCurrentRes.Font = new Font("Bahnschrift", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblCurrentRes.Location = new Point(12, 480);
            lblCurrentRes.Name = "lblCurrentRes";
            lblCurrentRes.Size = new Size(401, 22);
            lblCurrentRes.TabIndex = 9;
            lblCurrentRes.Text = "Current Resolution: ";
            // 
            // ResolutionChoice
            // 
            ResolutionChoice.LargeChange = 100;
            ResolutionChoice.Location = new Point(12, 432);
            ResolutionChoice.Maximum = 1000;
            ResolutionChoice.Minimum = 100;
            ResolutionChoice.Name = "ResolutionChoice";
            ResolutionChoice.Size = new Size(401, 45);
            ResolutionChoice.SmallChange = 50;
            ResolutionChoice.TabIndex = 10;
            ResolutionChoice.Value = 550;
            ResolutionChoice.Scroll += ResolutionChoice_Scroll;
            ResolutionChoice.ValueChanged += ResolutionChoice_ValueChanged;
            // 
            // MaxIterChoice
            // 
            MaxIterChoice.LargeChange = 100;
            MaxIterChoice.Location = new Point(12, 300);
            MaxIterChoice.Maximum = 1000;
            MaxIterChoice.Minimum = 50;
            MaxIterChoice.Name = "MaxIterChoice";
            MaxIterChoice.Size = new Size(401, 45);
            MaxIterChoice.SmallChange = 50;
            MaxIterChoice.TabIndex = 12;
            MaxIterChoice.Value = 250;
            MaxIterChoice.Scroll += MaxIterChoice_Scroll;
            MaxIterChoice.ValueChanged += MaxIterChoice_ValueChanged;
            // 
            // label6
            // 
            label6.Font = new Font("Bahnschrift", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(12, 264);
            label6.Name = "label6";
            label6.Size = new Size(401, 29);
            label6.TabIndex = 11;
            label6.Text = "Mandelbrot Max Iterations (higher = longer rendering time)";
            // 
            // lblCurrentMaxIter
            // 
            lblCurrentMaxIter.Font = new Font("Bahnschrift", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblCurrentMaxIter.Location = new Point(12, 339);
            lblCurrentMaxIter.Name = "lblCurrentMaxIter";
            lblCurrentMaxIter.Size = new Size(401, 22);
            lblCurrentMaxIter.TabIndex = 13;
            lblCurrentMaxIter.Text = "Current Max Iterations: ";
            // 
            // InitialiseVisualiser
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(944, 561);
            Controls.Add(lblCurrentMaxIter);
            Controls.Add(MaxIterChoice);
            Controls.Add(label6);
            Controls.Add(ResolutionChoice);
            Controls.Add(lblCurrentRes);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(btnInitialiseApp);
            Controls.Add(label1);
            Name = "InitialiseVisualiser";
            Text = "Mandelbrot Visualiser";
            ((System.ComponentModel.ISupportInitialize)ResolutionChoice).EndInit();
            ((System.ComponentModel.ISupportInitialize)MaxIterChoice).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button btnInitialiseApp;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label lblCurrentRes;
        private TrackBar ResolutionChoice;
        private TrackBar MaxIterChoice;
        private Label label6;
        private Label lblCurrentMaxIter;
    }
}
