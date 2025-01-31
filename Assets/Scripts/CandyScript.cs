using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyScript : MonoBehaviour
{

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // using the script from GameControl
    private GameControl gameControl;

    // Start is called before the first frame update
    void Start()
    {
        gameControl = GameObject.Find("Player").GetComponent<GameControl>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // When the player touches the candy, the candy is destroyed and the player collects the candy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            audioManager.PlaySound(audioManager.collect);
            gameControl.AddCandy();
            Destroy(gameObject);
        }
    }
}
