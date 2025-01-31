using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovements : MonoBehaviour
{

    private Rigidbody2D rigidbody;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public Sprite monsterIdle1;
    public Sprite monsterIdle2;
    public Sprite monsterJump;
    public Sprite monsterHurt;

    private float horizontal;
    private float speed = 8f;
    private float jumpForce = 14f;

    private float time = 0f;
    private float time2 = 0f;
    private bool isHurt = false;


    private Sprite[] monsterSprites = new Sprite[4];
    private int monsterIndex;

    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        monsterSprites[0] = monsterIdle1;
        monsterSprites[1] = monsterIdle2;
        monsterSprites[2] = monsterJump;
        monsterSprites[3] = monsterHurt;

        gameObject.GetComponent<SpriteRenderer>().sprite = monsterSprites[0];
        monsterIndex = 0;

    }

    private void Update()
    {

        horizontal = Input.GetAxisRaw("Horizontal");

        // Jump
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && IsOnGround())
        {
            moveUp();

        }

        if ((Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)) && rigidbody.velocity.y > 0f)
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, rigidbody.velocity.y * 0.5f);

        }

        if (!IsOnGround() && !isHurt)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = monsterSprites[2];
            monsterIndex = 2;

        }

        // back to idle mode

        if (IsOnGround() && !isHurt)
        {
            if (monsterIndex == 2)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = monsterSprites[0];
                monsterIndex = 0;
            }

        }

        // Walk

        if (inMoveOnGround() && !isHurt)
        {
            time2 += Time.deltaTime;
            if (time2 > 0.2f)
            {
                audioManager.PlaySound(audioManager.walk);
                time2 = 0f;

            }
                
            if (monsterIndex == 0)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = monsterSprites[1];
                monsterIndex = 1;

            }
            else if (monsterIndex == 1)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = monsterSprites[0];
                monsterIndex = 0;

            }
        }

        // Hurt
        if(isHurt)
        {

            time += Time.deltaTime;
            if (time > 0.3f)
            {
                isHurt = false;
                gameObject.GetComponent<SpriteRenderer>().sprite = monsterSprites[0];
                monsterIndex = 0;
                time = 0;
            }
        }

        if (gameObject.transform.position.y < -7)
        {
            getsHurt();
            getsHurt();

            if(SceneManager.GetActiveScene().name == "Level1")
            {
                gameObject.transform.position = new Vector2(-7, 0);
            }
            else
            {
                gameObject.transform.position = new Vector2(-15, 0);
            }
            
        }



    }


    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector2(horizontal * speed, rigidbody.velocity.y);
    }


    // Checks if the player is on the ground
    private bool IsOnGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);


    }


    // Makes the player jump
    private void moveUp()
    {

        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpForce);
        audioManager.PlaySound(audioManager.jump);

    }

    // Checks if the player is moving on the ground
    private bool inMoveOnGround()
    {
        return rigidbody.velocity.x != 0 && IsOnGround();
    }

    // Checks if the player is supposed to get hurt
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "DangerSquare" || collision.gameObject.name == "Bullet(Clone)")
        {
            getsHurt();
            
        }

    }

    // Makes the player get hurt (Muhaha)

    private void getsHurt()
    {
        isHurt = true;
        GameObject.Find("Player").GetComponent<GameControl>().GotHit();
        audioManager.PlaySound(audioManager.hit);
        gameObject.GetComponent<SpriteRenderer>().sprite = monsterSprites[3];
        monsterIndex = 3;
    }

}
