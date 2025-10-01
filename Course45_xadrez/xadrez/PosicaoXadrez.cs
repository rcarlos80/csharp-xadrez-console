using System;
using tabuleiro;

namespace xadrez
{
    class PosicaoXadrez
    {
        public char Coluna { get; set; } //A,B,C,D,E,F,G,H...
        public int Linha { get; set; } //1,2,3,4,5,6,7,8...

        public PosicaoXadrez(char coluna, int linha)
        {
            Coluna = coluna;
            Linha = linha;
        }

        public Posicao ToPosicao()
        {
            return new Posicao(8 - Linha, Coluna - 'a'); //internamente 'a' é um nº inteiro
        }

        public override string ToString()
        {
            return "" + Coluna + Linha;
        }
    }
}
