using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ASPNetMVC.Helpers;

public class IsCourseSaved
{
    private readonly AppDbContext _context;

    public IsCourseSaved(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> IsCourseSavedByUser(int courseId, string userId)
    {
        //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.Include(u => u.Courses).FirstOrDefaultAsync(x => x.Id == userId);

        if (user!.Courses != null)
        {
            if (user!.Courses!.Any(x => x.Id == courseId))
            {
                return true;
            }
        }

        return false;
    }
}
