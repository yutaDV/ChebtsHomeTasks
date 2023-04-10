
using Newtonsoft.Json;

namespace FreeApiJoke;

public class Joke
{
    public string? type { get; set; }
    public string? setup { get; set; }
    public string? punchline { get; set; }
    public int? id { get; set; }
}

class Program
{
    static async Task Main(string[] args)
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("Choose a joke category:");
            Console.WriteLine("1 - General Jokes");
            Console.WriteLine("2 - Knock-Knock Jokes");
            Console.WriteLine("3 - Programming Jokes");
            Console.WriteLine("4 - Exit");

            // використовуємо цикл для перевірки правильності введення
            int category;
            while (!int.TryParse(Console.ReadLine(), out category) || category < 1 || category > 4)
            {
                Console.WriteLine("Invalid input. Please choose a number between 1 and 4.");
            }

            if (category == 4)
            {
                isRunning = false;
                continue;
            }

            string url = "";

            switch (category)
            {
                case 1:
                    url = "https://official-joke-api.appspot.com/jokes/random";
                    break;
                case 2:
                    url = "https://official-joke-api.appspot.com/jokes/knock-knock/random";
                    break;
                case 3:
                    url = "https://official-joke-api.appspot.com/jokes/programming/random";
                    break;
            }

            // json формується по різному для 1 та 2,3 тому формуємо два варіанти виведення жарту
            if (category == 2 || category == 3)
            { 
                // Відправляємо запит до API за допомогою HttpClient.
                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(url);

                // Отримуємо відповідь в форматі JSON.
                var json = await response.Content.ReadAsStringAsync();

                //Console.WriteLine(json); розкоментувати якщо потрібно побачити що отримали

                // десеріалізуємо JSON масив у список об'єктів класу Joke
                var jokes = JsonConvert.DeserializeObject<List<Joke>>(json);

                // виводимо setup та punchline для кожного анекдоту
                foreach (var joke in jokes)
                {
                    Console.WriteLine("");
                    Console.WriteLine(joke.setup);
                    Console.WriteLine(joke.punchline);
                }
            }
            if (category == 1)
            {
                // Відправляємо запит до API за допомогою HttpClient.
                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(url);

                // Отримуємо відповідь в форматі JSON.
                var json = await response.Content.ReadAsStringAsync();

                // Розпаковуємо JSON і виводимо setup та punchline.
                var joke = JsonConvert.DeserializeObject<Joke>(json);
                Console.WriteLine(joke.setup);
                Console.WriteLine(joke.punchline);
            }


            // запитуємо користувача, чи хоче він згенерувати інший анекдот
            Console.WriteLine("");
            Console.WriteLine("Do you want to generate another joke? (y/n)");
            string input = Console.ReadLine();

            // перевіряємо введення на правильність та опрацьовуємо його
            while (input != "y" && input != "n")
            {
                Console.WriteLine("Invalid input. Please enter 'y' or 'n'.");
                input = Console.ReadLine();
            }

            if (input == "n")
            {
                isRunning = false;
            }
        }
    }
}

