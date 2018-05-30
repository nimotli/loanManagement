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
    public partial class Donation : UserControl
    {
        DataTable dt = new DataTable();
        private static Donation _instance;
        public static Donation Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Donation();
                }
                return _instance;
            }
        }

        private void Donation_Load(object sender, EventArgs e)
        {
            ajouterbtn.selected = true;
            intializeDataGridView();
        }

        public Donation()
        {
            InitializeComponent();
        }

        //Events
        //

        private void ajouterbtn_Click(object sender, EventArgs e)
        {
            AjouterPanel.BringToFront();
        }

        private void modifierbtn_Click(object sender, EventArgs e)
        {
            if (dondgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un don");
                AjouterPanel.BringToFront();
            }
            else
            {
                ModifierPanel.BringToFront();
            }
            
        }
        private void acintxt_TextChanged(object sender, EventArgs e)
        {
            string requete = "select nom,prenom from Fonctionnaire where UPPER(cin)=UPPER('" + acintxt.Text + "')";
            DataTable tempDt = new DataTable();
            tempDt.Load(Utility.query(requete, MainPage.cnx));
            if (tempDt.Rows.Count > 0)
            {
                anomtxt.Text = tempDt.Rows[0][0].ToString();
                aprenomtxt.Text = tempDt.Rows[0][1].ToString();
            }
            else
            {
                anomtxt.Text = "";
                aprenomtxt.Text = "";
            }
        }

        private void mcintxt_TextChanged(object sender, EventArgs e)
        {
            string requete = "select nom,prenom from Fonctionnaire where UPPER(cin)=UPPER('" + mcintxt.Text + "')";
            DataTable tempDt = new DataTable();
            tempDt.Load(Utility.query(requete, MainPage.cnx));
            if (tempDt.Rows.Count > 0)
            {
                mnomtxt.Text = tempDt.Rows[0][0].ToString();
                mprenomtxt.Text = tempDt.Rows[0][1].ToString();
            }
            else
            {
                mnomtxt.Text = "";
                mprenomtxt.Text = "";
            }
        }
        private void supprimerbtn_Click(object sender, EventArgs e)
        {
            if (dondgv.SelectedRows.Count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un don");
                AjouterPanel.BringToFront();
            }
            else
            {
                SupprimerPanel.BringToFront();
            }
        }
        private void snonbtn_Click(object sender, EventArgs e)
        {
            AjouterPanel.BringToFront();
            supprimerbtn.selected = false;
            ajouterbtn.selected = true;
        }

        private void dondgv_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                mcintxt.Text = dondgv.SelectedRows[0].Cells[1].Value.ToString();
                mnomtxt.Text = dondgv.SelectedRows[0].Cells[2].Value.ToString();
                mprenomtxt.Text = dondgv.SelectedRows[0].Cells[3].Value.ToString();
                mmontanttxt.Text = dondgv.SelectedRows[0].Cells[4].Value.ToString();
                mtypetxt.Text = dondgv.SelectedRows[0].Cells[5].Value.ToString();
                mdatedp.Value = DateTime.Parse( dondgv.SelectedRows[0].Cells[6].Value.ToString());
            }
            catch (Exception)
            {
            }
        }
        //utility
        //
        void clearAdd()
        {
            acintxt.Text = "";
            anomtxt.Text = "";
            aprenomtxt.Text = "";
            amontant.Text = "";
            atypetxt.Text = "";
        }

        void intializeDataGridView()
        {
            string requete = "select idDon, d.cin as 'CIN',f.nom as 'Nom',f.prenom as 'Prenom',montant as 'Montant',type as 'Type',date as 'Date' from Don d,Fonctionnaire f where d.cin=f.cin";
            dt.Clear();
            dt.Load(Utility.query(requete, MainPage.cnx));
            Utility.fillDatagridview(dondgv, dt);
        }

        //database manipulation
        //
        private void avalidetbtn_Click(object sender, EventArgs e)
        {
            string date = adate.Value.ToString("yyyy-MM-dd");
            string requete = "insert into don(montant, type, date, cin) values('" + amontant.Text + "','" + atypetxt.Text + "','" +date+ "',UPPER('" + acintxt.Text + "'))";
            string buffer = Utility.nonQuery(requete, MainPage.cnx);
            if (buffer != null)
                MessageBox.Show(buffer);
            else
            {
                MessageBox.Show("Don ajouté", "Info");
                clearAdd();
                intializeDataGridView();
            }
        }

        private void mvaliderbtn_Click(object sender, EventArgs e)
        {
            string date = mdatedp.Value.ToString("yyyy-MM-dd");
            string requete = "Update Don set cin='" + mcintxt.Text + "',type='" + mtypetxt.Text + "',date='" + date + "',montant=" + mmontanttxt.Text + " where idDon='" + dondgv.SelectedRows[0].Cells[0].Value.ToString() + "'";
            string buffer = Utility.nonQuery(requete, MainPage.cnx);
            if (buffer != null)
                MessageBox.Show(buffer);
            else
            {
                MessageBox.Show("Don modifié", "Info");
                AjouterPanel.BringToFront();
                modifierbtn.selected = false;
                ajouterbtn.selected = true;
                intializeDataGridView();
            }
        }

        private void souibtn_Click(object sender, EventArgs e)
        {
            string requete = "Delete from Don where idDon='" + dondgv.SelectedRows[0].Cells[0].Value.ToString() + "'";
            string buffer = Utility.nonQuery(requete, MainPage.cnx);
            if (buffer != null)
            {
                MessageBox.Show(buffer);
            }
            else
            {
                MessageBox.Show("Don supprimé", "Info");
                intializeDataGridView();
                AjouterPanel.BringToFront();
                supprimerbtn.selected = false;
                ajouterbtn.selected = true;
            }
        }

        private void recherchbtn_Click(object sender, EventArgs e)
        {
            string requete = "select idDon, d.cin as 'CIN',f.nom as 'Nom',f.prenom as 'Prenom',montant as 'Montant',type as 'Type',date as 'Date' from Don d,Fonctionnaire f where d.cin=f.cin ";
            
            if (rndontxt.Text != "")
            {
                requete += "and idDon like'%" + rndontxt.Text + "%' ";
            }
            if (rcintxt.Text != "")
            {
                requete += "and d.cin like'%" + rcintxt.Text + "%' ";
                
            }
            dt.Clear();
            dt.Load(Utility.query(requete, MainPage.cnx));
            Utility.fillDatagridview(dondgv, dt);
        }
    }
}
