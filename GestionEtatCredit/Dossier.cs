using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace GestionEtatCredit
{
    public partial class Dossier : UserControl
    {
        private static Dossier _instance;
        public static Dossier Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Dossier();
                }
                return _instance;
            }
        }
        public Dossier()
        {
            InitializeComponent();
        }

        private void suivant1_Click(object sender, EventArgs e)
        {
            etap2panel.BringToFront();
            page1panel.BackColor = Color.Gray;
            page2panel.BackColor = Color.FromArgb(255, 54, 52, 103); ;
            page3panel.BackColor = Color.Gray;
            page4panel.BackColor = Color.Gray;
            page5panel.BackColor = Color.Gray;
        }

        private void retour2_Click(object sender, EventArgs e)
        {
            etap1panel.BringToFront();
            page1panel.BackColor = Color.FromArgb(255, 54, 52, 103);
            page2panel.BackColor = Color.Gray;
            page3panel.BackColor = Color.Gray;
            page4panel.BackColor = Color.Gray;
            page5panel.BackColor = Color.Gray;
        }

        private void suivant2_Click(object sender, EventArgs e)
        {
            etap3panel.BringToFront();
            page1panel.BackColor = Color.Gray;
            page2panel.BackColor = Color.Gray;
            page3panel.BackColor = Color.FromArgb(255, 54, 52, 103); 
            page4panel.BackColor = Color.Gray;
            page5panel.BackColor = Color.Gray;
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            etap2panel.BringToFront();
            page1panel.BackColor = Color.Gray;
            page2panel.BackColor = Color.FromArgb(255, 54, 52, 103); ;
            page3panel.BackColor = Color.Gray;
            page4panel.BackColor = Color.Gray;
            page5panel.BackColor = Color.Gray;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            etap4panel.BringToFront();
            page1panel.BackColor = Color.Gray;
            page2panel.BackColor = Color.Gray;
            page3panel.BackColor = Color.Gray;
            page4panel.BackColor = Color.FromArgb(255, 54, 52, 103);
            page5panel.BackColor = Color.Gray;
        }

        private void retour4_Click(object sender, EventArgs e)
        {
            etap3panel.BringToFront();
            page1panel.BackColor = Color.Gray;
            page2panel.BackColor = Color.Gray;
            page3panel.BackColor = Color.FromArgb(255, 54, 52, 103); 
            page4panel.BackColor = Color.Gray;
            page5panel.BackColor = Color.Gray;
        }

        private void dossierbtn_Click(object sender, EventArgs e)
        {
            etap1panel.BringToFront();
            page1panel.BackColor = Color.FromArgb(255, 54, 52, 103);
            page2panel.BackColor = Color.Gray;
            page3panel.BackColor = Color.Gray;
            page4panel.BackColor = Color.Gray;
            page5panel.BackColor = Color.Gray;
            dossierpanel.BringToFront();
        }

        private void menubtn1_Click(object sender, EventArgs e)
        {
            InitializeComponent();
            menuPanel.BringToFront();
        }

        private void demendbtn_Click(object sender, EventArgs e)
        {
            demendPanel.BringToFront();
        }

        private void inscriptionbtn_Click(object sender, EventArgs e)
        {
            inscriptionpanel.BringToFront();
        }

        private void bunifuFlatButton2_Click_1(object sender, EventArgs e)
        {
            menuPanel.BringToFront();
        }

        private void Retourbtn_Click(object sender, EventArgs e)
        {
            menuPanel.BringToFront();
        }

        private void generateRegesterContract()
        {
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("Contrats/Inscription/" + DateTime.Today.Year + ".pdf", FileMode.Create));
            doc.Open();

            string fontLoc = @"c:\windows\fonts\arial.ttf";
            // string filenumber = DateTime.Today.Year + "/" + npay;

            PdfPTable table = new PdfPTable(1);
            table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;

            BaseFont bf = BaseFont.CreateFont(fontLoc, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            iTextSharp.text.Font f = new iTextSharp.text.Font(bf, 14);

            Phrase text1 = new Phrase("جمعية الأعمال الإجتماعية" +
                "\nلموظفي و أعوان بلدية السمارة", f);
            PdfPCell cell1 = new PdfPCell(text1);
            cell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell1.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cell1.SetLeading(20, 0);
            table.AddCell(cell1);

            BaseFont bf2 = BaseFont.CreateFont(fontLoc, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            iTextSharp.text.Font f2 = new iTextSharp.text.Font(bf, 16);
            f2.SetStyle(1);
            Phrase text2 = new Phrase("\nطلب الإنخراط في الجمعية", f2);
            PdfPCell cell2 = new PdfPCell(text2);
            cell2.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell2);

            f = new iTextSharp.text.Font(bf, 12);

            Paragraph text3 = new Paragraph(20, "\nالإسم العائلي و الشخصي : " + insctxt1.Text + " " + insctxt2.Text +
                "\nالدرجة : " + insctxt3.Text +
                "\nمقر العمل : " + insctxt4.Text +
                "\nتاريج و مكان الإزدياد : " + insctxt5.Text + " " + insctxt6.Text +
                "\nالعنوان الشخصي : " + insctxt7.Text +
                "\nرقم بطاقة التعريف الوطنية : " + insctxt8.Text +
                "\nالهاتف : " + insctxt9.Text +
                "\nاسم الزوج أو الزوجة : " + insctxt10.Text +
                "\nمهنة الزوج أو الزوجة : " + insctxt11.Text +
                "\nرقم بطاقة التعريف الوطنية للزوج أو الزوجة : " + insctxt12.Text +
                "\nعدد الأطفال تحت الكفالة : " + insctxt13.Text + "\n\n"
                , f);
            PdfPCell cell3 = new PdfPCell(text3);
            cell3.SetLeading(25, 0);
            cell3.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell3.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            table.AddCell(cell3);

            PdfPTable table2 = new PdfPTable(2);
            table2.WidthPercentage = 70;
            table2.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            
            int kidnumber=0;
            try
            {
                 kidnumber= int.Parse(insctxt13.Text);
            }
            catch (Exception)
            {}
            if (kidnumber>0)
            {
                Paragraph texttt = new Paragraph(20, "تاريخ الإزدياد", f);
                PdfPCell celltt = new PdfPCell(texttt);
                celltt.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                table2.AddCell(celltt);
                Paragraph textt = new Paragraph(20, "الإسم", f);
                PdfPCell cellt = new PdfPCell(textt);
                cellt.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                table2.AddCell(cellt);
                
                Paragraph text5 = new Paragraph(20, d1.Text, f);
                PdfPCell cell5 = new PdfPCell(text5);
                cell5.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                table2.AddCell(cell5);
                Paragraph text4 = new Paragraph(20, n1.Text, f);
                PdfPCell cell4 = new PdfPCell(text4);
                cell4.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                table2.AddCell(cell4);

                Paragraph text7 = new Paragraph(20, d2.Text, f);
                PdfPCell cell7 = new PdfPCell(text7);
                cell7.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                table2.AddCell(cell7);
                Paragraph text6 = new Paragraph(20, n2.Text, f);
                PdfPCell cell6 = new PdfPCell(text6);
                cell6.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                table2.AddCell(cell6);

                Paragraph text9 = new Paragraph(20, d3.Text, f);
                PdfPCell cell9 = new PdfPCell(text9);
                cell9.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                table2.AddCell(cell9);
                Paragraph text8 = new Paragraph(20, n3.Text, f);
                PdfPCell cell8 = new PdfPCell(text8);
                cell8.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                table2.AddCell(cell8);

                Paragraph text11 = new Paragraph(20, d4.Text, f);
                PdfPCell cell11 = new PdfPCell(text11);
                cell11.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                table2.AddCell(cell11);
                Paragraph text10 = new Paragraph(20, n4.Text, f);
                PdfPCell cell10 = new PdfPCell(text10);
                cell10.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                table2.AddCell(cell10);

                Paragraph text13 = new Paragraph(20, d5.Text, f);
                PdfPCell cell13 = new PdfPCell(text13);
                cell13.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                table2.AddCell(cell13);
                Paragraph text12 = new Paragraph(20, n5.Text, f);
                PdfPCell cell12 = new PdfPCell(text12);
                cell12.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                table2.AddCell(cell12);
            }
            PdfPTable table3 = new PdfPTable(1);
            table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;


            Phrase text22 = new Phrase("\nأشهد بصحة المعلومات,أعلاه, وألتزم باحترام القانون الإساسي للجمعية ونظامها الداخلي وتسديد واجب انخراطها السنوي بانتضام.\n\n"+
                                        "                                                         التوقيع :\n"+
                                        "ملاحظة :\n"+
                                        "تبعث طلبات الإنخراط مصحوبة بإذن للإقتطاع مصادق عليه و صورتين للتعريف ونسخة من بطاقة التعريف الوطنية للمنخرط و زوجته الى جمعية الأعمال الإجتماعية.", f);
            PdfPCell cell22 = new PdfPCell(text22);
            cell22.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell22.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cell22.SetLeading(20, 0);
            table3.AddCell(cell22);

            doc.Add(table);
            doc.Add(table2);
            doc.Add(table3);
            doc.Close();

            try
            {
                string directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                System.Diagnostics.Process.Start(directory + @"\Contrats\Inscription\" + DateTime.Today.Year + ".pdf");
            }
            catch (Exception)
            {
            }
        }

        void generateCutContract()
        {
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("Contrats/Demande/" + DateTime.Today.Year + ".pdf", FileMode.Create));
            doc.Open();

            string fontLoc = @"c:\windows\fonts\arial.ttf";

            PdfPTable table = new PdfPTable(1);
            table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;

            BaseFont bf = BaseFont.CreateFont(fontLoc, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            iTextSharp.text.Font f = new iTextSharp.text.Font(bf, 14);

            Phrase text1 = new Phrase("جمعية الأعمال الإجتماعية" +
                "\nلموظفي و أعوان بلدية السمارة", f);
            PdfPCell cell1 = new PdfPCell(text1);
            cell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell1.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cell1.SetLeading(20, 0);
            table.AddCell(cell1);

            BaseFont bf2 = BaseFont.CreateFont(fontLoc, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            iTextSharp.text.Font f2 = new iTextSharp.text.Font(bf, 16);
            f2.SetStyle(1);
            Phrase text2 = new Phrase("\n\nطلب الإنخراط في الجمعية", f2);
            PdfPCell cell2 = new PdfPCell(text2);
            cell2.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell2);

            Phrase text3 = new Phrase("\nأنا الموقع أدناه :  "+txt1.Text+" "+ txt2.Text+
                "\n\nالمزداد :  " + txt3.Text +
                "\n\nالساكن ب :  " + txt4.Text +
                "\n\nصاحب بطاقة التعريف رقم :  " + txt5.Text +
                "\n\nو الموظف بالجماعة الحضرية للسمارة منذ :  " + txt6.Text +
                "\n\nسلم :  " + txt7.Text +
                "\n\n إذن, وفق للمرسوم الملكي المؤرخ في 14 يونيو عام 1941, اقتطاع مبلغ شهري من راتبي قدره " + txt8.Text+ " درهما لصالح جمعية الأعمال الإجتماعية لموظفي و أعوان بلدية السمارة في حسابها المفتوح بالخزينة الإقليمية بالسمارة تحت رقم 31.0260.1033.122.00317720177 وحتى إشعار اخر مكتوب من طرفي."+
                "\n\n\n\n\nتوقيع المعني بالأمر          توقيع و ختم الأمر بالصرف          توقيع و ختم الجمعية\n(مصادق عليه)", f);
            PdfPCell cell3 = new PdfPCell(text3);
            cell3.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell3.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cell3.SetLeading(20, 0);
            table.AddCell(cell3);

            doc.Add(table);
            doc.Close();

            try
            {
                string directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                System.Diagnostics.Process.Start(directory + @"\Contrats\Demande\" + DateTime.Today.Year + ".pdf");
            }
            catch (Exception)
            {
            }
        }

        void generateDocument()
        {
            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream("Contrats/Dossier/" + p11txt.Text + "-"+DateTime.Today.Year +"-"+ DateTime.Today.Month+"-"+ DateTime.Today.Day+ ".pdf", FileMode.Create));
            doc.Open();

            string fontLoc = @"c:\windows\fonts\arial.ttf";

            PdfPTable table = new PdfPTable(1);
            table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;

            BaseFont bf = BaseFont.CreateFont(fontLoc, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            iTextSharp.text.Font f = new iTextSharp.text.Font(bf, 14);

            Phrase text1 = new Phrase("جمعية الأعمال الإجتماعية" +
                "\nلموظفي و أعوان بلدية السمارة", f);
            PdfPCell cell1 = new PdfPCell(text1);
            cell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell1.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cell1.SetLeading(20, 0);
            table.AddCell(cell1);

            BaseFont bf2 = BaseFont.CreateFont(fontLoc, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            iTextSharp.text.Font f2 = new iTextSharp.text.Font(bf, 18);
            f2.SetStyle(1);
            Phrase text2 = new Phrase("\n\nجمعية الأعمال الإجتماعية لموظفي \nو أعوان بلدية السمارة", f2);
            PdfPCell cell2 = new PdfPCell(text2);
            cell2.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cell2.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell2);
            Phrase text4 = new Phrase("\n\nملف حول الوضع الإجتماعي للمنخرط في الجمعية تحت رقم : "+p11txt.Text, f2);
            PdfPCell cell4 = new PdfPCell(text4);
            cell4.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell4.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cell4.HorizontalAlignment = Element.ALIGN_CENTER;
            table.AddCell(cell4);

            Phrase text3 = new Phrase("\nالحالة المدنية :  " + p12txt.Text +
                "\nالإسم الشخصي :  " + p13txt.Text +
                "\nالإسم العائلي :  " + p14txt.Text +
                "\nتاريخ و مكان الإزدياد" + p15txt.Text +
                "\nرقم بطاقة التعريف الوطنية :  " + p16txt.Text +
                "\nالمستوى الدراسي :  " + p17txt.Text +
                "\nالشواهد المحصل عليها :  " + p18txt.Text +
                "\nللإتصال :  " + p19txt.Text +
                "\nهاتف المنزل :  " + p110txt.Text +
                "\nهاتف العمل :  " + p111txt.Text +
                "\nالمحمول :  " + p112txt.Text +
                "\nالبريد الإلكتروني :  " + p113txt.Text +
                "\nالحالة السكنية :  " + p114txt.Text +
                "\nعنوان السكن :  " + p115txt.Text +
                "\nنوعية السكن :  " + p116txt.Text +
                "\nالحالة العائلية :  " + p117txt.Text +
                "\nعدد الأبناء :  " + p118txt.Text +
                "\nالوضعية الإدارية :  " +
                "\nالإطار الوظيفي :  " + p119txt.Text +
                "\nالسلم :  " + p120txt.Text+
                "\nالمصلحة :  " + p121txt.Text+
                "\nالمهمة :  " + p122txt.Text+
                "\nالحالة الصحية :  \n\n\n\n" + p123txt.Text, f);
            PdfPCell cell3 = new PdfPCell(text3);
            cell3.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell3.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cell3.SetLeading(19, 0);
            table.AddCell(cell3);
            PdfPTable tablewife = new PdfPTable(1);
            Phrase text6 = new Phrase("معلومات عن الزوجة", f2);
            PdfPCell cell6 = new PdfPCell(text6);
            cell6.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cell6.HorizontalAlignment = Element.ALIGN_CENTER;
            cell6.VerticalAlignment = Element.ALIGN_CENTER;
            tablewife.AddCell(cell6);

            Phrase text5 = new Phrase("\n\n\nالإسم الشخصي :  " + p21txt.Text +
                "\nالإسم العائلي :  " + p22txt.Text +
                "\nالمهنة :  " + p24txt.Text +
                "\nرقم بطاقة التعريف الوطنية :  " + p25txt.Text +
                "\nالحالة الصحية :  " + p23txt.Text, f);
            PdfPCell cell5 = new PdfPCell(text5);
            cell5.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cell5.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cell5.SetLeading(20, 0);
            tablewife.AddCell(cell5);

            Phrase text7 = new Phrase("معلومات عن الأبناء" , f2);
            PdfPCell cell7 = new PdfPCell(text7);
            cell7.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cell7.HorizontalAlignment = Element.ALIGN_CENTER;
            cell7.VerticalAlignment = Element.ALIGN_CENTER;
            tablewife.AddCell(cell7);

            iTextSharp.text.Font f3 = new iTextSharp.text.Font(bf, 14);
            f3.SetStyle(1);
            PdfPTable tablekids = new PdfPTable(5);
            #region space
            Phrase space1 = new Phrase("\n", f3);
            PdfPCell space1c = new PdfPCell(space1);
            space1c.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            space1c.Border = iTextSharp.text.Rectangle.NO_BORDER;
            for (int i = 0; i < 20; i++)
            {
                tablekids.AddCell(space1c);
            }
            #endregion
            #region childs table

            Phrase c1text0 = new Phrase("الحالة الصحية", f3);
            PdfPCell c1cell0 = new PdfPCell(c1text0);
            c1cell0.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c1cell0);
            Phrase c2text0 = new Phrase("المستوى الدراسي", f3);
            PdfPCell c2cell0 = new PdfPCell(c2text0);
            c2cell0.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c2cell0);
            Phrase c3text0 = new Phrase("رقم ب.ت.و", f3);
            PdfPCell c3cell0 = new PdfPCell(c3text0);
            c3cell0.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c3cell0);
            Phrase c4text0 = new Phrase("تاريخ الإزدياد", f3);
            PdfPCell c4cell0 = new PdfPCell(c4text0);
            c4cell0.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c4cell0);
            Phrase c5text0 = new Phrase("الإسم الشخصي", f3);
            PdfPCell c5cell0 = new PdfPCell(c5text0);
            c5cell0.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c5cell0);

            Phrase c1text1 = new Phrase(p2sit1.Text, f);
            PdfPCell c1cell1 = new PdfPCell(c1text1);
            c1cell1.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c1cell1);
            Phrase c2text1 = new Phrase(p2niv1.Text, f);
            PdfPCell c2cell1 = new PdfPCell(c2text1);
            c2cell1.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c2cell1);
            Phrase c3text1 = new Phrase(p2cin1.Text, f);
            PdfPCell c3cell1 = new PdfPCell(c3text1);
            c3cell1.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c3cell1);
            Phrase c4text1 = new Phrase(p2date1.Text, f);
            PdfPCell c4cell01 = new PdfPCell(c4text1);
            c4cell01.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c4cell01);
            Phrase c5text1 = new Phrase(p2nom1.Text, f);
            PdfPCell c5cell1 = new PdfPCell(c5text1);
            c5cell1.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c5cell1);

            Phrase c1text2 = new Phrase(p2sit2.Text, f);
            PdfPCell c1cell2 = new PdfPCell(c1text2);
            c1cell2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c1cell2);
            Phrase c2text2 = new Phrase(p2niv2.Text, f);
            PdfPCell c2cell2 = new PdfPCell(c2text2);
            c2cell2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c2cell2);
            Phrase c3text2 = new Phrase(p2cin2.Text, f);
            PdfPCell c3cell2 = new PdfPCell(c3text2);
            c3cell2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c3cell2);
            Phrase c4text2 = new Phrase(p2date2.Text, f);
            PdfPCell c4cell02 = new PdfPCell(c4text2);
            c4cell02.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c4cell02);
            Phrase c5text2 = new Phrase(p2nom2.Text, f);
            PdfPCell c5cell2 = new PdfPCell(c5text2);
            c5cell2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c5cell2);

            Phrase c1text3 = new Phrase(p2sit3.Text, f);
            PdfPCell c1cell3 = new PdfPCell(c1text3);
            c1cell3.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c1cell3);
            Phrase c2text3 = new Phrase(p2niv3.Text, f);
            PdfPCell c2cell3 = new PdfPCell(c2text3);
            c2cell3.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c2cell3);
            Phrase c3text3 = new Phrase(p2cin3.Text, f);
            PdfPCell c3cell3 = new PdfPCell(c3text3);
            c3cell3.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c3cell3);
            Phrase c4text3 = new Phrase(p2date3.Text, f);
            PdfPCell c4cell03 = new PdfPCell(c4text3);
            c4cell03.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c4cell03);
            Phrase c5text3 = new Phrase(p2nom3.Text, f);
            PdfPCell c5cell3 = new PdfPCell(c5text3);
            c5cell3.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c5cell3);


            Phrase c1text4 = new Phrase(p2sit4.Text, f);
            PdfPCell c1cell4 = new PdfPCell(c1text4);
            c1cell4.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c1cell4);
            Phrase c2text4 = new Phrase(p2niv4.Text, f);
            PdfPCell c2cell4 = new PdfPCell(c2text4);
            c2cell4.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c2cell4);
            Phrase c3text4 = new Phrase(p2cin4.Text, f);
            PdfPCell c3cell4 = new PdfPCell(c3text4);
            c3cell4.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c3cell4);
            Phrase c4text4 = new Phrase(p2date4.Text, f);
            PdfPCell c4cell04 = new PdfPCell(c4text4);
            c4cell04.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c4cell04);
            Phrase c5text4 = new Phrase(p2nom4.Text, f);
            PdfPCell c5cell4 = new PdfPCell(c5text4);
            c5cell4.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c5cell4);

            Phrase c1text5 = new Phrase(p2sit5.Text, f);
            PdfPCell c1cell5 = new PdfPCell(c1text5);
            c1cell5.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c1cell5);
            Phrase c2text5 = new Phrase(p2niv5.Text, f);
            PdfPCell c2cell5 = new PdfPCell(c2text5);
            c2cell5.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c2cell5);
            Phrase c3text5 = new Phrase(p2cin5.Text, f);
            PdfPCell c3cell5 = new PdfPCell(c3text5);
            c3cell5.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c3cell5);
            Phrase c4text5 = new Phrase(p2date5.Text, f);
            PdfPCell c4cell05 = new PdfPCell(c4text5);
            c4cell05.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c4cell05);
            Phrase c5text5 = new Phrase(p2nom5.Text, f);
            PdfPCell c5cell5 = new PdfPCell(c5text5);
            c5cell5.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c5cell5);


            Phrase c1text6 = new Phrase(p2sit6.Text, f);
            PdfPCell c1cell6 = new PdfPCell(c1text6);
            c1cell6.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c1cell6);
            Phrase c2text6 = new Phrase(p2niv6.Text, f);
            PdfPCell c2cell6 = new PdfPCell(c2text6);
            c2cell6.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c2cell6);
            Phrase c3text6 = new Phrase(p2cin6.Text, f);
            PdfPCell c3cell6 = new PdfPCell(c3text6);
            c3cell6.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c3cell6);
            Phrase c4text6 = new Phrase(p2date6.Text, f);
            PdfPCell c4cell06 = new PdfPCell(c4text6);
            c4cell06.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c4cell06);
            Phrase c5text6 = new Phrase(p2nom6.Text, f);
            PdfPCell c5cell6 = new PdfPCell(c5text6);
            c5cell6.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c5cell6);


            Phrase c1text7 = new Phrase(p2sit7.Text, f);
            PdfPCell c1cell7 = new PdfPCell(c1text7);
            c1cell7.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c1cell7);
            Phrase c2text7 = new Phrase(p2niv7.Text, f);
            PdfPCell c2cell7 = new PdfPCell(c2text7);
            c2cell7.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c2cell7);
            Phrase c3text7 = new Phrase(p2cin7.Text, f);
            PdfPCell c3cell7 = new PdfPCell(c3text7);
            c3cell7.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c3cell7);
            Phrase c4text7 = new Phrase(p2date7.Text, f);
            PdfPCell c4cell07 = new PdfPCell(c4text7);
            c4cell07.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c4cell07);
            Phrase c5text7 = new Phrase(p2nom7.Text, f);
            PdfPCell c5cell7 = new PdfPCell(c5text7);
            c5cell7.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c5cell7);


            Phrase c1text8 = new Phrase(p2sit8.Text, f);
            PdfPCell c1cell8 = new PdfPCell(c1text8);
            c1cell8.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c1cell8);
            Phrase c2text8 = new Phrase(p2niv8.Text, f);
            PdfPCell c2cell8 = new PdfPCell(c2text8);
            c2cell8.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c2cell8);
            Phrase c3text8 = new Phrase(p2cin8.Text, f);
            PdfPCell c3cell8 = new PdfPCell(c3text8);
            c3cell8.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c3cell8);
            Phrase c4text8 = new Phrase(p2date8.Text, f);
            PdfPCell c4cell08 = new PdfPCell(c4text8);
            c4cell08.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c4cell08);
            Phrase c5text8 = new Phrase(p2nom8.Text, f);
            PdfPCell c5cell8 = new PdfPCell(c5text8);
            c5cell8.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c5cell8);


            Phrase c1text9 = new Phrase(p2sit9.Text, f);
            PdfPCell c1cell9 = new PdfPCell(c1text9);
            c1cell9.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c1cell9);
            Phrase c2text9 = new Phrase(p2niv9.Text, f);
            PdfPCell c2cell9 = new PdfPCell(c2text9);
            c2cell9.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c2cell9);
            Phrase c3text9 = new Phrase(p2cin9.Text, f);
            PdfPCell c3cell9 = new PdfPCell(c3text9);
            c3cell9.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c3cell9);
            Phrase c4text9 = new Phrase(p2date9.Text, f);
            PdfPCell c4cell09 = new PdfPCell(c4text9);
            c4cell09.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c4cell09);
            Phrase c5text9 = new Phrase(p2nom9.Text, f);
            PdfPCell c5cell9 = new PdfPCell(c5text9);
            c5cell9.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c5cell9);


            Phrase c1text10 = new Phrase(p2sit10.Text, f);
            PdfPCell c1cell10 = new PdfPCell(c1text10);
            c1cell10.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c1cell10);
            Phrase c2text10 = new Phrase(p2niv10.Text, f);
            PdfPCell c2cell10 = new PdfPCell(c2text10);
            c2cell10.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c2cell10);
            Phrase c3text10 = new Phrase(p2cin10.Text, f);
            PdfPCell c3cell10 = new PdfPCell(c3text10);
            c3cell10.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c3cell10);
            Phrase c4text10 = new Phrase(p2date10.Text, f);
            PdfPCell c4cell010 = new PdfPCell(c4text10);
            c4cell010.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c4cell010);
            Phrase c5text10 = new Phrase(p2nom10.Text, f);
            PdfPCell c5cell10 = new PdfPCell(c5text10);
            c5cell10.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            tablekids.AddCell(c5cell10);

            #endregion

            PdfPTable titleTable = new PdfPTable(1);
            PdfPTable historyTable = new PdfPTable(5);

            Phrase title = new Phrase("جدول الإستفادة من خدماة الجمعية" , f2);
            PdfPCell titlec = new PdfPCell(title);
            titlec.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            titlec.HorizontalAlignment = Element.ALIGN_CENTER;
            titlec.VerticalAlignment = Element.ALIGN_CENTER;
            titleTable.AddCell(titlec);

            #region space
            Phrase space2 = new Phrase("\n", f3);
            PdfPCell space2c = new PdfPCell(space1);
            space1c.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            space1c.Border = iTextSharp.text.Rectangle.NO_BORDER;
            for (int i = 0; i < 20; i++)
            {
                historyTable.AddCell(space1c);
            }
            #endregion
            #region history table

            Phrase text10 = new Phrase("ملاحظات", f);
            PdfPCell cell10 = new PdfPCell(text10);
            cell10.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell10);
            Phrase text20 = new Phrase("المستفيد", f);
            PdfPCell cell20 = new PdfPCell(text20);
            cell20.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell20);
            Phrase text30 = new Phrase("القيمة", f);
            PdfPCell cell30 = new PdfPCell(text30);
            cell30.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell30);
            Phrase text40 = new Phrase("الخدمة", f);
            PdfPCell cell40 = new PdfPCell(text40);
            cell40.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell40);
            Phrase text50 = new Phrase("التاريخ", f);
            PdfPCell cell50 = new PdfPCell(text50);
            cell50.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell50);

            Phrase text11 = new Phrase(p3not1.Text, f);
            PdfPCell cell11 = new PdfPCell(text11);
            cell11.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell11);
            Phrase text21 = new Phrase(p3to1.Text, f);
            PdfPCell cell21 = new PdfPCell(text21);
            cell21.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell21);
            Phrase text31 = new Phrase(p3val1.Text, f);
            PdfPCell cell31 = new PdfPCell(text31);
            cell31.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell31);
            Phrase text41 = new Phrase(p3serv1.Text, f);
            PdfPCell cell41 = new PdfPCell(text41);
            cell41.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell41);
            Phrase text51 = new Phrase(p3date1.Text, f);
            PdfPCell cell51 = new PdfPCell(text51);
            cell51.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell51);

            Phrase text12 = new Phrase(p3not2.Text, f);
            PdfPCell cell12 = new PdfPCell(text12);
            cell12.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell12);
            Phrase text22 = new Phrase(p3to2.Text, f);
            PdfPCell cell22 = new PdfPCell(text22);
            cell22.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell22);
            Phrase text32 = new Phrase(p3val2.Text, f);
            PdfPCell cell32 = new PdfPCell(text32);
            cell32.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell32);
            Phrase text42 = new Phrase(p3serv2.Text, f);
            PdfPCell cell42 = new PdfPCell(text42);
            cell42.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell42);
            Phrase text52 = new Phrase(p3date2.Text, f);
            PdfPCell cell52 = new PdfPCell(text52);
            cell52.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell52);

            Phrase text13 = new Phrase(p3not3.Text, f);
            PdfPCell cell13 = new PdfPCell(text13);
            cell13.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell13);
            Phrase text23 = new Phrase(p3to3.Text, f);
            PdfPCell cell23 = new PdfPCell(text23);
            cell23.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell23);
            Phrase text33 = new Phrase(p3val3.Text, f);
            PdfPCell cell33 = new PdfPCell(text33);
            cell33.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell33);
            Phrase text43 = new Phrase(p3serv3.Text, f);
            PdfPCell cell43 = new PdfPCell(text43);
            cell43.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell43);
            Phrase text53 = new Phrase(p3date3.Text, f);
            PdfPCell cell53 = new PdfPCell(text53);
            cell53.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell53);

            Phrase text14 = new Phrase(p3not4.Text, f);
            PdfPCell cell14 = new PdfPCell(text14);
            cell14.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell14);
            Phrase text24 = new Phrase(p3to4.Text, f);
            PdfPCell cell24 = new PdfPCell(text24);
            cell24.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell24);
            Phrase text34 = new Phrase(p3val4.Text, f);
            PdfPCell cell34 = new PdfPCell(text34);
            cell34.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell34);
            Phrase text44 = new Phrase(p3serv4.Text, f);
            PdfPCell cell44 = new PdfPCell(text44);
            cell44.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell44);
            Phrase text54 = new Phrase(p3date4.Text, f);
            PdfPCell cell54 = new PdfPCell(text54);
            cell54.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell54);

            Phrase text15 = new Phrase(p3not5.Text, f);
            PdfPCell cell15 = new PdfPCell(text15);
            cell15.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell15);
            Phrase text25 = new Phrase(p3to5.Text, f);
            PdfPCell cell25 = new PdfPCell(text25);
            cell25.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell25);
            Phrase text35 = new Phrase(p3val5.Text, f);
            PdfPCell cell35 = new PdfPCell(text35);
            cell35.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell35);
            Phrase text45 = new Phrase(p3serv5.Text, f);
            PdfPCell cell45 = new PdfPCell(text45);
            cell45.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell45);
            Phrase text55 = new Phrase(p3date5.Text, f);
            PdfPCell cell55 = new PdfPCell(text55);
            cell55.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell55);

            Phrase text16 = new Phrase(p3not6.Text, f);
            PdfPCell cell16 = new PdfPCell(text16);
            cell16.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell16);
            Phrase text26 = new Phrase(p3to6.Text, f);
            PdfPCell cell26 = new PdfPCell(text26);
            cell26.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell26);
            Phrase text36 = new Phrase(p3val6.Text, f);
            PdfPCell cell36 = new PdfPCell(text36);
            cell36.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell36);
            Phrase text46 = new Phrase(p3serv6.Text, f);
            PdfPCell cell46 = new PdfPCell(text46);
            cell46.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell46);
            Phrase text56 = new Phrase(p3date6.Text, f);
            PdfPCell cell56 = new PdfPCell(text56);
            cell56.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell56);

            Phrase text17 = new Phrase(p3not7.Text, f);
            PdfPCell cell17 = new PdfPCell(text17);
            cell17.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell17);
            Phrase text27 = new Phrase(p3to7.Text, f);
            PdfPCell cell27 = new PdfPCell(text27);
            cell27.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell27);
            Phrase text37 = new Phrase(p3val7.Text, f);
            PdfPCell cell37 = new PdfPCell(text37);
            cell37.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell37);
            Phrase text47 = new Phrase(p3serv7.Text, f);
            PdfPCell cell47 = new PdfPCell(text47);
            cell47.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell47);
            Phrase text57 = new Phrase(p3date7.Text, f);
            PdfPCell cell57 = new PdfPCell(text57);
            cell57.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell57);

            Phrase text18 = new Phrase(p3not8.Text, f);
            PdfPCell cell18 = new PdfPCell(text18);
            cell18.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell18);
            Phrase text28 = new Phrase(p3to8.Text, f);
            PdfPCell cell28 = new PdfPCell(text28);
            cell28.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell28);
            Phrase text38 = new Phrase(p3val8.Text, f);
            PdfPCell cell38 = new PdfPCell(text38);
            cell38.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell38);
            Phrase text48 = new Phrase(p3serv8.Text, f);
            PdfPCell cell48 = new PdfPCell(text48);
            cell48.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell48);
            Phrase text58 = new Phrase(p3date8.Text, f);
            PdfPCell cell58 = new PdfPCell(text58);
            cell58.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell58);

            Phrase text19 = new Phrase(p3not9.Text, f);
            PdfPCell cell19 = new PdfPCell(text19);
            cell19.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell19);
            Phrase text29 = new Phrase(p3to9.Text, f);
            PdfPCell cell29 = new PdfPCell(text29);
            cell29.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell29);
            Phrase text39 = new Phrase(p3val9.Text, f);
            PdfPCell cell39 = new PdfPCell(text39);
            cell39.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell39);
            Phrase text49 = new Phrase(p3serv9.Text, f);
            PdfPCell cell49 = new PdfPCell(text49);
            cell49.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell49);
            Phrase text59 = new Phrase(p3date9.Text, f);
            PdfPCell cell59 = new PdfPCell(text59);
            cell59.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell59);

            Phrase text110 = new Phrase(p3not10.Text, f);
            PdfPCell cell110 = new PdfPCell(text110);
            cell110.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell110);
            Phrase text210 = new Phrase(p3to10.Text, f);
            PdfPCell cell210 = new PdfPCell(text210);
            cell210.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell210);
            Phrase text310 = new Phrase(p3val10.Text, f);
            PdfPCell cell310 = new PdfPCell(text310);
            cell310.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell310);
            Phrase text410 = new Phrase(p3serv10.Text, f);
            PdfPCell cell410 = new PdfPCell(text410);
            cell410.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell410);
            Phrase text510 = new Phrase(p3date10.Text, f);
            PdfPCell cell510 = new PdfPCell(text510);
            cell510.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell510);

            Phrase text111 = new Phrase(p3not11.Text, f);
            PdfPCell cell111 = new PdfPCell(text111);
            cell111.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell111);
            Phrase text211 = new Phrase(p3to11.Text, f);
            PdfPCell cell211 = new PdfPCell(text211);
            cell211.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell211);
            Phrase text311 = new Phrase(p3val11.Text, f);
            PdfPCell cell311 = new PdfPCell(text311);
            cell311.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell311);
            Phrase text411 = new Phrase(p3serv11.Text, f);
            PdfPCell cell411 = new PdfPCell(text411);
            cell411.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell411);
            Phrase text511 = new Phrase(p3date11.Text, f);
            PdfPCell cell511 = new PdfPCell(text511);
            cell511.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell511);

            Phrase text112 = new Phrase(p3not12.Text, f);
            PdfPCell cell112 = new PdfPCell(text112);
            cell112.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell112);
            Phrase text212 = new Phrase(p3to12.Text, f);
            PdfPCell cell212 = new PdfPCell(text212);
            cell212.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell212);
            Phrase text312 = new Phrase(p3val12.Text, f);
            PdfPCell cell312 = new PdfPCell(text312);
            cell312.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell312);
            Phrase text412 = new Phrase(p3serv12.Text, f);
            PdfPCell cell412 = new PdfPCell(text412);
            cell412.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell412);
            Phrase text512 = new Phrase(p3date12.Text, f);
            PdfPCell cell512 = new PdfPCell(text512);
            cell512.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell512);

            Phrase text113 = new Phrase(p3not13.Text, f);
            PdfPCell cell113 = new PdfPCell(text113);
            cell113.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell113);
            Phrase text213 = new Phrase(p3to13.Text, f);
            PdfPCell cell213 = new PdfPCell(text213);
            cell213.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell213);
            Phrase text313 = new Phrase(p3val13.Text, f);
            PdfPCell cell313 = new PdfPCell(text313);
            cell313.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell313);
            Phrase text413 = new Phrase(p3serv13.Text, f);
            PdfPCell cell413 = new PdfPCell(text413);
            cell413.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell413);
            Phrase text513 = new Phrase(p3date13.Text, f);
            PdfPCell cell513 = new PdfPCell(text513);
            cell513.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell513);

            Phrase text114 = new Phrase(p3not14.Text, f);
            PdfPCell cell114 = new PdfPCell(text114);
            cell114.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell114);
            Phrase text214 = new Phrase(p3to14.Text, f);
            PdfPCell cell214 = new PdfPCell(text214);
            cell214.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell214);
            Phrase text314 = new Phrase(p3val14.Text, f);
            PdfPCell cell314 = new PdfPCell(text314);
            cell314.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell314);
            Phrase text414 = new Phrase(p3serv14.Text, f);
            PdfPCell cell414 = new PdfPCell(text414);
            cell414.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell414);
            Phrase text514 = new Phrase(p3date14.Text, f);
            PdfPCell cell514 = new PdfPCell(text514);
            cell514.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            historyTable.AddCell(cell514);



            #endregion


            PdfPTable title2table = new PdfPTable(1);
            PdfPTable title2table2 = new PdfPTable(3);
            PdfPTable title2table3 = new PdfPTable(3);
            PdfPTable regestrTable = new PdfPTable(9);
            PdfPTable regestrTable2 = new PdfPTable(9);


            Phrase tit = new Phrase("تتبع تحصيل الإنخراط السنوي و المساهمات الشهرية", f2);
            PdfPCell titcel = new PdfPCell(tit);
            titcel.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            titcel.HorizontalAlignment = Element.ALIGN_CENTER;
            titcel.VerticalAlignment = Element.ALIGN_CENTER;
            title2table.AddCell(titcel);

            #region space
            PdfPCell space3c = new PdfPCell(space1);
            space1c.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            space1c.Border = iTextSharp.text.Rectangle.NO_BORDER;
            for (int i = 0; i < 4; i++)
            {
                title2table.AddCell(space1c);
            }
            Phrase tit1 = new Phrase("انخرط سنة : "+year2.Text, f);
            PdfPCell titcel1 = new PdfPCell(tit1);
            titcel1.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            titcel1.HorizontalAlignment = Element.ALIGN_CENTER;
            titcel1.VerticalAlignment = Element.ALIGN_CENTER;
            title2table2.AddCell(titcel1);
            Phrase tit2 = new Phrase("", f);
            PdfPCell titcel2 = new PdfPCell(tit2);
            titcel2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            titcel2.Border = iTextSharp.text.Rectangle.NO_BORDER;
            title2table2.AddCell(titcel2);
            Phrase tit3 = new Phrase("انخرط سنة : " + year1.Text, f);
            PdfPCell titcel3 = new PdfPCell(tit3);
            titcel3.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            titcel3.HorizontalAlignment = Element.ALIGN_CENTER;
            titcel3.VerticalAlignment = Element.ALIGN_CENTER;
            title2table2.AddCell(titcel3);


            #region dates tables

            
            #endregion
            addFirstRow(regestrTable,f);
            addSecondRow(regestrTable,f);
            addThirdRow(regestrTable,f);

            #endregion

            #region space
            PdfPCell space4c = new PdfPCell(space1);
            space1c.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            space1c.Border = iTextSharp.text.Rectangle.NO_BORDER;
            for (int i = 0; i < 27; i++)
            {
                regestrTable.AddCell(space1c);
            }
            #endregion

            Phrase tit4 = new Phrase("انخرط سنة : " + year4.Text, f);
            PdfPCell titcel4 = new PdfPCell(tit4);
            titcel4.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            titcel4.HorizontalAlignment = Element.ALIGN_CENTER;
            titcel4.VerticalAlignment = Element.ALIGN_CENTER;
            title2table3.AddCell(titcel4);
            Phrase tit5 = new Phrase("", f);
            PdfPCell titcel5 = new PdfPCell(tit5);
            titcel5.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            titcel5.Border = iTextSharp.text.Rectangle.NO_BORDER;
            title2table3.AddCell(titcel5);
            Phrase tit6 = new Phrase("انخرط سنة : " + year3.Text, f);
            PdfPCell titcel6 = new PdfPCell(tit6);
            titcel6.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            titcel6.HorizontalAlignment = Element.ALIGN_CENTER;
            titcel6.VerticalAlignment = Element.ALIGN_CENTER;
            title2table3.AddCell(titcel6);

            #region dates tables

            addFirstRow2(regestrTable2, f);
            addSecondRow2(regestrTable2, f);
            addThirdRow2(regestrTable2, f);

            #endregion

            PdfPTable lasttable = new PdfPTable(1);

            Phrase tttt3 = new Phrase("\n\nملاحظة عامة  :  " + note.Text+
                "\nقام بتعبئة هذا الملف"+"                                              تم عرض هذا الملف خلال إجتماع"+" \n"+
                "السيد(ة) : "+sir.Text + "                                 المكتب المنعقد"+
                "\nبتاريخ : "+ date.Text +"                                                 بتاريخ : "+date2.Text+
                "\n\nالتوقيع                                                                   التوقيع", f);
            PdfPCell cellll = new PdfPCell(tttt3);
            cellll.Border = iTextSharp.text.Rectangle.NO_BORDER;
            cellll.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cellll.SetLeading(19, 0);
            lasttable.AddCell(cellll);

            doc.Add(table);
            doc.Add(tablewife);
            doc.Add(tablekids);
            doc.NewPage();
            doc.Add(titleTable);
            doc.Add(historyTable);
            doc.NewPage();
            doc.Add(title2table);
            doc.Add(title2table2);
            doc.Add(regestrTable);
            doc.Add(title2table3);
            doc.Add(regestrTable2);
            doc.Add(lasttable);
            doc.Close();




            try
            {
                string directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
                System.Diagnostics.Process.Start(directory + @"\Contrats\Dossier\" + p11txt.Text + "-" + DateTime.Today.Year + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day + ".pdf");
            }
            catch (Exception)
            {
            }
        }


        void addFirstRow(PdfPTable regestrTable, iTextSharp.text.Font f)
        {
            Phrase t1 = new Phrase("ابريل", f);
            PdfPCell c1 = new PdfPCell(t1);
            c1.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c1.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c1);
            Phrase t2 = new Phrase("مارس", f);
            PdfPCell c2 = new PdfPCell(t2);
            c2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c2.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c2);
            Phrase t3 = new Phrase("فبراير", f);
            PdfPCell c3 = new PdfPCell(t3);
            c3.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c3.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c3);
            Phrase t4 = new Phrase("يناير", f);
            PdfPCell c4 = new PdfPCell(t4);
            c4.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c4.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c4);
            Phrase t5 = new Phrase("", f);
            PdfPCell c5 = new PdfPCell(t5);
            c5.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c5.Border = iTextSharp.text.Rectangle.NO_BORDER;
            regestrTable.AddCell(c5);
            Phrase t6 = new Phrase("ابريل", f);
            PdfPCell c6 = new PdfPCell(t6);
            c6.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c6.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c6);
            Phrase t7 = new Phrase("مارس", f);
            PdfPCell c7 = new PdfPCell(t7);
            c7.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c7.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c7);
            Phrase t8 = new Phrase("فبراير", f);
            PdfPCell c8 = new PdfPCell(t8);
            c8.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c8.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c8);
            Phrase t9 = new Phrase("يناير", f);
            PdfPCell c9 = new PdfPCell(t9);
            c9.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c9.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c9);

            Phrase t10 = new Phrase(month8.Text, f);
            PdfPCell c10 = new PdfPCell(t10);
            c10.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c10.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c10);
            Phrase t11 = new Phrase(month7.Text, f);
            PdfPCell c11 = new PdfPCell(t11);
            c11.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c11.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c11);
            Phrase t12 = new Phrase(month6.Text, f);
            PdfPCell c12 = new PdfPCell(t12);
            c12.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c12.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c12);
            Phrase t13 = new Phrase(month5.Text, f);
            PdfPCell c13 = new PdfPCell(t13);
            c13.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c13.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c13);
            regestrTable.AddCell(c5);
            Phrase t14 = new Phrase(month4.Text, f);
            PdfPCell c14 = new PdfPCell(t14);
            c14.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c14.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c14);
            Phrase t15 = new Phrase(month3.Text, f);
            PdfPCell c15 = new PdfPCell(t15);
            c15.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c15.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c15);
            Phrase t16 = new Phrase(month2.Text, f);
            PdfPCell c16 = new PdfPCell(t16);
            c16.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c16.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c16);
            Phrase t17 = new Phrase(month1.Text, f);
            PdfPCell c17 = new PdfPCell(t17);
            c17.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c17.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c17);
        }
        void addSecondRow(PdfPTable regestrTable, iTextSharp.text.Font f)
        {
            Phrase tt1 = new Phrase("غشت", f);
            PdfPCell cc1 = new PdfPCell(tt1);
            cc1.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc1.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc1);
            Phrase tt2 = new Phrase("يوليوز", f);
            PdfPCell cc2 = new PdfPCell(tt2);
            cc2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc2.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc2);
            Phrase tt3 = new Phrase("يونيو", f);
            PdfPCell cc3 = new PdfPCell(tt3);
            cc3.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc3.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc3);
            Phrase tt4 = new Phrase("مايو", f);
            PdfPCell cc4 = new PdfPCell(tt4);
            cc4.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc4.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc4);
            Phrase tt5 = new Phrase("", f);
            PdfPCell cc5 = new PdfPCell(tt5);
            cc5.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc5.Border = iTextSharp.text.Rectangle.NO_BORDER;
            regestrTable.AddCell(cc5);
            Phrase tt6 = new Phrase("غشت", f);
            PdfPCell cc6 = new PdfPCell(tt6);
            cc6.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc6.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc6);
            Phrase tt7 = new Phrase("يوليوز", f);
            PdfPCell cc7 = new PdfPCell(tt7);
            cc7.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc7.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc7);
            Phrase tt8 = new Phrase("يونيو", f);
            PdfPCell cc8 = new PdfPCell(tt8);
            cc8.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc8.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc8);
            Phrase tt9 = new Phrase("مايو", f);
            PdfPCell cc9 = new PdfPCell(tt9);
            cc9.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc9.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc9);

            Phrase tt10 = new Phrase(month16.Text, f);
            PdfPCell cc10 = new PdfPCell(tt10);
            cc10.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc10.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc10);
            Phrase tt11 = new Phrase(month15.Text, f);
            PdfPCell cc11 = new PdfPCell(tt11);
            cc11.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc11.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc11);
            Phrase tt12 = new Phrase(month14.Text, f);
            PdfPCell cc12 = new PdfPCell(tt12);
            cc12.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc12.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc12);
            Phrase tt13 = new Phrase(month13.Text, f);
            PdfPCell cc13 = new PdfPCell(tt13);
            cc13.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc13.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc13);
            regestrTable.AddCell(cc5);
            Phrase tt14 = new Phrase(month12.Text, f);
            PdfPCell cc14 = new PdfPCell(tt14);
            cc14.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc14.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc14);
            Phrase tt15 = new Phrase(month11.Text, f);
            PdfPCell cc15 = new PdfPCell(tt15);
            cc15.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc15.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc15);
            Phrase tt16 = new Phrase(month10.Text, f);
            PdfPCell cc16 = new PdfPCell(tt16);
            cc16.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc16.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc16);
            Phrase tt17 = new Phrase(month9.Text, f);
            PdfPCell cc17 = new PdfPCell(tt17);
            cc17.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc17.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc17);
            
        }
        void addThirdRow(PdfPTable regestrTable, iTextSharp.text.Font f)
        {
            Phrase tt1 = new Phrase("دجنبر", f);
            PdfPCell cc1 = new PdfPCell(tt1);
            cc1.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc1.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc1);
            Phrase tt2 = new Phrase("نونبر", f);
            PdfPCell cc2 = new PdfPCell(tt2);
            cc2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc2.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc2);
            Phrase tt3 = new Phrase("أكتوبر", f);
            PdfPCell cc3 = new PdfPCell(tt3);
            cc3.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc3.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc3);
            Phrase tt4 = new Phrase("شتنبر", f);
            PdfPCell cc4 = new PdfPCell(tt4);
            cc4.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc4.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc4);
            Phrase tt5 = new Phrase("", f);
            PdfPCell cc5 = new PdfPCell(tt5);
            cc5.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc5.Border = iTextSharp.text.Rectangle.NO_BORDER;
            regestrTable.AddCell(cc5);
            Phrase tt6 = new Phrase("دجنبر", f);
            PdfPCell cc6 = new PdfPCell(tt6);
            cc6.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc6.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc6);
            Phrase tt7 = new Phrase("نونبر", f);
            PdfPCell cc7 = new PdfPCell(tt7);
            cc7.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc7.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc7);
            Phrase tt8 = new Phrase("أكتوبر", f);
            PdfPCell cc8 = new PdfPCell(tt8);
            cc8.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc8.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc8);
            Phrase tt9 = new Phrase("شتنبر", f);
            PdfPCell cc9 = new PdfPCell(tt9);
            cc9.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc9.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc9);

            Phrase tt10 = new Phrase(month24.Text, f);
            PdfPCell cc10 = new PdfPCell(tt10);
            cc10.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc10.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc10);
            Phrase tt11 = new Phrase(month23.Text, f);
            PdfPCell cc11 = new PdfPCell(tt11);
            cc11.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc11.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc11);
            Phrase tt12 = new Phrase(month22.Text, f);
            PdfPCell cc12 = new PdfPCell(tt12);
            cc12.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc12.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc12);
            Phrase tt13 = new Phrase(month21.Text, f);
            PdfPCell cc13 = new PdfPCell(tt13);
            cc13.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc13.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc13);
            regestrTable.AddCell(cc5);
            Phrase tt14 = new Phrase(month20.Text, f);
            PdfPCell cc14 = new PdfPCell(tt14);
            cc14.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc14.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc14);
            Phrase tt15 = new Phrase(month19.Text, f);
            PdfPCell cc15 = new PdfPCell(tt15);
            cc15.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc15.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc15);
            Phrase tt16 = new Phrase(month18.Text, f);
            PdfPCell cc16 = new PdfPCell(tt16);
            cc16.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc16.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc16);
            Phrase tt17 = new Phrase(month17.Text, f);
            PdfPCell cc17 = new PdfPCell(tt17);
            cc17.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc17.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc17);
        }

        void addFirstRow2(PdfPTable regestrTable, iTextSharp.text.Font f)
        {
            Phrase t1 = new Phrase("ابريل", f);
            PdfPCell c1 = new PdfPCell(t1);
            c1.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c1.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c1);
            Phrase t2 = new Phrase("مارس", f);
            PdfPCell c2 = new PdfPCell(t2);
            c2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c2.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c2);
            Phrase t3 = new Phrase("فبراير", f);
            PdfPCell c3 = new PdfPCell(t3);
            c3.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c3.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c3);
            Phrase t4 = new Phrase("يناير", f);
            PdfPCell c4 = new PdfPCell(t4);
            c4.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c4.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c4);
            Phrase t5 = new Phrase("", f);
            PdfPCell c5 = new PdfPCell(t5);
            c5.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c5.Border = iTextSharp.text.Rectangle.NO_BORDER;
            regestrTable.AddCell(c5);
            Phrase t6 = new Phrase("ابريل", f);
            PdfPCell c6 = new PdfPCell(t6);
            c6.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c6.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c6);
            Phrase t7 = new Phrase("مارس", f);
            PdfPCell c7 = new PdfPCell(t7);
            c7.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c7.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c7);
            Phrase t8 = new Phrase("فبراير", f);
            PdfPCell c8 = new PdfPCell(t8);
            c8.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c8.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c8);
            Phrase t9 = new Phrase("يناير", f);
            PdfPCell c9 = new PdfPCell(t9);
            c9.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c9.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c9);

            Phrase t10 = new Phrase(mon8.Text, f);
            PdfPCell c10 = new PdfPCell(t10);
            c10.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c10.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c10);
            Phrase t11 = new Phrase(mon7.Text, f);
            PdfPCell c11 = new PdfPCell(t11);
            c11.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c11.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c11);
            Phrase t12 = new Phrase(mon6.Text, f);
            PdfPCell c12 = new PdfPCell(t12);
            c12.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c12.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c12);
            Phrase t13 = new Phrase(mon5.Text, f);
            PdfPCell c13 = new PdfPCell(t13);
            c13.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c13.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c13);
            regestrTable.AddCell(c5);
            Phrase t14 = new Phrase(mon4.Text, f);
            PdfPCell c14 = new PdfPCell(t14);
            c14.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c14.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c14);
            Phrase t15 = new Phrase(mon3.Text, f);
            PdfPCell c15 = new PdfPCell(t15);
            c15.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c15.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c15);
            Phrase t16 = new Phrase(mon2.Text, f);
            PdfPCell c16 = new PdfPCell(t16);
            c16.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c16.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c16);
            Phrase t17 = new Phrase(mon1.Text, f);
            PdfPCell c17 = new PdfPCell(t17);
            c17.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            c17.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(c17);
        }
        void addSecondRow2(PdfPTable regestrTable, iTextSharp.text.Font f)
        {
            Phrase tt1 = new Phrase("غشت", f);
            PdfPCell cc1 = new PdfPCell(tt1);
            cc1.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc1.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc1);
            Phrase tt2 = new Phrase("يوليوز", f);
            PdfPCell cc2 = new PdfPCell(tt2);
            cc2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc2.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc2);
            Phrase tt3 = new Phrase("يونيو", f);
            PdfPCell cc3 = new PdfPCell(tt3);
            cc3.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc3.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc3);
            Phrase tt4 = new Phrase("مايو", f);
            PdfPCell cc4 = new PdfPCell(tt4);
            cc4.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc4.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc4);
            Phrase tt5 = new Phrase("", f);
            PdfPCell cc5 = new PdfPCell(tt5);
            cc5.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc5.Border = iTextSharp.text.Rectangle.NO_BORDER;
            regestrTable.AddCell(cc5);
            Phrase tt6 = new Phrase("غشت", f);
            PdfPCell cc6 = new PdfPCell(tt6);
            cc6.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc6.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc6);
            Phrase tt7 = new Phrase("يوليوز", f);
            PdfPCell cc7 = new PdfPCell(tt7);
            cc7.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc7.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc7);
            Phrase tt8 = new Phrase("يونيو", f);
            PdfPCell cc8 = new PdfPCell(tt8);
            cc8.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc8.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc8);
            Phrase tt9 = new Phrase("مايو", f);
            PdfPCell cc9 = new PdfPCell(tt9);
            cc9.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc9.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc9);

            Phrase tt10 = new Phrase(mon16.Text, f);
            PdfPCell cc10 = new PdfPCell(tt10);
            cc10.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc10.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc10);
            Phrase tt11 = new Phrase(mon15.Text, f);
            PdfPCell cc11 = new PdfPCell(tt11);
            cc11.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc11.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc11);
            Phrase tt12 = new Phrase(mon14.Text, f);
            PdfPCell cc12 = new PdfPCell(tt12);
            cc12.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc12.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc12);
            Phrase tt13 = new Phrase(mon13.Text, f);
            PdfPCell cc13 = new PdfPCell(tt13);
            cc13.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc13.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc13);
            regestrTable.AddCell(cc5);
            Phrase tt14 = new Phrase(mon12.Text, f);
            PdfPCell cc14 = new PdfPCell(tt14);
            cc14.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc14.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc14);
            Phrase tt15 = new Phrase(mon11.Text, f);
            PdfPCell cc15 = new PdfPCell(tt15);
            cc15.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc15.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc15);
            Phrase tt16 = new Phrase(mon10.Text, f);
            PdfPCell cc16 = new PdfPCell(tt16);
            cc16.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc16.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc16);
            Phrase tt17 = new Phrase(mon9.Text, f);
            PdfPCell cc17 = new PdfPCell(tt17);
            cc17.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc17.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc17);

        }
        void addThirdRow2(PdfPTable regestrTable, iTextSharp.text.Font f)
        {
            Phrase tt1 = new Phrase("دجنبر", f);
            PdfPCell cc1 = new PdfPCell(tt1);
            cc1.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc1.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc1);
            Phrase tt2 = new Phrase("نونبر", f);
            PdfPCell cc2 = new PdfPCell(tt2);
            cc2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc2.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc2);
            Phrase tt3 = new Phrase("أكتوبر", f);
            PdfPCell cc3 = new PdfPCell(tt3);
            cc3.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc3.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc3);
            Phrase tt4 = new Phrase("شتنبر", f);
            PdfPCell cc4 = new PdfPCell(tt4);
            cc4.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc4.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc4);
            Phrase tt5 = new Phrase("", f);
            PdfPCell cc5 = new PdfPCell(tt5);
            cc5.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc5.Border = iTextSharp.text.Rectangle.NO_BORDER;
            regestrTable.AddCell(cc5);
            Phrase tt6 = new Phrase("دجنبر", f);
            PdfPCell cc6 = new PdfPCell(tt6);
            cc6.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc6.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc6);
            Phrase tt7 = new Phrase("نونبر", f);
            PdfPCell cc7 = new PdfPCell(tt7);
            cc7.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc7.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc7);
            Phrase tt8 = new Phrase("أكتوبر", f);
            PdfPCell cc8 = new PdfPCell(tt8);
            cc8.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc8.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc8);
            Phrase tt9 = new Phrase("شتنبر", f);
            PdfPCell cc9 = new PdfPCell(tt9);
            cc9.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc9.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc9);

            Phrase tt10 = new Phrase(mon24.Text, f);
            PdfPCell cc10 = new PdfPCell(tt10);
            cc10.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc10.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc10);
            Phrase tt11 = new Phrase(mon23.Text, f);
            PdfPCell cc11 = new PdfPCell(tt11);
            cc11.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc11.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc11);
            Phrase tt12 = new Phrase(mon22.Text, f);
            PdfPCell cc12 = new PdfPCell(tt12);
            cc12.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc12.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc12);
            Phrase tt13 = new Phrase(mon21.Text, f);
            PdfPCell cc13 = new PdfPCell(tt13);
            cc13.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc13.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc13);
            regestrTable.AddCell(cc5);
            Phrase tt14 = new Phrase(mon20.Text, f);
            PdfPCell cc14 = new PdfPCell(tt14);
            cc14.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc14.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc14);
            Phrase tt15 = new Phrase(mon19.Text, f);
            PdfPCell cc15 = new PdfPCell(tt15);
            cc15.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc15.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc15);
            Phrase tt16 = new Phrase(mon18.Text, f);
            PdfPCell cc16 = new PdfPCell(tt16);
            cc16.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc16.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc16);
            Phrase tt17 = new Phrase(mon17.Text, f);
            PdfPCell cc17 = new PdfPCell(tt17);
            cc17.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            cc17.VerticalAlignment = Element.ALIGN_CENTER;
            regestrTable.AddCell(cc17);
        }

        private void bunifuFlatButton1_Click_1(object sender, EventArgs e)
        {
            generateRegesterContract();
        }

        private void validercut_Click(object sender, EventArgs e)
        {
            generateCutContract();
        }

        private void valider1_Click(object sender, EventArgs e)
        {
            generateDocument();
        }

        private void retour5btn_Click(object sender, EventArgs e)
        {
            etap4panel.BringToFront();
            page1panel.BackColor = Color.Gray;
            page2panel.BackColor = Color.Gray;
            page3panel.BackColor = Color.Gray;
            page4panel.BackColor = Color.FromArgb(255, 54, 52, 103);
            page5panel.BackColor = Color.Gray;
        }

        private void suivant4_Click(object sender, EventArgs e)
        {
            etap5panel.BringToFront();
            page1panel.BackColor = Color.Gray;
            page2panel.BackColor = Color.Gray;
            page3panel.BackColor = Color.Gray;
            page4panel.BackColor = Color.Gray;
            page5panel.BackColor =  Color.FromArgb(255, 54, 52, 103);
        }

        private void reset_Click(object sender, EventArgs e)
        {
            
        }
    }
}
