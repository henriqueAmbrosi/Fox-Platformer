using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class BossScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 0;
    public GameObject edgeCheck;
    public GameObject bear;

    RaycastHit2D groundInfo;
    float rayDistance = 1;
    public int hp;
    public GameObject enemyDeathFeedback;
    BoxCollider2D[] colliders2D;
    SpriteRenderer spriteRenderer;
    Animator animator;
    public static BossScript boss;


    private void Start()
    {
        boss = this;
        colliders2D = GetComponents<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector2.left * speed * Time.deltaTime);

        groundInfo = Physics2D.Raycast(edgeCheck.transform.position, Vector2.down, rayDistance);
        
        if (!groundInfo.collider)
        {
            // Inverte a direção do inimigo
            speed = speed * -1;

            // Inverte o sprite do inimigo
            Vector3 tempScale = transform.localScale;
            tempScale.x = -tempScale.x;
            transform.localScale = tempScale;
        }
    }

    void toggleBossOpacity()
    {
        spriteRenderer.enabled = !spriteRenderer.enabled;   
    }

    public void EnableBoss()
    {
        speed = 6;
        animator.Play("BearWalk");
    }

    IEnumerator ImunityTime()
    {
        for(int i = 0; i < colliders2D.Length; i++)
        {
            colliders2D[i].enabled = false;
        }
        InvokeRepeating("toggleBossOpacity", 0.5f, 0.5f);
        yield return 4.0f;
        CancelInvoke();

        for (int i = 0; i < colliders2D.Length; i++)
        {
            colliders2D[i].enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hp--;
            if (hp <= 0)
            {
                Instantiate(enemyDeathFeedback, bear.transform.position, bear.transform.rotation);
                Destroy(bear);
            }
            else
            {
                StartCoroutine(ImunityTime());
            }
        }
    }
}
