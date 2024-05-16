namespace Player.Estado
{
    public class Caindo : EstadoBase
    {
        public Caindo(Maquina _contextoAtual, Fabrica _fabrica) : base(_contextoAtual, _fabrica)
        {
            estadoRaiz = true;
            InicializaSubestado();
        }

        public override void InicializaEstado()
        {
 
        }

        public override void AtualizaEstado()
        {
            ctx.CalculoMovimentosY = ctx.Rb.velocity.y;
            ChecaTrocaDeEstado();
        }

        public override void ChecaTrocaDeEstado()
        {
            if (ctx.EstaNoChao)
            {
                TrocaEstados(fabrica.NoChao());
            }
        }

        public override void FinalizaEstado()
        {
            ctx.CalculoMovimentosY = 0;
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