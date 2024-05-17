using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Luz
{
    public class Temporaria : Comum
    {
        [SerializeField] private float TempoParaApagar;

        protected override void Acender()
        {
            base.Acender();
            StartCoroutine(DesligaLuz());
        }

        protected override void Apagar()
        {
            base.Apagar();
            StopCoroutine(DesligaLuz());
        }

        private IEnumerator DesligaLuz()
        {
            yield return new WaitForSeconds(TempoParaApagar);
            Apagar();
        }
    }

}

