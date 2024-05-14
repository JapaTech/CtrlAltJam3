using UnityEngine;
using Player.Estado;

namespace Player
{
    public class Maquina : MonoBehaviour
    {
        //Componente
        private Rigidbody2D rb;
        private Transform tr;
        private Animator anim;

        //Movimento no chão
        [SerializeField] private float velMovimento = 5f;
        private Vector2 CalculaMovimentos;
        private Vector3 OlhaParaDirecao;
        private bool viradoParaDireita;

        //Pulo
        [SerializeField] private float velPulo;
        [SerializeField] private float duracaoPulo;
        [SerializeField] private Transform[] pontosChecagemSolo = new Transform[2];
        [SerializeField] private LayerMask solo_lm;
        private RaycastHit2D hitNoChao_ray;
        private bool estaNoChao;
        private bool pediuPular;

        private EstadoBase estadoAtual;
        private Fabrica fabrica;


        //Expor Variáveis
        public EstadoBase EstadoAtual { get { return estadoAtual; } set { estadoAtual = value; } }

        void Start()
        {

        }

        void Update()
        {

        }
    }

}
