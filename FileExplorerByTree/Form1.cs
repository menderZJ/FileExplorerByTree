using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace FileExplorerByTree
{
    public partial class Form1 : Form
    {
        private string selePath;

        public Form1()
        {
            InitializeComponent();
        }

        private void 选择文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            FillTree ft = new FillTree();
            var selePath = folderBrowserDialog1.SelectedPath;
            textBox1.Text = selePath;
            ft.FillToTree(treeView1, selePath);
            treeView1.ExpandAll();
        }

        private void 字体设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            treeView1.Font = fontDialog1.Font;
           
        }



        private void Form1_Resize(object sender, EventArgs e)
        {
            treeView1.Top = 60;
            treeView1.Width = Width-50;
            treeView1.Height = Height-100;
            treeView1.Left = 20;
            textBox1.Width = Width - 250;
            button1.Left = Width - 120;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            selePath = textBox1.Text.Trim();
            FillTree ft = new FillTree();
            ft.FillToTree(treeView1, selePath);
            treeView1.ExpandAll();
        }

        private void treeView1_MouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Process.Start(e.Node.FullPath);
        }
    }
}
