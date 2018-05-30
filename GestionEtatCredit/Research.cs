using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionEtatCredit
{
    public partial class Research : UserControl
    {
        DataTable dt = new DataTable();
        private static Research _instance;
        public static Research Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Research();
                }
                return _instance;
            }
        }
        public Research()
        {
            InitializeComponent();
            MainPage.onShowDashBoard += clearDgv;
        }

        private void retourbtn_Click(object sender, EventArgs e)
        {
            RechPanel.BringToFront();
        }

        private void validerbtn_Click(object sender, EventArgs e)
        {
            validate();
        }

        private void recherchbtn_Click(object sender, EventArgs e)
        {
            recherch();
        }

        void recherch()
        {
            #region Creating query
            string requete = "select cin as 'CIN ', nom as 'Nom' , prenom as 'Prenom', cnops as 'Cnops', sitFamiliale as 'Situation familiale' , nbrEnfant as 'Nombre enfants', numPhon as 'Numero de telephone', adresse as 'Adresse',ppr as 'PPR' from Fonctionnaire";
            bool first = true;
            //cnn cnss nom prenom numpay date
            if (numpayrech.Text != "")
            {
                requete = "select cin as 'CIN ', nom as 'Nom' , prenom as 'Prenom', cnops as 'Cnops', sitFamiliale as 'Situation familiale' , nbrEnfant as 'Nombre enfants', numPhon as 'Numero de telephone', adresse as 'Adresse',ppr as 'PPR' from Fonctionnaire where cin in (select cin from Credit Where numPay ='" + numpayrech.Text + "')";
                first = false;
            }
            if (cinrech.Text != "")
            {
                if (first)
                {
                    requete = requete + " Where cin like'%" + cinrech.Text + "%'";
                    first = false;
                }
                else
                {
                    requete = requete + " and cin like'%" + cinrech.Text + "%'";
                }
            }
            if (cnssrech.Text != "")
            {
                if (first)
                {
                    requete = requete + " Where cnops like'%" + cnssrech.Text + "%'";
                    first = false;
                }
                else
                {
                    requete = requete + " and cnops like'%" + cnssrech.Text + "%'";
                }
            }
            if (nomrech.Text != "")
            {
                if (first)
                {
                    requete = requete + " Where nom like'%" + nomrech.Text + "%'";
                    first = false;
                }
                else
                {
                    requete = requete + " and nom like'%" + nomrech.Text + "%'";
                }
            }
            if (prenomrech.Text != "")
            {
                if (first)
                {
                    requete = requete + " Where ppr like'%" + prenomrech.Text + "%'";
                    first = false;
                }
                else
                {
                    requete = requete + " and ppr='" + prenomrech.Text + "'";
                }
            }
            #endregion
            dt.Clear();
            dt.Load(Utility.query(requete, MainPage.cnx));
            Utility.fillDatagridview(fonctionnairedgv, dt);
        }


        void validate()
        {
            DataTable tempDT = new DataTable();
            DataTable tempDT2 = new DataTable();
            if (fonctionnairedgv.SelectedRows.Count > 0)
            {
                resPanel.BringToFront();
                string requete = "select * from Fonctionnaire where cin='" + fonctionnairedgv.SelectedRows[0].Cells[0].Value.ToString() + "'";
                tempDT.Load(Utility.query(requete, MainPage.cnx));
                try
                {
                    cinres.Text = tempDT.Rows[0][0].ToString();
                    nomres.Text = tempDT.Rows[0][1].ToString();
                    prenomres.Text = tempDT.Rows[0][2].ToString();
                    cnssres.Text = tempDT.Rows[0][3].ToString();
                    sitfamres.Text = tempDT.Rows[0][4].ToString();
                    nbrenfres.Text = tempDT.Rows[0][5].ToString();
                    phonres.Text = tempDT.Rows[0][6].ToString();
                    adresseRes.Text = tempDT.Rows[0][7].ToString();
                }
                catch (Exception)
                {

                }
                

                requete = "select idCredit as 'Id credit', numPay as 'Numero de pay',montant as 'Montant de credit',montantRet as 'Montant de retour',rest as 'Reste',dateDebut as 'Date de debut',dateFin as 'Date de fin',cin as 'CIN' from Credit where cin='" + cinres.Text + "'";
                tempDT2.Load(Utility.query(requete, MainPage.cnx));
                creditdgv.DataSource = tempDT2;
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un fonctionnaire", "Info");
            }
        }

        private void seeContract_Click(object sender, EventArgs e)
        {
            if (creditdgv.SelectedRows.Count>0)
            {
                try
                {
                    string directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                    System.Diagnostics.Process.Start( directory + @"\Contrats\" + creditdgv.SelectedRows[0].Cells[1].Value.ToString() + creditdgv.SelectedRows[0].Cells[7].Value.ToString() + ".pdf");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un credit");
            }
            
        }

        private void supprimercreditbtn_Click(object sender, EventArgs e)
        {
            if(creditdgv.SelectedRows.Count>0)
            {
                DialogResult dialogResult = MessageBox.Show("Voulez vous vraiment supprimer cette enregistrement ?", "info", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    string requete = "delete from Credit where idCredit ='" + creditdgv.SelectedRows[0].Cells[0].Value.ToString() + "'";
                    string buffer = Utility.nonQuery(requete, MainPage.cnx);
                    if (buffer != null)
                    {
                        MessageBox.Show(buffer);
                    }
                    else
                    {
                        MessageBox.Show("Credit supprimé", "Info");
                        RechPanel.BringToFront();
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un credit");
            }
        }
        private void clearDgv()
        {
            fonctionnairedgv.DataSource = null;
            fonctionnairedgv.Rows.Clear();
        }
    }
}
