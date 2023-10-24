using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;

namespace PokeCareAndBattle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
              O jogo tem como objetivo que o usuário cuide de um pokemon dentro do console e use-o para batalhar contra o Rival ou contra um pokemon adversário(pc) aleatório.
              O game mantém os dados do pokemon mesmo ao fechar.
              Inicia o programa via prompt e coleta os dados do pokemon.
              Armazena e coleta os dados de fome, higiene, felicidade, nome dado ao pokemon, nome do player, tipo do pokemon e nivel dentro de um arquivo texto.
              Sempre que o usuário voltar a jogar, pode voltar a cuidar do seu respectivo pokemon.
              Sempre que o Rival perde a batalha e toda vez que o jogo reabrir o Pokemon do Rival é atualizado.
           */

            string file = "";//Arquivo onde é salvo os dados do pokemon
            Player player = new Player();//Cria um novo Player
            Pokedex pokedex = new Pokedex();//Cria uma nova lista de pokemons
            Pokemon pPlayer = new Pokemon();//Cria o Pokemon do usuário
            
            //define o pokemon do rival
            Random r = new Random();
            int codigo = r.Next(0, pokedex.Pokemons.Count);
            Pokemon pRival = pokedex.Pokemons[codigo];
            pRival.Nivel = r.Next(0, 100);

            //Verifica os dados inseridos pelo usuário
            VerificarDados(ref player, ref pokedex, ref pPlayer, ref file);

            //Programa Principal
            do
            {
                //Armazenar os dados
                ArmazenarDados(ref player, ref pPlayer);

                Console.Clear();
                int resp = Menu();
                                
                if (resp == 1)
                {
                    Console.Clear();
                    pPlayer.PokeStatus();
                    Console.WriteLine("Para voltar ao menu aperte <qualquer tecla> para sair aperte <ESC>.");
                }
                else if (resp == 2)
                {
                    do
                    {
                        Console.Clear();
                        AttCaract(ref pPlayer);
                        EstadoPoke(ref pPlayer);
                        PergPoke(ref pPlayer);

                    } while (Console.ReadKey().Key != ConsoleKey.Escape);
                    Console.WriteLine("AAperte <qualquer tecla> para voltar ao menu ou aperte <ESC> para sair do jogo.");
                    //Armazenar os dados
                    ArmazenarDados(ref player, ref pPlayer);
                }
                else if (resp == 3)
                {
                    TreinarPokemon(ref player, ref pPlayer, ref file);
                    //Armazenar os dados
                    ArmazenarDados(ref player, ref pPlayer);
                }
                else if (resp == 4)
                {
                    Console.Clear();
                    Console.WriteLine("---------------------------------------------------------------------");
                    Console.Write("Seu rival possui um: ");
                    pRival.ExibirDados(true);
                    Console.WriteLine("Para voltar ao menu aperte <qualquer tecla> para sair aperte <ESC>");
                    Console.WriteLine("---------------------------------------------------------------------");
                }
                else if (resp == 5)
                {
                    Console.Clear();
                    Console.WriteLine("---------------------------------------------------------------------");
                    Console.Write("Seu rival possui um: ");
                    pRival.ExibirDados(true);
                    Console.WriteLine("---------------------------------------------------------------------");
                    Thread.Sleep(3000);
                    Console.Clear();
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine("Os Pokemons estão batalhando!!!!!");
                    Console.WriteLine("---------------------------------");
                    Thread.Sleep(3000);
                    Console.Clear();
                    //batalha
                    if (pPlayer.Nivel > pRival.Nivel)
                    {
                        pPlayer.Felicidade += 20;
                        pPlayer.Higiene -= 10;
                        pPlayer.Fome -= 10;
                        pPlayer.Nivel += r.Next(1, 3);
                        if (pPlayer.Felicidade >= 100) pPlayer.Felicidade = 100;
                        if (pPlayer.Nivel >= 100)
                        {
                            pPlayer.Nivel = 100;
                            Console.WriteLine("---------------------------------------------------------------------");
                            Console.WriteLine("Parabens!!! Seu Pokemon atingiu o nível máximo!!!");
                            Console.WriteLine("---------------------------------------------------------------------");
                        }

                        codigo = r.Next(0, pokedex.Pokemons.Count);
                        pRival = pokedex.Pokemons[codigo];
                        pRival.Nivel = r.Next(0, 100);

                        Console.WriteLine("---------------------------------------------------------------------");
                        Console.WriteLine("Parabens!!! Você Ganhou!!!");
                        Console.WriteLine("---------------------------------------------------------------------");
                        Console.WriteLine("Seu Pokemon ficou mais feliz!!!");
                        Console.WriteLine("---------------------------------------------------------------------");
                        Console.WriteLine("Fique atento!!! Seu rival usará outro pokemon na próxima batalha!!!  ");
                        Console.WriteLine("---------------------------------------------------------------------");
                        Console.WriteLine("Para continuar jogando aperte <qualquer tecla> para sair aperte <ESC>");
                        Console.WriteLine("---------------------------------------------------------------------");
                        
                    }
                    else if (pPlayer.Nivel == pRival.Nivel)
                    {
                        pPlayer.Higiene -= 10;
                        pPlayer.Fome -= 10;
                        Console.WriteLine("---------------------------------------------------------------------");
                        Console.WriteLine("Houve um empate!!!");
                        Console.WriteLine("---------------------------------------------------------------------");
                        Console.WriteLine("Para continuar jogando aperte <qualquer tecla> para sair aperte <ESC>");
                        Console.WriteLine("---------------------------------------------------------------------");

                    }
                    else if(pPlayer.Nivel < pRival.Nivel)
                    {
                        pPlayer.Felicidade -= 20;
                        pPlayer.Higiene -= 10;
                        pPlayer.Fome -= 10;
                        if (pPlayer.Felicidade <= 0) pPlayer.Felicidade = 0;
                        Console.WriteLine("---------------------------------------------------------------------");
                        Console.WriteLine("Que pena!!! Você Perdeu!!!");
                        Console.WriteLine("---------------------------------------------------------------------");
                        Console.WriteLine("Seu pokemon ficou triste com isso!!!");
                        Console.WriteLine("---------------------------------------------------------------------");
                        Console.WriteLine("Para continuar jogando aperte <qualquer tecla> para sair aperte <ESC>");
                        Console.WriteLine("---------------------------------------------------------------------");

                    }
                    //Armazenar os dados
                    ArmazenarDados(ref player, ref pPlayer);
                }
                else if (resp == 6)
                {
                    codigo = r.Next(0, pokedex.Pokemons.Count);
                    Pokemon pPc = pokedex.Pokemons[codigo];
                    pPc.Nivel = r.Next(0, 100);
                    Console.Clear();
                    Console.WriteLine("---------------------------------------------------------------------");
                    Console.Write("Seu adversário possui um: ");
                    pPc.ExibirDados(true);
                    Console.WriteLine("---------------------------------------------------------------------");
                    Thread.Sleep(3000);
                    Console.Clear();
                    Console.WriteLine("---------------------------------");
                    Console.WriteLine("Os Pokemons estão batalhando!!!!!");
                    Console.WriteLine("---------------------------------");
                    Thread.Sleep(3000);
                    Console.Clear();
                    //batalha
                    if (pPlayer.Nivel > pPc.Nivel)
                    {
                        pPlayer.Felicidade += 20;
                        pPlayer.Higiene -= 10;
                        pPlayer.Fome -= 10;
                        pPlayer.Nivel += r.Next(1,3);
                        if (pPlayer.Felicidade >= 100) pPlayer.Felicidade = 100;
                        if (pPlayer.Nivel >= 100)
                        {
                            pPlayer.Nivel = 100;
                            Console.WriteLine("---------------------------------------------------------------------");
                            Console.WriteLine("Parabens!!! Seu Pokemon atingiu o nível máximo!!!");
                            Console.WriteLine("---------------------------------------------------------------------");
                        }
                        if (pPlayer.Felicidade >= 100) pPlayer.Felicidade = 100;
                        Console.WriteLine("---------------------------------------------------------------------");
                        Console.WriteLine("Parabens!!! Você Ganhou!!!");
                        Console.WriteLine("---------------------------------------------------------------------");
                        Console.WriteLine("Seu Pokemon ficou mais feliz!!!");
                        Console.WriteLine("---------------------------------------------------------------------");
                        Console.WriteLine("Para continuar jogando aperte <qualquer tecla> para sair aperte <ESC>");
                        Console.WriteLine("---------------------------------------------------------------------");

                    }
                    else if (pPlayer.Nivel == pPc.Nivel)
                    {
                        pPlayer.Higiene -= 10;
                        pPlayer.Fome -= 10;
                        Console.WriteLine("---------------------------------------------------------------------");
                        Console.WriteLine("Houve um empate!!!");
                        Console.WriteLine("---------------------------------------------------------------------");
                        Console.WriteLine("Para continuar jogando aperte <qualquer tecla> para sair aperte <ESC>");
                        Console.WriteLine("---------------------------------------------------------------------");

                    }
                    else if(pPlayer.Nivel < pPc.Nivel)
                    {
                        pPlayer.Felicidade -= 20;
                        pPlayer.Higiene -= 10;
                        pPlayer.Fome -= 10;
                        if (pPlayer.Felicidade <= 0) pPlayer.Felicidade = 0;
                        Console.WriteLine("---------------------------------------------------------------------");
                        Console.WriteLine("Que pena!!! Você Perdeu!!!");
                        Console.WriteLine("---------------------------------------------------------------------");
                        Console.WriteLine("Seu pokemon ficou triste com isso!!!");
                        Console.WriteLine("---------------------------------------------------------------------");
                        Console.WriteLine("Para continuar jogando aperte <qualquer tecla> para sair aperte <ESC>");
                        Console.WriteLine("---------------------------------------------------------------------");

                    }
                }
                else if (resp == 7)
                {
                    if (pPlayer.Fome <= 0)
                    {
                        Console.Clear();
                        string dir = Environment.CurrentDirectory + "\\PokeCares\\";
                        file = dir + pPlayer.Nickname + player.Nome + ".txt";
                        File.Delete(file);
                        Console.WriteLine("----------------------------------------------------------");
                        Console.WriteLine("!!! Você deixou seu pokemon com muita fome e ele morreu !!!");
                        Console.WriteLine("----------------------------------------------------------");
                        Console.WriteLine("Da próxima cuide melhor dele...");
                        Thread.Sleep(3500);
                        System.Environment.Exit(1);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("---------------------------------------");
                        Console.WriteLine("Obrigado por jogar PokeCare & Battle!!!");
                        Console.WriteLine("---------------------------------------");
                        Console.WriteLine("Até a próxima!!!");
                        Thread.Sleep(3500);
                        System.Environment.Exit(1);
                    }

                }

            } while (Console.ReadKey().Key != ConsoleKey.Escape);

        }
        
        
        //Menu do Game
        static int Menu()
        {
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("  Poke Care & Battle by Adrian Alexandre  ");
            Console.WriteLine("------------------------------------------");
            int resp = 0;
            try
            {
                Console.WriteLine("1 - Ver Status do Pokemon");
                Console.WriteLine("2 - Cuidar do seu Pokemon");
                Console.WriteLine("3 - Treinar Pokemon");
                Console.WriteLine("4 - Ver Pokemon do seu Rival");
                Console.WriteLine("5 - Batalhar contra seu Rival");
                Console.WriteLine("6 - Lutar contra adversário aleatório");
                Console.WriteLine("7 - Sair do jogo");
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("O que deseja fazer?");
                Console.Write("Digite:");
                resp = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("Digite um número entre 1 e 6!!!!");
            }
            return resp;
        }

        //Verifica os dados inseridos pelo usuário
        static void VerificarDados(ref Player player, ref Pokedex pokedex, ref Pokemon pPlayer, ref string file)
        {
            Boolean retorno = false;
                do
                {
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("Você já possui um pokemon pra cuidar?");
                Console.Write("Digite (S/N): ");
                string possuiPoke = Console.ReadLine().ToLower();
                    Console.Clear();
               
                switch (possuiPoke)
                    {
                        case "s":
                            //verificar os dados do Pokemon no arquivo texto
                            //Entrada de dados - Nomes
                            InputDados(ref player, ref pPlayer);
                            Console.Clear();
                            string dir = Environment.CurrentDirectory + "\\PokeCares\\";
                            file = dir + pPlayer.Nickname + player.Nome + ".txt";
                            if (File.Exists(file))
                            {
                            string[] dados = File.ReadAllLines(file);
                            pPlayer.Fome = float.Parse(dados[2]);
                            pPlayer.Higiene = float.Parse(dados[3]);
                            pPlayer.Felicidade = float.Parse(dados[4]);
                            pPlayer.Nome = dados[5];
                            pPlayer.Tipo = dados[6];
                            pPlayer.Nivel = int.Parse(dados[7]);
                            retorno = false;
                            if (pPlayer.Fome >= 1 & pPlayer.Fome <= 20)
                            {
                                pPlayer.Fome = 100;
                                pPlayer.Higiene = 100;
                                pPlayer.Felicidade = 100;
                                Console.WriteLine("-----------------------------------------------");
                                Console.WriteLine("Seu pokemon ficou muito fraco!!");
                                Console.WriteLine("Levamos ele no centro pokemon para se curar!!!");
                                Console.WriteLine("Agora ele está saudável e feliz!!!");
                                Console.WriteLine("-----------------------------------------------");
                                Console.WriteLine("Aperte <qualquer tecla> para continuar.");
                                Console.WriteLine("-----------------------------------------------");
                                Console.ReadKey();
                            }
                            if (pPlayer.Higiene <= 20)
                            {
                                Console.WriteLine("-----------------------------------------------");
                                Console.WriteLine("Seu pokemon está precisando de um banho !!!");
                                Console.WriteLine("Não se esqueça de cuidar dele!!!");
                                Console.WriteLine("-----------------------------------------------");
                                Console.WriteLine("Aperte <qualquer tecla> para continuar.");
                                Console.WriteLine("-----------------------------------------------");
                                Console.ReadKey();
                            }
                            if (pPlayer.Felicidade <= 20)
                            {
                                Console.WriteLine("-----------------------------------------------");
                                Console.WriteLine("Seu pokemon está ficando triste !!!");
                                Console.WriteLine("Não se esqueça de brincar com ele!!!");
                                Console.WriteLine("-----------------------------------------------");
                                Console.WriteLine("Aperte <qualquer tecla> para continuar.");
                                Console.WriteLine("-----------------------------------------------");
                                Console.ReadKey();
                            }
                        } 
                            else
                            {
                            Console.WriteLine("-----------------------------------------------------");
                            Console.WriteLine("Pokemon e usuario nao encontrados!!! Tente Novamente.");
                            Console.WriteLine("-----------------------------------------------------");
                            retorno = true;
                            Thread.Sleep(3000);
                            }
                            Console.Clear();
                        break;
                        case "n":
                            //Escolhe qual pokemon o usuario quer cuidar
                            EscolhaPokemon(ref pokedex, ref pPlayer);
                            Thread.Sleep(3000);
                            //Entrada de dados - Nomes
                            InputDados(ref player, ref pPlayer);
                            retorno = false;
                            Console.Clear();
                        break;
                        case "":
                        Console.WriteLine("------------------");
                        Console.WriteLine("Digite (S) ou (N):");
                        Console.WriteLine("------------------");
                        break;
                    }
                if (possuiPoke != "s" && possuiPoke != "n")
                {
                    Console.WriteLine("--------------------");
                    Console.WriteLine("Digite (S) ou (N)!!!");
                    Console.WriteLine("--------------------");
                    Thread.Sleep(3000);
                    Console.Clear();
                    retorno = true;
                }
                } while (retorno == true);
        }
        //Armazena os dados inseridos pelo usuário
        static void ArmazenarDados(ref Player player, ref Pokemon pPlayer)
        {
            string dir = Environment.CurrentDirectory + "\\PokeCares\\";
            string file = dir + pPlayer.Nickname.ToUpper() + player.Nome.ToUpper() + ".txt";
            string fileContent = pPlayer.Nickname + Environment.NewLine;
            fileContent += player.Nome + Environment.NewLine;
            fileContent += pPlayer.Fome + Environment.NewLine;
            fileContent += pPlayer.Higiene + Environment.NewLine;
            fileContent += pPlayer.Felicidade + Environment.NewLine;
            fileContent += pPlayer.Nome + Environment.NewLine;
            fileContent += pPlayer.Tipo + Environment.NewLine;
            fileContent += pPlayer.Nivel + Environment.NewLine;
            File.WriteAllText(file, fileContent);
        }
        
        //Controla as falas do pokemon do usuário
        static string Falas()
        {
            //falas do pokemon
            Random rand = new Random();
            string[] frases = new string[5];
            frases[0] = "A vida de um pokemon não é facil viu...";
            frases[1] = "Bora treinar!!!";
            frases[2] = "Bora dominar esse mundo pokemon?";
            frases[3] = "Tomara que eu evolua logo.";
            frases[4] = "Você é um ótimo treinador.";
            return frases[rand.Next(frases.Length)];
        }

        //Atualiza os status do pokemon do usuário
        static void AttCaract(ref Pokemon pPlayer)
        {
            Console.Clear();
            //alterar o status do Pokemon
            // 0 - alimento; 1 - Limpo; 2 - Feliz
            Random rand = new Random();
            int caract = 0;
            caract = rand.Next(3);
            switch (caract)
            {
                case 0: pPlayer.Fome -= rand.Next(15); break;
                case 1: pPlayer.Higiene -= rand.Next(15); break;
                case 2: pPlayer.Felicidade -= rand.Next(15); break;
            }

        }

        //Exibe uma fala do pokemon caso esteja com fome, sujo ou triste
        static void EstadoPoke(ref Pokemon pPlayer)
        {
            if (pPlayer.Fome > 20 && pPlayer.Fome < 50)
            {
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("To morrendo de fome!!!");
                Console.WriteLine("Me de alguma coisa pra comer por favor!!!");
                Console.WriteLine("-----------------------------------------");
            }
            else if (pPlayer.Higiene > 20 && pPlayer.Higiene < 50)
            {
                Console.WriteLine("-------------------------------------");
                Console.WriteLine("Que cheirinho estranho...");
                Console.WriteLine("Acho que to precisando de um banho!!!");
                Console.WriteLine("-------------------------------------");
            }
            else if (pPlayer.Felicidade > 20 && pPlayer.Felicidade < 50)
            {
                Console.WriteLine("--------------------------------------------------------------");
                Console.WriteLine("Estou ficando muito triste, tomara que não entre em depressão.");
                Console.WriteLine("Bora brincar de alguma coisa?");
                Console.WriteLine("--------------------------------------------------------------");
            }
            else
            {
                Console.WriteLine("---------------------------------------------------------------------------------------");
                Console.WriteLine(Falas());
                Console.WriteLine("---------------------------------------------------------------------------------------");
                Thread.Sleep(2600);
                Console.Clear();
                Console.WriteLine("---------------------------------------------------------------------------------------");
                Console.WriteLine(Falas());
                Console.WriteLine("---------------------------------------------------------------------------------------");
                Thread.Sleep(2600);
                Console.Clear();
            }
        }
        
        //Onde o usuário pode aumentar o nível do seu pokemon 
        static void TreinarPokemon(ref Player player, ref Pokemon pPlayer, ref string file)
        {

            Random r = new Random();
            Console.Clear();
            Console.WriteLine("----------------------");
            Console.WriteLine("Eba!!! Bora Treinar!!!");
            Console.WriteLine("----------------------");
            Thread.Sleep(2300);
            Console.Clear();
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Quero ser o mais forte de todos!!!");
            Console.WriteLine("----------------------------------");
            Thread.Sleep(2300);
            Console.Clear();

            pPlayer.Nivel += r.Next(1,6);
            pPlayer.Higiene -= 10;

            if (pPlayer.Nivel >= 16 && pPlayer.Nivel < 32)
            {
                if(pPlayer.Nome == "CHARMANDER")
                {
                    //Deleta o arquivo antigo do pokemon pré evolução
                    string dir = Environment.CurrentDirectory + "\\PokeCares\\";
                    file = dir + pPlayer.Nickname.ToUpper() + player.Nome.ToUpper() + ".txt";
                    File.Delete(file);

                    Console.WriteLine("Estou evoluindo!!!");
                    pPlayer.Nome = "CHARMELEON";
                    pPlayer.Img = Environment.CurrentDirectory + "\\ImgPokes\\charmeleon.jpg";
                    Thread.Sleep(3000);
                    Console.Clear();
                    Console.WriteLine("--------------------------------------------------");
                    Console.WriteLine("PARABÉNS SEU CHARMANDER EVOLUIU PARA CHARMELEON!!!");
                    Console.WriteLine("--------------------------------------------------");
                    ExibirImagem(pPlayer.Img, 60, 60);
                }
                else if (pPlayer.Nome == "SQUIRTLE")
                {
                    //Deleta o arquivo antigo do pokemon pré evolução
                    string dir = Environment.CurrentDirectory + "\\PokeCares\\";
                    file = dir + pPlayer.Nickname.ToUpper() + player.Nome.ToUpper() + ".txt";
                    File.Delete(file);

                    Console.WriteLine("Estou evoluindo!!!");
                    pPlayer.Nome = "WARTORTLE";
                    pPlayer.Img = Environment.CurrentDirectory + "\\ImgPokes\\wartortle.jpg";
                    Thread.Sleep(3000);
                    Console.Clear();
                    Console.WriteLine("-----------------------------------------------");
                    Console.WriteLine("PARABÉNS SEU SQUIRTLE EVOLUIU PARA WARTORTLE!!!");
                    Console.WriteLine("-----------------------------------------------");
                    ExibirImagem(pPlayer.Img, 60, 60);
                }
                else if (pPlayer.Nome == "BULBASSAURO")
                {
                    //Deleta o arquivo antigo do pokemon pré evolução
                    string dir = Environment.CurrentDirectory + "\\PokeCares\\";
                    file = dir + pPlayer.Nickname.ToUpper() + player.Nome.ToUpper() + ".txt";
                    File.Delete(file);

                    Console.WriteLine("Estou evoluindo!!!");
                    pPlayer.Nome = "IVYSSAURO";
                    pPlayer.Img = Environment.CurrentDirectory + "\\ImgPokes\\ivyssauro.jpg";
                    Thread.Sleep(3000);
                    Console.Clear();
                    Console.WriteLine("--------------------------------------------------");
                    Console.WriteLine("PARABÉNS SEU BULBASSAURO EVOLUIU PARA IVYSSAURO!!!");
                    Console.WriteLine("--------------------------------------------------");
                    ExibirImagem(pPlayer.Img, 60, 60);
                }
                else if (pPlayer.Nome == "PICHU")
                {
                    string dir = Environment.CurrentDirectory + "\\PokeCares\\";
                    file = dir + pPlayer.Nickname.ToUpper() + player.Nome.ToUpper() + ".txt";
                    File.Delete(file);

                    Console.WriteLine("Estou evoluindo!!!");
                    pPlayer.Nome = "PIKACHU";
                    pPlayer.Img = Environment.CurrentDirectory + "\\ImgPokes\\pikachu.jpg";
                    Thread.Sleep(3000);
                    Console.Clear();
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("PARABÉNS SEU PICHU EVOLUIU PARA PIKACHU!!!");
                    Console.WriteLine("------------------------------------------");
                    ExibirImagem(pPlayer.Img, 60, 60);
                }
                else if (pPlayer.Nome == "GEODUDE")
                {
                    string dir = Environment.CurrentDirectory + "\\PokeCares\\";
                    file = dir + pPlayer.Nickname.ToUpper() + player.Nome.ToUpper() + ".txt";
                    File.Delete(file);

                    Console.WriteLine("Estou evoluindo!!!");
                    pPlayer.Nome = "GRAVELER";
                    pPlayer.Img = Environment.CurrentDirectory + "\\ImgPokes\\graveler.jpg";
                    Thread.Sleep(3000);
                    Console.Clear();
                    Console.WriteLine("---------------------------------------------");
                    Console.WriteLine("PARABÉNS SEU GEODUDE EVOLUIU PARA GRAVELER!!!");
                    Console.WriteLine("---------------------------------------------");
                    ExibirImagem(pPlayer.Img, 60, 60);
                }
                else if (pPlayer.Nome == "ABRA")
                {
                    string dir = Environment.CurrentDirectory + "\\PokeCares\\";
                    file = dir + pPlayer.Nickname.ToUpper() + player.Nome.ToUpper() + ".txt";
                    File.Delete(file);

                    Console.WriteLine("Estou evoluindo!!!");
                    pPlayer.Nome = "KADABRA";
                    pPlayer.Img = Environment.CurrentDirectory + "\\ImgPokes\\kadabra.jpg";
                    Thread.Sleep(3000);
                    Console.Clear();
                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("PARABÉNS SEU ABRA EVOLUIU PARA KADABRA!!! ");
                    Console.WriteLine("------------------------------------------");
                    ExibirImagem(pPlayer.Img, 60, 60);
                }
                else if (pPlayer.Nome == "GASTLY")
                {
                    string dir = Environment.CurrentDirectory + "\\PokeCares\\";
                    file = dir + pPlayer.Nickname.ToUpper() + player.Nome.ToUpper() + ".txt";
                    File.Delete(file);

                    Console.WriteLine("Estou evoluindo!!!");
                    pPlayer.Nome = "HAUNTER";
                    pPlayer.Img = Environment.CurrentDirectory + "\\ImgPokes\\haunter.jpg";
                    Thread.Sleep(3000);
                    Console.Clear();
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine("PARABÉNS SEU GASTLY EVOLUIU PARA HAUNTER!!!");
                    Console.WriteLine("-------------------------------------------");
                    ExibirImagem(pPlayer.Img, 60, 60);
                }
                else if (pPlayer.Nome == "DRATINI")
                {
                    string dir = Environment.CurrentDirectory + "\\PokeCares\\";
                    file = dir + pPlayer.Nickname.ToUpper() + player.Nome.ToUpper() + ".txt";
                    File.Delete(file);

                    Console.WriteLine("Estou evoluindo!!!");
                    pPlayer.Nome = "DRAGONAIR";
                    pPlayer.Img = Environment.CurrentDirectory + "\\ImgPokes\\dragonair.jpg";
                    Thread.Sleep(3000);
                    Console.Clear();
                    Console.WriteLine("----------------------------------------------");
                    Console.WriteLine("PARABÉNS SEU DRATINI EVOLUIU PARA DRAGONAIR!!!");
                    Console.WriteLine("----------------------------------------------");
                    ExibirImagem(pPlayer.Img, 60, 60);
                }
            }
            
            else if (pPlayer.Nivel >= 32 && pPlayer.Nivel < 100)
            {
                
                if (pPlayer.Nome == "CHARMELEON")
                {
                    string dir = Environment.CurrentDirectory + "\\PokeCares\\";
                    file = dir + pPlayer.Nickname.ToUpper() + player.Nome.ToUpper() + ".txt";
                    File.Delete(file);

                    Console.WriteLine("Estou evoluindo!!!");
                    pPlayer.Nome = "CHARIZARD";
                    pPlayer.Img = Environment.CurrentDirectory + "\\ImgPokes\\charizard.jpg";
                    Thread.Sleep(3000);
                    Console.Clear();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("PARABÉNS SEU CHARMELEON EVOLUIU PARA CHARIZARD!!!");
                    Console.WriteLine("-------------------------------------------------");
                    ExibirImagem(pPlayer.Img, 60, 60);
                }
                else if (pPlayer.Nome == "WARTORTLE")
                {
                    string dir = Environment.CurrentDirectory + "\\PokeCares\\";
                    file = dir + pPlayer.Nickname.ToUpper() + player.Nome.ToUpper() + ".txt";
                    File.Delete(file);

                    Console.WriteLine("Estou evoluindo!!!");
                    pPlayer.Nome = "BLASTOISE";
                    pPlayer.Img = Environment.CurrentDirectory + "\\ImgPokes\\blastoise.jpg";
                    Thread.Sleep(3000);
                    Console.Clear();
                    Console.WriteLine("------------------------------------------------");
                    Console.WriteLine("PARABÉNS SEU WARTORTLE EVOLUIU PARA BLASTOISE!!!");
                    Console.WriteLine("------------------------------------------------");
                    ExibirImagem(pPlayer.Img, 60, 60);
                }
                else if (pPlayer.Nome == "IVYSSAURO")
                {
                    string dir = Environment.CurrentDirectory + "\\PokeCares\\";
                    file = dir + pPlayer.Nickname.ToUpper() + player.Nome.ToUpper() + ".txt";
                    File.Delete(file);

                    Console.WriteLine("Estou evoluindo!!!");
                    pPlayer.Nome = "VENOSSAURO";
                    pPlayer.Img = Environment.CurrentDirectory + "\\ImgPokes\\venossauro.jpg";
                    Thread.Sleep(3000);
                    Console.Clear();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("PARABÉNS SEU IVYSSAURO EVOLUIU PARA VENOSSAURO!!!");
                    Console.WriteLine("-------------------------------------------------");
                    ExibirImagem(pPlayer.Img, 60, 60);
                }
                else if (pPlayer.Nome == "PIKACHU")
                {
                    string dir = Environment.CurrentDirectory + "\\PokeCares\\";
                    file = dir + pPlayer.Nickname.ToUpper() + player.Nome.ToUpper() + ".txt";
                    File.Delete(file);

                    Console.WriteLine("Estou evoluindo!!!");
                    pPlayer.Nome = "RAICHU";
                    pPlayer.Img = Environment.CurrentDirectory + "\\ImgPokes\\raichu.jpg";
                    Thread.Sleep(3000);
                    Console.Clear();
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine("PARABÉNS SEU PIKACHU EVOLUIU PARA RAICHU!!!");
                    Console.WriteLine("-------------------------------------------");
                    ExibirImagem(pPlayer.Img, 60, 60);
                }
                else if (pPlayer.Nome == "GRAVELER")
                {
                    string dir = Environment.CurrentDirectory + "\\PokeCares\\";
                    file = dir + pPlayer.Nickname.ToUpper() + player.Nome.ToUpper() + ".txt";
                    File.Delete(file);

                    Console.WriteLine("Estou evoluindo!!!");
                    pPlayer.Nome = "GOLEM";
                    pPlayer.Img = Environment.CurrentDirectory + "\\ImgPokes\\golem.jpg";
                    Thread.Sleep(3000);
                    Console.Clear();
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine("PARABÉNS SEU GRAVELER EVOLUIU PARA GOLEM!!!");
                    Console.WriteLine("-------------------------------------------");
                    ExibirImagem(pPlayer.Img, 60, 60);
                }
                else if (pPlayer.Nome == "HAUNTER")
                {
                    string dir = Environment.CurrentDirectory + "\\PokeCares\\";
                    file = dir + pPlayer.Nickname.ToUpper() + player.Nome.ToUpper() + ".txt";
                    File.Delete(file);

                    Console.WriteLine("Estou evoluindo!!!");
                    pPlayer.Nome = "GENGAR";
                    pPlayer.Img = Environment.CurrentDirectory + "\\ImgPokes\\gengar.jpg";
                    Thread.Sleep(3000);
                    Console.Clear();
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine("PARABÉNS SEU HAUNTER EVOLUIU PARA GENGAR!!!");
                    Console.WriteLine("-------------------------------------------");
                    ExibirImagem(pPlayer.Img, 60, 60);
                }
                else if (pPlayer.Nome == "DRAGONAIR")
                {
                    string dir = Environment.CurrentDirectory + "\\PokeCares\\";
                    file = dir + pPlayer.Nickname.ToUpper() + player.Nome.ToUpper() + ".txt";
                    File.Delete(file);

                    Console.WriteLine("Estou evoluindo!!!");
                    pPlayer.Nome = "DRAGONITE";
                    pPlayer.Img = Environment.CurrentDirectory + "\\ImgPokes\\dragonite.jpg";
                    Thread.Sleep(3000);
                    Console.Clear();
                    Console.WriteLine("------------------------------------------------");
                    Console.WriteLine("PARABÉNS SEU DRAGONAIR EVOLUIU PARA DRAGONITE!!!");
                    Console.WriteLine("------------------------------------------------");
                    ExibirImagem(pPlayer.Img, 60, 60);
                }
            }

            else if (pPlayer.Nivel >= 100)
            {
                pPlayer.Nivel = 100;
                if (pPlayer.Nome == "CHARMELEON")
                {
                    string dir = Environment.CurrentDirectory + "\\PokeCares\\";
                    file = dir + pPlayer.Nickname.ToUpper() + player.Nome.ToUpper() + ".txt";
                    File.Delete(file);

                    Console.WriteLine("Estou evoluindo!!!");
                    pPlayer.Nome = "CHARIZARD";
                    pPlayer.Img = Environment.CurrentDirectory + "\\ImgPokes\\charizard.jpg";
                    Thread.Sleep(3000);
                    Console.Clear();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("PARABÉNS SEU CHARMELEON EVOLUIU PARA CHARIZARD!!!");
                    Console.WriteLine("-------------------------------------------------");
                    ExibirImagem(pPlayer.Img, 60, 60);
                    Console.WriteLine("---------------------------------------------------------------------");
                    Console.WriteLine("Parabens!!! Seu Pokemon atingiu o nível máximo!!!");
                    Console.WriteLine("---------------------------------------------------------------------");
                }
                else if (pPlayer.Nome == "WARTORTLE")
                {
                    string dir = Environment.CurrentDirectory + "\\PokeCares\\";
                    file = dir + pPlayer.Nickname.ToUpper() + player.Nome.ToUpper() + ".txt";
                    File.Delete(file);

                    Console.WriteLine("Estou evoluindo!!!");
                    pPlayer.Nome = "BLASTOISE";
                    pPlayer.Img = Environment.CurrentDirectory + "\\ImgPokes\\blastoise.jpg";
                    Thread.Sleep(3000);
                    Console.Clear();
                    Console.WriteLine("------------------------------------------------");
                    Console.WriteLine("PARABÉNS SEU WARTORTLE EVOLUIU PARA BLASTOISE!!!");
                    Console.WriteLine("------------------------------------------------");
                    ExibirImagem(pPlayer.Img, 60, 60);
                }
                else if (pPlayer.Nome == "IVYSSAURO")
                {
                    string dir = Environment.CurrentDirectory + "\\PokeCares\\";
                    file = dir + pPlayer.Nickname.ToUpper() + player.Nome.ToUpper() + ".txt";
                    File.Delete(file);

                    Console.WriteLine("Estou evoluindo!!!");
                    pPlayer.Nome = "VENOSSAURO";
                    pPlayer.Img = Environment.CurrentDirectory + "\\ImgPokes\\venossauro.jpg";
                    Thread.Sleep(3000);
                    Console.Clear();
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("PARABÉNS SEU IVYSSAURO EVOLUIU PARA VENOSSAURO!!!");
                    Console.WriteLine("-------------------------------------------------");
                    ExibirImagem(pPlayer.Img, 60, 60);
                }
                else if (pPlayer.Nome == "PIKACHU")
                {
                    string dir = Environment.CurrentDirectory + "\\PokeCares\\";
                    file = dir + pPlayer.Nickname.ToUpper() + player.Nome.ToUpper() + ".txt";
                    File.Delete(file);

                    Console.WriteLine("Estou evoluindo!!!");
                    pPlayer.Nome = "RAICHU";
                    pPlayer.Img = Environment.CurrentDirectory + "\\ImgPokes\\raichu.jpg";
                    Thread.Sleep(3000);
                    Console.Clear();
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine("PARABÉNS SEU PIKACHU EVOLUIU PARA RAICHU!!!");
                    Console.WriteLine("-------------------------------------------");
                    ExibirImagem(pPlayer.Img, 60, 60);
                }
                else if (pPlayer.Nome == "GRAVELER")
                {
                    string dir = Environment.CurrentDirectory + "\\PokeCares\\";
                    file = dir + pPlayer.Nickname.ToUpper() + player.Nome.ToUpper() + ".txt";
                    File.Delete(file);

                    Console.WriteLine("Estou evoluindo!!!");
                    pPlayer.Nome = "GOLEM";
                    pPlayer.Img = Environment.CurrentDirectory + "\\ImgPokes\\golem.jpg";
                    Thread.Sleep(3000);
                    Console.Clear();
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine("PARABÉNS SEU GRAVELER EVOLUIU PARA GOLEM!!!");
                    Console.WriteLine("-------------------------------------------");
                    ExibirImagem(pPlayer.Img, 60, 60);
                }
                else if (pPlayer.Nome == "HAUNTER")
                {
                    string dir = Environment.CurrentDirectory + "\\PokeCares\\";
                    file = dir + pPlayer.Nickname.ToUpper() + player.Nome.ToUpper() + ".txt";
                    File.Delete(file);

                    Console.WriteLine("Estou evoluindo!!!");
                    pPlayer.Nome = "GENGAR";
                    pPlayer.Img = Environment.CurrentDirectory + "\\ImgPokes\\gengar.jpg";
                    Thread.Sleep(3000);
                    Console.Clear();
                    Console.WriteLine("-------------------------------------------");
                    Console.WriteLine("PARABÉNS SEU HAUNTER EVOLUIU PARA GENGAR!!!");
                    Console.WriteLine("-------------------------------------------");
                    ExibirImagem(pPlayer.Img, 60, 60);
                    Console.WriteLine("---------------------------------------------------------------------");
                    Console.WriteLine("Parabens!!! Seu Pokemon atingiu o nível máximo!!!");
                    Console.WriteLine("---------------------------------------------------------------------");
                }
                else if (pPlayer.Nome == "DRAGONAIR")
                {
                    string dir = Environment.CurrentDirectory + "\\PokeCares\\";
                    file = dir + pPlayer.Nickname.ToUpper() + player.Nome.ToUpper() + ".txt";
                    File.Delete(file);

                    Console.WriteLine("Estou evoluindo!!!");
                    pPlayer.Nome = "DRAGONITE";
                    pPlayer.Img = Environment.CurrentDirectory + "\\ImgPokes\\dragonite.jpg";
                    Thread.Sleep(3000);
                    Console.Clear();
                    Console.WriteLine("------------------------------------------------");
                    Console.WriteLine("PARABÉNS SEU DRAGONAIR EVOLUIU PARA DRAGONITE!!!");
                    Console.WriteLine("------------------------------------------------");
                    ExibirImagem(pPlayer.Img, 60, 60);
                    Console.WriteLine("---------------------------------------------------------------------");
                    Console.WriteLine("Parabens!!! Seu Pokemon atingiu o nível máximo!!!");
                    Console.WriteLine("---------------------------------------------------------------------");
                }

            }

            Console.WriteLine("------------------------------------------------------------------------");
            Console.WriteLine("Aperte <qualquer tecla> para voltar ao menu ou <ESC> para fechar o jogo.");
            Console.WriteLine("------------------------------------------------------------------------");

        }

        //Pergunta do pokemon para que se possa cuidar dele
        static void PergPoke(ref Pokemon pPlayer)
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("O que vamos fazer hoje?");
            Console.Write("Comer | Banho | Brincar | Nada:");
            string entrada = Console.ReadLine().ToLower();//entrada de dados
            switch (entrada)
            {
                case "comer": pPlayer.Fome += 20; break;
                case "banho": pPlayer.Higiene += 20; break;
                case "brincar": pPlayer.Felicidade += 20; break;
            }

            if (pPlayer.Fome > 100) pPlayer.Fome = 100;
            if (pPlayer.Higiene > 100) pPlayer.Higiene = 100;
            if (pPlayer.Felicidade > 100) pPlayer.Felicidade = 100;

            if (entrada == "comer")
            {
                Console.Clear();
                Console.WriteLine("-----------------------------------------------------------------------------------------------");
                Console.WriteLine("Ai sim, comer é tudo de bom!!!");
                Console.WriteLine("-----------------------------------------------------------------------------------------------");
                Thread.Sleep(2600);
                Console.WriteLine("-----------------------------------------------------------------------------------------------");
                Console.WriteLine("Para continuar cuidando do seu pokemon aperte <qualquer tecla> para voltar ao menu aperte <ESC>");
                Console.WriteLine("-----------------------------------------------------------------------------------------------");
            }
            else if (entrada == "banho")
            {
                Console.Clear();
                Console.WriteLine("-----------------------------------------------------------------------------------------------");
                Console.WriteLine("Agora sim, que banho relaxante!!!");
                Console.WriteLine("-----------------------------------------------------------------------------------------------");
                Thread.Sleep(2600);
                Console.WriteLine("-----------------------------------------------------------------------------------------------");
                Console.WriteLine("Para continuar cuidando do seu pokemon aperte <qualquer tecla> para voltar ao menu aperte <ESC>");
                Console.WriteLine("-----------------------------------------------------------------------------------------------");
            }
            else if (entrada == "brincar")
            {
                Console.Clear();
                Console.WriteLine("-----------------------------------------------------------------------------------------------");
                Console.WriteLine("Eba!!! Brincar com você é muito legal!!!");
                Console.WriteLine("-----------------------------------------------------------------------------------------------");
                Thread.Sleep(2600);
                Console.WriteLine("-----------------------------------------------------------------------------------------------");
                Console.WriteLine("Para continuar cuidando do seu pokemon aperte <qualquer tecla> para voltar ao menu aperte <ESC>");
                Console.WriteLine("-----------------------------------------------------------------------------------------------");
            }
            else if (entrada == "nada")
            {
                Console.WriteLine("-----------------------------------------------------------------------------------------------");
                Console.WriteLine("Para continuar cuidando do seu pokemon aperte <qualquer tecla> para voltar ao menu aperte <ESC>");
                Console.WriteLine("-----------------------------------------------------------------------------------------------");
            }
            else if (entrada == "")
            {
                Console.WriteLine("-----------------------------------------------------------------------------------------------");
                Console.WriteLine("Para continuar cuidando do seu pokemon aperte <qualquer tecla> para voltar ao menu aperte <ESC>");
                Console.WriteLine("-----------------------------------------------------------------------------------------------");
            }
        }

        //Recebe os dados inseridos pelo usuário para armazenar o nome do pokemon e o nome do jogador
        static void InputDados(ref Player player, ref Pokemon pPlayer)
        {
            //Entrada de dados - Nomes
                Console.WriteLine("-----------------------------");
                Console.WriteLine("Qual é o nome do seu Pokemon?");
                Console.Write("Digite:");
                pPlayer.Nickname = Console.ReadLine();
                if (pPlayer.Nickname == "")
                {
                   pPlayer.Nickname += pPlayer.Nome;
                }
                Console.WriteLine("--------------------------");
                Console.WriteLine("Qual é o nome do meu dono?");
                Console.Write("Digite:");
                player.Nome = Console.ReadLine().ToUpper();
                if (player.Nome == "")
                {
                    player.Nome = "USER";
                }         
        }

        //Usado para que o usuário escolha o pokemon que quer treinar e cuidar
        static void EscolhaPokemon(ref Pokedex pokedex,ref Pokemon pPlayer)
        {
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("Para visualizar melhor o seu pokemon, use o programa em tela cheia!!!");
            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine("De qual Pokemon você gostaria de cuidar?");
            Console.WriteLine("---------------------------------------------------------------------");
            pokedex.ListarPokemons();
            Console.Write("Digite o código do Pokemon escolhido:");
            int codigo = Convert.ToInt32(Console.ReadLine());
                //pegar o pokemon do player
                pPlayer = pokedex.Pokemons[codigo];
                Console.Clear();
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine("PARABÉNS VOCÊ ESCOLHEU O " + pPlayer.Nome + "!!!");
                Console.WriteLine("------------------------------------------------");
                ExibirImagem(pPlayer.Img, 60, 60);
        }

        //inicio da imagem
        static Bitmap ResizeImage(Bitmap image, int width, int height)
        {
            // Cria um novo Bitmap com a largura e altura desejadas
            Bitmap resizedImage = new Bitmap(width, height);

            // Desenha a imagem original no novo Bitmap usando as dimensões desejadas
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.DrawImage(image, 0, 0, width, height);
            }

            return resizedImage;
        }

        static string ConvertToAscii(Bitmap image)
        {
            // Caracteres ASCII usados para representar a imagem
            char[] asciiChars = { ' ', '.', ':', '-', '=', '+', '*', '#', '%', '@' };

            StringBuilder asciiArt = new StringBuilder();

            // Percorre os pixels da imagem e converte cada um em um caractere ASCII correspondente
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    int grayScale = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    int asciiIndex = grayScale * (asciiChars.Length - 1) / 255;
                    char asciiChar = asciiChars[asciiIndex];
                    asciiArt.Append(asciiChar);
                }
                asciiArt.Append(Environment.NewLine);
            }

            return asciiArt.ToString();
        }

        static void ExibirImagem(string imagePath, int width, int height)
        {
            // Caminho para a imagem que deseja exibir
            //string imagePath = @"C:\Users\Danilo Filitto\Downloads\Panda.jpg";

            // Carrega a imagem
            Bitmap image = new Bitmap(imagePath);

            // Redimensiona a imagem para a largura e altura desejadas
            int consoleWidth = width;
            int consoleHeight = height;
            Bitmap resizedImage = ResizeImage(image, consoleWidth, consoleHeight);

            // Converte a imagem em texto ASCII
            string asciiArt = ConvertToAscii(resizedImage);

            // Exibe o texto ASCII no console
            Console.WriteLine(asciiArt);
        }
        //fim da imagem
    }
}
