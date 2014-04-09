using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
//NPOI
using NPOI.HSSF.UserModel;
using NPOI.SS.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Formula.Functions;


namespace Report_Excel2007_Module
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //TEST_Sheet();
            Read_Write_Module("D:\\TEST.xls");
        }

        private void Read_Write_Module(string fileUrl)
        {
            FileStream fs = new FileStream(fileUrl, FileMode.Open);
            HSSFWorkbook hssfWorkBook_1 = new HSSFWorkbook(fs);
            HSSFSheet sheet = (NPOI.HSSF.UserModel.HSSFSheet)hssfWorkBook_1.GetSheet("Sheet3");

            IRow row= sheet.CreateRow(11);//12
            ICell cell = row.CreateCell(0);//A
            cell.SetCellValue("A");

            cell = row.CreateCell(1);//B12
            cell.SetCellValue((double)9);

            cell = row.CreateCell(2);//C12
            cell.SetCellValue((double)9);

            cell = row.CreateCell(3);//D12
            cell.SetCellValue((double)9);

            FileStream fs1 = new FileStream("D:\\TEST9.xls", FileMode.Create);
            hssfWorkBook_1.Write(fs1);
            fs1.Close();
            fs1.Dispose();
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
            sheet.CreateFreezePane(2,1);

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
            FileStream fs = new FileStream("D:\\TEST.xls",FileMode.Create);
            hssfWorkBook_1.Write(fs);

            fs.Close();
            fs.Dispose();

        }
    }
}