using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DevOpsCheckinPolicies
{
    public partial class CheckFileExistSettings : Form
    {
        public CheckFileExistConfig Config { get; set; }
        public CheckFileExistSettings(CheckFileExistConfig _config)
        {
            InitializeComponent();
            Config = _config;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilename.Text))
            {
                MessageBox.Show("Please Specify Filename!");
            }

            if (string.IsNullOrEmpty(txtLocation.Text))
            {
                if (MessageBox.Show("You didnt Specify Folder name!, are you sure you want to ignore the location of the file?","Confirm", MessageBoxButtons.OKCancel,MessageBoxIcon.Information) == DialogResult.OK)
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    DialogResult = DialogResult.Cancel;
                }

            }
            else
            {
                DialogResult = DialogResult.OK;
            }

            if (DialogResult == DialogResult.OK)
            {
                Config.Filename = txtFilename.Text;
                Config.location = txtLocation.Text;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
