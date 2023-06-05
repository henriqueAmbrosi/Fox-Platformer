using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class LeverScript : MonoBehaviour
{

    public GameObject[] doors;
    public Sprite nextStepLever;
    public AudioSource toggleSfx;
    public bool isSingleUse = false;

    bool disabled = false;


    SpriteRenderer spr;

    bool isInsideTrigger = false;

    void Start()
    {

        spr = GetComponent<SpriteRenderer>();
        toggleSfx = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInsideTrigger && !disabled)
        {
            GetComponentsInChildren<SpriteRenderer>()[1].enabled = true;
            // Toggle doors and lever state
            if (Input.GetButtonDown("Submit"))
            {
                toggleSfx.Play();
                Sprite aux = spr.sprite;
                spr.sprite = nextStepLever;
                nextStepLever = aux;
                for (int i = 0; i < doors.Length; i++)
                {
                    doors[i].SetActive(!doors[i].activeSelf);
                }

                if (isSingleUse)
                {
                    if(BossScript.boss)
                        BossScript.boss.EnableBoss();
                    disabled = true;
                    GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
                }
            }
        }
        else
        {
            GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!disabled)
        {
            isInsideTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInsideTrigger = false;
    }
}
