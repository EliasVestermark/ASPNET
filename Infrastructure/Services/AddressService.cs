using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class AddressService(AppDbContext context)
{
    private readonly AppDbContext _context = context;

    public async Task<AddressEntity> GetAddressAsync(string userId)
    {
        var addressEntity = await _context.Addresses.FirstOrDefaultAsync(x => x.UserId == userId);
        return addressEntity!;
    }

    public async Task<bool> CreateAddressAsync(AddressEntity entity)
    {
        try
        {
            _context.Addresses.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateAddressAsync(AddressEntity entity)
    {
        try
        {
            var existing = await _context.Addresses.FirstOrDefaultAsync(x => x.UserId == entity.UserId);
            if (existing != null)
            {
                _context.Entry(existing).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }
    }
}
