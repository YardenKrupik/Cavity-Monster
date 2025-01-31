using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothbrushScript : MonoBehaviour
{
    public float speed = 1f;
    public float distance = 1f;
    private float yInitial;
    private float x;
    private float y;
    private float time = 0f;
    private float time2 = 0f;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        yInitial = transform.position.y;
        x = transform.position.x;
        y = yInitial;

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        time2 += Time.deltaTime;


        y = yInitial + distance * Mathf.Sin(speed * time);
        transform.position = new Vector2(x, y);

        if (time2 > 2f)
        {
            audioManager.PlaySoundAtPoint(audioManager.toothbrush, gameObject.transform.position);
            time2 = 0f;
        }

    }


}
