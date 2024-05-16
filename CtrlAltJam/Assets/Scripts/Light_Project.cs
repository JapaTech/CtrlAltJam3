using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_Project : MonoBehaviour
{
    [SerializeField] bool aceso = false;
    [SerializeField] Color corAceso = Color.yellow;
    
    protected bool Aceso
    {
        set
        {
            aceso = value;
            sp.color = getColor();
        }
    }

    protected SpriteRenderer sp;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        Aceso = aceso;
    }

    public virtual void AcenderApagar()
    {
        Aceso = !aceso;
    }

    Color getColor()
    {
        if (aceso)
        {
            return corAceso;
        }
        return Color.white;
    }

}
