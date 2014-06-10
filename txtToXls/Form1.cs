using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace txtToXls
{
    public partial class Form1 : Form
    {
        private List<string> lstFileName;
        private List<string> lstFileUrl;

        public Form1()
        {
            InitializeComponent();

            lstFileName = new List<string>();
            lstFileUrl = new List<string>();
        }

        private void TEST_Sheet()
        {
            //Create Sheet
            HSSFWorkbook hssfWorkBook_1 = new HSSFWorkbook();
            HSSFSheet sheet = (NPOI.HSSF.UserModel.HSSFSheet)hssfWorkBook_1.CreateSheet("TEST_sheet1");

            //Create Row
            IRow rowFirst = sheet.CreateRow(0);//row1
            IRow rowSecond = sheet.CreateRow(1);//row2

            //Create Cell
            ICell cell_column = rowFirst.CreateCell(0);//A1
            cell_column.SetCellValue("TEST");

            cell_column = rowFirst.CreateCell(2);//C1
            HSSFHyperlink link = new HSSFHyperlink(HyperlinkType.URL);
            link.Address = ("http://www.google.com.tw");
            ICellStyle cellStyle = hssfWorkBook_1.CreateCellStyle();
            IFont cellFont = hssfWorkBook_1.CreateFont();
            cellFont.Underline = 1;
            cellFont.Color = NPOI.HSSF.Util.HSSFColor.BLUE_GREY.index;
            cellStyle.SetFont(cellFont);

            cell_column.SetCellValue("Google");
            cell_column.Hyperlink = link;
            cell_column.CellStyle = cellStyle;

            //Merge Cell            
            cell_column = rowFirst.CreateCell(8);
            cell_column.SetCellValue("123456789__TEST");
            sheet.AddMergedRegion(new CellRangeAddress(0, 0, 8, 9));

            //Fliter
            sheet.SetAutoFilter(CellRangeAddress.ValueOf("A1:B1"));

            //Freeze
            sheet.CreateFreezePane(2, 1);

            //公式(A2 + B2) = C2
            cell_column = rowSecond.CreateCell(0);//A2
            cell_column.SetCellValue(1);
            cell_column = rowSecond.CreateCell(1);//B2
            cell_column.SetCellValue(3);
            cell_column = rowSecond.CreateCell(2);//C2
            cell_column.CellFormula = "A2+B2";

            //邊框


            //Lock Sheet
            //sheet.ProtectSheet("marvin");

            //Save File (D:\TEST.xls)
            FileStream fs = new FileStream("D:\\TEST.xls", FileMode.Create);
            hssfWorkBook_1.Write(fs);

            fs.Close();
            fs.Dispose();
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            HSSFWorkbook hssfWorkBook_1 = new HSSFWorkbook();

            for (int i = 0; i < lstFileName.Count; i++)
            {
                HSSFSheet sheet = (NPOI.HSSF.UserModel.HSSFSheet)hssfWorkBook_1.CreateSheet(lstFileName[i]);
                IRow row; ICell cell;
                int Row_Count = 0;
                bool isOnly = false;
                
                StreamReader sr = new StreamReader(lstFileUrl[i]);
                while (!sr.EndOfStream)
                {
                    string Line = sr.ReadLine().Trim().Replace("Test Name","TestName").Replace(" ohm", "").Replace(" mA", "");
                    string[] Line_Split = Line.Split(" ".ToCharArray());

                    int Cell_Count = 0;
                    row = sheet.CreateRow(Row_Count);
                    foreach (string item in Line_Split)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            cell = row.CreateCell(Cell_Count);
                            cell.SetCellValue(item.Replace("TestName", "Test Name"));
                            Cell_Count++;
                        }
                    }
                                        
                    if (Line.Contains("Result") && isOnly==false) 
                    {
                        sheet.SetAutoFilter(CellRangeAddress.ValueOf(string.Format(@"A{0}:K{0}", Row_Count + 1)));//Fliter
                        sheet.CreateFreezePane(Cell_Count, Row_Count + 1);//Freeze
                        isOnly = true;
                    }

                    Row_Count++;
                }//end while

                sr.Close();                
            }//end for

            if (lstFileName.Count > 0)
            {
                //Save File (D:\TEST.xls)
                FileStream fs = new FileStream(string.Format(@"C:\\PINCHECK\\TEST_Lisa_{0}.xls", DateTime.Now.ToString("D")), FileMode.Create);
                hssfWorkBook_1.Write(fs);

                fs.Close();
                fs.Dispose();

                MessageBox.Show("OK");
            }
            else
            {
                MessageBox.Show("請選擇檔案");
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            //<add name="OVEN" connectionString="User Id=oven_sys;Password=oven!!;Data Source=AUTO"/>
            //<add name="OVEN" connectionString="User Id=spare_part;Password=spare!!part;Data Source=AUTO"/>

            string conn = "User Id=oven_sys;Password=oven!!;Data Source=AUTO";
            App_Code.AdoDbConn ado = new App_Code.AdoDbConn(App_Code.AdoDbConn.AdoDbType.Oracle,conn);
            List<string> arrStr = new List<string>();
            StreamReader sr = new StreamReader(lstFileUrl[0]);
            while (!sr.EndOfStream)
            {
                string Line = sr.ReadLine().Trim();
                if (!Line.Contains("Date"))
                {
                    string[] Line_Split = Line.Split("\t".ToCharArray());

                    #region insert Txn
                    arrStr.Add(string.Format(@"insert into oven_assy_log (oven_assy_logid,machine_id,oven_assy_logkindid,time,data,batch_NO,target,control_limit,isoverspec)
                                           values(oven_assy_log_sequence.nextval,'PO-002','1',to_date('{0}','mm/dd/yy hh24:mi:ss'),'{1}','{2}','{3}','{4}','{5}')", Line_Split[0] + " " + Line_Split[1],
                                                                                                                Line_Split[7],
                                                                                                                "942640A106",
                                                                                                                "5",
                                                                                                                "0.5",
                                                                                                                "N"));//Pressure
                    arrStr.Add(string.Format(@"insert into oven_assy_log (oven_assy_logid,machine_id,oven_assy_logkindid,time,data,batch_NO,target,control_limit,isoverspec)
                                           values(oven_assy_log_sequence.nextval,'PO-002','2',to_date('{0}','mm/dd/yy hh24:mi:ss'),'{1}','{2}','{3}','{4}','{5}')", Line_Split[0] + " " + Line_Split[1],
                                                                                                                Line_Split[4],
                                                                                                                "942640A106",
                                                                                                                "122",
                                                                                                                "5",
                                                                                                                "N"));//Temperature(Ch1)
                    arrStr.Add(string.Format(@"insert into oven_assy_log (oven_assy_logid,machine_id,oven_assy_logkindid,time,data,batch_NO,target,control_limit,isoverspec)
                                           values(oven_assy_log_sequence.nextval,'PO-002','3',to_date('{0}','mm/dd/yy hh24:mi:ss'),'{1}','{2}','{3}','{4}','{5}')", Line_Split[0] + " " + Line_Split[1],
                                                                                                                Line_Split[5],
                                                                                                                "942640A106",
                                                                                                                "122",
                                                                                                                "5",
                                                                                                                "N"));//Temperature(Ch2)
                    arrStr.Add(string.Format(@"insert into oven_assy_log (oven_assy_logid,machine_id,oven_assy_logkindid,time,data,batch_NO,target,control_limit,isoverspec)
                                           values(oven_assy_log_sequence.nextval,'PO-002','4',to_date('{0}','mm/dd/yy hh24:mi:ss'),'{1}','{2}','{3}','{4}','{5}')", Line_Split[0] + " " + Line_Split[1],
                                                                                                                 Line_Split[6],
                                                                                                                "942640A106",
                                                                                                                "122",
                                                                                                                "5",
                                                                                                                "N"));//Temperature(Ch3)
                    #endregion
                }
            }//end while

            sr.Close();

            //commit
            string reStr = ado.SQL_transaction(arrStr, conn);
            if (reStr.Equals("SUCCESS")) { MessageBox.Show("OK"); }
            else { MessageBox.Show("fail"); }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = file.FileName;
                lstFileUrl.Add(file.FileName);
                lstFileName.Add(file.SafeFileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = file.FileName;
                lstFileUrl.Add(file.FileName);
                lstFileName.Add(file.SafeFileName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = file.FileName;
                lstFileUrl.Add(file.FileName);
                lstFileName.Add(file.SafeFileName);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                textBox6.Text = file.FileName;
                lstFileUrl.Add(file.FileName);
                lstFileName.Add(file.SafeFileName);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                textBox5.Text = file.FileName;
                lstFileUrl.Add(file.FileName);
                lstFileName.Add(file.SafeFileName);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                textBox4.Text = file.FileName;
                lstFileUrl.Add(file.FileName);
                lstFileName.Add(file.SafeFileName);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                textBox9.Text = file.FileName;
                lstFileUrl.Add(file.FileName);
                lstFileName.Add(file.SafeFileName);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                textBox8.Text = file.FileName;
                lstFileUrl.Add(file.FileName);
                lstFileName.Add(file.SafeFileName);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                textBox7.Text = file.FileName;
                lstFileUrl.Add(file.FileName);
                lstFileName.Add(file.SafeFileName);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                textBox10.Text = file.FileName;
                lstFileUrl.Add(file.FileName);
                lstFileName.Add(file.SafeFileName);
            }
        }

        

    }
}
