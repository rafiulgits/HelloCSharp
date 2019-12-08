using System;
using System.IO;

namespace CSharp.FileIO
{
    public class FileHandling {
        private static string directoryPath = @"D:\Temp";
        private static string filePath = directoryPath+@"\index.txt";

        public static void WriteOperation(){
            DirectoryFix();
            Console.WriteLine($"Writing on : {filePath}");
            if(!File.Exists(filePath)){
                var stream = File.Create(filePath);
                byte[] byteData = System.Text.Encoding.ASCII.GetBytes("Hello World");
                stream.Write(byteData);
                stream.Close();
            }
            else{
                File.WriteAllText(filePath, "Hello World");
            }
        }

        private static void DirectoryFix(){
            if(!Directory.Exists(directoryPath)){
                Console.WriteLine($"{directoryPath} is not exist. Creating...");
                Directory.CreateDirectory(directoryPath);
            }
        }

        public static void ReadOperation(){
            if(File.Exists(filePath)){
                Console.WriteLine(File.ReadAllText(filePath));
            }
        }
    }


    public class FileIOExamples {
        public static void FileReadWrite(){
            FileHandling.WriteOperation();
            FileHandling.ReadOperation();
        }
    }
}