using Microsoft.AspNetCore.Mvc;
using ZombieParty.Models;
using ZombieParty.Models.Data;
using ZombieParty.ViewModels;

namespace ZombieParty.Controllers
{
    public class ZombieTypeController : Controller
    {
        private BaseDonnees _baseDonnees { get; set; }
        public ZombieTypeController(BaseDonnees baseDonnees)
        {
            _baseDonnees = baseDonnees;
        }

        public IActionResult Index()
        {
            //this.ViewBag.MaListe = _baseDonnees.ZombieTypes.ToList(); /*new List<ZombieType>()*/

            //return View();
            List<ZombieType> zombieTypesList = _baseDonnees.ZombieTypes.ToList();

            return View(zombieTypesList);

        }

        //GET CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        public IActionResult Create(Models.ZombieType zombieType)
        {
            if (ModelState.IsValid)
            {
                // Ajouter à la BD
                _baseDonnees.ZombieTypes.Add(zombieType);
                TempData["Success"] = $"{zombieType.TypeName} zombie type added";
                return this.RedirectToAction("Index");
            }

            return this.View(zombieType);
        }
        public IActionResult Details(int id)
        {
            ZombieTypeVM zombieTypeVM = new()
            {
                ZombieType = new(),
                ZombiesList = _baseDonnees.Zombies.Where(z => z.ZombieTypeId == id).ToList(),
                ZombiesCount = _baseDonnees.Zombies.Count(z => z.ZombieTypeId == id),
                PointsAverage = _baseDonnees.Zombies.Where(z => z.ZombieTypeId == id).Average(p => p.Point)

            };

            zombieTypeVM.ZombieType = _baseDonnees.ZombieTypes.FirstOrDefault(zt => zt.Id == id);
            return View(zombieTypeVM);

        }


    }
}
