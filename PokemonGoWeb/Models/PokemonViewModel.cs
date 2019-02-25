using PokemonGOCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokemonGOWeb.Models
{
    public class PokemonViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public bool CurrentHave { get; set; }

        public int PokemonTypeId { get; set; }
        public PokemonTypeVM PokemonType { get; set; }

        public PokemonViewModel()
        {

        }

        public PokemonViewModel(Pokemon pokemon)
        {
            this.Id = pokemon.Id;
            this.Name = pokemon.Name;
            this.ImagePath = pokemon.ImagePath;
            this.CurrentHave = pokemon.CurrentHave;
            this.PokemonType = new PokemonTypeVM(pokemon.PokemonType);
        }
    }
}