using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
namespace GestionEtatCredit
{
    public static class Utility
    {


        public static bool connect(SQLiteConnection cnx)
        {
            string directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location)+ @"\CreditDB.s3db";
            MessageBox.Show(directory);
            string connectionString = @"Data Source="+directory+";Version=3;";
            try
            {
                cnx = new SQLiteConnection(connectionString);
                cnx.Open();
                MessageBox.Show("opened");
                return true;
            }
            catch (Exception )
            {
                return false;
            }
        }


        public static void closeConnection(SQLiteConnection cnx)
        {
            cnx.Close();
        }


        public static SQLiteDataReader query(string requete,SQLiteConnection cnx)
        {
            SQLiteCommand cmd;
            SQLiteDataReader reader;
            try
            {
                cmd = new SQLiteCommand(requete, cnx);
                reader = cmd.ExecuteReader();
                return reader;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }

        public static string nonQuery(string requete, SQLiteConnection cnx)
        {
            SQLiteCommand cmd;
            try
            {
                cmd = new SQLiteCommand(requete, cnx);
                cmd.ExecuteNonQuery();
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public static void fillDatagridview(DataGridView dgv,System.Data.DataTable dt)
        {
            dgv.DataSource = null;
            dgv.Rows.Clear();
            dgv.DataSource = dt;
        }

        public static void exportToExcele(System.Data.DataTable dt)
        {
            bool color = false;
            
            string directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            Excel.Application excelApp = new Excel.Application();

            Excel.Workbook excelWorkBook = excelApp.Workbooks.Add(1);
            
            Excel.Worksheet excelWorkSheet = excelWorkBook.Sheets.Add();
            excelWorkSheet.Name = dt.TableName;

            for (int i = 1; i < dt.Columns.Count + 1; i++)
            {
                excelWorkSheet.Cells[1, i] = dt.Columns[i - 1].ColumnName;
                var columnHeadingsRange = excelWorkSheet.Range[excelWorkSheet.Cells[1, 1], excelWorkSheet.Cells[1, dt.Columns.Count]];
                columnHeadingsRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange);
                columnHeadingsRange.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                columnHeadingsRange.Borders.Weight = 2d;
            }

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    excelWorkSheet.Cells[j + 2, k + 1].NumberFormat = "@";
                    excelWorkSheet.Cells[j + 2, k + 1] = dt.Rows[j].ItemArray[k].ToString();
                }
                var columnHeadingsRange = excelWorkSheet.Range[excelWorkSheet.Cells[j + 2, 1], excelWorkSheet.Cells[j + 2, dt.Columns.Count]];
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

            try
            {
                excelWorkBook.SaveAs(directory + @"\Rapports\" + DateTime.Today.Day + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day + "-" + dt.TableName + ".xlsx");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           excelWorkBook.Close();
            excelApp.Quit();

            System.Diagnostics.Process.Start(directory + @"\Rapports\" + DateTime.Today.Day + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day + "-" + dt.TableName + ".xlsx");
            
        }



    }
}
