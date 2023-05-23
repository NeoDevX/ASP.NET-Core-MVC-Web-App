#pragma warning disable 1998

using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.Services.Data;
using WebApp.Services.Image;
using WebApp.ViewModel;

namespace WebApp.Controllers
{
    public class ClubController : Controller
    {
        private readonly IDataProvider _dataProvider;
        private readonly IDataUpdater _dataUpdater;
        private readonly IImageService _imageService;

        public ClubController(IDataProvider dataProvider, IDataUpdater dataUpdater, IImageService imageService)
        {
            _dataProvider = dataProvider;
            _dataUpdater = dataUpdater;
            _imageService = imageService;
        }

        public async Task<IActionResult> Index()
        {
            var clubs = await _dataProvider.GetClubs();
            return View(clubs);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var club = await _dataProvider.GetClubBy(id);
            return View(club);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateClubViewModel clubViewModel)
        {
            if (ModelState.IsValid == false) 
                ModelState.AddModelError("CreateClubError", "Failed to Create Club");

            var result = await _imageService.AddImage(clubViewModel.Image); 
            var club = new Club
            {
                Title = clubViewModel.Title,
                Description = clubViewModel.Description,
                ClubCategory = clubViewModel.ClubCategory,
                Image = result?.Url.ToString(),
                Address = new Address
                {
                    State = clubViewModel.Address.State,
                    City = clubViewModel.Address.City,
                    Street = clubViewModel.Address.Street
                }
            };
            
            _dataUpdater.Add(club);
            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            var club = await _dataProvider.GetClubBy(id);
        
            if (club == null)
                return View("Error");

            var editClubViewModel = new EditClubViewModel
            {
                Title = club.Title,
                Description = club.Description,
                Address = club.Address,
                AddressId = club.AddressId,
                ClubCategory = club.ClubCategory
            };

            return View(editClubViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditClubViewModel clubViewModel)
        {
            var club = await _dataProvider.GetClubBy(id, true); 
            
            if (ModelState.IsValid == false || club == null)
            {
                ModelState.AddModelError("EditClubError", "Failed to Edit Club");
                return View(clubViewModel);
            }

            try
            {
                await _imageService.DeleteImage(club.Image);
            }
            catch (Exception)
            {
                ModelState.AddModelError("DeleteImageError", "Failed to Delete Image");
                return View(clubViewModel);
            }

            var result = await _imageService.AddImage(clubViewModel.Image);
            var updatedClub = new Club
            {
                Id = id,
                Title = clubViewModel.Title,
                Description = clubViewModel.Description,
                AddressId = clubViewModel.AddressId,
                ClubCategory = clubViewModel.ClubCategory,
                Image = result?.Url.ToString(),
                Address = new Address
                {
                    State = clubViewModel.Address.State,
                    City = clubViewModel.Address.City,
                    Street = clubViewModel.Address.Street
                }
            };

            _dataUpdater.Update(updatedClub);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var club = await _dataProvider.GetClubBy(id);
            return club == null ? View("Error") : View(club);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var club = await _dataProvider.GetClubBy(id);
            if (club == null)
                return View("Error");

            _dataUpdater.Delete(club);
            return RedirectToAction("Index");
        }
    }
}