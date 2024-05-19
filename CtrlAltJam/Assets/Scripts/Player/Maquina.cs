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
        [SerializeField] private Transform origemEncostoParede;
        [SerializeField] private Vector2 encostadoParede;
        RaycastHit2D bateuNaParede_rh;

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

        //Acoes
        [SerializeField] private Transform inicioAcao;
        [SerializeField] float raioAcao;
        [SerializeField] float distanciaAcao;
        [SerializeField] LayerMask acao_lm;
        RaycastHit2D acaoDetectada_rh;

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
            playerInput.Player.Acao.canceled += TentaExecutarAcao;
        }

        private void OnDisable()
        {
            playerInput.Player.Pulo.performed -= Pulo_performed;
            playerInput.Player.Acao.canceled -= TentaExecutarAcao;
        }

        private void Pulo_performed(InputAction.CallbackContext obj)
        {
            if (estaNoChao)
            {
                pediuPular = true;
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
            EncostadoNaParede();
            AplicaMovimento();
        }

        private void LateUpdate()
        {
            OlhaParaDirecao();
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

        private void OlhaParaDirecao()
        {
            if(inputMovimentos.x != 0)
            {
                olhaParaDirecao.x = inputMovimentos.x;
            }

            tr.right = olhaParaDirecao;
        }

        private void EncostadoNaParede()
        {
            if(Physics2D.OverlapBox(origemEncostoParede.position, encostadoParede, 0, solo_lm))
            {
                if(inputMovimentos.x > 0 && calculaMovimentos.x > 0)
                {
                    calculaMovimentos.x = 0;
                }
                else if (inputMovimentos.x < 0 && calculaMovimentos.x < 0)
                {
                    calculaMovimentos.x = 0;
                }
                
            }
        }

        private void AplicaMovimento()
        {
            if(calculaMovimentos.y < -15)
            {
                calculaMovimentos.y = -15;
            }
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

        private void TentaExecutarAcao(InputAction.CallbackContext obj)
        {
            Debug.Log("Tentou Executar acao");
            acaoDetectada_rh = Physics2D.CircleCast(inicioAcao.position, raioAcao, Vector2.right, distanciaAcao, acao_lm);

            if (acaoDetectada_rh)
            Debug.Log(acaoDetectada_rh.transform.name);

            if (acaoDetectada_rh)
            {
                acaoDetectada_rh.transform.gameObject.GetComponent<Acoes>().ExecutaAcao();
            }
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(inicioAcao.position, raioAcao);
            Gizmos.DrawLine(inicioAcao.position, inicioAcao.position + Vector3.right * distanciaAcao);

            Gizmos.color = Color.gray;
            Gizmos.DrawWireCube(origemEncostoParede.position, encostadoParede);
        }
    }

}
