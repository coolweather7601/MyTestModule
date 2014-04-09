using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace StreamHandle_Module
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Select File
        private void button1_Click(object sender, EventArgs e)
        {
            //設定Filter，過濾檔案 
            openFileDialog1.Filter = "Picture(*.jpg)|*.jpg|Picture2(*.png)|*.png|All files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        //Save Folder
        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = folderBrowserDialog1.SelectedPath;             
            }
        }

        //Picture Stream Handle
        private void button3_Click(object sender, EventArgs e)
        {

            FileStream fs = new FileStream(textBox1.Text, FileMode.Open);
            byte[] picData = new byte[fs.Length];
            fs.Read(picData, 0, picData.Length);
            fs.Close();

            using (MemoryStream ms = new MemoryStream(picData))
            {
                Image img = Image.FromStream(ms);
                img.Save(string.Format("{0}\\Test.jpg",textBox2.Text), ImageFormat.Jpeg);
            }
        }

    }
}
