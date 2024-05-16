using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FonteDeLuz : MonoBehaviour
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

    private void Start()
    {
        luz.gameObject.SetActive(aceso);
    }

    public void Troca()
    {
        if (aceso)
        {
            luz.gameObject.SetActive(false);
            aceso = false;
        }
        else
        {
            luz.gameObject.SetActive(true);
            aceso = true;
        }
    }
    
}
