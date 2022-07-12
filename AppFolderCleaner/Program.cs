using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFolderCleaner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Folder cleaner " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            Console.WriteLine("Application folder cleaner");
            Console.WriteLine("Created by C. Lambert / july 2022");
            Console.WriteLine("Cleaning of designated folders in config file");
            Console.WriteLine();
            Console.WriteLine("Loading configuration...");

            //Load custom config from App.config file
            List<FolderConfig> folders = new List<FolderConfig>();
            CustomSection customSection = (CustomSection)ConfigurationManager.GetSection("customSection");
            foreach (FolderConfig folderConfig in customSection.Elements)
            {
                if (folderConfig.Path != null && folderConfig.Path != "")
                {
                    folders.Add(folderConfig);
                }
            }

            Console.WriteLine("Found " + folders.Count + " folders");
            Console.WriteLine("The cleaning process is starting");

            foreach (FolderConfig folder in folders)
            {
                if (Directory.Exists(folder.Path))
                {
                    Console.Write("Cleaning folder " + folder.Path + "... ");

                    DirectoryInfo dirInfo = new DirectoryInfo(folder.Path);
                    //IOrderedEnumerable<FileInfo> 
                    List<FileInfo> files = dirInfo.GetFiles().OrderByDescending(p => p.CreationTime).ToList();

                    //Trim the oldest files, if there is too much files.
                    while (files.Count() > folder.MaxElements)
                    {
                        //Console.WriteLine("Delete file " + files.Last().FullName);
                        File.Delete(files.Last().FullName);
                        files.Remove(files.Last());
                        //files = dirInfo.GetFiles().OrderByDescending(p => p.CreationTime).ToList();
                    }
                    Console.WriteLine("Done.");
                }
                else
                    Console.WriteLine("Folder not found : " + folder.Path);
            }
        }
    }
}
