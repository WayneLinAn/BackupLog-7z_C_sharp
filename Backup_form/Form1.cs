using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Compression;
using System.Diagnostics;


namespace Backup_form
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void DeleteAndCompressFile(string fileDirect, int saveDay)
        {
            try
            {
                string[] dirs = Directory.GetDirectories(fileDirect, "*", SearchOption.TopDirectoryOnly);
                foreach (string dir in dirs)
                {
                    Console.WriteLine(dir);
                    DateTime nowTime = DateTime.Now;
                    //string[] files = Directory.GetFiles(fileDirect, "*.log", SearchOption.AllDirectories);  //獲取該目錄下所有 .log文件
                    string[] files = Directory.GetFiles(dir, "*.log");  //獲取該目錄下所有 .log文件
                    foreach (string file in files)
                    {
                        CreateDir();
                        FileInfo fileInfo = new FileInfo(file);
                        TimeSpan t = nowTime - fileInfo.CreationTime;  //當前時間  減去 文件創建時間
                        int day = t.Days;
                        if (day > saveDay)   //保存的時間 ；  單位：天
                        {
                            Zip(fileInfo.FullName, fileInfo.Name, dir);
                            File.Delete(file);  //刪除超過時間的文件
                        }
                    }
                }
            }
            catch (Exception expc)
            {
                Console.WriteLine("The process failed: {0}", expc.ToString());
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            //DeleteFile(Environment.CurrentDirectory + "\\CYMC_LOG\\ai_mgr\\", 7);  //刪除該目錄下 超過 7天的文件
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text.ToString().All(char.IsDigit))
                {
                    Console.WriteLine("The Given String is a Number.");
                    DeleteAndCompressFile(Environment.CurrentDirectory + "\\CYMC_LOG", Int32.Parse(textBox1.Text));  
                }
                else
                {
                    Console.WriteLine("The Given String is Not a Number.");
                    throw new InvalidOperationException("The Given String is Not a Number.");
                }
            }
            catch (Exception)
            {

                Console.WriteLine("The Given String is Not a Number.");
                MessageBox.Show("The Given String is Not a Number.");
            }
          
        }
        private void Zip(string file_Full_Name, string file_Name, string fileDirect)
        {
            Process process = new Process();
            process.StartInfo.FileName = @".\tools\7z.exe";
            process.StartInfo.Arguments = @"a -tzip "+ fileDirect+ "\\backuplog\\" + file_Name+".zip "+ file_Full_Name;
            process.Start();
        }
            private void button2_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = @".\tools\7z.exe";
            process.StartInfo.Arguments = @"a -tzip C:\Users\ASUS\wayne\2.Technique\Coding\8.C#\Backup_form\Backup_form\bin\Debug\CYMC_LOG\ai_mgr\backuplog\test.zip C:\Users\ASUS\wayne\2.Technique\Coding\8.C#\Backup_form\Backup_form\bin\Debug\CYMC_LOG\ai_mgr\test.log";
            process.Start();
        }

        private void Unzip(string fileDirect, string file_Name)
        {
            Process process = new Process();
            process.StartInfo.FileName = @".\tools\7z.exe";
            process.StartInfo.Arguments = @"e "+ fileDirect + "backuplog\\" + file_Name + ".zip "+" -o"+ fileDirect + " -y";
            process.Start();
        }

         public void CreateDir()
        {
            // Specify the directory you want to manipulate.
            string path = @".\CYMC_LOG\ai_mgr\backuplog";

            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(path))
                {
                    Console.WriteLine("That path exists already.");
                    return;
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(path);
                Console.WriteLine("The directory was created successfully at {0}.", Directory.GetCreationTime(path));

                // Delete the directory.
                //di.Delete();
                //Console.WriteLine("The directory was deleted successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
            finally { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CreateDir();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (textBox1.Text.ToString().All(char.IsDigit))
            {
                Console.WriteLine("The Given String is a Number.");
            }
            else
            {
                Console.WriteLine("The Given String is Not a Number.");
            }
        }
    }
}
