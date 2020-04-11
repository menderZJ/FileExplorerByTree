using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FileExplorerByTree
{
    class FillTree
    {

        public void  FillToTree(TreeView t, string path)
        {
            t.Nodes.Clear();
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);   // using System.IO;
                dir.GetDirectories();
                TreeNode ndRoot = new TreeNode(path);
                t.Nodes.Add(ndRoot);
                GetSubDirectoryNodes(ndRoot, ndRoot.Text, true);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// 填充目录到TreeView中
        /// </summary>
        /// <param name="tvw"></param>
        /// <param name="isSource"></param>
        private void FillDirectoryTree(TreeView tvw, bool isSource)
        {
            tvw.Nodes.Clear();

            // 获取逻辑驱动器，并放入根节点。
            // 用本机上所有逻辑驱动器填充数组。
            string[] strDrives = Environment.GetLogicalDrives();

            // 遍历驱动器，添加到树中
            // 用try/catch块，在驱动器未准备好时，如是一个空软盘或CD时，不把它添加到树中
            foreach (string rootDirectoryName in strDrives)
            {
                if (rootDirectoryName != @"E:\")
                    continue;
                try
                {
                    // 用所有一级子目录填充数组，如驱动器未准备好，抛出异常
                    DirectoryInfo dir = new DirectoryInfo(rootDirectoryName);   // using System.IO;
                    dir.GetDirectories();

                    TreeNode ndRoot = new TreeNode(rootDirectoryName);

                    // 为每个根目录添加节点
                    tvw.Nodes.Add(ndRoot);

                    // 添加子目录节点
                    // 如isSource==true，在TreeView中显示到文件，否则只显示到目录
                    GetSubDirectoryNodes(ndRoot, ndRoot.Text, isSource);
                }
                catch (Exception e)
                {
                    // 捕捉错误，在驱动器未准备好时。
                    MessageBox.Show(e.Message);
                }
            }
        }   // FillDirectoryTree

        /// <summary>
        /// 获取目录节点下的所有子目录，并添加到目录树中。
        /// 传入的参数为此子目录的父节点，此子目录的完整路径名，以及一个bool值，表示是否获取子目录的文件
        /// </summary>
        private void GetSubDirectoryNodes(TreeNode parentNode, string fullName, bool getFileNames)
        {
            DirectoryInfo dir = new DirectoryInfo(fullName);
            DirectoryInfo[] dirSubs = dir.GetDirectories();

            // 为每个子目录添加一个子节点
            foreach (DirectoryInfo dirSub in dirSubs)
            {
                // 不显示隐藏文件夹
                if ((dirSub.Attributes & FileAttributes.Hidden) != 0)
                {
                    continue;
                }

                //MessageBox.Show(dirSub.FullName);
                /// <summary>
                /// 每个目录都有完整的路径，分割后只显示最后一个节点
                /// </summary>
                TreeNode subNode = new TreeNode(dirSub.Name);
                parentNode.Nodes.Add(subNode);

                // 递归调用
                GetSubDirectoryNodes(subNode, dirSub.FullName, getFileNames);

            }
            if (getFileNames) // 书中源码中，这部分在foreach内部，不正确
            {
                // 获取此节点的所有文件
                FileInfo[] files = dir.GetFiles();

                // 放置节点后。放置子目录中的文件。
                foreach (FileInfo file in files)
                {
                    TreeNode fileNode = new TreeNode(file.Name);
                    parentNode.Nodes.Add(fileNode);
                }
            }
        }   // GetSubDirectoryNodes
    }
}
