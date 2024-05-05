using chatBot.Answers;
using chatBot.Data;
using chatBot.Services;
using chatBot.Services.Interfaces;

internal class Program
{
    private static void Main(string[] args)
    {
        AnswerManager.Instance.Init();
        foreach (var button in AnswerManager.Instance.buttons)
        {
            Console.WriteLine(button.name);
        }

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllersWithViews();

        builder.Services.AddTransient<IPostsService, PostsService>();
        builder.Services.AddSingleton<MyDataContext>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();


        app.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action=Index}/{id?}");

        app.MapFallbackToFile("index.html"); ;

        app.Run();
    }
}