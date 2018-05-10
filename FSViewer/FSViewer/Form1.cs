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

namespace FSViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            cbDrives.Items.Clear();
            foreach (var item in allDrives)
            {
                cbDrives.Items.Add(item.Name);
            }
            //chọn mặc định ổ đầu tiên
            cbDrives.SelectedIndex = 0;
            //thiết lập DataGridView có 3 cột
            //tên file, kịch thước, ngày h cập nhật lần cuối
            dgvFiles.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvFiles.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvFiles.GridColor = Color.Black;
            dgvFiles.ColumnCount = 3;
            dgvFiles.Columns[0].Name = "Tên file";
            dgvFiles.Columns[1].Name = "Kích thước";
            dgvFiles.Columns[2].Name = "Cập nhật lần cuối lúc";
            dgvFiles.Font = new Font("Tahoma", 10f);
            dgvFiles.Name = "dgvFiles";
            dgvFiles.AutoResizeColumns();
            dgvFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFiles.MultiSelect = true;
            dgvFiles.AllowUserToResizeColumns = true;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            int cx = ClientSize.Width;
            int cy = ClientSize.Height;
            int top = tvFolders.Top;
            int left = tvFolders.Left;
            tvFolders.Size = new Size(cx / 3 - left, cy - 8 - top); //TreeView chiếm 1/3 chiều ngang
            dgvFiles.Location = new Point(cx / 3, top);
            dgvFiles.Size = new Size(2 * cx / 3 - 8, cy - 8 - top); //DataGridView chiếm 2/3 còn lại
        }

        private void cbDrives_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lấy chuỗi miêu tả ổ ₫ĩa ₫ược chọn
            String sdrv = cbDrives.SelectedItem.ToString();
            //xóa nội dung hiện hành của TreeView chứa cây thư mục
            tvFolders.Nodes.Clear();
            //tạo ₫ối tượng TreeNode miêu tả thư mục gốc
            TreeNode FdNode = new TreeNode();
            FdNode.Text = sdrv; //tên thư mục gốc
                                //add ₫ối tượng TreeNode miêu tả thư mục gốc vào cây
            tvFolders.Nodes.Add(FdNode);
            //triển khai nội dung thư mục gốc ₫ể nó chứa các thư mục con hiện hành
            Populate(FdNode, sdrv);
            //mở rộng nội dung nút miêu tả thư mục gốc trong cây
            FdNode.Expand();
            //hiển thị lại cây thư mục theo nội dung vừa thiết lập
            tvFolders.Refresh();
        }
        //hàm triển khai nội dung của nút thư mục xác ₫ịnh
        private void Populate(TreeNode FdNode, String sdir)
        {
            //lấy danh sách thư mục con của thư mục sdir
            string[] sdlistw = Directory.GetDirectories(sdir);
            TreeNode ChildNode;
            //xóa tất cả nội dung cũ của nút thư mục sdir
            FdNode.Nodes.Clear();
            //duyệt và thêm từng nút thư mục con vào cho nút thư mục sdir
            foreach (string subdir in sdlistw)
            {
                string[] lstStr = subdir.Split('\\');
                ChildNode = new TreeNode();
                ChildNode.Text = lstStr[lstStr.Length - 1]; //nút con
                ChildNode.Nodes.Add(new TreeNode());
                FdNode.Nodes.Add(ChildNode);
            }
        }

        private void tvFolders_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.X >= e.Node.Bounds.Left)
            {
                //chọn folder ==> hiển thị các file bên trong nó lên DataGridView
                DisplayFiles(getDir(e.Node));
            }
            else //mở rộng/thu gọn nội dung nút thư mục tương ứng trong cây
                tvFolders_NodeMouseDoubleClick(sender, e);
        }
        private void DisplayFiles(String sdir)
        {
            //xóa danh sách file của DataGridView
            dgvFiles.Rows.Clear();
            //lấy danh sách các file trong thư mục sdir
            string[] flist = Directory.GetFiles(sdir);
            String[] sbuf = new String[3];
            String[] buf;
            //lặp lấy thông tin từng file trong thư mục và hiển thị nó lên DataGridView
            foreach (string fname in flist)
            {
                FileInfo fi = new FileInfo(fname);
                sbuf[1] = fi.Length.ToString();
                sbuf[2] = fi.LastWriteTime.ToString();
                buf = fname.Split('\\');
                sbuf[0] = buf[buf.Length - 1];
                dgvFiles.Rows.Add(sbuf);
            }
        }

        private void tvFolders_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode node = e.Node;
            //chuyển trạng thái mở rộng/thu gọn của nút hiện hành
            if (node.NextNode != null && node.IsExpanded == false) node.Collapse();
            else
            {
                String sdir = getDir(node);
                Populate(node, sdir);
            }
        }
        private string getDir(TreeNode node)
        {
            String kq = node.Text;
            while (node.Parent != null)
            {//nút này có cha
                node = node.Parent;
                if (node.Parent != null) //nút này có cha
                    kq = node.Text + "\\" + kq;
                else kq = node.Text + kq;
            }
            return kq;
        }
    }
}
