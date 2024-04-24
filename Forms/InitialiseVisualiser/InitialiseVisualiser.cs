using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using static MandelbrotSet.Utils.Design;
namespace MandelbrotSet
{
    public partial class InitialiseVisualiser : Form
    {
        public bool UserClosed = true;
        public InitialiseVisualiser()
        {
            InitializeComponent();
            SetControlGradient(this, Colours1);
            this.ForeColor = Color.WhiteSmoke;
        }
        private void BtnInitialiseApp_Click(object sender, EventArgs e)
        {
            UserClosed = false;
            this.Close();
        }
    }
}
