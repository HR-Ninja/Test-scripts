using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 5f;

    private float xDir;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput(); 
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(xDir * speed, rb.velocity.y);
    }


    void GetInput()
    {
        xDir = Input.GetAxisRaw("Horizontal");
    }

}
