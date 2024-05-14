
namespace Player.Estado
{
    public abstract class EstadoBase
    {
        protected bool estadoRaiz = false;

        protected Maquina ctx;

        protected Fabrica fabrica;

        protected EstadoBase superEstadoAtual;

        protected EstadoBase subEstadoAtual;

        public EstadoBase(Maquina _contextoAtual, Fabrica _fabrica)
        {
            ctx = _contextoAtual;
            fabrica = _fabrica;
        }


        public abstract void InicializaEstado();
        public abstract void AtualizaEstado();
        public abstract void FinalizaEstado();
        public abstract void ChecaTrocaDeEstado();
        public abstract void InicializaSubestado();


        public void UpdateEstados()
        {
            AtualizaEstado();
            if (subEstadoAtual != null)
            {
                subEstadoAtual.UpdateEstados();
            }
        }

        protected void TrocaEstados(EstadoBase novoEstado)
        {
            FinalizaEstado();

            novoEstado.InicializaEstado();

            if (estadoRaiz)
                ctx.EstadoAtual = novoEstado;
            else if (superEstadoAtual != null)
                superEstadoAtual.DefinaSubestado(novoEstado);

        }

        protected void DefinaSuperestado(EstadoBase novoSuperestado)
        {
            superEstadoAtual = novoSuperestado;
        }

        protected void DefinaSubestado(EstadoBase novoSubestado)
        {
            subEstadoAtual = novoSubestado;
            novoSubestado.DefinaSuperestado(this);
        }
    }
}

