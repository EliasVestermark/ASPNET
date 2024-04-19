using ASPNetMVC.Models.Models;
using ASPNetMVC.Models.Views;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ASPNetMVC.Services;

public class ProfileManager(UserManager<UserEntity> userManager, SignInManager<UserEntity> signInManager, AddressService addressManager, AppDbContext context, IConfiguration configuration)
{
    private readonly UserManager<UserEntity> _userManager = userManager;
    private readonly SignInManager<UserEntity> _signInManager = signInManager;
    private readonly AddressService _addressManager = addressManager;
    private readonly AppDbContext _context = context;
    private readonly IConfiguration _configuration = configuration;

    public async Task<bool> UploadImage(ClaimsPrincipal claims, IFormFile file)
    {
        try
        {
            if (claims != null && file != null && file.Length != 0)
            {
                var user = await _userManager.GetUserAsync(claims);
                if (user != null)
                {
                    var fileName = $"p_{user.Id}_{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), _configuration["FileUploadPath"]!, fileName);

                    using var fs = new FileStream(filePath, FileMode.Create);
                    await file.CopyToAsync(fs);

                    user.ProfileImageUrl = fileName;
                    _context.Update(user);
                    await _context.SaveChangesAsync();

                    return true;
                }
            }
        }
        catch { }

        return false;
    }

    public async Task<ProfileBasicInfoModel> PopulateProfileBasicInfoAsync(ClaimsPrincipal claims)
    {
        var user = await _userManager.GetUserAsync(claims);
        if (user != null)
        {
            return new ProfileBasicInfoModel
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email!,
                Phone = user.PhoneNumber,
                Bio = user.Bio,
                ProfileImage = user.ProfileImageUrl
            };
        }

        return null!;
    }

    public async Task<ProfileAddressModel> PopulateAddressInfoAsync(ClaimsPrincipal claims)
    {
        var user = await _userManager.GetUserAsync(claims);

        if (user != null)
        {
            var address = await _addressManager.GetAddressAsync(user.Id);

            if (address != null)
            {
                return new ProfileAddressModel
                {
                    AddresLine1 = address.AddressLine1,
                    AddresLine2 = address.AddressLine2,
                    PostalCode = address.PostalCode,
                    City = address.City
                };
            }
        }

        return null!;
    }

    public async Task<ProfileIndexViewModel> PopulateProfileIndexAsync(ClaimsPrincipal claims)
    {
        var user = await _userManager.GetUserAsync(claims);

        return new ProfileIndexViewModel
        {
            BasicInfo = await PopulateProfileBasicInfoAsync(claims),
            AddressInfo = await PopulateAddressInfoAsync(claims),
            IsExternalAccount = user!.IsExternalAccount
        };
    }

    public async Task<ProfileIndexViewModel> GetSavedCourses(ProfileIndexViewModel viewModel, ClaimsPrincipal claims)
    {
        var userId = claims.FindFirstValue(ClaimTypes.NameIdentifier);
        var courseIdList = new List<int>();

        if (userId != null)
        {
            var user = await _context.Users.Include(u => u.Courses).FirstOrDefaultAsync(x => x.Id == userId);

            if (user!.Courses != null)
            {
                foreach (var course in user.Courses)
                {
                    courseIdList.Add(course.Id);
                }
            }
        }

        if (courseIdList.Count > 0)
        {
            using var http = new HttpClient();

            foreach (var id in courseIdList)
            {
                var response = await http.GetAsync($"https://localhost:7269/api/course/{id}?key=OTExMDIyYjQtNzUzMi00ZTQ0LTgxOWEtNDg3NDhiN2UwZGI1&Id=");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<SingleCourseModel>(json);
                    viewModel.SavedCoursesModel.Courses.Add(data!);
                }
            }
        }

        return viewModel;
    }

    public async Task<bool> UpdateBasicInfo(ClaimsPrincipal claims, ProfileBasicInfoModel model)
    {
        try
        {
            var user = await _userManager.GetUserAsync(claims);

            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.PhoneNumber = model.Phone;
                user.Bio = model.Bio;

                await _userManager.UpdateAsync(user);

                return true;
            }
        }
        catch { }

        return false;
    }

    public async Task<bool> UpdateAddressInfo(ClaimsPrincipal claims, ProfileAddressModel model)
    {
        try
        {
            var user = await _userManager.GetUserAsync(claims);

            if (user != null)
            {
                var address = await _addressManager.GetAddressAsync(user.Id);

                if (address != null)
                {
                    address.AddressLine1 = model.AddresLine1;
                    address.AddressLine2 = model.AddresLine2;
                    address.PostalCode = model.PostalCode;
                    address.City = model.City;
                    await _addressManager.UpdateAddressAsync(address);
                }
                else
                {
                    address = new AddressEntity
                    {
                        UserId = user.Id,
                        AddressLine1 = model.AddresLine1,
                        AddressLine2 = model.AddresLine2,
                        PostalCode = model.PostalCode,
                        City = model.City
                    };

                    await _addressManager.CreateAddressAsync(address);
                }

                return true;
            }

            return false;
        }
        catch { }

        return false;
    }

    public async Task<bool> ChangePassword(ClaimsPrincipal claims, ProfileSecurityModel model)
    {
        try
        {
            var user = await _userManager.GetUserAsync(claims);

            if (user != null)
            {
                await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

                return true;
            }

            return false;
        }
        catch { }

        return false;
    }

    public async Task<bool> DeleteAccount(ClaimsPrincipal claims, ProfileSecurityModel model)
    {
        try
        {
            if (model.DeleteModel!.Delete)
            {
                var user = await _userManager.GetUserAsync(claims);

                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    await _userManager.DeleteAsync(user);

                    return true;
                }

                return false;
            }
        }
        catch { }

        return false;
    }

    public async Task<bool> RemoveCourse(ClaimsPrincipal claims, int courseId)
    {
        var userId = claims.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.Include(u => u.Courses).FirstOrDefaultAsync(x => x.Id == userId);

        if (user! != null)
        {
            var courseToBeRemoved = user.Courses!.FirstOrDefault(x => x.Id == courseId);

            if (courseToBeRemoved != null)
            {
                user.Courses!.Remove(courseToBeRemoved);
                _context.Update(user);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        return false;
    }

    public async Task<bool> RemoveAllCourses(ClaimsPrincipal claims)
    {
        var userId = claims.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _context.Users.Include(u => u.Courses).FirstOrDefaultAsync(x => x.Id == userId);

        if (user!.Courses! != null)
        {
            foreach (var course in user.Courses!)
            {
                user.Courses.Remove(course);
                _context.Update(user);
                await _context.SaveChangesAsync();
            }

            return true;
        }

        return false;
    }
}
