using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using NPOI.HSSF;
using NPOI;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;
using System.Windows.Forms;
using NPOI.XSSF.UserModel;
using NPOI.SS.Formula.Eval;

namespace test
{
    public class ExcelUtil
    {
        //创建xls文件
        public void WriteExcel(string str)
        {
            //创建工作薄
            HSSFWorkbook wk = new HSSFWorkbook();
            //创建一个名称为mySheet的表
            ISheet tb = wk.CreateSheet("报表");
            //创建一行，此行为第二行
            IRow row = tb.CreateRow(1);
            for (int i = 0; i < 20; i++)
            {
                ICell cell = row.CreateCell(i);  //在第二行中创建单元格
                cell.SetCellValue(i);//循环往第二行的单元格中添加数据
            }
            using (FileStream fs = File.OpenWrite(str)) //打开一个xls文件，如果没有则自行创建，如果存在myxls.xls文件则在创建是不要打开该文件！
            {
                wk.Write(fs);   //向打开的这个xls文件中写入mySheet表并保存。
                MessageBox.Show("导出成功！");
            }
        }


        public object[,] ReadExcel(string str,string sheetname)
        {
            StringBuilder sbr = new StringBuilder();
            using (FileStream fs = File.OpenRead(str))   //打开myxls.xls文件
            {
                IWorkbook wk = null;
                if (str.ToLower().EndsWith("xls")||str.ToLower().EndsWith("xla")||str.ToLower().EndsWith("xlm"))
                    wk = new HSSFWorkbook(fs);
                else
                    if (str.ToLower().EndsWith("xlsx")||str.ToLower().EndsWith("xlax")||str.ToLower().EndsWith("xlsm"))
                        wk = new XSSFWorkbook(fs);
                    else
                    {
                        MessageBox.Show("格式不正确");
                        return null;
                    }
                ISheet sheet = wk.GetSheet(sheetname);//.GetSheetAt(0);   //读取当前表数据
                if (sheet == null)
                {
                    MessageBox.Show("sheet名称："+sheetname+"不存在");
                    return null;
                }
                IFormulaEvaluator formEval = wk.GetCreationHelper().CreateFormulaEvaluator();
                object[,] arryItem = new object[sheet.LastRowNum, 35];
                for (int j = 1; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                {
                    IRow row = sheet.GetRow(j);  //读取当前行数
                    if (row != null)
                    {
                        //arryItem[1,j] = new object[3];
                        // sbr.Append("-------------------------------------\r\n"); //读取行与行之间的提示界限
                        for (int k = 0; k <= row.LastCellNum; k++)  //LastCellNum 是当前行的总列数
                        {
                            ICell cell = row.GetCell(k);  //当前表格
                            if (cell != null)
                            {
                                string value = null;
                                    switch (cell.CellType) {  
                                    case CellType.Boolean: 
                                            value = GetStringValue(cell.BooleanCellValue);
                                        break;  
                                    case CellType.Numeric:  
                                            value = GetStringValue(cell.NumericCellValue); 
                                            if (HSSFDateUtil.IsCellDateFormatted(cell))
                                                value=cell.DateCellValue.ToString("yyyy-MM-dd");
                                        break;  
                                    case CellType.String:  
                                            value = GetStringValue(cell.StringCellValue); 
                                        break;  
                                    case CellType.Blank:  
                                        break;  
                                    case CellType.Error:
                                        value = GetStringValue(cell.ErrorCellValue); 
                                        break;
                                    // CELL_TYPE_FORMULA will never occur  
                                    case CellType.Formula:
                                        switch (row.GetCell(k).CachedFormulaResultType)
                                        {
                                            case CellType.String:
                                                string strFORMULA = row.GetCell(k).StringCellValue;
                                                if (strFORMULA != null && strFORMULA.Length > 0)
                                                {
                                                    value = strFORMULA.ToString();
                                                }
                                                else
                                                {
                                                    value = null;
                                                }
                                                break;
                                            case CellType.Numeric:
                                                value = Convert.ToString(row.GetCell(k).NumericCellValue);
                                                break;
                                            case CellType.Boolean:
                                                value = Convert.ToString(row.GetCell(k).BooleanCellValue);
                                                break;
                                            case CellType.Error:
                                                value = ErrorEval.GetText(row.GetCell(k).ErrorCellValue);
                                                break;
                                            default:
                                                value = "";
                                                break;
                                        }
                                        break;

                                        //value = GetStringValue(formEval.Evaluate(cell).StringValue);
                                        //                              if (value==null || value.Length == 0)
                                        //                              value = GetStringValue(formEval.Evaluate(cell).NumberValue);
                                        //                                  //formEval.EvaluateFormulaCell(cell);
                                        //                                  //value = cell.CellFormula.ToString();
                                        //                                  //value = value;
                                        //                              break;

                                }
                                arryItem[j - 1, k] = value;
                                //sbr.Append(cell.ToString());   //获取表格中的数据并转换为字符串类型
                            }
                        }
                    }
                }
                return arryItem;
            }
            return null;
            //sbr.ToString();
            //using (StreamWriter wr = new StreamWriter(new FileStream(@"c:/myText.txt", FileMode.Append)))  //把读取xls文件的数据写入myText.txt文件中
            //{
            //    wr.Write(sbr.ToString());
            //    wr.Flush();
            //}

        }

        private string GetStringValue(object obj)
        {
            if (obj == null)
                return null;
            return obj.ToString();
        }

    }

}
