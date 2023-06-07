using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeCheckScript : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        BossScript.boss.ChangeDirection();
    }

}
