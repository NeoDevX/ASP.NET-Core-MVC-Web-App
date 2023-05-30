using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Extensions;
using WebApp.Models;
using WebApp.Services.Data;
using WebApp.Services.Image;
using WebApp.ViewModel;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IDataProvider _dataProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IImageService _imageService;
        private readonly UserManager<AppUser> _userManager;

        public UserController(IDataProvider dataProvider, IImageService imageService, IHttpContextAccessor httpContextAccessor, 
            UserManager<AppUser> userManager)
        {
            _dataProvider = dataProvider;
            _imageService = imageService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        [HttpGet("Users")]
        public async Task<IActionResult> Index()
        {
            var users = await _dataProvider.GetAllUsers();
            var userViewModels = new List<AppUserViewModel>();

            foreach (var user in users)
            {
                var userViewModel = new AppUserViewModel
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    ProfileImageUrl = user.ProfileImageUrl,
                    Address = user.Address,
                    Mileage = user.Mileage,
                    Pace = user.Pace
                };
                userViewModels.Add(userViewModel);
            }

            return View(userViewModels);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = await _dataProvider.GetUserBy(id);
            var userViewModel = new UserDetailViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Pace = user.Pace,
                Mileage = user.Mileage,
                Address = user.Address,
                ProfileImageUrl = user.ProfileImageUrl
            };
            return View(userViewModel);
        }
        
        public async Task<IActionResult> EditProfile()
        {
            string userId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _dataProvider.GetUserBy(userId);
            if (user == null)
                return View("Error");
            
            var userViewModel = new EditProfileViewModel
            {
                UserName = user.UserName,
                Pace = user.Pace,
                Mileage = user.Mileage,
                Address = user.Address
            };
            return View(userViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileViewModel editProfileViewModel)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null || ModelState.IsValid == false)
            {
                ModelState.AddModelError("EditProfileError", "Failed to edit profile");
                return View("Error");
            }
            
            if (string.IsNullOrEmpty(user.ProfileImageUrl) == false)
            {
                try
                {
                    await _imageService.DeleteImage(user.ProfileImageUrl);
                }
                catch (Exception)
                {
                    ModelState.AddModelError("DeleteImageError", "Failed to delete image");
                    return View(editProfileViewModel);
                }
            }

            var imageResult = await _imageService.AddImage(editProfileViewModel.Image);

            user.Address = new Address
            {
              State  = editProfileViewModel.Address?.State,
              Street = editProfileViewModel.Address?.Street,
              City = editProfileViewModel.Address?.City
            };
            user.Pace = editProfileViewModel.Pace;
            user.Mileage = editProfileViewModel.Mileage;
            user.ProfileImageUrl = imageResult?.Url.ToString();

            await _userManager.UpdateAsync(user);
            return RedirectToAction("Index");
        }
    }
}