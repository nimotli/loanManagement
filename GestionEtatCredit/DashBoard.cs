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
using Excel = Microsoft.Office.Interop.Excel;
namespace GestionEtatCredit
{
    public partial class DashBoard : UserControl
    {
        private static DashBoard _instance;
        public static DashBoard Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DashBoard();
                }
                return _instance;
            }
        }
        public DashBoard()
        {
            InitializeComponent();
        }
        private void DashBoard_Load(object sender, EventArgs e)
        {
            initializeItems();
            MainPage.onShowDashBoard += initializeItems;
        }

        void initializeItems()
        {
            generateCreditsStats();
            generateOtherStats();
            generateRevenuStats();
        }

        void generateCreditsStats()
        {
            DataTable dt = new DataTable();
            string requete1 = "select count(idCredit) from credit where rest > 0";
            string requete2 = "select count(idCredit) from credit where rest = 0";
            string requete3 = "select count(idCredit) from credit";


            dt.Load(Utility.query(requete1, MainPage.cnx));
            creditc.Text = dt.Rows[0][0].ToString();
            dt.Clear();
            dt.Load(Utility.query(requete2, MainPage.cnx));
            creditp.Text = dt.Rows[0][0].ToString();
            dt.Clear();
            dt.Load(Utility.query(requete3, MainPage.cnx));
            creditt.Text = dt.Rows[0][0].ToString();
        }

        void generateOtherStats()
        {
            DataTable dt = new DataTable();
            string requete1 = "select count(idDon) from don";
            string requete2 = "select count(cin) from fonctionnaire";
            string requete3 = "select sum(montant) from don";

            dt.Load(Utility.query(requete2, MainPage.cnx));
            autref.Text = dt.Rows[0][0].ToString();
            dt = new DataTable();
            dt.Load(Utility.query(requete1, MainPage.cnx));
            autred.Text = dt.Rows[0][0].ToString();
            dt = new DataTable();
            dt.Load(Utility.query(requete3, MainPage.cnx));
            autrem.Text = dt.Rows[0][0].ToString()+" DH";
        }

        void generateRevenuStats()
        {
            DataTable dt = new DataTable();
            string requete1 = "select sum(montantRet) from credit where rest > 0";
            string requete2 = "select sum(rest) from credit";
            string requete3 = "select sum(montant) from credit";

            dt.Load(Utility.query(requete2, MainPage.cnx));
            revenur.Text = dt.Rows[0][0].ToString() + " DH";
            dt = new DataTable();
            dt.Load(Utility.query(requete1, MainPage.cnx));
            revenum.Text = dt.Rows[0][0].ToString() + " DH";
            dt = new DataTable();
            dt.Load(Utility.query(requete3, MainPage.cnx));
            revenut.Text = dt.Rows[0][0].ToString() + " DH";
        }

        private void detailbtn_Click(object sender, EventArgs e)
        {
            DetailsPanel.BringToFront();
        }

        private void fonctionnairessvc_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string requete = "select cin as 'CIN ', nom as 'Nom' , prenom as 'Prenom', cnops as 'cnops', sitFamiliale as 'Situation familiale' , nbrEnfant as 'Nombre enfants', numPhon as 'Numero de telephone', adresse as 'Adresse',ppr as 'PPR' from Fonctionnaire";
            dt.Load(Utility.query(requete, MainPage.cnx));
            Utility.exportToExcele(dt);
        }

        private void creditssvc_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string requete = "select numPay as 'Numero de pay',f.nom as 'Nom',f.prenom as 'Prenom',montant as 'Montant de credit',montantRet as 'Montant de retour',rest as 'Reste',dateDebut as 'Date de debut',dateFin as 'Date de fin',c.cin as 'CIN' from Credit c,Fonctionnaire f Where c.cin=f.cin";
            dt.Load(Utility.query(requete, MainPage.cnx));
            dt.TableName = "Credit";
            Utility.exportToExcele(dt);
        }

        private void donssvc_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            string requete = "select idDon, d.cin as 'CIN',f.nom as 'Nom',f.prenom as 'Prenom',montant as 'Montant',type as 'Type',date as 'Date' from Don d,Fonctionnaire f where d.cin=f.cin";
            dt.Load(Utility.query(requete, MainPage.cnx));
            dt.TableName = "Don";
            Utility.exportToExcele(dt);
        }

        private void retourbtn_Click(object sender, EventArgs e)
        {
            DashPanel.BringToFront();
        }

        private void generateRepport_Click(object sender, EventArgs e)
        {
            string directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            int currentLine = 2;
            #region creating the repport file
            Excel.Application excelApp = new Excel.Application();

            Excel.Workbook excelWorkBook = excelApp.Workbooks.Add(1);

            Excel.Worksheet excelWorkSheet = excelWorkBook.Sheets.Add();
            excelWorkSheet.Name = "Rapport";
            #endregion

            #region generating the repport

            #region arbitary part
            DataTable dt = new DataTable();
            string requete1 = "select sum(montantRet) from credit where rest > 0";
            string requete2 = "select sum(rest) from credit";
            string requete3 = "select sum(montant) from credit";

            dt.Load(Utility.query(requete1, MainPage.cnx));
            excelWorkSheet.Cells[currentLine, 1] = "Montant revenue du mois";
            excelWorkSheet.Cells[currentLine, 3] = dt.Rows[0][0].ToString() + " DH";
            currentLine++;

            dt = new DataTable();
            dt.Load(Utility.query(requete2, MainPage.cnx));
            excelWorkSheet.Cells[currentLine, 1] = "Montant total du rest";
            excelWorkSheet.Cells[currentLine, 3] = dt.Rows[0][0].ToString() + " DH";
            currentLine++;


            dt = new DataTable();
            dt.Load(Utility.query(requete3, MainPage.cnx));
            excelWorkSheet.Cells[currentLine, 1] = "Montant total crédité";
            excelWorkSheet.Cells[currentLine, 3] = dt.Rows[0][0].ToString() + " DH";
            currentLine++;

            var HeadingsRange = excelWorkSheet.Range[excelWorkSheet.Cells[2, 3], excelWorkSheet.Cells[currentLine -1, 3]];
            HeadingsRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            HeadingsRange.Borders.Weight = 2d;
            HeadingsRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver);
            HeadingsRange = excelWorkSheet.Range[excelWorkSheet.Cells[2, 1], excelWorkSheet.Cells[currentLine - 1, 2]];
            HeadingsRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange);
            #endregion


            if (creditcheck.Checked)
            {
                DataTable dt1 = new DataTable();
                string requete = "select numPay as 'Numero de pay',f.nom as 'Nom',f.prenom as 'Prenom',montant as 'Montant de credit',montantRet as 'Montant de retour',rest as 'Reste',dateDebut as 'Date de debut',dateFin as 'Date de fin',c.cin as 'CIN' from Credit c,Fonctionnaire f Where c.cin=f.cin";
                dt1.Load(Utility.query(requete, MainPage.cnx));
                bool color = false;


                

                for (int i = 1; i < dt1.Columns.Count + 1; i++)
                {
                    excelWorkSheet.Cells[currentLine + 1, i] = dt1.Columns[i - 1].ColumnName;
                    var columnHeadingsRange = excelWorkSheet.Range[excelWorkSheet.Cells[currentLine + 1, 1], excelWorkSheet.Cells[currentLine + 1, dt1.Columns.Count]];
                    columnHeadingsRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange);
                    columnHeadingsRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    columnHeadingsRange.Borders.Weight = 2d;
                }

                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    for (int k = 0; k < dt1.Columns.Count; k++)
                    {
                        excelWorkSheet.Cells[currentLine+j + 2, k + 1].NumberFormat = "@";
                        excelWorkSheet.Cells[currentLine + j + 2, k + 1] = dt1.Rows[j].ItemArray[k].ToString();
                    }
                    var columnHeadingsRange = excelWorkSheet.Range[excelWorkSheet.Cells[currentLine + j + 2, 1], excelWorkSheet.Cells[currentLine + j + 2, dt1.Columns.Count]];
                    columnHeadingsRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    columnHeadingsRange.Borders.Weight = 2d;
                    if (color)
                    {
                        columnHeadingsRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver);
                        color = false;
                    }
                    else
                    {
                        color = true;
                    }
                }
                currentLine += dt1.Rows.Count + 2;
            }
            if (donscheck.Checked)
            {
                DataTable dt1 = new DataTable();
                string requete = "select idDon, d.cin as 'CIN',f.nom as 'Nom',f.prenom as 'Prenom',montant as 'Montant',type as 'Type',date as 'Date' from Don d,Fonctionnaire f where d.cin=f.cin";
                dt1.Load(Utility.query(requete, MainPage.cnx));
                bool color = false;




                for (int i = 1; i < dt1.Columns.Count + 1; i++)
                {
                    excelWorkSheet.Cells[currentLine + 1, i] = dt1.Columns[i - 1].ColumnName;
                    var columnHeadingsRange = excelWorkSheet.Range[excelWorkSheet.Cells[currentLine + 1, 1], excelWorkSheet.Cells[currentLine + 1, dt1.Columns.Count]];
                    columnHeadingsRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange);
                    columnHeadingsRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    columnHeadingsRange.Borders.Weight = 2d;
                }

                for (int j = 0; j < dt1.Rows.Count; j++)
                {
                    for (int k = 0; k < dt1.Columns.Count; k++)
                    {
                        excelWorkSheet.Cells[currentLine + j + 2, k + 1].NumberFormat = "@";
                        excelWorkSheet.Cells[currentLine + j + 2, k + 1] = dt1.Rows[j].ItemArray[k].ToString();
                    }
                    var columnHeadingsRange = excelWorkSheet.Range[excelWorkSheet.Cells[currentLine + j + 2, 1], excelWorkSheet.Cells[currentLine + j + 2, dt1.Columns.Count]];
                    columnHeadingsRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    columnHeadingsRange.Borders.Weight = 2d;
                    if (color)
                    {
                        columnHeadingsRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver);
                        color = false;
                    }
                    else
                    {
                        color = true;
                    }
                }
                currentLine += dt1.Rows.Count + 2;
            }
            if (creditcheck.Checked)
            {

            }

            #endregion

            #region save the repport
            try
            {
                excelWorkBook.SaveAs(directory + @"\Rapports\" + DateTime.Today.Day + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day + "-" + excelWorkSheet.Name + ".xlsx");
                directory = directory + @"\Rapports\" + DateTime.Today.Day + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day + "-" + excelWorkSheet.Name + ".xlsx";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            excelWorkBook.Close();
            excelApp.Quit();
            #endregion  
            System.Diagnostics.Process.Start(directory);

        }
    }
}
