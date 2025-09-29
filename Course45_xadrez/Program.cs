using System;
using tabuleiro;

namespace Course45_xadrez
{
    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8, 8);

            Tela.ImprimirTabuleiro(tab);

            //Console.WriteLine(pos);
        }
    }
}
