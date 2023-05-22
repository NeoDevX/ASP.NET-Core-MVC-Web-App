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
    public class RaceController : Controller
    {
        private readonly IDataProvider _dataProvider;
        private readonly IDataUpdater _dataUpdater;
        private readonly IImageService _imageService;

        public RaceController(IDataProvider dataProvider, IDataUpdater dataUpdater, IImageService imageService)
        {
            _dataProvider = dataProvider;
            _dataUpdater = dataUpdater;
            _imageService = imageService;
        }
        
        public async Task<IActionResult> Index()
        {
            var model= await _dataProvider.GetRaces();
            return View(model);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var race = await _dataProvider.GetRaceBy(id);
            return View(race);
        }

        public IActionResult Create() => View();
        
        [HttpPost]
        public async Task<IActionResult> Create(CreateRaceViewModel raceViewModel)
        {
            if (ModelState.IsValid == false)
                ModelState.AddModelError("CreateRaceError", "Failed to Create Race");

            var result = await _imageService.AddImage(raceViewModel.Image);
            var race = new Race
            {
                Title = raceViewModel.Title,
                Description = raceViewModel.Description,
                RaceCategory = raceViewModel.RaceCategory,
                Address = new Address
                {
                    State = raceViewModel.Address.State,
                    City = raceViewModel.Address.City,
                    Street = raceViewModel.Address.Street
                },
                Image = result?.Url.ToString()
            };
            
            _dataUpdater.Add(race);
            return RedirectToAction("Index");
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            var race = await _dataProvider.GetRaceBy(id);
        
            if (race == null)
                return View("Error");

            var editRaceViewModel = new EditRaceViewModel
            {
                Title = race.Title,
                Description = race.Description,
                Address = race.Address,
                AddressId = race.AddressId,
                RaceCategory = race.RaceCategory
            };

            return View(editRaceViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditRaceViewModel raceViewModel)
        {
            var race = await _dataProvider.GetRaceBy(id, true); 
            
            if (ModelState.IsValid == false || race == null)
            {
                ModelState.AddModelError("EditRaceError", "Failed to Edit Race");
                return View(raceViewModel);
            }

            try
            {
                await _imageService.DeleteImage(race.Image);
            }
            catch (Exception)
            {
                ModelState.AddModelError("DeleteImageError", "Failed to Delete Image");
                return View(raceViewModel);
            }

            var result = await _imageService.AddImage(raceViewModel.Image);
            var updatedRace = new Race
            {
                Id = id,
                Title = raceViewModel.Title,
                Description = raceViewModel.Description,
                AddressId = raceViewModel.AddressId,
                RaceCategory = raceViewModel.RaceCategory,
                Image = result?.Url.ToString(),
                Address = new Address
                {
                    State = raceViewModel.Address.State,
                    City = raceViewModel.Address.City,
                    Street = raceViewModel.Address.Street
                }
            };

            _dataUpdater.Update(updatedRace);
            return RedirectToAction("Index");
        }
    }
}   