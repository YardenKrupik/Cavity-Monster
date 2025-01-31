using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HShiftPlatform : MonoBehaviour  // Platform that shifts back and forth Horizontally using sin
{
    public float speed = 1f;
    public float distance = 1f;
    private float xInitial;
    private float x;
    private float y;
    private float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        xInitial = transform.position.x;
        x = xInitial;
        y = transform.position.y;

    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        x = xInitial + distance * Mathf.Sin(speed * time);
        transform.position = new Vector2(x, y);
    }

    // Player sticks to the platform

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" )
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
