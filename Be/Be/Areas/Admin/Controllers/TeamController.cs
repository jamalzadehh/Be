using AutoMapper;
using Be.DAL;
using Be.Models;
using Be.ViewModels.TeamVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Be.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;

        public TeamController(AppDbContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var teams = await _context.Teams.ToListAsync();

            return View(teams);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(TeamCreateVM team)
        {
            if (!ModelState.IsValid)
            {
                return View(team);
            }

            var isExisted = await _context.Teams.AnyAsync(x => x.FullName.ToLower().Contains(team.FullName.ToLower()));
            if (isExisted)
            {
                ModelState.AddModelError("FullName", "Boyle bir ad artiq movcuddur");
                return View(team);
            }

            if (!team.Image.ContentType.Contains("image"))
            {
                ModelState.AddModelError("Image", "Seklin tipi yanlisdir");
                return View(team);
            }
            if (!(team.Image.Length < 1024 * 1024 * 1024))
            {
                ModelState.AddModelError("Image", "Seklin olcusu uygun deyil");
                return View(team);
            }

            string filename = Guid.NewGuid() + team.Image.FileName;
            string path = Path.Combine(_environment.ContentRootPath, "wwwroot", "assets", "img", filename);

            using (FileStream stream = new(path, FileMode.Create))
            {
                await team.Image.CopyToAsync(stream);
            }

            var newteam = _mapper.Map<Team>(team);
            newteam.ImgUrl = filename;

            await _context.Teams.AddAsync(newteam);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == id);
            if (team == null)
            {
                throw new Exception("Movcud deyil");
            }
            var vm = _mapper.Map<TeamUpdateVM>(team);
            vm.ImageUrl = team.ImgUrl;
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(TeamUpdateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var existed = await _context.Teams.FirstOrDefaultAsync(x => x.Id == vm.Id);
            if (existed == null)
            {
                throw new Exception("Artiq Movcuddur");
            }
            string filename = existed.ImgUrl;

            var isexisted = await _context.Teams.AnyAsync(x => x.FullName.ToLower().Contains(vm.FullName.ToLower()) && x.Id != vm.Id);
            if (isexisted)
            {
                ModelState.AddModelError("FullName", "Bu name artiq movcuddur");
                return View(vm);
            }
            if (vm.Image is not null)
            {
                filename = Guid.NewGuid() + vm.Image.FileName;
                string path = Path.Combine(_environment.ContentRootPath, "wwwroot", "assets", "img");
                if (System.IO.File.Exists(path + "/" + existed.ImgUrl))
                {
                    System.IO.File.Delete(path + "/" + existed.ImgUrl);
                }


                using (FileStream stream = new(path + "/" + filename, FileMode.Create))
                {
                    await vm.Image.CopyToAsync(stream);
                }


            }


            existed.ImgUrl = filename;

            existed = _mapper.Map(vm, existed);
            existed.ImgUrl = filename;

            _context.Teams.Update(existed);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }



    }
}
