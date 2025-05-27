using Microsoft.AspNetCore.SignalR;
using NewsPortal.Models;

public class NewsHub : Hub
{
    private readonly INewsRepository _repository;

    public NewsHub(INewsRepository repository)
    {
        _repository = repository;
    }

    public async Task SendReaction(int newsId)
    {
        var news = _repository.News.FirstOrDefault(n => n.NewsID == newsId);
        if (news != null)
        {
            news.ReactionCount++;
            _repository.UpdateNews(news);
            await Clients.All.SendAsync("ReceiveReaction", news.NewsID, news.ReactionCount);
        }
    }
}

