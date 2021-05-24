using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

//using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml;
using System.Collections;

namespace test
{
    public partial class ImportUI : Form
    {
        public ImportUI()
        {
            InitializeComponent();
        }

        public string GetRequestContext(string url, string data, string method, Encoding encoding)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "text/xml";//application/x-www-form-urlencoded
            request.Accept = "text/xml";//text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8
            request.Headers["Pragma"] = "no-cache";
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.152 Safari/537.36";
            request.Method = method;
            if (method == "POST")
            {
                var sm = request.GetRequestStream();//exception
                var byts = Encoding.UTF8.GetBytes(data);
                sm.Write(byts, 0, byts.Length);
                sm.Close();
            }
            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader read = new StreamReader(response.GetResponseStream(), encoding))
                {
                    return read.ReadToEnd();
                }
            }
        }

        OpenFileDialog ofd = null;
        private void btPost_Click(object sender, EventArgs e)
        {
            try
            {
                String billType = cb1.Text;
                if (!"材料出库单".Equals(billType) && !"库存盘点单".Equals(billType))
                    return;
                String contents = tbIP.Text + "\r\n" + billType + "\r\n" + tbCorp.Text + "\r\n" + tbSender.Text + "\r\n" + tbAccount.Text;

                File.WriteAllText("param.txt", contents);
                if (ofd == null)
                {
                    ofd = new OpenFileDialog();
                    ofd.Filter = "Excel文件|*.xls;|所有文件|*.*";
                    ofd.ValidateNames = true;
                    ofd.CheckPathExists = true;
                    ofd.CheckFileExists = true;
                }
                DialogResult dr = ofd.ShowDialog();
                if (dr == System.Windows.Forms.DialogResult.OK)
                {
                    string strFileName = ofd.FileName;
                    DateTime l1 = DateTime.Now;
                    ExcelUtil eu = new ExcelUtil();
                    object[,] arry = eu.ReadExcel(strFileName, "Sheet1");
                    if (arry == null)
                        return;
                    String data="";
                    if(billType.Equals("材料出库单"))
                        data = GetXML(tbSender.Text,arry);// File.ReadAllText("c:\\4d1.xml");
                    if (billType.Equals("库存盘点单"))
                        data = GetXMLForPDD(tbSender.Text, arry);

                    string xml = GetRequestContext("http://" + tbIP.Text + "/service/XChangeServlet?account="+tbAccount.Text+"&receiver=" + tbCorp.Text, data, "POST", Encoding.UTF8);
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);
                    XmlNodeList list = doc.SelectNodes("//resultdescription");
                    string result = "";
                    int num = 1;
                    foreach (XmlNode xn1 in list)
                    {
                        result += num+". "+ xn1.InnerText+"\r\n";
                        num += 1;
                    }
                    MessageBox.Show(result);
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }

        private string GetCellValue(Object[,] arry, int row, int col)
        {
            object o = arry.GetValue(row, col);
            if (o == null || o is DBNull || o.Equals(""))
            {
                return "";
            }
            return o.ToString();
        }

        private String GetXMLForPDD(string sender, object[,] arry)
        {
            String head = "<?xml version=\"1.0\" encoding=\"UTF-8\"?> <ufinterface billtype=\"4R\" filename=\"4R.xml\" isDebug=\"N\" isexchange=\"Y\" proc=\"add\" receiver=\"1\" replace=\"Y\" roottag=\"ic_bill\" sender=\"" + sender + "\" subbilltype=\"\">";
            int iRowCnt = arry.GetLength(0);
            int iColCnt = arry.GetLength(1);
            int i = 1;

            string bills = "";

            ArrayList al = new ArrayList();
            for (int j = 0; j < iRowCnt; )
            {
                string billnum = GetCellValue(arry, j, 0);
                int j0 = j;
                int billcnt = 1;
                while (true)
                {
                    j++;
                    if (j == iRowCnt)
                    {
                        break;
                    }
                    string billnum2 = GetCellValue(arry, j, 0);
                    if (!billnum.Equals(billnum2))
                        break;
                }
                billcnt = j - j0;
                al.Add(billcnt);
            }

            int rownum = 0;

            for (int k = 0; k < al.Count; k++)
            {
                bills += @"<ic_bill>        
                    <ic_bill_head>
                        <cbilltypecode>4R</cbilltypecode>            
                        <dbilldate>" + GetCellValue(arry, k, 1) + @"</dbilldate>
                                
                        <pk_corp>" + tbCorp.Text + @"</pk_corp>            
                        <vbillcode></vbillcode>            
                        <coperatorid>" + GetCellValue(arry, k, 2) + @"</coperatorid>            
                        <coperatoridnow>" + GetCellValue(arry, k, 2) + @"</coperatoridnow>            
                        <coutwarehouseid>" + GetCellValue(arry, k, 3) + @"</coutwarehouseid>
                        <vnote>" + GetCellValue(arry, k, 4) + @"</vnote>                              
                        <fbillflag>2</fbillflag>
                        <icheckmode>4</icheckmode>
<iprintcount>0</iprintcount>
                    </ic_bill_head>
                    <body>";
                for (int m = 0; m < (int)al[k]; m++)
                {
                    bills = bills + @"<entry>
                            <crowno>" + 10 * (m + 1) + @"</crowno>
                            <cinventoryid>" + GetCellValue(arry, rownum + m, 9) + @"</cinventoryid>                
                            <cinvbasid>" + GetCellValue(arry, rownum + m, 9) + @"</cinvbasid>                          
			                <dbizdate>" + GetCellValue(arry, rownum + m, 1) + @"</dbizdate>
			                <naccountnum>" + GetCellValue(arry, rownum + m, 11) + @"</naccountnum>
			                <nchecknum>" + GetCellValue(arry, rownum + m, 12) + @"</nchecknum>
			                <nadjustnum>" + GetCellValue(arry, rownum + m, 13) + @"</nadjustnum>

                            </entry>";
                }
                rownum = rownum + (int)al[k];//vuserdef1烤漆面积  vuserdef19 工序单号 vnotebody 备注  cprojectid 项目号
                bills = bills + @"</body></ic_bill>";
            }
            return head + bills + " </ufinterface>";
        }



        private String GetXML(string sender,object[,] arry)
        {
            String head = "<?xml version=\"1.0\" encoding=\"UTF-8\"?> <ufinterface billtype=\"IC\" filename=\"4d.xml\" isDebug=\"N\" isexchange=\"Y\" proc=\"add\" receiver=\"1\" replace=\"Y\" roottag=\"ic_bill\" sender=\"" + sender + "\" subtype=\"run\">";
            int iRowCnt = arry.GetLength(0);
            int iColCnt = arry.GetLength(1);
            int i=1;

            string bills = @"<ic_bill >        
                    <ic_bill_head>
                        <cbilltypecode>4D</cbilltypecode>            
                        <dbilldate>2019-06-24</dbilldate>            
                        <pk_corp>1</pk_corp>            
                        <vbillcode></vbillcode>            
                        <coperatorid>lbh</coperatorid>            
                        <coperatoridnow>lbh</coperatoridnow>            
                        <cwarehouseid>1</cwarehouseid>            
                        <fbillflag>2</fbillflag>
                    </ic_bill_head>
                    <body>
                        <entry>
                            <crowno>10</crowno>
                            <cinventoryid>01</cinventoryid>                
                            <cinvbasid>01</cinvbasid>                
                            <castunitid>3</castunitid>
			                <dbizdate>2019-06-24</dbizdate>
			                <noutnum>2</noutnum>
			                <nshouldoutnum>1.3</nshouldoutnum>
			                <noutassistnum>2</noutassistnum>
                        </entry>
                    </body>
             </ic_bill>";
            bills = "";

            ArrayList al = new ArrayList();
            for (int j = 0;j < iRowCnt;)
            {
                string billnum = GetCellValue(arry, j, 0);
                int j0 = j;
                int billcnt = 1;
                while (true)
                {
                    j++;
                    if (j== iRowCnt)
                    {
                        break;
                    }
                    string billnum2 =GetCellValue(arry, j, 0);
                    if (!billnum.Equals(billnum2))
                        break;                    
                }
                billcnt = j - j0;
                al.Add(billcnt);
            }

            int rownum = 0;

            for (int k = 0; k < al.Count; k++)
            {
                //vnote 工单号
                //vuserdef1 备注
                //vuserdef17 项目号 <pk_defdoc17>" + GetCellValue(arry, k, 6) + @"</pk_defdoc17>
                //cdispatcherid
                bills += @"<ic_bill>        
                    <ic_bill_head>
                        <cbilltypecode>4D</cbilltypecode>            
                        <dbilldate>" + GetCellValue(arry, k, 1) + @"</dbilldate>            
                        <pk_corp>" + tbCorp.Text + @"</pk_corp>            
                        <vbillcode></vbillcode>            
                        <coperatorid>" + GetCellValue(arry, k, 2) + @"</coperatorid>            
                        <coperatoridnow>" + GetCellValue(arry, k, 2) + @"</coperatoridnow>            
                        <cwarehouseid>" + GetCellValue(arry, k, 3) + @"</cwarehouseid>
                        
                        <cdispatcherid>" + GetCellValue(arry, k, 5) + @"</cdispatcherid>            
                        <vuserdef17>" + GetCellValue(arry, k, 6) + @"</vuserdef17>
                        <vnote>" + GetCellValue(arry, k, 7) + @"</vnote>            
                        <vuserdef1>" + GetCellValue(arry, k, 8) + @"</vuserdef1>            
                        <fbillflag>2</fbillflag>
                    </ic_bill_head>
                    <body>";
                for (int m = 0; m < (int)al[k]; m++)
                {
                    bills = bills + @"<entry>
                            <crowno>" + 10 * (m+1) + @"</crowno>
                            <cinventoryid>" + GetCellValue(arry, rownum + m, 9) + @"</cinventoryid>                
                            <cinvbasid>" + GetCellValue(arry, rownum + m, 9) + @"</cinvbasid>                
                            <castunitid>" + GetCellValue(arry, rownum + m, 10) + @"</castunitid>
			                <dbizdate>" + GetCellValue(arry, rownum + m, 1) + @"</dbizdate>
			                <noutnum>0</noutnum>
			                <nshouldoutnum>" + GetCellValue(arry, rownum + m, 11) + @"</nshouldoutnum>
			                <vuserdef1>" + GetCellValue(arry, rownum + m, 13) + @"</vuserdef1>
			                <vuserdef19>" + GetCellValue(arry, rownum + m, 14) + @"</vuserdef19>
			                <cprojectid>" + GetCellValue(arry, k, 6) + @"</cprojectid>
			                <vnotebody>" + GetCellValue(arry, rownum + m, 15) + @"</vnotebody>
			                <noutassistnum>0</noutassistnum>
                            </entry>";
                }
                rownum = rownum + (int)al[k];//vuserdef1烤漆面积  vuserdef19 工序单号 vnotebody 备注  cprojectid 项目号
                bills = bills + @"</body></ic_bill>";
            }
            return head + bills + " </ufinterface>";
        }

        private void ImportUI_Load(object sender, EventArgs e)
        {
            try
            {
                string[] param = File.ReadAllLines("param.txt");
                tbIP.Text = param[0];
                cb1.Text = param[1];
                tbCorp.Text = param[2];
                tbSender.Text = param[3];
                tbAccount.Text = param[4];
            }
            catch (Exception)
            {
            }

        }

    }
}
