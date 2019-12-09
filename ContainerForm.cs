using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TallerArchivoVectores
{
    public partial class ContainerForm : Form
    {
        public ContainerForm()
        {
            InitializeComponent();
        }

        Form currentForm = null;
        private void chargeNewForm(Form childForm) {
            if (currentForm != null) {
                currentForm.Close();
            }

            currentForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(childForm);
            panelContainer.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            chargeNewForm(new MainWindowsForm());
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            //chargeNewForm(new MainWindowsForm());
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            chargeNewForm(new EstudiantesForm());
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm login = new LoginForm();
            login.Show();
        }
    }
}
