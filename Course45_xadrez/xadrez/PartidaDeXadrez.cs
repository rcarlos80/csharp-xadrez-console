using System;
using tabuleiro;
using System.Collections.Generic;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Encerrada { get; private set; }
        public bool Xeque { get; private set; }

        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;


        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branco;
            Encerrada = false;
            Xeque = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarQteMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
            if (pecaCapturada != null) {
                capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.RetirarPeca(destino);
            p.DecrementarQteMovimentos();
            if (pecaCapturada != null) {
                Tab.ColocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            Tab.ColocarPeca(p, origem);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);
            if (EstaEmCheque(JogadorAtual)) {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            if (EstaEmCheque(Adversaria(JogadorAtual))) {
                Xeque = true;
            }
            else {
                Xeque = false;
            }

            if (EstaEmCheque(Adversaria(JogadorAtual))) {
                Encerrada = true;
            }
            else {

                Turno++;
                MudaJogador();
            }
        }

        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if (Tab.Peca(pos) == null) {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (JogadorAtual != Tab.Peca(pos).Cor) {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!Tab.Peca(pos).ExisteMovimentosPossiveis()) {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.Peca(origem).MovimentoPossivel(destino)) {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }

        private void MudaJogador()
        {
            if (JogadorAtual == Cor.Branco) {
                JogadorAtual = Cor.Preto;
            }
            else {
                JogadorAtual = Cor.Branco;
            }
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturadas) {
                if (x.Cor == cor) {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas) {
                if (x.Cor == cor) {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Branco) {
                return Cor.Preto;
            }
            else {
                return Cor.Branco;
            }
        }

        private Peca Rei(Cor cor) //testa se é o Rei
        {
            foreach (Peca x in PecasEmJogo(cor)) {
                if (x is Rei) {
                    return x;
                }
            }
            return null;
        }

        public bool EstaEmCheque(Cor cor) //testa se o Rei está em Xeque-Mate
        {
            Peca r = Rei(cor);
            if (r == null) {
                throw new TabuleiroException("Não há um rei da cor " + cor + " no tabuleiro!");
            }
            foreach (Peca x in PecasEmJogo(Adversaria(cor))) {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[r.Posicao.Linha, r.Posicao.Coluna]) {
                    return true;
                }
            }
            return false;
        }

        public bool TesteXequeMate(Cor cor) //testa se é possível o Rei sair do xeque-mate, movendo alguma peça
        {
            if (!EstaEmCheque(cor)) {
                return false;
            }
            foreach (Peca x in PecasEmJogo(cor)) {
                bool[,] mat = x.MovimentosPossiveis();
                for (int i = 0; i < Tab.Linhas; i++) {
                    for (int j = 0; j < Tab.Colunas; j++) {
                        if (mat[i, j]) {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmCheque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque) {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }


        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            pecas.Add(peca);
        }

        public void ColocarPecas()
        {
            ColocarNovaPeca('c', 1, new Torre(Tab, Cor.Branco));
            //ColocarNovaPeca('c', 2, new Torre(Tab, Cor.Branco));
            //ColocarNovaPeca('d', 2, new Torre(Tab, Cor.Branco));
            //ColocarNovaPeca('e', 1, new Torre(Tab, Cor.Branco));
            //ColocarNovaPeca('e', 2, new Torre(Tab, Cor.Branco));
            ColocarNovaPeca('d', 1, new Rei(Tab, Cor.Branco));
            ColocarNovaPeca('h', 7, new Torre(Tab, Cor.Branco));

            //ColocarNovaPeca('c', 7, new Torre(Tab, Cor.Preto));
            //ColocarNovaPeca('c', 8, new Torre(Tab, Cor.Preto));
            //ColocarNovaPeca('d', 7, new Torre(Tab, Cor.Preto));
            //ColocarNovaPeca('e', 7, new Torre(Tab, Cor.Preto));
            //ColocarNovaPeca('e', 8, new Torre(Tab, Cor.Preto));
            //ColocarNovaPeca('d', 8, new Rei(Tab, Cor.Preto));
            ColocarNovaPeca('a', 8, new Rei(Tab, Cor.Preto));
            ColocarNovaPeca('b', 8, new Torre(Tab, Cor.Preto));
        }

    }
}
