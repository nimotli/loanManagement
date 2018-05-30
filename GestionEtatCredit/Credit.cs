using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace GestionEtatCredit
{
    public partial class Credit : UserControl
    {
        private static Credit _instance;
        public static Credit Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Credit();
                }
                return _instance;
            }
        }
        public Credit()
        {
            InitializeComponent();
            calculatePayments();
        }

        private void suivantetap2btn_Click(object sender, EventArgs e)
        {
            if (npaytxt.Text!="" && montantretcmb.Text!="" && montantcmb.Text!="")
            {
                Etap3Panel.BringToFront();
                Etap1btn.BackColor = Color.Gray;
                etap2btn.BackColor = Color.Gray;
                Etap3btn.BackColor = Color.FromArgb(255, 54, 52, 103);
                fillValidationPage();
            }
            else
            {
                MessageBox.Show("Veuillez entrer tout les information", "info");
            }
        }

        private void suivant1_Click(object sender, EventArgs e)
        {
            

            if (anomtxt.Text!="")
            {
                string checkRequete = "select count(idCredit) from Credit where cin = '" + acintxt.Text + "'";
                DataTable tempDt = new DataTable();
                tempDt.Load(Utility.query(checkRequete, MainPage.cnx));
                int number = int.Parse(tempDt.Rows[0][0].ToString());

                if (number < 3)
                {
                    Etap2Panel.BringToFront();
                    Etap1btn.BackColor = Color.Gray;
                    etap2btn.BackColor = Color.FromArgb(255, 54, 52, 103);
                    Etap3btn.BackColor = Color.Gray;
                }
                else
                {
                    MessageBox.Show("Le fonctionnaire que vous avez selectionné a atteint le montant maximal des crédits ( 3 )", "Info");
                }
                
            }
            else
            {
                MessageBox.Show("Veuillez entrer le cin d'un fonctionnaire existant dans la base de données", "info");
            }
            
        }

        private void retour_Click(object sender, EventArgs e)
        {
            Etap1Panel.BringToFront();
            Etap1btn.BackColor = Color.FromArgb(255, 54, 52, 103);
            etap2btn.BackColor = Color.Gray;
            Etap3btn.BackColor = Color.Gray;
        }

        private void Credit_Load(object sender, EventArgs e)
        {
            Etap1btn.BackColor = Color.FromArgb(255, 54, 52, 103);
            etap2btn.BackColor = Color.Gray;
            Etap3btn.BackColor = Color.Gray;
        }

        private void acintxt_TextChanged(object sender, EventArgs e)
        {
            string requete = "select * from Fonctionnaire where UPPER(cin)=UPPER('" + acintxt.Text + "')";
            DataTable tempDt = new DataTable();
            tempDt.Load(Utility.query(requete, MainPage.cnx));
            if (tempDt.Rows.Count > 0)
            {
                anomtxt.Text = tempDt.Rows[0][1].ToString();
                aprenomtxt.Text = tempDt.Rows[0][2].ToString();
                acnsstxt.Text= tempDt.Rows[0][3].ToString();
                asitfamtxt.Text = tempDt.Rows[0][4].ToString();
                anbrenfant.Text = tempDt.Rows[0][5].ToString();
                aphontxt.Text = tempDt.Rows[0][8].ToString();
            }
            else
            {
                anomtxt.Text = "";
                aprenomtxt.Text = "";
                acnsstxt.Text = "";
                asitfamtxt.Text = "";
                anbrenfant.Text = "";
                aphontxt.Text = "";
            }
        }

        private void acnsstxt_TextChanged(object sender, EventArgs e)
        {
            string requete = "select * from Fonctionnaire where UPPER(cnops)=UPPER('" + acnsstxt.Text + "')";
            DataTable tempDt = new DataTable();
            tempDt.Load(Utility.query(requete, MainPage.cnx));
            if (tempDt.Rows.Count > 0)
            {
                acintxt.Text = tempDt.Rows[0][0].ToString();
                anomtxt.Text = tempDt.Rows[0][1].ToString();
                aprenomtxt.Text = tempDt.Rows[0][2].ToString();
                asitfamtxt.Text = tempDt.Rows[0][4].ToString();
                anbrenfant.Text = tempDt.Rows[0][5].ToString();
                aphontxt.Text = tempDt.Rows[0][8].ToString();
                aadressetxt.Text= tempDt.Rows[0][7].ToString();
            }
            else
            {
                acintxt.Text = "";
                anomtxt.Text = "";
                aprenomtxt.Text = "";
                asitfamtxt.Text = "";
                anbrenfant.Text = "";
                aphontxt.Text = "";
                aadressetxt.Text = "";
            }
        }

        private void montantcmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            montantretcmb.Items.Clear();
            if(montantcmb.Text == "1000")
            {
                montantretcmb.Items.Add("200");
                montantretcmb.Items.Add("500");
            }
            else if (montantcmb.Text == "1500")
            {
                montantretcmb.Items.Add("250");
                montantretcmb.Items.Add("300");
                montantretcmb.Items.Add("500");
            }
            else if (montantcmb.Text == "2000")
            {
                montantretcmb.Items.Add("200");
                montantretcmb.Items.Add("500");
            }
            else if (montantcmb.Text == "2500")
            {
                montantretcmb.Items.Add("250");
                montantretcmb.Items.Add("500");
            }
            else if (montantcmb.Text == "3000")
            {
                montantretcmb.Items.Add("250");
                montantretcmb.Items.Add("300");
                montantretcmb.Items.Add("500");
            }
            else if (montantcmb.Text == "3500")
            {
                montantretcmb.Items.Add("350");
                montantretcmb.Items.Add("500");
            }
            else if (montantcmb.Text == "4000")
            {
                montantretcmb.Items.Add("400");
                montantretcmb.Items.Add("500");
                montantretcmb.Items.Add("1000");
            }
        }

        private void valider_Click(object sender, EventArgs e)
        {

            string startDate, endDate = "";
            DateTime date = datedp.Value;
            DateTime bufferstart, bufferend;

            bufferstart = date;
            if (bufferstart.Day!=1)
            {
                bufferstart = bufferstart.AddMonths(1);
                bufferstart = new DateTime(bufferstart.Year, bufferstart.Month, 1);
            }
            startDate = bufferstart.ToString("yyyy-MM-dd");

            bufferend = bufferstart;
            bufferend = bufferend.AddMonths(int.Parse(moisretinftxt.Text)-1);
            try
            {
                bufferend = new DateTime(bufferend.Year, bufferend.Month, 30);
            }
            catch (Exception)
            {
                bufferend = new DateTime(bufferend.Year, bufferend.Month, 27);
            }
            
            endDate = bufferend.ToString("yyyy-MM-dd");
            
            string requete = "insert into Credit (numPay,montant,montantRet,rest,dateDebut,dateFin,cin) values('" + npaytxt.Text + "'," + montantinftxt.Text + "," + mretinftxt.Text + "," + montantinftxt.Text + ",'" + startDate + "','"+endDate+"',UPPER('" + cininftxt.Text + "'))";
            string buffer = Utility.nonQuery(requete, MainPage.cnx);
            if (buffer != null)
                MessageBox.Show(buffer);
            else
            {
                generateContrat(npaytxt.Text, cininftxt.Text, nominftxt.Text, prenominftxt.Text, aadressetxt.Text,montantinftxt.Text,montantretcmb.Text,moisretinftxt.Text,aphontxt.Text);
                Etap1Panel.BringToFront();
                acintxt.Text = "";
                npaytxt.Text = "";
                montantcmb.Text = "";
                montantretcmb.Text = "";
                Etap1btn.BackColor = Color.FromArgb(255, 54, 52, 103);
                etap2btn.BackColor = Color.Gray;
                Etap3btn.BackColor = Color.Gray;
            }
        }
        


        void fillValidationPage()
        {
            cininftxt.Text = acintxt.Text;
            sfaminftxt.Text = asitfamtxt.Text;
            nominftxt.Text = anomtxt.Text;
            prenominftxt.Text = aprenomtxt.Text;
            montantinftxt.Text = montantcmb.Text;
            mretinftxt.Text = montantretcmb.Text;
            datedebutinftxt.Text = datedp.Value.ToShortDateString();
            moisretinftxt.Text = (int.Parse(montantcmb.Text) / int.Parse(montantretcmb.Text)).ToString();
        }

        void generateContrat(string npay,string cin,string nom ,string prenom,string adresse,string amount,string returnAmount,string duration,string ppr)
        {
            string amountInLetters="", returnAmountInLetters="", durationInLetters="",startDate,endDate="";
            DateTime date = datedp.Value;
            DateTime bufferstart, bufferend;


            bufferstart = date;
            if (bufferstart.Day != 1)
            {
                bufferstart = bufferstart.AddMonths(1);
            }
            startDate = "1/" + bufferstart.Month + "/" + bufferstart.Year;
            
            bufferend = bufferstart.AddMonths(int.Parse(duration)-1);
            if (bufferend.Month!=2)
            {
                endDate = "30/" + bufferend.Month + "/" + bufferend.Year;
            }
            else
            {
                endDate = "27/" + bufferend.Month + "/" + bufferend.Year;
            }
            

            #region setting Letter numbers
            if (amount =="1000")
            {
                amountInLetters = "ألف";
            }
            else if(amount == "1500")
            {
                amountInLetters = "الف و خمسمائة";
            }
            else if (amount == "2000")
            {
                amountInLetters = "ألفين";
            }
            else if (amount == "2500")
            {
                amountInLetters = "ألفين و خمسمائة";
            }
            else if (amount == "3000")
            {
                amountInLetters = "تلاتة الاف";
            }
            else if (amount == "3500")
            {
                amountInLetters = "تلاتة الاف و خمسمائة";
            }
            else if (amount == "4000")
            {
                amountInLetters = "أربعة الاف";
            }

            if (returnAmount == "200")
            {
                returnAmountInLetters = "مائتان";
            }
            else if (returnAmount == "250")
            {
                returnAmountInLetters = "مائتان وخمسون";
            }
            else if (returnAmount == "300")
            {
                returnAmountInLetters = "ثلاثمائة";
            }
            else if (returnAmount == "350")
            {
                returnAmountInLetters = "ثلاثمائة و خمسون";
            }
            else if (returnAmount == "400")
            {
                returnAmountInLetters = "اربعمائة";
            }
            else if (returnAmount == "500")
            {
                returnAmountInLetters = "خمسمائة";
            }
            else if (returnAmount == "1000")
            {
                returnAmountInLetters = "ألف";
            }

            if(duration == "2")
            {
                durationInLetters = "شهرين";
            }
            else if (duration == "3")
            {
                durationInLetters = "تلاتة أشهر";
            }
            else if (duration == "4")
            {
                durationInLetters = "أربعة أشهر";
            }
            else if (duration == "5")
            {
                durationInLetters = "خمسة أشهر";
            }
            else if (duration == "6")
            {
                durationInLetters = "ستة أشهر";
            }
            else if (duration == "7")
            {
                durationInLetters = "سبعة أشهر";
            }
            else if (duration == "8")
            {
                durationInLetters = "ثمانية أشهر";
            }
            else if (duration == "10")
            {
                durationInLetters = "عشرة أشهر";
            }
            else if (duration == "12")
            {
                durationInLetters = "اثنا عشر شهرا";
            }

            #endregion

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("Contrats/" + npay + cin + ".pdf", FileMode.Append));
            doc.Open();

            string fontLoc = @"c:\windows\fonts\arialuni.ttf";
            string filenumber = DateTime.Today.Year + "/" + npay;

            PdfPTable table = new PdfPTable(1);
            table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;

            BaseFont bf = BaseFont.CreateFont(fontLoc, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            iTextSharp.text.Font f = new iTextSharp.text.Font(bf, 12);

            Phrase text1 = new Phrase("جمعية الأعمال الإجتماعية                                                   ملف رقم : "+filenumber+
                "\nلموظفي بلدية السمارة", f);
            PdfPCell cell1 = new PdfPCell(text1);
            cell1.Border=iTextSharp.text.Rectangle.NO_BORDER;
            cell1.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cell1.SetLeading(20, 0);
            table.AddCell(cell1);

            Phrase text11 = new Phrase("                                                                                           PPR : "+ppr, f);
            PdfPCell cell11 = new PdfPCell(text11);
            cell11.HorizontalAlignment = Element.ALIGN_LEFT;
            cell11.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell11.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cell11.SetLeading(20, 0);
            table.AddCell(cell11);

            Phrase text2 = new Phrase("\n\nأمر بتحويل مؤقت\n(لأجل سلف اجتماعي)", f);
            PdfPCell cell2 = new PdfPCell(text2);
            cell2.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell2);

            PdfPCell cell4 = new PdfPCell(new Phrase("\n\n\n\n "));
            cell4.Border = iTextSharp.text.Rectangle.NO_BORDER;
            table.AddCell(cell4);

            Paragraph text3 = new Paragraph(20,"أنا الموقع أسفله :                     nom et prénom :    " + nom + " " + prenom + "\n" +
                "رقم بطاقة التعريف الوطنية :   " + cin + "\n" +
                "العنوان : " + adresse + "\n\n" +
                "أرجو من السيد القابض البلدي, أن يخصم من راتبي, ماقدره شهريا "+returnAmountInLetters+" درهم ( "+returnAmount+" درهم ) و ذالك لمدة ( "+durationInLetters+" ) "+duration+" أشهر , ابتداءا من "+startDate+" الى غاية "+endDate+ " ويحوله لجمعية الأعمال الإجتماعية لموظفي بلدية السمارة في الحساب المفتوح بالخزينة الإقليمية بالسمارة, تحت الرقم :" + "\n"+
                "31.0260.1033.122.00317720177" +"\n"+
                " وذلك مقابل حصولي من الجمعية على قرض إجتماعي, بدون فائدة, مبلغه "+ amountInLetters + " درهم("+amount+" درهم)" +"\n\n\n"+
                "                                                                              السمارة في :    " + DateTime.Today.ToShortDateString()+"\n \n\n\n"+
                "توقيع رئيس الجمعية                                             إمضاء المعني بالأمر مصادق عليه", f);
            PdfPCell cell3 = new PdfPCell(text3);
            cell3.SetLeading(20,0);
            cell3.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell3.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            table.AddCell(cell3);


            doc.Add(table);
            doc.Close();

            try
            {
                string directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                System.Diagnostics.Process.Start(directory + @"\Contrats\" + npay + cin + ".pdf");
            }
            catch (Exception)
            {
            }
            //Paragraph par = new Paragraph("");
            //doc.Add(par);
            //doc.Close();
        }


        void calculatePayments()
        {
            DateTime today = DateTime.Today;

            if (today.Day < 27)
            {
                    today = new DateTime(today.Year, today.Month, 27);
            }

            DataTable tempDT = new DataTable();
            tempDT.Load(Utility.query("select * from Credit", MainPage.cnx));

            foreach (DataRow item in tempDT.Rows)
            {
                if (int.Parse(item[4].ToString()) > 0)
                {
                    DateTime itemDate = Convert.ToDateTime(item[5].ToString());


                    itemDate = new DateTime(itemDate.Year, itemDate.Month, 27);
                    int yearDeference = today.Year - itemDate.Year;
                    int monthDeference = (yearDeference * 12) + (today.Month - itemDate.Month);
                    int rest = 0;
                    if ((monthDeference * int.Parse(item[3].ToString()))<0)
                    {
                        rest = int.Parse(item[2].ToString());
                    }
                    else
                    {
                         rest = int.Parse(item[2].ToString()) - (monthDeference * int.Parse(item[3].ToString()));
                    }
                    if (rest < 0)
                    {
                        rest = 0;
                    }
                    string requete = "Update Credit set rest=" + rest + " where idCredit ='" + item[0].ToString() + "'";
                    Utility.nonQuery(requete, MainPage.cnx);
                }
            }

        }
    }
}
