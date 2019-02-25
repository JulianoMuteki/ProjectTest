using PokemonGOCore.Model;
using PokemonGOCore.Repository;
using System.Collections.Generic;
using System.Linq;

namespace PokemonGOCore.Service
{
    public class PokemonService
    {
        protected PokemonRepository _Repository;

        public PokemonService()
        {
            _Repository = new PokemonRepository();
        }

        public List<Pokemon> FindAll()
        {

            var allPokers = _Repository.FindAll();

            var serviceType = new PokemonTypeService();
            List<PokemonType> allTypes = serviceType.FindAll();
            allPokers.Select(x =>
            {
                x.PokemonType = (from z in allTypes where z.Id == x.PokemonTypeId select z)
                                 .FirstOrDefault(); return x;
            })
                     .ToList();

            return allPokers;

        }

        public List<Pokemon> FindByText(string word)
        {
            var allPokers = FindAll();
            var result = (from x in allPokers where x.Name.Contains(word) || x.PokemonType.Description.Contains(word) select x ).ToList();
           
            return result;

        }

        public void Update(Pokemon pokemon)
        {
            _Repository.Update(pokemon);
        }

        public void Create(Pokemon pokemon)
        {
            _Repository.Create(pokemon);
        }

        public Pokemon FindById(int id)
        {
            var serviceType = new PokemonTypeService();     
            var pokemon = _Repository.FindById(id);

            var type = serviceType.FindId(pokemon.PokemonTypeId);
            pokemon.PokemonType = type;

            return pokemon;
        }

        public void Delete(Pokemon pokemon)
        {
             _Repository.Remove(pokemon);
        }
    }
}
