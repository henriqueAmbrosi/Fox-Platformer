using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public GameObject itemFeedback;
    public GameObject enemyDeathFeedback;
    public GameObject gem;
    public AudioClip jumpSFX, pickupsSFX;

    float horizontal;
    bool jmp = false;
    bool canControlPlayer = true;
    Rigidbody2D rb2d;
    SpriteRenderer spr;
    Animator anim;
    AudioSource audioS;


    // Start is called before the first frame update
    void Start()
    {

      rb2d = GetComponent<Rigidbody2D>();
	  spr = GetComponent<SpriteRenderer>();
	  anim = GetComponent<Animator>();
      audioS = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canControlPlayer)
        {
            horizontal = Input.GetAxis("Horizontal");
            rb2d.velocity = new Vector2(horizontal * speed, rb2d.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && rb2d.velocity.y < 0.1f && rb2d.velocity.y > -0.1f)
        {
            jmp = true;
            audioS.PlayOneShot(jumpSFX, 0.2f);
        }

        anim.SetFloat("speed", math.abs(horizontal));
        anim.SetFloat("speedY", rb2d.velocity.y);

        if (horizontal < 0)
        {
            spr.flipX = true;
        }
        else if (horizontal > 0)
        {
            spr.flipX = false;
        } 
        
    }

    private void FixedUpdate()
    {
        if (jmp)
        {
            rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jmp = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickUp"))
        {
            audioS.PlayOneShot(pickupsSFX);
            GameManager.gm.AddGem();
            Destroy(collision.gameObject);
            Instantiate(itemFeedback, collision.transform.position, collision.transform.rotation);
        }

        if (collision.CompareTag("Enemy"))
        {
            StartCoroutine(PlayerDeath());
        }
    }

    IEnumerator PlayerDeath()
    {
        rb2d.velocity = Vector2.zero;
        rb2d.isKinematic = true;
        canControlPlayer = false;
        GetComponent<CapsuleCollider2D>().enabled = false;
        anim.SetTrigger("playerDeath");
        yield return new WaitForSeconds(2.5f);
        GameManager.gm.ReloadScene();
    }

    IEnumerator spawnGemOnEnemyDeath(Vector3 position, Quaternion rotation)
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(gem, position, rotation);
    }

    // Função para matar o inimigo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            jmp = true;
            Destroy(collision.gameObject);
            Instantiate(enemyDeathFeedback, collision.transform.position, collision.transform.rotation);
            StartCoroutine(spawnGemOnEnemyDeath(collision.transform.position, collision.transform.rotation));
        }
    }
}
