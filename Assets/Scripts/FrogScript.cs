using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class FrogScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float jumpForce;
    
    bool jmp = false;

    Rigidbody2D rb2d;
    Animator anim;

    SpriteRenderer spr;

    void Start()
    {

        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("speedY", rb2d.velocity.y);

       

        if (!jmp)
        {
            StartCoroutine(FrogJump());
        }
       
    }

    IEnumerator FrogJump()
    {
        jmp = true;
        yield return new WaitForSeconds(3.5f);
        // Inverte a direção do inimigo
        speed = speed * -1;


        // Inverte o sprite do inimigo
        spr.flipX = !spr.flipX;
        rb2d.AddForce(new Vector2(speed, jumpForce), ForceMode2D.Impulse);

        jmp = false;
    }





}

