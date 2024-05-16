using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using Player.Estado;

namespace Player
{
    public class Maquina : MonoBehaviour
    {
        //Componente
        private Rigidbody2D rb;
        private Transform tr;
        private Animator anim;
        private float gravidadeInicial;

        //Movimento no chão
        [SerializeField] private float velMovimento = 1.5f;
        private Vector2 calculaMovimentos;
        private Vector2 inputMovimentos;
        private Vector3 olhaParaDirecao;
        private bool viradoParaDireita;

        //Pulo
        [SerializeField] private float velocidadePulo;
        [SerializeField] private float duracaoPulo;
        [SerializeField] private Transform[] pontosChecagemSolo = new Transform[2];
        [SerializeField] private LayerMask solo_lm;
        private RaycastHit2D hitNoChao_ray;
        private bool estaPulando;
        private bool estaNoChao;
        private bool pediuPular;

        //Flutuar
        [SerializeField] private float velParaFlutuar;
        private bool flutuar;
        private bool podeFlutuar;

        //Inputs
        private PlayerInputsActions playerInput;

        //Máquina de estados
        private EstadoBase estadoAtual;
        private Fabrica fabrica;


        //Expor Variáveis
        public EstadoBase EstadoAtual { get { return estadoAtual; } set { estadoAtual = value; } }
        public Rigidbody2D Rb { get { return rb; } set { rb = value; } }
        public Transform Tr { get { return tr; } set { tr = value; } }
        public Vector2 InputMovimentos { get { return inputMovimentos; } }
        public float CalculoMovimentosX { get { return calculaMovimentos.x; } set { calculaMovimentos.x = value; } }
        public float CalculoMovimentosY { get { return calculaMovimentos.y; } set { calculaMovimentos.y = value; } }
        public float VelMovimento { get { return velMovimento; } }
        public float AlturaPulo { get { return velocidadePulo; } }
        public bool EstaNoChao { get { return estaNoChao; } }
        public bool PediuPulo { get { return pediuPular; } set { pediuPular = value; } }
        public bool EstaPulando { get { return estaPulando; } }
        public float VelParaFlutuar { get { return velParaFlutuar; } }
        public bool Flutuar { get { return flutuar; } set { flutuar = value; } }
        public float GravidadeInicial { get { return gravidadeInicial; } }
        public bool PodeFlutuar { get { return podeFlutuar; } }


        private void OnEnable()
        {
            playerInput.Player.Enable();
            playerInput.Player.Pulo.performed += Pulo_performed;
        }

        private void Pulo_performed(InputAction.CallbackContext obj)
        {
            if (estaNoChao)
            {
                pediuPular = true;
                rb.AddForce(Vector2.up * 100);
            }
        }

        private void Awake()
        {
            playerInput = new PlayerInputsActions();
            tr = transform;
            rb = GetComponent<Rigidbody2D>();


            fabrica = new Fabrica(this);
            estadoAtual = fabrica.NoChao();
            estadoAtual.InicializaEstado();
        }

        private void Start()
        {
            gravidadeInicial = rb.gravityScale;
        }

        void Update()
        {
            InputMovimento();
            InputFlutuar();
            Debug.Log(EstadoAtual);
        }

        private void FixedUpdate()
        {
            estadoAtual.UpdateEstados();
            VerificaChao();
            AplicaMovimento();

        }

        private void InputMovimento()
        {
            inputMovimentos = playerInput.Player.Movimento.ReadValue<Vector2>();
        }

        private void InputFlutuar()
        {
            if(podeFlutuar && inputMovimentos.y != 0)
            {
                flutuar = true;
            }
        }

        private void AplicaMovimento()
        {
            rb.velocity = calculaMovimentos;
        }

        IEnumerator EstaNoPulo()
        {
            estaPulando = true;
            yield return new WaitForSeconds(duracaoPulo);
            estaPulando = false;
            pediuPular = false;
        }

        private void VerificaChao()
        {
            hitNoChao_ray = Physics2D.Linecast(pontosChecagemSolo[0].position, pontosChecagemSolo[1].position, solo_lm);

            if(hitNoChao_ray.collider == null)
            {
                estaNoChao = false;
            }
            else
            {
                estaNoChao = true;
            }
            //Debug.Log(estaNoChao);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Flutuar"))
            {
                podeFlutuar = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Flutuar"))
            {
                podeFlutuar = false;
                flutuar = false;
            }
        }
    }

}
