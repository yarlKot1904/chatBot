using chatBot.Data;
using chatBot.Models;
using chatBot.Services.Interfaces;
using chatBot.Answers;
using System.Reflection;

namespace chatBot.Services
{
    public class PostsService : IPostsService
    {
        private MyDataContext _dataContext;
        public PostsService(MyDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public PostModel Create(PostModel model)
        {
            // Найти последний пост в базе данных для генерации уникального ID
            var lastPost = _dataContext.Posts.LastOrDefault();
            int newId = lastPost == null ? 1 : lastPost.Id + 1;
            model.Id = newId;

            // Добавить сообщение пользователя в базу данных
            _dataContext.Posts.Add(model);


            // Сгенерировать ответ от бота с использованием вашего AnswerManager
            string botResponseText = AnswerManager.Instance.GenerateAnswer(model.Text);

            // Создать объект ответа бота
            var botResponse = new PostModel
            {
                Id = newId + 1, // Уникальный ID для ответа бота
                Header = "bot",
                Text = botResponseText,
                Timestamp = DateTime.Now.ToString("o"), // Временная метка
            };

            // Добавить ответ бота в базу данных
            _dataContext.Posts.Add(botResponse);


            // Возвращаем сообщение пользователя и ответ от бота в формате "сообщение пользователя && ответ от бота"
            PostModel toReturn = new PostModel(model);
            toReturn.Text += $" && {botResponseText}";

            return toReturn;
        }


        public PostModel Update(PostModel model)
        {
            var modelToUpdate = _dataContext.Posts.FirstOrDefault(x => x.Id == model.Id);
            modelToUpdate.Text = model.Text;
            modelToUpdate.Timestamp = model.Timestamp;
            modelToUpdate.Header = model.Header;

            return modelToUpdate;
        }

        public void Delete(int id)
        {
            var modelToDelete = _dataContext.Posts.FirstOrDefault(x => x.Id == id);
            if (modelToDelete == null)
                return;
            _dataContext.Posts.Remove(modelToDelete);

        }

        public PostModel Get(int id)
        {
            return _dataContext.Posts.FirstOrDefault(x => x.Id == id);
        }

        public List<PostModel> Get()
        {
            return _dataContext.Posts;
        }


        
    }
}
