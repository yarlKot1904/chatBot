using System.Text.Json;

namespace chatBot.Data
{

    public class PersonDataManager
    {
        public static PersonData currentUser;


        private HashSet<PersonData> personDataSet = new HashSet<PersonData>();

        // Чтение данных из JSON файла и добавление их в HashSet
        public void LoadFromJson(string filePath)
        {
            string jsonData = File.ReadAllText(filePath);
            // Десериализация JSON в массив объектов PersonData
            PersonData[] personArray = JsonSerializer.Deserialize<PersonData[]>(jsonData);

            if (personArray != null)
            {
                // Добавление каждого объекта в HashSet
                foreach (PersonData person in personArray)
                {
                    personDataSet.Add(person);
                }
            }
            Console.WriteLine(personArray.Length);
            currentUser = personArray.FirstOrDefault();
        }

        // Запись массива данных из HashSet обратно в JSON файл
        public void SaveToJson(string filePath)
        {
            // Сериализация HashSet в массив
            PersonData[] personArray = new List<PersonData>(personDataSet).ToArray();
            string jsonData = JsonSerializer.Serialize(personArray);

            // Запись JSON данных в файл
            File.WriteAllText(filePath, jsonData);
        }

        // Получение HashSet для доступа к данным (опционально)
        public HashSet<PersonData> GetPersonDataSet()
        {
            return personDataSet;
        }
    }
}
