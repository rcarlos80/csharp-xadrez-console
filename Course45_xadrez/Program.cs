using System;
using tabuleiro;
using xadrez;

namespace Course45_xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            try {

                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.encerrada) {
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.tab);

                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                    Console.Write("Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();

                    partida.ExecutarMovimento(origem, destino);
                }




                /*
                PosicaoXadrez pos = new PosicaoXadrez('c', 7);
                Console.WriteLine(pos);
                Console.WriteLine(pos.ToPosicao());
                 */

                /*
                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.ColocarPeca(new Torre(tab, Cor.Preto), new Posicao(0, 0));


                Tela.ImprimirTabuleiro(tab);

                */
                //Console.WriteLine(pos);
            }
            catch (TabuleiroException e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}
