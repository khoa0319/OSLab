using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatrixMultiplication
{
    public partial class Form1 : Form
    {
        double[,] A;
        double[,] B;
        double[,] C;
        int N;
        //danh sách trạng thái thi hành của các thread con
        int[] stateLst = new int[20];
        //danh sách thời gian thi hành của các thread con
        TimeSpan[] dateLst = new TimeSpan[20];
        //sư ưu tiên cho chương trình
        ProcessPriorityClass myPrio = ProcessPriorityClass.Normal;
        Process MyProc;
        //các quyền ưu tiên dành cho các Thread
        ThreadPriority[] tPrio = new ThreadPriority[] 
        {
            ThreadPriority.Lowest,
            ThreadPriority.BelowNormal,
            ThreadPriority.Normal,
            ThreadPriority.AboveNormal,
            ThreadPriority.Highest
        };
        public Form1()
        {
            InitializeComponent();
            lbKetqua.Items.Clear();
            N = 10000;
            A = new double[N, N];
            B = new double[N, N];
            C = new double[N, N];
            int h, c;
            for (h = 0; h < N; h++)
            {
                for (c = 0; c < N; c++)
                {
                    A[h, c] = B[h, c] = c;
                }
            }
        }
        public void TinhTich(object obj)
        {
            DateTime t1 = DateTime.Now;
            Params p = obj as Params;
            int h, c, k;
            for (h = p.sr; h < p.er; h++)
            {
                for (c = 0; c < N; c++)
                {
                    double s = 0;
                    for (k = 0; k < N; k++)
                    {
                        s = s + A[h, k] * B[k, c];
                    }
                    C[h, c] = s;
                   
                }
            }
            //ghi nhận đã hoàn thành
            stateLst[p.id] = 1;
            //ghi nhận thời gian
            dateLst[p.id] = DateTime.Now.Subtract(t1);
        }

        private void btnCham_Click(object sender, EventArgs e)
        {
            myPrio = ProcessPriorityClass.BelowNormal;
        }

        private void btnNhanh_Click(object sender, EventArgs e)
        {
            myPrio = ProcessPriorityClass.RealTime;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            MyProc = Process.GetCurrentProcess();
            MyProc.PriorityClass = myPrio;
            int cnt = int.Parse(txtThreads.Text);
            int i;
            //ghi nhận thời gian bắt đầu tính
            DateTime t1 = DateTime.Now;
            if (cnt == 1)
            {
                TinhTich(new Params(null, 0, N, 0));
            }
            else //giải thuật song song gồm cnt - 1 thread con và 1 thread chính có sẵn
            {
                Thread t;
                for (i = 0; i < cnt - 1; i++)
                {
                    //lặp tạo và chạy từng thread con
                    stateLst[i] = 0; // ghi nhận thread i chưa chạy xong
                    t = new Thread(new ParameterizedThreadStart(TinhTich))
                    {
                        //thiết lập quyền ưu tiên cho thread i
                        Priority = tPrio[i % 5]
                    };
                    //hiển thị độ ưu tiên của Thread i
                    lbKetqua.Items.Add(string.Format("Thread {0:d} có độ ưu tiên {1:d}", i, t.Priority.ToString()));
                    //kích hoạt thread i chạy và truyền các tham số cho nó
                    t.Start(new Params(t, i * N / cnt, (i + 1) * N / cnt, i));

                }
            }
            //thread cha sẽ tính N/cnt hàng cuối của ma trận tích
            TinhTich(new Params(null, (cnt - 1) * N, N, cnt - 1));
            //chờ thread con hoàn thành
            for (i = 0; i < cnt - 1; i++)
            {
                while (stateLst[i] == 0) ; //chờ
            }
            //ghi nhận thời điểm kết thúc tính tích
            DateTime t2 = DateTime.Now;
            TimeSpan diff;
            //hiển thị độ ưu tiên hiện hành của chương trình
            lbKetqua.Items.Add("ứng dụng đã chạy với quyền" + myPrio.ToString());
            //hiển thị thời gian tính của từng thread con
            for (i = 0; i < cnt - 1; i++)
            {
                diff = dateLst[i];
                lbKetqua.Items.Add(string.Format("Thread {0:d} chạy tốn {1:d2} phút {2:d2} giây" +
                    "{3:d3} ms", i, diff.Minutes, diff.Seconds, diff.Milliseconds));

            }
            diff = t2.Subtract(t1);
            // hiển thị thời gian tổng cộng để tính tích
            lbKetqua.Items.Add(string.Format("{0:d} Thread ==> thời gian chạy tốn {1:d2} phút {2:d2} giây" +
                    "{3:d3} ms", cnt, diff.Minutes, diff.Seconds, diff.Milliseconds));
        }
    }
    public class Params
    {
        public Thread t;
        public int sr; //start row
        public int er; //end row
        public int id; // thread index
        public Params(Thread t, int s, int e, int i)
        {
            this.t = t;
            sr = s;
            er = e;
            id = i;
        }
    }
}
