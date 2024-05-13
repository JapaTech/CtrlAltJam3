using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed, jump;
    [SerializeField] LayerMask layerChao;
    Rigidbody2D rb;
    float input;
    Collider2D col;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        bool noChao = isGrounded();
        input = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && noChao)
        {
            rb.velocity = new Vector2(rb.velocity.x, jump);
        }

    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(input * speed, rb.velocity.y);
    }

    bool isGrounded()
    {
        float extraHeight = 0.1f;
        bool retorno = Physics2D.BoxCast(col.bounds.center, new Vector2(col.bounds.size.x - 0.005f, col.bounds.size.y), 0, Vector2.down, extraHeight, layerChao);
        return retorno;
    }
}
