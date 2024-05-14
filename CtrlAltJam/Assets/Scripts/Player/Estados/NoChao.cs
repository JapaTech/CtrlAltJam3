namespace Player.Estado
{
    public class NoChao : EstadoBase
    {
        public NoChao(Maquina _contextoAtual, Fabrica _fabrica) : base(_contextoAtual, _fabrica)
        {
            estadoRaiz = true;
            InicializaSubestado();
        }

        public override void AtualizaEstado()
        {
            throw new System.NotImplementedException();
        }

        public override void ChecaTrocaDeEstado()
        {
            throw new System.NotImplementedException();
        }

        public override void FinalizaEstado()
        {
            throw new System.NotImplementedException();
        }

        public override void InicializaEstado()
        {
            throw new System.NotImplementedException();
        }

        public override void InicializaSubestado()
        {
            throw new System.NotImplementedException();
        }
    }
}