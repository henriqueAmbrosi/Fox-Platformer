using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossVisionScript : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
            BossScript.boss.enraged = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BossScript.boss.enraged = false;
        }
    }

}
