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
}
