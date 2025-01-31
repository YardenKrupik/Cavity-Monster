using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    private GameObject newBullet;

    public Transform roof;
    public LayerMask roofLayer;
    private bool firstTap = false;
    private bool isFree;

    private float timer = 0f;
    private float x;
    private float y;

    public Sprite fluorideIdle;
    public Sprite fluorideShoot;
    public Sprite fluorideHurt;
    private bool shootingFace;

    private SpriteRenderer spriteRenderer;

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = fluorideIdle;

        x = transform.position.x;
        y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;
        shoot();

        // Change sprite to idle after shooting

        if (shootingFace && timer > 0.5f)
        {
            spriteRenderer.sprite = fluorideIdle;
            shootingFace = false;
        }

        // Check if player is on top of the shooter

        if (!firstTap && HasPlayerOnTop())
        {
            firstTap = true;
            ShooterGotHurt();
        }

        // Check if player is not on top of the shooter after first tap
        if (firstTap && !HasPlayerOnTop())
        {
            isFree = true;
        }

        // Check if player is on top of the shooter again

        if (isFree && HasPlayerOnTop() )
        {

            ShooterGotHurt();
            audioManager.PlaySound(audioManager.killFluoride);
            isFree = false;
            firstTap = false;
            Destroy(gameObject);
        }
    }

    // Shoot bullet
    private void shoot()
    {

        if (timer > 5f)
        {
            spriteRenderer.sprite = fluorideShoot;
            shootingFace = true;
            newBullet = Instantiate(bullet, new Vector2(x-2,y), transform.rotation);
            audioManager.PlaySoundAtPoint(audioManager.shoot, gameObject.transform.position);
            Destroy(newBullet, 1f);
            timer = 0f;
        }

    }

    // Check if player is on top of the shooter
    private bool HasPlayerOnTop()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(roof.position, Vector2.up, 0.2f, roofLayer);
        return hit.collider != null;
    }

    // Change sprite to hurt
    private void ShooterGotHurt()
    {
        spriteRenderer.sprite = fluorideHurt;
        audioManager.PlaySound(audioManager.hitFluorid);

    }


}
