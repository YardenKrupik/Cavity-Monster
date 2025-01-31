using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour

{

    private Rigidbody2D rb;
    private float force = 20f;
    public float addForce = 0.04f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {

        rb.velocity = new Vector2(-force, 0);

    }

    // Destroy bullet when it hits something, but not when it goes out
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.transform.name != "Fluoride")
        {
            Destroy(gameObject);
        }

    }
}
