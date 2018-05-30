using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
namespace GestionEtatCredit
{
    public partial class MainPage : Form
    {
        public static SQLiteConnection cnx;
        public delegate void OnShowDashBoard();
        public static OnShowDashBoard onShowDashBoard;
        string passFilePath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\confData";
        bool firstUse = true;
        public MainPage()
        {
            InitializeComponent();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            string directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\BD\CreditDB.s3db";
            string connectionString = @"Data Source=" + directory + ";Version=3;";
            cnx = new SQLiteConnection(connectionString);
            cnx.Open();
            initializeUserControls();
            DashBoardbtn.selected = true;
            if (File.Exists(passFilePath))
            {
                firstUse = false;
                startmsglabel.Visible = false;
                passlabel.Visible = false;
                passconfirmtxt.Visible = false;
                connectbtn.Text = "Connecter";
            }
            else
            {
                firstUse = true;
                startmsglabel.Visible = true;
                passlabel.Visible = true;
                passconfirmtxt.Visible = true;
                connectbtn.Text = "Creer compte";
            }
        }

        //Events
        //
        //
        private void Close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Hide_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Maximize_Click(object sender, EventArgs e)
        {
            if (WindowState==FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
            
        }
        private void TogleMenu_Click(object sender, EventArgs e)
        {
            if (MenuPanel.Size.Width == 296)
            {
                Hideanim.HideSync(MenuPanel);
                MenuPanel.Size = new Size(0, 586);
                MenuPanel.Visible = false;

            }
            else
            {
                MenuPanel.Visible = true;
                MenuPanel.Size = new Size(296, 586);
            }
        }

        private void DashBoardbtn_Click(object sender, EventArgs e)
        {
            DashBoard.Instance.BringToFront();
            Title.Text = "Tableau de bord";
            if (onShowDashBoard!=null)
            {
                onShowDashBoard.Invoke();
            }
        }

        private void Recherchbtn_Click(object sender, EventArgs e)
        {
            Research.Instance.BringToFront();
            Title.Text = "Recherche";
            if (onShowDashBoard != null)
            {
                onShowDashBoard.Invoke();
            }

        }

        private void Creditbtn_Click(object sender, EventArgs e)
        {
            Credit.Instance.BringToFront();
            Title.Text = "Crédits";
        }

        private void Fonctionnairebrn_Click(object sender, EventArgs e)
        {
            Fonctionnair.Instance.BringToFront();
            Title.Text = "Fonctionnaires";
        }

        private void Donbtn_Click(object sender, EventArgs e)
        {
            Donation.Instance.BringToFront();
            Title.Text = "Dons";
        }

        private void dossierbtn_Click(object sender, EventArgs e)
        {
            Dossier.Instance.BringToFront();
            Title.Text = "Dossier";
        }

        //Functions
        //
        //

        void initializeUserControls()
        {

            MainPanel.Controls.Add(Credit.Instance);
            MainPanel.Controls.Add(DashBoard.Instance);
            MainPanel.Controls.Add(Fonctionnair.Instance);
            MainPanel.Controls.Add(Donation.Instance);
            MainPanel.Controls.Add(Research.Instance);
            MainPanel.Controls.Add(Dossier.Instance);
            DashBoard.Instance.Dock = DockStyle.Fill;
            Credit.Instance.Dock = DockStyle.Fill;
            Fonctionnair.Instance.Dock = DockStyle.Fill;
            Donation.Instance.Dock = DockStyle.Fill;
            Research.Instance.Dock = DockStyle.Fill;
            Dossier.Instance.Dock = DockStyle.Fill;
            DashBoard.Instance.BringToFront();
        }

        void changeToMainSize()
        {
            //
            this.Size = new Size(1152, 666);
            this.MinimumSize = new Size(1000, 600);
        }

        bool checkLogin(string pass)
        {

            if (passwordtxt.Text == pass)
            {
                return true;
            }
            else
                return false;
        }
        private void connectbtn_Click(object sender, EventArgs e)
        {
            if (firstUse)
            {
                if (passwordtxt.Text != "")
                {
                    if (passwordtxt.Text==passconfirmtxt.Text)
                    {
                        savePassword(passwordtxt.Text);
                        Login.Visible = false;
                        changeToMainSize();
                    }
                    else
                    {
                        MessageBox.Show("la confirmation du mot de passe est incorrecte", "info"); 
                    }
                }
                else
                {
                    MessageBox.Show("Veuillez saisir un mot de passe", "info");
                }
            }
            else
            {
                if (checkLogin(getPassword()))
                {
                    Login.Visible = false;
                    changeToMainSize();
                }
                else
                {
                    MessageBox.Show("Le mot de passe est incorect", "info");
                }
            }
            //string directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location) + @"\confData";
            //string password = getPassword(directory);
            //if (checkLogin(password))
            //{
            //    Login.Visible = false;
            //    changeToMainSize();
            //}
            //else
            //{
            //    MessageBox.Show("Le nom d'utilisateur ou mot de passe est incorect", "info");
            //}
            
        }


        void savePassword(string password)
        {
            string directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            string file = directory+@"\confData";
            byte[] ba = Encoding.UTF8.GetBytes(password);
            File.WriteAllBytes(file, ba);
        }
        
        string getPassword()
        {
            byte[] ba2 = File.ReadAllBytes(passFilePath);
            string password = Encoding.UTF8.GetString(ba2);
            return password;
        }

        
    }
}
