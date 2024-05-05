using Newtonsoft.Json;

namespace chatBot.Answers
{
    public class ButtonManager
    {
        public static List<Button> InitializeButtons(string jsonFilePath)
        {
            // Проверяем, существует ли файл
            if (!File.Exists(jsonFilePath))
            {
                Console.WriteLine("Файл не найден: " + jsonFilePath);
                return new List<Button>();
            }

            try
            {
                // Читаем содержимое файла
                string jsonData = File.ReadAllText(jsonFilePath);
                Console.WriteLine(jsonData);

                // Десериализуем JSON в список объектов Button
                List<Button> buttons = JsonConvert.DeserializeObject<List<Button>>(jsonData);

                return buttons;
            }
            catch (Exception ex)
            {
                // Обработка ошибок при чтении или десериализации файла
                Console.WriteLine("Ошибка при чтении файла или десериализации JSON: " + ex.Message);
                return new List<Button>();
            }
        }
    }
}
