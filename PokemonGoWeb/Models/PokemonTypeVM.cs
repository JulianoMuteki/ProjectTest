using PokemonGOCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PokemonGOWeb.Models
{
    public class PokemonTypeVM
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public PokemonTypeVM(PokemonType pokemonType)
        {
            this.Id = pokemonType.Id;
            this.Description = pokemonType.Description;
        }
    }
}