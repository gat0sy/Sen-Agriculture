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

namespace AppSenAgriculture.Views.Parametre
{
    public partial class frmUtilisateur : Form
    {
        BdSenAgricultureContext db = new BdSenAgricultureContext();

        public frmUtilisateur()
        {
            InitializeComponent();
        }

        private void Effacer()
        {
            txtNomComplet.Text = string.Empty;
            txtAdresse.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtTel.Text = string.Empty;
            txtIdentifiant.Text = string.Empty;
            txtMotDePasse.Text = string.Empty;

            dgUtilisateur.DataSource = db.utilisateurs.ToList();
        }

        private void frmUtilisateur_Load(object sender, EventArgs e)
        {
            Effacer();
        }

        

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(dgUtilisateur.CurrentRow.Cells[0].Value.ToString());
                Utilisateur u = db.utilisateurs.Find(id);

                db.utilisateurs.Remove(u);
                db.SaveChanges();

                Effacer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgUtilisateur.CurrentRow == null)
                {
                    MessageBox.Show("Aucune ligne sélectionnée.");
                    return;
                }

                int id = int.Parse(dgUtilisateur.CurrentRow.Cells[0].Value.ToString());
                Utilisateur u = db.utilisateurs.Find(id);

                if (u == null)
                {
                    MessageBox.Show("Utilisateur introuvable.");
                    return;
                }

                u.NomCompletUtilisateur = txtNomComplet.Text;
                u.AdresseUtilisateur = txtAdresse.Text;
                u.EmailUtilisateur = txtEmail.Text;
                u.TelUtilisateur = txtTel.Text;
                u.IdentifiantUtilisateur = txtIdentifiant.Text;

                if (!string.IsNullOrEmpty(txtMotDePasse.Text))
                {
                    u.MotDePasseUtilisateur = Crypto.HashMd5(txtMotDePasse.Text);
                }

                db.SaveChanges();
                Effacer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSelectionner_Click(object sender, EventArgs e)
        {
            if (dgUtilisateur.CurrentRow != null)
            {
                var row = dgUtilisateur.CurrentRow;

                txtNomComplet.Text = row.Cells[1].Value?.ToString();
                txtAdresse.Text = row.Cells[2].Value?.ToString();
                txtEmail.Text = row.Cells[3].Value?.ToString();
                txtTel.Text = row.Cells[4].Value?.ToString();
                txtIdentifiant.Text = row.Cells[5].Value?.ToString();

                // pour ne pas montrer le mot de pass hashé
                txtMotDePasse.Text = "";
            }
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                Utilisateur u = new Utilisateur();

                u.NomCompletUtilisateur = txtNomComplet.Text;
                u.AdresseUtilisateur = txtAdresse.Text;
                u.EmailUtilisateur = txtEmail.Text;
                u.TelUtilisateur = txtTel.Text;
                u.IdentifiantUtilisateur = txtIdentifiant.Text;

                // Enregistre le hash du mot de pass
                u.MotDePasseUtilisateur = Crypto.HashMd5(txtMotDePasse.Text);

                db.utilisateurs.Add(u);
                db.SaveChanges();
                try
                {
                    EmailHelper.SendAccountCreatedEmail(
                        u.EmailUtilisateur,
                        u.NomCompletUtilisateur
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Utilisateur créé, mais l'email n'a pas pu être envoyé.\n" + ex.Message);
                }

                Effacer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
