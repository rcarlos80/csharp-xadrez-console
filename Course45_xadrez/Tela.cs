﻿using System;
using tabuleiro;

namespace Course45_xadrez
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++) {
                Console.Write((8 - i) + " ");
                for (int j = 0; j < tab.Colunas; j++) {
                    if (tab.Peca(i, j) == null) {
                        Console.Write("- ");
                    }
                    else {
                        Tela.ImprimirPeca(tab.Peca(i, j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void ImprimirPeca(Peca peca)
        {
            ConsoleColor aux = Console.ForegroundColor;
            if (peca.Cor == Cor.Branco) {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            Console.Write(peca);
            Console.ForegroundColor = aux;
        }
    }
}
