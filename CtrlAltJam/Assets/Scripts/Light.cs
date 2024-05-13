using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    [SerializeField] bool aceso = false;


    //FIXME: Isso é apenas um teste
    SpriteRenderer sp;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        sp.enabled = aceso;
    }


    public void AcenderApagar()
    {
        aceso = !aceso;
        sp.enabled = aceso;
    }

}
