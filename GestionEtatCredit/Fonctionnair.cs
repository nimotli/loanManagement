using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
namespace GestionEtatCredit
{
    public partial class Fonctionnair : UserControl
    {
        DataTable dt=new DataTable();
        private static Fonctionnair _instance;
        public static Fonctionnair Instance
        {
            get
            {
                if (_instance==null)
                {
                    _instance = new Fonctionnair();
                }
                return _instance;
            }
        }

        private void Fonctionnair_Load(object sender, EventArgs e)
        {
            ajouterbtn.selected = true;
            intializeDataGridView();
        }

        public Fonctionnair()
        {
            InitializeComponent();
        }

        private void ajouterbtn_Click(object sender, EventArgs e)
        {
            AjouterPanel.BringToFront();
        }

        private void modifierbtn_Click(object sender, EventArgs e)
        {
            if (fonctionnairedgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un fonctionnaire");
                AjouterPanel.BringToFront();
            }
            else
            {
                ModifierPanel.BringToFront();
            }
        }

        private void supprimerbtn_Click(object sender, EventArgs e)
        {
            if (fonctionnairedgv.SelectedRows.Count==0)
            {
                MessageBox.Show("Veuillez sélectionner un fonctionnaire");
                AjouterPanel.BringToFront();
            }
            else
            {
                SupprimerPanel.BringToFront();
                questionlabel.Text = "Voulez vous vraiment supprimer " + fonctionnairedgv.SelectedRows[0].Cells[1].Value.ToString() + " " + fonctionnairedgv.SelectedRows[0].Cells[2].Value.ToString() + " d'une façon permanente ? ";
            }
        }

        private void snonbtn_Click(object sender, EventArgs e)
        {
            AjouterPanel.BringToFront();
            supprimerbtn.selected = false;
            ajouterbtn.selected = true;
        }

        private void fonctionnairedgv_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                mcintxt.Text = fonctionnairedgv.SelectedRows[0].Cells[0].Value.ToString();
                mnomtxt.Text = fonctionnairedgv.SelectedRows[0].Cells[1].Value.ToString();
                mprenomtxt.Text = fonctionnairedgv.SelectedRows[0].Cells[2].Value.ToString();
                mcnsstxt.Text = fonctionnairedgv.SelectedRows[0].Cells[3].Value.ToString();
                msfamcmb.Text = fonctionnairedgv.SelectedRows[0].Cells[4].Value.ToString();
                mnbrenfanttxt.Text = fonctionnairedgv.SelectedRows[0].Cells[5].Value.ToString();
                mphontxt.Text = fonctionnairedgv.SelectedRows[0].Cells[6].Value.ToString();
                madressetxt.Text= fonctionnairedgv.SelectedRows[0].Cells[7].Value.ToString();
                mpprtxt.Text= fonctionnairedgv.SelectedRows[0].Cells[8].Value.ToString();
            }
            catch (Exception)
            {
            }
        }

        //utility Functions

        void clearAdd()
        {
            acintxt.Text = "";
            acintxt.Focus();
            anomtxt.Text = "";
            aprenomtxt.Text = "";
            acnsstxt.Text = "";
            asfamcmb.Text = "";
            anbrenfanttxt.Text = "";
            aphontxt.Text = "";
            aadressetxt.Text = "";
            apprtxt.Text = "";
        }

        void intializeDataGridView()
        {
            string requete = "select cin as 'CIN ', nom as 'Nom' , prenom as 'Prenom', cnops as 'cnops', sitFamiliale as 'Situation familiale' , nbrEnfant as 'Nombre enfants', numPhon as 'Numero de telephone', adresse as 'Adresse',ppr as 'PPR' from Fonctionnaire";
            dt.Clear();
            dt.Load(Utility.query(requete, MainPage.cnx));
            Utility.fillDatagridview(fonctionnairedgv, dt);
        }

        //Database Manipulation

        private void avalidetbtn_Click(object sender, EventArgs e)
        {
            string requete = "insert into Fonctionnaire values(UPPER('"+acintxt.Text+"'),'"+anomtxt.Text + "','"+aprenomtxt.Text + "'";
            
            #region generate Query
            if (acnsstxt.Text != "")
            {
                requete += ",'" + acnsstxt.Text + "'";
            }
            else
            {
                requete += ",null";
            }
            if (asfamcmb.Text != "")
            {
                requete += ",'" + asfamcmb.Text + "'";
            }
            else
            {
                requete += ",null";
            }
            if (anbrenfanttxt.Text != "")
            {
                requete += "," + anbrenfanttxt.Text ;
            }
            else
            {
                requete += ",null";
            }
            if (aphontxt.Text != "")
            {
                requete += ",'" + aphontxt.Text + "'";
            }
            else
            {
                requete += ",null";
            }
            if (aadressetxt.Text != "")
            {
                requete += ",'" + aadressetxt.Text + "'";
            }
            else
            {
                requete += ",null";
            }
            if (apprtxt.Text != "")
            {
                requete += ",'" + apprtxt.Text + "'";
            }
            else
            {
                requete += ",null";
            }
            #endregion

            requete += ")";
            string buffer = Utility.nonQuery(requete, MainPage.cnx);
            if (buffer != null)
                MessageBox.Show(buffer);
            else
            {
                MessageBox.Show("Fonctionnaire ajouté","Info");
                clearAdd();
                intializeDataGridView();
            }
        }

        private void mvaliderbtn_Click(object sender, EventArgs e)
        {
            string requete = "Update Fonctionnaire set cin=UPPER('" + mcintxt.Text + "'),nom='" + mnomtxt.Text + "',prenom='" + mprenomtxt.Text+"'";

            #region generate Query
            if (mcnsstxt.Text != "")
            {
                requete += ",cnops='" + mcnsstxt.Text + "'";
            }
            else
            {
                requete += ",cnops=null";
            }
            if (msfamcmb.Text != "")
            {
                requete += ",sitFamiliale='" + msfamcmb.Text + "'";
            }
            else
            {
                requete += ",sitFamiliale=null";
            }
            if (mnbrenfanttxt.Text != "")
            {
                requete += ",nbrEnfant=" + mnbrenfanttxt.Text;
            }
            else
            {
                requete += ",nbrEnfant=null";
            }
            if (mphontxt.Text != "")
            {
                requete += ",numPhon='" + mphontxt.Text + "'";
            }
            else
            {
                requete += ",numPhon=null";
            }
            if (madressetxt.Text != "")
            {
                requete += ",adresse='" + madressetxt.Text + "'";
            }
            else
            {
                requete += ",adresse=null";
            }
            if (mpprtxt.Text != "")
            {
                requete += ",ppr='" + mpprtxt.Text + "'";
            }
            else
            {
                requete += ",ppr=null";
            }
            #endregion
            requete+= " where cin='" + fonctionnairedgv.SelectedRows[0].Cells[0].Value.ToString() + "'";
            string buffer = Utility.nonQuery(requete, MainPage.cnx);
            if (buffer != null)
                MessageBox.Show(buffer);
            else
            {
                MessageBox.Show("Fonctionnaire modifié", "Info");
                AjouterPanel.BringToFront();
                intializeDataGridView();
                modifierbtn.selected = false;
                ajouterbtn.selected = true;
            }
        }

        private void souibtn_Click(object sender, EventArgs e)
        {
            string requete = "Delete from Fonctionnaire where cin='" + fonctionnairedgv.SelectedRows[0].Cells[0].Value.ToString() + "'";
            string requete2= "Delete from Credit where cin='" + fonctionnairedgv.SelectedRows[0].Cells[0].Value.ToString() + "'";
            string buffer = Utility.nonQuery(requete, MainPage.cnx);
            string buffer2 = Utility.nonQuery(requete2, MainPage.cnx);
            if (buffer!=null)
            {
                MessageBox.Show(buffer);
            }
            else
            {
                if (buffer2!=null)
                {
                    MessageBox.Show(buffer2);
                }
                else
                {
                    MessageBox.Show("Fonctionnaire supprimé", "Info");
                    intializeDataGridView();
                    AjouterPanel.BringToFront();
                    supprimerbtn.selected = false;
                    ajouterbtn.selected = true;
                }
            }
            
        }

        private void recherchbtn_Click(object sender, EventArgs e)
        {
            string requete = "select cin as 'CIN ', nom as 'Nom' , prenom as 'Prenom', cnops as 'Cnops', sitFamiliale as 'Situation familiale' , nbrEnfant as 'Nombre enfants', numPhon as 'Numero de telephone', adresse as 'Adresse',ppr as 'PPR' from Fonctionnaire ";
            bool first = true;
            if (rcintxt.Text != "")
            {
                requete += "where cin like'%" + rcintxt.Text + "%' ";
                first = false;
            }
            if (rnomtxt.Text != "")
            {
                if (first)
                {
                    requete += "where nom like'%" + rnomtxt.Text + "%' ";
                    first = false;
                }
                else
                {
                    requete += "and nom like'%" + rnomtxt.Text + "%' ";
                }
            }
            if (rcnsstxt.Text != "")
            {
                if (first)
                {
                    requete += "where cnops like '%" + rcnsstxt.Text + "%' ";
                    first = false;
                }
                else
                {
                    requete += "and cnops like '%" + rcnsstxt.Text + "%' ";
                }
            }

            dt.Clear();
            dt.Load(Utility.query(requete, MainPage.cnx));
            Utility.fillDatagridview(fonctionnairedgv, dt);
        }

       

        
    }
}
