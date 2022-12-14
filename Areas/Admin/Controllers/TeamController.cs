using AspnetcoreEcommercedemo.Interfaces;
using AspnetcoreEcommercedemo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetcoreEcommercedemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly ITeamRepository _repo;
        private readonly IFileManager _fileManager;

        public TeamController(ITeamRepository repo,
            IFileManager fileManager)
        {
            _repo = repo;
            _fileManager = fileManager;
        }
        public IActionResult Index()
        {
            var team = _repo.GetAllTeams();
            return View(team);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return View(new TeamViewModel());
            else
            {
                var team = _repo.GetTeam((int)id);
                return View(new TeamViewModel
                {
                   Id = team.Id,
                   KnownAs = team.KnownAs,
                   Created = team.Created,
                   LastActive = team.LastActive,
                   Gender = team.Gender,
                   Introduction = team.Introduction,
                   LookingFor = team.LookingFor,
                   Interests = team.Interests,
                   City = team.City,
                   Country = team.Country,
                   CurrentImage = team.Photo
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TeamViewModel vm)
        {
            var team = new Models.Team
            {
                Id = vm.Id,
                KnownAs = vm.KnownAs,
                Created = vm.Created,
                LastActive = vm.LastActive,
                Gender = vm.Gender,
                Introduction = vm.Introduction,
                LookingFor = vm.LookingFor,
                Interests = vm.Interests,
                City = vm.City,
                Country = vm.Country
            };
            if (vm.Photo == null)
            {
                team.Photo = vm.CurrentImage;
            }
            else
            {
                team.Photo = await _fileManager.SaveImage(vm.Photo);
            }
            if (team.Id > 0)
                _repo.UpdateTeam(team);
            else
                _repo.AddTeam(team);
            if (await _repo.SaveChangesAsync())
                return RedirectToAction("Index");
            else
                return View(team);
        }

        [HttpGet("/TeamPhoto/{teamPhoto}")]
        [ResponseCache(CacheProfileName = "Monthly")]
        public IActionResult MemberPhoto(string teamPhoto)
        {
            var mine = teamPhoto.Substring(teamPhoto.LastIndexOf('.') + 1);
            return new FileStreamResult(_fileManager.ImageStream(teamPhoto), $"teamPhoto/{mine}");
        }
    }
}
