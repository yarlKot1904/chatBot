using chatBot.Data;
using chatBot.Models;
using Microsoft.AspNetCore.Mvc;

namespace chatBot.Services.Interfaces
{
    public interface IPostsService
    {
        PostModel Create(PostModel model);

        PostModel Update(PostModel model);

        PostModel Get(int id);

        List<PostModel> Get();

        void Delete(int id);
    }
}
