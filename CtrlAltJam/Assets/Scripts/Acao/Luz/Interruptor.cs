using UnityEngine;
using Luz;

public class Interruptor : MonoBehaviour, Acoes
{
    [SerializeField] private Comum[] luzes;

    public void ExecutaAcao()
    {
        foreach (Comum luz in luzes)
        {
            luz.Troca();
        }
    }

}
