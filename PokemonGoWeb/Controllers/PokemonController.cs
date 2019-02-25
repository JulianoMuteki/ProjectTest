using PokemonGOCore.Model;
using PokemonGOCore.Service;
using PokemonGOWeb.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace PokemonGOWeb.Controllers
{
    public class PokemonController : Controller
    {
        public ActionResult Index()
        {
            var service = new PokemonService();
            List<Pokemon> allPokemons = service.FindAll();

            //ToDo: Passar o allPokemons para a view model
            //Opcional: Criar uma view model para passar para a tela ao invés de passar a model do banco
            List<PokemonViewModel> allPokemonsVms = (from x in allPokemons select new PokemonViewModel(x)).ToList();

            return View(allPokemonsVms);
        }

        [HttpPost]
        public ActionResult Index(string searchString)
        {
            var service = new PokemonService();
            List<Pokemon> filterPokemons = service.FindByText(searchString);
            List<PokemonViewModel> allPokemonsVms = (from x in filterPokemons select new PokemonViewModel(x)).ToList();
            return PartialView("_PartialList_Pokemons", allPokemonsVms);
        }

        public ActionResult Search(string word)
        {
            return View("Index");
        }

        public ActionResult Create()
        {
            var serviceType = new PokemonTypeService();
            List<PokemonType> allTypes = serviceType.FindAll();

            ViewBag.TypesPoke = new SelectList(allTypes, "Id", "Description");
            return View();
        }

        public ActionResult Delete(int? id)
        {
            var service = new PokemonService();
            Pokemon pokemon = service.FindById((int)id);

            PokemonViewModel poker = new PokemonViewModel(pokemon);
            return View(poker);
        }

        [HttpPost]
        public JsonResult Capture(int id)
        {
            var service = new PokemonService();

            var pokemon = service.FindById(id);
            pokemon.Capture();

            service.Update(pokemon);
            return Json(new
            {
                result = pokemon.CurrentHave,
                success = true
            },
               JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Create(PokemonViewModel pokemon)
        {
            var service = new PokemonService();

            Pokemon poker = new Pokemon()
            {
                Name = pokemon.Name,
                ImagePath = pokemon.ImagePath,
                CurrentHave = pokemon.CurrentHave,
                PokemonTypeId = pokemon.PokemonTypeId
            };

            service.Create(poker);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult AjaxDelete(int id)
        {
            var service = new PokemonService();
            Pokemon pokemon = service.FindById(id);
            service.Delete(pokemon);

            return Json(new
            {
                result = pokemon.CurrentHave,
                success = true
            },
              JsonRequestBehavior.AllowGet);
        }
    }
}