namespace chatBot.Models
{

    public class PostModel
    {
        public PostModel()
        {

        }

        public PostModel(PostModel model)
        {
            this.Id = model.Id;
            this.Header = model.Header;
            this.Timestamp = model.Timestamp;
            this.Text = model.Text;
        }

        public int Id { get; set; }
        public string Header { get; set; }
        
        public string Timestamp { get; set;}
        
        public string Text { get; set; }
    }
}
