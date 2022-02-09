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

        //private void TimerDelete_Tick(object sender, EventArgs e)
        //{

        //    DeleteFile(Environment.CurrentDirectory + "\\Logs\\", 7);  //刪除該目錄下 超過 7天的文件

        //}

        private void DeleteFile(string fileDirect, int saveDay)
        {

            //string startPath = @".\start";
            //string zipPath = @".\result.zip";
            //string extractPath = @".\extract";
            DateTime nowTime = DateTime.Now;

            string[] files = Directory.GetFiles(fileDirect, "*.log", SearchOption.AllDirectories);  //獲取該目錄下所有 .log文件
            foreach (string file in files)
            {
                CreateDir();

                FileInfo fileInfo = new FileInfo(file);
                TimeSpan t = nowTime - fileInfo.LastWriteTime;  //當前時間  減去 文件創建時間
                int day = t.Days;
                if (day > saveDay)   //保存的時間 ；  單位：天
                {
                    Zip(fileInfo.FullName, fileInfo.Name, fileDirect);
                    File.Delete(file);  //刪除超過時間的文件

                    //MessageBox.Show(fileInfo.FullName);
                    //MessageBox.Show(fileInfo.Name);
                    //ZipFile.CreateFromDirectory(startPath, zipPath);
                    //ZipFile.ExtractToDirectory(zipPath, extractPath);
                }
            }
            
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            //DeleteFile(Environment.CurrentDirectory + "\\CYMC_LOG\\ai_mgr\\", 7);  //刪除該目錄下 超過 7天的文件
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DeleteFile(Environment.CurrentDirectory + "\\CYMC_LOG\\ai_mgr\\", 7);  //刪除該目錄下 超過 7天的文件
        }
        private void Zip(string file_Full_Name, string file_Name, string fileDirect)
        {
            Process process = new Process();
            process.StartInfo.FileName = @".\tools\7z.exe";
            process.StartInfo.Arguments = @"a -tzip "+ fileDirect+ "backuplog\\" + file_Name+".zip "+ file_Full_Name;
            process.Start();
        }
            private void button2_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = @".\tools\7z.exe";
            process.StartInfo.Arguments = @"a -tzip C:\Users\ASUS\wayne\2.Technique\Coding\8.C#\Backup_form\Backup_form\bin\Debug\CYMC_LOG\ai_mgr\backuplog\test.zip C:\Users\ASUS\wayne\2.Technique\Coding\8.C#\Backup_form\Backup_form\bin\Debug\CYMC_LOG\ai_mgr\test.log";
            process.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Process process = new Process();
            //process.StartInfo.FileName = @"C:\Program Files\7-zip\7z.exe";
            //process.StartInfo.Arguments = @"e E:\DannyChang.zip -oE:\ -y";
            //process.Start();
            Unzip(@"C:\Users\ASUS\wayne\2.Technique\Coding\8.C#\Backup_form\Backup_form\bin\Debug\CYMC_LOG\ai_mgr\", @"test");
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
