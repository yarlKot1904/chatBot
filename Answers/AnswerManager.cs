using System.Text;

namespace chatBot.Answers
{
    public class AnswerManager
    {
        private static AnswerManager instance;
        public static AnswerManager Instance
        {
            get
            {
                instance ??= new AnswerManager();
                return instance;
            }
        }
        public List<Button> buttons { private set; get; }


        private AnswerManager()
        {
            
        }

        public void Init()
        {
            string baseDirectory = Directory.GetCurrentDirectory();


            string dataDirectory = Path.Combine(baseDirectory, "Data");
            string filePath = Path.Combine(dataDirectory, "buttons.json");

            buttons = ButtonManager.InitializeButtons(filePath);
        }

        public HashSet<Button> GenerateButtons(string input)
        {
            HashSet<Button> result = new();

            string[] potentialTags = input.Split(' ');

            foreach (string potentialTag in potentialTags)
            {
                foreach (Button button in buttons)
                {
                    if (button.tags.Contains(potentialTag))
                    {
                        result.Add(button);
                    }
                }
            } 

            return result;
        }

        public string GenerateAnswer(string input)
        {
            HashSet<Button> buttons = GenerateButtons(input);
            if (buttons.Count == 0)
            {
                return "Не понял.";
            }

            StringBuilder answer = new StringBuilder();

            answer.Append("Вот результат по вашему запросу:#");
            foreach (Button button in buttons)
            {
                answer.Append($"{button.name}|{button.URL}^");
            }
            answer.Remove(answer.Length - 1, 1);
            return answer.ToString();
        }

        
    }
}
