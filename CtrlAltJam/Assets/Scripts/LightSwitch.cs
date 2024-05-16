using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] GameObject e;
    bool active = false;

    public delegate void MyDelegate();
    MyDelegate ligarApagar;

    [SerializeField] Light_Project[] luzes;

    private void Start()
    {
        foreach(Light_Project l in luzes)
        {
            ligarApagar += l.AcenderApagar;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (!active)
            return;

        if (Input.GetKeyDown(KeyCode.E) && ligarApagar != null)
        {
            ligarApagar();
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            e.SetActive(true);
            active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            e.SetActive(false);
            active = false;
        }
    }
}
