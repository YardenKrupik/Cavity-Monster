using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VShiftPlatform : MonoBehaviour
{
    public float speed = 1f;
    public float distance = 1f;
    private float yInitial;
    private float x;
    private float y;
    private float time = 0f;


    // Start is called before the first frame update
    void Start()
    {
        yInitial = transform.position.y;
        x = transform.position.x;
        y = yInitial;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        y = yInitial + distance * Mathf.Sin(speed * time);
        transform.position = new Vector2(x, y);
    }

    // Player sticks to the platform

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.collider.transform.SetParent(null);
        }
    }

}


