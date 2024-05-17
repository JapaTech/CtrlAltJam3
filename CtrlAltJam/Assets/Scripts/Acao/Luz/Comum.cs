using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Luz
{
    public class Comum : MonoBehaviour
    {
        [SerializeField] private bool aceso;
        [SerializeField] private Light2D luz;

        protected bool Aceso
        {
            set
            {
                aceso = value;
            }
        }

        protected virtual void Start()
        {
            if (aceso)
            {
                Acender();
            }
            else
            {
                Apagar();
            }
        }

        public void Troca()
        {
            if (aceso)
            {
                Apagar();
            }
            else
            {
                Acender();
            }
        }

        protected virtual void Acender()
        {
            luz.gameObject.SetActive(true);
            aceso = true;
        }

        protected virtual void Apagar()
        {
            luz.gameObject.SetActive(false);
            aceso = false;
        }

    }
}

