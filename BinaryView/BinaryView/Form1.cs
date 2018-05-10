using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinaryView
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            int i, j;
            int byt;
            string slLine, srLine;
            string sline;
            OpenFileDialog dlg = new OpenFileDialog();
            DialogResult ret = dlg.ShowDialog();
            if (ret != DialogResult.OK)
            {
                return;
            }
            FileStream stream = new FileStream(dlg.FileName, FileMode.Open);
            long flen = stream.Length;
            //3. xuất từng nhóm 16 byte của file ra thành 1 dòng
            j = 0;
            slLine = srLine = "";
            lbOutput.Items.Clear();
            for (i = 0; i < flen; i++)
            {
                byt = stream.ReadByte(); //₫ọc 1 byte ở vị trí hiện hành trên file
                sline = String.Format("{0:X2} ", byt);
                slLine = slLine + sline;
                if (byt < 32) sline = ".";
                else sline = Char.ToString((char)byt);
                srLine = srLine + sline;
                if (++j == 16)
                { //₫ủ 16 byte trên 1 dòng ==> hiển thị dòng văn bản kết quả
                    lbOutput.Items.Add(slLine + " " + srLine);
                    j = 0;
                    slLine = srLine = "";
                }
            }
            if (j == 0) return;
            //4. xử lý dòng cuối cùng
            while (j++ < 16) slLine = slLine + " ";
            lbOutput.Items.Add(slLine + " " + srLine);
            //5. ₫óng file lại
            stream.Close();
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            int cx = this.Size.Width;
            int cy = this.Size.Height;
            //tính lại kích thước của ListBox
            cx = cx - 8 - lbOutput.Location.X * 2;
            cy = cy - 8 - 25 - lbOutput.Location.Y;
            //thay ₫ổi kích thước của ListBox theo kích thước Form
            lbOutput.Size = new Size(cx, cy);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lbOutput.Items.Clear();
            lbOutput.Refresh();
        }
    }
}
