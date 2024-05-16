
namespace Player.Estado
{
    public class Fabrica
    {
        private Maquina contexto;

        //Construtor
        public Fabrica(Maquina _contextoAtual)
        {
            contexto = _contextoAtual;
        }

        public EstadoBase NoChao()
        {
            return new NoChao(contexto, this);
        }

        public EstadoBase Andando()
        {
            return new Andando(contexto, this);
        }

        public EstadoBase Flutuar()
        {
            return new Flutuar(contexto, this);
        }

        public EstadoBase Pulo()
        {
            return new Pulo(contexto, this);
        }

        public EstadoBase Parado()
        {
            return new Parado(contexto, this);
        }

        public EstadoBase Caindo()
        {
            return new Caindo(contexto, this);
        }

    }
}

