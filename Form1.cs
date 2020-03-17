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
using System.Text.RegularExpressions;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            BarCode.BarCodeEvent += new BarCodeHook.BarCodeDelegate(BarCode_BarCodeEvent);
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


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string data = File.ReadAllText("c:\\4d1.xml");
                string result = GetRequestContext("http://127.0.0.1:8008/service/XChangeServlet?account=2&receiver=1", data, "POST", Encoding.UTF8);
                MessageBox.Show(result);
            }
            catch (Exception e1)
            {
                MessageBox.Show( e1.Message);
            }
            //DataTable dt1 = new DataTable();
            //dt1.Columns.Add("aa");
            //dt1.Columns.Add("bb");
            //DataRow dr2 = dt1.NewRow();
            //dr2["aa"] = "xx";
            //dr2["bb"] = "yy";
            //dt1.Rows.Add(dr2);

            //DataTable dt3 = new DataTable();
            //dt3.Columns.Add("bb");
            //dt3.Columns.Add("aa");
            //DataRow dr3 = dt3.NewRow();
            //dr3["aa"] = "yy";
            //dr3["bb"] = "xx";
            //dt3.Rows.Add(dr3);


            //dt1.Rows.Add(dt3.Rows[0].ItemArray);
            //dgv1.DataSource = dt1;
          



        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.J)
                MessageBox.Show("aaaa");

        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                

            //初始化
           //int port = int.Parse(textBox2.Text);

           int icdev = IC.auto_init(0, 9600);

            if (icdev < 0)
            {

                MessageBox.Show("端口初始化失败,请检查接口线是否连接正确。", "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int md = IC.setsc_md(icdev, 1); //设备密码格式

            int i = IC.dv_beep(icdev, 10);  //发出蜂鸣声

            unsafe
            {

                Int16 status = 0;

                Int16 result = 0;

                result = IC.get_status(icdev, &status);

                if (result != 0)
                {

                    MessageBox.Show("设备当前状态错误！");

                    int d1 = IC.ic_exit(icdev);   //关闭设备

                    return;

                }

                if (status != 1)
                {

                    MessageBox.Show("请插入ＩＣ卡");

                    int d2 = IC.ic_exit(icdev);   //关闭设备

                    return;

                }

            }

            unsafe
            {

                char str = 'a';

                int read = -1;

                for (int j = 0; j < 6; j++)
                {

                    read = IC.srd_4442(icdev, 33 + j, 1, &str);

                    textBox1.Text = textBox1.Text + Convert.ToString(str);

                }

                if (read == 0)

                    MessageBox.Show("ＩＣ卡中数据读取成功！");

            }

            int d = IC.ic_exit(icdev);  //关闭设备
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }

        }

        private delegate void ShowInfoDelegate1(BarCodeHook.BarCodes barCode);
        private void HandleBarCode(BarCodeHook.BarCodes barCode)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new ShowInfoDelegate1(HandleBarCode), new object[] { barCode });
            }
            else
            { 
                if (barCode.IsValid)
                {
                   // textBox1.Focus();
                   // MessageBox.Show(barCode.BarCode,"xx");
                    this.textBox1.Text = Regex.Replace(barCode.BarCode, @"\s", "");//这里使用了一个正则，为了去掉里面的空格，制表符等无效字符
                }
            }
        }
 
        //钩子回调方法
        void BarCode_BarCodeEvent(BarCodeHook.BarCodes barCode)
        {
            HandleBarCode(barCode);
        }

        
        BarCodeHook BarCode = new BarCodeHook();

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BarCode.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BarCode.Stop();//钩子卸载
        }
    }
}
