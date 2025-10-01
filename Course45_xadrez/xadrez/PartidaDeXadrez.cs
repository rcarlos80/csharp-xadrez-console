using System;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        private int turno;
        private Cor jogadorAtual;
        public bool encerrada { get; private set;}

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branco;
            ColocarPecas();
        }

        public void ExecutarMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPeca(origem);
            p.IncrementarQteMovimentos();
            Peca pecaCapturada = tab.RetirarPeca(destino); //futuramente
            tab.ColocarPeca(p, destino);
        }

        public void ColocarPecas()
        {
            tab.ColocarPeca(new Torre(tab, Cor.Branco), new PosicaoXadrez('c', 1).ToPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Branco), new PosicaoXadrez('c', 2).ToPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Branco), new PosicaoXadrez('d', 2).ToPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Branco), new PosicaoXadrez('e', 1).ToPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Branco), new PosicaoXadrez('e', 2).ToPosicao());
            tab.ColocarPeca(new Rei(tab, Cor.Branco), new PosicaoXadrez('d', 1).ToPosicao());

            tab.ColocarPeca(new Torre(tab, Cor.Preto), new PosicaoXadrez('c', 7).ToPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Preto), new PosicaoXadrez('c', 8).ToPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Preto), new PosicaoXadrez('d', 7).ToPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Preto), new PosicaoXadrez('e', 7).ToPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Preto), new PosicaoXadrez('e', 8).ToPosicao());
            tab.ColocarPeca(new Rei(tab, Cor.Preto), new PosicaoXadrez('d', 8).ToPosicao());
        }

    }
}
