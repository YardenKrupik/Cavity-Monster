using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothGiftScript : MonoBehaviour
{

    public GameObject candy;
    public Sprite yellowTeeth;

    private GameObject newCandy;
    private bool isTouched = false;
    private float time = 0;
    private bool timeIsUp = false;
    private bool done = false;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isTouched && !done)
        {
            time += Time.deltaTime;
            if (time > 0.3f)
            {
                timeIsUp = true;
            }
        }

        if (timeIsUp && !done)
        {
            done = true;
            InstantiateCandy();
        }



    }

    // Check if player has touched the tooth

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player" && isTouched == false)
        {

            isTouched = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = yellowTeeth;

            audioManager.PlaySound(audioManager.tooth);

        }
    }

    // Instantiate candy after player has touched the tooth
    private void InstantiateCandy()
    {
        
        newCandy = Instantiate(candy, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1), Quaternion.identity);
        newCandy.AddComponent<Rigidbody2D>();
        newCandy.GetComponent<Rigidbody2D>().gravityScale = 1;
        newCandy.GetComponent<Rigidbody2D>().freezeRotation = true;
        newCandy.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        newCandy.GetComponent<Rigidbody2D>().sleepMode = RigidbodySleepMode2D.NeverSleep;
        newCandy.GetComponent<Rigidbody2D>().interpolation = RigidbodyInterpolation2D.Interpolate;
    }

}
