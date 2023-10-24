using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace PokeCareAndBattle
{
    public class Pokemon
    {
        public Pokemon()
        {
            this.Nickname = "";
            this.Nome = "";
            this.Tipo = "";
            this.Nivel = 1;
            this.Img = "";
            this.Fome = 100;
            this.Higiene = 100;
            this.Felicidade = 100;
        }

        public Pokemon(string nome, string tipo, int nivel, string img)
        {
            this.Nickname = "";
            this.Nome = nome;
            this.Tipo = tipo;
            this.Nivel = nivel;
            this.Img = img;
            this.Fome = 100;
            this.Higiene = 100;
            this.Felicidade = 100;
        }

        private string nickname;
        public String Nickname
        {
            get { return nickname; }
            set { nickname = value.ToUpper(); }
        }

        private string nome;
        public string Nome{get { return this.nome; }set { this.nome = value.ToUpper(); }}
        
        private string tipo;
        public string Tipo{ get { return this.tipo; } set { this.tipo = value.ToUpper(); }}
        public int Nivel { get; set; }
        public String Img { get; set; }
        public float Fome { get; set; }
        public float Higiene { get; set; }
        public float Felicidade { get; set; }

        public void ExibirDados()
        {
            Console.WriteLine("Nome do Pokemon: " + this.Nome);
            Console.WriteLine("Tipo: " + this.Tipo);
            Console.WriteLine("Nível: " + this.Nivel);
        }
        public void ExibirDados(Boolean formatado)
        {
            if (formatado == true)
            {
                Console.WriteLine("Nome: " + this.Nome + " | Tipo: " + this.Tipo + " | Nível: " + this.Nivel + " |");
            }
            else
            {
                Console.WriteLine("Nome do Pokemon: " + this.Nome);
                Console.WriteLine("Tipo: " + this.Tipo);
                Console.WriteLine("Poder: " + this.Nivel);
            }
        }

        public void PokeStatus()
        {
            Console.WriteLine("---------------------");
            Console.WriteLine("  STATUS DO POKEMON  ");
            Console.WriteLine("---------------------");
            Console.WriteLine("Nome: {0}", this.Nickname);
            Console.WriteLine("Pokemon: {0}", this.Nome);
            Console.WriteLine("Fome: {0}", this.Fome);
            Console.WriteLine("Higiene: {0}", this.Higiene);
            Console.WriteLine("Felicidade: {0}", this.Felicidade);
            Console.WriteLine("Nível: {0}", this.Nivel);
            Console.WriteLine("---------------------");
        }
    }
}
