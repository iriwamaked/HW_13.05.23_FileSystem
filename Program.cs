using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HW_13._05._23
{
    internal class Program
    {
        public static void ArrInitiation(int []arr)
        {
            Random random = new Random();
            int min = -10000, max = 20000, randomNum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                
                randomNum = random.Next(min, max);
                arr[i] = randomNum;
            }
        }

        public static void CreateFile(string path, int[]arr) 
        {
            using (FileStream file = new FileStream(path, FileMode.OpenOrCreate))
            {
                using (StreamWriter write = new StreamWriter(file, Encoding.UTF8))
                {
                    for (int i = 0;i<arr.Length; i++)
                        write.WriteLine(arr[i]);
                }
            }
        }

        public static void ReadFileToArr(string path, int[]arr)
        {
            int j = 0;
            using (FileStream file = new FileStream(path, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(file, Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (int.TryParse(line, out int number))
                        {
                            arr[j] = number;
                            j++;
                        }
                        else
                        {
                            break;
                        }
                    }

                }
            }
        }
        static void Main(string[] args)
        {
            int choice = 1;
            do
            {
                Console.WriteLine("\n\tВыберите номер задания (1,2)");
                choice= Int32.Parse(Console.ReadLine());
                switch (choice) 
                { 
                    case 1:
                        Console.WriteLine("\n\tВведите название файла:");
                        string path=Console.ReadLine();                        
                        int[]arr= new int[10000];
                        ArrInitiation(arr);
                        //for (int i = 0;i<arr.Length;i++)
                        //{
                        //    Console.WriteLine(arr[i]);
                        //}
                        CreateFile(path, arr);
                        int[] arrAfterReading=new int[arr.Length];
                        ReadFileToArr(path, arrAfterReading);
                        int countPositive = 0, countNegative=0, countDoubleNumb=0, countFiveDigit=0;
                        foreach(int i in arrAfterReading)
                        {
                            if (i>0)
                                countPositive++;
                            if (i<0)
                                countNegative++;
                            if (i>9&&i<99)
                                countDoubleNumb++;
                            if (i>9999&&i<=99999)
                                countFiveDigit++;
                        }
                        Console.WriteLine("\n\tКоличество положительных чисел в файле: "+ countPositive);
                        Console.WriteLine("\n\tКоличество отрицательных чисел в файле: " + countNegative);
                        Console.WriteLine("\n\tКоличество двузначных чисел в файле: " + countDoubleNumb);
                        Console.WriteLine("\n\tКоличество пятизначных чисел в файле: " + countFiveDigit);
                        break;
                  case 2:
                        string filePath = "Lorem Ipsum.txt", userPath;
                        Console.WriteLine("\n\tВведите путь к файлу (по умолчанию - \"Lorem Ipsum.txt.\"");
                       
                            userPath = Console.ReadLine();
                            if (string.IsNullOrEmpty(userPath))
                            {
                                userPath = filePath;
                            }
                            if (!File.Exists(userPath))
                            {
                                userPath = filePath;
                            }
                        
                        Console.WriteLine("\n\tВведите слово для поиска: ");
                        string searchWord = Console.ReadLine();
                        bool wordFound = false;
                        int wordCount = 0;
                        string line;
                     
                            using (FileStream file = new FileStream(userPath, FileMode.Open))
                            using (StreamReader str = new StreamReader(file, Encoding.UTF8))
                            {

                                while ((line = str.ReadLine()) != null)
                                {
                                    if (line.Contains(searchWord))
                                    {
                                        wordFound = true;
                                        wordCount++;
                                    }
                                }

                                if (wordFound)
                                {
                                    Console.WriteLine("Слово найдено в файле " + wordCount + " раз.");
                                }
                                else
                                {
                                    Console.WriteLine("Слово не найдено в файле.");
                                }
                            file.Close();
                        }

                            
                        
                        Console.WriteLine("\n\tВведите слово для замены:");
                        string changeWord = Console.ReadLine();
                        string[] lines=File.ReadAllLines(userPath);
                        using (FileStream file = new FileStream(userPath, FileMode.Open))
                        using (StreamWriter writer = new StreamWriter(file, Encoding.UTF8))
                        {
                            for (int i=0; i<lines.Length; i++)
                            {
                                //if (lines[i].Contains(changeWord))
                                //{
                                    string modifiedLine = lines[i].Replace(searchWord, changeWord);
                                    writer.WriteLine(modifiedLine);
                                //}
                            }
                        }
                        break;
                }
            }
            while (choice!=0);
            



        }
    }
}
