namespace Player.Estado
{
    public class Andando : EstadoBase
    {
        public Andando(Maquina _contextoAtual, Fabrica _fabrica) : base(_contextoAtual, _fabrica)
        {
        }


        public override void InicializaEstado()
        {
        }

        public override void AtualizaEstado()
        {
            ChecaTrocaDeEstado();

            ctx.CalculoMovimentosX = ctx.InputMovimentos.x * ctx.VelMovimento;
        }

        public override void ChecaTrocaDeEstado()
        {
            if(ctx.CalculoMovimentosX == 0)
            {
                TrocaEstados(fabrica.Parado());
            }
        }

        public override void FinalizaEstado()
        {
        }

        public override void InicializaSubestado()
        {
        }
    }
}