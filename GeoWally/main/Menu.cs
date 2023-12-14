using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace Geo_Wall_E
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void NewProjectButton_Click(object sender, EventArgs e)
        {
            Work work = new Work();
            work.Show();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void WallEInformation_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = @".\GeoWall-E.pdf";
            process.Start();
        }

        private void ReportButton_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = @".\Informe 3er Proyecto.pdf";
            process.Start();
        }
    }
}
