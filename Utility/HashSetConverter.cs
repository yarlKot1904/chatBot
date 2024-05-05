using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace chatBot.Utility
{
    

    public class HashSetConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            // Определяем, можно ли конвертировать объект данного типа
            return objectType == typeof(HashSet<string>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Считываем JSON-данные
            JArray jsonArray = JArray.Load(reader);
            // Создаем новый HashSet<string>
            HashSet<string> hashSet = new HashSet<string>();

            // Добавляем каждый элемент из JSON-массива в HashSet
            foreach (var item in jsonArray)
            {
                hashSet.Add(item.ToString());
            }

            return hashSet;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // Записываем HashSet в виде массива JSON
            HashSet<string> hashSet = (HashSet<string>)value;
            serializer.Serialize(writer, hashSet);
        }
    }
}
