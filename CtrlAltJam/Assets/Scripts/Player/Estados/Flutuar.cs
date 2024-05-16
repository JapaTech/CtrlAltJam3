namespace Player.Estado
{
    public class Flutuar : EstadoBase
    {
        public Flutuar(Maquina _contextoAtual, Fabrica _fabrica) : base(_contextoAtual, _fabrica)
        {
            estadoRaiz = true;
            InicializaSubestado();
        }

        public override void InicializaEstado()
        {
            ctx.Rb.gravityScale = 0;
        }

        public override void AtualizaEstado()
        {
            ctx.CalculoMovimentosY = ctx.InputMovimentos.y * ctx.VelParaFlutuar;
            ChecaTrocaDeEstado();
        }

        public override void ChecaTrocaDeEstado()
        {
            if(ctx.EstaNoChao && !ctx.PodeFlutuar)
            {
                TrocaEstados(fabrica.NoChao());
            }
            if(!ctx.EstaNoChao && !ctx.PodeFlutuar)
            {
                TrocaEstados(fabrica.Caindo());
            }
        }

        public override void FinalizaEstado()
        {
            ctx.Rb.gravityScale = ctx.GravidadeInicial;
            ctx.PediuPulo = false;
        }

        public override void InicializaSubestado()
        {
            if (ctx.InputMovimentos.x != 0)
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