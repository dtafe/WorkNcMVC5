using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ionic.Zip;
using System.IO;

namespace WorkNCInfoService.WorkZoneStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                 string folderStorage = "";
                 if (args.Count() == 0)
                     folderStorage = Directory.GetCurrentDirectory();
                 else
                     folderStorage = args[0];

                ZipFile zip = new ZipFile();
                Console.Write("Begin Zip file");                
                zip.AddDirectory(folderStorage);
                zip.Save(folderStorage + ".zip");
                Console.Write("End zip \n");
                Console.Write("Create Blob \n");
                BlobManager blobManager = new BlobManager();
                Console.Write("Create Stream \n");
                FileStream stream = new FileStream(folderStorage + ".zip", FileMode.Open);
                Console.Write("Begin Upload \n");
                string name = Path.GetFileName(folderStorage);
               
                blobManager.UploadFromStream(stream, name + ".zip");
                Console.Write("End Uplaod \n");
                Console.Write("Begin Delete File Upalod \n");
                stream.Close();
                File.Delete(folderStorage + ".zip");
                Console.Write("End Delete File Upload file =",  name + ".zip");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
