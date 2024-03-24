using ASPNetAPI.Models;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPNetAPI.Services;

public class SubscribersService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<bool> CreateSubscriber(SubscribeFormModel model)
    {
        try
        {
            var subscriberEntity = new SubscriberEntity
            {
                Email = model.Email,
                DailyNewsletter = model.DailyNewsletter,
                AdvertisingUpdates = model.AdvertisingUpdates,
                WeekInReview = model.WeekInReview,
                EventUpdates = model.EventUpdates,
                StartupsWeekly = model.StartupsWeekly,
                Podcasts = model.Podcasts
            };

            _context.Subscribers.Add(subscriberEntity);
            await _context.SaveChangesAsync();

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<IEnumerable<SubscriberEntity>> GetAllSubscribers()
    {
        var subscribers = await _context.Subscribers.ToListAsync();

        return subscribers;
    }

    public async Task<SubscriberEntity> GetOneSubscriber(int id)
    {
        var subscriber = await _context.Subscribers.FirstOrDefaultAsync(x => x.Id == id);

        return subscriber!;
    }
}
