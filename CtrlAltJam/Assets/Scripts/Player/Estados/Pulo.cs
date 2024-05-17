namespace Player.Estado
{
    public class Pulo : EstadoBase
    {

        public Pulo(Maquina _contextoAtual, Fabrica _fabrica) : base(_contextoAtual, _fabrica)
        {
            estadoRaiz = true;
            InicializaSubestado();
        }

        public override void InicializaEstado()
        {
            ctx.StartCoroutine("EstaNoPulo");
            ctx.CalculoMovimentosY = ctx.AlturaPulo;
        }

        public override void AtualizaEstado()
        {
            ChecaTrocaDeEstado();
        }

        public override void ChecaTrocaDeEstado()
        {
            if (ctx.EstaNoChao == true && !ctx.EstaPulando)
            {
                TrocaEstados(fabrica.NoChao());
            }
            else if (ctx.EstaPulando == false && ctx.EstaNoChao == false)
            {
                TrocaEstados(fabrica.Caindo());
            }
            else if (ctx.PodeFlutuar && ctx.Flutuar)
            {
                TrocaEstados(fabrica.Flutuar());
            }
        }

        public override void FinalizaEstado()
        {
            
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