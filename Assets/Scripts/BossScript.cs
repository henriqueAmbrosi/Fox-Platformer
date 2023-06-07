using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class BossScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 0;
    public GameObject bear;

    public int hp;
    public GameObject enemyDeathFeedback;
    BoxCollider2D[] colliders2D;
    Animator animator;
    public static BossScript boss;
    bool bossEnabled = false;
    public bool enraged = false;


    private void Start()
    {
        boss = this;
        colliders2D = GetComponents<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossEnabled)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            

            Vector2 visionDirection;
            if (transform.localScale.x > 0)
            {
                visionDirection = Vector2.left;
            }
            else
            {
                visionDirection = Vector2.right;
            }

        
            if (enraged)
            {
                setSpeed(10);
                animator.SetBool("Enraged", true);
            }
            else 
            {
                setSpeed(6);
                animator.SetBool("Enraged", false);
            }
        }
    }

    void setSpeed(float spd)
    {
        if (speed < 0)
            speed = -spd;
        else
            speed = spd;
    }

    public void EnableBoss()
    {
        bossEnabled = true;
        speed = 6;
        animator.Play("BearWalk");
    }

    IEnumerator ImunityTime()
    {
        for(int i = 0; i < colliders2D.Length; i++)
        {
            colliders2D[i].enabled = false;
        }
        animator.SetBool("Imune", true);
        setSpeed(10);
        yield return new WaitForSeconds(4.0f);


        for (int i = 0; i < colliders2D.Length; i++)
        {
            colliders2D[i].enabled = true;
        }
        animator.SetBool("Imune", false);
        setSpeed(6);
    }

    public void ChangeDirection()
    {
        // Inverte a direção do inimigo
        speed = speed * -1;

        // Inverte o sprite do inimigo
        Vector3 tempScale = transform.localScale;
        tempScale.x = -tempScale.x;
        transform.localScale = tempScale;
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
