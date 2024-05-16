namespace Player.Estado
{
    public class NoChao : EstadoBase
    {
        public NoChao(Maquina _contextoAtual, Fabrica _fabrica) : base(_contextoAtual, _fabrica)
        {
            estadoRaiz = true;
            InicializaSubestado();
        }

        public override void InicializaEstado()
        {
         
        }

        public override void AtualizaEstado()
        {
            ctx.CalculoMovimentosY = 0;
            ChecaTrocaDeEstado();
        }

        public override void ChecaTrocaDeEstado()
        {
            
            if (ctx.PediuPulo && ctx.EstaNoChao)
            {
                TrocaEstados(fabrica.Pulo());
            }
            else if (ctx.Flutuar)
            {
                TrocaEstados(fabrica.Flutuar());
            }
            if (!ctx.EstaNoChao)
            {
                TrocaEstados(fabrica.Caindo());
            }
        }

        public override void FinalizaEstado()
        {
        }

        public override void InicializaSubestado()
        {
            if(ctx.InputMovimentos.x != 0)
            {
                DefinaSubestado(fabrica.Andando());
            }
            else if (ctx.InputMovimentos.x == 0)
            {
                DefinaSubestado(fabrica.Parado());
            }
        }
    }
}