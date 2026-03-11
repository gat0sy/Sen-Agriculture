using AppSenAgriculture.Helpers;
using AppSenAgriculture.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppSenAgriculture
{
    public partial class frmConnexion : Form
    {
        public frmConnexion()
        {
            InitializeComponent();
        }

        private void btnQuitter_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSeConnecter_Click(object sender, EventArgs e)
        {
            // ----ANCIEN CODE---- -
            //frmMDI f=new frmMDI();
            //f.Show();
            //this.Hide();
            string identifiant = txtIdentifiant.Text;
            string password = txtMotDePasse.Text;

            string hashedPassword = Crypto.HashMd5(password);

            using (BdSenAgricultureContext db = new BdSenAgricultureContext())
            {
                //Attention
                //Il faut que le mot de passe soit un hash au préalable sinon la connexion ne pourra pas marcher
                //Example:  user.MotDePasseUtilisateur = Crypto.HashMd5(plainPassword);
                var user = db.utilisateurs
                             .FirstOrDefault(u =>
                                 u.IdentifiantUtilisateur == identifiant &&
                                 u.MotDePasseUtilisateur == hashedPassword);

                if (user != null)
                {
                    frmMDI f = new frmMDI();
                    f.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Identifiant ou mot de passe incorrect !");
                }
            }
        }
    }
}
