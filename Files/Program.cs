string workDir = "ForTests";
string extension = ".txt";
int counter = 1;

bool res = Directory.Exists(workDir);
if (!res)
{
    Directory.CreateDirectory(workDir);
    for (int i = 1; i <= 3; i++)
    {
        File.WriteAllText($"{workDir}\\{i}.txt", $"{i}-й тескстовый файл");
    }
}
Console.WriteLine("Введите текст");
string input = Console.ReadLine();

Menu(input);

void Menu(string userText)
{

    Console.WriteLine("Чтобы вы хотели сделать с текстом:\n1) Сохранить\n2) Дописать в существующий файл");
    string userChoice = Console.ReadLine();
    switch (userChoice)
    {
        case "1":
            Console.WriteLine("Введите имя файла");
            string fileName = Console.ReadLine();
            if (fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 || string.IsNullOrEmpty(fileName))
            {
                Console.WriteLine("Неправильное имя файла");
                Menu(userText);
            }
            if (File.Exists($"{workDir}\\{fileName}{extension}"))
            {
                Console.WriteLine($"Файл с таким именем уже существует по пути {workDir}\\{fileName}{extension}" +
                    $"\nНажмите клавишу Y/н для перезаписи файла, или любую другую клавишу для отмены");
                if (Console.ReadKey().Key == ConsoleKey.Y)
                {
                    File.WriteAllText($"{workDir}\\{fileName}{extension}", userText);
                }
                else
                {
                    Menu(userText);
                }
            }
            else
            {
                File.WriteAllText($"{workDir}\\{fileName}{extension}", userText);
            }

            break;
        case "2":
            string[] files = Directory.GetFiles(workDir);
            foreach (string file in files)
            {
                Console.WriteLine($"{counter}) {file}");
                counter++;
            }
            Console.WriteLine("Выберите файл");
            string fileNumb = Console.ReadLine();
            bool result = int.TryParse(fileNumb, out int number);
            if (number <= 0 || number > files.Length)
            {
                Console.WriteLine("Неправильный ввод данных");
                Menu(userText);
            }
            File.AppendAllText(files[number - 1], $"\n{userText}");
            Console.WriteLine("Новое содержимое данного файла:");
            string[] readedLines = File.ReadAllLines(files[number - 1]);
            foreach (string line in readedLines)
            {
                Console.WriteLine(line);
            }

            break;
        default:
            Console.WriteLine("Выберите 1-2");
            break;
    }
}