using System;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string path;
        int max = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void chooseFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();            
            folderBrowserDialog.ShowNewFolderButton = false;
            if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                path = folderBrowserDialog.SelectedPath;
                label2.Text = $"Path: {path}";
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void zad1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            zadanie1(new DirectoryInfo(path));
        }

        private void zadanie1(DirectoryInfo directoryInfo)
        {            
            FileInfo[] fileInfos = directoryInfo.GetFiles("*.*");
            listBox1.Items.Add($"                   {fileInfos.Length} in folder: {directoryInfo.FullName}:");

            int cur = fileInfos.Length;
            

            foreach (FileInfo file in fileInfos)
            {
                listBox1.Items.Add($"{file.ToString()}, {file.CreationTime}");
                if (max < cur && file.CreationTime.Year == 2021)
                {
                    max = fileInfos.Length;
                    label1.Text = $"Max files: {max} in {directoryInfo.FullName}";
                }
            }
            DirectoryInfo[] folders = directoryInfo.GetDirectories();
            foreach(DirectoryInfo directory in folders)
            {                
                zadanie1(directory);
            }
        }

        private void zad2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int count = 0;
            listBox1.Items.Clear();

            string[] subDirs = Directory.GetFiles(path);
            foreach (string subdirPath in subDirs)
            {
                System.IO.FileInfo fileInfo = new FileInfo(subdirPath);
                DateTime creationTime = Directory.GetCreationTime(subdirPath);
                if (creationTime.Year >= 2010 && creationTime.Year <= 2021 && fileInfo.Length > 1000000)
                {
                    listBox1.Items.Add($" {subdirPath}, {creationTime.Year}, {fileInfo.Length}");                
                    count++;
                }
            }
            label1.Text = $"Count: {count}";            
        }

        private void zad3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            string pattern = Microsoft.VisualBasic.Interaction.InputBox("Enter pattern: ");            
            string[] file = Directory.GetFiles(path, $"*.{pattern}", SearchOption.AllDirectories);           
            label1.Text = $"Count: {file.Length}";
            foreach(string str in file)
            {
                listBox1.Items.Add($"{str}");
            }
        }
    }
}