using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryLight : Light_Project
{
    //Luz temporária
    [SerializeField] float tempoApagado = 5;
    public override void AcenderApagar()
    {
        Aceso = false;
        StartCoroutine(nameof(esperaTempo));
    }

    IEnumerator esperaTempo()
    {
        yield return new WaitForSeconds(tempoApagado);
        Aceso = true;

    }
}
