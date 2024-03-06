using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class ProfileService
{
    private readonly AppDbContext _context;
    private readonly UserManager<UserEntity> _userManager;

    public ProfileService(AppDbContext context, UserManager<UserEntity> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    //public async Task<bool> UpdateUserAsync (UserEntity user)
    //{
    //    try
    //    {
    //        var userResult = await _userManager.Users.FirstOrDefaultAsync(user => user.Email == user.Email);
    //        if (userResult != null)
    //        {
    //            userResult.FirstName = user.FirstName;
    //            userResult.LastName = user.LastName;
    //            userResult.Email = user.Email;
    //            userResult.UserName = user.Email;
    //            userResult.PhoneNumber = user.PhoneNumber;
    //        }
        
    //    }
    //    catch { }
    //}
}
