using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace module16_dz
{
    class Program
    {
        static string logFilePath = "file_manager_log.txt";

        static void Main()
        {
            while (true)
            {
                Console.WriteLine("1. Просмотр содержимого директории");
                Console.WriteLine("2. Создание файла/директории");
                Console.WriteLine("3. Удаление файла/директории");
                Console.WriteLine("4. Копирование файла/директории");
                Console.WriteLine("5. Перемещение файла/директории");
                Console.WriteLine("6. Чтение файла");
                Console.WriteLine("7. Запись в файл");
                Console.WriteLine("8. Выйти");

                Console.Write("Выберите действие (1-8): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        DisplayDirectoryContents();
                        break;
                    case "2":
                        CreateFileOrDirectory();
                        break;
                    case "3":
                        DeleteFileOrDirectory();
                        break;
                    case "4":
                        CopyFileOrDirectory();
                        break;
                    case "5":
                        MoveFileOrDirectory();
                        break;
                    case "6":
                        ReadFile();
                        break;
                    case "7":
                        WriteToFile();
                        break;
                    case "8":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                        break;
                }
            }
        }

        static void DisplayDirectoryContents()
        {
            Console.Write("Введите путь к директории: ");
            string path = Console.ReadLine();

            try
            {
                string[] files = Directory.GetFiles(path);
                string[] directories = Directory.GetDirectories(path);

                Console.WriteLine("\nСписок файлов:");
                foreach (var file in files)
                {
                    Console.WriteLine(file);
                }

                Console.WriteLine("\nСписок директорий:");
                foreach (var directory in directories)
                {
                    Console.WriteLine(directory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void CreateFileOrDirectory()
        {
            Console.Write("Введите путь к файлу/директории для создания: ");
            string path = Console.ReadLine();

            try
            {
                Console.Write("Выберите тип (1 - файл, 2 - директория): ");
                int typeChoice = int.Parse(Console.ReadLine());

                if (typeChoice == 1)
                {
                    File.Create(path).Close();
                    Console.WriteLine("Файл создан успешно.");
                }
                else if (typeChoice == 2)
                {
                    Directory.CreateDirectory(path);
                    Console.WriteLine("Директория создана успешно.");
                }
                else
                {
                    Console.WriteLine("Неверный выбор типа.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void DeleteFileOrDirectory()
        {
            Console.Write("Введите путь к файлу/директории для удаления: ");
            string path = Console.ReadLine();

            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    Console.WriteLine("Файл удален успешно.");
                }
                else if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                    Console.WriteLine("Директория удалена успешно.");
                }
                else
                {
                    Console.WriteLine("Файл/директория не существует.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void CopyFileOrDirectory()
        {
            Console.Write("Введите путь к файлу/директории для копирования: ");
            string sourcePath = Console.ReadLine();

            Console.Write("Введите путь, куда скопировать: ");
            string destinationPath = Console.ReadLine();

            try
            {
                if (File.Exists(sourcePath))
                {
                    File.Copy(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)), true);
                    Console.WriteLine("Файл скопирован успешно.");
                }
                else if (Directory.Exists(sourcePath))
                {
                    CopyDirectory(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                    Console.WriteLine("Директория скопирована успешно.");
                }
                else
                {
                    Console.WriteLine("Файл/директория не существует.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void MoveFileOrDirectory()
        {
            Console.Write("Введите путь к файлу/директории для перемещения: ");
            string sourcePath = Console.ReadLine();

            Console.Write("Введите путь, куда переместить: ");
            string destinationPath = Console.ReadLine();

            try
            {
                if (File.Exists(sourcePath))
                {
                    File.Move(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                    Console.WriteLine("Файл перемещен успешно.");
                }
                else if (Directory.Exists(sourcePath))
                {
                    Directory.Move(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                    Console.WriteLine("Директория перемещена успешно.");
                }
                else
                {
                    Console.WriteLine("Файл/директория не существует.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void ReadFile()
        {
            Console.Write("Введите путь к файлу для чтения: ");
            string filePath = Console.ReadLine();

            try
            {
                string content = File.ReadAllText(filePath);
                Console.WriteLine($"\nСодержимое файла:\n{content}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void WriteToFile()
        {
            Console.Write("Введите путь к файлу для записи: ");
            string filePath = Console.ReadLine();

            Console.Write("Введите текст для записи в файл: ");
            string content = Console.ReadLine();

            try
            {
                File.WriteAllText(filePath, content);
                Console.WriteLine("Текст успешно записан в файл.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void CopyDirectory(string sourceDir, string destDir)
        {
            if (!Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
            }

            string[] files = Directory.GetFiles(sourceDir);
            foreach (string file in files)
            {
                string destFile = Path.Combine(destDir, Path.GetFileName(file));
                File.Copy(file, destFile, true);
            }

            string[] dirs = Directory.GetDirectories(sourceDir);
            foreach (string dir in dirs)
            {
                string destSubDir = Path.Combine(destDir, Path.GetFileName(dir));
                CopyDirectory(dir, destSubDir);
            }
        }
    }
}
