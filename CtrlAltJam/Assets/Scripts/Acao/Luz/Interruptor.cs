using UnityEngine;

public class Interruptor : MonoBehaviour, Acoes
{
    [SerializeField] private FonteDeLuz[] luzes;

    public void ExecutaAcao()
    {
        foreach (FonteDeLuz luz in luzes)
        {
            luz.Troca();
        }
    }

}
