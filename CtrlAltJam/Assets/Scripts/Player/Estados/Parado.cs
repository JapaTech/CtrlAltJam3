namespace Player.Estado
{
    public class Parado : EstadoBase
    {
        public Parado(Maquina _contextoAtual, Fabrica _fabrica) : base(_contextoAtual, _fabrica)
        {
        }


        public override void InicializaEstado()
        {
            ctx.CalculoMovimentosX = 0;
        }

        public override void AtualizaEstado()
        {
            ChecaTrocaDeEstado();
        }

        public override void ChecaTrocaDeEstado()
        {
            if (ctx.InputMovimentos.x != 0)
            {
                TrocaEstados(fabrica.Andando());
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