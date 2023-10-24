using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeCareAndBattle
{
    public class Pokedex
    {
        public Pokedex()
        {
            this.InicializaLista();
        }

        private List<Pokemon> pokemons;

        public List<Pokemon> Pokemons
        {
            get { return pokemons; }
        }

        private void InicializaLista()
        {
            this.pokemons = new List<Pokemon>();
            Pokemon p = new Pokemon("charmander", "fogo", 1, Environment.CurrentDirectory + "\\ImgPokes\\charmander.jpg");
            this.pokemons.Add(p);
            p = new Pokemon("charmeleon", "fogo", 16, Environment.CurrentDirectory + "\\ImgPokes\\charmeleon.jpg");
            this.pokemons.Add(p);
            p = new Pokemon("charizard", "fogo", 32, Environment.CurrentDirectory + "\\ImgPokes\\charizard.jpg");
            this.pokemons.Add(p);
            p = new Pokemon("squirtle", "agua", 1, Environment.CurrentDirectory + "\\ImgPokes\\squirtle.jpg");
            this.pokemons.Add(p);
            p = new Pokemon("wartortle", "agua", 16, Environment.CurrentDirectory + "\\ImgPokes\\wartortle.jpg");
            this.pokemons.Add(p);
            p = new Pokemon("blastoise", "agua", 32, Environment.CurrentDirectory + "\\ImgPokes\\blastoise.jpg");
            this.pokemons.Add(p);
            p = new Pokemon("bulbassauro", "planta", 1, Environment.CurrentDirectory + "\\ImgPokes\\bulbassauro.jpg");
            this.pokemons.Add(p);
            p = new Pokemon("ivyssauro", "planta", 16, Environment.CurrentDirectory + "\\ImgPokes\\ivyssauro.jpg");
            this.pokemons.Add(p);
            p = new Pokemon("venossauro", "planta", 32, Environment.CurrentDirectory + "\\ImgPokes\\venossauro.jpg");
            this.pokemons.Add(p);
            p = new Pokemon("pichu", "eletrico", 1, Environment.CurrentDirectory + "\\ImgPokes\\pichu.jpg");
            this.pokemons.Add(p);
            p = new Pokemon("pikachu", "eletrico", 16, Environment.CurrentDirectory + "\\ImgPokes\\pikachu.jpg");
            this.pokemons.Add(p);
            p = new Pokemon("raichu", "eletrico", 32, Environment.CurrentDirectory + "\\ImgPokes\\raichu.jpg");
            this.pokemons.Add(p);
            p = new Pokemon("geodude", "pedra", 1, Environment.CurrentDirectory + "\\ImgPokes\\geodude.jpg");
            this.pokemons.Add(p);
            p = new Pokemon("graveler", "pedra", 16, Environment.CurrentDirectory + "\\ImgPokes\\graveler.jpg");
            this.pokemons.Add(p);
            p = new Pokemon("golem", "pedra", 32, Environment.CurrentDirectory + "\\ImgPokes\\golem.jpg");
            this.pokemons.Add(p);
            p = new Pokemon("snorlax", "normal", 1, Environment.CurrentDirectory + "\\ImgPokes\\snorlax.jpg");
            this.pokemons.Add(p);
            p = new Pokemon("lucario", "lutador", 1, Environment.CurrentDirectory + "\\ImgPokes\\lucario.jpg");
            this.pokemons.Add(p);
            p = new Pokemon("lapras", "gelo", 1, Environment.CurrentDirectory + "\\ImgPokes\\lapras.jpg");
            this.pokemons.Add(p);
            p = new Pokemon("Abra", "psiquico", 1, Environment.CurrentDirectory + "\\ImgPokes\\abra.jpg");
            this.pokemons.Add(p);
            p = new Pokemon("kadabra", "psiquico", 16, Environment.CurrentDirectory + "\\ImgPokes\\kadabra.jpg");
            this.pokemons.Add(p);
            p = new Pokemon("Alakazam", "psiquico", 32, Environment.CurrentDirectory + "\\ImgPokes\\alakazam.jpg");
            this.pokemons.Add(p);
            p = new Pokemon("gastly", "fantasma", 1, Environment.CurrentDirectory + "\\ImgPokes\\gastly.jpg");
            this.pokemons.Add(p);
            p = new Pokemon("haunter", "fantasma", 16, Environment.CurrentDirectory + "\\ImgPokes\\haunter.jpg");
            this.pokemons.Add(p);
            p = new Pokemon("gengar", "fantasma", 32, Environment.CurrentDirectory + "\\ImgPokes\\gengar.jpg");
            this.pokemons.Add(p);
            p = new Pokemon("dratini", "dragao", 1, Environment.CurrentDirectory + "\\ImgPokes\\dratini.jpg");
            this.pokemons.Add(p);
            p = new Pokemon("dragonair", "dragao", 16, Environment.CurrentDirectory + "\\ImgPokes\\dragonair.jpg");
            this.pokemons.Add(p);
            p = new Pokemon("dragonite", "dragao", 32, Environment.CurrentDirectory + "\\ImgPokes\\dragonite.jpg");
            this.pokemons.Add(p);
        }

        public void ListarPokemons()
        {
            for (int i = 0; i < this.Pokemons.Count; i++)
            {
                Console.Write(" | Código: " + i + "| ");
                this.Pokemons[i].ExibirDados(true);
                //Pokemon p = this.Pokemons[i];
                //p.ExibirDados(true);
            }
        }

        //public void ImagensPokemons()
        //{
        //    this.Pokemons[0] = Environment.CurrentDirectory + "\\ImgPokes\\charmander.jpg";
        //}
    }
}
